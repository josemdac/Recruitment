using System;
using System.Collections.Generic;
using System.Text;

namespace RenovaHrEmploymentAPI.Contracts
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarn(string message); 
        void LogDebug(string message);
        void LogError(string message);
    }
}
