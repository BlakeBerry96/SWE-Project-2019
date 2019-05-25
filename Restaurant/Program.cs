using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant {
    static class Program {
        public static User user;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try {
                if (Firebase.Connect()) {
                    do {
                        user = new User();

                        Application.Run(new Login_Form());

                        switch (user.type) {
                            case UserType.Busboy:
                                Application.Run(new Busboy_Form(user));
                                break;
                            case UserType.Cook:
                                Application.Run(new Cook_Form(user));
                                break;
                            case UserType.Host:
                                Application.Run(new Host_Form(user));
                                break;
                            case UserType.Manager:
                                Application.Run(new Manager_Form(user));
                                break;
                            case UserType.Waiter:
                                Application.Run(new Waiter_Form(user));
                                break;
                            default:
                                break;
                        }
                    } while (user.type != UserType.None);
                } else {
                    MessageBox.Show("Unable to connect to the Database", "Restaurant",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception e) {
                MessageBox.Show(e.Message, e.Source,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                Firebase.Close();
            }
        }
    }
}
