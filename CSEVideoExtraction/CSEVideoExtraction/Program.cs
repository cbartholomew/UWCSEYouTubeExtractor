using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSEVideoExtraction.Model;
using CSEVideoExtraction.Settings;
using CSEVideoExtraction.Utility;
using System.IO;

namespace CSEVideoExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Output> videoOutputList = new List<Output>();

            youtubeRequestObject channelRequest 
                = new youtubeRequestObject();
            youtubeRequestObject playlistItemsRequest
                = new youtubeRequestObject();
     
            // get upload id
            channelRequest.id = new Settings.YouTubeConfig().YouTubeChannel;

            // get the channel request ype
            ExtractLink.REQUEST_TYPES channelRequestType 
                = ExtractLink.GetRequestType("channel");
            ExtractLink.REQUEST_TYPES playlistRequestType 
                = ExtractLink.GetRequestType("playlist_items");
            ExtractLink.REQUEST_TYPES playlistNameRequestType
                = ExtractLink.GetRequestType("playlist");
            ExtractLink.REQUEST_TYPES videoRequestType 
                = ExtractLink.GetRequestType("video");

            // create endpoint for channel request
            ExtractLink.GetEndPoint(channelRequestType, channelRequest);

            // get back video request end point
            youtubeChannelListResponseRoot.Rootobject channelResponse
                = ExtractLink.MakeAPIRequest<youtubeChannelListResponseRoot.Rootobject>(
                channelRequestType, 
                channelRequest);

            // get channel request 
            playlistItemsRequest.id = channelResponse
                .items[0]
                .contentDetails
                .relatedPlaylists
                .uploads;
 
            while (playlistItemsRequest.pageToken != "")
            {
                // get videos
                ExtractLink.GetEndPoint(playlistRequestType, playlistItemsRequest);

                // get back video request end point
                youtubePlaylistItemListResponseRoot.Rootobject playlistResponse
                    = ExtractLink.MakeAPIRequest<youtubePlaylistItemListResponseRoot.Rootobject>(
                    playlistRequestType,
                    playlistItemsRequest);

                // iterate through each video on the play list to gather information about it.
                foreach (youtubePlaylistItemListResponseRoot.Item item
                    in playlistResponse.items)
                {
                    Output videoOutput = new Output();

                    youtubeRequestObject videoItemsRequest
                        = new youtubeRequestObject();

                    // assign video id 
                    videoItemsRequest.id = item.contentDetails.videoId;

                    // get video meta data
                    ExtractLink.GetEndPoint(videoRequestType, videoItemsRequest);

                    // get back video request end point
                    youtubeVideoListResponseRoot.Rootobject videoListResponse 
                        = ExtractLink.MakeAPIRequest<youtubeVideoListResponseRoot.Rootobject>(
                        videoRequestType,
                        videoItemsRequest);

                    videoOutput.videoTitle            = scrub(item.snippet.title);
                    videoOutput.publishedOn           = item.snippet.publishedAt;
                    videoOutput.videoPlaylist          = scrub(videoListResponse.items[0].snippet.localized.title);
                    videoOutput.defaultAudioLanguage  = videoListResponse.items[0].snippet.defaultAudioLanguage;
                    videoOutput.duration              = videoListResponse.items[0].contentDetails.duration;
                    videoOutput.dimension             = videoListResponse.items[0].contentDetails.dimension;
                    videoOutput.definition            = videoListResponse.items[0].contentDetails.definition;
                    videoOutput.caption               = videoListResponse.items[0].contentDetails.caption;
                    videoOutput.projection            = videoListResponse.items[0].contentDetails.projection;
                     
                    // add
                    videoOutputList.Add(videoOutput);
                }

                playlistItemsRequest.pageToken = (playlistResponse.nextPageToken != null) 
                    ?  playlistResponse.nextPageToken : "";
            }

             
            // set csv output
            string csvOutput = new YouTubeConfig().FILE_EXPORT_PATH;

            try
            {
                // on new run, remove output file
                if (File.Exists(csvOutput))
                {
                    File.Copy(csvOutput, csvOutput.Replace(".csv", "_" + DateTime.Now.ToFileTime() + ".csv"));
                    File.Delete(csvOutput);
                }
            }
            catch (Exception)
            {
                File.Copy(csvOutput, csvOutput.Replace(".csv", "_" + DateTime.Now.ToFileTime() + ".csv"));
                File.Delete(csvOutput);
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.  
            using (StreamWriter sw = File.AppendText(csvOutput))
            {
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                "VIDEO_TITLE",
                "VIDEO_PLAYLIST",
                "PUBLISHED_ON",
                "IS_CLOSED_CAPTION",
                "DURATION",
                "DIMENSION",
                "DEFINITION",
                "LICENSED_CONTENT",
                "PROJECTION",
                "DEFAULT_AUDTIO_LANGUAGE"
                );

                foreach (Output outputElement in videoOutputList)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                             outputElement.videoTitle,
                             outputElement.videoPlaylist,
                             outputElement.publishedOn,
                             outputElement.caption,
                             outputElement.duration,
                             outputElement.dimension,
                             outputElement.definition,                            
                             outputElement.licensedContent,
                             outputElement.projection,
                             outputElement.defaultAudioLanguage
                             );
                }
            }

        }

        public static string scrub(string data)
        {
            return (data.Contains(",")) ? data.Replace(",", " ") : data;
        }
    }
}
