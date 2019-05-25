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
    internal partial class Busboy_Form : Form {
        Button lastClicked = null;
        Table lastChanged;
        User user;

        internal Busboy_Form(User busboy) {
            user = busboy;
            InitializeComponent();
            this.Text = Helper.TitleText("Busboy", user);
            backgroundWorker.RunWorkerAsync();
            Clean_Label.ForeColor = Color.Green;
            Dirty_Label.ForeColor = Color.Red;

            SendDumbyFocus();
        }

        private async void Clean_Click(object sender, EventArgs e) {
            if (lastClicked != null) {
                var table_num = Helper.GetTableNumber(lastClicked);
                if (table_num != 9) {
                    lastChanged = await Helper.SetTableStatus(table_num, "clean");
                    lastClicked = null;
                    Undo_Button.Enabled = true;
                }
            }
            SendDumbyFocus();
        }

        private async void Undo_Click(object sender, EventArgs e) {
            if (lastChanged != null) {
                await Firebase.UpdateAsync<Table>("Tables", Helper.GetTableKey(lastChanged.table_number), lastChanged);
                lastChanged = null;
                lastClicked = null;
                Undo_Button.Enabled = false;
            }
            SendDumbyFocus();
        }

        private void Logout_Click(object sender, EventArgs e) {
            SendDumbyFocus();

            this.Close();
        }

        private void SendDumbyFocus() {
            DumbyFocus.Focus();
        }

        private void Table_Click(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            if (tableButton.BackColor == Color.Red)
                lastClicked = tableButton;
            else {
                SendDumbyFocus();
                lastClicked = null;
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e) {
            backgroundWorker.CancelAsync();
        }

        private async void Background_DoWork(object sender, DoWorkEventArgs e) {
            try {
                while (!backgroundWorker.CancellationPending) {
                    TableDatabaseReturn tables;
                    tables = await Firebase.GetAsync<TableDatabaseReturn>();
                    Button b;

                    foreach (var table in tables.Tables) {
                        b = Helper.GetButton(LayoutPanel, table.Value.table_number);
                        Helper.SetButton(b, table.Value.status, "dirty", Color.Red, Color.Green);
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            } catch (Exception) { }
        }
    }
}
