using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat_.APIS.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userEmail = user.FindFirstValue(ClaimTypes.Email);
            var User = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == userEmail);

            return User;
        }
    }
}
