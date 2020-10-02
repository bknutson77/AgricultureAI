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

        public static String FIREBASE_URL { get; set; }

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

            // Close and return.
            response.Close();
            return value;
        }

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
