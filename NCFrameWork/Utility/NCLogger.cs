using System.Collections;

namespace DI.NCFrameWork
{
	using System;
    using System.Windows.Forms;
	using System.Data;
	using System.IO;
	using System.Text;
	using System.Configuration;	

	#region ���O�o�̓��x���񋓌^
	//************************************************************************
	/// <summary>
	/// ���O�o�̓��x���񋓌^
	/// <remarks>
	/// ���O�o�̓��x���񋓌^
	/// </remarks>
	/// </summary>
	//************************************************************************
	public enum LogLevel {Error = 1, Warning = 2, Information = 3, Debug = 4};	
	#endregion

    #region ���O�o�̓^�C�v�񋓌^
    //************************************************************************
    /// <summary>
    /// ���O�o�̓^�C�v�񋓌^
    /// <remarks>
    /// ���O�o�̓^�C�v�񋓌^
    /// </remarks>
    /// </summary>
    //************************************************************************
    public enum LogTypeManager { AllUser = 0, PerUser = 1 };
    #endregion

	//************************************************************************
	/// <summary>
	/// ���O�������N���X
	/// �p�X�A�t�@�C�������C���A�g���q�A���O�t�@�C�����A���O�t�@�C���T�C�Y���󂯎��B
	/// �w�肳�ꂽ�p�X�̃t�@�C�����Ƀ��O���o�͂���B
	/// �t�@�C�����w�肳�ꂽ�T�C�Y�ɂ����΁A�V�������O�t�@�C�������B
	/// ���O�t�@�C���̌��͎w�肳�ꂽ���O�t�@�C�����𒴂���ꍇ�A��ԌÂ��t�@�C�����폜�����B
	/// </summary>
	//************************************************************************
	public class NCLogger
	{		
		#region �t�B�[���h
		/// <summary>
		/// ���O���^�C�v�̗񋓌^
		/// </summary>
		public enum LogInfoEnum {Start = 1, End = 2};
		private int    mLevel;
		private string mPath;
		private string mFileMain;
		private string mFileExt;
		private int    mFileCount;
		private int    mFileSize;
		private string mCurrFileNoExt;
		//���݂̃t�@�C�����́@"c:\logger001.txt"�̂悤�ɁA����1�Ԗ�
		private string mCurrFullName;
		//�t�@�C���ϕ����̌����A���w�肳�ꂽ���O�t�@�C�����̌���
		private int		lenSeq;		
		private StreamWriter sw;
		private static NCPublicFunction m_pFunc = new NCPublicFunction();
		private static NCLogger s_me = null;
        private static LogTypeManager _logType = LogTypeManager.PerUser;

		#endregion

        
        #region �R���X�g���N�^
        //************************************************************************
		/// <summary>
		/// ���O�N���X�̍\��
		/// </summary>
		//************************************************************************
		private NCLogger()
		{
			// 
			// �p�����[�^���w�肳��Ă��Ȃ��ꍇ�ADefault�l���w�肷��B
			//
			string strLogPath = "";
			string strLogFile = "";
			string strFileExt = "";
			int iLogSize = 0;
			int iLogCount = 0;
			int iLogLevel = 0;

			GetLogConfig(ref strLogPath, ref strLogFile, ref strFileExt, ref iLogCount, ref iLogSize, ref iLogLevel);

			Create(strLogPath, strLogFile, strFileExt, iLogCount, iLogSize, iLogLevel);
	
		}

		//************************************************************************
		/// <summary>
		/// ���O�N���X�̍\��
		/// </summary>
		/// <param name="path">�t�@�C���p�X</param>
		/// <param name="FileMain">�t�@�C���̖���</param>
		/// <param name="FileExt">�t�@�C���̊g���q</param>
		/// <param name="FileCount">�t�@�C���̐�</param>
		/// <param name="FileSize">�t�@�C���̃T�C�Y</param>
		/// <param name="LogLevel">���O�̃��x��</param>
		//************************************************************************
		private NCLogger(string path, string FileMain, string FileExt,
			int FileCount, int FileSize, int LogLevel)
		{
			Create(path, FileMain, FileExt, FileCount, FileSize, LogLevel);
		}
		#endregion

