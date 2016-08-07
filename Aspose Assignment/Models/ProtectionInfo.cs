using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aspose_Assignment.Models
{
    public class ProtectionInfo
    {
        public string ProtectionType { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public ProtectionInfo()
        {
            ProtectionType = "";
            Password = "";
            NewPassword = "";
        }

        public ProtectionInfo(string ProtectionType, string Password, string NewPassword)
        {
            this.ProtectionType = ProtectionType;
            this.Password = Password;
            this.NewPassword = NewPassword;
        }
    }
}