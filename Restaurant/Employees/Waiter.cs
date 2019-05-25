using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Employees {
    public class Waiter : Employee {
        public Waiter(string username) : base(username) {
            type = EmployeeType.Waiter;
        }

        public Waiter(Employee employee) : base(employee) {

        }
    }
}
