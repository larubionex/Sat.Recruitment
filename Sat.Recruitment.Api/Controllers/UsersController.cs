using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Model.Common;
using Sat.Recruitment.Model.Users;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly AppValues _appValues;

        public UsersController(IUserBusiness userBusiness, IOptions<AppValues> appValues)
        {
            _userBusiness = userBusiness;
            _appValues = appValues.Value;
        }

        /// <summary>
        /// Description
        /// </summary>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 202)]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UsersModel request)
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from modelStateVal in ModelState.Values
                                 from error
                                    in modelStateVal.Errors
                                 select error.ErrorMessage).ToList();

                return BadRequest(new OperationResult()
                {
                    IsSuccess = false,
                    Message = errorList.ToString()
                });
            }

            try
            {
                var result = await _userBusiness.SaveUserAsync(request, _appValues);

                if (result.IsSuccess)
                {
                    return Accepted(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
