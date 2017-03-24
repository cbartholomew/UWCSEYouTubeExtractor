using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEVideoExtraction.Model
{
    public class youtubeRequestObject
    {
        public string id { get; set; }
        public string key { get; set; }
        public string part { get; set; }
        public string pageToken { get; set; }
        public string maxResults { get; set; }
        public string endpoint { get; set; }

        public youtubeRequestObject()
        {
            // get api key on render
            this.key = new Settings.APISettings().API_KEY;
            

        }
    }
}
