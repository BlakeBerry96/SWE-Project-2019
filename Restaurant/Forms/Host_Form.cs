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
    internal partial class Host_Form : Form {
        Button lastClicked = null;
        Table lastChanged;
        string lastSelected = null;
        User user;
        Dictionary<string, Waiter> waiters = null;

        internal Host_Form(User waiter) {
            user = waiter;
            InitializeComponent();
            backgroundWorker.RunWorkerAsync();
            this.Text = Helper.TitleText("Host", user);
            Not_Label.ForeColor = Color.Red;
            Available_Label.ForeColor = Color.Green;
        }

        private void SendDumbyFocus() {
            DumbyFocus.Focus();
        }

        private void Table_Click(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            lastClicked = tableButton;
        }

        private async void Assign_Click(object sender, EventArgs e) {
            if (lastClicked != null && lastSelected != null && lastClicked.BackColor == Color.Green) {
                var table_num = Helper.GetTableNumber(lastClicked);
                if (table_num != 9) {
                    lastChanged = await Helper.SetTableStatus(table_num, "occupied", lastSelected);
                    lastClicked = null;
                    lastSelected = null;
                    Undo_Button.Enabled = true;
                    Waiter_Names.SelectedItem = null;
                }
            }

            SendDumbyFocus();
        }

        private async void Undo_Click(object sender, EventArgs e) {
            if (lastChanged != null) {
                await Firebase.UpdateAsync<Table>("Tables", Helper.GetTableKey(lastChanged.table_number), lastChanged);
                lastChanged = null;
                lastClicked = null;
                lastSelected = null;
                Undo_Button.Enabled = false;
                Waiter_Names.SelectedItem = null;
            }
            SendDumbyFocus();
        }

        private void Logout_Click(object sender, EventArgs e) {
            SendDumbyFocus();

            this.Close();
        }

        private void Time_Click(object sender, EventArgs e) {
            Waiter_Names.SelectedIndex = Waiter_Clockout_Times.SelectedIndex;
        }

        private void Name_Click(object sender, EventArgs e) {
            lastSelected = Waiter_Names.GetItemText(Waiter_Names.SelectedItem);
            Waiter_Clockout_Times.SelectedIndex = Waiter_Names.SelectedIndex;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e) {
            backgroundWorker.CancelAsync();
        }

        private async void Background_DoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    var tables = await Firebase.GetAsync<TableDatabaseReturn>();
                    Button b;

                    foreach (var table in tables.Tables) {
                        b = Helper.GetButton(LayoutPanel, table.Value.table_number);
                        Helper.SetButton(b, table.Value.status, "clean", Color.Green, Color.Red);
                    }

                    var temp = await Firebase.GetAsync<WaiterDatabaseReturn>();
                    if (!temp.Waiters.Equals(waiters)) {
                        waiters = temp.Waiters;
                        Invoke(new Action(() => {
                            Waiter_Names.Items.Clear();
                            Waiter_Clockout_Times.Items.Clear();

                            foreach (var item in waiters) {
                                Waiter_Names.Items.Add(item.Key);
                                Waiter_Clockout_Times.Items.Add(item.Value.clockout);
                            }
                        }));
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            } catch (Exception) { }
        }
    }
}
