using System;

namespace izibongo.api.DAL.Models
{
    public class FamilyModel
    {
        public Guid Id  { get; set; }
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
}