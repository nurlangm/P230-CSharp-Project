
using FinalAppCSharp.Dtos;
using FinalAppCSharp.Entities;

namespace FinalAppCSharp.Interfaces
{
    public interface IUserService
    {
        public UserDto? FindUser(string email);

        public bool RegisterUser(User user);
        public IEnumerable<UserDto> ListUsers();

        public double GetUserBalanceByEmail(string email);
        public bool IsUserExists(string email);
        public bool LoginUser(string email, string password);
        public bool LogoutUser(string email);
        public bool IsUserAdmin(string email);
        public bool BlockUser(string userEmailToBlock);
        public bool ChangeUserPassword(string email, string oldpass, string newpass);
        public void TopUpBalanceUserBalance(string email, double amount);
    }
}
