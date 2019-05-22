using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MailerWeb.Server.Security;
using MailerWeb.Shared.Models;
using NUnit.Framework;

namespace MailerWeb.Tests.Services
{
    public class RijndaelTests
    {
        [Test]
        public void Encrypt()
        {
            var value = RijndaelManager.EncryptStringToBase64String("value");
            Assert.IsNotNull(value);
        }

        [Test]
        public void Decrypt()
        {
            var value = RijndaelManager.EncryptStringToBase64String("value");
            var rvalue = RijndaelManager.DecryptStringFromBytes(Convert.FromBase64String(value.Data),
                Convert.FromBase64String(value.Key), Convert.FromBase64String(value.Iv));
            Assert.AreEqual("value", rvalue);
        }
    }
}
