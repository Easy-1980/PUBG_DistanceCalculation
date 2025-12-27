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
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // Setup
            // 
            Setup.Location = new Point(378, 118);
            Setup.Name = "Setup";
            Setup.Size = new Size(150, 90);
            Setup.TabIndex = 0;
            Setup.Text = "开始测距";
            Setup.UseVisualStyleBackColor = true;
            Setup.Click += Setup_Click;
            // 
            // Prompt
            // 
            Prompt.AutoSize = true;
            Prompt.Location = new Point(49, 198);
            Prompt.Name = "Prompt";
            Prompt.Size = new Size(243, 28);
            Prompt.TabIndex = 1;
            Prompt.Text = "4. CTRL  + `：  退出程序";
            Prompt.Click += Prompt_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(Prompt);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(365, 317);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "CTRL 快捷键";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 250);
            label4.Name = "label4";
            label4.Size = new Size(352, 28);
            label4.TabIndex = 5;
            label4.Text = "5. CTRL + SHIFT + 右键： 重置标零";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 149);
            label3.Name = "label3";
            label3.Size = new Size(267, 28);
            label3.TabIndex = 4;
            label3.Text = "3. CTRL + 右键： 清除绘制";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 47);
            label2.Name = "label2";
            label2.Size = new Size(267, 28);
            label2.TabIndex = 3;
            label2.Text = "1. CTRL + 左键： 标点绘线";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 98);
            label1.Name = "label1";
            label1.Size = new Size(237, 28);
            label1.TabIndex = 2;
            label1.Text = "2. CTRL + S： 保存标零";
            // 
            // PUBG_Dist
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(540, 317);
            Controls.Add(groupBox1);
            Controls.Add(Setup);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PUBG_Dist";
            Text = "PUBG测距工具";
            FormClosing += PUBG_Dist_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button Setup;
        private Label Prompt;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
    }
}
