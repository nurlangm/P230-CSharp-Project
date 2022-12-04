using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalAppCSharp.Services
{
    internal static class MenuService
    {
        readonly static BankService _bankservice;

        static MenuService()
        {
            _bankservice = new BankService();
        }

        public static void UserRegistration()
        {

            Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
            User user = new User();
        Name:
            Console.Write("Name:");
            user.Name = Console.ReadLine();
            if (user.Name.Length<3)
            {
                Console.WriteLine("--Zehmet Olmasa Duzgun Daxil edin--");
                goto Name;
            }
        Surname:
            Console.Write("Surname:");
            user.Surname = Console.ReadLine();
            if (user.Surname.Length<3)
            {
                Console.WriteLine("--Zehmet Olmasa Duzgun Daxil edin--");
                goto Surname;
            }
        Email:
            Console.Write("Email:");
            user.Email = Console.ReadLine();
            if (!validateEmailRegex.IsMatch(user.Email))
            {
                Console.WriteLine("--Email Sertleri Odenmir!--");
                goto Email;
            }
        Password:
            Console.WriteLine("Password");
            user.Password = Console.ReadLine();
            if (user.Password.Length<8 || user.Password == user.Password.ToLower()|| user.Password == user.Password.ToUpper())
            {
                Console.WriteLine("--Password Serti Odenmir--");
                goto Password;
            }
            Console.WriteLine("IsAdmin?");
            Console.WriteLine("1.True");
            Console.WriteLine("2.False");
            string select;
            selection:
            select = Console.ReadLine();
            do
            {
                switch (select)
                {
                    case "1":
                        user.IsAdmin = true;
                        break;
                    case "2":
                        user.IsAdmin = false;
                        break;
                    default: Console.WriteLine("Choose Correct Number");
                        goto selection;
                }
            } while (false);

        }

        public static void ChangePassword()
        {
        current:
            Console.WriteLine("Zehmet olmasa parolunuzu yazin");
            string currentPass= Console.ReadLine();
            if (string.IsNullOrWhiteSpace(currentPass))
            {
                Console.WriteLine("Please enter valid pass");
                goto current;
            }
        newpass:
            Console.WriteLine("Zehmet olmasa yeni parolunuzu");
            string newpass = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newpass))
            {
                Console.WriteLine("Please enter valid pass");
                goto newpass;
            }
          bool result=_bankservice.ChangePassword(currentPass, newpass);
            if (!result)
            {
                goto current;
            }

        }

    }
}
