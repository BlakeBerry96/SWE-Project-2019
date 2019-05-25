using System;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class RemoveForm : Form {
        internal RemoveForm() {
            InitializeComponent();
        }

        private async void confirmClick(object sender, EventArgs e) {
            await Firebase.DeleteAsync("Employees", Employee_Box.Text);

            this.Close();
        }

        private async void formLoad(object sender, EventArgs e) {
            var temp = await Firebase.GetAsync<EmployeeDatabaseReturn>();
            foreach (var item in temp.Employees) {
                if (item.Key != Program.user.username)
                    Employee_Box.Items.Add(item.Key);
            }
        }
    }
}
