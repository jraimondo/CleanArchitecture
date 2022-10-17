using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Persistence
{
    public class StreamerDbContext : DbContext
    {


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CleanArchitectureDb")
        //                  .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name})
        //                  .EnableSensitiveDataLogging();
        //}

        public StreamerDbContext(DbContextOptions<StreamerDbContext> options)
            : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "SYSTEM";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "SYSTEM";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }




    }
}
