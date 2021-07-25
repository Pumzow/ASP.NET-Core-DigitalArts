using DigitalArts.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalArts.Data
{
    public class DigitalArtsDbContext : IdentityDbContext<Artist>
    {
        public DbSet<Artist> Artists { get; init; }

        public DbSet<Art> Arts { get; init; }

        public DbSet<ArtistArt> ArtistArts { get; init; }

        public DigitalArtsDbContext(DbContextOptions<DigitalArtsDbContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ArtistArt>()
                .HasKey(x => new
                {
                    x.ArtistId,
                    x.ArtId
                });

            base.OnModelCreating(builder);
        }
    }
}
