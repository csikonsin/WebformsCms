using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebformsCms.Src
{
    public class WebSettings
    {

        private static WebSettings instance;
        public Domain.Websites Settings = null;

        private WebSettings() {
            using (var session = new Data.DataSession())
            {
                var websiteRepo = new Data.WebsitesRepository(session.UnitOfWork);
                Settings = websiteRepo.GetSingle(WebSettings.WebsiteId);
            }
        }

        public static WebSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WebSettings();
                }
                return instance;
            }
        }



        public static int WebsiteId
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WebsiteId"]);
            }
        }



    }
}