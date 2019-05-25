using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class HostForm : Form {
        Button lastClicked = null;
        Table lastChanged;
        string lastSelected = null;
        User user;
        Dictionary<string, Waiter> waiters = null;

        internal HostForm(User waiter) {
            user = waiter;
            InitializeComponent();
            backgroundWorker.RunWorkerAsync();
            this.Text = Helper.TitleText("Host", user);
            notLabel.ForeColor = Color.Red;
            availableLabel.ForeColor = Color.Green;
        }

        private async void assignClick(object sender, EventArgs e) {
            if (lastClicked != null && lastSelected != null && lastClicked.BackColor == Color.Green) {
                var tableNum = Helper.GetTableNumber(lastClicked);
                if (tableNum != 9) {
                    lastChanged = await Helper.SetTableStatus(tableNum, "occupied", lastSelected);
                    lastClicked = null;
                    lastSelected = null;
                    undoButton.Enabled = true;
                    waiterNames.SelectedItem = null;
                }
            }

            sendDumbyFocus();
        }

        private void logoutClick(object sender, EventArgs e) {
            sendDumbyFocus();

            this.Close();
        }

        private void nameClick(object sender, EventArgs e) {
            lastSelected = waiterNames.GetItemText(waiterNames.SelectedItem);
            waiterClockoutTimes.SelectedIndex = waiterNames.SelectedIndex;
        }

        private void tableClick(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            lastClicked = tableButton;
        }

        private void timeClick(object sender, EventArgs e) {
            waiterNames.SelectedIndex = waiterClockoutTimes.SelectedIndex;
        }

        private async void undoClick(object sender, EventArgs e) {
            if (lastChanged != null) {
                await Firebase.UpdateAsync<Table>("Tables", Helper.GetTableKey(lastChanged.TableNumber), lastChanged);
                lastChanged = null;
                lastClicked = null;
                lastSelected = null;
                undoButton.Enabled = false;
                waiterNames.SelectedItem = null;
            }
            sendDumbyFocus();
        }

        private void sendDumbyFocus() {
            dumbyFocus.Focus();
        }

        private async void backgroundDoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    var tables = await Firebase.GetAsync<TableDatabaseReturn>();
                    Button b;

                    foreach (var table in tables.Tables) {
                        b = Helper.GetButton(LayoutPanel, table.Value.TableNumber);
                        Helper.SetButton(b, table.Value.Status, "clean", Color.Green, Color.Red);
                    }

                    var temp = await Firebase.GetAsync<WaiterDatabaseReturn>();
                    if (!temp.Waiters.Equals(waiters)) {
                        waiters = temp.Waiters;
                        Invoke(new Action(() => {
                            waiterNames.Items.Clear();
                            waiterClockoutTimes.Items.Clear();

                            foreach (var item in waiters) {
                                waiterNames.Items.Add(item.Key);
                                waiterClockoutTimes.Items.Add(item.Value.Clockout);
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
