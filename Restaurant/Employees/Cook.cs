using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Employees {
    public class Cook : Employee {
        public Cook(string username) : base(username) {
            this.username = username;
            type = EmployeeType.Cook;
        }

        public Cook(Employee employee) : base(employee) {

        }
    }
}
