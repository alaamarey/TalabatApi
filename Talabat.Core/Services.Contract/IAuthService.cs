using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Services.Contract
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(ApplicationUser applicationUser , UserManager<ApplicationUser> userManager);

    }
}
