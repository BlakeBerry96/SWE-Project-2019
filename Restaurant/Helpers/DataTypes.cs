using System.Collections.Generic;

namespace Restaurant {
    public class Employee {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassHash { get; set; }
        public string Username { get; set; }
        public string UserType { get; set; }
    }

    public class Order {
        public int OrderNum { get; set; }
        public string Status { get; set; }
        public string OrderString { get; set; }
        public int TableNum { get; set; }
        public string Waiter { get; set; }
    }

    public class Table {
        public int TableNumber { get; set; }
        public string AssignedWaiter { get; set; }
        public string Status { get; set; }
    }

    public class Waiter {
        public string Username { get; set; }
        public string Clockout { get; set; }
    }

    public class EmployeeDatabaseReturn {
        public Dictionary<string, Employee> Employees { get; set; }
    }

    public class OrderDatabaseReturn {
        public Dictionary<string, Order> Orders { get; set; }
    }

    public class TableDatabaseReturn {
        public Dictionary<string, Table> Tables { get; set; }
    }

    public class WaiterDatabaseReturn {
        public Dictionary<string, Waiter> Waiters { get; set; }
    }
}