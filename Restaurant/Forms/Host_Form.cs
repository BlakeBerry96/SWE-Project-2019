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
    public partial class Host_Form : Form {
        Button lastClicked = null;
        Button lastChanged = null;
        Host employee;

        public Host_Form(Host waiter) {
            employee = waiter;
            InitializeComponent();
        }

        private void SendDumbyFocus() {
            DumbyFocus.Focus();
        }

        private void Table_Button_Clicked(object sender, EventArgs e) {
            Button tableButton = (sender as Button);
            lastClicked = tableButton;
        }

        private void Assign_Button_Click(object sender, EventArgs e) {
            if (lastClicked != null) {
                ToggleColor(lastClicked);
                lastChanged = lastClicked;
                lastClicked = null;
            }

            SendDumbyFocus();
        }

        private void Undo_Button_Click(object sender, EventArgs e) {
            if (lastChanged != null) {
                ToggleColor(lastChanged);
                lastChanged = null;
                lastClicked = null;
            }
            SendDumbyFocus();
        }

        private void Logout_Button_Click(object sender, EventArgs e) {
            SendDumbyFocus();

            this.Close();
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

        private void Waiter_Assigner_DoWork(object sender, DoWorkEventArgs e) {
            throw new NotImplementedException();
        }

        private void Time_List_Index_Change(object sender, EventArgs e) {
            Waiter_Names.SelectedIndex = Waiter_Clockout_Times.SelectedIndex;
        }

        private void Name_List_Index_Change(object sender, EventArgs e) {
            Waiter_Clockout_Times.SelectedIndex = Waiter_Names.SelectedIndex;
        }
    }
}
