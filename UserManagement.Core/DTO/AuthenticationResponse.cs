namespace UserManagement.Core.DTO
{
    public class AuthenticationResponse
    {
        public required string Username { get; set; }

        public required string Token { get; set; }

        public DateTime Expiration { get; set; }

    }
}
