﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTLX.Common;

namespace TTLX.MockTestPaper_V2
{
    static class Program
    {
        public int a =;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (IsAppRunning())
                {
                    MessageBox.Show("应用程序已经在运行中...");
                    Environment.Exit(1);
                    return;
                }

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                FrmLogin login = new FrmLogin();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new FrmPapers());
                }

            }
            catch (Exception ex)
            {
                LogTool.AddLog(ex.Message);
            }
        }

        static Mutex mutex;
        static bool IsAppRunning()
        {
            try
            {
                Mutex.OpenExisting("TTLX.MockTestPaper");
            }
            catch
            {
                mutex = new Mutex(true, "TTLX.MockTestPaper");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 处理UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                LogTool.AddLog(e.Exception.ToString());
            }
            catch
            {
                LogTool.AddLog("不可恢复的Windows窗体线程异常，应用程序将退出！");
            }
        }
        /// <summary>
        /// 处理未捕获的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                LogTool.AddLog(((Exception)e.ExceptionObject).ToString());
            }
            catch
            {
                LogTool.AddLog("不可恢复的非Windows窗体线程异常，应用程序将退出！");
            }
        }
    }
}
