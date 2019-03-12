using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using izibongo.api.DAL.Contracts.IAccount;
using izibongo.api.DAL.DbContext;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace izibongo.api.DAL.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository(
           RepositoryContext context,
           SignInManager<User> signInManager,
           IConfigurationRoot config,
           UserManager<User> userManager
        )
        {
            _signInManager = signInManager;
            _context = context;
            _config = config;
            _userManager = userManager;
        }
        private RepositoryContext _context;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IConfigurationRoot _config;
        public async Task<TokenResponse> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (!result.Succeeded)
            {
                return new TokenResponse();
            }
            else
            {
                var _user = await _userManager.FindByNameAsync(model.UserName);
                var roles = await _userManager.GetRolesAsync(_user);
                var claims = new List<Claim>();             
                
                claims.Add(new Claim(ClaimTypes.Email, _user.Email));
                claims.Add(new Claim(ClaimTypes.Name, _user.UserName));                 
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role , role));
                }

                var KeySecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Security:Secret"]));
                var credentials = new SigningCredentials(KeySecurity, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                     issuer: "https://www.izibongo.co.za",
                    audience: "https://www.izibongo.co.za",
                    claims: claims,
                    expires: DateTime.Now.AddHours(9),
                    signingCredentials: credentials
                );

                var response = new TokenResponse();
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return response;
            }

        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }
    }
}