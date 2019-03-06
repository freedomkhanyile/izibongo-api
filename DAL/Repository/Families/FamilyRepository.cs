using System;
using System.Collections.Generic;
using System.Linq;
using izibongo.api.DAL.Contracts.IFamily;
using izibongo.api.DAL.DbContext;
using izibongo.api.DAL.Entities;
using izibongo.api.DAL.Models;
using izibongo.api.DAL.Repository.RepositoryBase;

namespace izibongo.api.DAL.Repository.Families
{
    public class FamilyRepository
            : RepositoryBase<Family>, IFamilyRepository
    {
        public FamilyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
        public bool AddFamily(Entities.Family model)
        {
            throw new NotImplementedException();
        }

        public Entities.Family GetAFamily(Guid id)
        {
            return _repositoryContext.Families
                    .Where(f => f.Id == id)
                    .DefaultIfEmpty(new Family())
                    .FirstOrDefault();
        }

        public FamilyModelExtended GetAFamilyWithIzibongo(Guid id)
        {
            return new FamilyModelExtended(GetAFamily(id))
            {
                Izibongo = _repositoryContext.Izibongo
                            // .Where(i => i.Family.Id == id)
                            .Where(i => i.FamilyId == id)
            };
        }

        public IEnumerable<Entities.Family> GetAllFamilies()
        {
            return _repositoryContext.Families.ToList();
        }

        public bool UpdateFamily(Entities.Family dbModel, FamilyModel model)
        {
            throw new NotImplementedException();
        }
    }
}