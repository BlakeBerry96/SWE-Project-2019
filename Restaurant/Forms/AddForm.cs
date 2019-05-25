using System;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class AddForm : Form {
        internal AddForm() {
            InitializeComponent();
        }

        private async void confirmClick(object sender, EventArgs e) {
            if (firstBox.Text != "" && lastBox.Text != "" && passBox.Text != "" && typeBox.Text != "") {
                var temp = new Employee() {
                    Username = firstBox.Text.ToLower(),
                    FirstName = firstBox.Text,
                    LastName = lastBox.Text,
                    PassHash = Hash.ComputeSha256Hash(passBox.Text),
                    UserType = typeBox.Text.ToLower()
                };

                await Firebase.InsertAsync<Employee>("Employees", temp.Username, temp);

                this.Close();
            }
        }
    }
}
