﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bira.App.SchoolManager.Infra.Repositories.BaseContext
{
    public class IdentDbContext : IdentityDbContext
    {
        public IdentDbContext(DbContextOptions<IdentDbContext> options) : base(options) { }
    }
}