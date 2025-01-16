namespace WinThumbsPreloader
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.CloseButton = new System.Windows.Forms.Button();
            this.HelpBox = new System.Windows.Forms.GroupBox();
            this.HelpTextBox = new System.Windows.Forms.TextBox();
            this.HelpButton = new System.Windows.Forms.Button();
            this.FAQButton = new System.Windows.Forms.Button();
            this.HelpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CloseButton.Location = new System.Drawing.Point(297, 324);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 25);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // HelpBox
            // 
            this.HelpBox.BackColor = System.Drawing.SystemColors.Window;
            this.HelpBox.Controls.Add(this.FAQButton);
            this.HelpBox.Controls.Add(this.HelpButton);
            this.HelpBox.Controls.Add(this.HelpTextBox);
            this.HelpBox.Location = new System.Drawing.Point(12, 12);
            this.HelpBox.Name = "HelpBox";
            this.HelpBox.Size = new System.Drawing.Size(360, 306);
            this.HelpBox.TabIndex = 3;
            this.HelpBox.TabStop = false;
            this.HelpBox.Text = "Help / FAQ";
            // 
            // HelpTextBox
            // 
            this.HelpTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.HelpTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpTextBox.Location = new System.Drawing.Point(6, 19);
            this.HelpTextBox.Multiline = true;
            this.HelpTextBox.Name = "HelpTextBox";
            this.HelpTextBox.ReadOnly = true;
            this.HelpTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HelpTextBox.Size = new System.Drawing.Size(348, 250);
            this.HelpTextBox.TabIndex = 0;
            this.HelpTextBox.Text = resources.GetString("HelpTextBox.Text");
            this.HelpTextBox.TextChanged += new System.EventHandler(this.HelpTextBox_TextChanged);
            // 
            // HelpButton
            // 
            this.HelpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HelpButton.Location = new System.Drawing.Point(6, 275);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(75, 25);
            this.HelpButton.TabIndex = 4;
            this.HelpButton.Text = "Help";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // FAQButton
            // 
            this.FAQButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FAQButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FAQButton.Location = new System.Drawing.Point(87, 275);
            this.FAQButton.Name = "FAQButton";
            this.FAQButton.Size = new System.Drawing.Size(75, 25);
            this.FAQButton.TabIndex = 5;
            this.FAQButton.Text = "FAQ";
            this.FAQButton.UseVisualStyleBackColor = true;
            this.FAQButton.Click += new System.EventHandler(this.FAQButton_Click);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.HelpBox);
            this.Controls.Add(this.CloseButton);
            this.MaximizeBox = false;
            this.Name = "HelpForm";
            this.Text = "WinThumbsPreloader - Help / FAQ";
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.HelpBox.ResumeLayout(false);
            this.HelpBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.GroupBox HelpBox;
        private System.Windows.Forms.TextBox HelpTextBox;
        private System.Windows.Forms.Button FAQButton;
        private System.Windows.Forms.Button HelpButton;
    }
}