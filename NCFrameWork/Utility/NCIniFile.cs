namespace DI.NCFrameWork
{	
	using System;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;

    //************************************************************************
	/// <summary>
    /// 指定された .ini ファイル（初期化ファイル）の、指定されたセクション内に、
    /// 指定されたキー名とそれに関連付けられた文字列を格納または取得する
	/// </summary>
    //************************************************************************	
	public class NCIniFile
	{
		private string m_strPath; //ini file path

		[DllImport("kernel32")]

		private static extern bool WritePrivateProfileString(string section,string key,string val,string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int size,string filePath);

        //************************************************************************
		/// <summary>
        /// .iniファイル初期化
		/// </summary>
		/// <param name="strIniName">ファイル名</param>
        //************************************************************************
		public NCIniFile(string strIniName)
		{
			m_strPath = strIniName;
		}

        //************************************************************************
		/// <summary>
        /// 指定された .ini ファイル（初期化ファイル）の、指定されたセクション内に、
        /// 指定されたキー名とそれに関連付けられた文字列を格納する
		/// </summary>
		/// <param name="Section">セクション</param>
		/// <param name="Key">キー</param>
		/// <param name="Value">バリュー</param>
        //************************************************************************
		public void IniWriteValue(string Section, string Key, string Value)
		{
            try
            {
                WritePrivateProfileString(Section, Key, Value, this.m_strPath);
            }
            catch
            {
            }
		}
	
        //************************************************************************
		/// <summary>
        /// 指定された .ini ファイル（初期化ファイル）の指定されたセクション内にある、
        /// 指定されたキーに関連付けられている文字列を取得する
		/// </summary>
		/// <param name="Section">セクション</param>
		/// <param name="Key">キー</param>
		/// <param name="def">def</param>
        //************************************************************************
		public string IniReadValue(string Section, string Key, string def)
		{
			StringBuilder temp = new StringBuilder(255);
            try
            {
                int i = GetPrivateProfileString(Section, Key, def, temp, 255, this.m_strPath);
            }
            catch
            {
            }
			return temp.ToString();
		}
	}
}
