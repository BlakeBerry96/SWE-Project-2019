using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Employees {
    public class Busboy : Employee {
        public Busboy(string username) : base(username) {
            type = EmployeeType.Busboy;
        }

        public Busboy(Employee employee) : base(employee) {

        }
    }
}
