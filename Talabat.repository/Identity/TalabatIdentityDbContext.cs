using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.Identity;

namespace Talabat.repository.Identity
{
    public class TalabatIdentityDbContext :IdentityDbContext<ApplicationUser>
    {

        public TalabatIdentityDbContext(DbContextOptions<TalabatIdentityDbContext> options):base(options)
        {
        }

    }
}
