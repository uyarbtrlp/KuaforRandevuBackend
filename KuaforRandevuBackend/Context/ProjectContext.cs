using KuaforRandevuBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<ApprovedCustomer> ApprovedCustomers { get; set; }
        public DbSet<LoggedUsers> LoggedUsers { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DbSet<Customer> Customers{ get; set; }
    }
}
