using AJTracker.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AJTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Define DbSets for your entities, for example:
        public DbSet<MovieAdaptation> MovieAdaptations { get; set; }
    }
}
