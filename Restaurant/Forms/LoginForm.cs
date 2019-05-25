using System;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class LoginForm : Form {
        internal LoginForm() {
            InitializeComponent();
            Username_TextBox.Text = Properties.Settings.Default.Last_Login;
        }

        private void enterKeyPress(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                login();
            }
        }

        private void loginClick(object sender, EventArgs e) {
            login();
        }

        private async void login() {
            var user = Username_TextBox.Text.ToLower();
            var pass = Hash.ComputeSha256Hash(Password_TextBox.Text);
            Properties.Settings.Default.Last_Login = Username_TextBox.Text;
            Properties.Settings.Default.Save();

            User tempUser = new User() {
                type = UserType.None
        };

            try {
                tempUser = await Helper.GetEmployee(user.ToLower(), pass);
            } catch (Exception) { }

            if (tempUser.type != UserType.None) {
                Program.user = tempUser;
                this.Close();
            } else {
                Program.user = new User();
                Failed_Label.Visible = true;
            }
        }
    }
}
