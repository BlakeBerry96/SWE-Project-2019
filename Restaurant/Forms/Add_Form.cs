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
    internal partial class Add_Form : Form {
        internal Add_Form() {
            InitializeComponent();
        }

        private async void Confirm_Click(object sender, EventArgs e) {
            if (First_Box.Text != "" && Last_Box.Text != "" && Pass_Box.Text != "" && Type_Box.Text != "") {
                var temp = new Employee() {
                    username = First_Box.Text.ToLower(),
                    first_name = First_Box.Text,
                    last_name = Last_Box.Text,
                    password_hash = Hash.ComputeSha256Hash(Pass_Box.Text),
                    user_type = Type_Box.Text.ToLower()
                };

                await Firebase.InsertAsync<Employee>("Employees", temp.username, temp);

                this.Close();
            }
        }
    }
}
