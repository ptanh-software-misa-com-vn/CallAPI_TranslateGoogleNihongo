namespace Anh.Translate
{
    partial class Form1
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
            this.btnTranslateCJKUnifiedIdeographs = new System.Windows.Forms.Button();
            this.btnTEST = new System.Windows.Forms.Button();
            this.btnGetMulti = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnTranslateCJKUnifiedIdeographs
            // 
            this.btnTranslateCJKUnifiedIdeographs.Location = new System.Drawing.Point(24, 25);
            this.btnTranslateCJKUnifiedIdeographs.Name = "btnTranslateCJKUnifiedIdeographs";
            this.btnTranslateCJKUnifiedIdeographs.Size = new System.Drawing.Size(213, 23);
            this.btnTranslateCJKUnifiedIdeographs.TabIndex = 0;
            this.btnTranslateCJKUnifiedIdeographs.Text = "Translate CJK Unified Ideographs";
            this.btnTranslateCJKUnifiedIdeographs.UseVisualStyleBackColor = true;
            this.btnTranslateCJKUnifiedIdeographs.Click += new System.EventHandler(this.btnTranslateCJKUnifiedIdeographs_Click);
            // 
            // btnTEST
            // 
            this.btnTEST.Location = new System.Drawing.Point(179, 70);
            this.btnTEST.Name = "btnTEST";
            this.btnTEST.Size = new System.Drawing.Size(75, 23);
            this.btnTEST.TabIndex = 1;
            this.btnTEST.Text = "TEST API";
            this.btnTEST.UseVisualStyleBackColor = true;
            this.btnTEST.Click += new System.EventHandler(this.btnTEST_Click);
            // 
            // btnGetMulti
            // 
            this.btnGetMulti.Location = new System.Drawing.Point(266, 25);
            this.btnGetMulti.Name = "btnGetMulti";
            this.btnGetMulti.Size = new System.Drawing.Size(75, 23);
            this.btnGetMulti.TabIndex = 1;
            this.btnGetMulti.Text = "TEST APIs";
            this.btnGetMulti.UseVisualStyleBackColor = true;
            this.btnGetMulti.Click += new System.EventHandler(this.btnGetMulti_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("MS Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.textBox1.Location = new System.Drawing.Point(24, 70);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 67);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(260, 72);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(142, 65);
            this.textBox2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 242);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGetMulti);
            this.Controls.Add(this.btnTEST);
            this.Controls.Add(this.btnTranslateCJKUnifiedIdeographs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTranslateCJKUnifiedIdeographs;
        private System.Windows.Forms.Button btnTEST;
        private System.Windows.Forms.Button btnGetMulti;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

