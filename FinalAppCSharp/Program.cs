using FinalAppCSharp.Entities;
using FinalAppCSharp.Interfaces;
using FinalAppCSharp.Repos;
using FinalAppCSharp.Services;
using FinalAppCSharp.Utils;
using Spectre.Console;

namespace Nurlan
{
    public class Program
    {
        private delegate bool ActionContext();

        static string? _loggedInUserEmail;

        // her dovrde hansi promptlarin gosterileceyine qerar verir, loginden sonra ancaq login promptlari kimi
        static ActionContext _actionontext = HomeMenu;

        static readonly IUserRepository UsersRepo = new UsersRepo();
        static readonly IUserService UserService = new UserService(UsersRepo);


        public static void Main()
        {
            AnsiConsole.Markup("[underline red]Welcome To Mammedli Bank[/]");
            while (true)
            {
                Console.WriteLine();

                var c = _actionontext(); // menu false return etse loop bitir

                if (c is false)
                {
                    break;
                }
                Console.WriteLine();
            }
        }

        private static bool AfterLoginMenu()
        {
            var c = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose action")
                    .AddChoices(new[]
                    {
                        "CheckBalance",
                        "TopUpBalance",
                        "ChangePass",
                        "ListUsers",
                        "BlockUser",
                        "LogOut"
                    }));

            switch (c)
            {
                // list all
                case "ListUsers" when AdminCheck() is false:
                    Console.WriteLine("Not authorised! ");
                    return true;
                case "ListUsers":
                    {
                        var table = new Table();
                        table.AddColumn("Name");
                        table.AddColumn("Surname");
                        foreach (var listUser in UserService.ListUsers())
                        {
                            table.AddRow(listUser.Name, listUser.Surname);
                        }

                        AnsiConsole.Write(table);
                        break;
                    }
                // list all
                case "LogOut":
                    Logoff();
                    break;
                case "CheckBalance":
                    {
                        var balance = UserService.GetUserBalanceByEmail(_loggedInUserEmail);
                        Console.WriteLine(balance);
                        break;
                    }
                case "TopUpBalance":
                    {
                        var amount = AnsiConsole.Ask<double>("Enter amount: ");
                        // amount validation
                        UserService.TopUpBalanceUserBalance(_loggedInUserEmail, amount);
                        break;
                    }
                case "BlockUser" when AdminCheck() is false:
                    Console.WriteLine("Not authorised! ");
                    return true;
                case "BlockUser":
                    {
                        var users = UserService.ListUsers()
                            .Where(a => a.Email != _loggedInUserEmail) // ozumuzu cixardiriq siyahidan
                            .Select(a => a.Email);

                        if (users.Count() == 0)
                        {
                            Console.WriteLine("No users to block :(");
                            return true;
                        }

                        var chosenUserEmail = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please choose user to block:")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more users)[/]")
                                .AddChoices(users));
                        if (AnsiConsole.Confirm("Run prompt example?"))
                        {
                            var result = UserService.BlockUser(chosenUserEmail);
                            Console.WriteLine(result ? "User has been successfully blocked" : "Provided user does not exist!");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Cancelled blocking operation.");
                        }

