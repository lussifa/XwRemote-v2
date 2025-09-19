﻿using KRBTabControlNS.CustomTab;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public partial class SSHForm : Form
    {
        private PuttyAppPanel puttyPanel;
        private Server server = null;
        private string ShhKeyFile = "";
        private System.Threading.Timer resourceTimer;
        private int resourceRefreshInProgress = 0;
        private const int ResourceRefreshIntervalMs = 5000;
        private static readonly string ResourceMonitoringCommand = BuildResourceMonitoringCommand();
        
        //*************************************************************************************************************
        public SSHForm(Server srv)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            TopLevel = false;
            server = srv;
        }

        //*************************************************************************************************************
        private static string BuildResourceMonitoringCommand()
        {
            StringBuilder script = new StringBuilder();
            script.Append("cpu_line=$(top -bn1 2>/dev/null | grep \"Cpu(s)\" | head -n 1); ");
            script.Append("if [ -z \"$cpu_line\" ]; then cpu_line=$(top -bn1 | grep \"Cpu(s)\" | head -n 1); fi; ");
            script.Append("cpu=$(echo \"$cpu_line\" | awk -F\",\" \"{for (i=1;i<=NF;i++) if ($i ~ /id/) {gsub(/[^0-9.]/, \\\"\\\", $i); printf \\\"%.1f\\\", 100-$i; exit }}\"); ");
            script.Append("mem=$(free -m 2>/dev/null | awk \"NR==2 {if ($2==0) {printf \\\"N/A\\\"} else {printf \\\"%s/%sMB (%.0f%%)\\\", $3, $2, $3*100/$2}}\" ); ");
            script.Append("disk=$(df -hP / 2>/dev/null | awk \"NR==2 {print $3 \\\"/\\\" $2 \\\" (\\\" $5 \\\")\\\"}\"); ");
            script.Append("if [ -z \"$cpu\" ]; then cpu=\"N/A\"; fi; ");
            script.Append("if [ -z \"$mem\" ]; then mem=\"N/A\"; fi; ");
            script.Append("if [ -z \"$disk\" ]; then disk=\"N/A\"; fi; ");
            script.Append("printf \"CPU=%s%%\\nMEM=%s\\nDISK=%s\\n\" \"$cpu\" \"$mem\" \"$disk\"");
            return "LANG=C sh -lc '" + script.ToString() + "'";
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            if (server.Port == 0)
                server.Port = 22;

            SuspendLayout();

            buttonConnect.Visible = false;

            puttyPanel = new PuttyAppPanel(Main.config.GetValue("SSH_CORRECT_FOCUS").ToBoolOrDefault(true));
            puttyPanel.Parent = this;
            puttyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            puttyPanel.ApplicationCommand = @"putty\putty.exe";
            puttyPanel.ApplicationParameters = string.Empty;
            puttyPanel.Name = "puttyPanel";
            puttyPanel.Margin = new Padding(10);
            puttyPanel.TabIndex = 0;
            puttyPanel.Visible = false;
            Controls.Add(puttyPanel);

            statusBarPanel.BringToFront();
            UpdateResourceLabels("--", "--", "--");
            statusBarPanel.Visible = false;

            ResumeLayout();
        }

        //*************************************************************************************************************
        private void OnShown(object sender, EventArgs e)
        {
            Connect();
        }

        //*************************************************************************************************************
        public void Connect()
        {
            StopResourceMonitor();
            statusBarPanel.Visible = false;
            UpdateResourceLabels("--", "--", "--");

            // Auto accept the host key
            // using putty is starting to be complicated
            // but there is no real alternative
            // if we try to avoid some focus problems, we need to auto accept host keys
            if (Main.config.GetValue("SSH_ACCEPT_KEYS").ToBoolOrDefault(true))
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo procStartInfo =
                        new System.Diagnostics.ProcessStartInfo("cmd", $"/c echo y | putty\\plink -ssh {server.Username}@{server.Host} -P {server.Port} \"exit\"");

                    procStartInfo.RedirectStandardOutput = true;
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.CreateNoWindow = true;
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo = procStartInfo;
                    proc.Start();
                    //string result = proc.StandardOutput.ReadToEnd();
                }
                catch
                {
                    // do nothing, yes do nothing
                    // putty will ask for the key acceptance
                }
            }

            PuttyAppPanel.PuttyAppStartedCallback startedCallback = delegate ()
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    loadingCircle1.Visible = false;
                    loadingCircle1.Active = false;
                    statusLabel.Visible = false;
                    puttyPanel.Visible = true;
                    statusBarPanel.Visible = true;
                    if (!StartResourceMonitor())
                        UpdateResourceLabels("N/A", "N/A", "N/A");
                });
            };

            PuttyAppPanel.PuttyAppClosedCallback closedCallback = delegate (bool error)
            {
                try
                {
                    BeginInvoke((MethodInvoker)delegate
                    {
                        StopResourceMonitor();
                        statusBarPanel.Visible = false;
                        UpdateResourceLabels("--", "--", "--");

                        if (error)
                        {
                            SetStatusText("Connection closed");
                            buttonConnect.Visible = true;
                        }
                        else
                        {
                            ((KRBTabControl)(Parent.Parent)).TabPages.Remove((TabPageEx)Parent);
                            DeletePuttySession();
                        }
                    });
                }
                catch 
                { 
                    // yeah yeah! it will have to do
                }
            };

            puttyPanel.StartedCallback = startedCallback;
            puttyPanel.ClosedCallback = closedCallback;

            loadingCircle1.Active = true;
            loadingCircle1.InnerCircleRadius = 15;
            loadingCircle1.OuterCircleRadius = 30;
            loadingCircle1.SpokeThickness = 5;
            loadingCircle1.Top = (this.Height / 2) + 10;
            loadingCircle1.Left = (this.Width / 2) - 40;
            loadingCircle1.Visible = true;
            loadingCircle1.BringToFront();
            SetStatusText("Connecting...");
            buttonConnect.Visible = false;

            if (File.Exists("putty\\putty.exe"))
            {
                CreatePuttySession();
                puttyPanel.ApplicationParameters = string.Format(" -load \"{4}\" {0} -P {5} -l {1} -pw {2} {3}",
                    server.Host,
                    server.Username,
                    server.Password,
                    (server.SSH1) ? "-1" : "-2",
                    string.Format("XwRemote{0}", server.ID),
                    server.Port);

                ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectTrd), this);
            }
            else
            {
                loadingCircle1.Visible = false;
                SetStatusText("Putty not found");
            }
        }

        //*************************************************************************************************************
        static void ConnectTrd(object state)
        {
            SSHForm form = (SSHForm)state;
            try
            {
                // try with socket first, if connection cant be done, no point on trying with putty
                using (Socket stk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
                {
                    stk.Connect(form.server.Host, form.server.Port);
                    stk.Close();
                }

                form.BeginInvoke((MethodInvoker)delegate
                {
                    form.puttyPanel.Open();
                });
            }
            catch (Exception ex)
            {
                try
                {
                    form.BeginInvoke((MethodInvoker)delegate
                    {
                        form.SetStatusText(ex.Message);
                        form.loadingCircle1.Visible = false;
                        form.buttonConnect.Visible = true;
                    });
                }
                catch
                {
                    // Crap window gone?
                    // ok... don't show the message 
                    // there is no point since the user closed the tab already
                    // MessageBox.Show(ex.Message);
                }
            }
        }

        //*************************************************************************************************************
        private void SetStatusText(string txt)
        {
            statusLabel.Visible = true;
            statusLabel.Text = txt;
            statusLabel.Top = (this.Height / 2) - (statusLabel.Height / 2) - 10;
            statusLabel.Left = (this.Width / 2) - (statusLabel.Width / 2);
        }

        //*************************************************************************************************************
        public bool OnTabClose()
        {
            StopResourceMonitor();
            statusBarPanel.Visible = false;
            UpdateResourceLabels("--", "--", "--");
            DeletePuttySession();
            puttyPanel.Close();
            return true;
        }
     
        //*************************************************************************************************************
        public void OnTabFocus()
        {
            puttyPanel?.Focus();
        }

        //*************************************************************************************************************
        public void ResizeEnded()
        {

        }

        //*************************************************************************************************************
        private bool StartResourceMonitor()
        {
            if (server == null || string.IsNullOrWhiteSpace(server.Host))
                return false;

            string plinkPath = Path.Combine("putty", "plink.exe");
            if (!File.Exists(plinkPath))
                return false;

            if (string.IsNullOrWhiteSpace(server.Username))
                return false;

            if (string.IsNullOrEmpty(server.Password) && string.IsNullOrEmpty(ShhKeyFile))
                return false;

            StopResourceMonitor();

            resourceTimer = new System.Threading.Timer(
                RefreshResourceUsage,
                null,
                TimeSpan.Zero,
                TimeSpan.FromMilliseconds(ResourceRefreshIntervalMs));

            return true;
        }

        //*************************************************************************************************************
        private void StopResourceMonitor()
        {
            System.Threading.Timer timer = resourceTimer;
            resourceTimer = null;
            if (timer != null)
            {
                try
                {
                    timer.Dispose();
                }
                catch
                {
                }
            }

            Interlocked.Exchange(ref resourceRefreshInProgress, 0);
        }

        //*************************************************************************************************************
        private void RefreshResourceUsage(object state)
        {
            if (Interlocked.Exchange(ref resourceRefreshInProgress, 1) == 1)
                return;

            try
            {
                if (!IsHandleCreated || IsDisposed)
                    return;

                string output = ExecuteResourceCommand();
                if (string.IsNullOrWhiteSpace(output))
                {
                    BeginInvoke(new MethodInvoker(() => UpdateResourceLabels("N/A", "N/A", "N/A")));
                    return;
                }

                string cpu = null;
                string memory = null;
                string disk = null;

                using (StringReader reader = new StringReader(output))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("CPU=", StringComparison.OrdinalIgnoreCase))
                            cpu = line.Substring(4).Trim();
                        else if (line.StartsWith("MEM=", StringComparison.OrdinalIgnoreCase))
                            memory = line.Substring(4).Trim();
                        else if (line.StartsWith("DISK=", StringComparison.OrdinalIgnoreCase))
                            disk = line.Substring(5).Trim();
                    }
                }

                BeginInvoke(new MethodInvoker(() =>
                    UpdateResourceLabels(cpu ?? "N/A", memory ?? "N/A", disk ?? "N/A")));
            }
            catch
            {
                try
                {
                    BeginInvoke(new MethodInvoker(() => UpdateResourceLabels("N/A", "N/A", "N/A")));
                }
                catch
                {
                }
            }
            finally
            {
                Interlocked.Exchange(ref resourceRefreshInProgress, 0);
            }
        }

        //*************************************************************************************************************
        private string ExecuteResourceCommand()
        {
            try
            {
                string plinkPath = Path.Combine("putty", "plink.exe");
                if (!File.Exists(plinkPath))
                    return null;

                if (server == null || string.IsNullOrWhiteSpace(server.Host))
                    return null;

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = plinkPath,
                    Arguments = BuildPlinkArguments(ResourceMonitoringCommand),
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.StandardError.ReadToEnd();

                    if (!process.WaitForExit(7000))
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch
                        {
                        }
                        return null;
                    }

                    if (process.ExitCode != 0)
                        return null;

                    return output;
                }
            }
            catch
            {
                return null;
            }
        }

        //*************************************************************************************************************
        private string BuildPlinkArguments(string remoteCommand)
        {
            StringBuilder args = new StringBuilder();
            args.Append("-batch -ssh ");

            if (server.Port > 0)
                args.Append("-P ").Append(server.Port).Append(' ');

            if (!string.IsNullOrWhiteSpace(server.Username))
                args.Append("-l ").Append(QuoteArgument(server.Username)).Append(' ');

            if (!string.IsNullOrEmpty(server.Password))
                args.Append("-pw ").Append(QuoteArgument(server.Password)).Append(' ');

            if (!string.IsNullOrEmpty(ShhKeyFile))
                args.Append("-i ").Append(QuoteArgument(ShhKeyFile)).Append(' ');

            args.Append(server.SSH1 ? "-1 " : "-2 ");
            args.Append(QuoteArgument(server.Host));

            if (!string.IsNullOrEmpty(remoteCommand))
            {
                args.Append(' ');
                args.Append(QuoteArgument(remoteCommand));
            }

            return args.ToString();
        }

        //*************************************************************************************************************
        private static string QuoteArgument(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "\"\"";

            StringBuilder builder = new StringBuilder();
            builder.Append('"');

            int backslashes = 0;
            foreach (char c in value)
            {
                if (c == '\\')
                {
                    backslashes++;
                }
                else if (c == '"')
                {
                    builder.Append('\', backslashes * 2 + 1);
                    builder.Append('"');
                    backslashes = 0;
                }
                else
                {
                    if (backslashes > 0)
                    {
                        builder.Append('\', backslashes);
                        backslashes = 0;
                    }
                    builder.Append(c);
                }
            }

            if (backslashes > 0)
                builder.Append('\', backslashes * 2);

            builder.Append('"');
            return builder.ToString();
        }

        //*************************************************************************************************************
        private void UpdateResourceLabels(string cpu, string memory, string disk)
        {
            cpuStatusLabel.Text = $"CPU: {cpu}";
            memoryStatusLabel.Text = $"Memory: {memory}";
            diskStatusLabel.Text = $"Disk: {disk}";
        }

        //*************************************************************************************************************
        private void CreatePuttySession()
        {
            string subkey = string.Format("Software\\SimonTatham\\PuTTY\\Sessions\\XwRemote{0}", server.ID);
            RegistryKey key = Registry.CurrentUser.CreateSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key != null)
            {
                using (Font font = new Font("Consolas", 10))
                {
                    if (font.Name == "Consolas")
                        key.SetValue("Font", "Consolas", RegistryValueKind.String);
                    else
                        key.SetValue("Font", "Courier New", RegistryValueKind.String);
                }

                key.SetValue("AltF4", 0x00000000, RegistryValueKind.DWord);
                
                using (Graphics g = this.CreateGraphics())
                {
                    int currentDPI = (int)g.DpiX;
                    key.SetValue("FontHeight", Main.config.GetValue("DEFAULT_SSH_FONT_SIZE").ToIntOrDefault(10), 
                        RegistryValueKind.DWord);
                }
                
                key.SetValue("TerminalType", "xterm", RegistryValueKind.String);
                key.SetValue("TerminalSpeed", "38400,38400", RegistryValueKind.String);
                key.SetValue("TerminalModes", "INTR=A,QUIT=A,ERASE=A,KILL=A,EOF=A,EOL=A,EOL2=A,START=A,STOP=A,SUSP=A,DSUSP=A,REPRINT=A,WERASE=A,LNEXT=A,FLUSH=A,SWTCH=A,STATUS=A,DISCARD=A,IGNPAR=A,PARMRK=A,INPCK=A,ISTRIP=A,INLCR=A,IGNCR=A,ICRNL=A,IUCLC=A,IXON=A,IXANY=A,IXOFF=A,IMAXBEL=A,ISIG=A,ICANON=A,XCASE=A,ECHO=A,ECHOE=A,ECHOK=A,ECHONL=A,NOFLSH=A,TOSTOP=A,IEXTEN=A,ECHOCTL=A,ECHOKE=A,PENDIN=A,OPOST=A,OLCUC=A,ONLCR=A,OCRNL=A,ONOCR=A,ONLRET=A,CS7=A,CS8=A,PARENB=A,PARODD=A,", RegistryValueKind.String);
                key.SetValue("ProxyExcludeList", "", RegistryValueKind.String);
                key.SetValue("ProxyHost", "proxy", RegistryValueKind.String);
                key.SetValue("ProxyUsername", "", RegistryValueKind.String);
                key.SetValue("ProxyPassword", "", RegistryValueKind.String);
                key.SetValue("ProxyTelnetCommand", "connect %host %port\\n", RegistryValueKind.String);
                key.SetValue("Environment", "", RegistryValueKind.String);
                key.SetValue("UserName", "", RegistryValueKind.String);
                key.SetValue("LocalUserName", "", RegistryValueKind.String);
                key.SetValue("Cipher", "aes,blowfish,3des,WARN,arcfour,des", RegistryValueKind.String);
                key.SetValue("KEX", "dh-gex-sha1,dh-group14-sha1,dh-group1-sha1,rsa,WARN", RegistryValueKind.String);
                key.SetValue("RekeyBytes", "1G", RegistryValueKind.String);
                key.SetValue("GSSLibs", "gssapi32,sspi,custom", RegistryValueKind.String);
                key.SetValue("GSSCustom", "", RegistryValueKind.String);
                key.SetValue("LogHost", "", RegistryValueKind.String);

                if (server.SshKey == "")
                    key.SetValue("PublicKeyFile", "", RegistryValueKind.String);
                else
                {
                    ShhKeyFile = Path.GetTempFileName();
                    File.WriteAllText(ShhKeyFile, server.SshKey);
                    key.SetValue("PublicKeyFile", ShhKeyFile, RegistryValueKind.String);
                }
                key.SetValue("RemoteCommand", "", RegistryValueKind.String);
                key.SetValue("Answerback", "PuTTY", RegistryValueKind.String);
                key.SetValue("BellWaveFile", "", RegistryValueKind.String);
                key.SetValue("WinTitle", "", RegistryValueKind.String);
                key.SetValue("Colour0", "187,187,187", RegistryValueKind.String);
                key.SetValue("Colour1", "255,255,255", RegistryValueKind.String);
                key.SetValue("Colour2", "0,0,0", RegistryValueKind.String);
                key.SetValue("Colour3", "85,85,85", RegistryValueKind.String);
                key.SetValue("Colour4", "0,0,0", RegistryValueKind.String);
                key.SetValue("Colour5", "0,255,0", RegistryValueKind.String);
                key.SetValue("Colour6", "0,0,0", RegistryValueKind.String);
                key.SetValue("Colour7", "85,85,85", RegistryValueKind.String);
                key.SetValue("Colour8", "187,0,0", RegistryValueKind.String);
                key.SetValue("Colour9", "255,85,85", RegistryValueKind.String);
                key.SetValue("Colour10", "0,187,0", RegistryValueKind.String);
                key.SetValue("Colour11", "85,255,85", RegistryValueKind.String);
                key.SetValue("Colour12", "187,187,0", RegistryValueKind.String);
                key.SetValue("Colour13", "255,255,85", RegistryValueKind.String);
                key.SetValue("Colour14", "0,0,187", RegistryValueKind.String);
                key.SetValue("Colour15", "85,85,255", RegistryValueKind.String);
                key.SetValue("Colour16", "187,0,187", RegistryValueKind.String);
                key.SetValue("Colour17", "255,85,255", RegistryValueKind.String);
                key.SetValue("Colour18", "0,187,187", RegistryValueKind.String);
                key.SetValue("Colour19", "85,255,255", RegistryValueKind.String);
                key.SetValue("Colour20", "187,187,187", RegistryValueKind.String);
                key.SetValue("Colour21", "255,255,255", RegistryValueKind.String);
                key.SetValue("Wordness0", "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0", RegistryValueKind.String);
                key.SetValue("Wordness32", "0,1,2,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1", RegistryValueKind.String);
                key.SetValue("Wordness64", "1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,2", RegistryValueKind.String);
                key.SetValue("Wordness96", "1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1", RegistryValueKind.String);
                key.SetValue("Wordness128", "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1", RegistryValueKind.String);
                key.SetValue("Wordness160", "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1", RegistryValueKind.String);
                key.SetValue("Wordness192", "2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,2,2,2,2,2,2,2,2", RegistryValueKind.String);
                key.SetValue("Wordness224", "2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,2,2,2,2,2,2,2,2", RegistryValueKind.String);
                key.SetValue("LineCodePage", "UTF-8", RegistryValueKind.String);
                key.SetValue("Printer", "", RegistryValueKind.String);
                key.SetValue("X11Display", "", RegistryValueKind.String);
                key.SetValue("X11AuthFile", "", RegistryValueKind.String);
                key.SetValue("PortForwardings", "", RegistryValueKind.String);
                key.SetValue("BoldFont", "", RegistryValueKind.String);
                key.SetValue("WideFont", "", RegistryValueKind.String);
                key.SetValue("WideBoldFont", "", RegistryValueKind.String);
                key.SetValue("SerialLine", "COM1", RegistryValueKind.String);
                key.SetValue("WindowClass", "", RegistryValueKind.String);
                key.SetValue("Present", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("LogType", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("LogFlush", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("SSHLogOmitPasswords", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("SSHLogOmitData", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("PortNumber", 0x00000016, RegistryValueKind.DWord);
                key.SetValue("CloseOnExit", 0x00000002, RegistryValueKind.DWord);
                key.SetValue("WarnOnClose", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("PingInterval", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("PingIntervalSecs", 0x0000003c, RegistryValueKind.DWord);
                key.SetValue("TCPNoDelay", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("TCPKeepalives", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("AddressFamily", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ProxyDNS", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("ProxyLocalhost", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ProxyMethod", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ProxyPort", 0x00000050, RegistryValueKind.DWord);
                key.SetValue("UserNameFromEnvironment", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("NoPTY", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("Compression", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("TryAgent", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("AgentFwd", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("GssapiFwd", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ChangeUsername", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("RekeyTime", 0x0000003c, RegistryValueKind.DWord);
                key.SetValue("SshNoAuth", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("SshBanner", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("AuthTIS", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("AuthKI", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("AuthGSSAPI", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("SshNoShell", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("SshProt", 0x00000002, RegistryValueKind.DWord);
                key.SetValue("SSH2DES", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("RFCEnviron", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("PassiveTelnet", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BackspaceIsDelete", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("RXVTHomeEnd", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("LinuxFunctionKeys", 0x00000002, RegistryValueKind.DWord);
                key.SetValue("NoApplicationKeys", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("NoApplicationCursors", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("NoMouseReporting", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("NoRemoteResize", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("NoAltScreen", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("NoRemoteWinTitle", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("RemoteQTitleAction", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("NoDBackspace", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("NoRemoteCharset", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ApplicationCursorKeys", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ApplicationKeypad", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("NetHackKeypad", 0x00000000, RegistryValueKind.DWord);
                
                key.SetValue("AltSpace", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("AltOnly", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ComposeKey", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("CtrlAltKeys", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("TelnetKey", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("TelnetRet", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("LocalEcho", 0x00000002, RegistryValueKind.DWord);
                key.SetValue("LocalEdit", 0x00000002, RegistryValueKind.DWord);
                key.SetValue("AlwaysOnTop", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("FullScreenOnAltEnter", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("HideMousePtr", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("SunkenEdge", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("WindowBorder", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("CurType", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BlinkCur", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("Beep", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("BeepInd", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BellOverload", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("BellOverloadN", 0x00000005, RegistryValueKind.DWord);
                key.SetValue("BellOverloadT", 0x000007d0, RegistryValueKind.DWord);
                key.SetValue("BellOverloadS", 0x00001388, RegistryValueKind.DWord);
                key.SetValue("ScrollbackLines", 0x000000c8, RegistryValueKind.DWord);
                key.SetValue("DECOriginMode", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("AutoWrapMode", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("LFImpliesCR", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("CRImpliesLF", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("DisableArabicShaping", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("DisableBidi", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("WinNameAlways", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("TermWidth", 0x00000050, RegistryValueKind.DWord);
                key.SetValue("TermHeight", 0x00000018, RegistryValueKind.DWord);

                
                if (Main.config.GetValue("SSH_BOLD_FONT").ToBoolOrDefault(false))
                    key.SetValue("FontIsBold", 0x00000001, RegistryValueKind.DWord);
                else
                    key.SetValue("FontIsBold", 0x00000000, RegistryValueKind.DWord);

                key.SetValue("FontCharSet", 0x00000000, RegistryValueKind.DWord);

                key.SetValue("FontQuality", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("FontVTMode", 0x00000004, RegistryValueKind.DWord);
                key.SetValue("UseSystemColours", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("TryPalette", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ANSIColour", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("Xterm256Colour", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("BoldAsColour", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("RawCNP", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("PasteRTF", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("MouseIsXterm", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("MouseOverride", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("RectSelect", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("CJKAmbigWide", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("UTF8Override", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("CapsLockCyr", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ScrollBar", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ScrollBarFullScreen", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ScrollOnKey", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ScrollOnDisp", 0x00000f001, RegistryValueKind.DWord);
                key.SetValue("EraseToScrollback", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("LockSize", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BCE", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("BlinkText", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("X11Forward", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("X11AuthType", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("LocalPortAcceptAll", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("RemotePortAcceptAll", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugIgnore1", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugPlainPW1", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugRSA1", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugIgnore2", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugHMAC2", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugDeriveKey2", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugRSAPad2", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugPKSessID2", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugRekey2", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("BugMaxPkt2", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("StampUtmp", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("LoginShell", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("ScrollbarOnLeft", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ShadowBold", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("ShadowBoldOffset", 0x00000001, RegistryValueKind.DWord);
                key.SetValue("SerialSpeed", 0x00002580, RegistryValueKind.DWord);
                key.SetValue("SerialDataBits", 0x00000008, RegistryValueKind.DWord);
                key.SetValue("SerialStopHalfbits", 0x00000002, RegistryValueKind.DWord);
                key.SetValue("SerialParity", 0x00000000, RegistryValueKind.DWord);
                key.SetValue("SerialFlowControl", 0x00000001, RegistryValueKind.DWord);
                
                key.Close();
            }
        }

        //*************************************************************************************************************
        private void DeletePuttySession()
        {
            switch (server.SshTerminal)
            {
                case 1:
                    {
                        if (File.Exists(ShhKeyFile))
                            File.Delete(ShhKeyFile);

                        string subkey = string.Format("Software\\SimonTatham\\PuTTY\\Sessions\\XwRemote{0}", server.ID);
                        RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey);
                        if (key != null)
                        {
                            key.Close();
                            Registry.CurrentUser.DeleteSubKeyTree(subkey);
                        }
                    }
                    break;
                case 2:
                    {
                        
                    }
                    break;
            }
        }

        //*************************************************************************************************************
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }
    }
}
