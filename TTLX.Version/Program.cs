using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TTLX.Version
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine($"检查版本中……");
            VersionCheckHelper versionCheckHelper = new VersionCheckHelper();

            var checkResult = versionCheckHelper.Check();

            if (checkResult == null)
            {
                Console.WriteLine($"网络连接错误，请确保有网再运行程序……");
                Console.ReadKey();
            }
            else
            {
                if (checkResult.CheckState)
                {
                    //下载 
                    versionCheckHelper.Download(checkResult.PackageUrl, checkResult.Version);

                    Console.WriteLine("检测到新版本，正在下载……");

                    while (versionCheckHelper.DownloadState == DownloadState.Ing)
                    {
                        Thread.Sleep(500);
                    }

                    if (versionCheckHelper.DownloadState == DownloadState.Finished)
                    {
                        Console.WriteLine("正在安装……");
                        versionCheckHelper.StopExe();
                        versionCheckHelper.Install();
                        versionCheckHelper.StartExe();
                        versionCheckHelper.UpdateVersion(checkResult.Version);

                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("下载失败……");
                    }

                    Console.ReadKey();
                }
            }


        }
    }
}
