using System.Collections.Generic;
using System.Threading.Tasks;
using MailerWeb.Models;

namespace MailerWeb.Services
{
    public interface IUserService
    {
        Task<Signature> AddSignatureAsync(string token, Signature signature);
        Task<Signature> GetSignatureAsync(string token, int signatureId);
        Task<IList<Signature>> GetSignaturesAsync(string token);
        Task DeleteSignatureAsync(string token, int signatureId);
        Task<Signature> EditSignatureAsync(string token, int signatureId, Signature newSignature);
        Task EditName(string token, string name);
    }
}