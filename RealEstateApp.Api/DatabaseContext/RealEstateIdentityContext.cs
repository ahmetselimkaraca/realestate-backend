﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RealEstateApp.Api.DatabaseContext
{
    public class RealEstateIdentityContext : IdentityDbContext<IdentityUser>
    {
        public RealEstateIdentityContext(DbContextOptions<RealEstateIdentityContext> options) : base(options)
        {
        }
    }
}
