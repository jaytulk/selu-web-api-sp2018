using System.Collections.Generic;

namespace CoreWebAPI
{
    public class UserDataContext
    {
        public List<User> Users { get; set; } = new List<User>
        {
            new User {Id = 1, Name = "Bob"},
            new User {Id = 2, Name = "Terry", Active = false},
            new User {Id = 3, Name = "Kim"},
            new User {Id = 4, Name = "Horton", Active = false},
            new User {Id = 5, Name = "Beverly"},
            new User {Id = 6, Name = "Willis"},
        };

        private static UserDataContext Instance { get; set; }

        protected UserDataContext()
        {
        }

        public static UserDataContext GetInstance()
        {
            Instance = Instance ?? new UserDataContext();
            return Instance;
        }

        public void Add(User user)
        {
            Users.Add(user);
        }

        public List<User> GetUsers( )
        {
            return Users;
        }
    }
}