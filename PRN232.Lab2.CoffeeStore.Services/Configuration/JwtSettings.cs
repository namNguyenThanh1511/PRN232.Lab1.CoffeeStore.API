namespace PRN232.Lab2.CoffeeStore.Services.Configuration
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string SecretKey { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; } = 60;
        public int RefreshTokenExpirationDays { get; set; } = 30;

        // Aliases để tương thích với config hiện tại
        public string Issuer => ValidIssuer;
        public string Audience => ValidAudience;
    }
}
