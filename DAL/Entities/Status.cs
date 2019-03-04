using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace izibongo.api.DAL.Entities
{
    public class Status 
    {
        [Column("StatusId")]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int IsActive { get; set; }
        
    }
}