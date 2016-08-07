using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aspose_Assignment.Models
{
    public class ProtectionResponse
    {
        public int Code { get; set; } 
        public string  Status { get; set; }
        public string  ProtectionType { get; set; }
        public string  DocumentLink { get; set; }

        public ProtectionResponse(int Code, string Status, string ProtectionType, string DocumentLink)
        {
            this.Code = Code;
            this.Status = Status;
            this.ProtectionType = ProtectionType;
            this.DocumentLink = DocumentLink;
        }
    }
}