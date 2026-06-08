using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AbitElit.DataAccess
{
    public class AbitElitDbContext(DbContextOptions<AbitElitDbContext> options) : DbContext (options)
    {
        public DbSet<Applicant> Applicants {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>().HasKey(x => x.Id);
            // Встановлюю граничні значення згідно ТЗ:
            modelBuilder.Entity<Applicant>().Property(x => x.FirstName).IsRequired().HasMaxLength(50); 
            modelBuilder.Entity<Applicant>().Property(x => x.LastName).HasMaxLength(50); 
            modelBuilder.Entity<Applicant>().Property(x => x.ExamScore).HasPrecision(5, 1);
            base.OnModelCreating(modelBuilder);
        }
    }
}