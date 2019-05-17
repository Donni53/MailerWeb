using System;

namespace MailerWeb.Shared.Models.Exceptions
{
    public class ConnectionDataException : Exception
    {
        public ConnectionDataException() : base("Could not find connection settings. You must specify them explicitly.")
        {
            HResult = 22;
        }
    }
}