using LMS_V2.Services.Interfaces;
using LMS_V2.Shared.Exceptions;
using LMS_V2.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_V2.API.Controllers
{
    [Authorize]
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationServce;

        public AccountController(IAuthenticationService authenticationServce)
        {
            _authenticationServce = authenticationServce;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UserAuth user)
        {
            var account = _authenticationServce.Authenticate(user);
            if (account.Item1 == null)
                return Unauthorized();

            return Ok(new
            {
                Account = account.Item1,
                Organisation = account.Item2
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateAccount(CreateOrganisationVM organisationVM)
        {
            try
            {
                var account = _authenticationServce.CreateAccount(organisationVM);
                return Ok(new
                {
                    Account = account.Item1,
                    Organisation = account.Item2
                });
            }
            catch(InvalidAccountExceptions exp)
            {
                return Ok(exp.Message);
            }catch(Exception exp)
            {
                return Ok(exp.Message);
            }
        }
    }
}
