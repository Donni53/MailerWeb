using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailerWeb.Server.Services;
using NUnit.Framework;

namespace MailerWeb.Tests.Services
{
    public class ImapServiceTests
    {
        private IImapService _imapService;

        [SetUp]
        public void Setup()
        {
            _imapService = new ImapService();
        }

        [Test]
        public async Task ImapConnect()
        {
            await _imapService.ConnectAsync(GlobalSetup.ImapAddress, GlobalSetup.ImapPort, true);
            Assert.Pass();
        }




        [TearDown]
        public void Teardown()
        {
            _imapService = null;
        }

    }
}
