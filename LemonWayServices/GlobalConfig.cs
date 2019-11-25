using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LemonWayServices
{
    public class GlobalConfig
    {
        #region Url_WebService
        /// <summary>
        /// URL du web service : A modifier ici
        /// </summary>
        private static string _url_WebService = "http://localhost:53692/LemonWayMetiers.asmx";
        public static string Url_WebService
        {
            get { return _url_WebService; }
            set
            {
                _url_WebService = value;
            }
        }
        #endregion
    }
}