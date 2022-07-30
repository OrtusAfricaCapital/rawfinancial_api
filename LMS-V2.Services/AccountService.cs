using LMS_V2.Data;
using LMS_V2.Data.Models;
using LMS_V2.Services.Interfaces;
using LMS_V2.Shared.Enums;
using LMS_V2.Shared.Exceptions;
using LMS_V2.Shared.Helpers;
using LMS_V2.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LMS_V2.Services
{
    public class AccountService : IAccountservice
    {
		private readonly LMSDbContext _dbContext;
		private readonly IConfiguration _configuration;

		public AccountService(IConfiguration configuration, LMSDbContext dbContext)
		{
			_configuration = configuration;
			_dbContext = dbContext;
		}

		public (Staff, Tokens) Authenticate(UserAuth user)
		{
			var account = _dbContext.Staffs
				.Where(x => x.Email.Equals(user.Email.ToLower()))
				.FirstOrDefault();

			if (account == null)
				return (null, null);

			var hashedPassword = PasswordHelpers.ComputeHash(Encoding.UTF8.GetBytes(user.Password), Encoding.UTF8.GetBytes(account.PasswordSalt));

			if (hashedPassword != account.PasswordHash)
				return (null, null);

			return (account, new Tokens { Token = GenerateToken(user.Email) });
		}

		public Organisation FetchOrganisation(string name, int id)
        {
            if (!string.IsNullOrEmpty(name))
            {
				var organisation = _dbContext.Organisations.Where(x => x.Name.Equals(name)).FirstOrDefault();
				return organisation;
			}
			
			if(id != 0)
            {
				var org = _dbContext.Organisations.Find(id);
				return org;
            }

			return null;
		}

		public Organisation CreateOrganisation(Organisation newOrgansation)
        {
			_dbContext.Organisations.Add(newOrgansation);
			_dbContext.SaveChanges();
			return newOrgansation;
		}

		public Staff FetchStaff(string email, int id)
		{
			if(!string.IsNullOrEmpty(email))
            {
				var staff = _dbContext.Staffs.Where(x => x.Email.Equals(email.ToLower())).FirstOrDefault();
				return staff;
			}
			
			if(id != 0)
            {
				var staff = _dbContext.Staffs.Find(id);
				return staff;
			}
			return null;
		}

		public Staff CreateStaff(Staff staff)
        {
			_dbContext.Add(staff);
			_dbContext.SaveChanges();
			return staff;
        }

		#region private helpers
		private string GenerateToken(string email)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, email)
				}),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
		#endregion private helpers
	}
}