		#region public ���\�b�h
        //************************************************************************
        /// <summary>
        /// ���O�^�C�v 1:�e���[�U�ʃ��O���Ǘ�����A0:���ׂẴ��[�U�͓������O���Ǘ�����
        /// </summary>
        //************************************************************************
        public static void SetLogType(LogTypeManager LogType)
        {
            _logType = LogType;
        }

		//************************************************************************
		/// <summary>
		/// �C���X�^���X���擾����B
		/// </summary>
		/// <returns>NCLogger�C���X�^���X</returns>
		//************************************************************************
		public static NCLogger GetInstance() 
		{
			if (s_me == null)
			{
				s_me = new NCLogger();				
			}
			if (s_me.sw == null) 
			{
				s_me.Open();
			}
			return s_me;
		}

		//************************************************************************
		/// <summary>
		/// �f�o�b�O���O���o�͂���B
		/// </summary>
		/// <param name="log">���O</param>
		//************************************************************************
		public void WriteDebugLog(string log)
		{
			Write(LogLevel.Debug, log);
		}

		//************************************************************************
		/// <summary>
		/// �����񃍃O���o�͂���B
		/// </summary>
		/// <param name="log">���O</param>
		//************************************************************************
		public void WriteInfoLog(string log)
		{
			Write(LogLevel.Information, log);
		}
		
		//************************************************************************
		/// <summary>
		/// ���\�b�h�̊J�n�^�I�����O���o�͂���B
		/// </summary>
		/// <param name="methodName">���\�b�h�̖���</param>
		/// <param name="logInfo">�J�n�I���敪�F�J�n�|LogInfoEnum.Start�A�I���|LogInfoEnum.End</param>
		//************************************************************************
		public void WriteInfoLog(string methodName, LogInfoEnum logInfo)
		{			
			if (logInfo == LogInfoEnum.Start)
			{
				WriteInfoLog(methodName +  "�@�J�n");
			}	
			else if (logInfo == LogInfoEnum.End)
			{
				WriteInfoLog(methodName +  "�@�I��");
			}
		}

		//************************************************************************
		/// <summary>
		/// �x����񃍃O���o�͂���B
		/// </summary>
		/// <param name="log">���O</param>
		//************************************************************************
		public void WriteWarningLog(string log)
		{
			Write(LogLevel.Warning, log);
		}

		//************************************************************************
		/// <summary>
		/// �G���[��񃍃O���o�͂���B
		/// </summary>
		/// <param name="log">���O</param>
		//************************************************************************
		public void WriteErrorLog(string log)
		{
			Write(LogLevel.Error, log);
		}

		//************************************************************************
		/// <summary>
		/// �ُ��񃍃O���o�͂���B
		/// </summary>
		/// <param name="ex">���O</param>
		//************************************************************************
		public void WriteExceptionLog(Exception ex)
		{
			WriteExceptionLog(ex, false);
		}

		//************************************************************************
		/// <summary>
		/// ��O��񃍃O���o�͂���B
		/// </summary>
		/// <param name="ex">��O�I�u�W�F�N�g</param>
		/// <param name="isShowErrorForm">��O��ʕ\���t���O�Ftrue-�\���Afalse-�\�����Ȃ�</param>
		//************************************************************************
		public void WriteExceptionLog(Exception ex, bool isShowErrorForm)
		{
			Write(ex);
			if (isShowErrorForm)
			{					
                //NdnErrorForm errorForm = new NdnErrorForm(ex);		
                //errorForm.Top = 0;
                //errorForm.ShowDialog();									
			}
		}		

		#region �t�@�C����CLOSE����
		//************************************************************************
		/// <summary>
		/// void Close()
		/// </summary>
		/// <remarks>
		/// �t�@�C����CLOSE����
		/// </remarks>
		/// <returns>�Ȃ�</returns>
		//************************************************************************
		public void Close()
		{
			try
			{
				if(sw != null)
				{
					sw.Close();
				}
				sw = null;
			}
			catch
			{
			}
		}
		#endregion

