using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Version
{
    public class LogTool
    {
        static readonly string temp = AppDomain.CurrentDomain.BaseDirectory;
        public static void AddLog(String value)
        {
            if (Directory.Exists(Path.Combine(temp, @"log\")) == false)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(temp, @"log\"));
                directoryInfo.Create();
            }
            using (StreamWriter sw = File.AppendText(Path.Combine(temp, $"log\\{DateTime.Now.ToString("yyyy-MM-dd")}.log")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + value);
            }
        }
    }
}
