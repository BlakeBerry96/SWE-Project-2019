using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class CookForm : Form {
        User user;
        string lastSelected = null;
        Order lastChanged = null;
        Dictionary<string, Order> orders = null;

        internal CookForm(User cook) {
            user = cook;
            InitializeComponent();
            this.Text = Helper.TitleText("Cook", user);
            backgroundWorker.RunWorkerAsync();
        }

        private void logoutClick(object sender, EventArgs e) {
            this.Close();
        }

        private async void orderClick(object sender, EventArgs e) {
            lastSelected = orderList.GetItemText(orderList.SelectedItem);
            var temp = lastSelected.Replace(" ", "_");
            var order = await Firebase.GetAsync<Order>("Orders", temp);

            orderTextbox.Text = order.OrderString;
        }

        private async void readyClick(object sender, EventArgs e) {
            if (lastSelected != null) {
                var temp = lastSelected.Replace(" ", "_");
                var order = await Firebase.GetAsync<Order>("Orders", temp);
                lastChanged = order;
                order.Status = "ready";
                await Firebase.UpdateAsync<Order>("Orders", temp, order);
                orderList.SelectedItem = null;
                lastSelected = null;
                undoButton.Enabled = true;
            }
        }

        private async void undoClick(object sender, EventArgs e) {
            if (lastChanged != null) {
                await Firebase.UpdateAsync<Order>("Orders", Helper.GetOrderKey(lastChanged.OrderNum), lastChanged);
                lastChanged = null;
                undoButton.Enabled = false;
            }
        }

        private async void backgroundDoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    var temp = (await Firebase.GetAsync<OrderDatabaseReturn>()).Orders;
                    var tempList = new List<string>();
                    foreach (var item in temp) {
                        if (item.Value.Status != "placed") {
                            tempList.Add(item.Key);
                        }
                    }
                    foreach (var item in tempList) {
                        temp.Remove(item);
                    }

                    if (temp != orders) {
                        orders = temp;
                        Invoke(new Action(() => {
                            orderList.Items.Clear();

                            foreach (var item in orders) {
                                orderList.Items.Add(item.Key.Replace("_", " "));
                            }
                        }));
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            } catch (Exception) { }
        }

        private void formClosing(object sender, FormClosingEventArgs e) {
            backgroundWorker.CancelAsync();
        }
    }
}
