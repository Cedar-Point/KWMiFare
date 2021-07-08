
namespace KWMiFare
{
    partial class Main
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
            this.MainListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // MainListBox
            // 
            this.MainListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainListBox.FormattingEnabled = true;
            this.MainListBox.IntegralHeight = false;
            this.MainListBox.Location = new System.Drawing.Point(0, 0);
            this.MainListBox.Margin = new System.Windows.Forms.Padding(0);
            this.MainListBox.Name = "MainListBox";
            this.MainListBox.Size = new System.Drawing.Size(438, 161);
            this.MainListBox.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 161);
            this.Controls.Add(this.MainListBox);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyboard Wedge - MiFare";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox MainListBox;
    }
}

