using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class IdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Youssef Ahmed",
                    Email = "youssef99119@gmail.com",
                    UserName = "youssef.ahmed",
                    PhoneNumber = "01126756907"
                };

                await _userManager.CreateAsync(user, "Pa$$W0rd");
            }


        }
    }
}
