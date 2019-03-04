using izibongo.api.DAL.Contracts.IAccount;
using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.DbContext;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Repository.Account;
using Microsoft.AspNetCore.Identity;

namespace izibongo.api.DAL.Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public RepositoryWrapper(RepositoryContext repositoryContext, SignInManager<User> signInManager)
        {
            _repositoryContext = repositoryContext;
            _signInManager = signInManager;
        }

        private RepositoryContext _repositoryContext { get; set; }
        private SignInManager<User> _signInManager { get; set; }
    
        private IAccountRepository _account;
        public IAccountRepository Account {
            get {
                if(_account == null)
                        _account = new AccountRepository(_repositoryContext, _signInManager);
                
                return _account;
            }
        }
    }
}