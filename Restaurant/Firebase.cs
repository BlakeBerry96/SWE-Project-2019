using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  FireSharp
//	https://github.com/ziyasal/FireSharp
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Restaurant {
    internal static class Firebase {
        internal static IFirebaseConfig config = new FirebaseConfig {
            AuthSecret = ApiKeys.FirebaseSecret,
            BasePath = ApiKeys.FirebasePath
        };
        internal static IFirebaseClient client;
        
        internal static bool Connect() {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
                return true;
            else
                return false;
        }

        internal static void Close() {
            client = null;
        }

        internal static async Task<T> InsertAsync<T>(string parent_node, string child_node, T insertData) {
            string address = parent_node + "/" + child_node;

            SetResponse response = await client.SetAsync(address, insertData);
            return response.ResultAs<T>();
        }

        internal static async Task<T> GetAsync<T>(string parent_node, string child_node) {
            string address = parent_node + "/" + child_node;

            FirebaseResponse response = await client.GetAsync(address);
            return response.ResultAs<T>();
        }

        internal static async Task<T> GetAsync<T>(string path) {
            FirebaseResponse response = await client.GetAsync(path);
            return response.ResultAs<T>();
        }

        internal static async Task<T> GetAsync<T>() {
            FirebaseResponse response = await client.GetAsync("/");
            return response.ResultAs<T>();
        }

        internal static async Task<T> UpdateAsync<T>(string parent_node, string child_node, T insertData) {
            string address = parent_node + "/" + child_node;

            FirebaseResponse response = await client.UpdateAsync(address, insertData);
            return response.ResultAs<T>();
        }

        internal static async Task DeleteAsync(string parent_node, string child_node) {
            string address = parent_node + "/" + child_node;

            await client.DeleteAsync(address);
        }
    }
}
