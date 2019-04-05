using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Exceptions
{
    public class NullUserException : Exception
    {
        public NullUserException() : base("Could not find user. Update required.")
        {

        }
    }
}
