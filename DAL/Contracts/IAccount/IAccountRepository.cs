using System.Threading.Tasks;
using izibongo.api.DAL.Models;

namespace izibongo.api.DAL.Contracts.IAccount
{
    public interface IAccountRepository
    {
        Task <bool>  Login(LoginModel model);
    }
}