using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class Cook_Form : Form {
        User user;
        string lastSelected = null;
        Order lastChanged = null;
        Dictionary<string, Order> orders = null;

        internal Cook_Form(User cook) {
            user = cook;
            InitializeComponent();
            this.Text = Helper.TitleText("Cook", user);
            backgroundWorker.RunWorkerAsync();
        }

        private async void Ready_Click(object sender, EventArgs e) {
            if (lastSelected != null) {
                var temp = lastSelected.Replace(" ", "_");
                var order = await Firebase.GetAsync<Order>("Orders", temp);
                lastChanged = order;
                order.status = "ready";
                await Firebase.UpdateAsync<Order>("Orders", temp, order);
                Order_List.SelectedItem = null;
                lastSelected = null;
                Undo_Button.Enabled = true;
            }
        }

        private async void Background_DoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    var temp = (await Firebase.GetAsync<OrderDatabaseReturn>()).Orders;
                    var temp_list = new List<string>();
                    foreach (var item in temp) {
                        if (item.Value.status != "placed") {
                            temp_list.Add(item.Key);
                        }
                    }
                    foreach (var item in temp_list) {
                        temp.Remove(item);
                    }

                    if (temp != orders) {
                        orders = temp;
                        Invoke(new Action(() => {
                            Order_List.Items.Clear();

                            foreach (var item in orders) {
                                Order_List.Items.Add(item.Key.Replace("_", " "));
                            }
                        }));
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            } catch (Exception) { }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e) {
            backgroundWorker.CancelAsync();
        }

        private async void Undo_Click(object sender, EventArgs e) {
            if (lastChanged != null) {
                await Firebase.UpdateAsync<Order>("Orders", Helper.GetOrderKey(lastChanged.order_num), lastChanged);
                lastChanged = null;
                Undo_Button.Enabled = false;
            }
        }

        private void Logout_Click(object sender, EventArgs e) {
            this.Close();
        }

        private async void Order_List_Select_Change(object sender, EventArgs e) {
            lastSelected = Order_List.GetItemText(Order_List.SelectedItem);
            var temp = lastSelected.Replace(" ", "_");
            var order = await Firebase.GetAsync<Order>("Orders", temp);

            Order_Textbox.Text = order.order_string;
        }
    }
}
