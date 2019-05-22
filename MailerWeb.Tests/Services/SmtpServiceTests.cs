using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailerWeb.Server.Services;
using NUnit.Framework;

namespace MailerWeb.Tests.Services
{
    public class SmtpServiceTests
    {
        private ISmtpService _smtpService;

        [SetUp]
        public void Setup()
        {
            _smtpService = new SmtpService();
        }

        [Test]
        public async Task SmtpConnect()
        {
            await _smtpService.ConnectAsync(GlobalSetup.SmtpAddress, GlobalSetup.SmtpPort, true);
            Assert.Pass();
        }
    }
}
