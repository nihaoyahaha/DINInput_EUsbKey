namespace DI.NCFrameWork
{	
	using System;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;

    //************************************************************************
	/// <summary>
    /// �w�肳�ꂽ .ini �t�@�C���i�������t�@�C���j�́A�w�肳�ꂽ�Z�N�V�������ɁA
    /// �w�肳�ꂽ�L�[���Ƃ���Ɋ֘A�t����ꂽ��������i�[�܂��͎擾����
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
        /// .ini�t�@�C��������
		/// </summary>
		/// <param name="strIniName">�t�@�C����</param>
        //************************************************************************
		public NCIniFile(string strIniName)
		{
			m_strPath = strIniName;
		}

        //************************************************************************
		/// <summary>
        /// �w�肳�ꂽ .ini �t�@�C���i�������t�@�C���j�́A�w�肳�ꂽ�Z�N�V�������ɁA
        /// �w�肳�ꂽ�L�[���Ƃ���Ɋ֘A�t����ꂽ��������i�[����
		/// </summary>
		/// <param name="Section">�Z�N�V����</param>
		/// <param name="Key">�L�[</param>
		/// <param name="Value">�o�����[</param>
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
        /// �w�肳�ꂽ .ini �t�@�C���i�������t�@�C���j�̎w�肳�ꂽ�Z�N�V�������ɂ���A
        /// �w�肳�ꂽ�L�[�Ɋ֘A�t�����Ă��镶������擾����
		/// </summary>
		/// <param name="Section">�Z�N�V����</param>
		/// <param name="Key">�L�[</param>
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
