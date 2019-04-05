using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailerWeb.Models.Exceptions
{
    public class InvalidFlagException : Exception
    {
        public InvalidFlagException()
            : base("Flag does not exist")
        {
            base.HResult = 11;
            base.Source = "Use one of this flags: Seen, Unseen, Flagged, Unflagged, Answered, Unanswered";
        }
    }
}
