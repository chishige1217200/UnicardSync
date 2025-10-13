namespace UnicardSync
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.textBoxUsedID = new System.Windows.Forms.TextBox();
            this.labelUsedID = new System.Windows.Forms.Label();
            this.labelPlaceUsed = new System.Windows.Forms.Label();
            this.textBoxPlaceUsed = new System.Windows.Forms.TextBox();
            this.labelAmountUsed = new System.Windows.Forms.Label();
            this.labelDateUsed = new System.Windows.Forms.Label();
            this.dateTimePickerDateUsed = new System.Windows.Forms.DateTimePicker();
            this.labelNote = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.numericUpDownAmountUsed = new System.Windows.Forms.NumericUpDown();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmountUsed)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxUsedID
            // 
            this.textBoxUsedID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUsedID.Location = new System.Drawing.Point(71, 6);
            this.textBoxUsedID.Name = "textBoxUsedID";
            this.textBoxUsedID.ReadOnly = true;
            this.textBoxUsedID.Size = new System.Drawing.Size(300, 19);
            this.textBoxUsedID.TabIndex = 0;
            // 
            // labelUsedID
            // 
            this.labelUsedID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUsedID.AutoSize = true;
            this.labelUsedID.Location = new System.Drawing.Point(12, 9);
            this.labelUsedID.Name = "labelUsedID";
            this.labelUsedID.Size = new System.Drawing.Size(53, 12);
            this.labelUsedID.TabIndex = 100;
            this.labelUsedID.Text = "明細番号";
            // 
            // labelPlaceUsed
            // 
            this.labelPlaceUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlaceUsed.AutoSize = true;
            this.labelPlaceUsed.Location = new System.Drawing.Point(12, 34);
            this.labelPlaceUsed.Name = "labelPlaceUsed";
            this.labelPlaceUsed.Size = new System.Drawing.Size(41, 12);
            this.labelPlaceUsed.TabIndex = 101;
            this.labelPlaceUsed.Text = "利用先";
            // 
            // textBoxPlaceUsed
            // 
            this.textBoxPlaceUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPlaceUsed.Location = new System.Drawing.Point(71, 31);
            this.textBoxPlaceUsed.Name = "textBoxPlaceUsed";
            this.textBoxPlaceUsed.Size = new System.Drawing.Size(300, 19);
            this.textBoxPlaceUsed.TabIndex = 1;
            // 
            // labelAmountUsed
            // 
            this.labelAmountUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAmountUsed.AutoSize = true;
            this.labelAmountUsed.Location = new System.Drawing.Point(12, 59);
            this.labelAmountUsed.Name = "labelAmountUsed";
            this.labelAmountUsed.Size = new System.Drawing.Size(29, 12);
            this.labelAmountUsed.TabIndex = 102;
            this.labelAmountUsed.Text = "金額";
            // 
            // labelDateUsed
            // 
            this.labelDateUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDateUsed.AutoSize = true;
            this.labelDateUsed.Location = new System.Drawing.Point(12, 86);
            this.labelDateUsed.Name = "labelDateUsed";
            this.labelDateUsed.Size = new System.Drawing.Size(41, 12);
            this.labelDateUsed.TabIndex = 103;
            this.labelDateUsed.Text = "利用日";
            // 
            // dateTimePickerDateUsed
            // 
            this.dateTimePickerDateUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerDateUsed.CustomFormat = "yyyy/MM/dd";
            this.dateTimePickerDateUsed.Location = new System.Drawing.Point(71, 81);
            this.dateTimePickerDateUsed.Name = "dateTimePickerDateUsed";
            this.dateTimePickerDateUsed.Size = new System.Drawing.Size(200, 19);
            this.dateTimePickerDateUsed.TabIndex = 3;
            this.dateTimePickerDateUsed.Value = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            // 
            // labelNote
            // 
            this.labelNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(12, 109);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(29, 12);
            this.labelNote.TabIndex = 104;
            this.labelNote.Text = "備考";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNote.Location = new System.Drawing.Point(71, 106);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(300, 40);
            this.textBoxNote.TabIndex = 4;
            // 
            // numericUpDownAmountUsed
            // 
            this.numericUpDownAmountUsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownAmountUsed.Location = new System.Drawing.Point(71, 56);
            this.numericUpDownAmountUsed.Maximum = new decimal(new int[] {
            2100000000,
            0,
            0,
            0});
            this.numericUpDownAmountUsed.Name = "numericUpDownAmountUsed";
            this.numericUpDownAmountUsed.Size = new System.Drawing.Size(300, 19);
            this.numericUpDownAmountUsed.TabIndex = 2;
            this.numericUpDownAmountUsed.ThousandsSeparator = true;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateButton.Location = new System.Drawing.Point(215, 176);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 105;
            this.UpdateButton.Text = "更新する";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BackButton.Location = new System.Drawing.Point(296, 176);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(75, 23);
            this.BackButton.TabIndex = 106;
            this.BackButton.Text = "戻る";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.numericUpDownAmountUsed);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.dateTimePickerDateUsed);
            this.Controls.Add(this.labelDateUsed);
            this.Controls.Add(this.labelAmountUsed);
            this.Controls.Add(this.textBoxPlaceUsed);
            this.Controls.Add(this.labelPlaceUsed);
            this.Controls.Add(this.labelUsedID);
            this.Controls.Add(this.textBoxUsedID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "EditForm";
            this.Text = "明細編集";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmountUsed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUsedID;
        private System.Windows.Forms.Label labelUsedID;
        private System.Windows.Forms.Label labelPlaceUsed;
        private System.Windows.Forms.TextBox textBoxPlaceUsed;
        private System.Windows.Forms.Label labelAmountUsed;
        private System.Windows.Forms.Label labelDateUsed;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateUsed;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.NumericUpDown numericUpDownAmountUsed;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button BackButton;
    }
}