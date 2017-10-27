using ChomikBot_Framework.bot;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ChomikBot_.forms {
    public partial class fileManagerSpecific : MaterialForm {

        public static fileManagerSpecific _fileManagerSpecific;
        public fileManagerSpecific() {
            InitializeComponent();
           
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightGreen500, Primary.LightGreen600, Primary.LightBlue200, Accent.LightBlue200, TextShade.WHITE);

            _fileManagerSpecific = this;
        }

        public static int dirEnum;
        public static List<string> lastLoaded;
        private void fileManagerSpecific_Load(object sender, EventArgs e) {
            this.Text = "ZARZĄDZANIE PLIKAMI - " + remote_file.getSubTreeString(dirEnum);
            lastLoaded = remote_file.castFileView(dirEnum);

            int i = 0;
            int max = lastLoaded.Count;
            while (i != max) {
                filePanel.Controls.Add(new PictureBox {
                    Name = "misc_file_" + i,
                    Size = new Size(130, 130),
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = Properties.Resources.file_misc
                });
                i++;
            }
            // picture.BackgroundImageLayout = ImageLayout.Stretch;

            var timer = new System.Timers.Timer(7500);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e) {
            List<string> newcast = remote_file.castFileView(dirEnum);

            if (!new HashSet<string>(lastLoaded).SetEquals(newcast)) {

                fileManagerSpecific._fileManagerSpecific.filePanel.Invoke(new MethodInvoker(delegate { fileManagerSpecific._fileManagerSpecific.filePanel.Controls.Clear(); }));
                lastLoaded = remote_file.castFileView(dirEnum);

                int i = 0;
                int max = lastLoaded.Count;
                while (i != max) {
                    fileManagerSpecific._fileManagerSpecific.filePanel.Invoke(new MethodInvoker(delegate {
                        fileManagerSpecific._fileManagerSpecific.filePanel.Controls.Add(new PictureBox {
                            Name = "misc_file_" + i,
                            Size = new Size(130, 130),
                            BorderStyle = BorderStyle.FixedSingle,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Image = Properties.Resources.file_misc
                        });
                    }));
                    i++;
                }
            }

       }

       private void uploadBtn_Click(object sender, EventArgs e) {
           OpenFileDialog dialog = new OpenFileDialog();
           dialog.Filter = "All files (*.*)|*.*";
           dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
           dialog.Multiselect = true;
           dialog.Title = "Wybierz pliki";

           DialogResult dr = dialog.ShowDialog();
           if (dr == System.Windows.Forms.DialogResult.OK) {
               List<string> toUpload = new List<string>();
               int count = 0;
               foreach (String file in dialog.FileNames) {
                   toUpload.Add(file);
                   count++;
               }

               Task.Factory.StartNew(() => remote_file.uploadFile(toUpload, dirEnum));
               MessageBox.Show("Rozpoczęto wrzucanie " + count + " plików do folderu: " + remote_file.getSubTreeString(dirEnum));
           }
       }
   }
}
