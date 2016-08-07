/**
 *
 * @author Imran Anwar
 * This class is added for the response to be separated in Json and InputStream
 */
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Aspose_Assignment.Models
{ 
    public class ResponseMessage
    {
        public ResponseMessage() { }
        public ResponseMessage(byte[] ResponseStream, int Code = 200, String Status = "OK", string DocumentLink = "")
        {
            this.Code = Code;
            this.Status = Status;
            this.ResponseStream = ResponseStream;
            this.DocumentLink = DocumentLink;
        }

        public int Code { get; set; }
        public string Status { get; set; }
        public string DocumentLink { get; set; }
        public string Message { get; set; }
        public byte[] ResponseStream { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ResponseMessage {\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}