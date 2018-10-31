using System;

namespace NeptunPro.Tests.Regression.Models
{
    public class User
    {
        public string UserName { get; }
        public string Password { get; }



        public User(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Username can't be empty!");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password can't be empty!");

            UserName = userName;
            Password = password;
        }
    }
}
