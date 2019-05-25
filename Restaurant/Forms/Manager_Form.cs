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
    public partial class Manager_Form : Form {
        Manager employee;

        public Manager_Form(Manager manager) {
            employee = manager;
            InitializeComponent();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
