using System;
using System.Collections.Generic;

namespace _1983.Models
{

    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid Hash { get; set; }


        public User(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
            Hash = Guid.NewGuid();
        }

    }

    

}