		#region ���O���t�@�C����FLUSH����
		//************************************************************************
		/// <summary>
		/// void Flush()
		/// </summary>
		/// <remarks>
		/// ���O���t�@�C����FLUSH����
		/// </remarks>
		/// <returns>�Ȃ�</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		public void Flush()
		{
			try
			{
				if (sw != null)
				{
					sw.Flush();
				}
			}
			catch
			{
			}
		}
		#endregion
		#endregion

		#region protected ���\�b�h
		//************************************************************************
		/// <summary>
		/// ���O�N���X���쐬����
		/// </summary>
		/// <remarks>
		/// ���O�N���X�̍\��
		/// </remarks>
		/// <param name="path">���O�t�@�C���p�X</param>
		/// <param name="FileMain">���O�t�@�C����</param>
		/// <param name="FileExt">���O�t�@�C���g���q</param>
		/// <param name="FileCount">���O�t�@�C����</param>
		/// <param name="FileSize">���O�t�@�C���T�C�Y</param>
		/// <returns>�Ȃ�</returns>
		//************************************************************************
		protected void Create(string path, string FileMain, string FileExt,
			int FileCount, int FileSize, int LogLevel)
		{
			sw = null;

			mLevel = LogLevel;

			//���̓`�F�b�N
			if( path == null || FileMain == null || FileExt == null || FileCount < 1 || FileSize < 100 ||
				path.Equals("") || FileMain.Equals("") || FileExt.Equals("") )
			{
				// �p�����[�^�������ȏꍇ�ADefault�l���w�肷��B
				mPath = "C:\\";
				mFileMain = "logger";
				mFileExt = ".txt";
				mFileCount = 10;
				mFileSize = 1024000;

				mCurrFileNoExt = "logger1";
				mCurrFullName = "c:\\logger1.txt";
				lenSeq = 1;
				return;
			}

			if(!path.EndsWith("\\") )
			{
				path = path +"\\";		//���"\"��ǉ�
			}
			mPath = path;
			mFileMain = FileMain;
			mFileExt = FileExt;
			mFileCount = FileCount;
			mFileSize = FileSize;

			string  StrLen = FileCount.ToString();		//FileCount's����
			lenSeq = StrLen.Length;
			if (lenSeq > 4) lenSeq =4;

			mCurrFileNoExt = mFileMain + "0001".Substring(4 - lenSeq, lenSeq) ;  //index from 0
			mCurrFullName = mPath + mCurrFileNoExt + mFileExt;
			
		}
		#endregion			

