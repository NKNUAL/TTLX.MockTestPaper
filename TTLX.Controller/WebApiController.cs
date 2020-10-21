using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TTLX.Common;
using TTLX.Controller.Model;
using TTLX.Controller.RequestModel;
using TTLX.Controller.ResposeModel;

namespace TTLX.Controller
{
    public class WebApiController
    {
        #region 单例
        private static readonly Lazy<WebApiController> _instance = new Lazy<WebApiController>(() => new WebApiController());
        public static WebApiController Instance
        {
            get { return _instance.Value; }
        }
        #endregion

        private string _baseUrl = "";
        private WebApiController()
        {
            string unionip = ConfigurationManager.AppSettings["ip"];
            string port = ConfigurationManager.AppSettings["port"];
            _baseUrl = "http://" + (string.IsNullOrEmpty(unionip) ? "116.62.131.33" : unionip) + ":" + (string.IsNullOrEmpty(port) ? "8099" : port);
        }

        WebHeaderCollection header = null;

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public UserInfoModel UserLogin(string userId, string pwd, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("post", "/api/mock/login", new LoginModel { UserId = userId, Password = pwd }));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var user = JsonConvert.DeserializeObject<UserInfoModel>(resultData.data.ToString());
                    header = new WebHeaderCollection { { "x-mock-token", user.Token } };

