using System;
using System.ComponentModel.DataAnnotations.Schema;
using izibongo.api.DAL.Entities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace izibongo.api.DAL.Entities
{
    public class User : IdentityUser
    {  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Family Family { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string StatusId { get; set; }
    }
}