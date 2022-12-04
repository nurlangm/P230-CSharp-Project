using FinalAppCSharp.Entities;


namespace FinalAppCSharp.Interfaces
{
    public interface IUserRepository
    {
        public User? FindUserByEmail(string email);
        public User AddUser(User user);
        public ICollection<User> GetAllUsers();
        void UpdateUserByEmail(string email, User user);
    }
}
