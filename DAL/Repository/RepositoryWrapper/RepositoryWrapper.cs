using izibongo.api.DAL.Contracts.IAccount;
using izibongo.api.DAL.Contracts.IFamily;
using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.DbContext;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Repository.Account;
using izibongo.api.DAL.Repository.Families;
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
        private IFamilyRepository _family;
        public IAccountRepository Account {
            get {
                if(_account == null)
                        _account = new AccountRepository(_repositoryContext, _signInManager);
                
                return _account;
            }
        }

        public IFamilyRepository Family {
            get {
                if(_family == null)
                    _family = new FamilyRepository(_repositoryContext);
                return _family;
            }
        }
    }
}