using LotteryStatsMVCApp.Data;
using LotteryStatsMVCApp.Models.Enums;
using LotteryStatsMVCApp.Models.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LotteryStatsMVCApp.Services
{
    public class SeedService
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Constructor
        public SeedService(UserManager<IdentityUser> userManager,
                    IOptions<AppSettings> appSettings,
                    RoleManager<IdentityRole> roleManager,
                    ApplicationDbContext context)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _context = context;
        }
        #endregion

        #region Manage Data
        public async Task ManageDataAsync()
        {
            // Create the Db from the migrations
            await _context.Database.MigrateAsync();

            // Seed roles into the system
            await SeedRolesAsync();

            // Seed users into the system
            await SeedUsersAsync();
        }
        #endregion

        #region Seed Roles
        private async Task SeedRolesAsync()
        {
            // If there are already Roles in the system, do nothing.
            if (_context.Roles.Any())
            {
                return;
            }
            else
            {
                foreach (var role in Enum.GetNames(typeof(Roles)))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        #endregion

        #region Seed Users
        private async Task SeedUsersAsync()
        {

            // Step 1: Creates a new instance of User
            // Administrator

            var newUser = new IdentityUser()
            {
                Email = _appSettings.DefaultCredentials.Email,
                UserName = _appSettings.DefaultCredentials.Email,
                EmailConfirmed = true
            };

            try
            {
                var user = await _userManager.FindByEmailAsync(newUser.Email);
                if (user == null)
                {
                    await _userManager.CreateAsync(newUser, _appSettings.DefaultCredentials.Password);
                    await _userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Default Site Admin User.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }

        } 
        #endregion

    }
}
