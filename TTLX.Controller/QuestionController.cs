using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TTLX.Controller.Model;
using TTLX.Controller.ResposeModel;
using TTLX.DataBase.Entity;
using TTLX.Common.Expand;

namespace TTLX.Controller
{
    public class QuestionController
    {
        #region single
        private static readonly Lazy<QuestionController> _instance = new Lazy<QuestionController>(() => new QuestionController());
        public static QuestionController Instance
        {
            get { return _instance.Value; }
        }

        private QuestionController()
        {
            //初始化数据库
            using (DbSqlite db = DbSqlite.Instance)
            {
                var temp = db.PaperRecord.ToList();
            }
        }
        #endregion


        /// <summary>
        /// 获取试卷记录
        /// </summary>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public List<EditPaperRecord> GetPaperRecords(string ruleNo)
        {
            using (DbSqlite db = DbSqlite.Instance)
            {

                var query = from a in db.PaperRecord
                            join b in db.PaperQuestionRelation on a.PGuid equals b.PGuid
                            join c in db.QuestionInfo on b.QGuid equals c.QGuid
                            orderby a.PaperEditDate descending
                            select new
                            {
                                a.IsNormal,
                                a.PGuid,
                                a.RuleNo,
                                a.PaperEditDate,
                                Question = c
                            };

                if (!string.IsNullOrEmpty(ruleNo))
                    query = query.Where(p => p.IsNormal == false && p.RuleNo == ruleNo);
                else
                    query = query.Where(p => p.IsNormal == false);

                var ques = query.ToList();

                Dictionary<string, EditPaperRecord> dic_record = new Dictionary<string, EditPaperRecord>();

                foreach (var item in ques)
                {
                    if (!dic_record.ContainsKey(item.PGuid))
                    {
                        dic_record.Add(item.PGuid, new EditPaperRecord
                        {
                            RuleNo = item.RuleNo,
                            PGuid = item.PGuid,
                            EditDate = item.PaperEditDate,
                            DicQuestions = new Dictionary<string, Dictionary<string, List<QuestionsInfoModel>>>()
                        });
                    }

                    if (!dic_record[item.PGuid].DicQuestions.ContainsKey(item.Question.CourseNo))
                    {
                        dic_record[item.PGuid].DicQuestions
                            .Add(item.Question.CourseNo, new Dictionary<string, List<QuestionsInfoModel>>());
                    }

                    if (!dic_record[item.PGuid].DicQuestions[item.Question.CourseNo].ContainsKey(item.Question.KnowNo))
                    {
                        dic_record[item.PGuid].DicQuestions[item.Question.CourseNo]
                            .Add(item.Question.KnowNo, new List<QuestionsInfoModel>());
                    }

                    dic_record[item.PGuid].DicQuestions[item.Question.CourseNo][item.Question.KnowNo]
                        .Add(new QuestionsInfoModel
                        {
                            Answer = item.Question.Answer,
                            NameImg = item.Question.NameImg,
                            Option0 = item.Question.Option0,
                            Option0Img = item.Question.Option0Img,
                            Option1 = item.Question.Option1,
                            Option1Img = item.Question.Option1Img,
                            Option2 = item.Question.Option2,
                            Option2Img = item.Question.Option2Img,
                            Option3 = item.Question.Option3,
                            Option3Img = item.Question.Option3Img,
                            Option4 = item.Question.Option4,
                            Option4Img = item.Question.Option4Img,
                            Option5 = item.Question.Option5,
                            Option5Img = item.Question.Option5Img,
                            QueContent = item.Question.QueContent,
                            QueType = item.Question.QueType,
                            ResolutionTips = item.Question.ResolutionTips,
                        });
                }


                return dic_record.Values.ToList();

            }
        }

        /// <summary>
        /// 保存临时记录
        /// </summary>
        /// <param name="pGuid"></param>
        /// <param name="ruleNo"></param>
        public async void SavePaper(string pGuid, string ruleNo)
        {
            using (DbSqlite db = DbSqlite.Instance)
            {
                try
                {
                    var count = db.PaperRecord.Count(p => p.PGuid == pGuid);

                    if (count == 0)
                    {
                        db.PaperRecord.Add(new PaperRecord
                        {
                            IsNormal = false,
                            PaperEditDate = DateTime.Now.ToNormalString(),
                            PGuid = pGuid,
                            RuleNo = ruleNo
                        });

                        await db.SaveChangesAsync();

                    }
                }
                catch
                {

                }


            }
        }

        /// <summary>
        /// 保存试题
        /// </summary>
        /// <param name="pGuid"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<bool> SaveQuestions(string pGuid, string courseNo, string knowNo, QuestionsInfoModel info)
        {
            using (DbSqlite db = DbSqlite.Instance)
            {
                try
                {
                    var que = db.QuestionInfo.FirstOrDefault(q => q.QGuid == info.No);

                    if (que == null)
                    {
                        db.QuestionInfo.Add(new QuestionInfo
                        {
                            Answer = info.Answer,
                            CourseNo = courseNo,
                            DifficultLevel = info.DifficultLevel,
                            KnowNo = knowNo,
                            NameImg = info.NameImg,
                            Option0 = info.Option0,
                            Option0Img = info.Option0Img,
                            Option1 = info.Option1,
                            Option1Img = info.Option1Img,
                            Option2 = info.Option2,
                            Option2Img = info.Option2Img,
                            Option3 = info.Option3,
                            Option3Img = info.Option3Img,
                            QGuid = info.No,
                            QueContent = info.QueContent,
                            QueType = info.QueType,
                            ResolutionTips = info.ResolutionTips,
                        });
                        await db.SaveChangesAsync();

                        db.PaperQuestionRelation.Add(new PaperQuestionRelation
                        {
                            PGuid = pGuid,
                            QGuid = info.No,
                            RGuid = Guid.NewGuid().GetGuid(),
                        });

                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        que.Answer = info.Answer;
                        que.Option0 = info.Option0;
                        que.Option0Img = info.Option0Img;
                        que.Option1 = info.Option1;
                        que.Option1Img = info.Option1Img;
                        que.Option2 = info.Option2;
                        que.Option2Img = info.Option2Img;
                        que.Option3 = info.Option3;
                        que.Option3Img = info.Option3Img;
                        que.QueContent = info.QueContent;
                        que.NameImg = info.NameImg;
                        que.QueType = info.QueType;
                        que.ResolutionTips = info.ResolutionTips;
                        que.DifficultLevel = info.DifficultLevel;
                        await db.SaveChangesAsync();
                    }
                }
                catch
                {
                    return false;
                }


            }
            return true;
        }

        /// <summary>
        /// 删除正常记录
        /// </summary>
        /// <param name="pGuid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteNormalRecord(string pGuid)
        {
            using (DbSqlite db = DbSqlite.Instance)
            {
                try
                {
                    var paper = db.PaperRecord.FirstOrDefault(q => q.PGuid == pGuid);
                    if (paper != null)
                    {
                        db.PaperRecord.Remove(paper);
                        await db.SaveChangesAsync();

                        var relations = db.PaperQuestionRelation.Where(q => q.PGuid == pGuid).ToList();

                        foreach (var item in relations)
                        {
                            var que = db.QuestionInfo.FirstOrDefault(q => q.QGuid == item.QGuid);

                            db.PaperQuestionRelation.Remove(item);

                            db.QuestionInfo.Remove(que);
                        }

                        await db.SaveChangesAsync();


                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}
