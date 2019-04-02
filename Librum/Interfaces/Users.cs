using System.Collections.Generic;
using System.Linq;
using Librum.Models;
using Microsoft.Extensions.Configuration;

namespace Librum.Interfaces
{
    public class Users : IUsers
    {
        public IConfiguration Configuration { get; }

        private List<UserStoreItem> _usersStore;

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