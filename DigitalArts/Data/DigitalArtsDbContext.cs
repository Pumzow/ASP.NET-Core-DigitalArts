using DigitalArts.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalArts.Data
{
    public class DigitalArtsDbContext : IdentityDbContext
    {
        public DbSet<Artist> Artist { get; init; }

        public DbSet<Art> Arts { get; init; }

        public DbSet<ArtistArt> ArtistArts { get; init; }
        public DigitalArtsDbContext(DbContextOptions<DigitalArtsDbContext> options)
            : base(options)
        {
        }
    }
}
