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

        public static String AttemptRegister(String name, String email, String occupation, String plantExpert, String username, String password)
        {
            if (TestIfUsernameAvailable(username) == "Unavailable")
            {
                return "Username Unavailable";
            }
            try
            {
                RestfulDBConnection.Store(username, "name", name);
                RestfulDBConnection.Store(username, "email", email);
                RestfulDBConnection.Store(username, "occupation", occupation);
                RestfulDBConnection.Store(username, "plantExpert", plantExpert);
                RestfulDBConnection.Store(username, "username", username);
                RestfulDBConnection.Store(username, "password", password);
                Debug.Write("Successfully created user " + username + ".\n");
                return "Success";
            } 
            catch (Exception e)
            {
                Debug.Write("Error creating user: " + e.Message + "\n");
                return "Error Creating User";
            }            
        }

        public static String AttemptLogin(String username, String password)
        {
            try
            {
                string truePassword = RestfulDBConnection.Retrieve(username + "/password");
                if (truePassword == password)
                {
                    Debug.Write("Successfully logged in user " + username + ".\n");
                    return "Success";
                } else
                {
                    Debug.Write("Username or password provided was incorrect: " + username + ", " + password + ".\n");
                    return "Incorrect Username/Password";
                }               
            }
            catch (Exception e)
            {
                Debug.Write("Error attempting login: " + e.Message + "\n");
                return "Error Attempting Login";
            }
        }

    }
}
