﻿namespace LibraryAPI.Infrastructure.Settings
{
    public class AuthSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public int RefreshTokenTTL { get; set; }
    }
}