namespace Octopus.Heroes.WebAPI.Models
{
    public sealed record CredentialsRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
