namespace DINEUsbServiceConsole
{
    partial class PMain
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PMain));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtRemotingObjectVersion = new System.Windows.Forms.TextBox();
			this.lblRemotingObjectVersion = new System.Windows.Forms.Label();
			this.txtServiceVersion = new System.Windows.Forms.TextBox();
			this.lblServiceVersion = new System.Windows.Forms.Label();
			this.txtServiceStatus = new System.Windows.Forms.TextBox();
			this.lblServiceStatus = new System.Windows.Forms.Label();
			this.btnStop = new System.Windows.Forms.Button();
			this.lblLine1 = new System.Windows.Forms.Label();
			this.lblLine2 = new System.Windows.Forms.Label();
			this.picTitle = new System.Windows.Forms.PictureBox();
			this.tapSystem = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCertificateName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudRemotePortSSL = new System.Windows.Forms.NumericUpDown();
			this.lblRemotePort = new System.Windows.Forms.Label();
			this.nudRemotePort = new System.Windows.Forms.NumericUpDown();
			this.tapDatabase = new System.Windows.Forms.TabPage();
			this.txtDataConnectionString = new System.Windows.Forms.TextBox();
			this.lblDataConnectionString = new System.Windows.Forms.Label();
			this.btnTest = new System.Windows.Forms.Button();
			this.tabSettings = new System.Windows.Forms.TabControl();
			((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
			this.tapSystem.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudRemotePortSSL)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRemotePort)).BeginInit();
			this.tapDatabase.SuspendLayout();
			this.tabSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(228, 403);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 200;
			this.btnOk.Text = "開始(&S)";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(404, 403);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 210;
			this.btnClose.Text = "閉じる(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = System.Drawing.Color.White;
			this.lblTitle.Font = new System.Drawing.Font("ＭＳ ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.Location = new System.Drawing.Point(6, 20);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(473, 21);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "eUSBキー管理システム サービス コンソール";
			// 
			// txtRemotingObjectVersion
			// 
			this.txtRemotingObjectVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtRemotingObjectVersion.Location = new System.Drawing.Point(208, 359);
			this.txtRemotingObjectVersion.Name = "txtRemotingObjectVersion";
			this.txtRemotingObjectVersion.ReadOnly = true;
			this.txtRemotingObjectVersion.Size = new System.Drawing.Size(216, 19);
			this.txtRemotingObjectVersion.TabIndex = 190;
			this.txtRemotingObjectVersion.TabStop = false;
			// 
			// lblRemotingObjectVersion
			// 
			this.lblRemotingObjectVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblRemotingObjectVersion.AutoSize = true;
			this.lblRemotingObjectVersion.Location = new System.Drawing.Point(25, 362);
			this.lblRemotingObjectVersion.Name = "lblRemotingObjectVersion";
			this.lblRemotingObjectVersion.Size = new System.Drawing.Size(177, 12);
			this.lblRemotingObjectVersion.TabIndex = 224;
			this.lblRemotingObjectVersion.Text = "リモーティングオブジェクトのバージョン：";
			// 
			// txtServiceVersion
			// 
			this.txtServiceVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtServiceVersion.Location = new System.Drawing.Point(208, 334);
			this.txtServiceVersion.Name = "txtServiceVersion";
			this.txtServiceVersion.ReadOnly = true;
			this.txtServiceVersion.Size = new System.Drawing.Size(216, 19);
			this.txtServiceVersion.TabIndex = 180;
			this.txtServiceVersion.TabStop = false;
			// 
			// lblServiceVersion
			// 
			this.lblServiceVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblServiceVersion.AutoSize = true;
			this.lblServiceVersion.Location = new System.Drawing.Point(39, 337);
			this.lblServiceVersion.Name = "lblServiceVersion";
			this.lblServiceVersion.Size = new System.Drawing.Size(163, 12);
			this.lblServiceVersion.TabIndex = 223;
			this.lblServiceVersion.Text = "リモーティングサービスのバージョン：";
			// 
			// txtServiceStatus
			// 
			this.txtServiceStatus.Location = new System.Drawing.Point(137, 82);
			this.txtServiceStatus.Name = "txtServiceStatus";
			this.txtServiceStatus.ReadOnly = true;
			this.txtServiceStatus.Size = new System.Drawing.Size(216, 19);
			this.txtServiceStatus.TabIndex = 5;
			this.txtServiceStatus.TabStop = false;
			// 
			// lblServiceStatus
			// 
			this.lblServiceStatus.AutoSize = true;
			this.lblServiceStatus.Location = new System.Drawing.Point(49, 85);
			this.lblServiceStatus.Name = "lblServiceStatus";
			this.lblServiceStatus.Size = new System.Drawing.Size(82, 12);
			this.lblServiceStatus.TabIndex = 226;
			this.lblServiceStatus.Text = "サービスの状態：";
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.Location = new System.Drawing.Point(309, 403);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 205;
			this.btnStop.Text = "停止(&T)";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// lblLine1
			// 
			this.lblLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLine1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLine1.Location = new System.Drawing.Point(-1, 64);
			this.lblLine1.Name = "lblLine1";
			this.lblLine1.Size = new System.Drawing.Size(492, 2);
			this.lblLine1.TabIndex = 227;
			// 
			// lblLine2
			// 
			this.lblLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblLine2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLine2.Location = new System.Drawing.Point(-1, 390);
			this.lblLine2.Name = "lblLine2";
			this.lblLine2.Size = new System.Drawing.Size(492, 2);
			this.lblLine2.TabIndex = 228;
			// 
			// picTitle
			// 
			this.picTitle.BackColor = System.Drawing.Color.White;
			this.picTitle.Location = new System.Drawing.Point(-1, -1);
			this.picTitle.Name = "picTitle";
			this.picTitle.Size = new System.Drawing.Size(493, 67);
			this.picTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picTitle.TabIndex = 2;
			this.picTitle.TabStop = false;
			// 
			// tapSystem
			// 
			this.tapSystem.Controls.Add(this.label2);
			this.tapSystem.Controls.Add(this.txtCertificateName);
			this.tapSystem.Controls.Add(this.label1);
			this.tapSystem.Controls.Add(this.nudRemotePortSSL);
			this.tapSystem.Controls.Add(this.lblRemotePort);
			this.tapSystem.Controls.Add(this.nudRemotePort);
			this.tapSystem.Location = new System.Drawing.Point(4, 22);
			this.tapSystem.Name = "tapSystem";
			this.tapSystem.Padding = new System.Windows.Forms.Padding(3);
			this.tapSystem.Size = new System.Drawing.Size(459, 183);
			this.tapSystem.TabIndex = 4;
			this.tapSystem.Text = "リモート";
			this.tapSystem.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(40, 66);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 12);
			this.label2.TabIndex = 226;
			this.label2.Text = "証明書名：";
			// 
			// txtCertificateName
			// 
			this.txtCertificateName.Location = new System.Drawing.Point(110, 63);
			this.txtCertificateName.MaxLength = 40;
			this.txtCertificateName.Name = "txtCertificateName";
			this.txtCertificateName.Size = new System.Drawing.Size(157, 19);
			this.txtCertificateName.TabIndex = 225;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(2, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 12);
			this.label1.TabIndex = 224;
			this.label1.Text = "リモート ポートSSL：";
			// 
			// nudRemotePortSSL
			// 
			this.nudRemotePortSSL.Location = new System.Drawing.Point(110, 38);
			this.nudRemotePortSSL.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudRemotePortSSL.Name = "nudRemotePortSSL";
			this.nudRemotePortSSL.Size = new System.Drawing.Size(76, 19);
			this.nudRemotePortSSL.TabIndex = 223;
			this.nudRemotePortSSL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblRemotePort
			// 
			this.lblRemotePort.AutoSize = true;
			this.lblRemotePort.Location = new System.Drawing.Point(22, 15);
			this.lblRemotePort.Name = "lblRemotePort";
			this.lblRemotePort.Size = new System.Drawing.Size(77, 12);
			this.lblRemotePort.TabIndex = 222;
			this.lblRemotePort.Text = "リモート ポート：";
			// 
			// nudRemotePort
			// 
			this.nudRemotePort.Location = new System.Drawing.Point(110, 13);
			this.nudRemotePort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudRemotePort.Name = "nudRemotePort";
			this.nudRemotePort.Size = new System.Drawing.Size(76, 19);
			this.nudRemotePort.TabIndex = 10;
			this.nudRemotePort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tapDatabase
			// 
			this.tapDatabase.Controls.Add(this.txtDataConnectionString);
			this.tapDatabase.Controls.Add(this.lblDataConnectionString);
			this.tapDatabase.Controls.Add(this.btnTest);
			this.tapDatabase.Location = new System.Drawing.Point(4, 22);
			this.tapDatabase.Name = "tapDatabase";
			this.tapDatabase.Padding = new System.Windows.Forms.Padding(3);
			this.tapDatabase.Size = new System.Drawing.Size(459, 183);
			this.tapDatabase.TabIndex = 2;
			this.tapDatabase.Text = "データ接続";
			this.tapDatabase.UseVisualStyleBackColor = true;
			// 
			// txtDataConnectionString
			// 
			this.txtDataConnectionString.Location = new System.Drawing.Point(11, 30);
			this.txtDataConnectionString.MaxLength = 512;
			this.txtDataConnectionString.Name = "txtDataConnectionString";
			this.txtDataConnectionString.Size = new System.Drawing.Size(427, 19);
			this.txtDataConnectionString.TabIndex = 10;
			// 
			// lblDataConnectionString
			// 
			this.lblDataConnectionString.AutoSize = true;
			this.lblDataConnectionString.Location = new System.Drawing.Point(9, 15);
			this.lblDataConnectionString.Name = "lblDataConnectionString";
			this.lblDataConnectionString.Size = new System.Drawing.Size(128, 12);
			this.lblDataConnectionString.TabIndex = 213;
			this.lblDataConnectionString.Text = "データベース接続文字列：";
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(345, 58);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(97, 23);
			this.btnTest.TabIndex = 100;
			this.btnTest.Text = "接続の確認";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// tabSettings
			// 
			this.tabSettings.Controls.Add(this.tapDatabase);
			this.tabSettings.Controls.Add(this.tapSystem);
			this.tabSettings.Location = new System.Drawing.Point(12, 115);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.SelectedIndex = 0;
			this.tabSettings.Size = new System.Drawing.Size(467, 209);
			this.tabSettings.TabIndex = 10;
			// 
			// PMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(491, 438);
			this.Controls.Add(this.tabSettings);
			this.Controls.Add(this.lblLine2);
			this.Controls.Add(this.lblLine1);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.txtServiceStatus);
			this.Controls.Add(this.lblServiceStatus);
			this.Controls.Add(this.txtRemotingObjectVersion);
			this.Controls.Add(this.lblRemotingObjectVersion);
			this.Controls.Add(this.txtServiceVersion);
			this.Controls.Add(this.lblServiceVersion);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.picTitle);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "配筋検査 サービス コンソール";
			this.Load += new System.EventHandler(this.PMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
			this.tapSystem.ResumeLayout(false);
			this.tapSystem.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudRemotePortSSL)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudRemotePort)).EndInit();
			this.tapDatabase.ResumeLayout(false);
			this.tapDatabase.PerformLayout();
			this.tabSettings.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtRemotingObjectVersion;
        private System.Windows.Forms.Label lblRemotingObjectVersion;
        private System.Windows.Forms.TextBox txtServiceVersion;
        private System.Windows.Forms.Label lblServiceVersion;
        private System.Windows.Forms.TextBox txtServiceStatus;
        private System.Windows.Forms.Label lblServiceStatus;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblLine1;
        private System.Windows.Forms.Label lblLine2;
        private System.Windows.Forms.PictureBox picTitle;
        private System.Windows.Forms.TabPage tapSystem;
        private System.Windows.Forms.Label lblRemotePort;
        private System.Windows.Forms.NumericUpDown nudRemotePort;
        private System.Windows.Forms.TabPage tapDatabase;
        private System.Windows.Forms.TextBox txtDataConnectionString;
        private System.Windows.Forms.Label lblDataConnectionString;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabControl tabSettings;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudRemotePortSSL;
		private System.Windows.Forms.TextBox txtCertificateName;
		private System.Windows.Forms.Label label2;
	}
}

