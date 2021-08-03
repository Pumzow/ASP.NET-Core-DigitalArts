using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DigitalArts.Areas.Identity.IdentityHostingStartup))]
namespace DigitalArts.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}