using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTLX.Common
{
    public class Global
    {
        private static readonly Lazy<Global> _instance = new Lazy<Global>(() => new Global());
        public static Global Instance
        {
            get { return _instance.Value; }
        }

        /// <summary>
        /// 判断题分数
        /// </summary>
        public double PanduanScore { get; set; }
        /// <summary>
        /// 多选题分数
        /// </summary>
        public double DuoxuanScore { get; set; }

        /// <summary>
        /// 单选题分数
        /// </summary>
        public double DanxuanScore { get; set; }

        /// <summary>
        /// 乐学号
        /// </summary>
        public string LexueID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 当前专业ID
        /// </summary>
        public int CurrentSpecialtyID { get; set; }
        /// <summary>
        /// 当前专业名称
        /// </summary>
        public string CurrentSpecialtyName { get; set; }

        public string QueTypeConvertToString(int type)
        {
            switch (type)
            {
                case (int)QuestionsType.Danxuan:
                    return "单选题";
                case (int)QuestionsType.Duoxuan:
                    return "多选题";
                case (int)QuestionsType.Panduan:
                    return "判断题";
                case (int)QuestionsType.Windows:
                    return "Windows题";
                case (int)QuestionsType.Wangluo:
                    return "网络题";
                case (int)QuestionsType.Word:
                    return "Word题";
                case (int)QuestionsType.Excel:
                    return "Excel题";
                case (int)QuestionsType.PowerPoint:
                    return "Ppt题";
                case (int)QuestionsType.Access:
                    return "Access题";
                default:
                    return "C语言题";
            }
        }
    }
}
