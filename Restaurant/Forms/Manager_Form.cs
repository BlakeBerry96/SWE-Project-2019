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
    public partial class Manager_Form : Form {
        User user;
        Busboy_Form busboy;
        Cook_Form cook;
        Host_Form host;
        Waiter_Form waiter;
        Add_Form add;
        Remove_Form remove;

        public Manager_Form(User manager) {
            user = manager;
            InitializeComponent();
            this.Text = Helper.TitleText("Manager", user);

        }

        private void Open_Busboy(object sender, EventArgs e) {
            busboy = new Busboy_Form(user);
            busboy.FormClosing += Form_Closing;
            busboy.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void Open_Cook(object sender, EventArgs e) {
            cook = new Cook_Form(user);
            cook.FormClosing += Form_Closing;
            cook.Show();
            this.Enabled = false;
        }

        private void Open_Host(object sender, EventArgs e) {
            host = new Host_Form(user);
            host.FormClosing += Form_Closing;
            host.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void Open_Waiter(object sender, EventArgs e) {
            waiter = new Waiter_Form(user);
            waiter.FormClosing += Form_Closing;
            waiter.Show();
            this.Hide();
            this.Enabled = false;
        }

        private void Open_Add(object sender, EventArgs e) {
            add = new Add_Form();
            add.Show();
            this.Enabled = false;
            add.FormClosing += Form_Closing;
        }

        private void Open_Remove(object sender, EventArgs e) {
            remove = new Remove_Form();
            remove.Show();
            this.Enabled = false;
            remove.FormClosing += Form_Closing;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e) {
            this.Show();
            this.Enabled = true;
        }

        private async void Refresh_Click(object sender, EventArgs e) {
            await RefreshList();
        }

        private async Task RefreshList() {
            var temp = (await Firebase.GetAsync<OrderDatabaseReturn>()).Orders;
            Invoke(new Action(() => {
                Order_List.Items.Clear();

                foreach (var item in temp) {
                    Order_List.Items.Add(item.Key.Replace("_", " "));
                }
            }));
        }

        private async void Order_List_Click(object sender, EventArgs e) {
            var temp = (Order_List.GetItemText(Order_List.SelectedItem)).Replace(" ", "_");
            var order = await Firebase.GetAsync<Order>("Orders", temp);

            Order_Textbox.Text = order.order_string;
            Order_Num_Label.Text = "Order " + order.order_num.ToString();
            var status = order.status;
            status = char.ToUpper(status[0]) + status.Substring(1);
            Order_Status_Label.Text = status;
        }

        private async void Form_Load(object sender, EventArgs e) {
            await RefreshList();
        }

        private void Logout_Click(object sender, EventArgs e) {
            this.Close();
        }

        private async void Reset_Click(object sender, EventArgs e) {
            for (int i = 1; i <= 8; i++) {
                var temp = new Table() {
                    assigned_waiter = "",
                    status = "clean",
                    table_number = i
                };

                await Firebase.UpdateAsync<Table>("Tables", "Table_" + i.ToString(), temp);
            }
        }
    }
}
