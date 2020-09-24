using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TTLX.Common
{
    public class QuetionSerializeHelper
    {



        public void GetEditRecord()
        {
            string base_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "papers_serialize");
            if (!Directory.Exists(base_path))
                Directory.CreateDirectory(base_path);

            string[] files = Directory.GetFiles(base_path, $"*_*.paper");

            string file_path = Path.Combine(base_path, $"{ruleNo}_{Guid.NewGuid.ToString()}")
        }





    }

    public class SerializeHelper
    {
        /// <summary>
        ///  反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="strPath">已经序列化的对象路径</param>
        /// <param name="nType">0,xml类型,1二进制类型</param>
        /// <returns>反序列化后得到的对象</returns>
        public static object DeSerialize(Type type, string strPath, int nType = 0)
        {
            if (type == null)
            {
                return null;
            }
            Assembly asm = Assembly.GetExecutingAssembly();
            object obj = asm.CreateInstance(type.ToString(), true);
            if (File.Exists(strPath))
            {
                FileStream fileStream = new FileStream(strPath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
                try
                {
                    if (nType == 0)
                    {
                        XmlSerializer bf = new XmlSerializer(type);
                        obj = bf.Deserialize(fileStream);
                        fileStream.Close();
                    }
                    else
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        obj = bf.Deserialize(fileStream);
                        fileStream.Close();
                    }
                }
                catch (System.Exception ex)
                {
                    fileStream.Close();
                    throw ex;
                }
            }
            return obj;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">被序列化的对象</param>
        /// <param name="strPath">序列化后的存储路径</param>
        /// <param name="nType">0,xml类型,1二进制类型</param>
        public static void Serialize(object obj, string strPath, int nType = 0)
        {
            if (obj == null)
            {
                return;
            }
            //modify by yzb 2015.8.3 解决xml序列化异常：xml文件最后会出现多的字符（微软bug）问题
            FileStream file = null;
            if (File.Exists(strPath))
            {
                file = new FileStream(strPath, System.IO.FileMode.Truncate);
            }
            else
            {
                file = new FileStream(strPath, System.IO.FileMode.OpenOrCreate);
            }
            //FileStream file = new FileStream(strPath, System.IO.FileMode.OpenOrCreate);
            try
            {
                if (nType == 0)
                {
                    XmlSerializer bf = new XmlSerializer(obj.GetType());
                    bf.Serialize(file, obj);
                    file.Close();
                }
                else
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(file, obj);
                    file.Close();
                }
            }
            catch (System.Exception ex)
            {
                file.Close();
                throw ex;
            }

        }
    }



}
