using System;
using System.Collections.Generic;
using System.Text;
using MailerWeb.Server.Security;
using NUnit.Framework;

namespace MailerWeb.Tests.Services
{
    public class JwtTests
    {
        [Test]
        public void JwtGeneration()
        {
            var value = Jwt.GenerateToken("login", "test", "test", 0);
            Assert.IsNotNull(value);
        }


        [Test]
        public void JwtDecodeFail()
        {
            var value = Assert.Throws<ArgumentException>(() => Jwt.DecodeToken("fail"));
            Assert.IsNotNull(value);
        }

        [Test]
        public void JwtDecode()
        {
            var value = Jwt.DecodeToken(GlobalSetup.Jwt);
            Assert.IsNotNull(value);
        }
    }
}
