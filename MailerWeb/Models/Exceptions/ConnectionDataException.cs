using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Exceptions
{
    public class ConnectionDataException : Exception
    {
        public ConnectionDataException() : base("Could not find connection settings. You must specify them explicitly.")
        {
            HResult = 22;
            base.Source = "Use SignUp method";
        }
    }
}
