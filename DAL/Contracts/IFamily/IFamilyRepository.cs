using System;
using System.Collections.Generic;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Models;

namespace izibongo.api.DAL.Contracts.IFamily
{
    public interface IFamilyRepository
    {
        IEnumerable<Family> GetAllFamilies();
        Family GetAFamily(Guid id);
        FamilyModelExtended GetAFamilyWithIzibongo(Guid id);
        bool AddFamily(Family model);
        bool UpdateFamily(Family dbModel, FamilyModel model);
    }
}