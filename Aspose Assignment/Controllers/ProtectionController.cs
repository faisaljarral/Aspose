using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aspose.Words;
using System.Web.Configuration;
using Common;
using Aspose_Assignment.Models;

namespace Aspose_Assignment.Controllers
{
    //RESTfull Web API to protect a document from unwanted changes
    //Author: Muhammad Faisal
    //Date: 07-AUG-2016
    public class ProtectionController : ApiController
    {
        //Implementation of GET Word/{document}/Protection to return the current protection applied to the document
        public ProtectionResponse Get(string DocName, [FromUri] string Folder = null)
        {
            
            string InputFileVirualPath, InputFilePath;
            string ApiKey, AppSid;

            try
            {

                Header RequestHeader = new Header(Request.Headers);

                //If header contains API Key and App Security ID then proceed otherwise return Unauthorized response message
                if (RequestHeader.GetHeaderValue("API_KEY") != null && RequestHeader.GetHeaderValue("APP_SID") != null)
                {

                    ApiKey = RequestHeader.GetHeaderValue("API_KEY").ToString();
                    AppSid = RequestHeader.GetHeaderValue("APP_SID").ToString();

                    //Check if API Key and App Key Security ID are correct
                    if (ApiKey == WebConfigurationManager.AppSettings.Get("API_KEY") &&
                        AppSid == WebConfigurationManager.AppSettings.Get("APP_SID"))
                    {
                        InputFileVirualPath = "~/" + App.DocumentsFolder + "/" + (Folder != null ? Folder : "") + "/" + DocName;
                        InputFilePath = System.Web.Hosting.HostingEnvironment.MapPath(InputFileVirualPath);

                        var Doc = new Document(InputFilePath);

                        //Derive the url of newly document
                        var httpRequest = System.Web.HttpContext.Current.Request;

                        return new ProtectionResponse((int)HttpStatusCode.OK, Enum.GetName(HttpStatusCode.OK.GetType(), HttpStatusCode.OK),
                            Enum.GetName(Doc.ProtectionType.GetType(), Doc.ProtectionType),
                           (httpRequest.Url.Authority + httpRequest.ApplicationPath + App.DocumentsFolder + "/" + (Folder != null ? Folder : "") + DocName).Replace("/~", ""));
                        
                    }
                    else
                    {
                        return new ProtectionResponse((int)HttpStatusCode.Unauthorized, "Invalid API Key and/or App Security ID", "", "");
                    }
                }
                else
                {
                    return new ProtectionResponse((int)HttpStatusCode.Unauthorized, "API Key and/or App Security ID not provided.", "", "");
                }
            }
            catch (Exception ex)
            {
                return new ProtectionResponse((int)HttpStatusCode.InternalServerError, ex.Message, "", "");
            }
        }

        //Implementation of POST Word/{document}/Protection to apply the chosen proection to the document
        public ProtectionResponse Post(string DocName, [FromBody] ProtectionInfo Protection, [FromUri] string Folder = null)
        {
            string inputFilePath;
            string ApiKey, AppSid;
            string ProtectionType, Password, NewPassword;

            try
            {

                ProtectionType = Protection.ProtectionType;
                Password = Protection.Password;
                NewPassword = Protection.NewPassword;

                Aspose.Words.ProtectionType NewProtectionType;

                Header RequestHeader = new Header(Request.Headers);

                //If header contains API Key and App Security ID then proceed otherwise return Unauthorized response message
                if (RequestHeader.GetHeaderValue("API_KEY") != null && RequestHeader.GetHeaderValue("APP_SID") != null)
                {

                    ApiKey = RequestHeader.GetHeaderValue("API_KEY").ToString();
                    AppSid = RequestHeader.GetHeaderValue("APP_SID").ToString();

                    //Check if API Key and App Key Security ID are correct
                    if (ApiKey == WebConfigurationManager.AppSettings.Get("API_KEY") &&
                        AppSid == WebConfigurationManager.AppSettings.Get("APP_SID"))
                    {

                        inputFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + App.DocumentsFolder + "/" + (Folder != null ? Folder : "") + "/" + DocName);

                        var Doc = new Document(inputFilePath);
                        NewProtectionType = GetProtectionType(ProtectionType);
                        
                        if (NewPassword != null && NewPassword != Password)
                            Doc.Protect(NewProtectionType, NewPassword);
                        else
                            Doc.Protect(NewProtectionType, Password);

                        Doc.Save(inputFilePath);
                        
                        //Derive the url of newly document
                        var httpRequest = System.Web.HttpContext.Current.Request;

                        return new ProtectionResponse((int)HttpStatusCode.OK, Enum.GetName(HttpStatusCode.OK.GetType(), HttpStatusCode.OK),
                           Enum.GetName(Doc.ProtectionType.GetType(), Doc.ProtectionType),
                           (httpRequest.Url.Authority + httpRequest.ApplicationPath + App.DocumentsFolder + "/" + (Folder != null ? Folder : "") + DocName).Replace("/~", "")); ;


                    }
                    else
                    {
                        return new ProtectionResponse((int)HttpStatusCode.Unauthorized, "Invalid API Key and/or App Security ID", "", "");
                    }
                }
                else
                {
                    return new ProtectionResponse((int)HttpStatusCode.Unauthorized, "API Key and/or App Security ID not provided.", "", "");
                }


            }
            catch (Exception ex)
            {
                return new ProtectionResponse((int)HttpStatusCode.InternalServerError, ex.Message, "", "");
            }
        }

        //Returns the protection type enumeration against the provided protection type string/text
        private Aspose.Words.ProtectionType GetProtectionType(string ProtectionType)
        {
            Aspose.Words.ProtectionType NewProtectionType;

            try
            {
                 switch(ProtectionType)
                    {
                        case "AllowOnlyComments":
                            {
                                NewProtectionType = Aspose.Words.ProtectionType.AllowOnlyComments;
                                break;
                            }
                        case "AllowOnlyFormFields":
                            {
                                NewProtectionType = Aspose.Words.ProtectionType.AllowOnlyFormFields;
                                break;
                            }
                        case "AllowOnlyRevisions":
                            {
                                NewProtectionType = Aspose.Words.ProtectionType.AllowOnlyRevisions;
                                break;
                            }
                        case "ReadOnly":
                            {
                                NewProtectionType = Aspose.Words.ProtectionType.ReadOnly;
                                break;
                            }
                        case "NoProtection":
                            {
                                NewProtectionType = Aspose.Words.ProtectionType.NoProtection;
                                break;
                            }
                        default:
                            {
                                NewProtectionType = Aspose.Words.ProtectionType.NoProtection;
                                break;
                            }

                 }

                return NewProtectionType;
            }
            catch(Exception ex)
            {
                return 0;
            }

            
        }
    }
}
