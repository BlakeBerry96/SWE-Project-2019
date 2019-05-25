using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant {
    internal enum UserType {
        None,
        Busboy,
        Cook,
        Host,
        Manager,
        Waiter
    }

    internal class User {
        public string username, firstName, lastName;
        public UserType type;

        public User() {
            this.username = "";
            this.firstName = "";
            this.lastName = "";
            this.type = UserType.None;
        }

        public User(string username, string firstName, string lastName, UserType type) {
            this.username = username;
            this.firstName = firstName;
            this.lastName = lastName;
            this.type = type;
        }
    }
}