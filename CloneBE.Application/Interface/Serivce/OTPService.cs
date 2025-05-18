using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using static System.Net.WebRequestMethods;

namespace CloneBE.Application.Interface.Serivce
{
    public class OTPService : IOTPService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _expiry = TimeSpan.FromMinutes(5);

        public OTPService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GenerateOTP(string email)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            _cache.Set(email, otp, _expiry);
            return otp;
        }

        public bool ValidateOTP(string email, string otp)
        {
            if (_cache.TryGetValue(email, out string? cachedOtp) && cachedOtp == otp)
            {
                _cache.Remove(email);
                return true;
            }
            return false;
        }
    }
//    Nhược điểm & Cải thiện:

//OTP có thể bị đoán nếu dùng Random(), nên nên dùng RNGCryptoServiceProvider hoặc Guid.NewGuid().
//Không có cơ chế chống brute-force(nếu người dùng nhập sai nhiều lần). Cần thêm giới hạn số lần nhập OTP.

}


