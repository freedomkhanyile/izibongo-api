using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using izibongo.api.API.Helpers.HATEOAS;
using izibongo.api.DAL.Contracts.IRepositoryWrapper;
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
            IUrlHelper urlHelper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _urlHelper = urlHelper;
        }
        private IRepositoryWrapper _repositoryWrapper;
        private IMapper _mapper;
        private IUrlHelper _urlHelper;

        [HttpGet(Name = "GetAllFamilies")]
        public IActionResult Get(ResourceParameter resourceParameter)
        {
            // var families = _repositoryWrapper.Family.GetAllFamilies();
            // if (families != null)
            //     return Ok(_mapper.Map<IEnumerable<FamilyModel>>(families));

            // return BadRequest("Server Error please try again.");
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
                    _urlHelper.Link("GetFamilyById", new { }),
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