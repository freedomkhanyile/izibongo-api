using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using izibongo.api.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace izibongo.api.DAL.DbContext
{
    public class DbInitializer
    {
        private RepositoryContext _repositoryContext;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            RepositoryContext repositoryContext
        )
        {
            _repositoryContext = repositoryContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            var user = await _userManager.FindByEmailAsync("admin@izibongo.co.za");
            if (user == null)
            {
                //Check if there is a role for or Admin
                if (!(await _roleManager.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    await _roleManager.CreateAsync(role);
                    await _roleManager.AddClaimAsync(role, new Claim("IsAdmin", "True"));
                }

                //Create a user object to seed.
                user = new User()
                {
                    UserName = "adminIzibongo",
                    FirstName = "admin",
                    LastName = "user",
                    Family = null,
                    Email = "admin@izibongo.co.za"
                };

                var createdUserResult = await _userManager.CreateAsync(user, "Izibongo2019@!");
                var addedToRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
                var addedToClaimsResult = await _userManager.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!createdUserResult.Succeeded || !addedToRoleResult.Succeeded || !addedToClaimsResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user or role");
                }
            }

            if(!_repositoryContext.Families.Any()){
                _repositoryContext.AddRange(_sampleFamily);
                await _repositoryContext.SaveChangesAsync();
            }

            if(!_repositoryContext.Statuses.Any()){
                _repositoryContext.AddRange(_sampleStatuses);
                await _repositoryContext.SaveChangesAsync();
            }

        }

        //Seed families Magwaza & Khanyile
        List<Family> _sampleFamily = new List<Family> {
            new Family(){
                
                Id =  Guid.NewGuid(),
                FamilyName ="Khanyile",
                FamilyClan ="Khanyile",
                FamilyOrigin ="Nkandla",
                FamilyLocation="Nkandla",
                CreateUserId = "1",
                CreateDate = DateTime.Now,
                ModifyUserId = "1",
                ModifyDate = DateTime.Now,
                StatusId = "55f8e2db-a8de-4b36-afe3-baa958df78e0"

            },
            new Family(){

                Id =  Guid.NewGuid(),
                FamilyName ="Magwaza",
                FamilyClan ="Salenzeni",
                FamilyOrigin ="Nkandla",
                FamilyLocation="Nkandla",
                CreateUserId = "1",
                CreateDate = DateTime.Now,
                ModifyUserId = "1",
                ModifyDate = DateTime.Now,
                StatusId = "55f8e2db-a8de-4b36-afe3-baa958df78e0"

            }
        };


        //Seed statuses Active & dissabled
        List<Status> _sampleStatuses = new List<Status>
        {
          new Status()
          {
              Id = new Guid("55f8e2db-a8de-4b36-afe3-baa958df78e0"),
              Description = "Active",
              CreateUserId = "1",
              CreateDate = DateTime.Now,
              ModifyUserId = "1",
              ModifyDate = DateTime.Now,
              IsActive = 1
          },
          new Status()
          {
              Id = new Guid("2bf8568b-c87c-4f2f-8a89-485f68d293a1"),
              Description = "Disabled",
              CreateUserId = "1",
              CreateDate = DateTime.Now,
              ModifyUserId = "1",
              ModifyDate = DateTime.Now,
              IsActive = 1
          }
        };

    }
}