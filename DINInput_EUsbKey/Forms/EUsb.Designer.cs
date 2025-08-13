namespace DINInput_EUsbKey
{
	partial class EUsb
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
			this.rdb_Active = new System.Windows.Forms.RadioButton();
			this.rdb_malfunction = new System.Windows.Forms.RadioButton();
			this.rdb_loss = new System.Windows.Forms.RadioButton();
			this.rdb_cancel = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.txt_KeyID = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_Company = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txt_UserName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txt_Mail = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txt_Tel = new System.Windows.Forms.TextBox();
			this.lb_UseStartDay = new System.Windows.Forms.Label();
			this.date_UseStartDay = new System.Windows.Forms.DateTimePicker();
			this.lb_UseEndDay = new System.Windows.Forms.Label();
			this.date_UseEndDay = new System.Windows.Forms.DateTimePicker();
			this.check_CADGetInfo = new System.Windows.Forms.CheckBox();
			this.check_Weight = new System.Windows.Forms.CheckBox();
			this.check_HunchInput = new System.Windows.Forms.CheckBox();
			this.check_SpecialCalculation = new System.Windows.Forms.CheckBox();
			this.check_DatxRecovery = new System.Windows.Forms.CheckBox();
			this.check_QR = new System.Windows.Forms.CheckBox();
			this.check_CreateSumFile = new System.Windows.Forms.CheckBox();
			this.check_SymbolOptions = new System.Windows.Forms.CheckBox();
			this.lb_UseEFStartDay = new System.Windows.Forms.Label();
			this.lb_UseEFEndDay = new System.Windows.Forms.Label();
			this.date_UseEFStartDay = new System.Windows.Forms.DateTimePicker();
			this.date_UseEFEndDay = new System.Windows.Forms.DateTimePicker();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.check_10 = new System.Windows.Forms.CheckBox();
			this.check_09 = new System.Windows.Forms.CheckBox();
			this.check_08 = new System.Windows.Forms.CheckBox();
			this.check_07 = new System.Windows.Forms.CheckBox();
			this.check_06 = new System.Windows.Forms.CheckBox();
			this.check_05 = new System.Windows.Forms.CheckBox();
			this.check_04 = new System.Windows.Forms.CheckBox();
			this.check_03 = new System.Windows.Forms.CheckBox();
			this.check_02 = new System.Windows.Forms.CheckBox();
			this.check_01 = new System.Windows.Forms.CheckBox();
			this.cmb_DINCAD = new System.Windows.Forms.ComboBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txt_CADVersion = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.txt_CADTarget = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.date_CADOPUseEndDay = new System.Windows.Forms.DateTimePicker();
			this.label15 = new System.Windows.Forms.Label();
			this.check_TPM = new System.Windows.Forms.CheckBox();
			this.check_DINsubcon = new System.Windows.Forms.CheckBox();
			this.label16 = new System.Windows.Forms.Label();
			this.txt_Notes = new System.Windows.Forms.TextBox();
			this.btn_OK = new System.Windows.Forms.Button();
			this.btn_UpdateKeyRelease = new System.Windows.Forms.Button();
			this.btn_Close = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "状態：";
			// 
			// rdb_Active
			// 
			this.rdb_Active.AutoSize = true;
			this.rdb_Active.Checked = true;
			this.rdb_Active.Location = new System.Drawing.Point(4, 5);
			this.rdb_Active.Name = "rdb_Active";
			this.rdb_Active.Size = new System.Drawing.Size(59, 16);
			this.rdb_Active.TabIndex = 1;
			this.rdb_Active.TabStop = true;
			this.rdb_Active.Text = "活動中";
			this.rdb_Active.UseVisualStyleBackColor = true;
			// 
			// rdb_malfunction
			// 
			this.rdb_malfunction.AutoSize = true;
			this.rdb_malfunction.Location = new System.Drawing.Point(83, 5);
			this.rdb_malfunction.Name = "rdb_malfunction";
			this.rdb_malfunction.Size = new System.Drawing.Size(47, 16);
			this.rdb_malfunction.TabIndex = 1;
			this.rdb_malfunction.TabStop = true;
			this.rdb_malfunction.Text = "故障";
			this.rdb_malfunction.UseVisualStyleBackColor = true;
			// 
			// rdb_loss
			// 
			this.rdb_loss.AutoSize = true;
			this.rdb_loss.Location = new System.Drawing.Point(153, 5);
			this.rdb_loss.Name = "rdb_loss";
			this.rdb_loss.Size = new System.Drawing.Size(47, 16);
			this.rdb_loss.TabIndex = 1;
			this.rdb_loss.TabStop = true;
			this.rdb_loss.Text = "紛失";
			this.rdb_loss.UseVisualStyleBackColor = true;
			// 
			// rdb_cancel
			// 
			this.rdb_cancel.AutoSize = true;
			this.rdb_cancel.Location = new System.Drawing.Point(224, 5);
			this.rdb_cancel.Name = "rdb_cancel";
			this.rdb_cancel.Size = new System.Drawing.Size(47, 16);
			this.rdb_cancel.TabIndex = 1;
			this.rdb_cancel.TabStop = true;
			this.rdb_cancel.Text = "廃止";
			this.rdb_cancel.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(29, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "KeyID：";
			// 
			// txt_KeyID
			// 
			this.txt_KeyID.Enabled = false;
			this.txt_KeyID.Location = new System.Drawing.Point(82, 52);
			this.txt_KeyID.MaxLength = 50;
			this.txt_KeyID.Name = "txt_KeyID";
			this.txt_KeyID.Size = new System.Drawing.Size(196, 21);
			this.txt_KeyID.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 82);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "会社名：";
			// 
			// txt_Company
			// 
			this.txt_Company.Location = new System.Drawing.Point(82, 79);
			this.txt_Company.MaxLength = 50;
			this.txt_Company.Name = "txt_Company";
			this.txt_Company.Size = new System.Drawing.Size(196, 21);
			this.txt_Company.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "利用者名：";
			// 
			// txt_UserName
			// 
			this.txt_UserName.Location = new System.Drawing.Point(82, 106);
			this.txt_UserName.MaxLength = 50;
			this.txt_UserName.Name = "txt_UserName";
			this.txt_UserName.Size = new System.Drawing.Size(196, 21);
			this.txt_UserName.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(23, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(53, 12);
			this.label5.TabIndex = 0;
			this.label5.Text = "メール：";
			// 
			// txt_Mail
			// 
			this.txt_Mail.Location = new System.Drawing.Point(82, 133);
			this.txt_Mail.MaxLength = 50;
			this.txt_Mail.Name = "txt_Mail";
			this.txt_Mail.Size = new System.Drawing.Size(196, 21);
			this.txt_Mail.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(41, 163);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 12);
			this.label6.TabIndex = 0;
			this.label6.Text = "Tel：";
			// 
			// txt_Tel
			// 
			this.txt_Tel.Location = new System.Drawing.Point(82, 160);
			this.txt_Tel.MaxLength = 50;
			this.txt_Tel.Name = "txt_Tel";
			this.txt_Tel.Size = new System.Drawing.Size(196, 21);
			this.txt_Tel.TabIndex = 2;
			// 
			// lb_UseStartDay
			// 
			this.lb_UseStartDay.AutoSize = true;
			this.lb_UseStartDay.Location = new System.Drawing.Point(11, 204);
			this.lb_UseStartDay.Name = "lb_UseStartDay";
			this.lb_UseStartDay.Size = new System.Drawing.Size(77, 12);
			this.lb_UseStartDay.TabIndex = 0;
			this.lb_UseStartDay.Text = "利用開始日：";
			// 
			// date_UseStartDay
			// 
			this.date_UseStartDay.Location = new System.Drawing.Point(82, 198);
			this.date_UseStartDay.Name = "date_UseStartDay";
			this.date_UseStartDay.Size = new System.Drawing.Size(130, 21);
			this.date_UseStartDay.TabIndex = 3;
			// 
			// lb_UseEndDay
			// 
			this.lb_UseEndDay.AutoSize = true;
			this.lb_UseEndDay.Location = new System.Drawing.Point(249, 203);
			this.lb_UseEndDay.Name = "lb_UseEndDay";
			this.lb_UseEndDay.Size = new System.Drawing.Size(77, 12);
			this.lb_UseEndDay.TabIndex = 0;
			this.lb_UseEndDay.Text = "利用終了日：";
			// 
			// date_UseEndDay
			// 
			this.date_UseEndDay.Location = new System.Drawing.Point(320, 198);
			this.date_UseEndDay.Name = "date_UseEndDay";
			this.date_UseEndDay.Size = new System.Drawing.Size(130, 21);
			this.date_UseEndDay.TabIndex = 3;
			// 
			// check_CADGetInfo
			// 
			this.check_CADGetInfo.AutoSize = true;
			this.check_CADGetInfo.Location = new System.Drawing.Point(15, 226);
			this.check_CADGetInfo.Name = "check_CADGetInfo";
			this.check_CADGetInfo.Size = new System.Drawing.Size(90, 16);
			this.check_CADGetInfo.TabIndex = 4;
			this.check_CADGetInfo.Text = "CAD情報取込";
			this.check_CADGetInfo.UseVisualStyleBackColor = true;
			// 
			// check_Weight
			// 
			this.check_Weight.AutoSize = true;
			this.check_Weight.Location = new System.Drawing.Point(15, 248);
			this.check_Weight.Name = "check_Weight";
			this.check_Weight.Size = new System.Drawing.Size(60, 16);
			this.check_Weight.TabIndex = 4;
			this.check_Weight.Text = "重量表";
			this.check_Weight.UseVisualStyleBackColor = true;
			// 
			// check_HunchInput
			// 
			this.check_HunchInput.AutoSize = true;
			this.check_HunchInput.Location = new System.Drawing.Point(117, 248);
			this.check_HunchInput.Name = "check_HunchInput";
			this.check_HunchInput.Size = new System.Drawing.Size(60, 16);
			this.check_HunchInput.TabIndex = 4;
			this.check_HunchInput.Text = "ハンチ";
			this.check_HunchInput.UseVisualStyleBackColor = true;
			// 
			// check_SpecialCalculation
			// 
			this.check_SpecialCalculation.AutoSize = true;
			this.check_SpecialCalculation.Location = new System.Drawing.Point(230, 248);
			this.check_SpecialCalculation.Name = "check_SpecialCalculation";
			this.check_SpecialCalculation.Size = new System.Drawing.Size(72, 16);
			this.check_SpecialCalculation.TabIndex = 4;
			this.check_SpecialCalculation.Text = "特殊計算";
			this.check_SpecialCalculation.UseVisualStyleBackColor = true;
			// 
			// check_DatxRecovery
			// 
			this.check_DatxRecovery.AutoSize = true;
			this.check_DatxRecovery.Location = new System.Drawing.Point(348, 248);
			this.check_DatxRecovery.Name = "check_DatxRecovery";
			this.check_DatxRecovery.Size = new System.Drawing.Size(72, 16);
			this.check_DatxRecovery.TabIndex = 4;
			this.check_DatxRecovery.Text = "Datx復元";
			this.check_DatxRecovery.UseVisualStyleBackColor = true;
			// 
			// check_QR
			// 
			this.check_QR.AutoSize = true;
			this.check_QR.Location = new System.Drawing.Point(15, 270);
			this.check_QR.Name = "check_QR";
			this.check_QR.Size = new System.Drawing.Size(60, 16);
			this.check_QR.TabIndex = 4;
			this.check_QR.Text = "携帯QR";
			this.check_QR.UseVisualStyleBackColor = true;
			// 
			// check_CreateSumFile
			// 
			this.check_CreateSumFile.AutoSize = true;
			this.check_CreateSumFile.Location = new System.Drawing.Point(117, 270);
			this.check_CreateSumFile.Name = "check_CreateSumFile";
			this.check_CreateSumFile.Size = new System.Drawing.Size(114, 16);
			this.check_CreateSumFile.TabIndex = 4;
			this.check_CreateSumFile.Text = "SUMファイル作成";
			this.check_CreateSumFile.UseVisualStyleBackColor = true;
			// 
			// check_SymbolOptions
			// 
			this.check_SymbolOptions.AutoSize = true;
			this.check_SymbolOptions.Location = new System.Drawing.Point(15, 313);
			this.check_SymbolOptions.Name = "check_SymbolOptions";
			this.check_SymbolOptions.Size = new System.Drawing.Size(108, 16);
			this.check_SymbolOptions.TabIndex = 4;
			this.check_SymbolOptions.Text = "絵符オプション";
			this.check_SymbolOptions.UseVisualStyleBackColor = true;
			this.check_SymbolOptions.CheckedChanged += new System.EventHandler(this.check_SymbolOptions_CheckedChanged);
			// 
			// lb_UseEFStartDay
			// 
			this.lb_UseEFStartDay.AutoSize = true;
			this.lb_UseEFStartDay.Location = new System.Drawing.Point(11, 336);
			this.lb_UseEFStartDay.Name = "lb_UseEFStartDay";
			this.lb_UseEFStartDay.Size = new System.Drawing.Size(77, 12);
			this.lb_UseEFStartDay.TabIndex = 0;
			this.lb_UseEFStartDay.Text = "利用開始日：";
			// 
			// lb_UseEFEndDay
			// 
			this.lb_UseEFEndDay.AutoSize = true;
			this.lb_UseEFEndDay.Location = new System.Drawing.Point(249, 333);
			this.lb_UseEFEndDay.Name = "lb_UseEFEndDay";
			this.lb_UseEFEndDay.Size = new System.Drawing.Size(77, 12);
			this.lb_UseEFEndDay.TabIndex = 0;
			this.lb_UseEFEndDay.Text = "利用終了日：";
			// 
			// date_UseEFStartDay
			// 
			this.date_UseEFStartDay.Location = new System.Drawing.Point(82, 330);
			this.date_UseEFStartDay.Name = "date_UseEFStartDay";
			this.date_UseEFStartDay.Size = new System.Drawing.Size(130, 21);
			this.date_UseEFStartDay.TabIndex = 3;
			// 
			// date_UseEFEndDay
			// 
			this.date_UseEFEndDay.Location = new System.Drawing.Point(320, 327);
			this.date_UseEFEndDay.Name = "date_UseEFEndDay";
			this.date_UseEFEndDay.Size = new System.Drawing.Size(130, 21);
			this.date_UseEFEndDay.TabIndex = 3;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.check_10);
			this.groupBox1.Controls.Add(this.check_09);
			this.groupBox1.Controls.Add(this.check_08);
			this.groupBox1.Controls.Add(this.check_07);
			this.groupBox1.Controls.Add(this.check_06);
			this.groupBox1.Controls.Add(this.check_05);
			this.groupBox1.Controls.Add(this.check_04);
			this.groupBox1.Controls.Add(this.check_03);
			this.groupBox1.Controls.Add(this.check_02);
			this.groupBox1.Controls.Add(this.check_01);
			this.groupBox1.Controls.Add(this.cmb_DINCAD);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.txt_CADVersion);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.txt_CADTarget);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.date_CADOPUseEndDay);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Location = new System.Drawing.Point(12, 373);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(470, 131);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "DINCAD";
			// 
			// check_10
			// 
			this.check_10.AutoSize = true;
			this.check_10.Location = new System.Drawing.Point(385, 107);
			this.check_10.Name = "check_10";
			this.check_10.Size = new System.Drawing.Size(36, 16);
			this.check_10.TabIndex = 4;
			this.check_10.Text = "10";
			this.check_10.UseVisualStyleBackColor = true;
			// 
			// check_09
			// 
			this.check_09.AutoSize = true;
			this.check_09.Location = new System.Drawing.Point(345, 107);
			this.check_09.Name = "check_09";
			this.check_09.Size = new System.Drawing.Size(36, 16);
			this.check_09.TabIndex = 4;
			this.check_09.Text = "09";
			this.check_09.UseVisualStyleBackColor = true;
			// 
			// check_08
			// 
			this.check_08.AutoSize = true;
			this.check_08.Location = new System.Drawing.Point(303, 107);
			this.check_08.Name = "check_08";
			this.check_08.Size = new System.Drawing.Size(36, 16);
			this.check_08.TabIndex = 4;
			this.check_08.Text = "08";
			this.check_08.UseVisualStyleBackColor = true;
			// 
			// check_07
			// 
			this.check_07.AutoSize = true;
			this.check_07.Location = new System.Drawing.Point(261, 107);
			this.check_07.Name = "check_07";
			this.check_07.Size = new System.Drawing.Size(36, 16);
			this.check_07.TabIndex = 4;
			this.check_07.Text = "07";
			this.check_07.UseVisualStyleBackColor = true;
			// 
			// check_06
			// 
			this.check_06.AutoSize = true;
			this.check_06.Location = new System.Drawing.Point(219, 107);
			this.check_06.Name = "check_06";
			this.check_06.Size = new System.Drawing.Size(36, 16);
			this.check_06.TabIndex = 4;
			this.check_06.Text = "06";
			this.check_06.UseVisualStyleBackColor = true;
			// 
			// check_05
			// 
			this.check_05.AutoSize = true;
			this.check_05.Location = new System.Drawing.Point(177, 107);
			this.check_05.Name = "check_05";
			this.check_05.Size = new System.Drawing.Size(36, 16);
			this.check_05.TabIndex = 4;
			this.check_05.Text = "05";
			this.check_05.UseVisualStyleBackColor = true;
			// 
			// check_04
			// 
			this.check_04.AutoSize = true;
			this.check_04.Location = new System.Drawing.Point(139, 107);
			this.check_04.Name = "check_04";
			this.check_04.Size = new System.Drawing.Size(36, 16);
			this.check_04.TabIndex = 4;
			this.check_04.Text = "04";
			this.check_04.UseVisualStyleBackColor = true;
			// 
			// check_03
			// 
			this.check_03.AutoSize = true;
			this.check_03.Location = new System.Drawing.Point(97, 107);
			this.check_03.Name = "check_03";
			this.check_03.Size = new System.Drawing.Size(36, 16);
			this.check_03.TabIndex = 4;
			this.check_03.Text = "03";
			this.check_03.UseVisualStyleBackColor = true;
			// 
			// check_02
			// 
			this.check_02.AutoSize = true;
			this.check_02.Location = new System.Drawing.Point(55, 107);
			this.check_02.Name = "check_02";
			this.check_02.Size = new System.Drawing.Size(36, 16);
			this.check_02.TabIndex = 4;
			this.check_02.Text = "02";
			this.check_02.UseVisualStyleBackColor = true;
			// 
			// check_01
			// 
			this.check_01.AutoSize = true;
			this.check_01.Location = new System.Drawing.Point(13, 107);
			this.check_01.Name = "check_01";
			this.check_01.Size = new System.Drawing.Size(36, 16);
			this.check_01.TabIndex = 4;
			this.check_01.Text = "01";
			this.check_01.UseVisualStyleBackColor = true;
			// 
			// cmb_DINCAD
			// 
			this.cmb_DINCAD.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cmb_DINCAD.FormattingEnabled = true;
			this.cmb_DINCAD.Location = new System.Drawing.Point(51, 19);
			this.cmb_DINCAD.Name = "cmb_DINCAD";
			this.cmb_DINCAD.Size = new System.Drawing.Size(87, 22);
			this.cmb_DINCAD.TabIndex = 1;
			this.cmb_DINCAD.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_DINCAD_DrawItem);
			this.cmb_DINCAD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_DINCAD_KeyDown);
			this.cmb_DINCAD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_DINCAD_KeyPress);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(9, 58);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(89, 12);
			this.label14.TabIndex = 0;
			this.label14.Text = "追加オプション";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(9, 23);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(47, 12);
			this.label11.TabIndex = 0;
			this.label11.Text = "Level：";
			// 
			// txt_CADVersion
			// 
			this.txt_CADVersion.Location = new System.Drawing.Point(385, 19);
			this.txt_CADVersion.Name = "txt_CADVersion";
			this.txt_CADVersion.Size = new System.Drawing.Size(67, 21);
			this.txt_CADVersion.TabIndex = 2;
			this.txt_CADVersion.TextChanged += new System.EventHandler(this.txt_CADVer_TextChanged);
			this.txt_CADVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_CADVer_KeyPress);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(288, 23);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(101, 12);
			this.label13.TabIndex = 0;
			this.label13.Text = "対象バージョン：";
			// 
			// txt_CADTarget
			// 
			this.txt_CADTarget.Location = new System.Drawing.Point(206, 20);
			this.txt_CADTarget.Name = "txt_CADTarget";
			this.txt_CADTarget.Size = new System.Drawing.Size(67, 21);
			this.txt_CADTarget.TabIndex = 2;
			this.txt_CADTarget.TextChanged += new System.EventHandler(this.txt_CADTarget_TextChanged);
			this.txt_CADTarget.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_CADTarget_KeyPress);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(153, 24);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(59, 12);
			this.label12.TabIndex = 0;
			this.label12.Text = "対象CAD：";
			// 
			// date_CADOPUseEndDay
			// 
			this.date_CADOPUseEndDay.Location = new System.Drawing.Point(89, 73);
			this.date_CADOPUseEndDay.Name = "date_CADOPUseEndDay";
			this.date_CADOPUseEndDay.Size = new System.Drawing.Size(130, 21);
			this.date_CADOPUseEndDay.TabIndex = 3;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(11, 79);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(77, 12);
			this.label15.TabIndex = 0;
			this.label15.Text = "利用終了日：";
			// 
			// check_TPM
			// 
			this.check_TPM.AutoSize = true;
			this.check_TPM.Location = new System.Drawing.Point(15, 510);
			this.check_TPM.Name = "check_TPM";
			this.check_TPM.Size = new System.Drawing.Size(42, 16);
			this.check_TPM.TabIndex = 4;
			this.check_TPM.Text = "TPM";
			this.check_TPM.UseVisualStyleBackColor = true;
			// 
			// check_DINsubcon
			// 
			this.check_DINsubcon.AutoSize = true;
			this.check_DINsubcon.Location = new System.Drawing.Point(82, 510);
			this.check_DINsubcon.Name = "check_DINsubcon";
			this.check_DINsubcon.Size = new System.Drawing.Size(72, 16);
			this.check_DINsubcon.TabIndex = 4;
			this.check_DINsubcon.Text = "サブコン";
			this.check_DINsubcon.UseVisualStyleBackColor = true;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(16, 542);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(41, 12);
			this.label16.TabIndex = 0;
			this.label16.Text = "備考：";
			// 
			// txt_Notes
			// 
			this.txt_Notes.Location = new System.Drawing.Point(55, 542);
			this.txt_Notes.MaxLength = 1200;
			this.txt_Notes.Multiline = true;
			this.txt_Notes.Name = "txt_Notes";
			this.txt_Notes.Size = new System.Drawing.Size(427, 87);
			this.txt_Notes.TabIndex = 6;
			// 
			// btn_OK
			// 
			this.btn_OK.Location = new System.Drawing.Point(151, 645);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Size = new System.Drawing.Size(98, 23);
			this.btn_OK.TabIndex = 7;
			this.btn_OK.Text = "実行";
			this.btn_OK.UseVisualStyleBackColor = true;
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_UpdateKeyRelease
			// 
			this.btn_UpdateKeyRelease.Location = new System.Drawing.Point(270, 645);
			this.btn_UpdateKeyRelease.Name = "btn_UpdateKeyRelease";
			this.btn_UpdateKeyRelease.Size = new System.Drawing.Size(98, 23);
			this.btn_UpdateKeyRelease.TabIndex = 7;
			this.btn_UpdateKeyRelease.Text = "更新キー発行";
			this.btn_UpdateKeyRelease.UseVisualStyleBackColor = true;
			this.btn_UpdateKeyRelease.Click += new System.EventHandler(this.btn_UpdateKeyRelease_Click);
			// 
			// btn_Close
			// 
			this.btn_Close.Location = new System.Drawing.Point(384, 645);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Size = new System.Drawing.Size(98, 23);
			this.btn_Close.TabIndex = 7;
			this.btn_Close.Text = "閉じる";
			this.btn_Close.UseVisualStyleBackColor = true;
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.rdb_malfunction);
			this.panel1.Controls.Add(this.rdb_Active);
			this.panel1.Controls.Add(this.rdb_cancel);
			this.panel1.Controls.Add(this.rdb_loss);
			this.panel1.Location = new System.Drawing.Point(77, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(323, 28);
			this.panel1.TabIndex = 9;
			// 
			// EUsb
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 686);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btn_Close);
			this.Controls.Add(this.btn_UpdateKeyRelease);
			this.Controls.Add(this.btn_OK);
			this.Controls.Add(this.txt_Notes);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.check_DatxRecovery);
			this.Controls.Add(this.check_SpecialCalculation);
			this.Controls.Add(this.check_HunchInput);
			this.Controls.Add(this.check_CreateSumFile);
			this.Controls.Add(this.check_SymbolOptions);
			this.Controls.Add(this.check_QR);
			this.Controls.Add(this.check_Weight);
			this.Controls.Add(this.check_CADGetInfo);
			this.Controls.Add(this.check_DINsubcon);
			this.Controls.Add(this.check_TPM);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.date_UseEFEndDay);
			this.Controls.Add(this.date_UseEndDay);
			this.Controls.Add(this.date_UseEFStartDay);
			this.Controls.Add(this.date_UseStartDay);
			this.Controls.Add(this.txt_Tel);
			this.Controls.Add(this.txt_Mail);
			this.Controls.Add(this.txt_UserName);
			this.Controls.Add(this.lb_UseEFEndDay);
			this.Controls.Add(this.txt_Company);
			this.Controls.Add(this.lb_UseEndDay);
			this.Controls.Add(this.lb_UseEFStartDay);
			this.Controls.Add(this.txt_KeyID);
			this.Controls.Add(this.lb_UseStartDay);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.Name = "EUsb";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "eUSB";
			this.Load += new System.EventHandler(this.EUsb_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton rdb_Active;
		private System.Windows.Forms.RadioButton rdb_malfunction;
		private System.Windows.Forms.RadioButton rdb_loss;
		private System.Windows.Forms.RadioButton rdb_cancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_KeyID;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_Company;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txt_UserName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txt_Mail;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txt_Tel;
		private System.Windows.Forms.Label lb_UseStartDay;
		private System.Windows.Forms.DateTimePicker date_UseStartDay;
		private System.Windows.Forms.Label lb_UseEndDay;
		private System.Windows.Forms.DateTimePicker date_UseEndDay;
		private System.Windows.Forms.CheckBox check_CADGetInfo;
		private System.Windows.Forms.CheckBox check_Weight;
		private System.Windows.Forms.CheckBox check_HunchInput;
		private System.Windows.Forms.CheckBox check_SpecialCalculation;
		private System.Windows.Forms.CheckBox check_DatxRecovery;
		private System.Windows.Forms.CheckBox check_QR;
		private System.Windows.Forms.CheckBox check_CreateSumFile;
		private System.Windows.Forms.CheckBox check_SymbolOptions;
		private System.Windows.Forms.Label lb_UseEFStartDay;
		private System.Windows.Forms.Label lb_UseEFEndDay;
		private System.Windows.Forms.DateTimePicker date_UseEFStartDay;
		private System.Windows.Forms.DateTimePicker date_UseEFEndDay;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cmb_DINCAD;
		private System.Windows.Forms.TextBox txt_CADTarget;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txt_CADVersion;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.DateTimePicker date_CADOPUseEndDay;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox check_01;
		private System.Windows.Forms.CheckBox check_02;
		private System.Windows.Forms.CheckBox check_03;
		private System.Windows.Forms.CheckBox check_04;
		private System.Windows.Forms.CheckBox check_05;
		private System.Windows.Forms.CheckBox check_06;
		private System.Windows.Forms.CheckBox check_07;
		private System.Windows.Forms.CheckBox check_08;
		private System.Windows.Forms.CheckBox check_09;
		private System.Windows.Forms.CheckBox check_10;
		private System.Windows.Forms.CheckBox check_TPM;
		private System.Windows.Forms.CheckBox check_DINsubcon;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txt_Notes;
		private System.Windows.Forms.Button btn_OK;
		private System.Windows.Forms.Button btn_UpdateKeyRelease;
		private System.Windows.Forms.Button btn_Close;
		private System.Windows.Forms.Panel panel1;
	}
}