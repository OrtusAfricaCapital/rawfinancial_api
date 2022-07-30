using LMS_V2.Data.Models;
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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountservice _authenticationServce;

        public AccountsController(IAccountservice authenticationServce)
        {
            _authenticationServce = authenticationServce;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Auth")]
        public IActionResult Authenticate(UserAuth staff)
        {
            var account = _authenticationServce.Authenticate(staff);
            if (account.Item1 == null)
                return Unauthorized();

            return Ok(new
            {
                Account = account.Item1,
                Tokens = account.Item2
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Create/Organisation")]
        public IActionResult CreateOrganisation(Organisation organisation)
        {
            var existingOrg = _authenticationServce.FetchOrganisation(organisation.Name);
            if (existingOrg != null)
                return new BadRequestObjectResult("Orgnasation account already exists");
            try
            {
                var newOrg = _authenticationServce.CreateOrganisation(organisation);
                return new CreatedResult("/Account/Create/Staff", newOrg);
            }
            catch (Exception exp)
            {
                return new BadRequestObjectResult(exp.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Create/Staff")]
        public IActionResult CreateAccount(Staff newStaff)
        {
            try
            {
                var staff = _authenticationServce.CreateStaff(newStaff);
                return new CreatedResult("/Account/Create/Staff", staff);
            }
            catch(InvalidAccountExceptions exp)
            {
                return Ok(exp.Message);
            }catch(Exception exp)
            {
                return Ok(exp.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Staff")]
        public IActionResult FetchStaff(string email = "", int staffId = 0)
        {
            var staff = _authenticationServce.FetchStaff(email, staffId);
            return Ok(staff);
        }

        [HttpGet]
        [Route("Organisation")]
        public IActionResult FetchOrganisation(string organsationName = "", int organisationId = 0)
        {
            var org = _authenticationServce.FetchOrganisation(organsationName, organisationId);
            return Ok(org);
        }
    }
}
