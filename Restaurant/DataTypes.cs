using System;
using System.Collections.Generic;

namespace Restaurant {
    public class Employee {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password_hash { get; set; }
        public string username { get; set; }
        public string user_type { get; set; }
    }

    public class Table {
        public int table_number { get; set; }
        public string assigned_waiter { get; set; }
        public string status { get; set; }
    }

    public class Order {
        public int order_num { get; set; }
        public string status { get; set; }
        public string order_string { get; set; }
        public int table_num { get; set; }
        public string waiter_username { get; set; }
    }

    public class Waiter {
        public string username { get; set; }
        public string clockout { get; set; }
    }


    public class EmployeeDatabaseReturn {
        public Dictionary<string, Employee> Employees { get; set; }
    }

    public class TableDatabaseReturn {
        public Dictionary<string, Table> Tables { get; set; }
    }

    public class OrderDatabaseReturn {
        public Dictionary<string, Order> Orders { get; set; }
    }

    public class WaiterDatabaseReturn {
        public Dictionary<string, Waiter> Waiters { get; set; }
    }
}