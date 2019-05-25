using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant {
    internal static class Helper {
        internal static string TitleText(string formName, User employee) {
            return formName + ": " + employee.firstName + " " + employee.lastName;
        }

        internal static string GetTableKey(int tableNum) {
            return "Table_" + tableNum.ToString();
        }

        internal static string GetOrderKey(int orderNum) {
            return "Order_" + orderNum.ToString();
        }

        internal static void SetButton(Button button, string status, string testStatus, Color trueColor, Color falseColor) {
            if (status == testStatus) {
                button.BackColor = trueColor;
                button.ForeColor = Color.White;
            } else {
                button.BackColor = falseColor;
                button.ForeColor = Color.White;
            }
        }

        public static Button GetButton(TableLayoutPanel layoutPanel, int table_number) {
            Button b = null;
            foreach (Button item in layoutPanel.Controls) {
                if (item.Text.EndsWith(table_number.ToString())) {
                    b = item;
                    break;
                }
            }
            return b;
        }

        internal static int GetTableNumber(Button button) {
            int t = 9;
            Int32.TryParse(button.Text.Substring(button.Text.Length - 1), out t);
            return t;
        }

        internal static async Task<Table> SetTableStatus(int table_num, string status) {
            var old = await Firebase.GetAsync<Table>("Tables", "Table_" + table_num.ToString());
            var temp = new Table() {
                AssignedWaiter = old.AssignedWaiter,
                Status = status,
                TableNumber = old.TableNumber
            };
            if (status != "occupied" && temp.AssignedWaiter != "")
                temp.AssignedWaiter = "";
            await Firebase.UpdateAsync<Table>("Tables", "Table_" + table_num.ToString(), temp);
            return old;
        }

        internal static async Task<Table> SetTableStatus(int table_num, string status, string waiter) {
            var old = await Firebase.GetAsync<Table>("Tables", "Table_" + table_num.ToString());
            var temp = new Table() {
                AssignedWaiter = waiter,
                Status = status,
                TableNumber = old.TableNumber
            };
            await Firebase.UpdateAsync<Table>("Tables", "Table_" + table_num.ToString(), temp);
            return old;
        }

        internal static async Task<User> GetEmployee(string user, string pass) {
            User employee = new User();

            Employee responce = await Firebase.GetAsync<Employee>("Employees", user);

            if (responce != null && responce.Username.ToLower() == user.ToLower() && responce.PassHash == pass) {
                employee.firstName = responce.FirstName;
                employee.lastName = responce.LastName;
                employee.username = responce.Username;

                string type = responce.UserType.ToLower();

                switch (type) {
                    case "busboy":
                        employee.type = UserType.Busboy;
                        break;
                    case "cook":
                        employee.type = UserType.Cook;
                        break;
                    case "host":
                        employee.type = UserType.Host;
                        break;
                    case "manager":
                        employee.type = UserType.Manager;
                        break;
                    case "waiter":
                        employee.type = UserType.Waiter;
                        break;
                    default:
                        employee.type = UserType.None;
                        break;
                }
            } else {
                employee = new User();
            }

            return employee;
        }
    }
}
