﻿using LibraryAPI.Domain.Entities.Auth;

namespace LibraryAPI.Domain.Interfaces.IService
{
    public interface IEmailService
    {
        void Send(User account, string subject, string? from = null);
    }
}