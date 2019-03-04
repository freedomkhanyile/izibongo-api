using System;

namespace izibongo.api.DAL.Entities.Extensions
{
    public interface IEntity
    {
        Guid Id  { get; set; }

        string CreateUserId { get; set; }
        DateTime CreateDate { get; set; }
        string ModifyUserId { get; set; }
        DateTime ModifyDate { get; set; }
        string StatusId { get; set; }
    }
}