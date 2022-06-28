using Sat.Recruitment.Model.Users;
using System.Threading.Tasks;

namespace Sat.Recruitment.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistAsync(UsersModel model);

        void SaveAsync(UsersModel model);
    }
}
