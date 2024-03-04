using Microsoft.AspNetCore.Authentication.Cookies;

namespace Octopus.Heroes.WebAPI
{
    public static class Authentication
    {
        public static void AddAuthConfiguration(this IServiceCollection services)
        {
            // Authentication is based in schemas (types):
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "OctopusCoodkie";
                    options.LoginPath = "/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });

            // Adding authorization policies based on claims
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Role", "Admin"));
            });
        }
    }
}
