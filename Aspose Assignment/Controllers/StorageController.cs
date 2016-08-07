using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Aspose_Assignment.Controllers
{
    //This API is meant to deal with saving the documents on the server
    //Author: Muhammad Faisal
    //Date: 07-AUG-2016
    public class StorageController : ApiController
    {
        // Implementation of POST Word/Storage
        [HttpHead()]
        [AcceptVerbs("POST")]
        public HttpResponseMessage Post()
        {            
           HttpResponseMessage result = null;
           var httpRequest = HttpContext.Current.Request;
           if (httpRequest.Files.Count > 0)
           {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + Common.App.DocumentsFolder + "/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
 
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
             return result;
        }

        
    }
}
