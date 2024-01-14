namespace LibraryAPI.Infrastructure.Settings
{
    public class AuthSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        // refresh token time to live (in days), inactive tokens are
        // automatically deleted from the database after this time
        public int RefreshTokenTTL { get; set; }
    }
}
