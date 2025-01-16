namespace WinThumbsPreloader
{
    partial class ExtensionsForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.DefaultButton = new System.Windows.Forms.Button();
            this.ExtensionsTextBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.ExtensionsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ClearButton);
            this.groupBox1.Controls.Add(this.DefaultButton);
            this.groupBox1.Controls.Add(this.ExtensionsTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 236);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extensions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(205, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Drag and Drop Supported";
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ClearButton.Location = new System.Drawing.Point(87, 204);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 25);
            this.ClearButton.TabIndex = 9;
            this.ClearButton.Text = "Clear";
            this.ExtensionsToolTip.SetToolTip(this.ClearButton, "Empties the extensions list to allow \r\nthe preloader to process all extensions");
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // DefaultButton
            // 
            this.DefaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DefaultButton.Location = new System.Drawing.Point(6, 204);
            this.DefaultButton.Name = "DefaultButton";
            this.DefaultButton.Size = new System.Drawing.Size(75, 25);
            this.DefaultButton.TabIndex = 8;
            this.DefaultButton.Text = "Default";
            this.ExtensionsToolTip.SetToolTip(this.DefaultButton, "Resets the extensions list to the default configuration");
            this.DefaultButton.UseVisualStyleBackColor = true;
            this.DefaultButton.Click += new System.EventHandler(this.DefaultButton_Click);
            // 
            // ExtensionsTextBox
            // 
            this.ExtensionsTextBox.AllowDrop = true;
            this.ExtensionsTextBox.Location = new System.Drawing.Point(6, 20);
            this.ExtensionsTextBox.Multiline = true;
            this.ExtensionsTextBox.Name = "ExtensionsTextBox";
            this.ExtensionsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExtensionsTextBox.Size = new System.Drawing.Size(328, 178);
            this.ExtensionsTextBox.TabIndex = 0;
            this.ExtensionsTextBox.Text = "avif,bmp,gif,heic,heif,jpg,jpeg,mkv,mov,mp4,png,svg,tif,tiff,webp";
            this.ExtensionsTextBox.TextChanged += new System.EventHandler(this.ExtensionsTextBox_TextChanged);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CloseButton.Location = new System.Drawing.Point(277, 255);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 25);
            this.CloseButton.TabIndex = 7;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(12, 255);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 25);
            this.CopyButton.TabIndex = 8;
            this.CopyButton.Text = "CLI Copy";
            this.ExtensionsToolTip.SetToolTip(this.CopyButton, "Copies the extension list with commas and without spaces and \r\nnewlines for use i" +
        "n the command line when using the -e argument");
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // ExtensionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 291);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ExtensionsForm";
            this.Text = "WinThumbsPreloader - Extensions";
            this.Load += new System.EventHandler(this.ExtensionsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button DefaultButton;
        private System.Windows.Forms.TextBox ExtensionsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.ToolTip ExtensionsToolTip;
    }
}