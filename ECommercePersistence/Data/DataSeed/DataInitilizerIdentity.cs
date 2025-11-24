using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence.Data.DataSeed
{
    public class DataInitilizerIdentity : IDataInitilizer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DataInitilizerIdentity> _logger;

        public DataInitilizerIdentity(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DataInitilizerIdentity> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitilizeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }
                if (!_userManager.Users.Any())
                {
                    var adminUser = new ApplicationUser
                    {
                        DisplayName = "Arwa",
                        UserName = "AdminUser",
                        Email = "arwa.walaa88@gmail.com",
                        PhoneNumber = "01009765043"
                    };
                    var SuperadminUser = new ApplicationUser
                    {
                        DisplayName = "Superadmin",
                        UserName = "Superadmin",
                        Email = "Superadmin@gmail.com",
                        PhoneNumber = "01009765040"
                    };

                    await _userManager.CreateAsync(adminUser, "Admin@123");
                    await _userManager.CreateAsync(SuperadminUser, "Admin@123");
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(SuperadminUser, "SuperAdmin");

                }
                    }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during Identity data seeding: {ex}");

            }


        }
    }
}
