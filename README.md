# SSWE-Project-2019

Created for a Mississippi State University software engineering project


The Project was to make a piece of software to run a restaurant and have individual interfaces for the busboys, cooks, hosts, managers, and waiters


We used Google's Firebase as a database and Fire# v2 created by ziyasal to connect to Firebase more easily.
https://github.com/ziyasal/FireSharp


This contains all the files except the ApiKey.cs file. The only things contained in it is the FirebasePath and FirebaseSecret used in the Firebase.cs file. Below is the class that's needed from ApiKey.cs

```C#
internal static class ApiKeys {
        internal const string FirebasePath = "Path";
        internal const string FirebaseSecret = "Secret";
}
```