                        break;
                    }
                case "ChangePass":
                    {
                        var resut = PromptPasswordChange();
                        var oldP = resut.Item1;
                        var newP = resut.Item2;

                        if (AnsiConsole.Confirm("Run prompt example?"))
                        {
                            var result = UserService.ChangeUserPassword(_loggedInUserEmail, oldP, newP);
                            Console.WriteLine(result ? "Password has been successfully changed" : "Provided password is wrong");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Password change operation cancelled.");
                        }

                        break;
                    }
            }


            return true;
        }
        private static bool AdminCheck()
        {
            return UserService.IsUserAdmin(_loggedInUserEmail);
        }
        static bool HomeMenu()
        {
            var c = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose action")
                    .AddChoices(new[] { "Register", "Login", "FindUser", "Exit" }));

            switch (c)
            {
                case "Exit":
                    return false;
                case "Register":
                    {
                        var user = PromptRegister();
                        var result = UserService.RegisterUser(user);
                        Console.WriteLine(result ? "User has been successfully registered" : "User already registered");
                        break;
                    }
                case "Login":
                    {
                        var result = PromptLogin();
                        string email = result.Item1;
                        string password = result.Item2;
                        if (UserService.LoginUser(email, password))
                        {
                            LoginUser(email, password);
                        }
                        else
                        {
                            Console.WriteLine("Error login");
                        }

                        break;
                    }
                case "FindUser":
                    {
                        var email = AnsiConsole.Ask<string>("Enter user's email:");
                        var user = UserService.FindUser(email);
                        if (user != null)
                        {
                            var table = new Table();
                            table.AddColumn("Name");
                            table.AddColumn("Surname");
                            table.AddRow(user.Name, user.Surname);
                            AnsiConsole.Write(table);
                        }
                        else
                        {
                            AnsiConsole.Markup("[underline red]User Not Found[/]");
                        }

                        break;
                    }
            }

            return true;
        }

        static (string, string) PromptLogin()
        {
            var email = AnsiConsole.Prompt(new TextPrompt<string>("Email:")
                .PromptStyle("green")
                .ValidationErrorMessage("Please enter valid email address!")
                .Validate(email => ValidationHelpers.ValidateEmail(email) ? ValidationResult.Success() : ValidationResult.Error()));


            var password = AnsiConsole.Prompt(new TextPrompt<string>("Password:")
                .PromptStyle("red")//.Secret()
                .ValidationErrorMessage("Please enter valid password!")
                .Validate(p => ValidationHelpers.ValidatePass(p) ? ValidationResult.Success() : ValidationResult.Error()));

            return (email, password);
        }
        static (string, string) PromptPasswordChange()
        {
            var oldPassword = AnsiConsole.Prompt(new TextPrompt<string>("Old password:")
                .PromptStyle("red")//.Secret()
                .ValidationErrorMessage("Please enter valid password!")
                .Validate(p => ValidationHelpers.ValidatePass(p) ? ValidationResult.Success() : ValidationResult.Error()));

            var newPassword = AnsiConsole.Prompt(new TextPrompt<string>("New password:")
                .PromptStyle("red")//.Secret()
                .ValidationErrorMessage("Please enter valid password!")
                .Validate(p => ValidationHelpers.ValidatePass(p) ? ValidationResult.Success() : ValidationResult.Error()));

            return (oldPassword, newPassword);
        }
        static User PromptRegister()
        {
            var email = AnsiConsole.Prompt(new TextPrompt<string>("Email:")
                .PromptStyle("green")
                .ValidationErrorMessage("Please enter valid email address!")
                .Validate(email => ValidationHelpers.ValidateEmail(email) ? ValidationResult.Success() : ValidationResult.Error()));

            var name = AnsiConsole.Prompt(new TextPrompt<string>("Name:")
                .PromptStyle("green")
                .ValidationErrorMessage("Please enter valid name!")
                .Validate(p => ValidationHelpers.ValidateName(p) ? ValidationResult.Success() : ValidationResult.Error()));

            var surname = AnsiConsole.Prompt(new TextPrompt<string>("Surname:")
                .PromptStyle("green")
                .ValidationErrorMessage("Please enter valid surname!")
                .Validate(p => ValidationHelpers.ValidateName(p) ? ValidationResult.Success() : ValidationResult.Error()));

            var password = AnsiConsole.Prompt(new TextPrompt<string>("Password:")
                .PromptStyle("red")//.Secret()
                .ValidationErrorMessage("Please enter valid password!")
                .Validate(p => ValidationHelpers.ValidatePass(p) ? ValidationResult.Success() : ValidationResult.Error()));

            bool isadmin = AnsiConsole.Ask<bool>("IsAdmin?");


            return new User()
            {
                Name = name,
                Password = password,
                Email = email,
                SurName = surname,
                IsAdmin = isadmin
            };
        }

        static void LoginUser(string email, string password)
        {
            _loggedInUserEmail = email;
            AnsiConsole.Markup($"[underline green]User {email} logged in[/]");
            _actionontext = AfterLoginMenu;
            Console.WriteLine();
        }

        static void Logoff()
        {
            UserService.LogoutUser(_loggedInUserEmail);
            _loggedInUserEmail = null;
            _actionontext = HomeMenu;
        }
    }
}