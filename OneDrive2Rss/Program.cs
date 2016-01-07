using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace OneDrive2Rss
{
    class Program
    {
        private static readonly string UrlFormatString = "https://api.onedrive.com/v1.0/drives/{0}/root:/public/podcast:/children";
        private static readonly string AudioUrlFormatString = "https://api.onedrive.com/v1.0/drives/{0}/root:/public/podcast/{1}:/content";

        static void Main(string[] args)
        {
            var feedTitle = ConfigurationManager.AppSettings["FeedTitle"];
            var description = ConfigurationManager.AppSettings["FeedDescription"];
            var deviceId = ConfigurationManager.AppSettings["OneDriveDeviceId"];

            var feedLink = string.Format(UrlFormatString, deviceId);

            var directoryPath = args[0];
            var di = new DirectoryInfo(directoryPath);
            var syndicationItems = di.EnumerateFiles("*.mp3").Select(f =>
                new SyndicationItem(f.Name, SyndicationContent.CreateUrlContent(new Uri(string.Format(AudioUrlFormatString, deviceId, f.Name)), "application/mp3"), null, null, f.CreationTime)
);
            var feed = new SyndicationFeed(feedTitle, description, new Uri(feedLink), syndicationItems);

            using (var textWriter = new XmlTextWriter(Path.Combine(directoryPath, "rss.xml"), Encoding.UTF8))
            {
                feed.SaveAsRss20(textWriter);
            }
        }
    }
}
