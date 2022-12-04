using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAppCSharp.Repositories
{
    internal class BankRepository : IBankRepository
    {
        User[] _users;

        public User[] Users { get => _users; }


        public BankRepository()
        {
            _users = new User[0];
        }

        public void BankUserList()
        {
            if (_users.Length != 0)
            {
                foreach (User user in _users)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("Bankda User Yoxdur");
            }
        }

        public string BlockUser(string email)
        {
            throw new NotImplementedException();
        }

        public string ChangePassword(User user, string newPassword)
        {


            user.Password = newPassword;

            return user.Password;

        }

        public void CheckBalance(User user, double balance)
        {
            if (user.IsLoggeed)
            {
                Console.WriteLine(balance);
            }
            else
            {
                Console.WriteLine("Siz Login Olmamisiniz");
            }

        }

        public bool FindUser(string email)
        {
            throw new NotImplementedException();

        }

        public void TopupBalance(User user, double balance)
        {
            if (user.IsLoggeed)
            {
            }
        }

        public bool UserLogin(User user, string logemail, string logpas)
        {
            if (user.Email != logemail || user.Password != logpas)
            {
                return false;
            }
            return true;
        } //???

        public string UserRegistration(string name, string surname, string email, string password, bool isadmin)
        {
            User user = new User(name, surname, email, password, isadmin);
            Array.Resize(ref _users, _users.Length + 1);
            _users[_users.Length - 1] = user;

            return user.Name + " " + user.Surname;  //???    
        }

    }
}
