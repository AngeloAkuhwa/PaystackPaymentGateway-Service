using EcommerceApi_dotNetFramework.Models;
using EcommerceApi_dotNetFramework.Models.PayGateTransactionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceApi_dotNetFramework.Contracts.IServices
{
    public interface IPaymentService
    {
        string ToUrlEncodedString(Dictionary<string, string> request);
        Dictionary<string, string> ToDictionary(string response);
        bool AddTransaction(Dictionary<string, string> request, string payRequestId);
        bool UpdateTransaction(Dictionary<string, string> request, string PayrequestId);
        Transaction GetTransaction(string payRequestId);
        string GetMd5Hash(Dictionary<string, string> data, string encryptionKey);
        bool VerifyMd5Hash(Dictionary<string, string> data, string encryptionKey, string hash);
        AppUser GetAuthenticatedUser();
        //void UpdateTransactionStatus(Transaction transaction);
    }
}