using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Repository;
using MailerWeb.Security;

namespace MailerWeb.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository<User> _dataRepository;

        public UserService(IUserRepository<User> dataRepository, IAuthService authService)
        {
            _dataRepository = dataRepository;
            _authService = authService;
        }


        public async Task<Signature> AddSignatureAsync(string token, Signature signature)
        {
            var refreshData = await _authService.GetRefreshData(token);
            if (!refreshData.User.Password.Equals(Sha256.GetHashString(refreshData.Password)))
                throw new ArgumentException("Wrong login or password");
            var newSignature = await _dataRepository.AddSignatureAsync(refreshData.User.Login, signature);
            await _dataRepository.SaveAsync();
            return newSignature;
        }

        public async Task<Signature> GetSignatureAsync(string token, int signatureId)
        {
            var refreshData = await _authService.GetRefreshData(token);
            if (!refreshData.User.Password.Equals(Sha256.GetHashString(refreshData.Password)))
                throw new ArgumentException("Wrong login or password");
            var signature = await _dataRepository.GetSignatureAsync(refreshData.User.Login, signatureId);
            return signature;
        }

        public async Task<IList<Signature>> GetSignaturesAsync(string token)
        {
            var refreshData = await _authService.GetRefreshData(token);
            if (!refreshData.User.Password.Equals(Sha256.GetHashString(refreshData.Password)))
                throw new ArgumentException("Wrong login or password");
            var signatures = await _dataRepository.GetSignaturesAsync(refreshData.User.Login);
            return signatures;
        }

        public async Task DeleteSignatureAsync(string token, int signatureId)
        {
            var refreshData = await _authService.GetRefreshData(token);
            if (!refreshData.User.Password.Equals(Sha256.GetHashString(refreshData.Password)))
                throw new ArgumentException("Wrong login or password");
            await _dataRepository.DeleteSignatureAsync(refreshData.User.Login, signatureId);
            await _dataRepository.SaveAsync();
        }

        public async Task<Signature> EditSignatureAsync(string token, int signatureId, Signature newSignature)
        {
            var refreshData = await _authService.GetRefreshData(token);
            if (!refreshData.User.Password.Equals(Sha256.GetHashString(refreshData.Password)))
                throw new ArgumentException("Wrong login or password");
            var signature = await _dataRepository.EditSignatureAsync(refreshData.User.Login, signatureId, newSignature);
            await _dataRepository.SaveAsync();
            return signature;
        }

        public async Task EditName(string token, string name)
        {
            var refreshData = await _authService.GetRefreshData(token);
            if (!refreshData.User.Password.Equals(Sha256.GetHashString(refreshData.Password)))
                throw new ArgumentException("Wrong login or password");
            await _dataRepository.EditNameAsync(refreshData.User.Login, name);
            await _dataRepository.SaveAsync();
        }
    }
}