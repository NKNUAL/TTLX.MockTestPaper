namespace TTLX.MockTestPaper_V2
{
    partial class FrmQuestionImg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuestionImg));
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnChoosePic = new System.Windows.Forms.Button();
            this.btnDelImg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(12, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(300, 300);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 12F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(226, 319);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(86, 31);
            this.btnConfirm.TabIndex = 26;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnChoosePic
            // 
            this.btnChoosePic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0)))));
            this.btnChoosePic.FlatAppearance.BorderSize = 0;
            this.btnChoosePic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoosePic.Font = new System.Drawing.Font("宋体", 12F);
            this.btnChoosePic.ForeColor = System.Drawing.Color.White;
            this.btnChoosePic.Location = new System.Drawing.Point(12, 319);
            this.btnChoosePic.Name = "btnChoosePic";
            this.btnChoosePic.Size = new System.Drawing.Size(86, 31);
            this.btnChoosePic.TabIndex = 27;
            this.btnChoosePic.Text = "选择图片";
            this.btnChoosePic.UseVisualStyleBackColor = false;
            this.btnChoosePic.Click += new System.EventHandler(this.btnChoosePic_Click);
            // 
            // btnDelImg
            // 
            this.btnDelImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelImg.BackColor = System.Drawing.Color.Red;
            this.btnDelImg.FlatAppearance.BorderSize = 0;
            this.btnDelImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelImg.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDelImg.ForeColor = System.Drawing.Color.White;
            this.btnDelImg.Location = new System.Drawing.Point(104, 319);
            this.btnDelImg.Name = "btnDelImg";
            this.btnDelImg.Size = new System.Drawing.Size(84, 31);
            this.btnDelImg.TabIndex = 28;
            this.btnDelImg.Text = "删除图片";
            this.btnDelImg.UseVisualStyleBackColor = false;
            this.btnDelImg.Click += new System.EventHandler(this.btnDelImg_Click);
            // 
            // FrmQuestionImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 362);
            this.Controls.Add(this.btnDelImg);
            this.Controls.Add(this.btnChoosePic);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.picBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmQuestionImg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图片";
            this.Load += new System.EventHandler(this.FrmQuestionImg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnChoosePic;
        private System.Windows.Forms.Button btnDelImg;
    }
}