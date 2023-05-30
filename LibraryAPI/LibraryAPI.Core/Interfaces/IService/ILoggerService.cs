using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAPI.Core.Interfaces.IService
{
    public interface ILoggerService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
