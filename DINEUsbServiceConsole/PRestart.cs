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
        /// �o�b�N�O�����h���������t���O
        /// </summary>
        private bool _bOk = false;
        /// <summary>
        /// �ċN������t���O
        /// </summary>
        private bool _bRestart = true;

		/// <summary>
		/// �ύX�O�̏ؖ���
		/// </summary>
		public string OldCertificateName { get; set; } = "";
		/// <summary>
		/// �T�[�r�X�ċN���R���g���[����ʂ̃C���X�^���X���\������
		/// </summary>
		/// <param name="bRestart">�ċN���t���O</param>
		public PRestart(bool bRestart)
        {
            _bRestart = bRestart;
            InitializeComponent();
        }

        /// <summary>
        /// ��ʂ�����������
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
        /// �o�b�N�O�����h���쏈��
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
        /// �o�b�N�O�����h����i�s�󋵏���
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
        /// �o�b�N�O�����h���슮���܂��̓L�����Z�����̊�������
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
                        MessageBox.Show(e.Error.Message, "�T�[�r�X �R���g���[��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (e.Result is bool && (bool)e.Result == false)
                    {
                        if (_bRestart)
                        {
                            MessageBox.Show("�T�[�r�X���ċN���ł��܂���B�V�X�e���Ǘ��҂ɂ��A�����������B", "�T�[�r�X �R���g���[��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("�T�[�r�X���~�ł��܂���B�V�X�e���Ǘ��҂ɂ��A�����������B", "�T�[�r�X �R���g���[��", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// �T�[�r�X�@�ċN����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// �T�[�r�X�@�R���g���[����ʂ��I�����O����
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
        /// �T�[�r�X���ċN������
        /// </summary>
        /// <param name="bw">�o�b�N�O�����h�X���b�h</param>
        /// <returns>�ċN�������FTrue�A���̈ȊO�FFalse</returns>
        private bool RestartService(BackgroundWorker bw)
        {
            bool bReturn = false;
            try
            {
                while (!bw.CancellationPending)
                {
                    // �T�[�r�X���~����
                    lblMsg.Text = "�T�[�r�X���~���悤�Ƃ��Ă��܂��c";
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
                        // �T�[�r�X���J�n����
                        lblMsg.Text = "�T�[�r�X���J�n���悤�Ƃ��Ă��܂��c";
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