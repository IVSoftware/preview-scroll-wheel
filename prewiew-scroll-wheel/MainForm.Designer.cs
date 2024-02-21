namespace prewiew_scroll_wheel
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer = new SplitContainer();
            richTextBox = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.BackColor = Color.Azure;
            splitContainer.Panel2.Controls.Add(richTextBox);
            splitContainer.Panel2.Padding = new Padding(10);
            splitContainer.Size = new Size(478, 244);
            splitContainer.SplitterDistance = 158;
            splitContainer.TabIndex = 0;
            // 
            // richTextBox
            // 
            richTextBox.Location = new Point(10, 10);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(130, 122);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 244);
            Controls.Add(splitContainer);
            Name = "MainForm";
            Text = "MainForm";
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer;
        private RichTextBox richTextBox;
    }
}
