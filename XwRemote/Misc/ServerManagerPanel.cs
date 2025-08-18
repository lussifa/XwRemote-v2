using System;
using System.Drawing;
using System.Windows.Forms;
using XwMaxLib.Extensions;
using XwRemote.Misc;
using XwRemote.Settings;

namespace XwRemote
{
    public partial class ServerManagerPanel : UserControl
    {
        private int SelectedTab = 1;
        public Server ConnectToThisServer = null;
        private Main mainForm = null;
        private string tmpSelectedNode = null;
        private string tmpSelectedLabel;
        private Point tmpScrollPost;

        public event EventHandler<Server> ServerSelected;

        //****************************************************************************************************
        public ServerManagerPanel(Main main)
        {
            InitializeComponent();
            mainForm = main;
            treeServers.KeyDown += new System.Windows.Forms.KeyEventHandler(tree_KeyDown);
        }

        //****************************************************************************************************
        private void OnLoad(object sender, EventArgs e)
        {
            LoadList();
        }

        //****************************************************************************************************
        public void LoadList()
        {
            if (tmpSelectedNode == null)
            {
                if (treeServers.SelectedNode != null)
                {
                    tmpSelectedNode = treeServers.SelectedNode.Name;
                    tmpSelectedLabel = treeServers.SelectedNode.Text;
                    // TreeView does not have HorizontalScroll and VerticalScroll properties
                    tmpScrollPost = new Point(0, 0);
                }
            }

            treeServers.Nodes.Clear();
            treeServers.ImageList = Main.myImageList;

            if (SelectedTab == 1) //Grouped
            {
                FilterGrouped.IsSelected = true;
                FilterOrdered.IsSelected = false;
                FilterFavorites.IsSelected = false;

                foreach (Group group in Main.config.groups)
                {
                    TreeNode groupNode = new TreeNode(group.Name);
                    groupNode.Name = "group_" + group.ID.ToString();
                    groupNode.Tag = group;
                    groupNode.ImageKey = "folder";
                    groupNode.SelectedImageKey = "folder";
                    treeServers.Nodes.Add(groupNode);

                    foreach (Server server in Main.servers)
                    {
                        if (server.GroupID != group.ID)
                            continue;
                        
                        TreeNode serverNode = new TreeNode(server.Name);
                        serverNode.Name = "server_" + server.ID.ToString();
                        serverNode.Tag = server;
                        serverNode.ImageKey = server.Type.ToString().ToLower();
                        serverNode.SelectedImageKey = server.Type.ToString().ToLower();
                        serverNode.ToolTipText = $"{server.Name}\n{server.Host}:{server.Port}";
                        groupNode.Nodes.Add(serverNode);
                    }

                    if (group.Expanded)
                        groupNode.Expand();
                }
            }
            else if (SelectedTab == 2) //Ordered
            {
                FilterGrouped.IsSelected = false;
                FilterOrdered.IsSelected = true;
                FilterFavorites.IsSelected = false;

                foreach (Server server in Main.servers)
                {
                    TreeNode serverNode = new TreeNode(server.Name);
                    serverNode.Name = "server_" + server.ID.ToString();
                    serverNode.Tag = server;
                    serverNode.ImageKey = server.Type.ToString().ToLower();
                    serverNode.SelectedImageKey = server.Type.ToString().ToLower();
                    serverNode.ToolTipText = $"{server.Name}\n{server.Host}:{server.Port}";
                    treeServers.Nodes.Add(serverNode);
                }
            }
            else if (SelectedTab == 3) //Favorites
            {
                FilterGrouped.IsSelected = false;
                FilterOrdered.IsSelected = false;
                FilterFavorites.IsSelected = true;

                foreach (Server server in Main.servers)
                {
                    if (!server.IsFavorite)
                        continue;
                    TreeNode serverNode = new TreeNode(server.Name);
                    serverNode.Name = "server_" + server.ID.ToString();
                    serverNode.Tag = server;
                    serverNode.ImageKey = server.Type.ToString().ToLower();
                    serverNode.SelectedImageKey = server.Type.ToString().ToLower();
                    serverNode.ToolTipText = $"{server.Name}\n{server.Host}:{server.Port}";
                    treeServers.Nodes.Add(serverNode);
                }
            }

            if (tmpSelectedNode != null)
            {
                TreeNode[] nodes = treeServers.Nodes.Find(tmpSelectedNode, true);
                if (nodes.Length > 0)
                {
                    treeServers.SelectedNode = nodes[0];
                    // Note: TreeView scroll position cannot be directly set in .NET Framework
                }
                tmpSelectedNode = null;
            }
        }

        //****************************************************************************************************
        private void tree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteServer_Click(sender, e);
            else if (e.KeyCode == Keys.F2)
                Edit();
            else if (e.KeyCode == Keys.Enter)
                Connect_Click(sender, e);
        }

