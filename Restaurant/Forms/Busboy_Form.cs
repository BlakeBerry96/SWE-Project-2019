using Restaurant.Employees;
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
    public partial class Busboy_Form : Form {
        Button lastClicked = null;
        Button lastChanged = null;
        Busboy employee;

        public Busboy_Form(Busboy busboy) {
            employee = busboy;
            InitializeComponent();
            InitialTableColor();
            Username_Label.Text = "Logged in: " + employee.username;

            SendDumbyFocus();
        }

        private void InitialTableColor() {
            foreach (Control control in LayoutPanel.Controls) {
                if (control is Button) {
                    Button table = control as Button;
                    table.BackColor = Color.Red;
                    table.ForeColor = Color.White;
                }
            }
        }

        private void Clean_Button_Click(object sender, EventArgs e) {
            if (lastClicked != null) {
                ToggleColor(lastClicked);
                lastChanged = lastClicked;
                lastClicked = null;
                Undo_Button.Enabled = true;
            }

            SendDumbyFocus();
        }

        private void Undo_Button_Click(object sender, EventArgs e) {
            if (lastChanged != null) {
                ToggleColor(lastChanged);
                lastChanged = null;
                lastClicked = null;
                Undo_Button.Enabled = false;
            }
            SendDumbyFocus();
        }

        private void Logout_Button_Click(object sender, EventArgs e) {
            SendDumbyFocus();

            this.Close();
        }

        private void SendDumbyFocus() {
            DumbyFocus.Focus();
        }

        private void Table_Button_Clicked(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            if (tableButton.BackColor == Color.Red)
                lastClicked = tableButton;
        }

        // Testing Method
        private void ToggleColor(Button lastClicked) {
            if (lastClicked.BackColor == Color.Red) {
                lastClicked.BackColor = Color.Green;
                lastClicked.ForeColor = Color.White;
            } else {
                lastClicked.BackColor = Color.Red;
                lastClicked.ForeColor = Color.White;
            }
        }

        private void Table_Cleaner_DoWork(object sender, DoWorkEventArgs e) {
            
        }
    }
}
