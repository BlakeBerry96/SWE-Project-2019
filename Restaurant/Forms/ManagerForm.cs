using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class ManagerForm : Form {
        User user;
        BusboyForm busboy;
        CookForm cook;
        HostForm host;
        WaiterForm waiter;
        AddForm add;
        RemoveForm remove;

        internal ManagerForm(User manager) {
            user = manager;
            InitializeComponent();
            this.Text = Helper.TitleText("Manager", user);

        }

        private void logoutClick(object sender, EventArgs e) {
            this.Close();
        }

        private async void orderClick(object sender, EventArgs e) {
            var temp = (orderList.GetItemText(orderList.SelectedItem)).Replace(" ", "_");
            var order = await Firebase.GetAsync<Order>("Orders", temp);

            orderTextbox.Text = order.OrderString;
            orderNumLabel.Text = "Order " + order.OrderNum.ToString();
            var status = order.Status;
            status = char.ToUpper(status[0]) + status.Substring(1);
            orderStatusLabel.Text = status;
        }

        private async void refreshClick(object sender, EventArgs e) {
            await refreshList();
        }

        private async void resetClick(object sender, EventArgs e) {
            for (int i = 1; i <= 8; i++) {
                var temp = new Table() {
                    AssignedWaiter = "",
                    Status = "clean",
                    TableNumber = i
                };

                await Firebase.UpdateAsync<Table>("Tables", "Table_" + i.ToString(), temp);
            }
        }

        private void openAdd(object sender, EventArgs e) {
            add = new AddForm();
            add.Show();
            this.Enabled = false;
            add.FormClosing += formClosing;
        }

        private void openBusboy(object sender, EventArgs e) {
            busboy = new BusboyForm(user);
            busboy.FormClosing += formClosing;
            busboy.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void openCook(object sender, EventArgs e) {
            cook = new CookForm(user);
            cook.FormClosing += formClosing;
            cook.Show();
            this.Enabled = false;
        }

        private void openHost(object sender, EventArgs e) {
            host = new HostForm(user);
            host.FormClosing += formClosing;
            host.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void openRemove(object sender, EventArgs e) {
            remove = new RemoveForm();
            remove.Show();
            this.Enabled = false;
            remove.FormClosing += formClosing;
        }

        private void openWaiter(object sender, EventArgs e) {
            waiter = new WaiterForm(user);
            waiter.FormClosing += formClosing;
            waiter.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void formClosing(object sender, FormClosingEventArgs e) {
            this.Show();
            this.Enabled = true;
        }

        private async Task refreshList() {
            var temp = (await Firebase.GetAsync<OrderDatabaseReturn>()).Orders;
            Invoke(new Action(() => {
                orderList.Items.Clear();

                foreach (var item in temp) {
                    orderList.Items.Add(item.Key.Replace("_", " "));
                }
            }));
        }

        private async void formLoad(object sender, EventArgs e) {
            await refreshList();
        }
    }
}
