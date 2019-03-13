using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using izibongo.api.API.Helpers.HATEOAS;
using izibongo.api.DAL.Contracts.ILoggerService;
using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Entities.Extensions;
using izibongo.api.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace izibongo.api.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class FamiliesController : Controller
    {
        public FamiliesController(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IUrlHelper urlHelper,
            ILoggerService logger,
            UserManager<User> userManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _urlHelper = urlHelper;
            _logger = logger;
            _userManager = userManager;
        }
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private IUrlHelper _urlHelper;
        private ILoggerService _logger;
        private UserManager<User> _userManager;


        [HttpGet(Name = "GetAllFamilies")]
        public IActionResult Get(ResourceParameter resourceParameter)
        {
            try
            {
                var familyList = _mapper.Map<IEnumerable<FamilyModel>>
                             (_repositoryWrapper.Family.GetAllFamilies());

                var pagedList = PageList<FamilyModel>.Create(
                    familyList,
                    resourceParameter.PageNumber,
                    resourceParameter.PageSize
                );

                var previousPageLink = pagedList.HasPrevious
                                    ? CreateResourceUri(resourceParameter, ResourceUriType.PreviousPage) : null;

                var nextPageLink = pagedList.HasNext
                                   ? CreateResourceUri(resourceParameter, ResourceUriType.NextPage) : null;

                //Add link for each Family resource
                var familyModels = familyList.Select(f => CreateLinksForResource(f));

                var wrapper = new LinkedCollectionResourceModel<FamilyModel>(familyModels);

                if (wrapper != null)
                    return Ok(this.CreateLinksForResource(wrapper));

                else
                    return BadRequest("Server Error please try again.");


            }
            catch (System.Exception)
            {
                return StatusCode(500, "internal server error");
            }

        }

        [HttpGet("{id}", Name = "GetFamilyById")]
        public IActionResult Get(string id)
        {
            try
            {
                var family = _mapper.Map<FamilyModel>(_repositoryWrapper.Family.GetAFamily(new Guid(id)));

                if (family.IsEmptyObject())
                {
                    _logger.LogError($"Family with id: {id}, has not been found in our records at {DateTime.Now}");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Family with Name {family.FamilyName} & Id: {family.Id} was retured successfully");
                    return Ok(CreateLinksForResource(family));
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpGet("{id}/izibongo", Name = "GetFamilyWithIzibongo")]
        public IActionResult GetFamilyWithIzibongo(string id)
        {
            try
            {
                var family = _repositoryWrapper.Family.GetAFamilyWithIzibongo(new Guid(id));
                if (family.IsEmptyObject())
                {
                    _logger.LogError($"Family with id: {id}, has not been found in our records at {DateTime.Now}");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Family with name {family.FamilyName} and id: {family.Id}, was returned successfully");
                    return Ok(family);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPost(Name = "AddFamily")]
        public async Task<IActionResult> Post([FromBody] Family model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);

                if (user != null)
                {
                    model.CreateUserId = user.Id;
                    model.ModifyUserId = model.CreateUserId;
                }
                else {
                     return BadRequest("User management fault. Contact system administratore");
                }
                model.Id = Guid.NewGuid();
                model.CreateDate = DateTime.Now;
                model.ModifyDate = DateTime.Now;
                model.StatusId = "55f8e2db-a8de-4b36-afe3-baa958df78e0";

                if (_repositoryWrapper.Family.AddFamily(model))
                    return CreatedAtRoute("GetFamilyById", new { id = model.Id }, model);
            }
            catch (System.Exception)
            {

                throw;
            }
            return BadRequest("Server Error please try again.");
        }

        [HttpPut("{id}", Name = "EditFamily")]
        public async Task<IActionResult> Put(string id, [FromBody]FamilyModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            
                if (user != null)                                  
                    model.ModifyUserId = user.Id;
                else  
                    return BadRequest("User management fault. Contact system administratore");

                var dbModel = _repositoryWrapper.Family.GetAFamily(new Guid(id));

                if(dbModel.IsObjectNull()) return NotFound($"Could not find a Family with Id {id} to Update");
                
                if(_repositoryWrapper.Family.UpdateFamily(dbModel, model))  return CreatedAtRoute("GetFamilyById", new { id = dbModel.Id }, dbModel);

            }
            catch (System.Exception)
            {

                throw;
            }
            return BadRequest("Server Error please try again.");
        }





        #region Links for the Families resource using HATEOAS

        private string CreateResourceUri(
            ResourceParameter resourceParameter,
            ResourceUriType uriType)
        {
            switch (uriType)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetAllFamilies", new
                    {
                        pageNumber = resourceParameter.PageNumber - 1,
                        pageSize = resourceParameter.PageSize
                    });

                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetAllFamilies", new
                    {
                        pageNumber = resourceParameter.PageNumber + 1,
                        pageSize = resourceParameter.PageSize
                    });
                default:
                    return _urlHelper.Link("GetAllFamilies", new
                    {
                        pageNumber = resourceParameter.PageNumber,
                        pageSize = resourceParameter.PageSize
                    });
            }
        }

        private FamilyModel CreateLinksForResource(FamilyModel model)
        {
            model.Links.Add(
                new LinkModel(
                    _urlHelper.Link("GetFamilyById", new { id = model.Id }),
                    "Get_Family_By_Id",
                    "GET"
                ));
            model.Links.Add(
                new LinkModel(
                    _urlHelper.Link("GetFamilyWithIzibongo", new { id = model.Id }),
                    "Get_Family_With_Izibongo",
                    "GET"
                ));
            model.Links.Add(
                new LinkModel(
                    _urlHelper.Link("EditFamily", new { id = model.Id }),
                    "Edit_Family",
                    "PUT"
                ));

            return model;
        }


        private LinkedCollectionResourceModel<FamilyModel> CreateLinksForResource(
            LinkedCollectionResourceModel<FamilyModel> familyWrapper
        )
        {
            familyWrapper.Links.Add(
                new LinkModel(_urlHelper.Link("GetAllFamilies", new { }),
                "self",
                "GET"
                ));
            
            familyWrapper.Links.Add(
                new LinkModel(_urlHelper.Link("AddFamily", new { }),
                "Add_A_Family",
                "POST"
                ));
            

            return familyWrapper;
        }

        #endregion


    }
}