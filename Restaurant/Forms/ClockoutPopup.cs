using System;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class ClockoutPopup : Form {
        User user;

        internal ClockoutPopup(User waiter) {
            user = waiter;
            InitializeComponent();
        }

        private async void SetClickAsync(object sender, EventArgs e) {
            var temp = new Waiter {
                Username = user.username,
                Clockout = Time_Box.Text
            };

            await Firebase.InsertAsync<Waiter>("Waiters", temp.Username, temp);

            this.Close();
        }
    }
}
