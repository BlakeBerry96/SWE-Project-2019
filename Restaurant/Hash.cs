using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Restaurant {
    internal class Hash {

        // Function source from:
        // https://www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
        // Then modified to suit the project
        internal static string ComputeSha256Hash(string str) {
            str = salt(str);

            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create()) {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                // Convert byte array to a string   
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