                    return user;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 获取所有规则
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<QuestionRule> GetAllRules(string userId, out string message)
        {

            message = string.Empty;

            var rules = CacheController.Instance(Global.Instance.CurrentSpecialtyID.ToString()).GetRules();

            if (rules != null)
                return rules;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/rules/{userId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    rules = JsonConvert.DeserializeObject<List<QuestionRule>>(resultData.data.ToString());
                    CacheController.Instance(Global.Instance.CurrentSpecialtyID.ToString()).SetRules(rules);
                    return rules;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 护理专业获取规则
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public QuestionRule_Nurse GetAllRules_Nurse(string userId, out string message)
        {

            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/rules/{userId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var rules = JsonConvert.DeserializeObject<QuestionRule_Nurse>(resultData.data.ToString());
                    return rules;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }


        /// <summary>
        /// 获取科目
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        public List<KVModel> GetCourse(string specialtyId, out string message)
        {
            message = string.Empty;

            var courses = CacheController.Instance(specialtyId).GetCourse();
            if (courses != null)
                return courses.Select(x => new KVModel { Key = x.Key, Value = x.Value }).ToList();

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/course/{specialtyId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    string data = resultData.data.ToString();
                    var course = JsonConvert.DeserializeObject<List<KVModel>>(data);
                    CacheController.Instance(specialtyId).SetCourse(course.ToDictionary(k => k.Key, v => v.Value));
                    return course;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 获取知识点
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <param name="courseNo"></param>
        /// <returns></returns>
        public List<KVModel> GetKnows(string specialtyId, string courseNo, out string message)
        {
            message = string.Empty;

            var knows = CacheController.Instance(specialtyId).GetKnows(courseNo);
            if (knows != null)
                return knows.Select(x => new KVModel { Key = x.Key, Value = x.Value }).ToList();

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/course/{specialtyId}/{courseNo}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    string data = resultData.data.ToString();
                    var know = JsonConvert.DeserializeObject<List<KVModel>>(data);
                    CacheController.Instance(specialtyId).SetKnow(courseNo, know.ToDictionary(k => k.Key, v => v.Value));
                    return know;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 获取试卷
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public List<MockPaperInfo> GetPapers(string userId, int specialtyId, string ruleNo, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/paper/{userId}/{specialtyId}?ruleNo={ruleNo}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var papers = JsonConvert.DeserializeObject<List<MockPaperInfo>>(resultData.data.ToString());
                    return papers;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 获取试卷--护理专业
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public List<MockPaperNurseInfo> GetPapers_Nurse(string userId, int specialtyId, string ruleNo, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/paper/{userId}/{specialtyId}?ruleNo={ruleNo}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var papers = JsonConvert.DeserializeObject<List<MockPaperNurseInfo>>(resultData.data.ToString());
                    return papers;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }


        /// <summary>
        /// 创建模拟试卷
        /// </summary>
        /// <param name="paper"></param>
        /// <returns></returns>
        public bool CreatePaper(PutQuestionModel paper, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("put", $"/api/mock/put/paper", paper));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    return resultData.success;
                }
                else
                {
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }

        /// <summary>
        /// 创建模拟试卷
        /// </summary>
        /// <param name="paper"></param>
        /// <returns></returns>
        public bool CreatePaper_Nurse(PutQuestionNurseModel paper, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("put", $"/api/mock/put/paper_nurse", paper));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    return resultData.success;
                }
                else
                {
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }

        /// <summary>
        /// 检查重复题目
        /// </summary>
        /// <param name="checkModel"></param>
        /// <param name="queContent"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CheckRepeatQuestions(QuestionCheckModel checkModel, out string queContent, out string message)
        {
            queContent = string.Empty;
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("post", $"/api/mock/get/similarity", checkModel));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    queContent = resultData.data?.ToString();
                    return resultData.success;
                }
                else
                {
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rule"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddRule(string userId, QuestionRule rule, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("put", $"/api/mock/put/rule/{userId}", rule));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    return resultData.success;
                }
                else
                {
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ruleNo"></param>
        /// <returns></returns>
        public bool DelRule(string userId, string ruleNo, out bool success, out string message)
        {
            success = false;
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("delete", $"/api/mock/del/rule/{userId}/{ruleNo}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    success = true;
                    return resultData.success;
                }
                else
                {
                    success = false;
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <returns></returns>
        public bool EditRule(string userId, QuestionRule rule, out bool success, out string message)
        {
            success = false;
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("post", $"/api/mock/edit/rule/{userId}", rule));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    success = true;
                    return resultData.success;
                }
                else
                {
                    success = false;
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }

        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <param name="queModel"></param>
        /// <returns></returns>
        public bool EditQuestion(string specialtyId, QuestionsInfoModel queModel, out bool success, out string message)
        {
            success = false;
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("post", $"/api/mock/edit/question/{specialtyId}", queModel));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    success = true;
                    return resultData.success;
                }
                else
                {
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }


        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <param name="queModel"></param>
        /// <returns></returns>
        public bool EditQuestion_Nurse(string specialtyId, PutQuestionA_Model queModel, out bool success, out string message)
        {
            success = false;
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("post", $"/api/mock/edit/question_nurse/{specialtyId}", queModel));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    success = true;
                    return resultData.success;
                }
                else
                {
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }


        /// <summary>
        /// 获取试卷题目
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<MockPaperCourseTreeModel> GetPaperDetails(int paperId, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/paperdetail/{paperId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var ques = JsonConvert.DeserializeObject<List<MockPaperCourseTreeModel>>(resultData.data.ToString());
                    return ques;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 获取试卷题目--护理专业
        /// </summary>
        /// <param name="paperId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<PutQuestionA_Model> GetPaperDetails_Nurse(int paperId, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/paperdetail/{paperId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var ques = JsonConvert.DeserializeObject<List<PutQuestionA_Model>>(resultData.data.ToString());
                    return ques;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 获取专业基础规则
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        public BaseRule GetBaseRule(string specialtyId, out string message)
        {
            message = string.Empty;

            var baseRule = CacheController.Instance(specialtyId).GetBaseRule();

            if (baseRule != null)
                return baseRule;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/baserule/{specialtyId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var rule = JsonConvert.DeserializeObject<BaseRule>(resultData.data.ToString());

                    CacheController.Instance(specialtyId).SetBaseRule(rule);

                    return rule;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;

        }


        #region 本地记录
        /// <summary>
        /// 上传本地记录
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        public bool UploadRecord(UploadLocalRecord upload, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("post", $"/api/mock/upload/record", upload));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    return resultData.success;
                }
                else
                {
                    message = resultData.message;
                    return resultData.success;
                }
            }
            message = "网络连接错误";
            return false;
        }

        /// <summary>
        /// 获取本地编辑记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<EditPaperRecord> GetLocalPaperRecord(string userId, int specialtyId, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/record/{userId}/{specialtyId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var records = JsonConvert.DeserializeObject<List<EditPaperRecord>>(resultData.data.ToString());

                    return records;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 获取本地编辑记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<EditPaperRecord_Nurse> GetLocalPaperRecord_Nurse(string userId, int specialtyId, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/record/{userId}/{specialtyId}"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                var resultData = JsonConvert.DeserializeObject<HttpResultModel>(result.Data);
                if (resultData.success)
                {
                    var records = JsonConvert.DeserializeObject<List<EditPaperRecord_Nurse>>(resultData.data.ToString());

                    return records;
                }
                else
                {
                    message = resultData.message;
                    return null;
                }
            }
            message = "网络连接错误";
            return null;
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        public void SaveLocalPaper(string userId, string pGuid, string ruleNo)
        {
            new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/save/record/{userId}?pGuid={pGuid}&ruleNo={ruleNo}"));
        }

        /// <summary>
        /// 保存记录
        /// </summary>
        public void SaveLocalQuestion(string pGuid, int specialtyId, string courseNo, string knowNo, QuestionsInfoModel info)
        {
            new HttpHelper().GetData(GetHttpItem("post", $"/api/mock/save/question/{pGuid}/{specialtyId}/{courseNo}/{knowNo}", info));
        }


        /// <summary>
        /// 保存记录--护理专业
        /// </summary>
        public void SaveLocalQuestion_Nrese(string pGuid, int specialtyId, int editType, PutQuestionA_Model info)
        {
            new HttpHelper().GetData(GetHttpItem("post", $"/api/mock/save/question/{pGuid}/{specialtyId}/{editType}", info));
        }


        /// <summary>
        /// 删除本地编辑记录
        /// </summary>
        /// <param name="pGuid"></param>
        public void DeleteLocalRecord(string pGuid)
        {
            new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/del/record/{pGuid}"));
        }
        #endregion

        private HttpItem GetHttpItem(string method, string url, object data = null)
        {
            HttpItem item = new HttpItem
            {
                URL = _baseUrl + url,
                Header = header,
                Method = method,
                ResultType = ResultType.String,
                PostDataType = PostDataType.String,
            };
            if (method.ToUpper() != "GET")
            {
                item.ContentType = "application/json";
                if (data != null)
                {
                    item.Postdata = JsonConvert.SerializeObject(data);
                }
            }
            return item;
        }
    }
}
