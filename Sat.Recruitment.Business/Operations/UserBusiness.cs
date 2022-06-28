using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Data.Repository.Interfaces;
using Sat.Recruitment.Model.Common;
using Sat.Recruitment.Model.Users;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Operations
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> SaveUserAsync(UsersModel request, AppValues _appValues)
        {
            switch (request.UserType)
            {
                case "Normal":

                    if (request.Money > 100)
                    {
                        //If new user is normal and has more than USD100
                        request.Money = request.Money + (request.Money * _appValues.UsersPercentage.Normal);
                    }
                    else if(request.Money > 10 && request.Money < 100)
                    {
                        request.Money = request.Money + (request.Money * _appValues.UsersPercentage.NormalLowerLimit);
                    }

                    break;

                case "SuperUser":

                    if (request.Money > 100)
                    {
                        request.Money = request.Money + (request.Money * _appValues.UsersPercentage.SuperUser);
                    }

                    break;

                case "Premium":

                    if (request.Money > 100)
                    {
                        request.Money = request.Money + (request.Money * _appValues.UsersPercentage.Premium);
                    }

                    break;
            }

            if(await _userRepository.UserExistAsync(request))
            {
                return new OperationResult
                {
                    IsSuccess = false,
                    Message = "User is duplicated"
                };
            }

            _userRepository.SaveAsync(request);

            return new OperationResult
            {
                IsSuccess = true,
                Message = "User Created"
            };
        }
    }
}
