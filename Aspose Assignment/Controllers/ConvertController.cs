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
    
    //RESTfull Web API to convert a document to other format
    //Author: Muhammad Faisal
    //Date: 07-AUG-2016
    public class ConvertController : ApiController
    {
       //Implementation of PUT request for Word.Convert
        public ResponseMessage Put([FromUri] string inputfile, [FromUri] string format, [FromUri] string outpath = null) 
        {
            string inputFilePath, outputFilePath;
            SaveFormat formatEnum;
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

                        inputFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/" + App.DocumentsFolder + "/" + inputfile);


                        outpath = @"~/" + App.DocumentsFolder + "/" + (outpath == null ? GetNewFileName(inputfile, format) : outpath);

                        outputFilePath = System.Web.Hosting.HostingEnvironment.MapPath(outpath);
                        
                        var Word = new Document(inputFilePath);

                        //Call our own method to get the respective enumeration against the required format
                        formatEnum = GetFormat(format);

                        //If the required format doesn't exist in our list
                        if (formatEnum == SaveFormat.Unknown)
                        {
                            return new ResponseMessage(null, (int)HttpStatusCode.UnsupportedMediaType, "Format not supported");
                        }


                        Aspose.Words.Saving.SaveOutputParameters oParam = Word.Save(outputFilePath, formatEnum);                       

                        byte [] OutputFile = System.IO.File.ReadAllBytes(outputFilePath);

                        //Derive the url of newly created document
                        var httpRequest = System.Web.HttpContext.Current.Request;

                        return new ResponseMessage(OutputFile, (int)HttpStatusCode.OK, "Operation completed successfully.",
                            (httpRequest.Url.Authority + httpRequest.ApplicationPath + outpath).Replace("/~", ""));
                    }
                    else
                    {
                        return new ResponseMessage(null, (int)HttpStatusCode.Unauthorized, "Invalid API Key and/or App Security ID");
                    }
                }
                else
                {
                    return new ResponseMessage(null, (int)HttpStatusCode.Unauthorized, "API Key and/or App Security ID not provided.");
                }

            }
            catch (Exception ex)
            {
                return new ResponseMessage(null, (int)HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        //Function returns the respective enumeration against the provided format
        private SaveFormat GetFormat(string format)
        {
            SaveFormat FormatEnum;

            try
            {
                switch (format)
                {
                    case "bmp":
                        {
                            FormatEnum = SaveFormat.Bmp;
                            break;
                        }
                    case "doc":
                        {
                            FormatEnum = SaveFormat.Doc;
                            break;
                        }

                    case "docm":
                        {
                            FormatEnum = SaveFormat.Docm;
                            break;
                        }

                    case "docx":
                        {
                            FormatEnum = SaveFormat.Docx;
                            break;
                        }

                    case "dot":
                        {
                            FormatEnum = SaveFormat.Dot;
                            break;
                        }

                    case "dotm":
                        {
                            FormatEnum = SaveFormat.Dotm;
                            break;
                        }

                    case "dotx":
                        {
                            FormatEnum = SaveFormat.Dotx;
                            break;
                        }

                    case "emf":
                        {
                            FormatEnum = SaveFormat.Emf;
                            break;
                        }

                    case "epub":
                        {
                            FormatEnum = SaveFormat.Epub;
                            break;
                        }

                    case "flatopc":
                        {
                            FormatEnum = SaveFormat.FlatOpc;
                            break;
                        }

                    case "html":
                    case "htm":
                        {
                            FormatEnum = SaveFormat.Html;
                            break;
                        }

                    case "jpeg":
                    case "jpg":
                        {
                            FormatEnum = SaveFormat.Jpeg;
                            break;
                        }

                    case "mhtml":
                        {
                            FormatEnum = SaveFormat.Mhtml;
                            break;
                        }

                    case "odt":
                        {
                            FormatEnum = SaveFormat.Odt;
                            break;
                        }

                    case "ott":
                        {
                            FormatEnum = SaveFormat.Ott;
                            break;
                        }

                    case "pdf":
                        {
                            FormatEnum = SaveFormat.Pdf;
                            break;
                        }

                    case "png":
                        {
                            FormatEnum = SaveFormat.Png;
                            break;
                        }

                    case "rtf":
                        {
                            FormatEnum = SaveFormat.Rtf;
                            break;
                        }

                    case "svg":
                        {
                            FormatEnum = SaveFormat.Svg;
                            break;
                        }

                    case "swf":
                        {
                            FormatEnum = SaveFormat.Swf;
                            break;
                        }

                    case "txt":
                    case "text":
                        {
                            FormatEnum = SaveFormat.Text;
                            break;
                        }

                    case "tiff":
                        {
                            FormatEnum = SaveFormat.Tiff;
                            break;
                        }

                    case "wml":
                        {
                            FormatEnum = SaveFormat.WordML;
                            break;
                        }

                    case "xps":
                        {
                            FormatEnum = SaveFormat.Xps;
                            break;
                        }

                    default:
                        {
                            FormatEnum = SaveFormat.Unknown;
                            break;
                        }
                }

                return FormatEnum;
            }

            catch (Exception ex)
            {
                return SaveFormat.Unknown;
            }

                              
        }

        //Function to replace the extension of the filename with the supplied one
        private string GetNewFileName(string FileName, string Extension)
        {
            int IndexOfLastPeriod;

            try
            {
                IndexOfLastPeriod = FileName.LastIndexOf(".");

                return IndexOfLastPeriod > 0 ? FileName.Substring(0, IndexOfLastPeriod) + "." + Extension : FileName + "." + Extension;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

    }
}