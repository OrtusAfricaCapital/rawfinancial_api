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
    public class AuthenticationService : IAuthenticationService
    {
		private readonly LMSDbContext _dbContext;
		private readonly IConfiguration _configuration;

		public AuthenticationService(IConfiguration configuration, LMSDbContext dbContext)
		{
			_configuration = configuration;
			_dbContext = dbContext;
		}

		public (Staff, Tokens) Authenticate(UserAuth user)
		{
			var account = _dbContext.Staffs
				.Where(x => x.Email.Equals(user.Email.ToLower()))
				.Include(x => x.Organisation)
				.FirstOrDefault();

			if (account == null)
				return (null, null);

			var hashedPassword = PasswordHelpers.ComputeHash(Encoding.UTF8.GetBytes(user.Password), Encoding.UTF8.GetBytes(account.PasswordSalt));

			if (hashedPassword != account.PasswordHash)
				return (null, null);

			return (account, new Tokens { Token = GenerateToken(user.Email) });
		}

		public (Staff, Tokens) CreateAccount(CreateOrganisationVM createOrganisation)
        {
			var organisation = _dbContext.Organisations.Where(x => x.Name.Equals(createOrganisation.OrganisationName)).FirstOrDefault();
			var user = _dbContext.Staffs.Where(x => x.Email.ToLower().Equals(createOrganisation.UserWorkEmail)).FirstOrDefault();

			if (organisation != null)
				throw new InvalidAccountExceptions("Organisation Account Name already registered");
			if (user != null)
				throw new InvalidAccountExceptions($"Account with Email {createOrganisation.UserWorkEmail} Already registered");
			if (string.IsNullOrEmpty(createOrganisation.Password))
				throw new InvalidAccountExceptions("Invalid password");

			try
            {
				var newOrganisation = new Organisation
				{
					Name = createOrganisation.OrganisationName,
					CreatedOnUTC = DateTime.UtcNow
				};
				var newOrganisationData = _dbContext.Add(newOrganisation);
				var update = _dbContext.SaveChanges();

				if (update > 0)
				{
					var newSalt = PasswordHelpers.GenerateSalt();
					var hashedPassword = PasswordHelpers.ComputeHash(Encoding.UTF8.GetBytes(createOrganisation.Password), Encoding.UTF8.GetBytes(newSalt));

					var newUser = new Staff
					{
						FullName = createOrganisation.UserFullName,
						Email = createOrganisation.UserWorkEmail.ToLower(),
						PasswordSalt = newSalt,
						PasswordHash = hashedPassword,
						StaffRole = StaffRole.ADMINISTRATOR,
						OrganisationId = newOrganisation.OrganisationId,
						CreatedOnUTC = DateTime.UtcNow
					};
					_dbContext.Add(newUser);
					_dbContext.SaveChanges();
				}
			}catch(Exception exp)
            {
				throw new Exception("Error creating Organisation Accout");
            }

			var userAccount = _dbContext.Staffs
				.Where(x => x.Email.ToLower().Equals(createOrganisation.UserWorkEmail))
				.Include(x => x.Organisation)
				.FirstOrDefault();
			return (userAccount, new Tokens { Token = GenerateToken(createOrganisation.UserWorkEmail.ToLower()) });
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
