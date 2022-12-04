using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAppCSharp.Repositories
{
    internal interface IBankRepository
    {

        public User[] Users { get; }

        //Logine qeder
        string UserRegistration(string name, string surname, string email, string password, bool isadmin);

        bool UserLogin(User user, string logemail, string logpas);

        bool FindUser(string email);

        //Login Olduqdan sonra


        void CheckBalance(User user, double balance);

        void TopupBalance(User user, double balance); // Login olmush user-in daxil etdiyi məbləğ
                                                      // qədər balansını artıracaq və artırılan məbləğ ekranda yazılacaq.

        string ChangePassword(User user, string newPassword);//Login olmush user-in daxil etdiyi yeni password köhnə password-u əvəzləyəcək
                                                             //(yeni password minimum uzunlugu 8 olmalıdır, içərisində minimum 1 hərf kiçik,
                                                             //minimum 1 hərf böyük olmalıdır). Log out olduqdan sonra artıq yeni password
                                                             //vasitəsi ilə login olmalıdır.

        void BankUserList(); //Bankdakı userlərin ad və soyadlarını

        string BlockUser(string email);//Adminin daxil etdiyi email-e uygun gelen user-in sistemə girişini
                                       //qadağan edir. (Yalnız admin olan user edə bilər);

        ///Log out - Login/Register səhifəsinə geri qayıtmalıdır.

    }
}
