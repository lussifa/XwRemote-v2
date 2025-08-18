namespace XwRemote.Settings
{
    public class Group
    {
        public int ID = 0;
        public string Name = "";
        public bool Expanded = false;
        public int ParentID = 0;

        public void New()
        {
            // Create new group dialog or inline editing
            Main.config.SaveGroup(this);
        }

        public void Edit()
        {
            // Edit group name - this will be handled by the UI
            // The actual editing is done through TreeView label editing
        }
    }
}
