namespace AgeCalculation.Forms
{
    partial class XmlForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonBGColor = new System.Windows.Forms.Button();
            this.buttonFont = new System.Windows.Forms.Button();
            this.buttonFontName = new System.Windows.Forms.Button();
            this.buttonSizeFont = new System.Windows.Forms.Button();
            this.buttonBoxStyle = new System.Windows.Forms.Button();
            this.buttonFontStyle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 31);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(417, 48);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Lorem ipsum dolor";
            // 
            // buttonBGColor
            // 
            this.buttonBGColor.Location = new System.Drawing.Point(31, 109);
            this.buttonBGColor.Name = "buttonBGColor";
            this.buttonBGColor.Size = new System.Drawing.Size(95, 37);
            this.buttonBGColor.TabIndex = 1;
            this.buttonBGColor.Text = "ColorBG";
            this.buttonBGColor.UseVisualStyleBackColor = true;
            this.buttonBGColor.Click += new System.EventHandler(this.buttonBGColor_Click);
            // 
            // buttonFont
            // 
            this.buttonFont.Location = new System.Drawing.Point(31, 179);
            this.buttonFont.Name = "buttonFont";
            this.buttonFont.Size = new System.Drawing.Size(95, 37);
            this.buttonFont.TabIndex = 1;
            this.buttonFont.Text = "ColorFont";
            this.buttonFont.UseVisualStyleBackColor = true;
            this.buttonFont.Click += new System.EventHandler(this.buttonFont_Click);
            // 
            // buttonFontName
            // 
            this.buttonFontName.Location = new System.Drawing.Point(189, 179);
            this.buttonFontName.Name = "buttonFontName";
            this.buttonFontName.Size = new System.Drawing.Size(95, 37);
            this.buttonFontName.TabIndex = 1;
            this.buttonFontName.Text = "Font";
            this.buttonFontName.UseVisualStyleBackColor = true;
            this.buttonFontName.Click += new System.EventHandler(this.buttonFontName_Click);
            // 
            // buttonSizeFont
            // 
            this.buttonSizeFont.Location = new System.Drawing.Point(189, 109);
            this.buttonSizeFont.Name = "buttonSizeFont";
            this.buttonSizeFont.Size = new System.Drawing.Size(95, 37);
            this.buttonSizeFont.TabIndex = 1;
            this.buttonSizeFont.Text = "FontSize";
            this.buttonSizeFont.UseVisualStyleBackColor = true;
            this.buttonSizeFont.Click += new System.EventHandler(this.buttonSizeFont_Click);
            // 
            // buttonBoxStyle
            // 
            this.buttonBoxStyle.Location = new System.Drawing.Point(353, 179);
            this.buttonBoxStyle.Name = "buttonBoxStyle";
            this.buttonBoxStyle.Size = new System.Drawing.Size(95, 37);
            this.buttonBoxStyle.TabIndex = 1;
            this.buttonBoxStyle.Text = "BoxStyle";
            this.buttonBoxStyle.UseVisualStyleBackColor = true;
            this.buttonBoxStyle.Click += new System.EventHandler(this.buttonBoxStyle_Click);
            // 
            // buttonFontStyle
            // 
            this.buttonFontStyle.Location = new System.Drawing.Point(353, 109);
            this.buttonFontStyle.Name = "buttonFontStyle";
            this.buttonFontStyle.Size = new System.Drawing.Size(95, 37);
            this.buttonFontStyle.TabIndex = 1;
            this.buttonFontStyle.Text = "FontStyle";
            this.buttonFontStyle.UseVisualStyleBackColor = true;
            this.buttonFontStyle.Click += new System.EventHandler(this.buttonFontStyle_Click);
            // 
            // XmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 240);
            this.Controls.Add(this.buttonFontStyle);
            this.Controls.Add(this.buttonSizeFont);
            this.Controls.Add(this.buttonBoxStyle);
            this.Controls.Add(this.buttonFontName);
            this.Controls.Add(this.buttonFont);
            this.Controls.Add(this.buttonBGColor);
            this.Controls.Add(this.textBox1);
            this.Name = "XmlForm";
            this.Text = "XmlForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XmlForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.XmlForm_FormClosed);
            this.Load += new System.EventHandler(this.XmlForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonBGColor;
        private System.Windows.Forms.Button buttonFont;
        private System.Windows.Forms.Button buttonFontName;
        private System.Windows.Forms.Button buttonSizeFont;
        private System.Windows.Forms.Button buttonBoxStyle;
        private System.Windows.Forms.Button buttonFontStyle;
    }
}