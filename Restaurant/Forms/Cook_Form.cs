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
    public partial class Cook_Form : Form {
        Cook employee;

        public Cook_Form(Cook cook) {
            employee = cook;
            InitializeComponent();
        }

        private void Order_Readier_DoWork(object sender, DoWorkEventArgs e) {

        }
    }
}
