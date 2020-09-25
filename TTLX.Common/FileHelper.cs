using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Common
{
    public class FileHelper
    {
        #region single
        private FileHelper() { }
        private static object locker = new object();
        private static FileHelper _instance = null;
        public static FileHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                            _instance = new FileHelper();
                    }
                }
                return _instance;
            }
        }
        #endregion


        //将image转化为二进制           
        public byte[] Image2Byte(Image img)
        {
            byte[] bt = null;
            if (img != null)
            {
                using (MemoryStream mostream = new MemoryStream())
                {
                    img.Save(mostream, System.Drawing.Imaging.ImageFormat.Png);//将图像以指定的格式存入缓存内存流

                    bt = new byte[mostream.Length];
                    mostream.Position = 0;//设置留的初始位置
                    mostream.Read(bt, 0, Convert.ToInt32(bt.Length));
                }
            }
            return bt;
        }

        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        public Image Byte2Image(byte[] bytes)
        {
            Image photo = null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                ms.Write(bytes, 0, bytes.Length);
                photo = Image.FromStream(ms, true);
            }
            return photo;
        }
    }
}
