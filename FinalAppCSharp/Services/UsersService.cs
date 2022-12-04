using FinalAppCSharp.Dtos;
using FinalAppCSharp.Entities;
using FinalAppCSharp.Interfaces;


namespace FinalAppCSharp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usersRepo;

        public UserService(IUserRepository usersRepo)
        {
            _usersRepo = usersRepo;
        }

        public IEnumerable<UserDto> ListUsers()
        {
            var users = _usersRepo.GetAllUsers();
            return users.Select(a => new UserDto() { Name = a.Name, Surname = a.SurName, Email = a.Email });
        }

        public double GetUserBalanceByEmail(string email)
        {
            var user = _usersRepo.FindUserByEmail(email);
            if (user != null)
            {
                return user.Balance;
            }
            return -1;
        }

        public UserDto? FindUser(string email)
        {
            var user = _usersRepo.FindUserByEmail(email);
            if (user != null)
            {
                return new UserDto() { Name = user.Name, Surname = user.SurName };
            }
            return null;
        }

        public bool IsUserExists(string email)
        {
            var user = _usersRepo.FindUserByEmail(email);
            return user != null;
        }

        public bool RegisterUser(User user)
        {
            if (IsUserExists(user.Email))
            {
                return false;
            }
            _usersRepo.AddUser(user);
            return true;
        }

        public bool LoginUser(string email, string password)
        {
            var user = _usersRepo.FindUserByEmail(email);
            if (user == null || user.IsBlocked)
            {
                return false;
            }

            if (user.Password == password)
            {
                user.IsLogged = true;
                return true;
            }
            return false;
        }
        public bool LogoutUser(string email)
        {
            var user = _usersRepo.FindUserByEmail(email);
            if (user == null || user.IsBlocked)
            {
                return false;
            }
            user.IsLogged = false;
            return true;
        }

        public bool IsUserAdmin(string email)
        {
            var user = _usersRepo.FindUserByEmail(email);
            if (user == null)
            {
                return false;
            }
            return user.IsAdmin;
        }

        public bool BlockUser(string userEmailToBlock)
        {
            var user = _usersRepo.FindUserByEmail(userEmailToBlock);
            if (user == null)
            {
                return false;
            }
            user.IsBlocked = true;
            return true;
        }

        public bool ChangeUserPassword(string email, string oldpass, string newpass)
        {
            var user = _usersRepo.FindUserByEmail(email);
            if (user == null)
            {
                return false;
            }
            if (user.Password == oldpass)
            {
                user.Password = newpass;
                return true;
            }
            return false;
        }

        public void TopUpBalanceUserBalance(string email, double amount)
        {
            var user = _usersRepo.FindUserByEmail(email);
            // if (user == null)
            // {
            //     return false;
            // }
            user.Balance += amount;
            _usersRepo.UpdateUserByEmail(email, user);
        }
    }
}
