using Librum.Models;

namespace Librum.Interfaces
{
    public interface IUsers
    {
        UserStoreItem GetUserByEmail(string authorEmail);
    }
}