using ChomikBot_Framework.bot;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Timers;
using System.Linq;
using ChomikBot_.forms;

namespace ChomikBot_ {
    public partial class fileManager : MaterialForm {

        public static fileManager _filemanager;
        public fileManager() {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightGreen500, Primary.LightGreen600, Primary.LightBlue200, Accent.LightBlue200, TextShade.WHITE);
            _filemanager = this;
        }

        private static List<string> lastLoaded;
        private void fileManager_Load(object sender, EventArgs e) {

            lastLoaded = remote_file.castTreeView();
            foreach (string elem in lastLoaded) {
                drzewko.Nodes.Add(new TreeNode(elem));
            }

            var timer = new System.Timers.Timer(7500);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;

            /*
            TreeNode treeNode = new TreeNode("Windows");
            drzewko.Nodes.Add(treeNode);

            TreeNode node2 = new TreeNode("C#");
            TreeNode node3 = new TreeNode("VB.NET");
            TreeNode[] array = new TreeNode[] { node2, node3 };

            treeNode = new TreeNode("Dot Net Perls", array);
            drzewko.Nodes.Add(treeNode);
            */
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e) {
            List<string> newcast = remote_file.castTreeView();

            if (!new HashSet<string>(lastLoaded).SetEquals(newcast)) {
                fileManager._filemanager.drzewko.Invoke(new MethodInvoker(delegate { fileManager._filemanager.drzewko.Nodes.Clear(); }));
                lastLoaded = newcast;
                foreach (string elem in lastLoaded) {
                    fileManager._filemanager.drzewko.Invoke(new MethodInvoker(delegate { fileManager._filemanager.drzewko.Nodes.Add(new TreeNode(elem)); }));
                }
            }
        }

        // context menu
        private void drzewko_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                // Select the clicked node
                drzewko.SelectedNode = drzewko.GetNodeAt(e.X, e.Y);

                if (drzewko.SelectedNode != null) {
                    contextMenu.Show(drzewko, e.Location);
                }
            }
        }
         
        private void uploadBtn_Click(object sender, EventArgs e) {
            try {
                // user input
                string newDir = Interaction.InputBox("Wprowadź nazwę dla nowego folderu", "", "", 0, 0);

                // fix for cross thread operation
                string crossFix1 = drzewko.SelectedNode.Text;
                string crossFix2 = newDir;
                Task.Factory.StartNew(() => remote_file.setFolderPassword(remote_file.getSubTreeEnum(crossFix1), crossFix2));
            } catch {
                // error handler to be developed
            }
        }

        private void contextDirRemove_Click(object sender, EventArgs e) {
            try {
                string name = drzewko.SelectedNode.Text;
                Task.Factory.StartNew(() => remote_file.removeFolder(remote_file.getSubTreeEnum(name)));
                drzewko.Nodes.Remove(drzewko.SelectedNode);
            } catch {
                // error handler to be developed
            }
        }

        private void renameDirContext_Click(object sender, EventArgs e) {
            // user input
            string newName = Interaction.InputBox("Wprowadź nową nazwę", "", drzewko.SelectedNode.Text, 0, 0);

            // fix for cross thread operation
            string crossFix1 = drzewko.SelectedNode.Text;
            string crossFix2 = newName;
            Task.Factory.StartNew(() => remote_file.renameFolder(remote_file.getSubTreeEnum(crossFix1), crossFix2));

            drzewko.SelectedNode.Text = newName; // rename in treeview
        }

        private void drzewko_MouseDoubleClick(object sender, MouseEventArgs e) {
            // otwórz folder z plikami
            fileManagerSpecific.dirEnum = remote_file.getSubTreeEnum(drzewko.SelectedNode.Text);
            fileManagerSpecific frm = new fileManagerSpecific();
            frm.ShowDialog();
        }

        private void changePassCOntext_Click(object sender, EventArgs e) {
            try {
                // user input
                string newPass = Interaction.InputBox("Wprowadź nowe hasło", "", "", 0, 0);

                // fix for cross thread operation
                string crossFix1 = drzewko.SelectedNode.Text;
                string crossFix2 = newPass;
                Task.Factory.StartNew(() => remote_file.setFolderPassword(remote_file.getSubTreeEnum(crossFix1), crossFix2));
            } catch {
                // error handler to be developed
            }
        }

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {

        }
    }
}