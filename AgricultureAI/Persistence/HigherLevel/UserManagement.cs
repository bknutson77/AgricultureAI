using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

/*
 * EXAMPLE USE:
 * if (UserManagement.TestIfUsernameAvailable("bknutson77") == "Available")
 * { UserManagement.CreateUser("Ben", "benjk117@gmail.com", "Software Engineer", "no", "bknutson77", "haha1234"); }
 */

namespace AgricultureAI.Persistence.HigherLevel
{
    public static class UserManagement
    {

        public static String TestIfUsernameAvailable(String username)
        {
            try
            {
                var check = RestfulDBConnection.Retrieve(username);
                if (check == "null")
                {
                    Debug.Write("Username " + username + " is available.\n");
                    return "Available";
                } else
                {
                    Debug.Write("Username " + username + " is unavailable.\n");
                    return "Unavailable";
                }
            }
            catch (Exception e)
            {
                Debug.Write("Error creating user: " + e.Message + "\n");
                return "DB Error";
            }
        }

        public static bool CreateUser(String name, String email, String occupation, String plantExpert, String username, String password)
        {
            try
            {
                RestfulDBConnection.Store(username, "name", name);
                RestfulDBConnection.Store(username, "email", email);
                RestfulDBConnection.Store(username, "occupation", occupation);
                RestfulDBConnection.Store(username, "plantExpert", plantExpert);
                RestfulDBConnection.Store(username, "username", username);
                RestfulDBConnection.Store(username, "password", password);
                Debug.Write("Successfully created user " + username + ".\n");
                return true;
            } 
            catch (Exception e)
            {
                Debug.Write("Error creating user: " + e.Message + "\n");
                return false;
            }            
        }

    }
}
