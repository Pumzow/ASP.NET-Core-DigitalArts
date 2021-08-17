using System.Security.Claims;

namespace DigitalArts.Test.Mocks
{
    public static class ClaimsPrincipalUser
    {
        public static ClaimsPrincipal User
        {

            get
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "ArtistId"),
                                        new Claim(ClaimTypes.Name, "Artist"),
                                        new Claim(ClaimTypes.Email, "Artist@gmail.com")
                                   }, "TestAuthentication"));

                return user;
            }
        }
        public static ClaimsPrincipal Admin
        {

            get
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "ArtistId"),
                                        new Claim(ClaimTypes.Name, "Artist"),
                                        new Claim(ClaimTypes.Email, "Artist@gmail.com"),
                                        new Claim(ClaimTypes.Role, "Administrator")
                                   }, "TestAuthentication"));

                return user;
            }
        }
    }
}