		#region private ���\�b�h
		#region �t�@�C�������݂��Ȃ���΁A�V�K����
		//************************************************************************
		/// <summary>
		/// ���O�t�@�C���n���h�����擾����
		/// </summary>
		/// <remarks>
		/// �t�@�C�������݂��Ȃ���΁A�V�K����
		/// �t�@�C���T�C�Y���I�[�o�[���Ă��Ȃ��ꍇ�A�ǉ�����
		/// �t�@�C���T�C�Y���I�[�o�[�����ꍇ�A
		/// log002-->log003, log001 -->log002�̂悤�Ɉړ��A������log001��V�K����
		/// ���O�t�@�C�����̓I�[�o�[����ꍇ�A��ԌÂ��t�@�C�����폜����
		/// �t�@�C����StreamWriter�Ώۂ�Ԃ�
		/// </remarks>
		/// <returns>StreamWriter</returns>
		//************************************************************************
		private StreamWriter getFileHandle()
		{
			try
			{	
				//���ʁFStreamWriter���݁A���t�@�C���T�C�Y���I�[�o�[���Ȃ��ꍇ�A����return
				if( sw != null )
				{	
					FileInfo currfi2 = new FileInfo(mCurrFullName);
					if( currfi2.Length < mFileSize)
					{
						return sw;
					}
				}

				//1��ڎ��s���鎞�A�܂��̓t�@�C���ؑւ̏ꍇ
				// DIR�����݂��Ȃ���΁A�V�K����
				DirectoryInfo di = new DirectoryInfo(mPath);
				if( !di.Exists )		
				{
					di.Create();
				}

				// �t�@�C�������݂��Ȃ���΁A�V�K����
				FileInfo currfi = new FileInfo(mCurrFullName);
				if( !currfi.Exists )
				{
                    sw = new StreamWriter(mCurrFullName, true, Encoding.GetEncoding("Shift_JIS"), 2048);
					return sw;
				}

				// �t�@�C���T�C�Y���I�[�o�[���Ă��Ȃ��ꍇ�A�ǉ�����
				if( currfi.Length < mFileSize)
				{
                    sw = new StreamWriter(mCurrFullName, true, Encoding.GetEncoding("Shift_JIS"), 2048);
					return sw;
				}

				//�T�C�Y�@�I�[�o�[�A�V�K���O�t�@�C�����K�v�B
				if( sw != null )
				{
					sw.Close();	//�܂�CLOSE
					sw = null;
				}
			
				//Filter�Ń��O�t�@�C���̉Ӑ��������擾����
				int fCount = di.GetFiles( mFileMain + "*" + mFileExt ).Length;
				int upperCount;
				if (fCount >= mFileCount )
				{
					//���O�t�@�C�������I�[�o�[�A��ԌÂ��t�@�C�����폜����
					upperCount = mFileCount;	
				}
				else
				{
					//���O�t�@�C�������I�[�o�[���Ă��Ȃ����A�t�@�C��RENAME�������邽�߁A
					//RENAME��̃t�@�C���𑶍݂��Ȃ��悤�ɁA�i���ʂ͑��݂��Ă��Ȃ��͂��j�폜����B
					upperCount = fCount+1;
				}

				string StrCount = "0000" +  upperCount.ToString();
				string StrMaxFile = mFileMain + StrCount.Substring(StrCount.Length-lenSeq, lenSeq) ;  //index from 0
				StrMaxFile = mPath + StrMaxFile + mFileExt;
				File.Delete(StrMaxFile);	//File.Delete ���݂��Ȃ��Ă��A��O�͂��܂���

				string fileMoto;
				string fileSaki;
				for(int  i= upperCount-1; i>=1; i-- )
				{
					fileMoto = "0000"+ i.ToString();
					fileMoto =  mFileMain + fileMoto.Substring(fileMoto.Length-lenSeq, lenSeq) ;  //index from 0
					fileMoto = mPath + fileMoto + mFileExt;

					fileSaki = "0000"+ (i+1).ToString();
					fileSaki =  mFileMain + fileSaki.Substring(fileSaki.Length-lenSeq, lenSeq) ;  //index from 0
					fileSaki = mPath + fileSaki + mFileExt;
					//make sure the fileMoto is exist.
					using (StreamWriter swr = File.AppendText(fileMoto)) {}

					//RENAME the file
					File.Move(fileMoto, fileSaki);
				}

			
				//File.Delete(mCurrFullName);
				File.Delete(mCurrFullName);
                sw = new StreamWriter(mCurrFullName, true, Encoding.GetEncoding("Shift_JIS"), 2048);
				return sw;
	
			}
			catch(Exception ex)
			{
				ex.ToString();
				//Console.WriteLine("getFileHandle is failed: {0}", e.ToString());
			}
			return null;
		}
		#endregion

		#region �t�@�C����OPEN����
		//************************************************************************
		/// <summary>
		/// void Open()
		/// </summary>
		/// <remarks>
		/// �t�@�C����OPEN����
		/// ����Write����O��OPEN���Ăяo���Ȃ��Ă�����
		/// </remarks>
		/// <returns>�Ȃ�</returns>
		//************************************************************************
		private void Open()
		{
			getFileHandle();
		}
		#endregion

		#region msg��"Information"level�Ń��O�t�@�C���ɏo�͂���
		//************************************************************************
		/// <summary>
		/// void Write(string msg)
		/// </summary>
		/// <remarks>
		/// msg��"Information"level�Ń��O�t�@�C���ɏo�͂���
		/// </remarks>
		/// <param name="msg">���b�Z�[�W������</param>
		/// <returns>�Ȃ�</returns>
		//************************************************************************
		private void Write(string msg)
		{
			Write(LogLevel.Information, msg, false);
		}
		#endregion

