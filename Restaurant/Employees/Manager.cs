using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Employees {
    public class Manager : Employee {
        public Manager(string username) : base(username) {
            type = EmployeeType.Manager;
        }

        public Manager(Employee employee) : base(employee) {

        }
    }
}
