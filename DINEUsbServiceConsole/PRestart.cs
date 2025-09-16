using CommonLib;
using System;
using System.ComponentModel;
using System.ServiceProcess;
using System.Windows.Forms;

namespace DINEUsbServiceConsole
{
    public partial class PRestart : Form
    {
        /// <summary>
        /// バックグランド処理完了フラグ
        /// </summary>
        private bool _bOk = false;
        /// <summary>
        /// 再起動するフラグ
        /// </summary>
        private bool _bRestart = true;

		/// <summary>
		/// 変更前の証明書
		/// </summary>
		public string OldCertificateName { get; set; } = "";
		/// <summary>
		/// サービス再起動コントロール画面のインスタンスを構成する
		/// </summary>
		/// <param name="bRestart">再起動フラグ</param>
		public PRestart(bool bRestart)
        {
            _bRestart = bRestart;
            InitializeComponent();
        }

        /// <summary>
        /// 画面を初期化する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PRestart_Load(object sender, EventArgs e)
        {
            try
            {
                Label.CheckForIllegalCrossThreadCalls = false;
                ProgressBar.CheckForIllegalCrossThreadCalls = false;
                bgwWorker.RunWorkerAsync(200);
            }
            catch
            {
            }
        }

        /// <summary>
        /// バックグランド操作処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bw = sender as BackgroundWorker;

                e.Result = RestartService(bw);

                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// バックグランド操作進行状況処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= pgbRestart.Minimum &&
                e.ProgressPercentage <= pgbRestart.Maximum)
            {
                pgbRestart.Value = e.ProgressPercentage;
            }
        }

        /// <summary>
        /// バックグランド操作完了またはキャンセル時の完了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled)
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(e.Error.Message, "サービス コントロール", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (e.Result is bool && (bool)e.Result == false)
                    {
                        if (_bRestart)
                        {
                            MessageBox.Show("サービスを再起動できません。システム管理者にご連絡ください。", "サービス コントロール", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("サービスを停止できません。システム管理者にご連絡ください。", "サービス コントロール", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    _bOk = true;
                    Close();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// サービス　再起動を取り消す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// サービス　コントロール画面を終了直前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PRestart_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_bOk)
                {
                    if (bgwWorker.IsBusy)
                    {
                        bgwWorker.CancelAsync();
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
            }
        }

        /// <summary>
        /// サービスを再起動する
        /// </summary>
        /// <param name="bw">バックグランドスレッド</param>
        /// <returns>再起動成功：True、その以外：False</returns>
        private bool RestartService(BackgroundWorker bw)
        {
            bool bReturn = false;
            try
            {
                while (!bw.CancellationPending)
                {
                    // サービスを停止する
                    lblMsg.Text = "サービスを停止しようとしています…";
                    bw.ReportProgress(10);
                    ServiceController scService = new ServiceController(CmConst.ServiceName);
                    bw.ReportProgress(20);
                    if (scService.CanStop)
                    {
                        scService.Stop();
                    }
                    bw.ReportProgress(40);
                    scService.WaitForStatus(ServiceControllerStatus.Stopped);
                    bw.ReportProgress(60);
                    if (_bRestart)
                    {
                        // サービスを開始する
                        lblMsg.Text = "サービスを開始しようとしています…";
						string[] args = string.IsNullOrEmpty(OldCertificateName) ? new string[] { "" } : new string[] { OldCertificateName };
						scService.Start(args);
						bw.ReportProgress(80);
                        scService.WaitForStatus(ServiceControllerStatus.Running);
                    }
                    bw.ReportProgress(100);
                    bReturn = true;
                    break;
                }
            }
            catch(Exception		ex)
            {
            }
            return bReturn;
        }

    }
}