using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTLX.Common;
using TTLX.Common.Expand;
using TTLX.Controller;
using TTLX.Controller.Model;
using TTLX.Controller.RequestModel;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2.Nurse
{
    public partial class FrmCreateQuestion_Nurse : Form
    {


        private FrmCreateQuestion_Nurse()
        {
            InitializeComponent();
        }

        public QuestionsInfoModel2 _question;
        FrmQuestion_Nurse _frmQuestion;
        string _p_guid;
        EditMode _editMode;
        int _typeId;

        int queType = 1;//默认单选题

        int editType;

        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="p_guid"></param>
        /// <param name="editMode"></param>
        public FrmCreateQuestion_Nurse(FrmQuestion_Nurse frmQuestion, int typeId, string p_guid, EditMode editMode)
            : this()
        {
            _p_guid = p_guid;
            _editMode = editMode;
            _frmQuestion = frmQuestion;

            _typeId = typeId;

            editType = 1;
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="question"></param>
        /// <param name="p_guid"></param>
        /// <param name="editMode"></param>
        public FrmCreateQuestion_Nurse(FrmQuestion_Nurse frmQuestion, QuestionsInfoModel2 question, int typeId, string p_guid, EditMode editMode)
            : this()
        {
            _question = question;
            _p_guid = p_guid;
            _editMode = editMode;
            _frmQuestion = frmQuestion;

            _typeId = typeId;

            editType = 2;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            InitQueType();

            if (_question != null)
            {
                tbQueName.Text = _question.QueContent;
                txtOptionA.Text = _question.Option0;
                txtOptionB.Text = _question.Option1;
                txtOptionC.Text = _question.Option2;
                txtOptionD.Text = _question.Option3;

                if (_question.DifficultLevel == 1)
                    this.rbtnDifficultLevel1.Checked = true;
                else if (_question.DifficultLevel == 2)
                    this.rbtnDifficultLevel2.Checked = true;
                else if (_question.DifficultLevel == 3)
                    this.rbtnDifficultLevel3.Checked = true;

                txtAnswerJiexi.Text = _question.ResolutionTips;
            }
            else
            {
                _question = new QuestionsInfoModel2
                {
                    QueType = queType
                };
            }

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            lblQueType.Text = Global.Instance.QueTypeConvertToString(queType);

            InitCbCourse(_question.CourseNo);

            InitCbKnow(_question.KnowNo);

            InitAnswer(_question.QueType, _question.Answer);//加载答案
        }


        private void InitCbCourse(string courseNo)
        {
            var courses = WebApiController.Instance
                .GetCourse(Global.Instance.CurrentSpecialtyID.ToString(), out _);

            cbCourses.Items.AddRange(courses.ToArray());

            cbCourses.SelectedItem = courses.Find(x => x.Key == courseNo);
        }

        private void InitCbKnow(string knowNo)
        {
            foreach (var item in cbKnows.Items)
            {
                if (item is KVModel kv)
                {
                    if (kv.Key == knowNo)
                    {
                        cbKnows.SelectedItem = item;
                        return;
                    }
                }
            }
        }

        private void InitAnswer(int queType, string standAnswer)
        {
            if (queType == (int)QuestionsType.Danxuan)
            {
                panAnswerDanxuan.Visible = true;
                panAnswerDuoxuan.Visible = false;
                panAnswerPanduan.Visible = false;
                foreach (Control control in this.panAnswerDanxuan.Controls)
                {
                    RadioButton rbtn = control as RadioButton;
                    if (rbtn.Text.Trim() == standAnswer)
                    {
                        rbtn.Checked = true;
                        break;
                    }
                }
            }
            else if (queType == (int)QuestionsType.Duoxuan)
            {
                panAnswerDanxuan.Visible = false;
                panAnswerDuoxuan.Visible = true;
                panAnswerPanduan.Visible = false;
                foreach (var an in standAnswer)
                {
                    if (an == 'A')
                        ckbAnswerA.Checked = true;
                    else if (an == 'B')
                        ckbAnswerB.Checked = true;
                    else if (an == 'C')
                        ckbAnswerC.Checked = true;
                    else if (an == 'D')
                        ckbAnswerD.Checked = true;
                }
            }
            else if (queType == (int)QuestionsType.Panduan)
            {
                panAnswerDanxuan.Visible = false;
                panAnswerDuoxuan.Visible = false;
                panAnswerPanduan.Visible = true;
                if (standAnswer == "A")
                    rbtnPanduanAnswerA.Checked = true;
                else if (standAnswer == "B")
                    rbtnPanduanAnswerB.Checked = true;
            }
        }

        private void InitQueType()
        {
            if (queType == (int)QuestionsType.Danxuan)//单选题
            {
                panAnswerDanxuan.Visible = true;
                panAnswerDuoxuan.Visible = false;
                panAnswerPanduan.Visible = false;

                EnableControl(true);
                txtOptionA.Text = "";
                txtOptionB.Text = "";
            }
            else if (queType == (int)QuestionsType.Duoxuan)//多选
            {
                panAnswerDanxuan.Visible = false;
                panAnswerDuoxuan.Visible = true;
                panAnswerPanduan.Visible = false;

                EnableControl(true);
                txtOptionA.Text = "";
                txtOptionB.Text = "";
            }
            else//判断
            {
                panAnswerDanxuan.Visible = false;
                panAnswerDuoxuan.Visible = false;
                panAnswerPanduan.Visible = true;

                EnableControl(false);
                txtOptionA.Text = "正确";
                txtOptionB.Text = "错误";
            }
        }

        private void EnableControl(bool enable)
        {
            txtOptionA.Enabled = enable;
            txtOptionB.Enabled = enable;
            txtOptionC.Enabled = enable;
            txtOptionD.Enabled = enable;
            btnImgA.Enabled = enable;
            btnImgB.Enabled = enable;
            btnImgC.Enabled = enable;
            btnImgD.Enabled = enable;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string queName = tbQueName.Text.Trim();
            string optionA = txtOptionA.Text.Trim();
            string optionB = txtOptionB.Text.Trim();
            string optionC = txtOptionC.Text.Trim();
            string optionD = txtOptionD.Text.Trim();
            byte[] queNameImg = _question.NameImg;
            byte[] AImg = _question.Option0Img;
            byte[] BImg = _question.Option1Img;
            byte[] CImg = _question.Option2Img;
            byte[] DImg = _question.Option3Img;
            string jiexi = txtAnswerJiexi.Text.Trim();
            string standardAnswer = string.Empty;
            int difficultLevel = 1;

            #region 合法性判断

            if (!(cbCourses.SelectedItem is KVModel kvCourse))
            {
                MessageBox.Show("请选择科目！");
                return;
            }
            else
            {
                _question.CourseNo = kvCourse.Key;
            }

            if (!(cbKnows.SelectedItem is KVModel kvKnow))
            {
                MessageBox.Show("请选择知识点！");
                return;
            }
            else
            {
                _question.KnowNo = kvKnow.Key;
            }

            if (string.IsNullOrEmpty(queName) && queNameImg == null)
            {
                MessageBox.Show("请输入题干!");
                return;
            }

            if (string.IsNullOrEmpty(optionA) && AImg == null)
            {
                MessageBox.Show("请输入选项A!");
                return;
            }
            if (string.IsNullOrEmpty(optionB) && BImg == null)
            {
                MessageBox.Show("请输入选项B!");
                return;
            }

            if (queType != 3)
            {
                if (string.IsNullOrEmpty(optionC) && CImg == null)
                {
                    MessageBox.Show("请输入选项C!");
                    return;
                }
                if (string.IsNullOrEmpty(optionD) && DImg == null)
                {
                    MessageBox.Show("请输入选项D!");
                    return;
                }
            }


            if (string.IsNullOrEmpty(jiexi))
            {
                MessageBox.Show("请输入解析!");
                return;
            }

            // 答案
            #region 
            if (queType == 1)
            {
                foreach (Control control in this.panAnswerDanxuan.Controls)
                {
                    if (control is RadioButton)
                    {
                        if ((control as RadioButton).Checked)
                        {
                            standardAnswer = control.Text;
                            continue;
                        }
                    }
                }
            }
            else if (queType == 2)
            {
                foreach (Control control in this.panAnswerDuoxuan.Controls)
                {
                    if (control is CheckBox)
                    {
                        if ((control as CheckBox).Checked)
                        {
                            standardAnswer += control.Text;
                        }
                    }
                }
            }
            else if (queType == 3)
            {
                if (rbtnPanduanAnswerA.Checked)
                    standardAnswer = "A";
                else
                    standardAnswer = "B";
            }
            #endregion
            if (string.IsNullOrEmpty(standardAnswer))
            {
                MessageBox.Show("请选择答案!");
                return;
            }

            if (rbtnDifficultLevel1.Checked)
                difficultLevel = 1;
            else if (rbtnDifficultLevel2.Checked)
                difficultLevel = 2;
            else if (rbtnDifficultLevel3.Checked)
                difficultLevel = 3;

            #endregion

            if (_editMode == EditMode.Create && string.IsNullOrEmpty(_question.No))
                _question.No = Guid.NewGuid().GetGuid();

            _question.DifficultLevel = difficultLevel;
            _question.QueType = queType;
            _question.QueContent = queName;
            _question.Option0 = optionA;
            _question.Option1 = optionB;
            _question.Option2 = optionC;
            _question.Option3 = optionD;
            _question.NameImg = queNameImg;
            _question.Option0Img = AImg;
            _question.Option1Img = BImg;
            _question.Option2Img = CImg;
            _question.Option3Img = DImg;
            _question.Answer = standardAnswer;
            _question.ResolutionTips = jiexi;

            if (_editMode == EditMode.Create)
            {
                if (CheckSimilarity(_question.No, queName, optionA, optionB, optionC, optionD))
                {
                    Task.Factory.StartNew(SaveQuestion);
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (CheckSimilarity(_question.No, queName, optionA, optionB, optionC, optionD))
                {
                    bool requestBool = WebApiController.Instance.CheckRepeatQuestions(new QuestionCheckModel
                    {
                        SpecialtyId = Global.Instance.CurrentSpecialtyID.ToString(),
                        QueContent = queName,
                        OptionA = optionA,
                        OptionB = optionB,
                        OptionC = optionC,
                        OptionD = optionD
                    }, out string queResult, out string message);



                    //bool resultBool = WebApiController.Instance
                    //    .EditQuestion(Global.Instance.CurrentSpecialtyID.ToString(), _question, out bool success, out message);

                    //if (resultBool)
                    //{
                    //    if (success)
                    //    {
                    //        MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                }
                else
                {
                    return;
                }

            }

            DialogResult = DialogResult.OK;
        }

        private bool CheckSimilarity(string queNo, string queName, string optionA, string optionB, string optionC, string optionD)
        {
            if (!_frmQuestion.CheckSimilarity(queNo, queName, out string content))
            {
                bool requestBool = WebApiController.Instance.CheckRepeatQuestions(new QuestionCheckModel
                {
                    SpecialtyId = Global.Instance.CurrentSpecialtyID.ToString(),
                    QueContent = queName,
                    OptionA = optionA,
                    OptionB = optionB,
                    OptionC = optionC,
                    OptionD = optionD
                }, out string queResult, out string message);

                if (requestBool)
                {
                    if (queResult != null)
                    {
                        MessageBox.Show("发现相似度大于60%的试题，推荐试题必须是题库中不存在相似的试题!!!" +
                            $"[题库题目内容]：【{queResult}】", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show($"您已经出过了【{content}】题目，请您重新出题！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void SaveQuestion()
        {
            WebApiController.Instance.SaveLocalQuestion_Nrese(_p_guid, Global.Instance.CurrentSpecialtyID, editType, new PutQuestionA_Model { TypeId = _typeId, Questions = new List<QuestionsInfoModel2> { _question } });
        }

        private void btnImg_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            byte[] imgByte;
            switch (btn.Name)
            {
                case "btnImgContent":
                    imgByte = _question.NameImg;
                    FrmQuestionImg frmContent = new FrmQuestionImg(imgByte);
                    frmContent.ShowDialog();
                    _question.NameImg = frmContent.imgBytes;
                    break;
                case "btnImgA":
                    imgByte = _question.Option0Img;
                    FrmQuestionImg frmA = new FrmQuestionImg(imgByte);
                    frmA.ShowDialog();
                    _question.Option0Img = frmA.imgBytes;
                    break;
                case "btnImgB":
                    imgByte = _question.Option1Img;
                    FrmQuestionImg frmB = new FrmQuestionImg(imgByte);
                    frmB.ShowDialog();
                    _question.Option1Img = frmB.imgBytes;
                    break;
                case "btnImgC":
                    imgByte = _question.Option2Img;
                    FrmQuestionImg frmC = new FrmQuestionImg(imgByte);
                    frmC.ShowDialog();
                    _question.Option2Img = frmC.imgBytes;
                    break;
                case "btnImgD":
                    imgByte = _question.Option3Img;
                    FrmQuestionImg frmD = new FrmQuestionImg(imgByte);
                    frmD.ShowDialog();
                    _question.Option3Img = frmD.imgBytes;
                    break;
                default:
                    break;
            }

        }

        private void cbKnows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKnows.SelectedItem is KVModel kv)
            {
                _question.KnowNo = kv.Key;
            }
        }

        private void cbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCourses.SelectedItem is KVModel kv)
            {
                var knows = WebApiController.Instance
                    .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), kv.Key, out _);

                cbKnows.Items.AddRange(knows.ToArray());
            }
        }
    }
}
