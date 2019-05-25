using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Restaurant {
    internal partial class BusboyForm : Form {
        Button lastClicked = null;
        Table lastChanged;
        User user;

        internal BusboyForm(User busboy) {
            user = busboy;
            InitializeComponent();
            this.Text = Helper.TitleText("Busboy", user);
            backgroundWorker.RunWorkerAsync();
            cleanLabel.ForeColor = Color.Green;
            dirtyLabel.ForeColor = Color.Red;

            sendDumbyFocus();
        }

        private async void cleanClick(object sender, EventArgs e) {
            if (lastClicked != null) {
                var tableNum = Helper.GetTableNumber(lastClicked);
                if (tableNum != 9) {
                    lastChanged = await Helper.SetTableStatus(tableNum, "clean");
                    lastClicked = null;
                    undoButton.Enabled = true;
                }
            }
            sendDumbyFocus();
        }

        private void logoutClick(object sender, EventArgs e) {
            sendDumbyFocus();

            this.Close();
        }

        private void tableClick(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            if (tableButton.BackColor == Color.Red)
                lastClicked = tableButton;
            else {
                sendDumbyFocus();
                lastClicked = null;
            }
        }

        private async void undoClick(object sender, EventArgs e) {
            if (lastChanged != null) {
                await Firebase.UpdateAsync<Table>("Tables", Helper.GetTableKey(lastChanged.TableNumber), lastChanged);
                lastChanged = null;
                lastClicked = null;
                undoButton.Enabled = false;
            }
            sendDumbyFocus();
        }

        private void sendDumbyFocus() {
            dumbyFocus.Focus();
        }

        private async void backgroundDoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    TableDatabaseReturn tables;
                    tables = await Firebase.GetAsync<TableDatabaseReturn>();
                    Button b;

                    foreach (var table in tables.Tables) {
                        b = Helper.GetButton(layoutPanel, table.Value.TableNumber);
                        Helper.SetButton(b, table.Value.Status, "dirty", Color.Red, Color.Green);
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
