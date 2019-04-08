using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Repository;
using MailerWeb.Security;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto.Tls;

namespace MailerWeb.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _dataRepository;

        public UserService(IUserRepository<User> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public string GetLoginFromToken(string token)
        {
            var claims = Jwt.DecodeToken(token);
            var enumerable = claims as Claim[] ?? claims.ToArray();
            var login = enumerable.FirstOrDefault(e => e.Type == "Login")?.Value;
            return login;
        }


        public async Task<Signature> AddSignatureAsync(string token, Signature signature)
        {
            var login = GetLoginFromToken(token);
            var newSignature = await _dataRepository.AddSignature(login, signature);
            await _dataRepository.SaveAsync();
            return newSignature;
        }

        public async Task<Signature> GetSignatureAsync(string token, int signatureId)
        {
            var login = GetLoginFromToken(token);
            var signature = await _dataRepository.GetSignature(login, signatureId);
            return signature;
        }

        public async Task<IList<Signature>> GetSignaturesAsync(string token)
        {
            var login = GetLoginFromToken(token);
            var signatures = await _dataRepository.GetSignatures(login);
            return signatures;
        }

        public async Task DeleteSignatureAsync(string token, int signatureId)
        {
            var login = GetLoginFromToken(token);
            await _dataRepository.DeleteSignature(login, signatureId);
            await _dataRepository.SaveAsync();
        }

        public async Task<Signature> EditSignatureAsync(string token, int signatureId, Signature newSignature)
        {
            var login = GetLoginFromToken(token);
            var signature = await _dataRepository.EditSignature(login, signatureId, newSignature);
            await _dataRepository.SaveAsync();
            return signature;
        }

        public async Task EditNames(string token, string name, string nickname)
        {
            var login = GetLoginFromToken(token);
            await _dataRepository.EditNames(login, name, nickname);
            await _dataRepository.SaveAsync();
        }
    }
}
