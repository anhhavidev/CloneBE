using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneBE.Application.DTO.Request
{
    public class VerifyOTPModel
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }


}
