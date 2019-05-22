using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MailerWeb.Server.Security;

namespace MailerWeb.Tests.Services
{
    public class Sha256Tests
    {
        [Test]
        public void GetHash()
        {
            var value = Sha256.GetHashString("password");
            Assert.IsNotEmpty(value);
        }
    }
}
