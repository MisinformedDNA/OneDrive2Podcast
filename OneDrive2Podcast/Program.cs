using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OneDrive2Podcast
{
    class Program
    {
        private const string OneDriveBaseUrl = "https://api.onedrive.com/v1.0";
        private const string RssXml = "rss.xml";

        static void Main(string[] args)
        {
            Task.Run(() => MainAsync(args)).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            var feedTitle = ConfigurationManager.AppSettings["FeedTitle"];
            var description = ConfigurationManager.AppSettings["FeedDescription"];
            var folderItemId = ConfigurationManager.AppSettings["OneDrive:FolderItemId"];

            string path = await GetOneDriveFolderPathAsync(folderItemId);

            var directoryPath = args.Length > 0 ? args[0] : ".";
            var di = new DirectoryInfo(directoryPath);
            var syndicationItems = di
                .EnumerateFiles("*.mp3")
                .OrderByDescending(f => f.CreationTime)
                .Select(file => GetSyndicationItem(path, file));
            var feed = new SyndicationFeed(feedTitle, description, GetOneDriveFileUrl(path, RssXml), syndicationItems);

            using (var textWriter = new XmlTextWriter(Path.Combine(directoryPath, RssXml), Encoding.UTF8))
            {
                feed.SaveAsRss20(textWriter);
            }
        }

        private static async Task<string> GetOneDriveFolderPathAsync(string folderItemId)
        {
            string response;
            using (var client = new HttpClient())
            {
                var url = $"{OneDriveBaseUrl}/drive/items/{folderItemId}";
                response = await client.GetStringAsync(url);
            }
            var json = JObject.Parse(response);

            var parentPath = json["parentReference"].Value<string>("path");
            var name = json.Value<string>("name");

            return $"{parentPath}/{name}";
        }

        private static SyndicationItem GetSyndicationItem(string path, FileInfo file)
        {
            var url = GetOneDriveFileUrl(path, file.Name);

            var syndicationItem = new SyndicationItem(file.Name, string.Empty, null, null, file.CreationTime);
            syndicationItem.Links.Add(SyndicationLink.CreateMediaEnclosureLink(url, "application/mp3", 0));

            return syndicationItem;
        }

        private static Uri GetOneDriveFileUrl(string path, string name)
        {
            return new Uri($"{OneDriveBaseUrl}{path}/{name}:/content");
        }
    }
}
