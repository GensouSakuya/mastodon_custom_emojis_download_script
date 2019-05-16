using System;

namespace DownloadTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入实例名：");
            var instanceName = Console.ReadLine();
            EmojiHelper.StartDownload(EmojiHelper.GetAllEmoji(instanceName), instanceName);
        }
    }
}
