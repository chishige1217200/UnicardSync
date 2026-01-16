namespace UnicardSync
{
    partial class TableForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableForm));
            this.Table = new System.Windows.Forms.DataGridView();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            this.TorikomiTypeComboBox = new System.Windows.Forms.ComboBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.placeText = new System.Windows.Forms.Label();
            this.Place = new System.Windows.Forms.TextBox();
            this.AmountFrom = new System.Windows.Forms.NumericUpDown();
            this.AmountTo = new System.Windows.Forms.NumericUpDown();
            this.amountText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DateTo = new System.Windows.Forms.DateTimePicker();
            this.Note = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TorikomiTypeComboBoxSearch = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountTo)).BeginInit();
            this.SuspendLayout();
            // 
            // Table
            // 
            this.Table.AllowUserToAddRows = false;
            this.Table.AllowUserToDeleteRows = false;
            this.Table.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Table.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table.Location = new System.Drawing.Point(0, 90);
            this.Table.Name = "Table";
            this.Table.ReadOnly = true;
            this.Table.RowHeadersVisible = false;
            this.Table.RowTemplate.Height = 21;
            this.Table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Table.Size = new System.Drawing.Size(1264, 591);
            this.Table.TabIndex = 100;
            this.Table.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Table_CellDoubleClick);
            // 
            // ExportButton
            // 
            this.ExportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportButton.Location = new System.Drawing.Point(1152, 3);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(100, 23);
            this.ExportButton.TabIndex = 54;
            this.ExportButton.Text = "データ出力";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.Location = new System.Drawing.Point(1046, 3);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(100, 23);
            this.ImportButton.TabIndex = 53;
            this.ImportButton.Text = "データ取込";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // TorikomiTypeComboBox
            // 
            this.TorikomiTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TorikomiTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TorikomiTypeComboBox.FormattingEnabled = true;
            this.TorikomiTypeComboBox.Location = new System.Drawing.Point(890, 5);
            this.TorikomiTypeComboBox.Name = "TorikomiTypeComboBox";
            this.TorikomiTypeComboBox.Size = new System.Drawing.Size(150, 20);
            this.TorikomiTypeComboBox.TabIndex = 52;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(3, 3);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(100, 23);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "明細をダブルクリックして編集画面を開きます。";
            // 
            // placeText
            // 
            this.placeText.AutoSize = true;
            this.placeText.Location = new System.Drawing.Point(3, 35);
            this.placeText.Name = "placeText";
            this.placeText.Size = new System.Drawing.Size(41, 12);
            this.placeText.TabIndex = 3;
            this.placeText.Text = "利用先";
            // 
            // Place
            // 
            this.Place.Location = new System.Drawing.Point(50, 32);
            this.Place.Name = "Place";
            this.Place.Size = new System.Drawing.Size(100, 19);
            this.Place.TabIndex = 4;
            // 
            // AmountFrom
            // 
            this.AmountFrom.Location = new System.Drawing.Point(191, 32);
            this.AmountFrom.Maximum = new decimal(new int[] {
            2099999999,
            0,
            0,
            0});
            this.AmountFrom.Name = "AmountFrom";
            this.AmountFrom.Size = new System.Drawing.Size(80, 19);
            this.AmountFrom.TabIndex = 6;
            // 
            // AmountTo
            // 
            this.AmountTo.Location = new System.Drawing.Point(300, 32);
            this.AmountTo.Maximum = new decimal(new int[] {
            2100000000,
            0,
            0,
            0});
            this.AmountTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AmountTo.Name = "AmountTo";
            this.AmountTo.Size = new System.Drawing.Size(80, 19);
            this.AmountTo.TabIndex = 8;
            this.AmountTo.Value = new decimal(new int[] {
            2100000000,
            0,
            0,
            0});
            // 
            // amountText
            // 
            this.amountText.AutoSize = true;
            this.amountText.Location = new System.Drawing.Point(156, 35);
            this.amountText.Name = "amountText";
            this.amountText.Size = new System.Drawing.Size(29, 12);
            this.amountText.TabIndex = 5;
            this.amountText.Text = "金額";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "～";
            // 
            // DateFrom
            // 
            this.DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateFrom.Location = new System.Drawing.Point(433, 32);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(110, 19);
            this.DateFrom.TabIndex = 10;
            this.DateFrom.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // dateText
            // 
            this.dateText.AutoSize = true;
            this.dateText.Location = new System.Drawing.Point(386, 35);
            this.dateText.Name = "dateText";
            this.dateText.Size = new System.Drawing.Size(41, 12);
            this.dateText.TabIndex = 9;
            this.dateText.Text = "利用日";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(549, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "～";
            // 
            // DateTo
            // 
            this.DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateTo.Location = new System.Drawing.Point(572, 32);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(110, 19);
            this.DateTo.TabIndex = 12;
            this.DateTo.Value = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            // 
            // Note
            // 
            this.Note.Location = new System.Drawing.Point(50, 61);
            this.Note.Name = "Note";
            this.Note.Size = new System.Drawing.Size(221, 19);
            this.Note.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "備考";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(815, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 12);
            this.label5.TabIndex = 51;
            this.label5.Text = "データ入出力";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(277, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "取込区分";
            // 
            // TorikomiTypeComboBoxSearch
            // 
            this.TorikomiTypeComboBoxSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TorikomiTypeComboBoxSearch.FormattingEnabled = true;
            this.TorikomiTypeComboBoxSearch.Location = new System.Drawing.Point(336, 61);
            this.TorikomiTypeComboBoxSearch.Name = "TorikomiTypeComboBoxSearch";
            this.TorikomiTypeComboBoxSearch.Size = new System.Drawing.Size(150, 20);
            this.TorikomiTypeComboBoxSearch.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(492, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "ファイル名";
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(549, 61);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(133, 19);
            this.FileName.TabIndex = 18;
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TorikomiTypeComboBoxSearch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DateTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateText);
            this.Controls.Add(this.DateFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.amountText);
            this.Controls.Add(this.AmountTo);
            this.Controls.Add(this.AmountFrom);
            this.Controls.Add(this.Place);
            this.Controls.Add(this.placeText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.TorikomiTypeComboBox);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.Table);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "TableForm";
            this.Text = "UnicardSync";
            this.Shown += new System.EventHandler(this.TableForm_Shown);
            this.Resize += new System.EventHandler(this.TableForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Table;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.ComboBox TorikomiTypeComboBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label placeText;
        private System.Windows.Forms.TextBox Place;
        private System.Windows.Forms.NumericUpDown AmountFrom;
        private System.Windows.Forms.NumericUpDown AmountTo;
        private System.Windows.Forms.Label amountText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DateFrom;
        private System.Windows.Forms.Label dateText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DateTo;
        private System.Windows.Forms.TextBox Note;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox TorikomiTypeComboBoxSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox FileName;
    }
}

