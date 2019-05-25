using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Restaurant {
    internal class Hash {

        internal static string ComputeSha256Hash(string str) {
            str = salt(str);
 
            using (SHA256 sha256Hash = SHA256.Create()) {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++) {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static string salt(string str) {
            string salt_string = "SoftwareEngineering2019";
            str += salt_string;
            return str;
        }
    }
}
