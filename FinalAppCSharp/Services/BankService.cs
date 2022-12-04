using FinalAppCSharp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAppCSharp.Services
{
    internal class BankService
    {
        readonly IBankRepository _repository;

        public BankService()
        {
            _repository= new BankRepository();
        }

        public bool UserRegistration(string name, string surname, string email, string password, bool isadmin)
        {
            //string result = _repository.UserRegistration(name,surname,email,password,isadmin);

            // if (!string.IsNullOrWhiteSpace(result))
            // {
            //     Console.WriteLine($"{result}:User Yarandi");
            //     return true;
            // }

            _repository.UserRegistration(name, surname, email, password, isadmin);
            return true;

        }
        public bool ChangePassword(string currentPass, string newPassword)
        {
            User existed=null;
            foreach (User user in _repository.Users)
            {
                if (user.Password == currentPass)
                {
                    existed = user;
                }
            }
            if (existed==null)
            {
                return false;
            }

             
            foreach (User user in _repository.Users)
            {
                if (user.Password == newPassword)
                {
                    return false;
                }
            }
            _repository.ChangePassword(existed,newPassword);
            return true;
        }

        public void BankUserList(User user)
        {
            if (user.IsAdmin)
            {
                _repository.BankUserList();
            }
            else
            {
                Console.WriteLine("Siz Admin Deyilsiz");
            }
        }



    }
}
