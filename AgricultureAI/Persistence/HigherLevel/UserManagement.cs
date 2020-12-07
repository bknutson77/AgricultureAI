using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;


namespace AgricultureAI.Persistence.HigherLevel
{
    public static class UserManagement
    {

        /// <summary>
        /// Method to check whether a username is available in the Firebase Realtime Database.
        /// </summary>
        /// <param name="username">The username in question</param>
        /// <returns>
        /// A string explaining the status of the username as available, unavailable, or unanswerable due to a database error.
        /// </returns>
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

        /// <summary>
        /// Method to attempt registering a user to the Firebase Realtime Database 
        /// (and to create a json structure to store their information and user data).
        /// </summary>
        /// <param name="name">The user's name</param>
        /// <param name="email">The user's email</param>
        /// <param name="occupation">The user's occupation</param>
        /// <param name="plantExpert">Whether the user is a plant expert or not</param>
        /// <param name="username">The user's chosen username</param>
        /// <param name="password">The user's chosen password</param>
        /// <returns>
        /// A string explaining the status of the attempt. If the username is unavailable, it will return that. If the registration was
        /// successful, it will return a success message. If there was a database error it will return an error message.
        /// </returns>
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

        /// <summary>
        /// Method to attempt logging in a user to the Firebase Realtime Database.
        /// </summary>
        /// <param name="username">The user's chosen username</param>
        /// <param name="password">The user's chosen password</param>
        /// <returns>
        /// A string explaining the status of the attempt. If the login was successful, it will return a success message.
        /// If the user enterred the wrong username/password, it will return a message indicating an incorrect username/password
        /// was provided. If there was a database error it will return an error message.
        /// </returns>
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
