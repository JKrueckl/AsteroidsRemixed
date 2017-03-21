namespace AstroidsRemixed
{
    partial class DLG_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DLG_Main));
            this.Game_Timer = new System.Windows.Forms.Timer(this.components);
            this.UI_Menu_Panel = new System.Windows.Forms.Panel();
            this.UI_B_Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.UI_Pause_Panel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.UI_GameOver_Panel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.UI_Menu_Panel.SuspendLayout();
            this.UI_Pause_Panel.SuspendLayout();
            this.UI_GameOver_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Game_Timer
            // 
            this.Game_Timer.Enabled = true;
            this.Game_Timer.Interval = 10;
            this.Game_Timer.Tick += new System.EventHandler(this.Game_Timer_Tick);
            // 
            // UI_Menu_Panel
            // 
            this.UI_Menu_Panel.BackColor = System.Drawing.Color.Black;
            this.UI_Menu_Panel.Controls.Add(this.UI_B_Start);
            this.UI_Menu_Panel.Controls.Add(this.label1);
            this.UI_Menu_Panel.Controls.Add(this.Title);
            this.UI_Menu_Panel.Location = new System.Drawing.Point(12, 12);
            this.UI_Menu_Panel.Name = "UI_Menu_Panel";
            this.UI_Menu_Panel.Size = new System.Drawing.Size(212, 393);
            this.UI_Menu_Panel.TabIndex = 0;
            // 
            // UI_B_Start
            // 
            this.UI_B_Start.Location = new System.Drawing.Point(69, 357);
            this.UI_B_Start.Name = "UI_B_Start";
            this.UI_B_Start.Size = new System.Drawing.Size(75, 23);
            this.UI_B_Start.TabIndex = 2;
            this.UI_B_Start.Text = "Start";
            this.UI_B_Start.UseVisualStyleBackColor = true;
            this.UI_B_Start.Click += new System.EventHandler(this.UI_B_Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 280);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(47, 15);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(113, 31);
            this.Title.TabIndex = 0;
            this.Title.Text = "Astroids";
            // 
            // UI_Pause_Panel
            // 
            this.UI_Pause_Panel.BackColor = System.Drawing.Color.Black;
            this.UI_Pause_Panel.Controls.Add(this.label2);
            this.UI_Pause_Panel.Location = new System.Drawing.Point(230, 12);
            this.UI_Pause_Panel.Name = "UI_Pause_Panel";
            this.UI_Pause_Panel.Size = new System.Drawing.Size(146, 87);
            this.UI_Pause_Panel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(18, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 62);
            this.label2.TabIndex = 1;
            this.label2.Text = "Astroids\r\nPause\r\n";
            // 
            // UI_GameOver_Panel
            // 
            this.UI_GameOver_Panel.BackColor = System.Drawing.Color.Black;
            this.UI_GameOver_Panel.Controls.Add(this.label3);
            this.UI_GameOver_Panel.Location = new System.Drawing.Point(230, 105);
            this.UI_GameOver_Panel.Name = "UI_GameOver_Panel";
            this.UI_GameOver_Panel.Size = new System.Drawing.Size(120, 87);
            this.UI_GameOver_Panel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(18, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 62);
            this.label3.TabIndex = 1;
            this.label3.Text = "Game\r\nOver\r\n";
            // 
            // DLG_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 637);
            this.Controls.Add(this.UI_GameOver_Panel);
            this.Controls.Add(this.UI_Pause_Panel);
            this.Controls.Add(this.UI_Menu_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DLG_Main";
            this.Text = "Astroids Remix v1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DLG_Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DLG_Main_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DLG_Main_KeyUp);
            this.UI_Menu_Panel.ResumeLayout(false);
            this.UI_Menu_Panel.PerformLayout();
            this.UI_Pause_Panel.ResumeLayout(false);
            this.UI_Pause_Panel.PerformLayout();
            this.UI_GameOver_Panel.ResumeLayout(false);
            this.UI_GameOver_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Game_Timer;
        private System.Windows.Forms.Panel UI_Menu_Panel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button UI_B_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel UI_Pause_Panel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel UI_GameOver_Panel;
        private System.Windows.Forms.Label label3;
    }
}

