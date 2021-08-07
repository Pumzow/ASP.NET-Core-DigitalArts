using System;
using System.Linq;
using DigitalArts.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalArts.Data
{
    public class DigitalArtsDbContext : IdentityDbContext<Artist>
    {
        public DbSet<Artist> Artists { get; init; }

        public DbSet<Art> Arts { get; init; }
        public DbSet<Likes> Likes { get; set; }

        public DigitalArtsDbContext(DbContextOptions<DigitalArtsDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Likes>()
                .HasKey(aa => new { aa.ArtId, aa.ArtistId });
            builder.Entity<Likes>()
                .HasOne(aa => aa.Art)
                .WithMany(a => a.Likes)
                .HasForeignKey(a => a.ArtId);
            builder.Entity<Likes>()
                .HasOne(aa => aa.Artist)
                .WithMany(a => a.Likes)
                .HasForeignKey(aa => aa.ArtistId);

            base.OnModelCreating(builder);
        }
    }
}
