using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using izibongo.api.DAL.Entities.Extensions;

namespace izibongo.api.DAL.Entities
{

    public class Family : IEntity
    {
        public Guid Id  { get; set; }

        public string FamilyName { get; set; }
        public string FamilyClan { get; set; }
        public string FamilyOrigin { get; set; }
        public string FamilyLocation { get; set; }
        public string CreateUserId { get; set; }
        public IEnumerable<Isibongo> Izibongo { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string StatusId { get; set; }
    }
}