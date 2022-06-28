using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Business.Operations;
using Sat.Recruitment.Data.Repository.Interfaces;
using Sat.Recruitment.Data.Repository.Operations;
using Sat.Recruitment.Model.Common;
using Sat.Recruitment.Model.Users;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }

    [CollectionDefinition("Users", DisableParallelization = true)]
    public class UsersTest
    {
        private readonly IUserBusiness _userBusiness;

        public UsersTest(IUserBusiness userBusiness) => _userBusiness = userBusiness;

        [Fact]
        public void CreateUserValid()
        {
            // Arrange
            var user = new UsersModel
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var appValues = new AppValues()
            {
                UsersPercentage = new UsersPercentage
                {
                    Normal = (decimal)0.12,
                    NormalLowerLimit = (decimal)0.08,
                    SuperUser = (decimal)0.20,
                    Premium = 2
                }
            };

            IOptions<AppValues> options = Options.Create(appValues);

            var userController = new UsersController(_userBusiness, options);

            // Act
            IActionResult result = userController.CreateUser(user).Result;

            // Assert
            var acceptedObjectResult = result as AcceptedResult;
            dynamic resultOperation = acceptedObjectResult.Value;

            Assert.NotNull(acceptedObjectResult);
            Assert.True(resultOperation.IsSuccess);
            Assert.Equal("User Created", resultOperation.Message);
        }

        [Fact]
        public void CreateUserExist()
        {
            // Arrange
            var user = new UsersModel
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var appValues = new AppValues()
            {
                UsersPercentage = new UsersPercentage
                {
                    Normal = (decimal)0.12,
                    NormalLowerLimit = (decimal)0.08,
                    SuperUser = (decimal)0.20,
                    Premium = 2
                }
            };

            IOptions<AppValues> options = Options.Create(appValues);

            var userController = new UsersController(_userBusiness, options);

            // Act
            IActionResult result = userController.CreateUser(user).Result;

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            dynamic resultOperation = badRequestResult.Value;

            Assert.NotNull(badRequestResult);
            Assert.False(resultOperation.IsSuccess);
            Assert.Equal("User is duplicated", resultOperation.Message);
        }
    }
}
