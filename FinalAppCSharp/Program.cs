using FinalAppCSharp;
using FinalAppCSharp.Services;

string select;
Console.WriteLine("Banka Xos gelmisiniz");
selection:
do
{
    Console.WriteLine("1. User Reg");
    Console.WriteLine("2. User Login");
    Console.WriteLine("3. Find User");
    Console.WriteLine("0. Exit");
    select = Console.ReadLine();

    switch (select)
    {
        case "1":
            MenuService.UserRegistration();
            break;
        case "2":
            
            break;
        case "3":
            break;
        case "0":
            break;
        default:
            Console.WriteLine("Choose correct number");
            goto selection;
    }
} while (select != "0");
