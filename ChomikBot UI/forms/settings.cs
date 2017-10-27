using ChomikBot_Framework;
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
    public partial class settings : MaterialForm {

        public settings() {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightGreen500, Primary.LightGreen600, Primary.LightBlue200, Accent.LightBlue200, TextShade.WHITE);
        }

        private void saveSettings_Click(object sender, EventArgs e) {
            var ini = new iniHandler();
            helpers.validateSettings();

            ini.Write("dbc_username", dbcLogin.Text, "CREDENTIALS");
            ini.Write("dbc_password", dbcPassword.Text, "CREDENTIALS");
            ini.Write("chomik_username", chomikujLogin.Text, "CREDENTIALS");
            ini.Write("chomik_password", chomikujPassword.Text, "CREDENTIALS");

            this.Close();
        }

        private void settings_Load(object sender, EventArgs e) {
            var ini = new iniHandler();
            helpers.validateSettings();

            dbcLogin.Text = ini.Read("dbc_username", "CREDENTIALS");
            dbcPassword.Text = ini.Read("dbc_password", "CREDENTIALS");
            chomikujLogin.Text = ini.Read("chomik_username", "CREDENTIALS");
            chomikujPassword.Text = ini.Read("chomik_password", "CREDENTIALS");
        }


    }
}
