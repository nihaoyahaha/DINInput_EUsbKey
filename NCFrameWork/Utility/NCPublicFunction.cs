namespace DI.NCFrameWork
{	
	using System;
	using System.IO;
	using System.Globalization;
    using System.Drawing;
    using System.Diagnostics;
	using System.Configuration;
    using System.Text;
    using System.Net;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Microsoft.VisualBasic;
    
	//************************************************************************
	/// <summary>
	/// NCPublicFunction �̊T�v�̐����ł��B
	/// </summary>
	//************************************************************************
    [Serializable]
	public class NCPublicFunction
	{
		public const string YYYYMMDD = "yyyyMMdd";
		public const string YYYYSlashMMSlashDD = "yyyy/MM/dd";
        public static string[] DAYOFWEEK = { "���j��", "���j��", "�Ηj��", "���j��", "�ؗj��", "���j��", "�y�j��" };
		
        public NCPublicFunction()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
        }

        #region STRING����l�̔��f
        //************************************************************************
		/// <summary>
		/// STRING����l�̔��f
		/// </summary>
		/// <param name="strValue">������</param>
		/// <returns>bool </returns>
		//************************************************************************
		public bool IsEmpty(string strValue)
		{
			if (strValue == null || strValue.Trim().Length == 0)
			{
				return true;
			}
			return false;
        }
        #endregion

        #region String����Int�֕ϊ�����
        //************************************************************************
		/// <summary>
		/// String����Int�֕ϊ�����
		/// </summary>
		/// <param name="strValue">������</param>
		/// <param name="iDefault">�f�t�H���g�l</param>
		/// <returns>int </returns>
		//************************************************************************
		//String����Int�֕ϊ�����
		public int StringToInt(string strValue, int iDefault)
		{
			int iReturn = iDefault;
			try
			{
                if (IsInteger(strValue))
                {
                    iReturn = Convert.ToInt32(strValue);
                }
			}
			catch 
			{
				
			}
			return iReturn;
		}
        #endregion

        #region String����Long�֕ϊ�����
        //************************************************************************
		/// <summary>
		/// String����Long�֕ϊ�����
		/// </summary>
		/// <param name="strValue">������</param>
		/// <param name="lDefault">�f�t�H���g�l</param>
		/// <returns>long </returns>
		//************************************************************************
		public long StringToLong(string strValue, long lDefault)
		{
			long lReturn = lDefault;
			try
			{
                if (IsInteger(strValue))
                {
                    lReturn = Convert.ToInt64(strValue);
                }
			}
			catch 
			{
				
			}
			return lReturn;
		}
        #endregion

        #region String����Double�֕ϊ�����
        //************************************************************************
		/// <summary>
		/// String����Double�֕ϊ�����
		/// </summary>
		/// <param name="strValue">������</param>
		/// <param name="dDefault">�f�t�H���g�l</param>
		/// <returns>double </returns>
		//************************************************************************
		public double StringToDouble(string strValue, double dDefault)
		{
			double dReturn = dDefault;
			try
			{
                if (IsNumeric(strValue))
                {
                    dReturn = Convert.ToDouble(strValue);
                }
			}
			catch
			{
			}
			return dReturn;
		}
        #endregion

        #region String����Decimal�֕ϊ�����
        //************************************************************************
        /// <summary>
        /// String����Decimal�֕ϊ�����
        /// </summary>
        /// <param name="strValue">������</param>
        /// <param name="dDefault">�f�t�H���g�l</param>
        /// <returns>double </returns>
        //************************************************************************
        public decimal StringToDecimal(string strValue, decimal dDefault)
        {
            decimal dReturn = dDefault;
            try
            {
                if (IsNumeric(strValue))
                {
                    dReturn = Convert.ToDecimal(strValue);
                }
            }
            catch
            {
            }
            return dReturn;
        }
        #endregion

        #region String����DateTime�֕ϊ�����
        //************************************************************************
		/// <summary>
		/// String����DateTime�֕ϊ�����
		/// �t�H�[�}�b�g�̌^�Fyyyy/MM/dd HH:mm:ss
		///					yyyy/MM/dd
		/// strCultureName:"en-US"							
		///				"ru-RU"	
		///				"ja-JP"
		/// </summary>
		/// <param name="strValue">������</param>
		/// <returns>DateTime </returns>
		//************************************************************************
		public DateTime StringToDateTime(string strValue)
		{
			DateTime dtReturn = System.DateTime.Now;
			try
			{
				strValue = GetStringDate(strValue, 1);
				dtReturn = Convert.ToDateTime(strValue);
			}
			catch 
			{
			}
			return dtReturn;
		}
        #endregion

        #region ���t�f�[�^�t�H�[�}�b�g�]��
        //************************************************************************
		/// <summary>
		/// ���t�f�[�^�t�H�[�}�b�g�]�� mode(1:YYYYMMDD -> YYYY/MM/DD   2:YYYY/MM/DD -> YYYYMMDD)
		/// </summary>
		/// <param name="strVale">������</param>
		/// <param name="iMode">���[�h</param>
		/// <returns>string </returns>
		//************************************************************************
		public string GetStringDate(string strVale, int iMode)
		{
			string strReturn = strVale;

			try
			{
				if (1 == iMode)
				{
					if (null != strVale && 8 == strVale.Length)	
					{
                        if (strVale.IndexOf('/') < 0 && strVale.IndexOf('-') < 0)
                        {
                            strReturn = String.Format("{0,-4}/{1,-2}/{2,-2}", strVale.Substring(0, 4), strVale.Substring(4, 2), strVale.Substring(6));
                        }
					}
				}
				else if (2 == iMode)
				{
					strVale.Trim();

					string[] arrayTemp = new String[3];
					char[] cToken = new char[1]{'/'};
					arrayTemp = strVale.Split(cToken);
		
					strReturn = String.Format("{0,4}{1,2}{2,2}", arrayTemp[0], arrayTemp[1].PadLeft(2, '0'), arrayTemp[2].PadLeft(2, '0'));
				}
			}
			catch
			{
			}
			return strReturn;
		}
		#endregion

        #region ���t�f�[�^��������ɕϊ�����
        //************************************************************************
		/// <summary>
		/// ���t�f�[�^��������ɕϊ�����
		/// </summary>
		/// <param name="dtValue">���t�f�[�^</param>
		/// <param name="iMode">���[�h</param>
		/// <returns>string </returns>
		//************************************************************************
		public string DateTimeToString(DateTime dtValue, int iMode)
		{
			string strReturn = "";
			try
			{
				switch(iMode)
				{
					case 0:
						strReturn = dtValue.ToString(YYYYMMDD);
						break;
					case 1:
						strReturn = dtValue.ToString(YYYYSlashMMSlashDD);
						break;
					case 2:
						strReturn = dtValue.ToString(YYYYSlashMMSlashDD);
						break;
					case 3:
						break;
					default:
						break;
				}
			}
			catch
			{
			}
			return strReturn;
		}
        #endregion

        #region object �\�� decimal ��Ԃ��܂�
        //************************************************************************
        /// <summary>
        /// double ObjectToDecimal(object objValue)
        /// </summary>
        /// <remarks>
        /// object �\�� decimal��Ԃ��܂�
        /// </remarks>
        /// <param name="objValue">object �l</param>
        /// <returns>decimal �^�C�v�̒l</returns>
        /// <example>
        /// <code></code>
        /// </example>
        //************************************************************************
        public decimal ObjectToDecimal(object objValue)
        {
            decimal dReturn = 0m;
            if (null != objValue)
            {
                try
                {
                    string strValue = objValue.ToString();
                    dReturn = StringToDecimal(strValue, 0m);
                }
                catch
                {
                    dReturn = 0m;
                }
            }
            return dReturn;
        }
        #endregion

		#region object �\�� double ��Ԃ��܂�
		//************************************************************************
		/// <summary>
		/// double ObjectToDouble(object objValue)
		/// </summary>
		/// <remarks>
		/// object �\�� double��Ԃ��܂�
		/// </remarks>
		/// <param name="objValue">object �l</param>
		/// <returns>double �^�C�v�̒l</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		public double ObjectToDouble(object objValue)
		{
			double dReturn = 0;
			if (null != objValue)
			{
				try
				{
                    string strValue = objValue.ToString();
                    dReturn = StringToDouble(strValue, 0);
				}
				catch
				{
					dReturn = 0;
				}
			}
			return dReturn;
		}
		#endregion

		#region object �\�� string ��Ԃ��܂�
		//************************************************************************
		/// <summary>
		/// string ObjectToString(object objValue)
		/// </summary>
		/// <remarks>
		/// object �\�� string��Ԃ��܂�
		/// </remarks>
		/// <param name="objValue">object �l</param>
		/// <returns>string �^�C�v�̒l</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		public string ObjectToString(object objValue)
		{
			string strReturn = "";
			if (null != objValue)
			{
				try
				{
					strReturn = objValue.ToString();
				}
				catch
				{
					strReturn = "";
				}
			}
			return strReturn;
		}
		#endregion

		#region object �\�� Int32 ��Ԃ��܂�
		//************************************************************************
		/// <summary>
		/// int ObjectToInt(object objValue)
		/// </summary>
		/// <remarks>
		/// object �\�� Int32 ��Ԃ��܂�
		/// </remarks>
		/// <param name="objValue">object �l</param>
		/// <returns>Int32 �^�C�v�̒l</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		public int ObjectToInt(object objValue)
		{
			int iReturn = 0;
			if (null != objValue)
			{
				try
				{
                    string strValue = objValue.ToString();
                    iReturn = StringToInt(strValue, 0);
				}
				catch
				{
					iReturn = 0;
				}
			}
			return iReturn;
		}
		#endregion

		#region object �\�� long ��Ԃ��܂�
		//************************************************************************
		/// <summary>
		/// long ObjectToLong(object objValue)
		/// </summary>
		/// <remarks>
		/// object ��\�� long ��Ԃ��܂�
		/// </remarks>
		/// <param name="objValue">object �l</param>
		/// <returns>long �^�C�v�̒l</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		public long ObjectToLong(object objValue)
		{
			long lValue = 0;

			if (objValue != null)
			{
				try		
				{
                    string strValue = objValue.ToString();
                    lValue = StringToLong(strValue, 0);

				}
				catch	
				{	//�����l�ݒ�
					lValue = 0;
				}
			}

			return lValue;
		}
		#endregion

		#region object �\�� ���t��Ԃ��܂�
		//************************************************************************
		/// <summary>
		/// DateTime ObjectToDateTime(object objValue)
		/// </summary>
		/// <remarks>
		/// object �\�� ���t��Ԃ��܂�
		/// </remarks>
		/// <param name="objValue">object �l</param>
		/// <returns>DateTime �^�C�v�̒l</returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		public DateTime ObjectToDateTime(object objValue)
		{
			DateTime dt = DateTime.Now;
			if (objValue != null)
			{
				try
				{
                    string strDateTime = objValue.ToString();
                    dt = StringToDateTime(strDateTime);
				}
				catch
				{
				}
			}
			return dt;
		}
		#endregion

        #region ���t�΂���j����Ԃ�
        //**********************************************************************
        /// <summary>
        /// ���t�΂���j����Ԃ�
        /// </summary>
        /// <param name="dtValue">���t</param>
        /// <returns>�j����\��������</returns>
        //**********************************************************************
        public string GetWeekString(DateTime dtValue)
        {
            string strReturn = "";
            try
            {
                strReturn = DAYOFWEEK[(int)dtValue.DayOfWeek];
            }
            catch
            { 
            }
            return strReturn;
        }
        #endregion

        #region DateTime �w�肵�������� string ��Ԃ��܂�
        //************************************************************************
		/// <summary>
		/// string GetDateTimeString()
		/// </summary>
		/// <remarks>
		/// DateTime �w�肵�������� string ��Ԃ��܂�
		/// </remarks>
		/// <param name="dt">�w�肵������</param>
		/// <returns>DateTime �w�肵�������� string </returns>
		/// <example>
		/// <code></code>
		/// </example>
		//************************************************************************
		public string GetDateTimeString(DateTime dt)
		{
			string strReturn = "";
			strReturn = String.Format("[{0:000#}-{1:0#}-{2:0#} {3:0#}:{4:0#}:{5:0#}:{6:00#}] ", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
			return strReturn;
		}
		#endregion

        #region ���t���m�����f
        /// <summary>
        /// ���t���m�����f
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public bool IsDateTime(string strValue)
        {
            bool retBool = false;
            try
            {
                retBool = Information.IsDate(strValue);
            }
            catch
            {
            }

            return retBool;
        }

        #endregion

        #region �S�p�������f
        /// <summary>
        /// �S�p�������f
        /// </summary>
        /// <param name="strValue">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsDoubleChar(string strValue)
        {
            bool retBool = true;

            if (IsEmpty(strValue))
            {
                return true;
            }
            if (Encoding.UTF8.GetByteCount(strValue) == strValue.Length)
            {
                retBool = false;
            }
            
            return retBool;
        }
        #endregion

        #region �S�p�������f
        /// <summary>
        /// �S�p�������f
        /// </summary>
        /// <param name="strValue">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsDoubleKANA(string strValue)
        {
            bool retBool = true;
            if (IsEmpty(strValue))
            {
                return true;
            }
            for (int i = 0; i < strValue.Length; i++)
            {
                string charValue = strValue.Substring(i, 1);
                int charCode = GetCodeOfChar(charValue);
                if (!((charCode >= 0x8140 && charCode <= 0x81FC)		//�S�p����
                    || (charCode >= 0x824F && charCode <= 0x8258)		//�S�p����
                    || (charCode >= 0x8260 && charCode <= 0x829A)		//�S�p�p��
                    || (charCode >= 0x8340 && charCode <= 0x8396)
                    || (charCode >= 0x82A0 && charCode <= 0x82F1)))		//�S�p����
                {
                    //�S�p�����͗L��܂���
                    retBool = false;
                    break;
                }
            }
            return retBool;
        }
        #endregion

        #region ���p�̉p�����A�L�����f
        /// <summary>
        /// ���p�̉p�����A�L�����f
        /// </summary>
        /// <param name="strValue">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsHanKakuSign(string strValue)
        {
            bool retBool = true;
            for (int i = 0; i < strValue.Length; i++)
            {
                string charValue = strValue.Substring(i, 1);
                int charCode = GetCodeOfChar(charValue);
                if ((charCode > 0xa5 || charCode < 0x21))
                {
                    retBool = false;
                    break;
                }
            }
            return retBool;
        }
        #endregion

        #region �p�����f
        /// <summary>
        /// �p�����f
        /// </summary>
        /// <param name="strValue">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsBytesAlpha(string strValue)
        {
            Regex regex = new Regex(@"^[a-zA-Z ]+$");
            return regex.IsMatch(strValue);
        }
        #endregion

        #region �������f
        /// <summary>
        /// �������f
        /// </summary>
        /// <param name="strNumber">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsNumeric(string strNumber)
        {
            bool bReturn = false;
            Regex regNotNumberPattern = new Regex("[^0-9.-]");
            Regex regTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex regTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex regNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            bReturn = !regNotNumberPattern.IsMatch(strNumber) &&
                !regTwoDotPattern.IsMatch(strNumber) &&
                !regTwoMinusPattern.IsMatch(strNumber) &&
                regNumberPattern.IsMatch(strNumber);

            return bReturn;
        }
        #endregion

        #region �p�������f
        /// <summary>
        /// �p�������f
        /// </summary>
        /// <param name="strValue">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsAlphaNumeric(string strValue)
        {
            Regex regAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");

            return !regAlphaNumericPattern.IsMatch(strValue);
        }
        #endregion

        #region �S���͐������f
        /// <summary>
        /// �S���͐������f
        /// </summary>
        /// <param name="strNumber">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsWholeNumber(string strNumber)
        {
            Regex regNotWholePattern = new Regex("[^0-9]");

            return !regNotWholePattern.IsMatch(strNumber);
        }
        #endregion

        #region �������f
        /// <summary>
        /// �������f
        /// </summary>
        /// <param name="strNumber">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsInteger(string strNumber)
        {
            Regex regNotIntPattern = new Regex("[^0-9-]");
            Regex regIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");

            return !regNotIntPattern.IsMatch(strNumber) &&
                regIntPattern.IsMatch(strNumber);
        }
        #endregion 

        #region ������͑S�p�����ł��邩�ǂ����𔻖�����
        //**********************************************************************
        /// <summary>
        /// ������͑S�p�����ł��邩�ǂ����𔻖�����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //**********************************************************************
        public bool IsZenkaku(string str)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(str);
            return num == str.Length * 2;
        }
        #endregion

        #region ������͑S�p�����ł��邩�ǂ����𔻖�����
        //**********************************************************************
        /// <summary>
        /// ������͔��p�����ł��邩�ǂ����𔻖�����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //**********************************************************************
        public bool IsHankaku(string str)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(str);
            return num == str.Length;
        }
        #endregion

        #region ���[���L�����f
        /// <summary>
        /// ���[���L�����f
        /// </summary>
        /// <param name="strMail">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsValidEmail(string strMail)
        {
            Regex regMail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regMail.IsMatch(strMail);
        }
        #endregion

        #region �L����URL���f
        /// <summary>
        /// �L����URL���f
        /// </summary>
        /// <param name="strUrl">URL������</param>
        /// <param name="bPrefix">�ړ���</param>
        /// <returns>���f����</returns>
        public bool IsValidURL(string strUrl, bool bPrefix)
        {
            Regex regURL = null;
            if (bPrefix)
            {
                regURL = new Regex(@"^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$");
            }
            else
            {
                regURL = new Regex(@"^(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$");
            }
            return regURL.IsMatch(strUrl);
        }
        #endregion

        #region �L����IP�A�h���X���f
        /// <summary>
        /// �L����IP�A�h���X���f
        /// </summary>
        /// <param name="strIP">IP�A�h���X������</param>
        /// <returns>���f����</returns>
        public bool IsValidIP(string strIP)
        {
            Regex regIP = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            return regIP.IsMatch(strIP);
        }
        #endregion

        #region �X�֔ԍ����f
        /// <summary>
        /// PostCode���f
        /// </summary>
        /// <param name="strPostCode">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool IsPostCode(string strPostCode)
        {
            bool bReturn = false;
            if (!IsEmpty(strPostCode) && strPostCode.Length == 7
            && IsWholeNumber(strPostCode))
            {
                bReturn = true;
            }
            return bReturn;
        }
        #endregion

        #region �d�b�ԍ��`�F�b�N
        /// <summary>
        /// �d�b�ԍ��`�F�b�N
        /// </summary>
        /// <param name="strTelNo">���ڕ�����</param>
        /// <returns>���f����</returns>
        public bool CheckTelNo(string strTelNo)
        {
            Regex regTel = new Regex(@"^[0-9]{1,7}-[0-9]{1,4}-[0-9]{1,5}$");
            return regTel.IsMatch(strTelNo);
        }
        #endregion 

        #region �R�[�h�]��
        /// <summary>
        /// �R�[�h�]��
        /// </summary>
        /// <param name="strValue">���ڕ�����</param>
        /// <returns>�]������</returns>
        public int GetCodeOfChar(string strValue)
        {
            try
            {
                Encoding utf8 = Encoding.UTF8;
                Encoding jis = Encoding.GetEncoding("Shift_JIS");
                byte[] utfBytes = utf8.GetBytes(strValue);
                byte[] jisBytes = Encoding.Convert(utf8, jis, utfBytes);
                if (jisBytes.Length == 1)
                {
                    //���p
                    if (jisBytes[0] == '?')
                    {
                        if (strValue == "?")
                        {
                            return '?';
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return jisBytes[0];
                    }

                }
                else
                {
                    //�S�p
                    int first = (int)jisBytes[0];
                    int second = (int)jisBytes[1];
                    int iValue = (first << 8) + second;
                    return iValue;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region �w�肵�������ʒu����J�n���A�w�肵���������̕������Ԃ�
        //**********************************************************************
        /// <summary>
        /// �w�肵�������ʒu����J�n���A�w�肵���������̕������Ԃ�
        /// </summary>
        /// <param name="strValue">������</param>
        /// <param name="nStart">�J�n�ʒu</param>
        /// <param name="nLens">������</param>
        /// <returns>������</returns>
        //**********************************************************************
        public string SubString(string strValue, int nStart, int nLens)
        {
            string strReturn = "";
            try
            {
                if (!IsEmpty(strValue))
                {
                    // �w�肵��������̉E������A�w�肵���������̕������Ԃ�
                    if (nStart == -1)
                    {
                        int nLength = Math.Min(strValue.Length, nLens);
                        int nStartIndex = Math.Max(nStart, 0);
                        strReturn = strValue.Substring(strValue.Length - nLength);
                    }
                    else
                    {
                        int nStartIndex = Math.Max(nStart, 0);
                        if (nLens == -1)
                        {
                            strReturn = strValue.Substring(nStartIndex);
                        }
                        else
                        {
                            if (strValue.Length >= nStartIndex)
                            {
                                int nLength = Math.Min(strValue.Length, nLens + nStartIndex);
                                strReturn = strValue.Substring(nStartIndex, nLength - nStartIndex);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            
            return strReturn;
        }
        #endregion

        #region �w�肵�������̎w�肵���o�C�g���̕������Ԃ�
        //**********************************************************************
        /// <summary>
        /// �w�肵�������ʒu����J�n���A�w�肵���������̕������Ԃ�
        /// </summary>
        /// <param name="strValue">������</param>
        /// <param name="nLens">������</param>
        /// <returns>������</returns>
        //**********************************************************************
        public string SubStringB(string strValue, int nLens)
        {
            string strReturn = "";
            try
            {
                if (!IsEmpty(strValue))
                {
                    // �w�肵��������̉E������A�w�肵���������̕������Ԃ�
                    strReturn = strValue;
                    int j = 0;
                    int k = 0;
                    for (int i = 0; i < strReturn.Length; i++)
                    {
                        if (IsDoubleChar(strReturn.Substring(i, 1)))
                        {
                            j += 2;
                        }
                        else
                        {
                            j += 1;
                        }
                        if (j <= nLens)
                        {
                            k += 1;
                        }
                        if (j >= nLens)
                        {
                            strReturn = strReturn.Substring(0, k);
                            break;
                        }
                    }
                }
            }
            catch
            {
            }

            return strReturn;
        }
        #endregion

        #region 10�i�������_�l��L������3���̐��l�Ɋۂ߂�i�������A1000�𒴂���ꍇ�́A���l�𐮐��l�Ɋۂ߂�j
        /// <summary>
        /// 10�i�������_�l��L������3���̐��l�Ɋۂ߂�i�������A1000�𒴂���ꍇ�́A���l�𐮐��l�Ɋۂ߂�j
        /// </summary>
        /// <param name="dValue">�ۂߑΏ�</param>
        /// <returns>�ۂ߂��ꂽ������Ԃ�</returns>
        public decimal Round(decimal dValue)
        {
            decimal dReturn = 0m;
            try
            {
                decimal dTmp = 0m;
                dTmp = Math.Abs(dValue);
                int nTmp = (int)dTmp;
                if (dTmp < 1000.0m)
                {
                    if (nTmp == 0)
                    {
                        nTmp = 3;
                    }
                    else
                    {
                        nTmp = 3 - nTmp.ToString().Length;
                    }
                }
                else
                {
                    nTmp = 0;
                }

                dReturn = Math.Round(dValue, nTmp);
            }
            catch
            {
            }
            return dReturn;
        }
        #endregion

        #region �����\������Ă���J���[��Ԃ�
        //**********************************************************************
        /// <summary>
        /// �����\������Ă���J���[��Ԃ�
        /// </summary>
        /// <param name="nColor">�����\������Ă���J���[</param>
        /// <returns>�J���[</returns>
        //**********************************************************************
        public Color IntToColor(int nColor)
        {
            Color color = Color.White;
            try
            {
                color = Color.FromArgb(nColor % 256, (nColor / 256) % 256, nColor / 65536);
            }
            catch
            { 
            }
            return color;
        }
        #endregion

        #region �J���[��32�r�b�g������Ԃ�
        //**********************************************************************
        /// <summary>
        /// �J���[��32�r�b�g������Ԃ�
        /// </summary>
        /// <param name="color">�J���[</param>
        /// <returns></returns>
        //**********************************************************************
        public int ColorToInt(Color color)
        {
            int nColor = 0;
            try
            {
                nColor = color.B * 65536 + color.G * 256 + color.R;
            }
            catch
            {
            }
            return nColor;
        }
        #endregion

        #region �J���[�΂��锽�]�F���擾����
        //**********************************************************************
        /// <summary>
        /// �J���[�΂��锽�]�F���擾����
        /// </summary>
        /// <param name="nColor">�����\������Ă���J���[</param>
        /// <returns>�J���[�΂��锽�]�F</returns>
        //**********************************************************************
        public Color GetInvertColor(int nColor)
        {
            Color invertColor = Color.White;
            try
            {
                invertColor = IntToColor(nColor ^ 0xffffff);
            }
            catch
            {
            }
            return invertColor;
        }
        #endregion

        #region �J���[�΂��锽�]�F���擾����
        //**********************************************************************
        /// <summary>
        /// �J���[�΂��锽�]�F���擾����
        /// </summary>
        /// <param name="nColor">�����\������Ă���J���[</param>
        /// <returns>�J���[�΂��锽�]�F</returns>
        //**********************************************************************
        public Color GetInvertColor(Color color)
        {
            Color invertColor = Color.White;
            try
            {
                invertColor = GetInvertColor(color.ToArgb());
            }
            catch
            {
            }
            return invertColor;
        }
        #endregion

        #region �t�@�C������Image���쐬����
        //**********************************************************************
        /// <summary>
        /// �t�@�C������Image���쐬����
        /// </summary>
        /// <param name="strFileName">�t�@�C����</param>
        /// <returns></returns>
        //**********************************************************************
        public Image GetImageFormFile(string strFileName)
        {
            Image image = null;
            try
            {
                if (!IsEmpty(strFileName))
                {
                    using (FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        if (fs != null)
                        {
                            image = Bitmap.FromStream(fs);
                            fs.Close();
                        }
                    }
                }
                
            }
            catch
            {
            }
            return image;
        }
        #endregion

        #region �t�@�C������Image���쐬����
        //**********************************************************************
        /// <summary>
        /// �t�@�C������Image���쐬����
        /// </summary>
        /// <param name="strFileName">�t�@�C����</param>
        /// <param name="nWidth">��</param>
        /// <param name="nHeight">����</param>
        /// <returns></returns>
        //**********************************************************************
        public Image GetThumbnailImage(string strFileName, int nWidth, int nHeight)
        {
            Image image = null;
            
            try
            {
                using (Image imageOriginal = new Bitmap(GetImageFormFile(strFileName)))
                {
                    if (imageOriginal != null)
                    {
                        Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                        image = imageOriginal.GetThumbnailImage(nWidth, nHeight, myCallback, IntPtr.Zero);
                    }
                }
            }
            catch (Exception exp)
            {
                string str = exp.Message;
            }
            
            return image;
        }

        /// <summary>
        /// �T���l�C���R�[���o�b�N���\�b�h
        /// </summary>
        /// <returns></returns>
        private bool ThumbnailCallback()
        {
            return false;
        }
        #endregion

        #region �Y���v���Z�X�̃��[�U�ʂ̃A�v���\���t�@�C���̃C���X�^���X���擾����
        //**********************************************************************
        /// <summary>
        /// �Y���v���Z�X�̃��[�U�ʂ̃A�v���\���t�@�C���̃C���X�^���X���擾����
        /// </summary>
        /// <returns></returns>
        //**********************************************************************
        public Configuration GetCurrentConfigFile()
        {
            Configuration config = null;
            try
            {
                string strFileName = Process.GetCurrentProcess().MainModule.FileName;
                int nPos = strFileName.LastIndexOf('\\');
                if (nPos >= 0)
                {
                    strFileName = strFileName.Substring(nPos + 1);
                }
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = String.Format(@"{0}\{1}.config",
                    Directory.GetCurrentDirectory(), strFileName);
                config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            }
            catch 
            {
            }
            return config;
        }
        #endregion

        #region Configuration�̔z�񂩂�w�肵���L�[�̒l��Ԃ�
        //**********************************************************************
        /// <summary>
        /// Configuration�̔z�񂩂�w�肵���L�[�̒l��Ԃ�
        /// </summary>
        /// <param name="settings">Configuration�̔z��</param>
        /// <param name="strKey">�L�[</param>
        /// <returns></returns>
        //**********************************************************************
        public string GetSettingValue(KeyValueConfigurationCollection settings, string strKey)
        {
            return GetSettingValue(settings, strKey, "");
        }
        #endregion

        #region Configuration�̔z�񂩂�w�肵���L�[�̒l��Ԃ�
        //**********************************************************************
        /// <summary>
        /// Configuration�̔z�񂩂�w�肵���L�[�̒l��Ԃ�
        /// </summary>
        /// <param name="settings">Configuration�̔z��</param>
        /// <param name="strKey">�L�[</param>
        /// <param name="strDefault">����l</param>
        /// <returns></returns>
        //**********************************************************************
        public string GetSettingValue(KeyValueConfigurationCollection settings, 
            string strKey, string strDefault)
        {
            string strReturn = "";
            if (settings != null)
            {
                KeyValueConfigurationElement element = settings[strKey];
                if (element != null)
                {
                    strReturn = element.Value;
                }
            }
            if (IsEmpty(strReturn))
            {
                strReturn = strDefault;
            }
            return strReturn;
        }
        #endregion

        #region �w�肵���L�[�̒l��Configuration�̔z��ɐݒ肷��
        //**********************************************************************
        /// <summary>
        /// �w�肵���L�[�̒l��Configuration�̔z��ɐݒ肷��
        /// </summary>
        /// <param name="settings">Configuration�̔z��</param>
        /// <param name="strKey">�L�[</param>
        /// <param name="strValue">�l</param>
        //**********************************************************************
        public void SetSettingValue(KeyValueConfigurationCollection settings, string strKey, string strValue)
        {
            if (settings != null)
            {
                KeyValueConfigurationElement element = settings[strKey];
                if (element != null)
                {
                    element.Value = strValue;
                }
                else
                {
                    settings.Add(strKey, strValue);
                }
            }
        }
        #endregion

        #region �w�肵���v���Z�X�͎w�肵�������ŋN������
        //**********************************************************************
        /// <summary>
        /// �w�肵���v���Z�X�͎w�肵�������ŋN������
        /// </summary>
        /// <param name="strProcessName">�v���Z�X�̖��O</param>
        /// <param name="strWorkDirectory">�v���Z�X�̋N���f�B���N�g��</param>
        /// <param name="strParameter">�v���Z�X�̋N���p�����[�^</param>
        /// <returns></returns>
        //**********************************************************************
        public bool ExecProcess(string strProcessName, string strWorkDirectory, string strParameter)
        {
            return ExecProcess(strProcessName, strWorkDirectory, strParameter, true);
        }
        #endregion

        #region �w�肵���v���Z�X�͎w�肵�������ŋN������
        //**********************************************************************
        /// <summary>
        /// �w�肵���v���Z�X�͎w�肵�������ŋN������
        /// </summary>
        /// <param name="strProcessName">�v���Z�X�̖��O</param>
        /// <param name="strWorkDirectory">�v���Z�X�̋N���f�B���N�g��</param>
        /// <param name="strParameter">�v���Z�X�̋N���p�����[�^</param>
        /// <param name="bShellExecute">�V�F�����g�p���邩�ǂ����������l</param>
        /// <returns></returns>
        //**********************************************************************
        public bool ExecProcess(string strProcessName, string strWorkDirectory, string strParameter, bool bShellExecute)
        {
            bool bReturn = false;
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = strProcessName;
                if (!IsEmpty(strParameter))
                {
                    startInfo.Arguments = strParameter;
                }
                if (!IsEmpty(strWorkDirectory))
                {
                    startInfo.WorkingDirectory = strWorkDirectory;
                }
                if (bShellExecute)
                {
                    startInfo.UseShellExecute = true;
                }

                Process p = Process.Start(startInfo);
                if (bShellExecute)
                {
                    p.WaitForExit();
                    p.Close();
                }
                bReturn = true;
            }
            catch (Exception exp)
            {
                NCLogger.GetInstance().WriteExceptionLog(exp);
            }
            return bReturn;
        }

        #endregion

        #region �w�肵���t�@�C�����T�[�o�ɑ��݂��邩�ǂ����𔻖�����
        //**********************************************************************
        /// <summary>
        /// �w�肵���t�@�C�����T�[�o�ɑ��݂��邩�ǂ����𔻖�����
        /// </summary>
        /// <param name="strServer">�T�[�o��</param>
        /// <param name="strDir">�f�B���N�g����</param>
        /// <param name="strUser">�T�[�o�A�N�Z�X�p���[�U</param>
        /// <param name="strPassword">�T�[�o�A�N�Z�X�p�p�X���[�h</param>
        /// <param name="strFileName">�t�@�C����</param>
        /// <returns></returns>
        //**********************************************************************
        public bool FindFile(string strServer, string strDir, string strUser, string strPassword, string strFileName)
        {
            bool bReturn = false;
            try
            {
                string strUri = String.Format("ftp://{0}/{1}/{2}", strServer, strDir, strFileName);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(strUri);
                if (!IsEmpty(strUser))
                {
                    webRequest.Credentials = new NetworkCredential(strUser, strPassword);
                }
                webRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                webRequest.KeepAlive = false;
                webRequest.UseBinary = false;

                using (WebResponse res = webRequest.GetResponse())
                {
                    using (Stream st = res.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(st, System.Text.Encoding.GetEncoding("Shift_JIS")))
                        {
                            string strBuf = sr.ReadToEnd();
                            if (strBuf != null && strBuf.Contains(strFileName))
                            {
                                bReturn = true;
                            }
                            sr.Close();
                        }
                        st.Close();
                    }
                    res.Close();
                }
            }
            catch (Exception exp)
            {
                NCLogger.GetInstance().WriteInfoLog(strFileName);
                NCLogger.GetInstance().WriteExceptionLog(exp);
            }
            return bReturn;
        }
        #endregion

        #region �w�肵���t�@�C�����T�[�o���烍�[�J���Ƀ_�E�����[�h����
        //**********************************************************************
        /// <summary>
        /// �w�肵���t�@�C�����T�[�o���烍�[�J���Ƀ_�E�����[�h����
        /// </summary>
        /// <param name="strServer">�T�[�o��</param>
        /// <param name="strDir">�f�B���N�g����</param>
        /// <param name="strUser">�T�[�o�A�N�Z�X�p���[�U</param>
        /// <param name="strPassword">�T�[�o�A�N�Z�X�p�p�X���[�h</param>
        /// <param name="strFileName">�t�@�C����</param>
        /// <param name="strLocalFileName">���[�J���t�@�C����</param>
        /// <returns></returns>
        //**********************************************************************
        public bool FileDownload(string strServer, string strDir, string strUser, 
            string strPassword, string strFileName, string strLocalFileName)
        {
            bool bReturn = false;
            try
            {
                string strUri = String.Format("ftp://{0}/{1}/{2}", strServer, strDir, strFileName);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(strUri);
                if (!IsEmpty(strUser))
                {
                    webRequest.Credentials = new NetworkCredential(strUser, strPassword);
                }
                webRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                webRequest.KeepAlive = false;
                webRequest.UseBinary = true;

                using (WebResponse res = webRequest.GetResponse())
                {
                    using (Stream st = res.GetResponseStream())
                    {
                        using (FileStream fs = new FileStream(strLocalFileName, FileMode.Create, FileAccess.Write))
                        {
                            byte[] buffer = new byte[1024];
                            while (true)
                            {
                                int readSize = st.Read(buffer, 0, buffer.Length);
                                if (readSize == 0)
                                {
                                    break;
                                }
                                fs.Write(buffer, 0, readSize);
                            }
                            fs.Close();
                        }
                        st.Close();
                    }
                    res.Close();
                    bReturn = true;
                }
            }
            catch (Exception exp)
            {
                NCLogger.GetInstance().WriteExceptionLog(exp);
            }
            return bReturn;
        }
        #endregion

        #region �w�肵���t�@�C�������[�J������T�[�o�ɃA�b�v���[�h����
        //**********************************************************************
        /// <summary>
        /// �w�肵���t�@�C�������[�J������T�[�o�ɃA�b�v���[�h����
        /// </summary>
        /// <param name="strServer">�T�[�o��</param>
        /// <param name="strDir">�f�B���N�g����</param>
        /// <param name="strUser">�T�[�o�A�N�Z�X�p���[�U</param>
        /// <param name="strPassword">�T�[�o�A�N�Z�X�p�p�X���[�h</param>
        /// <param name="strFileName">�t�@�C����</param>
        /// <param name="strLocalFileName">���[�J���t�@�C����</param>
        /// <returns></returns>
        //**********************************************************************
        public bool FileUpload(string strServer, string strDir, string strUser, 
            string strPassword, string strFileName, string strLocalFileName)
        {
            bool bReturn = false;
            try
            {
                string strUri = String.Format("ftp://{0}/{1}/{2}", strServer, strDir, strFileName);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(strUri);
                if (!IsEmpty(strUser))
                {
                    webRequest.Credentials = new NetworkCredential(strUser, strPassword);
                }
                webRequest.Method = WebRequestMethods.Ftp.UploadFile;
                webRequest.KeepAlive = false;
                webRequest.UseBinary = true;

                using (FileStream fs = new FileStream(strLocalFileName, FileMode.Open, FileAccess.Read))
                {
                    using (Stream st = webRequest.GetRequestStream())
                    {
                        byte[] buffer = new byte[1024];
                        while (true)
                        {
                            int readSize = fs.Read(buffer, 0, buffer.Length);
                            if (readSize == 0)
                            {
                                break;
                            }
                            st.Write(buffer, 0, readSize);
                        }
                        st.Close();
                    }
                    fs.Close();
                    bReturn = true;
                }
            }
            catch (Exception exp)
            {
                NCLogger.GetInstance().WriteExceptionLog(exp);
            }
            return bReturn;
        }
        #endregion

        #region �w�肵���t�@�C�����T�[�o����폜����
        //**********************************************************************
        /// <summary>
        /// �w�肵���t�@�C�����T�[�o����폜����
        /// </summary>
        /// <param name="strServer">�T�[�o��</param>
        /// <param name="strDir">�f�B���N�g����</param>
        /// <param name="strUser">�T�[�o�A�N�Z�X�p���[�U</param>
        /// <param name="strPassword">�T�[�o�A�N�Z�X�p�p�X���[�h</param>
        /// <param name="strFileName">�t�@�C����</param>
        /// <returns></returns>
        //**********************************************************************
        public bool FileDelete(string strServer, string strDir, string strUser,
            string strPassword, string strFileName)
        {
            bool bReturn = false;
            try
            {
                string strUri = String.Format("ftp://{0}/{1}/{2}", strServer, strDir, strFileName);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(strUri);
                if (!IsEmpty(strUser))
                {
                    webRequest.Credentials = new NetworkCredential(strUser, strPassword);
                }
                webRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                webRequest.KeepAlive = false;
                webRequest.UseBinary = true;

                using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
                {
                    if (webResponse != null)
                    {
                        if (webResponse.StatusCode == FtpStatusCode.FileActionOK)
                        {
                            bReturn = true;
                        }
                        webResponse.Close();                        
                    }
                }
            }
            catch (Exception exp)
            {
                NCLogger.GetInstance().WriteExceptionLog(exp);
            }
            return bReturn;
        }
        #endregion
        
        #region �w�肵���f�B���N�g���̓T�[�o�ɑ��݂��邩�ǂ����𒲂ׂ�
        //**********************************************************************
        /// <summary>
        /// �w�肵���f�B���N�g���̓T�[�o�ɑ��݂��邩�ǂ����𒲂ׂ�A
        /// </summary>
        /// <param name="strServer"></param>
        /// <param name="strDir"></param>
        /// <param name="strUser"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        //**********************************************************************
        public bool DirExists(string strServer, string strDir, string strUser, 
            string strPassword)
        {
            bool bReturn = false;
            try
            {
                string strUri = String.Format("ftp://{0}/{1}", strServer, strDir);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(strUri);
                if (!IsEmpty(strUser))
                {
                    webRequest.Credentials = new NetworkCredential(strUser, strPassword);
                }
                webRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                webRequest.KeepAlive = false;
                webRequest.UseBinary = true;

                using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
                {
                    if (webResponse != null)
                    {
                        if (webResponse.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                        {
                            bReturn = true;

                        }
                        webResponse.Close();
                    }
                }                
            }
            catch (Exception exp)
            {
                NCLogger.GetInstance().WriteExceptionLog(exp);
            }
            return bReturn;
        }
        #endregion

        #region �T�[�o�Ɏw�肵���f�B���N�g�����쐬����
        //**********************************************************************
        /// <summary>
        /// �T�[�o�Ɏw�肵���f�B���N�g�����쐬����
        /// </summary>
        /// <param name="strServer"></param>
        /// <param name="strDir"></param>
        /// <param name="strUser"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        //**********************************************************************
        public bool MakeDir(string strServer, string strDir, string strUser,
            string strPassword)
        {
            bool bReturn = false;
            try
            {
                string strUri = String.Format("ftp://{0}/{1}", strServer, strDir);
                FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create(strUri);
                if (!IsEmpty(strUser))
                {
                    webRequest.Credentials = new NetworkCredential(strUser, strPassword);
                }
                webRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                webRequest.KeepAlive = false;
                webRequest.UseBinary = true;

                using (FtpWebResponse webResponse = (FtpWebResponse)webRequest.GetResponse())
                {
                    if (webResponse != null)
                    {
                        if (webResponse.StatusCode == FtpStatusCode.PathnameCreated)
                        {
                            bReturn = true;
                        }
                        webResponse.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                NCLogger.GetInstance().WriteExceptionLog(exp);
            }
            return bReturn;
        }
        #endregion
        
        #region �����̊ۂ�
        //**********************************************************************
        /// <summary>
        /// �����̊ۂ�
        /// </summary>
        /// <param name="iDefault"></param>
        /// <param name="nMode"></param>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        //**********************************************************************
        public string DigitMarume(string strFormat, decimal iDefault, int nMode, int iIndex)
        {
            if (strFormat == string.Empty)
            {
                strFormat = "#,##0.";
            }
            string strRet;
            for (int i = 1; i <= iIndex; i++)
            {
                strFormat = strFormat + "0";
            }
            if (nMode == 1)
            {
                strRet = Math.Round(iDefault, iIndex).ToString(strFormat);
                return strRet;
            }
            else
            {
                string[] i = iDefault.ToString().Split('.');
                if (i.Length > 1)
                {
                    if (i[1].Length <= iIndex)
                    {
                        strRet = Math.Round(iDefault, iIndex).ToString(strFormat);
                        return strRet;
                    }
                    else
                    {
                        string str = i[1].Substring(0, iIndex);
                        str = i[0] + "." + str;
                        return str;
                    }
                }
                else
                {
                    if (iIndex == 0)
                    {
                        return iDefault.ToString();
                    }
                }
            }
            return "";
        }
		#endregion

		#region �S�p�����i������A���{��A���̑��̑S�p�������܂ށj�ł��邩�ǂ����𔻒f����
		/// <summary>
		/// �S�p�����i������A���{��A���̑��̑S�p�������܂ށj�ł��邩�ǂ����𔻒f����
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public bool IsFullWidthCharacter(char c)
		{
			return (c >= 0x4E00 && c <= 0x9FFF) ||
				   (c >= 0x3400 && c <= 0x4DBF) ||
				   (c >= 0xF900 && c <= 0xFAFF) ||
				   (c >= 0xFF01 && c <= 0xFF60) ||
				   (c >= 0xFFE0 && c <= 0xFFE6) ||
				   char.GetUnicodeCategory(c) == UnicodeCategory.OtherLetter;
		}
		#endregion
	}
}
