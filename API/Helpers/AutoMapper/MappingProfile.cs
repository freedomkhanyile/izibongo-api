using AutoMapper;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Models;

namespace izibongo.api.API.Helpers.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Family,FamilyModel>()
            .ReverseMap();
        }
    }
}