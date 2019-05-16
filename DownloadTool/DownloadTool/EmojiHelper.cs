using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static DownloadTool.HttpDownloadExtend;

namespace DownloadTool
{
    public class EmojiHelper
    {
        public static List<EmojiItem> GetAllEmoji(string instanceName)
        {
            var json = GetTextResponse($"https://{instanceName}/api/v1/custom_emojis");

            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmojiItem>>(json);
            
            return list;
        }

        public static void StartDownload(List<EmojiItem> emojiItems,string instanceName,string folderPath = null)
        {
            if (folderPath == null)
            {
                folderPath = Directory.GetCurrentDirectory();
            }
            
            var tasks = emojiItems.Select(async t =>
            {
                var fileType = t.url.Split(".").LastOrDefault()?.Split("?").FirstOrDefault();
                await HttpDownload(t.url, $@"{folderPath}\{instanceName}\",
                    $"{t.shortcode}.{(string.IsNullOrWhiteSpace(fileType) ? "png" : fileType)}");
            });
            Task.WaitAll(tasks.ToArray());
        }
    }
}