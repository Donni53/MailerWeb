using System;

namespace MailerWeb.Shared.Models.Exceptions
{
    public class InvalidFlagException : Exception
    {
        public InvalidFlagException()
            : base("Flag does not exist")
        {
            HResult = 11;
            base.Source = "Use one of this flags: Seen, Unseen, Flagged, Unflagged, Answered, Unanswered";
        }
    }
}