using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEVideoExtraction.Model
{
    public class Output
    {                                        
        public string videoTitle             { get; set; }
        public string videoPlaylist          { get; set; }
        public string publishedOn            { get; set; }
        public string duration               { get; set; }
        public string dimension              { get; set; }
        public string definition             { get; set; }
        public string caption                { get; set; }
        public string licensedContent        { get; set; }
        public string projection             { get; set; }
        public string defaultAudioLanguage   { get; set; }
    }
}
