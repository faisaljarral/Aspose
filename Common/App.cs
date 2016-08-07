using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Common
{
    public static class App
    {
        public static string DocumentsFolder
        {
            get
            {
                return WebConfigurationManager.AppSettings.Get("DOCUMENTS_FOLDER");
            }
            
        }
            
    }

}
