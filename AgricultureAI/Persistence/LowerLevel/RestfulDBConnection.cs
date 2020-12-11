using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AgricultureAI.Persistence
{
    public static class RestfulDBConnection
    {
        // Firebase URL (set from appsettings.json in Startup.cs):
        public static String FIREBASE_URL { get; set; }

        /// <summary>
        /// Generic method to retrieve data from the Firebase Realtime Database at a given json address level.
        /// </summary>
        /// <param name="address">The address of the json data of interest</param>
        /// <returns>
        /// The retrieved value from the request.
        /// </returns>
        public static String Retrieve(String address)
        {
            // Create a request using the URL and param.
            WebRequest request = WebRequest.Create(FIREBASE_URL + address + ".json");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            Stream dataStream;
            String value;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                value = reader.ReadToEnd();
            }
            response.Close();

            // Prettify response and return.
            value = value.Replace("\"", "");
            return value;
        }

        /// <summary>
        /// Generic method to store/patch data to the Firebase Real Time Database.
        /// </summary>
        /// <param name="address">The address of the json data of interest</param>
        /// <param name="param">The parameter beneath the parent address for which to store data</param>
        /// <param name="value">The value of the parameter to be stored to param</param>
        /// <returns>
        /// The status of the store call to Firebase (success or failure).
        /// </returns>
        public static String Store(String address, String param, String value)
        {
            // Create a request using the URL and param.
            WebRequest request = WebRequest.Create(FIREBASE_URL + address + ".json");
            request.Method = "PATCH";
            request.ContentType = "application/x-www-form-urlencoded";

            // Write data to request.
            string postData = "{" + '"' + param + '"' + ": " + '"' + value + '"' + "}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // Send request.
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                value = reader.ReadToEnd();
            }

            // Close and return.
            response.Close();
            return value;
        }

    }
}
