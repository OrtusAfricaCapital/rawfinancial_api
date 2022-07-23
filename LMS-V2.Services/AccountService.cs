using LMS_V2.Data;
using LMS_V2.Data.Models;
using LMS_V2.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Services
{
    public class AccountService : IAccountService
    {
        //private readonly LMSDbContext _dbContext;
        //public AccountService(LMSDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        public Organisation Create(CreateOrganisationVM createOrganisation)
        {
            //check if email already registered or name
            //register organisation and admin 
            throw new NotImplementedException();
        }

        public Staff Login(StaffLoginVM staffLogin)
        {
            throw new NotImplementedException();
        }
    }
}
