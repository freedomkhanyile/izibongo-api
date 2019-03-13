using System;
using izibongo.api.DAL.Models;

namespace izibongo.api.DAL.Entities.Extensions.Family
{
    public static class FamilyExtensions
    {
        public static void Map(this Entities.Family dbModel, FamilyModel model)
        {
            dbModel.FamilyClan = model.FamilyClan;
            dbModel.FamilyName = model.FamilyName;
            dbModel.FamilyOrigin = model.FamilyOrigin;
            dbModel.FamilyLocation = model.FamilyLocation;
            dbModel.ModifyUserId = model.ModifyUserId;
            dbModel.StatusId = model.StatusId;
            dbModel.ModifyDate = DateTime.Now;            
        }
    }
}