using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Employees {
    public enum EmployeeType {
        Busboy,
        Cook,
        Host,
        Manager,
        Waiter,
        None
    }

    public class Employee {
        public string username;
        public EmployeeType type;

        public Employee(string username) {
            this.username = username;
        }

        public Employee(Employee employee) {
            this.username = employee.username;
            this.type = employee.type;
        }
    }
}