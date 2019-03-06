using System;
using System.Collections.Generic;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Entities.Extensions;

namespace izibongo.api.DAL.Models
{
    public class FamilyModel : LinkedResourceBaseModel, IEntity
    {
        public FamilyModel() { }        
        public Guid Id { get; set; }
        public string FamilyName { get; set; }
        public string FamilyClan { get; set; }
        public string FamilyOrigin { get; set; }
        public string FamilyLocation { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string StatusId { get; set; }
        
    }

    public class FamilyModelExtended : IEntity
    {
        public FamilyModelExtended(){}
          public FamilyModelExtended(Family family)
        {
            Id = family.Id;
            FamilyName = family.FamilyName;
            FamilyClan = family.FamilyClan;
            FamilyOrigin = family.FamilyOrigin;
            FamilyLocation = family.FamilyLocation; 
            CreateUserId = family.CreateUserId; 
            CreateDate = family.CreateDate; 
            ModifyUserId = family.ModifyUserId; 
            ModifyDate = family.ModifyDate; 
            StatusId = family.StatusId;
        }
        public Guid Id { get; set; }
        public string FamilyName { get; set; }
        public string FamilyClan { get; set; }
        public string FamilyOrigin { get; set; }
        public string FamilyLocation { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string StatusId { get; set; }
        public IEnumerable<Isibongo> Izibongo { get; set; }
    }
}