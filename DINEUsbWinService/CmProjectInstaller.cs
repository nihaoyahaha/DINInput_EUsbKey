//******************************************************************************
// ���[�U��					�j���[�R��
// �V�X�e����				�z�،����V�X�e��
// �T�u�V�X�e����			�����[�g�T�[�r�X
// �쐬��					���o��
// ���œ�					2017/03/09
// ���œ��e					�V�K�쐬
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
        /// �C���X�g�[���̐V�����C���X�^���X������������
        /// </summary>
        public CmProjectInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �T�[�r�X�C���X�g�[���㏈��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serviceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            //
            // �T�[�r�X�𗧂��グ��
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
        /// �C���X�g�[���������e���폜���܂��B 
        /// </summary>
        /// <param name="savedState">�C���X�g�[����̃R���s���[�^�̏��</param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            //
            // �T�[�r�X�̒��~
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
            // �T�[�r�X����Ă���Uninstall����
            //
            base.Uninstall(savedState);
        }        
    }
}