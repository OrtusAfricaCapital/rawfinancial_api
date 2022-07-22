using LMS_V2.Data.Models;
using LMS_V2.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Services
{
    public interface IAccountService
    {
        Staff Login(StaffLoginVM staffLogin);
        Organisation Create(CreateOrganisationVM createOrganisation);
    }
}