		#region msg��level�Ń��O�t�@�C���ɏo�͂���
		//************************************************************************
		/// <summary>
		/// void Write(LogLevel level, string msg)
		/// </summary>
		/// <remarks>
		/// msg��level�Ń��O�t�@�C���ɏo�͂���
		/// </remarks>
		/// <param name="level">���O�o�̓��x���񋓌^�AError�GWarning�GInformation�GDebug</param>
		/// <param name="msg">���b�Z�[�W������</param>
		/// <returns>�Ȃ�</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		private void Write(LogLevel level, string msg)
		{
			Write(level, msg, false);
		}
		#endregion

		#region msg��"Information"level�Ń��O�t�@�C���ɏo�͂���
		//************************************************************************
		/// <summary>
		/// void Write(string msg, bool close)
		/// </summary>
		/// <remarks>
		/// msg��"Information"level�Ń��O�t�@�C���ɏo�͂���
		/// </remarks>
		/// <param name="msg">���b�Z�[�W������</param>
		/// <param name="close">�utrue�v�̓t�@�C������邵�A���\�[�X��������܂�</param>
		/// <return>�Ȃ�</return>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		private void Write(string msg, bool close)
		{
			Write(LogLevel.Information, msg, close);
		}
		#endregion

		#region msg��level�Ń��O�t�@�C���ɏo�͂���
		//************************************************************************
		/// <summary>
		/// void Write(LogLevel level, string msg, bool close)
		/// </summary>
		/// <remarks>
		/// msg��level�Ń��O�t�@�C���ɏo�͂���
		/// </remarks>
		/// <param name="level">���O�o�̓��x���񋓌^�AError�GWarning�GInformation�GDebug</param>
		/// <param name="msg">���b�Z�[�W������</param>
		/// <param name="close">�utrue�v�̓t�@�C������邵�A���\�[�X��������܂�</param>
		/// <return>�Ȃ�</return>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		private void Write(LogLevel level, string msg, bool close)
		{
			try
			{
				string strBuf = "";
				bool bReturn = GetLevelString(level, out strBuf);
				if (bReturn == false) return;
				StreamWriter sw = getFileHandle();
                lock (sw)
                {
                    if (sw != null)
                    {
                        sw.WriteLine(GetTimeString() + "  " + strBuf + "  " + msg);
                        //comment it in release version
                        sw.Flush();
                    }

                    //�t�@�C������邵�A���\�[�X��������܂�
                    if (close)
                    {
                        Close();
                    }
                }
			}
			catch
			{
			}

		}
		#endregion
		
		#region Exception �����ݒ肳�ꂽ�̕�����������o��
		//************************************************************************
		/// <summary>
		/// void Write(Exception exp)
		/// </summary>
		/// <remarks>
		/// Exception �����ݒ肳�ꂽ�̕�����������o��
		/// </remarks>
		/// <param name="exp">Exception ��������G���[��\���܂�</param>
		/// <returns>�Ȃ�</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		private void Write(Exception exp)
		{
			try
			{
				StreamWriter sw = getFileHandle();
                lock (sw)
                {
                    if (sw != null)
                    {
                        sw.WriteLine(String.Format("{0}\tError: \tFunction: {1}; \r\nMessage: {2}; \r\n{3}", GetTimeString(), exp.TargetSite, exp.Message, exp.StackTrace));
                        sw.Flush();
                    }
                }
			}
			catch
			{
			}
		}
		
		#endregion

