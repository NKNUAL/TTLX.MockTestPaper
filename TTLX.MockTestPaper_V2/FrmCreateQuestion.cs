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
using TTLX.Controller.RequestModel;
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmCreateQuestion : Form
    {


        private FrmCreateQuestion()
        {
            InitializeComponent();
        }

        public QuestionsInfoModel _question;
        CourseRule _courseRule;
        int _queType;
        public string _knowNo;
        string _p_guid;
        EditMode _editMode;

        public FrmCreateQuestion(CourseRule courseRule, int queType, string knowNo, string p_guid, EditMode editMode)
            : this()
        {
            _courseRule = courseRule;
            _queType = queType;
            _knowNo = knowNo;
            _p_guid = p_guid;
            _editMode = editMode;
        }

        public FrmCreateQuestion(QuestionsInfoModel question, CourseRule courseRule, int queType, string knowNo, string p_guid, EditMode editMode)
            : this()
        {
            _question = question;
            _courseRule = courseRule;
            _queType = queType;
            _knowNo = knowNo;
            _p_guid = p_guid;
            _editMode = editMode;
        }


        //public FrmCreateQuestion(QuestionsInfoModel question, SubRule course, SubRule know, string p_guid, EditMode editMode)
        //    : this(course, know, p_guid, editMode)
        //{
        //    this._question = question;
        //}
        //public FrmCreateQuestion(SubRule course, SubRule know, string p_guid, EditMode editMode) : this()
        //{
        //    this._course = course;
        //    this._know = know;
        //    _p_guid = p_guid;
        //    this._editMode = editMode;
        //}

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            lblQueType.Text = Global.Instance.QueTypeConvertToString(_queType);
            lblCourseName.Text = this._courseRule.CourseName;

            var knows = WebApiController.Instance
                .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), _courseRule.CourseNo, out _);

            if (!string.IsNullOrEmpty(_knowNo))
            {
                var know = knows.Find(k => k.Key == _knowNo);

                cbKnows.Items.Add(know);

                cbKnows.SelectedIndex = 0;

                cbKnows.Enabled = false;
            }
            else
            {
                cbKnows.Items.AddRange(knows.ToArray());
            }

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

                InitAnswer(_question.QueType, _question.Answer);//加载答案

                txtAnswerJiexi.Text = _question.ResolutionTips;
            }
            else
            {
                _question = new QuestionsInfoModel();
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
            if (_queType == (int)QuestionsType.Danxuan)//单选题
            {
                panAnswerDanxuan.Visible = true;
                panAnswerDuoxuan.Visible = false;
                panAnswerPanduan.Visible = false;

                EnableControl(true);
                txtOptionA.Text = "";
                txtOptionB.Text = "";
            }
            else if (_queType == (int)QuestionsType.Danxuan)//多选
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
            int queType = _queType;
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

            if (!(cbKnows.SelectedItem is KVModel))
            {
                MessageBox.Show("请选择知识点！");
                return;
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

            if (_editMode == EditMode.Create)
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
                if (!string.IsNullOrEmpty(queName))
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
                            return;
                        }
                        else
                        {
                            Task.Factory.StartNew(SaveQuestion);
                        }
                    }
                    else
                    {
                        MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                bool resultBool = WebApiController.Instance
                    .EditQuestion(Global.Instance.CurrentSpecialtyID.ToString(), _question, out bool success, out string message);

                if (resultBool)
                {
                    if (success)
                    {
                        MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }


        private void SaveQuestion()
        {
            if (cbKnows.SelectedItem is KVModel kv)
            {
                _ = QuestionController.Instance.SaveQuestions(_p_guid, _courseRule.CourseNo, kv.Key, _question);
            }
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
                _knowNo = kv.Key;
            }
        }
    }
}
