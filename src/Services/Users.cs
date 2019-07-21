using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Librum.Models;

namespace Librum.Services
{
    public class Users
    {
        public IConfiguration Configuration { get; }

        private readonly List<UserStoreItem> _usersStore;

        public Users(IConfiguration configuration)
        {
            Configuration = configuration;
            _usersStore = Configuration.GetSection("UsersStore").Get<UserStoreItem[]>().ToList();
        }

        public UserStoreItem GetUserByEmail(string authorEmail)
        {
            return _usersStore.First(x => x.Email.Equals(authorEmail));
        }
    }
}