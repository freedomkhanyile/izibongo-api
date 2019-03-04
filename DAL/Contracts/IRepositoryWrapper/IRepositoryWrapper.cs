using izibongo.api.DAL.Contracts.IAccount;
using izibongo.api.DAL.Contracts.IFamily;

namespace izibongo.api.DAL.Contracts.IRepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        IAccountRepository Account {get;}
        IFamilyRepository Family {get;}

    }
}