using System;

namespace izibongo.api.DAL.Models
{
    public class IsibongoModel
    {
        public Guid Id  { get; set; }
        public string MemberName { get; set; }
        public string MemberTitle { get; set; }
        public string Body { get; set; }    
        public string Author { get; set; }   
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public string StatusId { get; set; }
    }
}