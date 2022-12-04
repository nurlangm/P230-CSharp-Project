using FinalAppCSharp.Entities;
using FinalAppCSharp.Interfaces;

namespace FinalAppCSharp.Repos
{
    public class UsersRepo : IUserRepository
    {
        private int ID = 0;
        private IList<User> Users = new List<User>();

        public UsersRepo()
        {
    
        }

        public User? FindUserByEmail(string email)
        {
            return Users.SingleOrDefault(user => user.Email == email);
        }

        public User AddUser(User user)
        {
            // validate?
            user.Id = ID;
            Users.Add(user);
            ID += 1;
            return user;
        }

        public ICollection<User> GetAllUsers()
        {
            return Users;
        }

        public void UpdateUserByEmail(string email, User user)
        {
            var findedUser = Users.SingleOrDefault(u => u.Email == email);
            if (findedUser != null) findedUser.Balance = user.Balance;
        }
    }
}
