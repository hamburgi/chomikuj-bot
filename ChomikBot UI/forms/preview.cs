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
using System.Windows.Forms;

namespace ChomikBot_UI {
    public partial class preview : MaterialForm {
        public preview() {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightGreen500, Primary.LightGreen600, Primary.LightBlue200, Accent.LightBlue200, TextShade.WHITE);
        }

        private void preview_Load(object sender, EventArgs e) {
            ((Control)previewBrowser).Enabled = false;
            previewBrowser.DocumentText = main.previewBody;
        }


    }
}
