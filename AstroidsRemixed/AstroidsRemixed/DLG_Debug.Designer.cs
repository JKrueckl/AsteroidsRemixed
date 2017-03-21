namespace AstroidsRemixed
{
    partial class DLG_Debug
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
            this.Debug_SelectBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Debug_CallItems = new System.Windows.Forms.Timer(this.components);
            this.Debug_ObjectInfo = new System.Windows.Forms.ListBox();
            this.Debug_ItemDisplayedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Debug_SelectBox
            // 
            this.Debug_SelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Debug_SelectBox.FormattingEnabled = true;
            this.Debug_SelectBox.Location = new System.Drawing.Point(103, 9);
            this.Debug_SelectBox.Name = "Debug_SelectBox";
            this.Debug_SelectBox.Size = new System.Drawing.Size(579, 28);
            this.Debug_SelectBox.TabIndex = 0;
            this.Debug_SelectBox.SelectedIndexChanged += new System.EventHandler(this.Debug_SelectBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "View item: ";
            // 
            // Debug_CallItems
            // 
            this.Debug_CallItems.Enabled = true;
            this.Debug_CallItems.Interval = 200;
            this.Debug_CallItems.Tick += new System.EventHandler(this.Debug_CallItems_Tick);
            // 
            // Debug_ObjectInfo
            // 
            this.Debug_ObjectInfo.FormattingEnabled = true;
            this.Debug_ObjectInfo.ItemHeight = 20;
            this.Debug_ObjectInfo.Location = new System.Drawing.Point(16, 86);
            this.Debug_ObjectInfo.Name = "Debug_ObjectInfo";
            this.Debug_ObjectInfo.Size = new System.Drawing.Size(666, 564);
            this.Debug_ObjectInfo.TabIndex = 2;
            // 
            // Debug_ItemDisplayedLabel
            // 
            this.Debug_ItemDisplayedLabel.AutoSize = true;
            this.Debug_ItemDisplayedLabel.Location = new System.Drawing.Point(12, 63);
            this.Debug_ItemDisplayedLabel.Name = "Debug_ItemDisplayedLabel";
            this.Debug_ItemDisplayedLabel.Size = new System.Drawing.Size(128, 20);
            this.Debug_ItemDisplayedLabel.TabIndex = 3;
            this.Debug_ItemDisplayedLabel.Text = "No game objects";
            // 
            // DLG_Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 665);
            this.Controls.Add(this.Debug_ItemDisplayedLabel);
            this.Controls.Add(this.Debug_ObjectInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Debug_SelectBox);
            this.Name = "DLG_Debug";
            this.Text = "DLG_Debug";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Debug_SelectBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer Debug_CallItems;
        private System.Windows.Forms.ListBox Debug_ObjectInfo;
        private System.Windows.Forms.Label Debug_ItemDisplayedLabel;
    }
}