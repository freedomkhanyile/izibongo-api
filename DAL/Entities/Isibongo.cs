using System;
using System.ComponentModel.DataAnnotations.Schema;
using izibongo.api.DAL.Entities.Extensions;

namespace izibongo.api.DAL.Entities
{
    public class Isibongo : IEntity
    {
        public Guid Id  { get; set; }
        public string MemberName { get; set; }
        public string MemberTitle { get; set; }
        public string Body { get; set; }           
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string StatusId { get; set; }
        public Guid FamilyId { get; set; }

        //User that wrote the Isibongo
        public User User { get; set; }


    }
}