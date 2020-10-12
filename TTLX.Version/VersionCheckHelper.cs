using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Version
{
    public class VersionCheckHelper
    {
        string updateFilePath;

        public DownloadState DownloadState = DownloadState.Nono;


        /// <summary>
        /// 检查版本
        /// </summary>
        /// <returns></returns>
        public HttpVersionResult Check()
        {
            string version_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "version.json");

            var version = JsonConvert
                .DeserializeObject<VersionModel>(File.ReadAllText(version_file));

            string ip = ConfigurationManager.AppSettings["ip"];
            string port = ConfigurationManager.AppSettings["port"];
            string baseUrl = "http://" + (string.IsNullOrEmpty(ip) ? "116.62.131.33" : ip) + ":" + (string.IsNullOrEmpty(port) ? "8099" : port);

            HttpItem item = new HttpItem
            {
                URL = baseUrl + $"/api/update/check?product_name={version.name}&version={version.version}",
                Method = "get",
                ResultType = ResultType.String,
                PostDataType = PostDataType.String,
            };

            var result = new HttpHelper().GetData(item);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HttpVersionResult>(result.Data);
            }
            else
            {
                return null;
            }
        }

        public void Download(string downlaod_path, string version)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    DownloadState = DownloadState.Ing;
                    updateFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Version", version);

                    if (!Directory.Exists(updateFilePath))
                    {
                        Directory.CreateDirectory(updateFilePath);
                    }

                    updateFilePath = Path.Combine(updateFilePath, Path.GetFileName(downlaod_path));

                    webClient.Encoding = Encoding.UTF8;
                    webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                    webClient.DownloadFileAsync(new Uri(downlaod_path), updateFilePath);

                }
                catch (Exception ex)
                {
                    DownloadState = DownloadState.Fail;
                    LogTool.AddLog("下载失败，原因:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 下载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            DownloadState = DownloadState.Finished;
        }


        public void Update()
        {




            Console.WriteLine("正在启动出题工具……");


        }

        /// <summary>
        /// 替换文件
        /// </summary>
        public void Install()
        {
            using (ZipFile zip = new ZipFile(updateFilePath, Encoding.Default))
            {
                zip.ExtractAll(AppDomain.CurrentDomain.BaseDirectory, ExtractExistingFileAction.OverwriteSilently);
            }
        }
        /// <summary>
        /// 停止exe
        /// </summary>
        public void StopExe()
        {
            string product_name = "2020年模拟试卷出题工具";

            Process[] processes = Process.GetProcessesByName(product_name);
            foreach (Process p in processes)
            {
                p.Kill();
                p.Close();
            }
        }
        /// <summary>
        /// 启动exe
        /// </summary>
        public void StartExe()
        {
            Process.Start(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "2020年模拟试卷出题工具.exe"));
        }
        /// <summary>
        /// 更新版本号
        /// </summary>
        /// <param name="version"></param>
        public void UpdateVersion(string version)
        {
            VersionModel model = new VersionModel();
            model.name = "TTLX.MockTestPaper_V2";
            model.version = version;

            string version_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "version.json");

            File.WriteAllText(version_file, Newtonsoft.Json.JsonConvert.SerializeObject(model));

        }


    }
}
