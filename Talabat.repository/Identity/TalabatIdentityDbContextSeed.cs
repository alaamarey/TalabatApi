using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using Talabat.Core.Entities.Identity;

namespace Talabat.repository.Identity
{
    public static class TalabatIdentityDbContextSeed
    {
        public async static Task SeedAsync(UserManager<ApplicationUser> userManager  )
        {
            if (userManager.Users.Count() == 0)
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Alaa",
                    Email = "alaamarey616@gmail.com",
                    PhoneNumber = "01068741024",
                    UserName = "Alaa.Marey"
                };
                var result = await userManager.CreateAsync(user, "Alaa123##");
                if (!result.Succeeded) 
                {
                    foreach (var error in result.Errors) 
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
    }

}
