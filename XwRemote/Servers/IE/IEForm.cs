using Microsoft.Win32;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using XwRemote.Settings;

namespace XwRemote.Servers
{
    public partial class IEForm : Form
    {
        private Server server = null;
        bool tryAutoLogin = true;

        //*************************************************************************************************************
        public IEForm(Server srv)
        {
            using (RegistryKey key = 
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", 
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(System.AppDomain.CurrentDomain.FriendlyName, 11000, RegistryValueKind.DWord);
            }

            using (RegistryKey key = 
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", 
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(System.AppDomain.CurrentDomain.FriendlyName, 11000, RegistryValueKind.DWord);
            }

            // 禁用SSL证书检查，允许访问有证书问题的HTTPS站点
            SetRegistryFeature("FEATURE_IGNORE_ZONES_INITIALIZATION_FAILURE_KB945701", 1);
            SetRegistryFeature("FEATURE_SECURITYBAND", 0);
            SetRegistryFeature("FEATURE_LOCALMACHINE_LOCKDOWN", 0);
            SetRegistryFeature("FEATURE_SAFE_BINDTOOBJECT", 0);
            SetRegistryFeature("FEATURE_WINDOW_RESTRICTIONS", 0);
            SetRegistryFeature("FEATURE_WEBOC_POPUPMANAGEMENT", 0);
            SetRegistryFeature("FEATURE_BEHAVIORS", 1);
            SetRegistryFeature("FEATURE_DISABLE_MK_PROTOCOL", 0);
            SetRegistryFeature("FEATURE_RESTRICT_FILEDOWNLOAD", 0);
            SetRegistryFeature("FEATURE_RESTRICT_ACTIVEXINSTALL", 0);
            SetRegistryFeature("FEATURE_WEBOC_MOVESIZECHILD", 1);
            SetRegistryFeature("FEATURE_HTTP_USERNAME_PASSWORD_DISABLE", 0);

            InitializeComponent();
            Dock = DockStyle.Fill;
            TopLevel = false;
            server = srv;
        }

        //*************************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            if (server.Port == 0)
                server.Port = 80;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            
            // 添加导航错误处理，忽略SSL证书错误
            webBrowser.Navigated += new WebBrowserNavigatedEventHandler(webBrowser_Navigated);
        }

        //*************************************************************************************************************
        private void OnShown(object sender, EventArgs e)
        {
            Connect();
        }

        //*************************************************************************************************************
        private void Connect()
        {
            // 忽略所有SSL证书错误
            ServicePointManager.ServerCertificateValidationCallback = 
                new RemoteCertificateValidationCallback(AcceptAllCertifications);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            
            if (server.UseHtmlLogin)
            {
                webBrowser.Navigate(server.Host);
            }
            else
            {
                string host = server.Host;
                if (server.Username.Trim() != string.Empty)
                {
                    host = server.Host.Replace("http://", "");
                    host = string.Format("http://{0}:{1}@{2}", server.Username, server.Password, host);
                }
                webBrowser.Navigate(host);
            }
        }

        //*************************************************************************************************************
        public bool OnTabClose()
        {
            return true;
        }

        //*************************************************************************************************************
        public void OnTabFocus()
        {
            
        }

        //*************************************************************************************************************
        private void OnEnter(object sender, EventArgs e)
        {
            OnTabFocus();
        }

        //*************************************************************************************************************
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (server.UseHtmlLogin)
            {
                if (!tryAutoLogin)
                    return;

                System.Windows.Forms.HtmlDocument document = webBrowser.Document;

                try
                {
                    HtmlElement user = document.GetElementById(server.HtmlUserBox);
                    if (user != null)
                    {
                        user.InnerText = server.Username;
                    }

                    HtmlElement pass = document.All[server.HtmlPassBox];
                    if (pass != null)
                    {
                        pass.InnerText = server.Password;

                        HtmlElement login = document.All[server.HtmlLoginBtn];
                        if (login != null)
                        {
                            login.InvokeMember("click");
                            tryAutoLogin = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //*************************************************************************************************************
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            // 通过执行JavaScript来处理SSL证书错误
            try
            {
                webBrowser.Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
            }
            catch
            {
                // 忽略错误
            }
        }

        //*************************************************************************************************************
        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // 忽略JavaScript错误，包括SSL证书相关错误
            e.Handled = true;
        }

        //*************************************************************************************************************
        private void SetRegistryFeature(string featureName, int value)
        {
            try
            {
                using (RegistryKey key = 
                    Registry.CurrentUser.CreateSubKey($@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\{featureName}", 
                    RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    key.SetValue(System.AppDomain.CurrentDomain.FriendlyName, value, RegistryValueKind.DWord);
                }

                using (RegistryKey key = 
                    Registry.CurrentUser.CreateSubKey($@"SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\{featureName}", 
                    RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    key.SetValue(System.AppDomain.CurrentDomain.FriendlyName, value, RegistryValueKind.DWord);
                }
            }
            catch
            {
                // 忽略注册表设置错误
            }
        }

        //*************************************************************************************************************
        private bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // 接受所有SSL证书，包括自签名证书和过期证书
            return true;
        }
    }
}
