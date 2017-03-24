using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSEVideoExtraction.Model
{
    public class ExtractLink
    {
        public enum REQUEST_TYPES
        {
            GET_CHANNEL_UPLOAD_ID,
            GET_PLAYLIST_ITEMS,
            GET_VIDEO_DETAILS,
            UNKNOWN
        }


        public static REQUEST_TYPES GetRequestType(string requestType)
        {
            REQUEST_TYPES request = REQUEST_TYPES.UNKNOWN;

            switch (requestType)
            {
                case "channel":
                    request = REQUEST_TYPES.GET_CHANNEL_UPLOAD_ID;
                    break;
                case "playlist":
                    request = REQUEST_TYPES.GET_PLAYLIST_ITEMS;
                    break;
                case "video":
                    request = REQUEST_TYPES.GET_VIDEO_DETAILS;
                    break;
                default:
                    break;
            }

            return request;
        }

        private static void GetEndPoint(REQUEST_TYPES requestType,
                        youtubeRequestObject request)
        {
            string endpoint = new Settings.APISettings().API_BASE_URL;

            switch (requestType)
            {
                case REQUEST_TYPES.GET_CHANNEL_UPLOAD_ID:
                    endpoint = String.Concat(
                        endpoint, new Settings.APISettings().GET_CHANNEL_UPLOAD_ID
                        );

                    endpoint = endpoint
                        .Replace("#CHANNEL_ID#", 
                        request.id)
                        .Replace("#API_KEY#", 
                        request.key);

                    break;
                case REQUEST_TYPES.GET_PLAYLIST_ITEMS:
                    endpoint = String.Concat(
                        endpoint, new Settings.APISettings().GET_PLAYLIST_ITEMS
                        );
                    
                    endpoint = endpoint
                        .Replace("#UPLOAD_ID#", 
                        request.id)
                        .Replace("#API_KEY#", 
                        request.key)
                        .Replace("#PAGE_TOKEN#", 
                        request.pageToken);                    

                    break;
                case REQUEST_TYPES.GET_VIDEO_DETAILS:

                    endpoint = String.Concat(
                        endpoint, new Settings.APISettings().GET_VIDEO_DETAILS
                        );

                    endpoint = endpoint
                        .Replace("#VIDEO_ID#",
                        request.id)
                        .Replace("#API_KEY#",
                        request.key);
                    break;
                default:
                    break;
            }

            // set class object to correct endpoint
            request.endpoint = endpoint;
        }
        
        public static T MakeAPIRequest<T>(T obj, 
            REQUEST_TYPES requestType,
            youtubeRequestObject request)
        {
            T jsonResponse = Activator.CreateInstance<T>();

            // create the request
            WebRequest myWebRequest = WebRequest.Create(request.endpoint);

            // call warcraft logs
            WebResponse myWebResponse = myWebRequest.GetResponse();

            // stream the json in
            Stream jsonStream = myWebResponse.GetResponseStream();

            // populate stream reader
            StreamReader reader = new StreamReader(jsonStream);

            // bring into string format
            string jsonData = reader.ReadToEnd();

            // deserialize
            jsonResponse = Utility.myJSONHelper.Deserialize<T>(jsonData);

            return jsonResponse;
        }
    }
}
