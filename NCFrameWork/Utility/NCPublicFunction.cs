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
	/// NCPublicFunction の概要の説明です。
	/// </summary>
	//************************************************************************
    [Serializable]
	public class NCPublicFunction
	{
		public const string YYYYMMDD = "yyyyMMdd";
		public const string YYYYSlashMMSlashDD = "yyyy/MM/dd";
        public static string[] DAYOFWEEK = { "日曜日", "月曜日", "火曜日", "水曜日", "木曜日", "金曜日", "土曜日" };
		
        public NCPublicFunction()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
        }

        #region STRINGが空値の判断
        //************************************************************************
		/// <summary>
		/// STRINGが空値の判断
		/// </summary>
		/// <param name="strValue">文字列</param>
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

        #region StringからIntへ変換する
        //************************************************************************
		/// <summary>
		/// StringからIntへ変換する
		/// </summary>
		/// <param name="strValue">文字列</param>
		/// <param name="iDefault">デフォルト値</param>
		/// <returns>int </returns>
		//************************************************************************
		//StringからIntへ変換する
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

        #region StringからLongへ変換する
        //************************************************************************
		/// <summary>
		/// StringからLongへ変換する
		/// </summary>
		/// <param name="strValue">文字列</param>
		/// <param name="lDefault">デフォルト値</param>
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

        #region StringからDoubleへ変換する
        //************************************************************************
		/// <summary>
		/// StringからDoubleへ変換する
		/// </summary>
		/// <param name="strValue">文字列</param>
		/// <param name="dDefault">デフォルト値</param>
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

        #region StringからDecimalへ変換する
        //************************************************************************
        /// <summary>
        /// StringからDecimalへ変換する
        /// </summary>
        /// <param name="strValue">文字列</param>
        /// <param name="dDefault">デフォルト値</param>
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

        #region StringからDateTimeへ変換する
        //************************************************************************
		/// <summary>
		/// StringからDateTimeへ変換する
		/// フォーマットの型：yyyy/MM/dd HH:mm:ss
		///					yyyy/MM/dd
		/// strCultureName:"en-US"							
		///				"ru-RU"	
		///				"ja-JP"
		/// </summary>
		/// <param name="strValue">文字列</param>
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

        #region 日付データフォーマット転換
        //************************************************************************
		/// <summary>
		/// 日付データフォーマット転換 mode(1:YYYYMMDD -> YYYY/MM/DD   2:YYYY/MM/DD -> YYYYMMDD)
		/// </summary>
		/// <param name="strVale">文字列</param>
		/// <param name="iMode">モード</param>
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

        #region 日付データが文字列に変換する
        //************************************************************************
		/// <summary>
		/// 日付データが文字列に変換する
		/// </summary>
		/// <param name="dtValue">日付データ</param>
		/// <param name="iMode">モード</param>
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

        #region object 表す decimal を返します
        //************************************************************************
        /// <summary>
        /// double ObjectToDecimal(object objValue)
        /// </summary>
        /// <remarks>
        /// object 表す decimalを返します
        /// </remarks>
        /// <param name="objValue">object 値</param>
        /// <returns>decimal タイプの値</returns>
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

		#region object 表す double を返します
		//************************************************************************
		/// <summary>
		/// double ObjectToDouble(object objValue)
		/// </summary>
		/// <remarks>
		/// object 表す doubleを返します
		/// </remarks>
		/// <param name="objValue">object 値</param>
		/// <returns>double タイプの値</returns>
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

		#region object 表す string を返します
		//************************************************************************
		/// <summary>
		/// string ObjectToString(object objValue)
		/// </summary>
		/// <remarks>
		/// object 表す stringを返します
		/// </remarks>
		/// <param name="objValue">object 値</param>
		/// <returns>string タイプの値</returns>
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

		#region object 表す Int32 を返します
		//************************************************************************
		/// <summary>
		/// int ObjectToInt(object objValue)
		/// </summary>
		/// <remarks>
		/// object 表す Int32 を返します
		/// </remarks>
		/// <param name="objValue">object 値</param>
		/// <returns>Int32 タイプの値</returns>
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

		#region object 表す long を返します
		//************************************************************************
		/// <summary>
		/// long ObjectToLong(object objValue)
		/// </summary>
		/// <remarks>
		/// object を表す long を返します
		/// </remarks>
		/// <param name="objValue">object 値</param>
		/// <returns>long タイプの値</returns>
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
				{	//初期値設定
					lValue = 0;
				}
			}

			return lValue;
		}
		#endregion

		#region object 表す 日付を返します
		//************************************************************************
		/// <summary>
		/// DateTime ObjectToDateTime(object objValue)
		/// </summary>
		/// <remarks>
		/// object 表す 日付を返します
		/// </remarks>
		/// <param name="objValue">object 値</param>
		/// <returns>DateTime タイプの値</returns>
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

        #region 日付対する曜日を返す
        //**********************************************************************
        /// <summary>
        /// 日付対する曜日を返す
        /// </summary>
        /// <param name="dtValue">日付</param>
        /// <returns>曜日を表す文字列</returns>
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

        #region DateTime 指定した書式の string を返します
        //************************************************************************
		/// <summary>
		/// string GetDateTimeString()
		/// </summary>
		/// <remarks>
		/// DateTime 指定した書式の string を返します
		/// </remarks>
		/// <param name="dt">指定した時刻</param>
		/// <returns>DateTime 指定した書式の string </returns>
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

        #region 日付正確性判断
        /// <summary>
        /// 日付正確性判断
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

        #region 全角文字判断
        /// <summary>
        /// 全角文字判断
        /// </summary>
        /// <param name="strValue">項目文字列</param>
        /// <returns>判断結果</returns>
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

        #region 全角仮名判断
        /// <summary>
        /// 全角仮名判断
        /// </summary>
        /// <param name="strValue">項目文字列</param>
        /// <returns>判断結果</returns>
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
                if (!((charCode >= 0x8140 && charCode <= 0x81FC)		//全角符号
                    || (charCode >= 0x824F && charCode <= 0x8258)		//全角数字
                    || (charCode >= 0x8260 && charCode <= 0x829A)		//全角英字
                    || (charCode >= 0x8340 && charCode <= 0x8396)
                    || (charCode >= 0x82A0 && charCode <= 0x82F1)))		//全角仮名
                {
                    //全角文字は有りません
                    retBool = false;
                    break;
                }
            }
            return retBool;
        }
        #endregion

        #region 半角の英数字、記号判断
        /// <summary>
        /// 半角の英数字、記号判断
        /// </summary>
        /// <param name="strValue">項目文字列</param>
        /// <returns>判断結果</returns>
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

        #region 英字判断
        /// <summary>
        /// 英字判断
        /// </summary>
        /// <param name="strValue">項目文字列</param>
        /// <returns>判断結果</returns>
        public bool IsBytesAlpha(string strValue)
        {
            Regex regex = new Regex(@"^[a-zA-Z ]+$");
            return regex.IsMatch(strValue);
        }
        #endregion

        #region 数字判断
        /// <summary>
        /// 数字判断
        /// </summary>
        /// <param name="strNumber">項目文字列</param>
        /// <returns>判断結果</returns>
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

        #region 英数字判断
        /// <summary>
        /// 英数字判断
        /// </summary>
        /// <param name="strValue">項目文字列</param>
        /// <returns>判断結果</returns>
        public bool IsAlphaNumeric(string strValue)
        {
            Regex regAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");

            return !regAlphaNumericPattern.IsMatch(strValue);
        }
        #endregion

        #region 全部は数字判断
        /// <summary>
        /// 全部は数字判断
        /// </summary>
        /// <param name="strNumber">項目文字列</param>
        /// <returns>判断結果</returns>
        public bool IsWholeNumber(string strNumber)
        {
            Regex regNotWholePattern = new Regex("[^0-9]");

            return !regNotWholePattern.IsMatch(strNumber);
        }
        #endregion

        #region 整数判断
        /// <summary>
        /// 整数判断
        /// </summary>
        /// <param name="strNumber">項目文字列</param>
        /// <returns>判断結果</returns>
        public bool IsInteger(string strNumber)
        {
            Regex regNotIntPattern = new Regex("[^0-9-]");
            Regex regIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");

            return !regNotIntPattern.IsMatch(strNumber) &&
                regIntPattern.IsMatch(strNumber);
        }
        #endregion 

        #region 文字列は全角文字であるかどうかを判明する
        //**********************************************************************
        /// <summary>
        /// 文字列は全角文字であるかどうかを判明する
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

        #region 文字列は全角文字であるかどうかを判明する
        //**********************************************************************
        /// <summary>
        /// 文字列は半角文字であるかどうかを判明する
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

        #region メール有効判断
        /// <summary>
        /// メール有効判断
        /// </summary>
        /// <param name="strMail">項目文字列</param>
        /// <returns>判断結果</returns>
        public bool IsValidEmail(string strMail)
        {
            Regex regMail = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regMail.IsMatch(strMail);
        }
        #endregion

        #region 有効なURL判断
        /// <summary>
        /// 有効なURL判断
        /// </summary>
        /// <param name="strUrl">URL文字列</param>
        /// <param name="bPrefix">接頭語</param>
        /// <returns>判断結果</returns>
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

        #region 有効なIPアドレス判断
        /// <summary>
        /// 有効なIPアドレス判断
        /// </summary>
        /// <param name="strIP">IPアドレス文字列</param>
        /// <returns>判断結果</returns>
        public bool IsValidIP(string strIP)
        {
            Regex regIP = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            return regIP.IsMatch(strIP);
        }
        #endregion

        #region 郵便番号判断
        /// <summary>
        /// PostCode判断
        /// </summary>
        /// <param name="strPostCode">項目文字列</param>
        /// <returns>判断結果</returns>
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

        #region 電話番号チェック
        /// <summary>
        /// 電話番号チェック
        /// </summary>
        /// <param name="strTelNo">項目文字列</param>
        /// <returns>判断結果</returns>
        public bool CheckTelNo(string strTelNo)
        {
            Regex regTel = new Regex(@"^[0-9]{1,7}-[0-9]{1,4}-[0-9]{1,5}$");
            return regTel.IsMatch(strTelNo);
        }
        #endregion 

        #region コード転換
        /// <summary>
        /// コード転換
        /// </summary>
        /// <param name="strValue">項目文字列</param>
        /// <returns>転換結果</returns>
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
                    //半角
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
                    //全角
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

        #region 指定した文字位置から開始し、指定した文字数の文字列を返す
        //**********************************************************************
        /// <summary>
        /// 指定した文字位置から開始し、指定した文字数の文字列を返す
        /// </summary>
        /// <param name="strValue">文字列</param>
        /// <param name="nStart">開始位置</param>
        /// <param name="nLens">文字数</param>
        /// <returns>文字列</returns>
        //**********************************************************************
        public string SubString(string strValue, int nStart, int nLens)
        {
            string strReturn = "";
            try
            {
                if (!IsEmpty(strValue))
                {
                    // 指定した文字列の右側から、指定した文字数の文字列を返す
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

        #region 指定した文字の指定したバイト数の文字列を返す
        //**********************************************************************
        /// <summary>
        /// 指定した文字位置から開始し、指定した文字数の文字列を返す
        /// </summary>
        /// <param name="strValue">文字列</param>
        /// <param name="nLens">文字数</param>
        /// <returns>文字列</returns>
        //**********************************************************************
        public string SubStringB(string strValue, int nLens)
        {
            string strReturn = "";
            try
            {
                if (!IsEmpty(strValue))
                {
                    // 指定した文字列の右側から、指定した文字数の文字列を返す
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

        #region 10進数小数点値を有効数字3桁の数値に丸める（ただし、1000を超える場合は、数値を整数値に丸める）
        /// <summary>
        /// 10進数小数点値を有効数字3桁の数値に丸める（ただし、1000を超える場合は、数値を整数値に丸める）
        /// </summary>
        /// <param name="dValue">丸め対象</param>
        /// <returns>丸めされた数字を返す</returns>
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

        #region 数字表示されているカラーを返す
        //**********************************************************************
        /// <summary>
        /// 数字表示されているカラーを返す
        /// </summary>
        /// <param name="nColor">数字表示されているカラー</param>
        /// <returns>カラー</returns>
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

        #region カラーの32ビット数字を返す
        //**********************************************************************
        /// <summary>
        /// カラーの32ビット数字を返す
        /// </summary>
        /// <param name="color">カラー</param>
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

        #region カラー対する反転色を取得する
        //**********************************************************************
        /// <summary>
        /// カラー対する反転色を取得する
        /// </summary>
        /// <param name="nColor">数字表示されているカラー</param>
        /// <returns>カラー対する反転色</returns>
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

        #region カラー対する反転色を取得する
        //**********************************************************************
        /// <summary>
        /// カラー対する反転色を取得する
        /// </summary>
        /// <param name="nColor">数字表示されているカラー</param>
        /// <returns>カラー対する反転色</returns>
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

        #region ファイルからImageを作成する
        //**********************************************************************
        /// <summary>
        /// ファイルからImageを作成する
        /// </summary>
        /// <param name="strFileName">ファイル名</param>
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

        #region ファイルからImageを作成する
        //**********************************************************************
        /// <summary>
        /// ファイルからImageを作成する
        /// </summary>
        /// <param name="strFileName">ファイル名</param>
        /// <param name="nWidth">幅</param>
        /// <param name="nHeight">高さ</param>
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
        /// サムネイルコールバックメソッド
        /// </summary>
        /// <returns></returns>
        private bool ThumbnailCallback()
        {
            return false;
        }
        #endregion

        #region 該当プロセスのユーザ別のアプリ構成ファイルのインスタンスを取得する
        //**********************************************************************
        /// <summary>
        /// 該当プロセスのユーザ別のアプリ構成ファイルのインスタンスを取得する
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

        #region Configurationの配列から指定したキーの値を返す
        //**********************************************************************
        /// <summary>
        /// Configurationの配列から指定したキーの値を返す
        /// </summary>
        /// <param name="settings">Configurationの配列</param>
        /// <param name="strKey">キー</param>
        /// <returns></returns>
        //**********************************************************************
        public string GetSettingValue(KeyValueConfigurationCollection settings, string strKey)
        {
            return GetSettingValue(settings, strKey, "");
        }
        #endregion

        #region Configurationの配列から指定したキーの値を返す
        //**********************************************************************
        /// <summary>
        /// Configurationの配列から指定したキーの値を返す
        /// </summary>
        /// <param name="settings">Configurationの配列</param>
        /// <param name="strKey">キー</param>
        /// <param name="strDefault">既定値</param>
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

        #region 指定したキーの値をConfigurationの配列に設定する
        //**********************************************************************
        /// <summary>
        /// 指定したキーの値をConfigurationの配列に設定する
        /// </summary>
        /// <param name="settings">Configurationの配列</param>
        /// <param name="strKey">キー</param>
        /// <param name="strValue">値</param>
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

        #region 指定したプロセスは指定した引数で起動する
        //**********************************************************************
        /// <summary>
        /// 指定したプロセスは指定した引数で起動する
        /// </summary>
        /// <param name="strProcessName">プロセスの名前</param>
        /// <param name="strWorkDirectory">プロセスの起動ディレクトリ</param>
        /// <param name="strParameter">プロセスの起動パラメータ</param>
        /// <returns></returns>
        //**********************************************************************
        public bool ExecProcess(string strProcessName, string strWorkDirectory, string strParameter)
        {
            return ExecProcess(strProcessName, strWorkDirectory, strParameter, true);
        }
        #endregion

        #region 指定したプロセスは指定した引数で起動する
        //**********************************************************************
        /// <summary>
        /// 指定したプロセスは指定した引数で起動する
        /// </summary>
        /// <param name="strProcessName">プロセスの名前</param>
        /// <param name="strWorkDirectory">プロセスの起動ディレクトリ</param>
        /// <param name="strParameter">プロセスの起動パラメータ</param>
        /// <param name="bShellExecute">シェルを使用するかどうかを示す値</param>
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

        #region 指定したファイルをサーバに存在するかどうかを判明する
        //**********************************************************************
        /// <summary>
        /// 指定したファイルをサーバに存在するかどうかを判明する
        /// </summary>
        /// <param name="strServer">サーバ名</param>
        /// <param name="strDir">ディレクトリ名</param>
        /// <param name="strUser">サーバアクセス用ユーザ</param>
        /// <param name="strPassword">サーバアクセス用パスワード</param>
        /// <param name="strFileName">ファイル名</param>
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

        #region 指定したファイルをサーバからローカルにダウンロードする
        //**********************************************************************
        /// <summary>
        /// 指定したファイルをサーバからローカルにダウンロードする
        /// </summary>
        /// <param name="strServer">サーバ名</param>
        /// <param name="strDir">ディレクトリ名</param>
        /// <param name="strUser">サーバアクセス用ユーザ</param>
        /// <param name="strPassword">サーバアクセス用パスワード</param>
        /// <param name="strFileName">ファイル名</param>
        /// <param name="strLocalFileName">ローカルファイル名</param>
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

        #region 指定したファイルをローカルからサーバにアップロードする
        //**********************************************************************
        /// <summary>
        /// 指定したファイルをローカルからサーバにアップロードする
        /// </summary>
        /// <param name="strServer">サーバ名</param>
        /// <param name="strDir">ディレクトリ名</param>
        /// <param name="strUser">サーバアクセス用ユーザ</param>
        /// <param name="strPassword">サーバアクセス用パスワード</param>
        /// <param name="strFileName">ファイル名</param>
        /// <param name="strLocalFileName">ローカルファイル名</param>
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

        #region 指定したファイルをサーバから削除する
        //**********************************************************************
        /// <summary>
        /// 指定したファイルをサーバから削除する
        /// </summary>
        /// <param name="strServer">サーバ名</param>
        /// <param name="strDir">ディレクトリ名</param>
        /// <param name="strUser">サーバアクセス用ユーザ</param>
        /// <param name="strPassword">サーバアクセス用パスワード</param>
        /// <param name="strFileName">ファイル名</param>
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
        
        #region 指定したディレクトリはサーバに存在するかどうかを調べる
        //**********************************************************************
        /// <summary>
        /// 指定したディレクトリはサーバに存在するかどうかを調べる、
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

        #region サーバに指定したディレクトリを作成する
        //**********************************************************************
        /// <summary>
        /// サーバに指定したディレクトリを作成する
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
        
        #region 小数の丸め
        //**********************************************************************
        /// <summary>
        /// 小数の丸め
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

		#region 全角文字（中国語、日本語、その他の全角文字を含む）であるかどうかを判断する
		/// <summary>
		/// 全角文字（中国語、日本語、その他の全角文字を含む）であるかどうかを判断する
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
