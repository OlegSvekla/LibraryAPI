namespace LibraryAPI.Domain.Constants
{
    public static class Constants
    {
        public static string LogoFileType = "jpg";
        public static string LogoContentType = "image/jpeg";

        public static class Claims
        {
            public static readonly string UserId = "user_id";
            public static readonly string TenantId = "tenant_id";
            public static readonly string Subject = "sub";
            public static readonly string UserName = "name";
            public static readonly string FirstName = "given_name";
            public static readonly string AccessToken = "access_token";
            public static readonly string RefreshToken = "refresh_token";
            public static readonly string ExpiresAt = "expires_at";
            public static readonly string Email = "email";
            public static readonly string PhoneNumber = "phone_number";
        }

        public static readonly int ViewingAddCooldownPeriod = 10;
    }
}
