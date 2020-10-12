using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTLX.Common;

namespace TTLX.MockTestPaper_V2
{
    public partial class FrmQuestionImg : Form
    {
        public FrmQuestionImg()
        {
            InitializeComponent();
        }

        public FrmQuestionImg(byte[] imgBytes) : this()
        {
            if (imgBytes == null || imgBytes.Length == 0)
                this.imgBytes = null;
            else
            {
                this.imgBytes = imgBytes;
            }
        }

        public byte[] imgBytes { get; set; }

        private void btnChoosePic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "(.图片文件)|*.png;*/jpg"
            };
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            string fileName = openFile.FileName;

            picBox.LoadAsync(fileName);

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (picBox.Image != null)
            {
                imgBytes = FileHelper.Instance.Image2Byte(picBox.Image);

                picBox.Image.Dispose();
                picBox.Image = null;
            }
            this.Close();
        }

        private void FrmQuestionImg_Load(object sender, EventArgs e)
        {
            if (imgBytes != null)
            {
                picBox.Image = FileHelper.Instance.Byte2Image(imgBytes);
            }
        }

        private void btnDelImg_Click(object sender, EventArgs e)
        {
            imgBytes = null;
            if (picBox.Image != null)
            {
                picBox.Image.Dispose();
                picBox.Image = null;
            }
        }
    }
}
