﻿using KuaforRandevuBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuaforRandevuBackend.Context
{
    public class ProjectContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public ProjectContext(DbContextOptions<ProjectContext> options):base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
