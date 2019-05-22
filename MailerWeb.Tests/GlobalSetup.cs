using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MailerWeb.Tests
{
    [SetUpFixture]
    public class GlobalSetup
    {
        public static string ImapAddress = "imap.mail.ru";
        public static int ImapPort = 993;
        public static string SmtpAddress = "smtp.mail.ru";
        public static int SmtpPort = 465;
        public static string ImapAddressFail = "imap.mail.ru123";
        public static int ImapPortFali = 993123;
        public static string SmtpAddressFali = "smtp.mail.ru123";
        public static int SmtpPortFali = 465132;
        public static string Jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJMb2dpbiI6ImNhbGxvZmR1dHk5MjZAbWFpbC5ydSIsIklkIjoiMTAwNCIsIktleSI6IkUwa05PUkoxUHBTUVJncE1XM1l5bUJMS21ETWpIY3dTTXFPNTZFWDhqQmc9IiwiSVYiOiJzOHBiV3NwODlkWkxtVDNmZy9wSHJBPT0iLCJuYmYiOjE1NTg0NjUxNjQsImV4cCI6MTU2MTA1NzE2NCwiaWF0IjoxNTU4NDY1MTY0fQ.WcXtVXir90YlcDEySM2zTMID_EEeVTJ32INCez_GcqU";
    }
}
