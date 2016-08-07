using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace Common
{
    public class Header
    {
        public HttpRequestHeaders RequestHeader { get; set; }

        public Header(HttpRequestHeaders ReqHeader)
        {
            RequestHeader = ReqHeader;
        }

        public object GetHeaderValue(string Key)
        {
            try
            {
                //Check if key exists in the header otherwise return a null value
                if (RequestHeader.Contains(Key))
                {
                    IEnumerable<string> ColValues = RequestHeader.GetValues(Key);
                    IEnumerator<string> EnValues = ColValues.GetEnumerator();
                    EnValues.MoveNext();
                    var Value = EnValues.Current;
                    return Value;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //Throw the xeception back to the calling class
                throw ex;
            }
        }

       
    }
}
