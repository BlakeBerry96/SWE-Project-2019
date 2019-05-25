using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant {
    public partial class Login_Form : Form {
        public Login_Form() {
            InitializeComponent();
            Username_TextBox.Text = Properties.Settings.Default.Last_Login;
        }

        private void Login_Click(object sender, EventArgs e) {
            Login();
        }

        private void Pass_Key_Check(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                Login();
            }
        }

        private async void Login() {
            var user = Username_TextBox.Text.ToLower();
            var pass = Hash.ComputeSha256Hash(Password_TextBox.Text);
            Properties.Settings.Default.Last_Login = Username_TextBox.Text;
            Properties.Settings.Default.Save();

            User tempUser = new User() {
                type = UserType.None
        };

            try {
                tempUser = await GetEmployee(user.ToLower(), pass);
            } catch (Exception) { }

            if (tempUser.type != UserType.None) {
                Program.user = tempUser;
                this.Close();
            } else {
                Program.user = new User();
                Failed_Label.Visible = true;
            }
        }

        private async Task<User> GetEmployee(string user, string pass) {
            User employee = new User();

            Employee responce = await Firebase.GetAsync<Employee>("Employees", user);
            
            if (responce != null && responce.username.ToLower() == user.ToLower() && responce.password_hash == pass) {
                employee.firstName = responce.first_name;
                employee.lastName = responce.last_name;
                employee.username = responce.username;

                string type = responce.user_type.ToLower();

                switch (type) {
                    case "busboy":
                        employee.type = UserType.Busboy;
                        break;
                    case "cook":
                        employee.type = UserType.Cook;
                        break;
                    case "host":
                        employee.type = UserType.Host;
                        break;
                    case "manager":
                        employee.type = UserType.Manager;
                        break;
                    case "waiter":
                        employee.type = UserType.Waiter;
                        break;
                    default:
                        employee.type = UserType.None;
                        break;
                }
            } else {
                employee = new User();
            }

            return employee;
        }
    }
}
