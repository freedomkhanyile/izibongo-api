using System.Collections.Generic;
using AutoMapper;
using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace izibongo.api.API.Controllers
{

    [Route("api/[controller]")]
    public class FamiliesController : Controller
    {
        public FamiliesController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;

        [HttpGet]
        public IActionResult Get(){
            var families = _repositoryWrapper.Family.GetAllFamilies();
            if(families != null)
                return Ok(_mapper.Map<IEnumerable<FamilyModel>>(families));
            
            return BadRequest("Server Error please try again.");
        }

        
    }
}