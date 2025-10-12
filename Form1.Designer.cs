namespace PUBG_DistanceCalculation
{
    partial class PUBG_Dist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PUBG_Dist));
            Setup = new Button();
            Prompt = new Label();
            SuspendLayout();
            // 
            // Setup
            // 
            Setup.Location = new Point(260, 105);
            Setup.Name = "Setup";
            Setup.Size = new Size(161, 90);
            Setup.TabIndex = 0;
            Setup.Text = "开始测距";
            Setup.UseVisualStyleBackColor = true;
            Setup.Click += Setup_Click;
            // 
            // Prompt
            // 
            Prompt.AutoSize = true;
            Prompt.Location = new Point(53, 136);
            Prompt.Name = "Prompt";
            Prompt.Size = new Size(181, 28);
            Prompt.TabIndex = 1;
            Prompt.Text = "CTRL+`  退出程序";
            // 
            // PUBG_Dist
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(493, 353);
            Controls.Add(Prompt);
            Controls.Add(Setup);
            Cursor = Cursors.Default;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PUBG_Dist";
            Text = "PUBG测距工具";
            FormClosing += PUBG_Dist_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Setup;
        private Label Prompt;
    }
}
