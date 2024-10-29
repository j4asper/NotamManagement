using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NotamManagement.Core.Models;

namespace NotamManagement.Api.Helper
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // Add custom claims based on properties in ApplicationUser
            if (!string.IsNullOrEmpty(user.OrganizationId.ToString()))
            {
                identity.AddClaim(new Claim("OrganizationId", user.OrganizationId.ToString()));
            }


            return identity;
        }
    }
}
