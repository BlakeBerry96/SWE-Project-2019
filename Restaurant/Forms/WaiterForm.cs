using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class WaiterForm : Form {
        User user;
        Button lastClicked = null;
        string lastOrder = null;
        ClockoutPopup clock;

        internal WaiterForm(User waiter) {
            user = waiter;
            InitializeComponent();
            comfirmLabel.ForeColor = Color.Green;
            backgroundWorker.RunWorkerAsync();
            this.Text = Helper.TitleText("Waiter", user);
            sendDumbyFocus();
        }

        private async void leavingClick(object sender, EventArgs e) {
            if (lastClicked != null) {
                if (lastClicked != null) {
                    var tableNum = Helper.GetTableNumber(lastClicked);
                    if (tableNum != 9) {
                        await Helper.SetTableStatus(tableNum, "dirty");
                        lastClicked = null;
                    }
                }
                sendDumbyFocus();
            }
        }

        private void logoutClick(object sender, EventArgs e) {
            sendDumbyFocus();

            this.Close();
        }

        private async void orderClick(object sender, EventArgs e) {
            sendDumbyFocus();
            if (lastClicked != null) {
                int orderNum = await Firebase.GetAsync<int>("", "next_order");
                await Firebase.InsertAsync<int>("", "next_order", orderNum + 1);
                Order order = new Order {
                    OrderNum = orderNum,
                    OrderString = orderTextbox.Text,
                    Status = "placed",
                    TableNum = Helper.GetTableNumber(lastClicked),
                    Waiter = user.username
                };
                var orderKey = Helper.GetOrderKey(orderNum);
                await Firebase.InsertAsync<Order>("Orders", orderKey, order);
                orderTextbox.Text = "";
                lastOrder = orderKey;
                undoButton.Enabled = true;
                comfirmLabel.Visible = true;
                Thread.Sleep(TimeSpan.FromSeconds(5));
                comfirmLabel.Visible = false;
            }
        }

        private void tableClick(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            if (tableButton.BackColor == Color.Green)
                lastClicked = tableButton;
            else {
                sendDumbyFocus();
                lastClicked = null;
            }
        }

        private async void undoClick(object sender, EventArgs e) {
            sendDumbyFocus();
            if (lastOrder != null) {
                await Firebase.DeleteAsync("Orders", lastOrder);
                lastOrder = null;
                lastClicked = null;
                undoButton.Enabled = false;
            }
        }

        private void sendDumbyFocus() {
            DumbyFocus.Focus();
        }

        private async void backgroundDoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    var tempTables = (await Firebase.GetAsync<TableDatabaseReturn>()).Tables;
                    Button b;

                    foreach (var table in tempTables) {
                        b = Helper.GetButton(LayoutPanel, table.Value.TableNumber);
                        if (table.Value.AssignedWaiter == user.username) {
                            b.BackColor = Color.Green;
                            b.ForeColor = Color.White;
                        } else {
                            b.BackColor = Color.Red;
                            b.ForeColor = Color.White;
                        }
                    }

                    var tempOrders = (await Firebase.GetAsync<OrderDatabaseReturn>()).Orders;

                    foreach (var item in tempOrders) {
                        if (item.Value.Waiter == user.username && item.Value.Status == "ready") {
                            MessageBox.Show("Food ready for table " + item.Value.TableNum.ToString());
                            Order order = new Order() {
                                OrderNum = item.Value.OrderNum,
                                Status = "complete",
                                TableNum = item.Value.TableNum,
                                Waiter = item.Value.Waiter,
                                OrderString = item.Value.OrderString
                            };
                            await Firebase.UpdateAsync<Order>("Orders", item.Key, order);
                        }
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            } catch (Exception) { }
        }

        private void formLoad(object sender, EventArgs e) {
            clock = new ClockoutPopup(user);
            clock.FormClosing += clockoutFormClose;
            clock.Show();
            this.Hide();
        }

        private void clockoutFormClose(object sender, FormClosingEventArgs e) {
            this.Show();
        }

        private async void formClosing(object sender, FormClosingEventArgs e) {
            backgroundWorker.CancelAsync();
            await Firebase.DeleteAsync("Waiters", user.username);
        }
    }
}