		#region ���O�̃p�����[�^�[���擾���܂�
		//************************************************************************
		/// <summary>
		/// ���O�̃p�����[�^�[���擾���܂�
		/// </summary>
		/// <remarks>
		/// ���O�̃p�����[�^�[���擾���܂�
		/// </remarks>
		/// <param name="strLogPath">string ���O�t�@�C���p�X</param>
		/// <param name="strLogFile">string ���O�t�@�C����</param>
		/// <param name="strFileExt">string ���O�t�@�C���g���q</param>
		/// <param name="iLogCount">int ���O�t�@�C����</param>
		/// <param name="iLogSize">int ���O�t�@�C���T�C�Y</param>
		/// <returns>�Ȃ�</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		private void GetLogConfig(ref string strLogPath, ref string strLogFile, ref string strFileExt, ref int iLogCount, ref int iLogSize, ref int iLogLevel)
		{
			try
			{
				string strValue = "";
                Configuration config = null;
                if (_logType == LogTypeManager.PerUser)
                {
                    config = m_pFunc.GetCurrentConfigFile();
                }
                else
                {
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                }
                if (config != null)
                {
                    strLogPath = m_pFunc.GetSettingValue(config.AppSettings.Settings, "LogPath");
                    // ���O�t�@�C���i�[����p�X�͎w�肵�Ă��Ȃ��ꍇ�́A�Y���A�v������Log�Ƃ����t�H���_��ݒ肷��
                    if (strLogPath == null || strLogPath == "")
                    {
                        if (_logType == LogTypeManager.PerUser)
                        {
                            strLogPath = Directory.GetCurrentDirectory() + "\\Log";
                        }
                        else
                        {
                            strLogPath = Application.StartupPath + "\\Log";
                        }
                    }
                    strLogFile = m_pFunc.GetSettingValue(config.AppSettings.Settings, "LogFile");
                    strFileExt = m_pFunc.GetSettingValue(config.AppSettings.Settings, "LogExt");
                    if (m_pFunc.IsEmpty(strFileExt))
                    {
                        strFileExt = ".txt";
                    }
                    strValue = m_pFunc.GetSettingValue(config.AppSettings.Settings, "LogCount");
                    iLogCount = m_pFunc.StringToInt(strValue, 8);
                    strValue = m_pFunc.GetSettingValue(config.AppSettings.Settings, "LogSize");
                    iLogSize = m_pFunc.StringToInt(strValue, 1024);
                    iLogSize = iLogSize * 1000;
                    strValue = m_pFunc.GetSettingValue(config.AppSettings.Settings, "LogLevel");
                    iLogLevel = m_pFunc.StringToInt(strValue, 3);
                }
			}
			catch
			{
			}
		}
		#endregion

		#region �w�肵���G���[���x���Ή��̃��b�Z�[�W��Ԃ��܂�
		//************************************************************************
		/// <summary>
		/// void GetLogConfig(ref string strLogPath, ref string strLogFile, ref string strFileExt, ref int iLogCount, ref int iLogSize)
		/// </summary>
		/// <remarks>
		/// �w�肵���G���[���x���Ή��̃��b�Z�[�W��Ԃ��܂�
		/// </remarks>
		/// <param name="level">���O���x���񋓌^</param>
		/// <param name="strLevel">out:���O���x���Ή��̃��b�Z�[�W������</param>
		/// <returns>bool �w�肵�����O�o�̓��x�����Ⴂ�Ƃ��ɁA�utrue�v��Ԃ��܂�</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		private bool GetLevelString(LogLevel level, out string strLevel)
		{
			bool bReturn = false;
			strLevel = "";
			if ((int)level <= mLevel)
			{
				bReturn = true;
				//�G���[���x��
				if (level == LogLevel.Error)
				{
					strLevel = "Error:";
				}
					//�x��
				else if (level == LogLevel.Warning)
				{
					strLevel = "Warning:";
				}
					//�C���t�H�[���[�V����
				else if (level == LogLevel.Information)
				{
					strLevel = "Information:";
				}
					//�f�o�b�O
				else if (level == LogLevel.Debug)
				{
					strLevel = "Debug:";
				}
			}
			return bReturn;
		}
		#endregion

		#region DateTime �w�肵�������� string ��Ԃ��܂�
		//************************************************************************
		/// <summary>
		/// string GetTimeString()
		/// </summary>
		/// <remarks>
		/// DateTime �w�肵�������� string ��Ԃ��܂�
		/// </remarks>
		/// <returns>string:�l������Ɠ����ȕ�����`���ɕϊ����܂�</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		private string GetTimeString()
		{
			DateTime dt = DateTime.Now;			
			string strReturn = String.Format("[{0:000#}-{1:0#}-{2:0#} {3:0#}:{4:0#}:{5:0#}.{6:00#}] ", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
			return strReturn;
		}
		#endregion		
		#endregion
	}
}
