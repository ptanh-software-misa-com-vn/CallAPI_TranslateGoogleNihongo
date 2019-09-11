namespace Anh.DB_definition_diagram__WRS
{
    partial class TranslateWorkBook2
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtExcelName = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnPre = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbSheetName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbSelectSheet = new System.Windows.Forms.RadioButton();
            this.rbAllSheet = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbRangeSheet = new System.Windows.Forms.RadioButton();
            this.numF = new System.Windows.Forms.NumericUpDown();
            this.numT = new System.Windows.Forms.NumericUpDown();
            this.to = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numT)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtExcelName
            // 
            this.txtExcelName.Location = new System.Drawing.Point(104, 12);
            this.txtExcelName.Name = "txtExcelName";
            this.txtExcelName.ReadOnly = true;
            this.txtExcelName.Size = new System.Drawing.Size(359, 19);
            this.txtExcelName.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(36, 12);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(61, 12);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "File name：";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(196, 114);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(75, 23);
            this.btnPre.TabIndex = 9;
            this.btnPre.Text = "Translate";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 165);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(474, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Step = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // cbSheetName
            // 
            this.cbSheetName.FormattingEnabled = true;
            this.cbSheetName.Location = new System.Drawing.Point(104, 74);
            this.cbSheetName.Name = "cbSheetName";
            this.cbSheetName.Size = new System.Drawing.Size(121, 20);
            this.cbSheetName.TabIndex = 5;
            this.cbSheetName.DropDown += new System.EventHandler(this.cbSheetName_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sheet name：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // rbSelectSheet
            // 
            this.rbSelectSheet.AutoSize = true;
            this.rbSelectSheet.Checked = true;
            this.rbSelectSheet.Location = new System.Drawing.Point(3, 3);
            this.rbSelectSheet.Name = "rbSelectSheet";
            this.rbSelectSheet.Size = new System.Drawing.Size(70, 16);
            this.rbSelectSheet.TabIndex = 0;
            this.rbSelectSheet.TabStop = true;
            this.rbSelectSheet.Text = "Specified";
            this.rbSelectSheet.UseVisualStyleBackColor = true;
            this.rbSelectSheet.CheckedChanged += new System.EventHandler(this.rbSelectSheet_CheckedChanged);
            // 
            // rbAllSheet
            // 
            this.rbAllSheet.AutoSize = true;
            this.rbAllSheet.Location = new System.Drawing.Point(203, 3);
            this.rbAllSheet.Name = "rbAllSheet";
            this.rbAllSheet.Size = new System.Drawing.Size(70, 16);
            this.rbAllSheet.TabIndex = 1;
            this.rbAllSheet.TabStop = true;
            this.rbAllSheet.Text = "All Sheet";
            this.rbAllSheet.UseVisualStyleBackColor = true;
            this.rbAllSheet.CheckedChanged += new System.EventHandler(this.rbAllSheet_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sheet selection：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbRangeSheet);
            this.panel1.Controls.Add(this.rbSelectSheet);
            this.panel1.Controls.Add(this.rbAllSheet);
            this.panel1.Location = new System.Drawing.Point(104, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 28);
            this.panel1.TabIndex = 3;
            // 
            // rbRangeSheet
            // 
            this.rbRangeSheet.AutoSize = true;
            this.rbRangeSheet.Location = new System.Drawing.Point(112, 3);
            this.rbRangeSheet.Name = "rbRangeSheet";
            this.rbRangeSheet.Size = new System.Drawing.Size(55, 16);
            this.rbRangeSheet.TabIndex = 2;
            this.rbRangeSheet.TabStop = true;
            this.rbRangeSheet.Text = "Range";
            this.rbRangeSheet.UseVisualStyleBackColor = true;
            this.rbRangeSheet.CheckedChanged += new System.EventHandler(this.rbRangeSheet_CheckedChanged);
            // 
            // numF
            // 
            this.numF.Enabled = false;
            this.numF.Location = new System.Drawing.Point(241, 75);
            this.numF.Name = "numF";
            this.numF.Size = new System.Drawing.Size(46, 19);
            this.numF.TabIndex = 6;
            this.numF.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numT
            // 
            this.numT.Enabled = false;
            this.numT.Location = new System.Drawing.Point(323, 75);
            this.numT.Name = "numT";
            this.numT.Size = new System.Drawing.Size(46, 19);
            this.numT.TabIndex = 8;
            this.numT.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // to
            // 
            this.to.AutoSize = true;
            this.to.Location = new System.Drawing.Point(298, 79);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(15, 12);
            this.to.TabIndex = 7;
            this.to.Text = "to";
            // 
            // TranslateWorkBook2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 187);
            this.Controls.Add(this.to);
            this.Controls.Add(this.numT);
            this.Controls.Add(this.numF);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSheetName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.txtExcelName);
            this.Controls.Add(this.btnPre);
            this.Name = "TranslateWorkBook2";
            this.Text = "Translate WorkBook(.xls) [ja->en]";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtExcelName;
        private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button btnPre;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ComboBox cbSheetName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton rbSelectSheet;
		private System.Windows.Forms.RadioButton rbAllSheet;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.RadioButton rbRangeSheet;
		private System.Windows.Forms.NumericUpDown numF;
		private System.Windows.Forms.NumericUpDown numT;
		private System.Windows.Forms.Label to;
	}
}

