using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAppCSharp
{
    internal class User
    {

        public int Id;
        public string Name; // min uzunluq 3
        public string Surname; //min uzunluq 3
        public double Balance;
        public string Email;// @ isaresi olmalidir ve duplicate email ola bilmez
        public string Password;//min uzunluq 8,bir boyuk bir kicik
        public bool IsAdmin;
        public bool IsBlocked;
        public bool IsLoggeed;

        static int _count;

        public User()
        {
               _count= 0;
        }



        public User(string name, string surname,string email, string password, double balance)
        {
            Id = ++_count;
            Name = name;
            Surname = surname;
            Balance = balance;
            Email = email;
            Password = password;
            IsAdmin = false;
            IsBlocked = false;
            IsLoggeed = false;
        }
        public User(string name, string surname, string email, string password, bool isadmin = false)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;    
            IsAdmin=isadmin;
        }
    }
}
