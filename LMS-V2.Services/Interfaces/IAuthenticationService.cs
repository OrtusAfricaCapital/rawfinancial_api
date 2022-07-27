using LMS_V2.Data.Models;
using LMS_V2.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Services.Interfaces
{
    public interface IAuthenticationService
    {
        (Staff, Tokens) Authenticate(UserAuth user);
        (Staff, Tokens) CreateAccount(CreateOrganisationVM createOrganisation);
    }
}
