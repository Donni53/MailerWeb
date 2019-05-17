using System;

namespace MailerWeb.Shared.Models.Exceptions
{
    public class NullUserException : Exception
    {
        public NullUserException() : base("Could not find user. Update required.")
        {
        }
    }
}