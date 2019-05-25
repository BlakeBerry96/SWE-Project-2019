using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant {
    static class Helper {
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
                assigned_waiter = old.assigned_waiter,
                status = status,
                table_number = old.table_number
            };
            if (status != "occupied" && temp.assigned_waiter != "")
                temp.assigned_waiter = "";
            await Firebase.UpdateAsync<Table>("Tables", "Table_" + table_num.ToString(), temp);
            return old;
        }

        internal static async Task<Table> SetTableStatus(int table_num, string status, string waiter) {
            var old = await Firebase.GetAsync<Table>("Tables", "Table_" + table_num.ToString());
            var temp = new Table() {
                assigned_waiter = waiter,
                status = status,
                table_number = old.table_number
            };
            await Firebase.UpdateAsync<Table>("Tables", "Table_" + table_num.ToString(), temp);
            return old;
        }
    }
}
