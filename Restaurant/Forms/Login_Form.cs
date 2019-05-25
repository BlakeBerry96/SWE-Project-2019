using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant.Employees;

namespace Restaurant {
    public partial class Login_Form : Form {
        public Login_Form() {
            InitializeComponent();
            Username_TextBox.Text = Properties.Settings.Default.Last_Login;
        }

        private void Login_Button_Click(object sender, EventArgs e) {
            Login();
        }

        private void Password_TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                Login();
            }
        }

        private void Login() {
            var user = Username_TextBox.Text;
            var pass = Hash.ComputeSha256Hash(Password_TextBox.Text);
            Properties.Settings.Default.Last_Login = Username_TextBox.Text;
            Properties.Settings.Default.Save();

            if (user == "blake" && pass == Hash.ComputeSha256Hash("hello")) {
                Program.user = new Employee(user) {
                    type = EmployeeType.Manager
                };
            }

            if (Program.user.type != EmployeeType.None) {
                this.Close();
            } else {
                Failed_Label.Visible = true;
            }
        }
    }
}
