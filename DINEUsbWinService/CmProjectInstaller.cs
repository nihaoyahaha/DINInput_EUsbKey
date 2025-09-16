//******************************************************************************
// ユーザ名					ニューコン
// システム名				配筋検査システム
// サブシステム名			リモートサービス
// 作成者					李登基
// 改版日					2017/03/09
// 改版内容					新規作成
//******************************************************************************
using System;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Windows.Forms;

namespace DINEUsbWinService
{
	[RunInstaller(true)]
    public partial class CmProjectInstaller : Installer
    {

        /// <summary>
        /// インストールの新しいインスタンスを初期化する
        /// </summary>
        public CmProjectInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// サービスインストール後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serviceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            //
            // サービスを立ち上げる
            //
            try
            {
                ServiceController serviceController = new ServiceController();
                serviceController.MachineName = "127.0.0.1";
                serviceController.ServiceName = this.serviceInstaller.ServiceName;
                serviceController.Start();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString(), this.serviceInstaller.DisplayName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// インストールした内容を削除します。 
        /// </summary>
        /// <param name="savedState">インストール後のコンピュータの状態</param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            //
            // サービスの中止
            //
            try
            {
                ServiceController serviceController = new ServiceController();
                serviceController.MachineName = "127.0.0.1";
                serviceController.ServiceName = this.serviceInstaller.ServiceName;
                if (serviceController.CanStop)
                {
                    serviceController.Stop();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, this.serviceInstaller.DisplayName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //
            // サービスを閉じてからUninstallする
            //
            base.Uninstall(savedState);
        }        
    }
}