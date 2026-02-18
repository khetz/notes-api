namespace Application.Outputs
{
    public class RefreshTokenResponse
    {
        public required string Token { get; set; }
        public required string Username { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
