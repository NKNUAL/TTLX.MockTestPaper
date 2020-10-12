using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TTLX.Common;
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
        public List<MockPaperInfo> GetPapers(string userId, string ruleNo, out string message)
        {
            message = string.Empty;

            var result = new HttpHelper().GetData(GetHttpItem("get", $"/api/mock/get/paper/{userId}?ruleNo={ruleNo}"));

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
