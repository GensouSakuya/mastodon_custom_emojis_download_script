using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DownloadTool
{
    public static class HttpDownloadExtend
    {
        public static string GetTextResponse(string url)
        {
            var req = (HttpWebRequest) WebRequest.Create(url);
            var res = (HttpWebResponse)req.GetResponse();
            var text = "";
            using (var stream = res.GetResponseStream())
            {
                var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                text = reader.ReadToEnd();
            }
            return text;
        }

        public static async Task<bool> HttpDownload(string url, string path,string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            if (fileName == null)
            {
                fileName = "aaa.temp";
            }

            var fullFileName = Path.Combine(path, fileName);
            if (File.Exists(fullFileName))
            {
                return true;
            }

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                using (Stream responseStream = response.GetResponseStream())
                {
                    byte[] bArr = new byte[1024];
                    int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                    if (size == 0)
                    {
                        return false;
                    }
                    using (FileStream fs = new FileStream(fullFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        while (size > 0)
                        {
                            fs.Write(bArr, 0, size);
                            size = responseStream.Read(bArr, 0, (int) bArr.Length);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}