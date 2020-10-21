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
    public partial class FrmCreateQuestion_Nurse_A3 : Form
    {


        private FrmCreateQuestion_Nurse_A3()
        {
            InitializeComponent();
        }

        public PutQuestionA_Model _queModel;
        FrmQuestion_Nurse _frmQuestion;

        NurseQuestionRule _rule;

        string _p_guid;
        EditMode _editMode;

        int queType = 1;//默认单选题

        int editType;


        /// <summary>
        /// 新增题目
        /// </summary>
        /// <param name="p_guid"></param>
        /// <param name="editMode"></param>
        public FrmCreateQuestion_Nurse_A3(FrmQuestion_Nurse frmQuestion, NurseQuestionRule rule, string p_guid, EditMode editMode)
            : this()
        {
            _p_guid = p_guid;
            _editMode = editMode;
            _frmQuestion = frmQuestion;
            _rule = rule;

            editType = 1;
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="question"></param>
        /// <param name="p_guid"></param>
        /// <param name="editMode"></param>
        public FrmCreateQuestion_Nurse_A3(FrmQuestion_Nurse frmQuestion, PutQuestionA_Model queModel, NurseQuestionRule rule, string p_guid, EditMode editMode)
            : this()
        {
            _queModel = queModel;
            _p_guid = p_guid;
            _editMode = editMode;
            _frmQuestion = frmQuestion;
            _rule = rule;

            editType = 2;
        }


        private void InitQueView()
        {
            if (_queModel == null)
            {
                _queModel = new PutQuestionA_Model
                {
                    GeneralNo = Guid.NewGuid().GetGuid(),
                    Questions = new List<QuestionsInfoModel2>(),
                    TypeId = _rule.TypeId,
                };
            }

            if (dgvQuestions.Columns.Count == 0)
            {
                dgvQuestions.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "序号",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 10,
                    ReadOnly = true
                });
                dgvQuestions.Columns.Add(new DataGridViewColumn
                {
                    HeaderText = "题目内容",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    FillWeight = 80,
                    ReadOnly = true
                });
                dgvQuestions.Columns.Add(new DataGridViewLinkColumn
                {
                    HeaderText = "操作",
                    FillWeight = 10,
                    ReadOnly = true
                });
                dgvQuestions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvQuestions.EnableHeadersVisualStyles = false;
                dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgvQuestions.RowTemplate.Height = 26;
                dgvQuestions.AllowUserToAddRows = false;
                dgvQuestions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvQuestions.Rows.Clear();

            int index = dgvQuestions.Rows.Add();
            dgvQuestions.Rows[index].Cells[0].Value = $"总题干";
            dgvQuestions.Rows[index].Cells[0].Tag = true;
            if (!string.IsNullOrEmpty(_queModel.GeneralName) || _queModel.NameImg != null)
            {
                dgvQuestions.Rows[index].Cells[1].Value = _queModel.GeneralName;
                dgvQuestions.Rows[index].Cells[2].Value = "修改";
            }
            else
            {
                dgvQuestions.Rows[index].Cells[1].Value = "";
                dgvQuestions.Rows[index].Cells[2].Value = "出题";
            }

            foreach (var que in _queModel.Questions)
            {
                index = dgvQuestions.Rows.Add();
                dgvQuestions.Rows[index].Cells[0].Value = $"第{index}题";
                dgvQuestions.Rows[index].Cells[0].Tag = false;
                dgvQuestions.Rows[index].Cells[1].Value = que.QueContent;
                dgvQuestions.Rows[index].Cells[2].Value = "修改";
                dgvQuestions.Rows[index].Cells[2].Tag = que;
            }


            for (int i = 0; i < _rule.SubQueCount - _queModel.Questions.Count; i++)
            {
                index = dgvQuestions.Rows.Add();
                dgvQuestions.Rows[index].Cells[0].Value = $"第{index}题";
                dgvQuestions.Rows[index].Cells[0].Tag = false;
                dgvQuestions.Rows[index].Cells[1].Value = "";
                dgvQuestions.Rows[index].Cells[2].Value = "出题";
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitQueView();

            ClickDGCell(0);

        }

        private void ClickDGCell(int queIndex)
        {
            lblIndex.Text = dgvQuestions.Rows[queIndex].Cells[0].Value.ToString();

            _currIsGerenalQue = (dgvQuestions.Rows[queIndex].Cells[0].Tag as bool?) ?? false;

            _currQue = dgvQuestions.Rows[queIndex].Cells[2].Tag as QuestionsInfoModel2;

            InitQuestionPanel();
        }



        private void InitCbCourse(string courseNo)
        {
            var courses = WebApiController.Instance
                .GetCourse(Global.Instance.CurrentSpecialtyID.ToString(), out _);

            cbCourses.Items.AddRange(courses.ToArray());

            cbCourses.SelectedItem = courses.Find(x => x.Key == courseNo);
        }

        private void InitCbKnow(string courseNo, string knowNo)
        {
            if (string.IsNullOrEmpty(courseNo))
            {
                cbKnows.Items.Clear();
            }

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
            byte[] queNameImg = _currQue.NameImg;
            byte[] AImg = _currQue.Option0Img;
            byte[] BImg = _currQue.Option1Img;
            byte[] CImg = _currQue.Option2Img;
            byte[] DImg = _currQue.Option3Img;
            string jiexi = txtAnswerJiexi.Text.Trim();
            string standardAnswer = string.Empty;
            int difficultLevel = 1;

            string courseNo = string.Empty;
            string knowNo = string.Empty;

            #region 合法性判断

            if (!(cbCourses.SelectedItem is KVModel kvCourse))
            {
                MessageBox.Show("请选择科目！");
                return;
            }
            else
            {
                courseNo = kvCourse.Key;
            }

            if (!(cbKnows.SelectedItem is KVModel kvKnow))
            {
                MessageBox.Show("请选择知识点！");
                return;
            }
            else
            {
                knowNo = kvKnow.Key;
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

            if (_editMode == EditMode.Create && string.IsNullOrEmpty(_currQue.No))
                _currQue.No = Guid.NewGuid().GetGuid();

            _currQue.DifficultLevel = difficultLevel;
            _currQue.QueType = queType;
            _currQue.QueContent = queName;
            _currQue.Option0 = optionA;
            _currQue.Option1 = optionB;
            _currQue.Option2 = optionC;
            _currQue.Option3 = optionD;
            _currQue.NameImg = queNameImg;
            _currQue.Option0Img = AImg;
            _currQue.Option1Img = BImg;
            _currQue.Option2Img = CImg;
            _currQue.Option3Img = DImg;
            _currQue.Answer = standardAnswer;
            _currQue.ResolutionTips = jiexi;
            _currQue.CourseNo = courseNo;
            _currQue.KnowNo = knowNo;

            if (_editMode == EditMode.Create)
            {
                if (_queModel.Questions.Find(q => q.No == _currQue.No) == null)
                {
                    _queModel.Questions.Add(_currQue);
                }
                int queIndex = CheckIsFinish();
                if (queIndex == -1)
                {
                    Task.Factory.StartNew(SaveQuestion);
                }
                else
                {
                    InitQueView();
                    ClickDGCell(queIndex);
                    MessageBox.Show("保存成功！请继续出题", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {

                bool resultBool = WebApiController.Instance
                    .EditQuestion_Nurse(Global.Instance.CurrentSpecialtyID.ToString(), new PutQuestionA_Model
                    {
                        TypeId = _rule.TypeId,
                        Questions = new List<QuestionsInfoModel2> { _currQue }
                    }, out bool success, out string message);

                if (resultBool)
                {
                    if (success)
                    {
                        InitQueView();
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
            WebApiController.Instance
                .SaveLocalQuestion_Nrese(_p_guid, Global.Instance.CurrentSpecialtyID, editType, _queModel);
        }

        private void btnImg_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            byte[] imgByte;
            switch (btn.Name)
            {
                case "btnImgContent":
                    imgByte = _currQue.NameImg;
                    FrmQuestionImg frmContent = new FrmQuestionImg(imgByte);
                    frmContent.ShowDialog();
                    _currQue.NameImg = frmContent.imgBytes;
                    break;
                case "btnImgA":
                    imgByte = _currQue.Option0Img;
                    FrmQuestionImg frmA = new FrmQuestionImg(imgByte);
                    frmA.ShowDialog();
                    _currQue.Option0Img = frmA.imgBytes;
                    break;
                case "btnImgB":
                    imgByte = _currQue.Option1Img;
                    FrmQuestionImg frmB = new FrmQuestionImg(imgByte);
                    frmB.ShowDialog();
                    _currQue.Option1Img = frmB.imgBytes;
                    break;
                case "btnImgC":
                    imgByte = _currQue.Option2Img;
                    FrmQuestionImg frmC = new FrmQuestionImg(imgByte);
                    frmC.ShowDialog();
                    _currQue.Option2Img = frmC.imgBytes;
                    break;
                case "btnImgD":
                    imgByte = _currQue.Option3Img;
                    FrmQuestionImg frmD = new FrmQuestionImg(imgByte);
                    frmD.ShowDialog();
                    _currQue.Option3Img = frmD.imgBytes;
                    break;
                case "btnGerenalImg":
                    imgByte = _queModel.NameImg;
                    FrmQuestionImg frmGerenal = new FrmQuestionImg(imgByte);
                    frmGerenal.ShowDialog();
                    _queModel.NameImg = frmGerenal.imgBytes;
                    break;
                default:
                    break;
            }

        }


        private void cbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCourses.SelectedItem is KVModel kv)
            {
                var knows = WebApiController.Instance
                    .GetKnows(Global.Instance.CurrentSpecialtyID.ToString(), kv.Key, out _);
                cbKnows.Items.Clear();
                cbKnows.Items.AddRange(knows.ToArray());
            }
        }


        QuestionsInfoModel2 _currQue;
        bool _currIsGerenalQue;
        private void dgvQuestions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                ClickDGCell(e.RowIndex);
            }
        }
        /// <summary>
        /// 加载出题界面
        /// </summary>
        private void InitQuestionPanel()
        {
            if (_currIsGerenalQue)
            {
                panel_General.Show();

                tbGerenalName.Text = _queModel.GeneralName;
            }
            else
            {
                panel_General.Hide();

                InitQueType();

                if (_currQue == null)
                {
                    _currQue = new QuestionsInfoModel2
                    {
                        QueType = queType
                    };
                }

                tbQueName.Text = _currQue.QueContent;
                txtOptionA.Text = _currQue.Option0;
                txtOptionB.Text = _currQue.Option1;
                txtOptionC.Text = _currQue.Option2;
                txtOptionD.Text = _currQue.Option3;

                if (_currQue.DifficultLevel == 1)
                    this.rbtnDifficultLevel1.Checked = true;
                else if (_currQue.DifficultLevel == 2)
                    this.rbtnDifficultLevel2.Checked = true;
                else if (_currQue.DifficultLevel == 3)
                    this.rbtnDifficultLevel3.Checked = true;

                txtAnswerJiexi.Text = _currQue.ResolutionTips;

                InitAnswer(_currQue.QueType, _currQue.Answer);//加载答案

                InitCbCourse(_currQue.CourseNo);

                InitCbKnow(_currQue.CourseNo, _currQue.KnowNo);

            }
        }

        private void btnSaveFeneral_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbGerenalName.Text) && _queModel.NameImg == null)
            {
                MessageBox.Show("请输入题目内容在保存！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _queModel.GeneralName = tbGerenalName.Text;



            if (_editMode == EditMode.Create)
            {
                int queIndex = CheckIsFinish();
                if (queIndex == -1)
                {
                    Task.Factory.StartNew(SaveQuestion);

                    
                }
                else
                {
                    InitQueView();
                    ClickDGCell(queIndex);
                    MessageBox.Show("保存成功！请继续出题", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                bool resultBool = WebApiController.Instance
                    .EditQuestion_Nurse(Global.Instance.CurrentSpecialtyID.ToString(), new PutQuestionA_Model
                    {
                        TypeId = _rule.TypeId,
                        GeneralName = _queModel.GeneralName,
                        NameImg = _queModel.NameImg,
                        GeneralNo = _queModel.GeneralNo,
                    }, out bool success, out string message);

                if (resultBool)
                {
                    if (success)
                    {
                        InitQueView();
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


        /// <summary>
        /// 检查题目是否出完
        /// </summary>
        /// <returns></returns>
        private int CheckIsFinish()
        {
            if (string.IsNullOrEmpty(_queModel.GeneralName) && _queModel.NameImg == null)
            {
                return 0;
            }
            if (_queModel.Questions == null)
            {
                return 1;
            }
            else
            {
                if (_rule.SubQueCount == _queModel.Questions.Count)
                    return -1;

                return _queModel.Questions.Count + 1;

            }
        }



    }
}
