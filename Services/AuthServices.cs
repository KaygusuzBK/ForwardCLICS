using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Services
{
    public class AuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User? Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }
            return null;
        }

        // Basit bir şifre kontrolü (hash yerine bunu kullanabilirsiniz)
        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // İdeal olarak burada bir hash karşılaştırma işlemi yapılır.
            return inputPassword == storedPassword;
        }
    }

    public interface IUserRepository
    {
        User? GetByEmail(string email);
        IEnumerable<User> GetAllUsers();
    }

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepository(List<User> users)
        {
            _users = users ?? new List<User>(); // Null durumunu yönetme
        }

        public User? GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            return _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }
    }
}
