namespace DINInput_EUsbKey
{
	partial class MainForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.txt_keyID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txt_Company = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_UserName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.date_KeyUpdateDateFrom = new System.Windows.Forms.DateTimePicker();
			this.date_KeyUpdateDateTo = new System.Windows.Forms.DateTimePicker();
			this.btn_search = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.date_UseEndDayFrom = new System.Windows.Forms.DateTimePicker();
			this.date_UseEndDayTo = new System.Windows.Forms.DateTimePicker();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.date_UseEFUEndDayFrom = new System.Windows.Forms.DateTimePicker();
			this.date_UseEFUEndDayTo = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btn_KeyRecognition = new System.Windows.Forms.Button();
			this.btn_KeyInitialization = new System.Windows.Forms.Button();
			this.btn_KeyCreate = new System.Windows.Forms.Button();
			this.btn_KeyUpdate = new System.Windows.Forms.Button();
			this.fg_main = new C1.Win.C1FlexGrid.C1FlexGrid();
			((System.ComponentModel.ISupportInitialize)(this.fg_main)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(31, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Key_ID：";
			// 
			// txt_keyID
			// 
			this.txt_keyID.Location = new System.Drawing.Point(78, 22);
			this.txt_keyID.Name = "txt_keyID";
			this.txt_keyID.Size = new System.Drawing.Size(130, 21);
			this.txt_keyID.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(241, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "会社名：";
			// 
			// txt_Company
			// 
			this.txt_Company.Location = new System.Drawing.Point(290, 22);
			this.txt_Company.Name = "txt_Company";
			this.txt_Company.Size = new System.Drawing.Size(130, 21);
			this.txt_Company.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(463, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "利用者名：";
			// 
			// txt_UserName
			// 
			this.txt_UserName.Location = new System.Drawing.Point(523, 22);
			this.txt_UserName.Name = "txt_UserName";
			this.txt_UserName.Size = new System.Drawing.Size(130, 21);
			this.txt_UserName.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("宋体", 15F);
			this.label5.Location = new System.Drawing.Point(900, 29);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(22, 12);
			this.label5.TabIndex = 6;
			this.label5.Text = "~";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(702, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "Key更新日：";
			// 
			// date_KeyUpdateDateFrom
			// 
			this.date_KeyUpdateDateFrom.Checked = false;
			this.date_KeyUpdateDateFrom.Location = new System.Drawing.Point(767, 22);
			this.date_KeyUpdateDateFrom.Name = "date_KeyUpdateDateFrom";
			this.date_KeyUpdateDateFrom.ShowCheckBox = true;
			this.date_KeyUpdateDateFrom.Size = new System.Drawing.Size(136, 21);
			this.date_KeyUpdateDateFrom.TabIndex = 7;
			// 
			// date_KeyUpdateDateTo
			// 
			this.date_KeyUpdateDateTo.Checked = false;
			this.date_KeyUpdateDateTo.Location = new System.Drawing.Point(919, 22);
			this.date_KeyUpdateDateTo.Name = "date_KeyUpdateDateTo";
			this.date_KeyUpdateDateTo.ShowCheckBox = true;
			this.date_KeyUpdateDateTo.Size = new System.Drawing.Size(136, 21);
			this.date_KeyUpdateDateTo.TabIndex = 7;
			// 
			// btn_search
			// 
			this.btn_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_search.Location = new System.Drawing.Point(1091, 20);
			this.btn_search.Name = "btn_search";
			this.btn_search.Size = new System.Drawing.Size(75, 23);
			this.btn_search.TabIndex = 8;
			this.btn_search.Text = "検索";
			this.btn_search.UseVisualStyleBackColor = true;
			this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(31, 62);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(113, 12);
			this.label6.TabIndex = 0;
			this.label6.Text = "加工帳利用終了日：";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("宋体", 15F);
			this.label7.Location = new System.Drawing.Point(274, 63);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(22, 12);
			this.label7.TabIndex = 6;
			this.label7.Text = "~";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// date_UseEndDayFrom
			// 
			this.date_UseEndDayFrom.Checked = false;
			this.date_UseEndDayFrom.Location = new System.Drawing.Point(141, 56);
			this.date_UseEndDayFrom.Name = "date_UseEndDayFrom";
			this.date_UseEndDayFrom.ShowCheckBox = true;
			this.date_UseEndDayFrom.Size = new System.Drawing.Size(136, 21);
			this.date_UseEndDayFrom.TabIndex = 7;
			// 
			// date_UseEndDayTo
			// 
			this.date_UseEndDayTo.Checked = false;
			this.date_UseEndDayTo.Location = new System.Drawing.Point(293, 56);
			this.date_UseEndDayTo.Name = "date_UseEndDayTo";
			this.date_UseEndDayTo.ShowCheckBox = true;
			this.date_UseEndDayTo.Size = new System.Drawing.Size(136, 21);
			this.date_UseEndDayTo.TabIndex = 7;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(464, 64);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(161, 12);
			this.label8.TabIndex = 0;
			this.label8.Text = "絵符オプション利用終了日：";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("宋体", 15F);
			this.label9.Location = new System.Drawing.Point(755, 65);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(22, 12);
			this.label9.TabIndex = 6;
			this.label9.Text = "~";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// date_UseEFUEndDayFrom
			// 
			this.date_UseEFUEndDayFrom.Checked = false;
			this.date_UseEFUEndDayFrom.Location = new System.Drawing.Point(622, 58);
			this.date_UseEFUEndDayFrom.Name = "date_UseEFUEndDayFrom";
			this.date_UseEFUEndDayFrom.ShowCheckBox = true;
			this.date_UseEFUEndDayFrom.Size = new System.Drawing.Size(136, 21);
			this.date_UseEFUEndDayFrom.TabIndex = 7;
			// 
			// date_UseEFUEndDayTo
			// 
			this.date_UseEFUEndDayTo.Checked = false;
			this.date_UseEFUEndDayTo.Location = new System.Drawing.Point(774, 58);
			this.date_UseEFUEndDayTo.Name = "date_UseEFUEndDayTo";
			this.date_UseEFUEndDayTo.ShowCheckBox = true;
			this.date_UseEFUEndDayTo.Size = new System.Drawing.Size(136, 21);
			this.date_UseEFUEndDayTo.TabIndex = 7;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(13, 93);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1154, 2);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			// 
			// btn_KeyRecognition
			// 
			this.btn_KeyRecognition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_KeyRecognition.Location = new System.Drawing.Point(857, 105);
			this.btn_KeyRecognition.Name = "btn_KeyRecognition";
			this.btn_KeyRecognition.Size = new System.Drawing.Size(75, 23);
			this.btn_KeyRecognition.TabIndex = 10;
			this.btn_KeyRecognition.Text = "Key認識";
			this.btn_KeyRecognition.UseVisualStyleBackColor = true;
			this.btn_KeyRecognition.Click += new System.EventHandler(this.btn_KeyRecognition_Click);
			// 
			// btn_KeyInitialization
			// 
			this.btn_KeyInitialization.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_KeyInitialization.Location = new System.Drawing.Point(938, 105);
			this.btn_KeyInitialization.Name = "btn_KeyInitialization";
			this.btn_KeyInitialization.Size = new System.Drawing.Size(75, 23);
			this.btn_KeyInitialization.TabIndex = 10;
			this.btn_KeyInitialization.Text = "Key初期化";
			this.btn_KeyInitialization.UseVisualStyleBackColor = true;
			this.btn_KeyInitialization.Click += new System.EventHandler(this.btn_KeyInitialization_Click);
			// 
			// btn_KeyCreate
			// 
			this.btn_KeyCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_KeyCreate.Location = new System.Drawing.Point(1019, 105);
			this.btn_KeyCreate.Name = "btn_KeyCreate";
			this.btn_KeyCreate.Size = new System.Drawing.Size(75, 23);
			this.btn_KeyCreate.TabIndex = 10;
			this.btn_KeyCreate.Text = "Key作成";
			this.btn_KeyCreate.UseVisualStyleBackColor = true;
			this.btn_KeyCreate.Click += new System.EventHandler(this.btn_KeyCreate_Click);
			// 
			// btn_KeyUpdate
			// 
			this.btn_KeyUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_KeyUpdate.Location = new System.Drawing.Point(1100, 105);
			this.btn_KeyUpdate.Name = "btn_KeyUpdate";
			this.btn_KeyUpdate.Size = new System.Drawing.Size(75, 23);
			this.btn_KeyUpdate.TabIndex = 10;
			this.btn_KeyUpdate.Text = "Key更新";
			this.btn_KeyUpdate.UseVisualStyleBackColor = true;
			this.btn_KeyUpdate.Click += new System.EventHandler(this.btn_KeyUpdate_Click);
			// 
			// fg_main
			// 
			this.fg_main.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
			this.fg_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fg_main.ColumnInfo = "0,0,0,0,0,100,Columns:";
			this.fg_main.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
			this.fg_main.Location = new System.Drawing.Point(12, 140);
			this.fg_main.Name = "fg_main";
			this.fg_main.Rows.DefaultSize = 20;
			this.fg_main.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
			this.fg_main.Size = new System.Drawing.Size(1163, 448);
			this.fg_main.TabIndex = 11;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1187, 600);
			this.Controls.Add(this.fg_main);
			this.Controls.Add(this.btn_KeyUpdate);
			this.Controls.Add(this.btn_KeyCreate);
			this.Controls.Add(this.btn_KeyInitialization);
			this.Controls.Add(this.btn_KeyRecognition);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btn_search);
			this.Controls.Add(this.date_UseEFUEndDayTo);
			this.Controls.Add(this.date_UseEndDayTo);
			this.Controls.Add(this.date_KeyUpdateDateTo);
			this.Controls.Add(this.date_UseEFUEndDayFrom);
			this.Controls.Add(this.date_UseEndDayFrom);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.date_KeyUpdateDateFrom);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txt_UserName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txt_Company);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txt_keyID);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MainForm";
			this.Text = "eUSBキー管理システム";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.fg_main)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_keyID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_Company;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_UserName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker date_KeyUpdateDateFrom;
		private System.Windows.Forms.DateTimePicker date_KeyUpdateDateTo;
		private System.Windows.Forms.Button btn_search;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker date_UseEndDayFrom;
		private System.Windows.Forms.DateTimePicker date_UseEndDayTo;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DateTimePicker date_UseEFUEndDayFrom;
		private System.Windows.Forms.DateTimePicker date_UseEFUEndDayTo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btn_KeyRecognition;
		private System.Windows.Forms.Button btn_KeyInitialization;
		private System.Windows.Forms.Button btn_KeyCreate;
		private System.Windows.Forms.Button btn_KeyUpdate;
		private C1.Win.C1FlexGrid.C1FlexGrid fg_main;
	}
}