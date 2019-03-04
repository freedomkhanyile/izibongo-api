using izibongo.api.DAL.Contracts.IAccount;

namespace izibongo.api.DAL.Contracts.IRepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        IAccountRepository Account {get;}
    }
}