using Sat.Recruitment.Model.Common;
using Sat.Recruitment.Model.Users;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<OperationResult> SaveUserAsync(UsersModel request, AppValues _appValues);
    }
}
