namespace SearchQuestions
{
    partial class resultsBox
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
            this.searchBox = new System.Windows.Forms.TextBox();
            this.text1 = new System.Windows.Forms.Label();
            this.allQuestions = new System.Windows.Forms.RichTextBox();
            this.pickFile = new System.Windows.Forms.Button();
            this.answer = new System.Windows.Forms.TextBox();
            this.testing = new System.Windows.Forms.Button();
            this.testBox = new System.Windows.Forms.RichTextBox();
            this.infoText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(59, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(108, 20);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged_1);
            // 
            // text1
            // 
            this.text1.AutoSize = true;
            this.text1.Location = new System.Drawing.Point(12, 15);
            this.text1.Name = "text1";
            this.text1.Size = new System.Drawing.Size(41, 13);
            this.text1.TabIndex = 1;
            this.text1.Text = "Search";
            // 
            // allQuestions
            // 
            this.allQuestions.Location = new System.Drawing.Point(12, 38);
            this.allQuestions.Name = "allQuestions";
            this.allQuestions.Size = new System.Drawing.Size(676, 268);
            this.allQuestions.TabIndex = 2;
            this.allQuestions.Text = "";
            // 
            // pickFile
            // 
            this.pickFile.Location = new System.Drawing.Point(382, 12);
            this.pickFile.Name = "pickFile";
            this.pickFile.Size = new System.Drawing.Size(79, 20);
            this.pickFile.TabIndex = 3;
            this.pickFile.Text = "Browse";
            this.pickFile.UseVisualStyleBackColor = true;
            this.pickFile.Click += new System.EventHandler(this.pickFile_Click);
            // 
            // answer
            // 
            this.answer.Location = new System.Drawing.Point(173, 12);
            this.answer.Name = "answer";
            this.answer.Size = new System.Drawing.Size(203, 20);
            this.answer.TabIndex = 4;
            // 
            // testing
            // 
            this.testing.Location = new System.Drawing.Point(501, 12);
            this.testing.Name = "testing";
            this.testing.Size = new System.Drawing.Size(43, 23);
            this.testing.TabIndex = 5;
            this.testing.Text = "button1";
            this.testing.UseVisualStyleBackColor = true;
            this.testing.Click += new System.EventHandler(this.testing_Click);
            // 
            // testBox
            // 
            this.testBox.Location = new System.Drawing.Point(12, 313);
            this.testBox.Name = "testBox";
            this.testBox.Size = new System.Drawing.Size(675, 38);
            this.testBox.TabIndex = 6;
            this.testBox.Text = "";
            // 
            // infoText
            // 
            this.infoText.AutoSize = true;
            this.infoText.Location = new System.Drawing.Point(18, 362);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(0, 13);
            this.infoText.TabIndex = 7;
            // 
            // resultsBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 384);
            this.Controls.Add(this.infoText);
            this.Controls.Add(this.testBox);
            this.Controls.Add(this.testing);
            this.Controls.Add(this.answer);
            this.Controls.Add(this.pickFile);
            this.Controls.Add(this.allQuestions);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.searchBox);
            this.Name = "resultsBox";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label text1;
        private System.Windows.Forms.RichTextBox allQuestions;
        private System.Windows.Forms.Button pickFile;
        private System.Windows.Forms.TextBox answer;
        private System.Windows.Forms.Button testing;
        private System.Windows.Forms.RichTextBox testBox;
        private System.Windows.Forms.Label infoText;
    }
}

