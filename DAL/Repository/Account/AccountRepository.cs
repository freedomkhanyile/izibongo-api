using System.Threading.Tasks;
using izibongo.api.DAL.Contracts.IAccount;
using izibongo.api.DAL.DbContext;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace izibongo.api.DAL.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository(
           RepositoryContext context,
           SignInManager<User> signInManager
        )
        {
            _signInManager = signInManager;
            _context = context;
        }
        private RepositoryContext _context;
        private SignInManager<User> _signInManager;
        public async Task<bool> Login(LoginModel model)
        {
             var isDone = false;
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if(result.Succeeded)
                isDone = true;
            
            return isDone;
        }
    }
}