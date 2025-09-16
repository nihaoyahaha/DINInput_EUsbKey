using CommonLib;
using DI.NCFrameWork;
using Microsoft.Win32;
using NC.NCFrameWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceProcess;
using System.Windows.Forms;

namespace DINEUsbServiceConsole
{
    public partial class PMain : Form
    {
        #region private 定数
        // メッセージのタイトル
        public const string MSG_CAPTION = "サービス コントロール";
        #endregion

        #region private 変数
        /// <summary>
        /// サービスの状態の配列
        /// </summary>
        private string[] ServiceStatus = {"", "停止", "開始中", "停止中", "実行中", 
            "サービスの継続は保留中", "サービスの一時中断は保留中", "一時中断"};
        /// <summary>
        /// 共通ファンクションのインスタンス
        /// </summary>
        private NCPublicFunction PFunc = new NCPublicFunction();

		/// <summary>
		/// 変更前の証明書
		/// </summary>
		private string _oldcCertificateName;
		#endregion

		public PMain()
        {
            InitializeComponent();
        }

        #region private 画面イベント
        /// <summary>
        /// コンソール画面を初期化する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PMain_Load(object sender, EventArgs e)
        {
            try
            {
                ServiceController scService = new ServiceController(CmConst.ServiceName);
                txtServiceStatus.Text = ServiceStatus[(int)scService.Status];

                NCRegistry registry = new NCRegistry(RegistryConst.Reg_Root);
				string strBuf = registry.GetRegistryKeyValue(RegistryConst.Reg_DataConnectionString,
				"Server=172.29.2.241;Port=5433;User Id=postgres;Password=postgre123456;Database=postgres;Encoding=UTF8;CommandTimeout=300");

				txtDataConnectionString.Text = strBuf;

				List<string> config= GetRemotePortByConfig();
				nudRemotePort.Value = PFunc.ObjectToInt(config[0]);
				nudRemotePortSSL.Value = PFunc.ObjectToInt(config[1]);
				txtCertificateName.Text = config[2];

				txtServiceVersion.Text = registry.GetRegistryKeyValue(RegistryConst.Reg_ServiceVersion, "");
                txtRemotingObjectVersion.Text = registry.GetRegistryKeyValue(RegistryConst.Reg_RemotingObjectVersion, "");
				_oldcCertificateName = txtCertificateName.Text;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// <summary>
        /// サービスを再起動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveServerInfo())
                {
                    PRestart pRestart = new PRestart(true);
					pRestart.OldCertificateName = _oldcCertificateName == txtCertificateName.Text ? "" : _oldcCertificateName;
                    if (pRestart.ShowDialog() == DialogResult.OK)
                    {
                        ServiceController scService = new ServiceController(CmConst.ServiceName);
                        txtServiceStatus.Text = ServiceStatus[(int)scService.Status];
                        NCRegistry registry = new NCRegistry(RegistryConst.Reg_Root);
                        txtServiceVersion.Text = registry.GetRegistryKeyValue(RegistryConst.Reg_ServiceVersion, "");
                        txtRemotingObjectVersion.Text = registry.GetRegistryKeyValue(RegistryConst.Reg_RemotingObjectVersion, "");
						_oldcCertificateName = txtCertificateName.Text;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// サービスを停止する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveServerInfo())
                {
                    PRestart pRestart = new PRestart(false);
                    if (pRestart.ShowDialog() == DialogResult.OK)
                    {
                        ServiceController scService = new ServiceController(CmConst.ServiceName);
                        txtServiceStatus.Text = ServiceStatus[(int)scService.Status];
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// サービスコンソール画面を終了する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// データベースの接続をテスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            string strBuf = "";
            strBuf = txtDataConnectionString.Text;
            NCDataConnection.ConnectionString = strBuf;
            NCDataConnection connection = new NCDataConnection();
            try
            {
                if (connection.OpenConnection() != null)
                {
                    MessageBox.Show("データベース接続に成功しました。", MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.CloseConnection();
            }
        }
        #endregion

        #region private メソッド
        /// <summary>
        /// サービス実行ファイルのパスを返す
        /// </summary>
        /// <returns></returns>
        private string GetServiceExecutablePath()
        {
            string strExecutablePath = "";
            try
            {
                string strServiceKey = String.Format(@"{0}\{1}", @"SYSTEM\CurrentControlSet\Services", CmConst.ServiceName);
                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(strServiceKey))
                {
                    object objKey = key.GetValue("ImagePath");
                    if (null != objKey)
                    {
                        strExecutablePath = objKey.ToString();
                        strExecutablePath = strExecutablePath.Trim("\"".ToCharArray());
                    }
                    key.Close();
                }
            }
            catch(Exception ex)
            {
            }
            return strExecutablePath;
        }

        /// <summary>
        /// アプリケーションサーバ情報を保存する
        /// </summary>
        /// <returns></returns>
        private bool SaveServerInfo()
        {
            bool bReturn = false;
            try
            {
                NCRegistry registry = new NCRegistry(RegistryConst.Reg_Root);
                string strValue = "";
                strValue = txtDataConnectionString.Text;
                registry.SetRegistryKeyValue(RegistryConst.Reg_DataConnectionString, strValue);
                
                SetRemotePort((int)nudRemotePort.Value, (int)nudRemotePortSSL.Value,txtCertificateName.Text.Trim());
                bReturn = true;
            }
            catch
            {
            }
            return bReturn;
        }
        private List<string> GetRemotePortByConfig()
        {
			List<string> dicConfig = new List<string>();

            int nRemotePort = 8093;
			int nRemotePortSSL = 443;
			string certificateName = "dininput-cloud.com";
			try
            {
                // リモートポートを取得する
                string strExecutablePath = GetServiceExecutablePath();
                if (!PFunc.IsEmpty(strExecutablePath))
                {
                    string strConfigPath = String.Format("{0}.config", strExecutablePath);
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = strConfigPath;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                    nRemotePort = PFunc.StringToInt(PFunc.GetSettingValue(config.AppSettings.Settings, "channelport"), 8093);
					nRemotePortSSL = PFunc.StringToInt(PFunc.GetSettingValue(config.AppSettings.Settings, "channelportSSL"), 443);
					certificateName = PFunc.GetSettingValue(config.AppSettings.Settings, "certificateName").Trim();
					certificateName = certificateName == "" ? "dininput-cloud.com" : certificateName;

					dicConfig.Add(nRemotePort.ToString());
					dicConfig.Add(nRemotePortSSL.ToString());
					dicConfig.Add(certificateName);
				}
            }
            catch (Exception exp)
            {
                string str = exp.Message;
            }
            return dicConfig;
        }
       
		/// <summary>
		/// リモートポートを設定する
		/// </summary>
		/// <param name="nRemotePort"></param>
		/// <param name="nRemotePortSSL"></param>
		/// <param name="certificateName"></param>
		private void SetRemotePort(int nRemotePort, int nRemotePortSSL,string certificateName)
        {
            try
            {
                // リモートポートを変更する
                string strExecutablePath = GetServiceExecutablePath();
                if (!PFunc.IsEmpty(strExecutablePath))
                {
                    string strConfigPath = String.Format("{0}.config", strExecutablePath);
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = strConfigPath;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                    config.AppSettings.Settings["channelport"].Value = nRemotePort.ToString();
					config.AppSettings.Settings["channelportSSL"].Value = nRemotePortSSL.ToString();
					config.AppSettings.Settings["certificateName"].Value = certificateName;
					config.Save();
                }
            }
            catch
            {
            }
        }
        #endregion       
    }
}