using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using izibongo.api.API.Helpers.HATEOAS;
using izibongo.api.DAL.Contracts.ILoggerService;
using izibongo.api.DAL.Contracts.IRepositoryWrapper;
using izibongo.api.DAL.Entities.Extensions;
using izibongo.api.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace izibongo.api.API.Controllers
{

    [Route("api/[controller]")]
    public class FamiliesController : Controller
    {
        public FamiliesController(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IUrlHelper urlHelper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _urlHelper = urlHelper;
            _logger = logger;
        }
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private IUrlHelper _urlHelper;
        private ILoggerService _logger;


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

        [HttpGet("{id}/izibongo")]
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
                else{
                    _logger.LogInfo($"Family with name {family.FamilyName} and id: {family.Id}, was returned successfully");
                    return Ok(family);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
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

            return familyWrapper;
        }

        #endregion


    }
}