        //****************************************************************************************************
        private void Edit()
        {
            if (treeServers.SelectedNode?.Tag is Server)
            {
                Server srv = (Server)treeServers.SelectedNode.Tag;
                EditServerById(srv, false);
            }
            else if (treeServers.SelectedNode?.Tag is Group)
            {
                Group grp = (Group)treeServers.SelectedNode.Tag;
                treeServers.SelectedNode.BeginEdit();
            }
        }

        //****************************************************************************************************
        private void EditServerById(Server server, bool copy)
        {
            server.Edit();
            LoadList();
        }

        //****************************************************************************************************
        private void Connect_Click(object sender, EventArgs e)
        {
            if (treeServers.SelectedNode?.Tag is Server)
            {
                Server server = ((Server)treeServers.SelectedNode.Tag);
                ServerSelected?.Invoke(this, server);
            }
        }

        //****************************************************************************************************
        private void treeServers_DoubleClick(object sender, EventArgs e)
        {
            Connect_Click(sender, e);
        }

        //****************************************************************************************************
        private void FilterGrouped_Click(object sender, EventArgs e)
        {
            SelectedTab = 1;
            LoadList();
        }

        //****************************************************************************************************
        private void FilterOrdered_Click(object sender, EventArgs e)
        {
            SelectedTab = 2;
            LoadList();
        }

        //****************************************************************************************************
        private void FilterFavorites_Click(object sender, EventArgs e)
        {
            SelectedTab = 3;
            LoadList();
        }

        //****************************************************************************************************
        private int GetSelectedGroup()
        {
            if (treeServers.SelectedNode?.Tag is Group)
                return ((Group)treeServers.SelectedNode.Tag).ID;
            else if (treeServers.SelectedNode?.Tag is Server)
                return ((Server)treeServers.SelectedNode.Tag).GroupID;
            else
                return 1;
        }

        //****************************************************************************************************
        private void NewRDP_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.RDP);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewFTP_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.FTP);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewSFTP_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.SFTP);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewS3_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.AWSS3);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewAzureFile_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.AZUREFILE);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void newSSH_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.SSH);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void NewVNC_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.VNC);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void newIE_Click(object sender, EventArgs e)
        {
            Server server = Server.GetServerInstance(ServerType.IE);
            server.GroupID = GetSelectedGroup();
            server.New();
            LoadList();
        }

        //****************************************************************************************************
        private void DeleteServer_Click(object sender, EventArgs e)
        {
            if (treeServers.SelectedNode?.Tag is Server)
            {
                Server srv = (Server)treeServers.SelectedNode.Tag;
                if (MessageBox.Show($"Are you sure you want to delete the server \"{srv.Name}\"?", "Delete?", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    Main.config.DeleteServer(srv);
                    LoadList();
                }
            }
        }

        //****************************************************************************************************
        private void EditServer_Click(object sender, EventArgs e)
        {
            Edit();
        }

        //****************************************************************************************************
        private void treeServers_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Group)
                Main.config.ExpandGroup((Group)e.Node.Tag);
        }

        //****************************************************************************************************
        private void treeServers_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is Group)
                Main.config.CollapseGroup((Group)e.Node.Tag);
        }

        //****************************************************************************************************
        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            // Search functionality can be implemented here
        }

        //****************************************************************************************************
        private void treeServers_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeServers.GetNodeAt(e.X, e.Y);
                if (node != null)
                {
                    treeServers.SelectedNode = node;
                    
                    if (node.Tag is Server)
                    {
                        contextServer.Show(treeServers, e.Location);
                    }
                    else if (node.Tag is Group)
                    {
                        contextGroup.Show(treeServers, e.Location);
                    }
                }
            }
        }

        //****************************************************************************************************
        private void AddGroup_Click(object sender, EventArgs e)
        {
            Group group = new Group();
            group.ParentID = GetSelectedGroup();
            group.New();
            LoadList();
        }

        //****************************************************************************************************
        private void RenameGroup_Click(object sender, EventArgs e)
        {
            if (treeServers.SelectedNode?.Tag is Group)
            {
                Group group = (Group)treeServers.SelectedNode.Tag;
                group.Edit();
                LoadList();
            }
        }

        //****************************************************************************************************
        private void DeleteGroup_Click(object sender, EventArgs e)
        {
            if (treeServers.SelectedNode?.Tag is Group)
            {
                Group group = (Group)treeServers.SelectedNode.Tag;
                if (MessageBox.Show($"Are you sure you want to delete the group \"{group.Name}\"?", "Delete?", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {
                    Main.config.DeleteGroup(group);
                    LoadList();
                }
            }
        }
    }
}