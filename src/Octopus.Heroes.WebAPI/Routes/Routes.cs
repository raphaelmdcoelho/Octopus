using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Octopus.Heroes.WebAPI.Models;
using System.Security.Claims;

namespace Octopus.Heroes.WebAPI.Routes
{
    public static class Routes
    {
        public static void AddRoutes(this WebApplication app)
        {

            var summaries = new[]
            { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

            app.MapGet("/weatherforecast", () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();

            app.MapPost("/login", async (HttpContext httpContext, [FromBody] CredentialsRequest credentials) =>
            {
                // Placeholder: Validate user credentials (e.g., against a database)
                var isValidUser = true; // Replace with actual validation logic

                if (isValidUser)
                {
                    // Create claims based on the authenticated user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, credentials?.Username),
                        new Claim("Role", "Admin") // Example claim; adjust based on your logic
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    // TODO: Check on the other APP
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await httpContext.SignInAsync("Cookies", claimsPrincipal);

                    return Results.Ok("User logged in successfully.");
                }

                return Results.BadRequest("Invalid username or password.");
            })
            .WithName("LoginAction")
            .WithOpenApi();

        }

        internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
        {
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
