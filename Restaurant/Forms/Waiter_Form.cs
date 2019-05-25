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
    internal partial class Waiter_Form : Form {
        User user;
        Button lastClicked = null;
        string lastOrder = null;
        Clockout_Popup clock;

        internal Waiter_Form(User waiter) {
            user = waiter;
            InitializeComponent();
            Comfirm_Label.ForeColor = Color.Green;
            backgroundWorker.RunWorkerAsync();
            this.Text = Helper.TitleText("Waiter", user);
            SendDumbyFocus();
        }

        private void Table_Click(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            if (tableButton.BackColor == Color.Green)
                lastClicked = tableButton;
            else {
                SendDumbyFocus();
                lastClicked = null;
            }
        }

        private async void Order_Click(object sender, EventArgs e) {
            SendDumbyFocus();
            if (lastClicked != null) {
                int order_num = await Firebase.GetAsync<int>("", "next_order");
                await Firebase.InsertAsync<int>("", "next_order", order_num + 1);
                Order order = new Order {
                    order_num = order_num,
                    order_string = Order_Textbox.Text,
                    status = "placed",
                    table_num = Helper.GetTableNumber(lastClicked),
                    waiter_username = user.username
                };
                var orderKey = Helper.GetOrderKey(order_num);
                await Firebase.InsertAsync<Order>("Orders", orderKey, order);
                Order_Textbox.Text = "";
                lastOrder = orderKey;
                Undo_Button.Enabled = true;
                Comfirm_Label.Visible = true;
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Comfirm_Label.Visible = false;
            }
        }

        private async void Undo_Click(object sender, EventArgs e) {
            SendDumbyFocus();
            if (lastOrder != null) {
                await Firebase.DeleteAsync("Orders", lastOrder);
                lastOrder = null;
                lastClicked = null;
                Undo_Button.Enabled = false;
            }
        }

        private void Logout_Click(object sender, EventArgs e) {
            SendDumbyFocus();

            this.Close();
        }

        private void SendDumbyFocus() {
            DumbyFocus.Focus();
        }

        private async void Form_Closing(object sender, FormClosingEventArgs e) {
            backgroundWorker.CancelAsync();
            await Firebase.DeleteAsync("Waiters", user.username);
        }

        private async void Leaving_Click(object sender, EventArgs e) {
            if (lastClicked != null) {
                if (lastClicked != null) {
                    var table_num = Helper.GetTableNumber(lastClicked);
                    if (table_num != 9) {
                        await Helper.SetTableStatus(table_num, "dirty");
                        lastClicked = null;
                    }
                }
                SendDumbyFocus();
            }
        }

        private async void Background_DoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    var tempTables = (await Firebase.GetAsync<TableDatabaseReturn>()).Tables;
                    Button b;

                    foreach (var table in tempTables) {
                        b = Helper.GetButton(LayoutPanel, table.Value.table_number);
                        if (table.Value.assigned_waiter == user.username) {
                            b.BackColor = Color.Green;
                            b.ForeColor = Color.White;
                        } else {
                            b.BackColor = Color.Red;
                            b.ForeColor = Color.White;
                        }
                    }

                    var tempOrders = (await Firebase.GetAsync<OrderDatabaseReturn>()).Orders;

                    foreach (var item in tempOrders) {
                        if (item.Value.waiter_username == user.username && item.Value.status == "ready") {
                            MessageBox.Show("Food ready for table " + item.Value.table_num.ToString());
                            Order order = new Order() {
                                order_num = item.Value.order_num,
                                status = "complete",
                                table_num = item.Value.table_num,
                                waiter_username = item.Value.waiter_username,
                                order_string = item.Value.order_string
                            };
                            await Firebase.UpdateAsync<Order>("Orders", item.Key, order);
                        }
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            } catch (Exception) { }
        }

        private void Form_Load(object sender, EventArgs e) {
            clock = new Clockout_Popup(user);
            clock.FormClosing += Clockout_Form_Close;
            clock.Show();
            this.Hide();
        }

        private void Clockout_Form_Close(object sender, FormClosingEventArgs e) {
            this.Show();
        }
    }
}
