using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Services
{
    public class AuthenticationServices
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signingManager;

        public AuthenticationServices(UserManager<AppUser> UserManager, RoleManager<AppRole> RoleManager, SignInManager<AppUser> SigningManager)
        {
            userManager = UserManager;
            roleManager = RoleManager;
            signingManager = SigningManager;
        }
    }
}
