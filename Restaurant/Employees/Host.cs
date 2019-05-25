using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Employees {
    public class Host : Employee {
        public Host(string username) : base(username) {
            type = EmployeeType.Host;
        }

        public Host(Employee employee) : base(employee) {

        }
    }
}
