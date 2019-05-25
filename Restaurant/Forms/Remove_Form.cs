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
    internal partial class Remove_Form : Form {
        internal Remove_Form() {
            InitializeComponent();
        }

        private async void Confirm_Click(object sender, EventArgs e) {
            await Firebase.DeleteAsync("Employees", Employee_Box.Text);

            this.Close();
        }

        private async void Form_Load(object sender, EventArgs e) {
            var temp = await Firebase.GetAsync<EmployeeDatabaseReturn>();
            foreach (var item in temp.Employees) {
                if (item.Key != Program.user.username)
                    Employee_Box.Items.Add(item.Key);
            }
        }
    }
}
