using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace izibongo.api.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        public AuthenticateController(IRepositoryWrapper repositoryWrapper)
        {

            _repositoryWrapper = repositoryWrapper;
        }
        private IRepositoryWrapper _repositoryWrapper;

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var result = _repositoryWrapper.Account.Login(model);
                if (result.Result)
                    return Ok();
            }
            catch (System.Exception)
            {

                throw;
            }

            return BadRequest("Failed to login");
        }

    }
}