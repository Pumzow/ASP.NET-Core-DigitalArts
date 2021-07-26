using DigitalArts.Data;
using DigitalArts.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using static DigitalArts.WebConstants;

namespace DigitalArts.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);
            SeedAdministrator(services);

            return app;
        }
        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<DigitalArtsDbContext>();

            data.Database.Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<Artist>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminFirstName = "Ivan";
                    const string adminLastName = "Petrov";
                    const string adminEmail = "pumzow@gmail.com";
                    const string adminPassword = "ladep123654";
                    const string adminUsername = "Pumzow";
                    const string adminProfilePicture = "https://i.pinimg.com/280x280_RS/00/44/0b/00440b2a52c8fce1d6e4581c56f0d46a.jpg";

                    var artist = new Artist
                    {
                        Id = "Admin",
                        FirstName = adminFirstName,
                        LastName = adminLastName,
                        UserName = adminUsername,
                        ArtistUsername = adminUsername,
                        Email = adminEmail,
                        ProfileImage = adminProfilePicture
                    };

                    await userManager.CreateAsync(artist, adminPassword);

                    await userManager.AddToRoleAsync(artist, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
