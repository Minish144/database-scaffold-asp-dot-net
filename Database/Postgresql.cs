using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using database_scaffold_asp_dot_net.Models;

namespace database_scaffold_asp_dot_net.Database
{
    public class Postgresql : DbContext
    {
        public DbSet<BusinessTrip> BusinessTrips { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PersonalData> PersonalData { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin");
    }
}