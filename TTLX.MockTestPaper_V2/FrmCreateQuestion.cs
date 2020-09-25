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
using TTLX.Controller.ResposeModel;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmCreateQuestion : Form
    {


        private FrmCreateQuestion()
        {
            InitializeComponent();
        }

        public QuestionsInfoModel _question { get; set; }
        SubRule _course;
        SubRule _know;
        string _p_guid;


        public FrmCreateQuestion(QuestionsInfoModel question, SubRule course, SubRule know, string p_guid)
            : this(course, know, p_guid)
        {
            this._question = question;
        }
        public FrmCreateQuestion(SubRule course, SubRule know, string p_guid) : this()
        {
            this._course = course;
            this._know = know;
            _p_guid = p_guid;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cbQueType.Items.AddRange(new[] { "单选题", "多选题", "判断题" });

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            lblCourseName.Text = this._course.Name;
            lblKnowName.Text = this._know.Name;
            if (_question != null)
            {
                cbQueType.SelectedIndex = _question.QueType - 1;
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


        private void cbQueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbQueType.SelectedIndex == 0)//单选题
            {
                panAnswerDanxuan.Visible = true;
                panAnswerDuoxuan.Visible = false;
                panAnswerPanduan.Visible = false;
            }
            else if (cbQueType.SelectedIndex == 1)//多选
            {
                panAnswerDanxuan.Visible = false;
                panAnswerDuoxuan.Visible = true;
                panAnswerPanduan.Visible = false;
            }
            else//判断
            {
                panAnswerDanxuan.Visible = false;
                panAnswerDuoxuan.Visible = false;
                panAnswerPanduan.Visible = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int queType = cbQueType.SelectedIndex + 1;
            string queName = tbQueName.Text.Trim();
            string optionA = txtOptionA.Text.Trim();
            string optionB = txtOptionA.Text.Trim();
            string optionC = txtOptionA.Text.Trim();
            string optionD = txtOptionA.Text.Trim();
            byte[] queNameImg = _question.NameImg;
            byte[] AImg = _question.Option0Img;
            byte[] BImg = _question.Option1Img;
            byte[] CImg = _question.Option2Img;
            byte[] DImg = _question.Option3Img;
            string jiexi = txtAnswerJiexi.Text.Trim();
            string standardAnswer = string.Empty;
            int difficultLevel = 1;

            #region 合法性判断

            if (cbQueType.SelectedItem == null)
            {
                MessageBox.Show("请选择题目类型!");
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

            _question.No = Guid.NewGuid().GetGuid();
            _question.DifficultLevel = difficultLevel;
            _question.QueType = difficultLevel;
            _question.QueContent = queName;
            _question.Option0 = optionA;
            _question.Option1 = optionB;
            _question.Option2 = optionC;
            _question.Option3 = optionD;
            _question.NameImg = queNameImg;
            _question.Option0Img = DImg;
            _question.Option1Img = DImg;
            _question.Option2Img = DImg;
            _question.Option3Img = DImg;
            _question.Answer = standardAnswer;
            _question.ResolutionTips = jiexi;

            DialogResult = DialogResult.OK;

            Task.Factory.StartNew(SaveQuestion);
        }


        private void SaveQuestion()
        {
            _ = QuestionController.Instance.SaveQuestions(_p_guid, _course.No, _know.No, _question);
        }


        private void btnImg_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            byte[] imgByte = null;
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
                    _question.NameImg = frmA.imgBytes;
                    break;
                case "btnImgB":
                    imgByte = _question.Option1Img;
                    FrmQuestionImg frmB = new FrmQuestionImg(imgByte);
                    frmB.ShowDialog();
                    _question.NameImg = frmB.imgBytes;
                    break;
                case "btnImgC":
                    imgByte = _question.Option2Img;
                    FrmQuestionImg frmC = new FrmQuestionImg(imgByte);
                    frmC.ShowDialog();
                    _question.NameImg = frmC.imgBytes;
                    break;
                case "btnImgD":
                    imgByte = _question.Option3Img;
                    FrmQuestionImg frmD = new FrmQuestionImg(imgByte);
                    frmD.ShowDialog();
                    _question.NameImg = frmD.imgBytes;
                    break;
                default:
                    break;
            }

        }
    }
}
