namespace Fear
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AutoNext = new System.Windows.Forms.CheckBox();
            this.AutoReinforce = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FreeReinforcementCnt = new System.Windows.Forms.Button();
            this.StartCountBtn = new System.Windows.Forms.Button();
            this.BaseFitnessBtn = new System.Windows.Forms.Button();
            this.ImproveShields = new System.Windows.Forms.Button();
            this.GoldTB = new System.Windows.Forms.TextBox();
            this.ImproveSwords = new System.Windows.Forms.Button();
            this.Restart = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.BuyEliteSoldiers = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SilverTB = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.teamRedCounter = new System.Windows.Forms.TextBox();
            this.teamBlueCounter = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AutoNext);
            this.panel1.Controls.Add(this.AutoReinforce);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.Restart);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.BuyEliteSoldiers);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.SilverTB);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.teamRedCounter);
            this.panel1.Controls.Add(this.teamBlueCounter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 569);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 143);
            this.panel1.TabIndex = 0;
            // 
            // AutoNext
            // 
            this.AutoNext.AutoSize = true;
            this.AutoNext.Location = new System.Drawing.Point(941, 46);
            this.AutoNext.Name = "AutoNext";
            this.AutoNext.Size = new System.Drawing.Size(70, 17);
            this.AutoNext.TabIndex = 15;
            this.AutoNext.Text = "AutoNext";
            this.AutoNext.UseVisualStyleBackColor = true;
            // 
            // AutoReinforce
            // 
            this.AutoReinforce.AutoSize = true;
            this.AutoReinforce.Location = new System.Drawing.Point(542, 45);
            this.AutoReinforce.Name = "AutoReinforce";
            this.AutoReinforce.Size = new System.Drawing.Size(94, 17);
            this.AutoReinforce.TabIndex = 14;
            this.AutoReinforce.Text = "AutoReinforce";
            this.AutoReinforce.UseVisualStyleBackColor = true;
            this.AutoReinforce.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.FreeReinforcementCnt);
            this.panel2.Controls.Add(this.StartCountBtn);
            this.panel2.Controls.Add(this.BaseFitnessBtn);
            this.panel2.Controls.Add(this.ImproveShields);
            this.panel2.Controls.Add(this.GoldTB);
            this.panel2.Controls.Add(this.ImproveSwords);
            this.panel2.Location = new System.Drawing.Point(441, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(575, 64);
            this.panel2.TabIndex = 13;
            // 
            // FreeReinforcementCnt
            // 
            this.FreeReinforcementCnt.Location = new System.Drawing.Point(417, 32);
            this.FreeReinforcementCnt.Name = "FreeReinforcementCnt";
            this.FreeReinforcementCnt.Size = new System.Drawing.Size(144, 23);
            this.FreeReinforcementCnt.TabIndex = 19;
            this.FreeReinforcementCnt.Text = "FreeReinforcementCnt";
            this.FreeReinforcementCnt.UseVisualStyleBackColor = true;
            this.FreeReinforcementCnt.Click += new System.EventHandler(this.FreeReinforcementCnt_Click);
            // 
            // StartCountBtn
            // 
            this.StartCountBtn.Location = new System.Drawing.Point(417, 6);
            this.StartCountBtn.Name = "StartCountBtn";
            this.StartCountBtn.Size = new System.Drawing.Size(144, 23);
            this.StartCountBtn.TabIndex = 18;
            this.StartCountBtn.Text = "IncrStartCount";
            this.StartCountBtn.UseVisualStyleBackColor = true;
            this.StartCountBtn.Click += new System.EventHandler(this.IncrStartCount_Click);
            // 
            // BaseFitnessBtn
            // 
            this.BaseFitnessBtn.Location = new System.Drawing.Point(263, 32);
            this.BaseFitnessBtn.Name = "BaseFitnessBtn";
            this.BaseFitnessBtn.Size = new System.Drawing.Size(145, 23);
            this.BaseFitnessBtn.TabIndex = 17;
            this.BaseFitnessBtn.Text = "ImpBaseFitness";
            this.BaseFitnessBtn.UseVisualStyleBackColor = true;
            this.BaseFitnessBtn.Click += new System.EventHandler(this.ImpBaseFitness_Click);
            // 
            // ImproveShields
            // 
            this.ImproveShields.Location = new System.Drawing.Point(120, 32);
            this.ImproveShields.Name = "ImproveShields";
            this.ImproveShields.Size = new System.Drawing.Size(137, 23);
            this.ImproveShields.TabIndex = 15;
            this.ImproveShields.Text = "ImproveShields";
            this.ImproveShields.UseVisualStyleBackColor = true;
            this.ImproveShields.Click += new System.EventHandler(this.ImproveShields_Click);
            // 
            // GoldTB
            // 
            this.GoldTB.Location = new System.Drawing.Point(3, 6);
            this.GoldTB.Name = "GoldTB";
            this.GoldTB.Size = new System.Drawing.Size(75, 20);
            this.GoldTB.TabIndex = 14;
            // 
            // ImproveSwords
            // 
            this.ImproveSwords.Location = new System.Drawing.Point(120, 6);
            this.ImproveSwords.Name = "ImproveSwords";
            this.ImproveSwords.Size = new System.Drawing.Size(137, 23);
            this.ImproveSwords.TabIndex = 8;
            this.ImproveSwords.Text = "ImproveSwords";
            this.ImproveSwords.UseVisualStyleBackColor = true;
            this.ImproveSwords.Click += new System.EventHandler(this.ImproveSwords_Click);
            // 
            // Restart
            // 
            this.Restart.Location = new System.Drawing.Point(13, 58);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(75, 23);
            this.Restart.TabIndex = 12;
            this.Restart.Text = "Restart";
            this.Restart.UseVisualStyleBackColor = true;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(53, 31);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(35, 23);
            this.button9.TabIndex = 11;
            this.button9.Text = "-";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.slower_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(13, 31);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(43, 23);
            this.button8.TabIndex = 10;
            this.button8.Text = "+";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.faster_Click);
            // 
            // BuyEliteSoldiers
            // 
            this.BuyEliteSoldiers.Location = new System.Drawing.Point(819, 16);
            this.BuyEliteSoldiers.Name = "BuyEliteSoldiers";
            this.BuyEliteSoldiers.Size = new System.Drawing.Size(87, 23);
            this.BuyEliteSoldiers.TabIndex = 9;
            this.BuyEliteSoldiers.Text = "BuyEliteSoldiers";
            this.BuyEliteSoldiers.UseVisualStyleBackColor = true;
            this.BuyEliteSoldiers.Click += new System.EventHandler(this.buyEliteSoldiers_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(645, 46);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "FeedForFitness";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(738, 42);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "BuySwords";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(738, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "BuyShields";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(645, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Reinforce";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.reinforce_Click);
            // 
            // SilverTB
            // 
            this.SilverTB.Location = new System.Drawing.Point(542, 14);
            this.SilverTB.Name = "SilverTB";
            this.SilverTB.Size = new System.Drawing.Size(75, 20);
            this.SilverTB.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(941, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Next_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Pause";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.pause_resume_Click);
            // 
            // teamRedCounter
            // 
            this.teamRedCounter.ForeColor = System.Drawing.Color.DarkRed;
            this.teamRedCounter.Location = new System.Drawing.Point(13, 111);
            this.teamRedCounter.Name = "teamRedCounter";
            this.teamRedCounter.Size = new System.Drawing.Size(422, 20);
            this.teamRedCounter.TabIndex = 1;
            // 
            // teamBlueCounter
            // 
            this.teamBlueCounter.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.teamBlueCounter.Location = new System.Drawing.Point(13, 87);
            this.teamBlueCounter.Name = "teamBlueCounter";
            this.teamBlueCounter.Size = new System.Drawing.Size(422, 20);
            this.teamBlueCounter.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 712);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox teamRedCounter;
        private System.Windows.Forms.TextBox teamBlueCounter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox SilverTB;
        private System.Windows.Forms.Button BuyEliteSoldiers;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BaseFitnessBtn;
        private System.Windows.Forms.Button ImproveShields;
        private System.Windows.Forms.TextBox GoldTB;
        private System.Windows.Forms.Button ImproveSwords;
        private System.Windows.Forms.Button FreeReinforcementCnt;
        private System.Windows.Forms.Button StartCountBtn;
        private System.Windows.Forms.CheckBox AutoReinforce;
        private System.Windows.Forms.CheckBox AutoNext;
    }
}

