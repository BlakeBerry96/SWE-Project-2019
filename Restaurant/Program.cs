using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant.Employees;

namespace Restaurant {
    static class Program {
        public static Employee user;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            do {
                user = new Employee("") {
                    type = EmployeeType.None
                };

                Application.Run(new Login_Form());
                Application.Run(new Busboy_Form(new Busboy(user)));

                switch (user.type) {
                    case EmployeeType.Busboy:
                        var busboy = new Busboy(user);
                        Application.Run(new Busboy_Form(busboy));
                        break;
                    case EmployeeType.Cook:
                        var cook = new Cook(user);
                        Application.Run(new Cook_Form(cook));
                        break;
                    case EmployeeType.Host:
                        var host = new Host(user);
                        Application.Run(new Host_Form(host));
                        break;
                    case EmployeeType.Manager:
                        var manager = new Manager(user);
                        Application.Run(new Manager_Form(manager));
                        break;
                    case EmployeeType.Waiter:
                        var waiter = new Waiter(user);
                        Application.Run(new Waiter_Form(waiter));
                        break;
                    default:
                        break;
                }
            } while (user.type != EmployeeType.None);
        }
    }
}
