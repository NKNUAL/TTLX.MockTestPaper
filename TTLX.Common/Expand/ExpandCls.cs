using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Common.Expand
{
    public static class ExpandCls
    {
        public static string ToNormalString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        public static string ToNormalStringWithout(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmss");
        }

        public static string ToNormalStringWithout_ymd(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }

        public static string GetGuid(this Guid guid)
        {
            return guid.ToString().Replace("-", "");
        }

        public static List<T> Copy<T>(this List<T> list)
        {
            if (list == null)
                return null;

            string obj = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(obj);

        }

    }
}
