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
    internal partial class Clockout_Popup : Form {
        User user;

        internal Clockout_Popup(User waiter) {
            user = waiter;
            InitializeComponent();
        }

        private async void Set_Button_ClickAsync(object sender, EventArgs e) {
            var temp = new Waiter {
                username = user.username,
                clockout = Time_Box.Text
            };

            await Firebase.InsertAsync<Waiter>("Waiters", temp.username, temp);

            this.Close();
        }
    }
}
