//*****************************************************************************
// ���[�U��					�j���[�R��
// �V�X�e����				
// �T�u�V�X�e����			�x�[�X�t�H�[��
// �쐬��					��@NC
// ���œ�					2007/07/19
// ���œ��e					�V�K�쐬
//*****************************************************************************
namespace DI.NCFrameWork
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// �l�b�g���[�N�A�N�Z�X�N���X�B
    /// ���[�U�h�c�E�p�X���[�h�𗘗p���ăl�b�g���[�N���L�t�H���_���A�N�Z�X����B
    /// </summary>
    public class NetWorkAccess
    {
        [DllImport("mpr.dll", CharSet = CharSet.Ansi)]
        private static extern int WNetAddConnection2(NetResource netResource, string password, string Username, int Flag);
        [DllImport("mpr.dll", CharSet = CharSet.Ansi)]
        private static extern int WNetGetConnection(string strLocalName, string strRemotName, int Length);
        [DllImport("mpr.dll", CharSet = CharSet.Ansi)]
        private static extern int WNetCancelConnection2(string LocalName, int Flag, int Force);


        /// <summary>
        /// �l�b�g���[�N���L�t�H���_�A�N�Z�X���ʁ|�|����
        /// </summary>
        public const int NO_ERROR = 0;

        #region �l�b�g���[�N���L�t�H���_�A�N�Z�X
        //**********************************************************************
        /// <summary>
        /// static int Connect(string remoteName, string userName, string password)
        /// </summary>
        /// <remarks>
        /// ���[�U�h�c�E�p�X���[�h�𗘗p���ăl�b�g���[�N���L�t�H���_���A�N�Z�X����B
        /// </remarks>
        /// <param name="remoteName">�l�b�g���[�N���L�t�H���_�p�X</param>
        /// <param name="userName">���[�U�h�c</param>
        /// <param name="password">�p�X���[�h</param>
        /// <returns>int �A�N�Z�X����</returns>
        /// <example>
        /// <code></code>
        /// </example>
        //**********************************************************************
        public static int Connect(string remoteName, string userName, string password)
        {
            NetResource mappedDrive = new NetResource();
            mappedDrive.dwScope = 2;
            mappedDrive.dwType = 1;
            mappedDrive.dwDisplayType = 3;
            mappedDrive.dwUsage = 1;
            mappedDrive.LocalName = null;
            mappedDrive.RemoteName = remoteName;
            mappedDrive.Provider = null;

            return WNetAddConnection2(mappedDrive, password, userName, 0);
        }
        #endregion

        #region �����̃l�b�g���[�N�ڑ���ؒf����
        //**********************************************************************
        /// <summary>
        /// static int DisConnect(string remoteName)
        /// </summary>
        /// <remarks>
        /// �����̃l�b�g���[�N�ڑ���ؒf����
        /// </remarks>
        /// <param name="remoteName">������</param>
        /// <returns>�����́u0�v</returns>
        /// <example>
        /// <code></code>
        /// </example>
        //**********************************************************************
        public static int DisConnect(string remoteName)
        {
            return WNetCancelConnection2(remoteName, 0, 1);
        }
        #endregion
    }

    /// <summary>
    /// �l�b�g���[�N���\�[�X
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    class NetResource
    {
        /// <summary>
        /// ���\�[�X�͈̔�
        /// </summary>
        public int dwScope;
        /// <summary>
        /// ���\�[�X�̎��
        /// </summary>
        public int dwType;
        /// <summary>
        /// ���\�[�X�̕\���^�C�v
        /// </summary>
        public int dwDisplayType;
        /// <summary>
        /// ���\�[�X�̗p�r
        /// </summary>
        public int dwUsage;
        /// <summary>
        /// ���[�J����
        /// </summary>
        public String LocalName = null;
        /// <summary>
        /// �����[�g���\�[�X��
        /// </summary>
        public String RemoteName = null;
        /// <summary>
        /// �l�b�g���[�N�v���o�C�_�̃R�����g
        /// </summary>
        public String Comment = null;
        /// <summary>
        /// �l�b�g���[�N�v���o�C�_�̖��O
        /// </summary>
        public String Provider = null;
    }

}

