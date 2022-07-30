using LMS_V2.Data.Models;
using LMS_V2.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS_V2.Services.Interfaces
{
    public interface IAccountservice
    {
        (Staff, Tokens) Authenticate(UserAuth user);
        Organisation FetchOrganisation(string name = "", int organisationId = 0);
        Organisation CreateOrganisation(Organisation createOrganisationVM);
        Staff FetchStaff(string email = "", int staffId = 0);
        Staff CreateStaff(Staff staff);
    }
}
