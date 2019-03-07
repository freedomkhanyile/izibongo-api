using System;
using izibongo.api.DAL.Contracts.ILoggerService;
using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace izibongo.api.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        public AuthenticateController(
            IRepositoryWrapper repositoryWrapper,
            ILoggerService logger
            )
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }
        private IRepositoryWrapper _repositoryWrapper;
         private ILoggerService _logger;

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var result = _repositoryWrapper.Account.Login(model);
                if (result.Result.Token != null){
                    _logger.LogInfo($"Username {model.UserName}, was successfully authenticated");
                    return Ok(result.Result);
                }
                else {
                    _logger.LogError($"Username {model.UserName}, was not successully authenticated at {DateTime.Now}");
                     return BadRequest("Incorrect Username/Password");
                }                   
            }
            catch (System.Exception)
            {
                throw;
            }            
        }

    }
}