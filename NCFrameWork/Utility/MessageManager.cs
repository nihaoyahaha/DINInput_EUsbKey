//*****************************************************************************
// ���[�U��					�j���[�R��
// �V�X�e����				
// �T�u�V�X�e����			�G���[���b�Z�[�W�}�l�W���[�N���X
// �쐬��					��@NC
// ���œ�					2007/07/19
// ���œ��e					�V�K�쐬
//*****************************************************************************
using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using DI.NCFrameWork;

namespace DI.NCFrameWork
{
	#region ���b�Z�[�W�}�l�[�W���[
	/// <summary>
	/// ���b�Z�[�W�}�l�[�W���[�B
	/// </summary>
	public class MessageManager
	{
        /// <summary>
        /// �A�v���P�[�V����ID
        /// </summary>
		public static string application_id = "MessageID";

		/// <summary>
		/// �C���X�^���X�I�u�W�F�N�g
		/// </summary>
		private static MessageManager messageManager = null;

        private static Hashtable hashMessage = null;

		public MessageManager()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		/// <summary>
		/// �V�����C���X�^���X���쐬���Ă�������
		/// </summary>
		/// <returns>���b�Z�[�W�}�l�[�W���[�C���X�^���X</returns>
		public static MessageManager NewInstance()
		{
			if(messageManager == null)
			{
				messageManager = new MessageManager();
                hashMessage = new Hashtable();
                string strFileName = Path.Combine(Application.StartupPath, "Message","NCMessage.xml");
                GetALLMessage(strFileName);
				return messageManager;
			}
			else
			{
				return messageManager;
			}
		}

		/// <summary>
		/// ���b�Z�[�W���擾
		/// </summary>
		/// <param name="application">�A�v���P�[�V����</param>
		/// <param name="strFileName">�t�@�C����</param>
		/// <returns>bool</returns>
		private static bool GetALLMessage(string strFileName)
		{
			XmlTextReader reader = null;
			try
			{
				if (File.Exists(strFileName))
				{
					reader = new XmlTextReader(strFileName);
					//���[�v��XML�t�@�C�������擾����
					while (reader.Read())
					{
						//�m�[�h�^�C�v�𔻒f����
						string strMessage = "";
						string strCode="";
						switch (reader.NodeType)
						{
								//XML�^�O�̏ꍇ
							case XmlNodeType.Element:
								if (reader.LocalName.ToUpper() == "MESSAGE") 
								{
									if( reader.MoveToFirstAttribute() )
									{
										do
										{
											//���b�Z�[�WID���擾
											if (reader.Name.ToUpper() == "ID") 
											{
												strCode = reader.Value;
											}
										
											//���b�Z�[�W���擾
											if (reader.Name.ToUpper() == "MSG") 
											{
												strMessage = reader.Value;
											
											}
										} while( reader.MoveToNextAttribute() );
                                        if (!hashMessage.Contains(strCode))
                                        {
                                            hashMessage.Add(strCode, strMessage);
                                        }
									}						
								}

								break;
								//�e�L�X�g�̏ꍇ
							case XmlNodeType.Text:
								break;
								//�I���^�O�̏ꍇ
							case XmlNodeType.EndElement:
								break;
						}
					}
				}
			}
			catch(Exception exp)
			{
                NCLogger.GetInstance().WriteExceptionLog(exp);
			}	
			finally
			{
				if (reader != null) 
				{
					reader.Close();
				}
			}
			return true;

		}

		/// <summary>
		/// ���b�Z�[�W�R�[�h�ɉ����āA���b�Z�[�W�̏ڍׂ𓾂Ă��������B
		/// </summary>
		/// <param name="argMessageCode">���b�Z�[�W�R�[�h</param>
		/// <returns>���b�Z�[�W</returns>
		public string GetMessage(string argMessageCode)
		{
			string strMessage = "";

            if (hashMessage != null)
			{
                if (hashMessage.Contains(argMessageCode))
				{
                    object objmessage = hashMessage[argMessageCode];
					if(objmessage != null)
					{
                        strMessage = hashMessage[argMessageCode].ToString();
					}
				}
			}
			return strMessage;
		}
	}
	#endregion
}
