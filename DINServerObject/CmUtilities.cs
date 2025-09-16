using System;
using System.Diagnostics;

namespace DINServerObject
{
    /// <summary>
    /// �c�[����\���N���X
    /// </summary>
    public class CmUtilities
    {
        #region �t�B�[���h

        /// <summary>
        /// �C�x���g���O�o�͗pEventLog�I�u�W�F�N�g
        /// </summary>
        private static string s_logSource = "DiNetWorks";

        #endregion

        #region ���J�@���\�b�h

        /// <summary>
        ///�@�C�x���g���O�̐ݒ�
        /// </summary>
        /// <param name="logSource">�C�x���g���O�̃C���X�^���X</param>
        public static void SetLog(string logSource) {
            s_logSource = logSource;
            try {
                if (!EventLog.SourceExists(s_logSource)) {
                    EventLog.CreateEventSource(s_logSource, "Application");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Windows�̃C�x���g ���O�Ƀ��O�������o��
        /// </summary>
        public static void Log(string msg)
        {
            EventLog.WriteEntry(s_logSource, msg);
        }

        /// <summary>
        /// Windows�̃C�x���g ���O�Ƀ��O�������o��
        /// </summary>
        public static void Log(Exception exp)
        {
            EventLog.WriteEntry(s_logSource, "Exception: " + exp.Message + "\n" + exp.GetType() +
                                             "\nStack Trace:\n" + exp.StackTrace);
        }

        #endregion

        #region �v���C�x�[�g�@���\�b�h

        /// <summary>
        /// �V�K�C���X�^���X�̍쐬
        /// </summary>
        private CmUtilities()
        {
        }

        #endregion
    }
}