using System;
using System.Collections.Generic;
using System.Text;

namespace Distance.BLL.Model
{
  public  class University : ModelBase
    {
        public String Id { get; set; }
        public String location { get; set; }
        public String arabicName { get; set; }
        public String url { get; set; }
        public String turkeyName { get; set; }
        public string email { get; set; }
        public String universityType { get; set; }
        public String discription { get; set; }
        public String localNumber { get; set; }
        public String worldNumber { get; set; }
        public String dateOfEstablishment { get; set; }
        public String webSite { get; set; }
        public String webSiteNews { get; set; }
        public String faceBookWebSite { get; set; }
        public String twitterWebSite { get; set; }
        public String youToubChannel { get; set; }
        public String universityReserch { get; set; }
        public String universitywebSiteNews { get; set; }
       // public String webSiteLanguage { get; set; }
       // public String telephoneNumbers { get; set; }
    }
}
