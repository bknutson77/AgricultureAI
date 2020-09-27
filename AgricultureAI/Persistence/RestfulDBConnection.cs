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
    public class RestfulDBConnection
    {

        public String FIREBASE_URL = "https://agricultureai-15ce0.firebaseio.com/";

        public String Retrieve(String param)
        {
            // Create a request using the URL and param.
            WebRequest request = WebRequest.Create(FIREBASE_URL + param + ".json");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            Stream dataStream;
            String responseFromDB;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromDB = reader.ReadToEnd();
            }

            // Close and return.
            response.Close();
            return responseFromDB;
        }

        public String Store(String param1, String param2, String param3)
        {
            // Create a request using the URL and param.
            WebRequest request = WebRequest.Create(FIREBASE_URL + param1 + ".json");
            request.Method = "PATCH";
            request.ContentType = "application/x-www-form-urlencoded";

            // Write data to request.
            string postData = "{" + '"' + param2 + '"' + ": " + '"' + param3 + '"' + "}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            // Send request.
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            String responseFromDB;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromDB = reader.ReadToEnd();
            }

            // Close and return.
            response.Close();
            return responseFromDB;
        }

    }
}
