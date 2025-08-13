//*****************************************************************************
// ユーザ名					ニューコン
// システム名				
// サブシステム名			EUSBキークラス
// 作成者					蒋@NC
// 改版日					2008/04/24
// 改版内容					新規作成
//*****************************************************************************
namespace DI.NCFrameWork
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Runtime.InteropServices;

	public class NCSecurity
	{
		private const string  _synew6dPath = "Tools/syunew6d.dll";
		[DllImport(_synew6dPath)]
		private static extern unsafe int FindPort(int start, byte* OutKeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int NT_GetIDVersion(ref short version, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int FindPort_2(int start, int in_data, int verf_data, byte* OutKeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int GetID(ref int id_1, ref int id_2, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int YRead(byte[] OutData, short Address, short mylen, string HKey, string LKey, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int YWrite(byte[] InData, short Address, short mylen, string HKey, string LKey, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int YReadString(byte[] outstring, short Address, short mylen, string HKey, string LKey, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int YWriteString(string InString, short Address, string HKey, string LKey, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int sRead(ref int in_data, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int sWrite(int out_data, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int sWrite_2(int out_data, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int SetWritePassword(string W_HKey, string W_LKey, string new_HKey, string new_LKey, byte* KeyPath);
		[DllImport(_synew6dPath)]
		private static extern unsafe int SetReadPassword(string W_HKey, string W_LKey, string new_HKey, string new_LKey, byte* KeyPath);


		private static byte[] KeyPath;
		// 読み取り　パスワード
		private static readonly string ReadPassword_H = "xa2sDfs9";
		private static readonly string ReadPassword_L = "sdf23Fl2";
		// 書込み　パスワード
		private static readonly string WritePassword_H = "EK3df4x4";
		private static readonly string WritePassword_L = "olW3893E";
		// バージョンNo
		private static readonly int VersionID = 2;

		/// <summary>
		/// ESubキーを初期化する
		/// </summary>
		/// <returns></returns>
		unsafe public static bool InitKey()
		{
			try
			{
				if (!CheckKey())
				{
					return false;
				}
				// 書込みパスワードを設定する
				fixed (byte* pKeyPath = KeyPath)
				{
					if (SetWritePassword("ffffffff", "ffffffff", WritePassword_H, WritePassword_L, pKeyPath) != 0)
					{
						if (SetWritePassword(WritePassword_H, WritePassword_L, WritePassword_H, WritePassword_L, pKeyPath) != 0)
						{
							return false;
						}
					}
				}
				// 読み込みパスワードを設定する
				fixed (byte* pKeyPath = KeyPath)
				{
					if (SetReadPassword("ffffffff", "ffffffff", ReadPassword_H, ReadPassword_L, pKeyPath) != 0)
					{
						if (SetReadPassword(WritePassword_H, WritePassword_L, ReadPassword_H, ReadPassword_L, pKeyPath) != 0)
						{
							return false;
						}
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// データをEUsbキーに設定する
		/// </summary>
		/// <param name="data">書込文字列</param>
		/// <returns></returns>
		unsafe public static bool SetData(string data)
		{
			return SetData(0, data);
		}

		/// <summary>
		/// 指定した開始位置から指定した文字列をEUsbキーに設定する
		/// </summary>
		/// <param name="start">書込開始位置のインデックス</param>
		/// <param name="data">書込文字列</param>
		/// <returns></returns>
		unsafe public static bool SetData(short start, string data)
		{
			try
			{
				if (!CheckKey())
				{
					return false;
				}
				string tempstr = data;
				// データをEUsbキーに書き込む
				fixed (byte* pKeyPath = KeyPath)
				{
					if (YWriteString(tempstr, start, WritePassword_H, WritePassword_L, pKeyPath) != 0)
					{
						return false;
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 指定した開始位置から指定した文字列をEUsbキーに設定する
		/// </summary>
		/// <param name="start">書込開始位置のインデックス</param>
		/// <param name="data">書込文字列</param>
		/// <returns></returns>
		unsafe public static bool SetData(short start, byte data)
		{
			try
			{
				if (!CheckKey())
				{
					return false;
				}
				//string tempstr = data;
				byte[] buf = new byte[1];
				buf[0] = data;
				// データをEUsbキーに書き込む
				fixed (byte* pKeyPath = KeyPath)
				{
					if (YWrite(buf, start, (short)buf.Length, WritePassword_H, WritePassword_L, pKeyPath) != 0)
					{
						return false;
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 指定した開始位置から指定した文字列をEUsbキーに設定する
		/// </summary>
		/// <param name="start">書込開始位置のインデックス</param>
		/// <param name="data">書込文字列</param>
		/// <returns></returns>
		unsafe public static bool SetData(short start, short data)
		{
			try
			{
				if (!CheckKey())
				{
					return false;
				}
				//string tempstr = data;                
				byte[] buf = new byte[2];
				buf[0] = (byte)(data / 256);
				buf[1] = (byte)(data % 256);
				// データをEUsbキーに書き込む
				fixed (byte* pKeyPath = KeyPath)
				{
					if (YWrite(buf, start, (short)buf.Length, WritePassword_H, WritePassword_L, pKeyPath) != 0)
					{
						return false;
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// EUsbキーからデータを取得する
		/// </summary>
		/// <param name="data">読取文字列</param>
		/// <returns></returns>
		unsafe public static bool GetData(ref string data)
		{
			return GetData(0, 1024, ref data);
		}

		/// <summary>
		/// EUsbキーから指定した文字位置から開始し、指定した文字数の文字列を取得する
		/// </summary>
		/// <param name="start">読取開始位置のインデックス</param>
		/// <param name="length">読取文字列の文字数</param>
		/// <param name="data">読取文字列</param>
		/// <returns></returns>
		unsafe public static bool GetData(short start, short length, ref string data)
		{
			try
			{
				if (!CheckKey())
				{
					return false;
				}
				// EUsbキーからデータを読み出す
				byte[] buf = new byte[length]; //ここのサイズは読み出しのサイズ＋1です。
				fixed (byte* pKeyPath = KeyPath)
				{
					if (YReadString(buf, start, length, ReadPassword_H, ReadPassword_L, pKeyPath) != 0)
					{
						return false;
					}
				}
				data = ByteConvertString(buf).TrimEnd();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// EUsbキーから指定した文字位置から開始し、指定した文字数の文字列を取得する
		/// </summary>
		/// <param name="start">読取開始位置のインデックス</param>
		/// <param name="data">読取文字列</param>
		/// <returns></returns>
		unsafe public static bool GetData(short start, ref byte data)
		{
			try
			{
				if (!CheckKey())
				{
					return false;
				}
				// EUsbキーからデータを読み出す
				byte[] buf = new byte[1]; //ここのサイズは読み出しのサイズ＋1です。
				fixed (byte* pKeyPath = KeyPath)
				{
					if (YRead(buf, start, 1, ReadPassword_H, ReadPassword_L, pKeyPath) != 0)
					{
						return false;
					}
				}

				data = buf[0];
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// EUsbキーから指定した文字位置から開始し、指定した文字数の文字列を取得する
		/// </summary>
		/// <param name="start">読取開始位置のインデックス</param>
		/// <param name="data">読取文字列</param>
		/// <returns></returns>
		unsafe public static bool GetData(short start, ref short data)
		{
			try
			{
				if (!CheckKey())
				{
					return false;
				}
				// EUsbキーからデータを読み出す
				byte[] buf = new byte[2]; //ここのサイズは読み出しのサイズ＋1です。
				fixed (byte* pKeyPath = KeyPath)
				{
					if (YRead(buf, start, 2, ReadPassword_H, ReadPassword_L, pKeyPath) != 0)
					{
						return false;
					}
				}
				data = (short)(buf[0] * 256 + buf[1]);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// バイト配列を表すstringを返す
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		private static string ByteConvertString(byte[] buffer)
		{
			char[] null_string = { '\0', '\0' };
			System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("Shift_JIS");
			return encoding.GetString(buffer).TrimEnd(null_string);
		}

		/// <summary>
		/// EUsbキーを存在するかどうかを判明する
		/// </summary>
		/// <returns></returns>
		unsafe public static bool CheckKey()
		{
			try
			{
				KeyPath = new byte[256];
				// ポートを見つける
				int start = 0;
				fixed (byte* pKeyPath = KeyPath)
				{
					//if (FindPort_2(0, 1, 134226688, pKeyPath) != 0)
					if (FindPort(start, pKeyPath) != 0)
					{
						return false;
					}
				}
				// バージョン情報を取得する
				short Version = 0;
				fixed (byte* pKeyPath = KeyPath)
				{
					if (NT_GetIDVersion(ref Version, pKeyPath) != 0)
					{
						return false;
					}
				}
				// F2Kキーバージョンアップ可能性があるので、該当チェックを廃止する
				//// バージョンをチェックする
				//if (VersionID != Version)
				//{
				//    return false;
				//}
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// EUsbキーのIDを取得する
		/// </summary>
		/// <returns></returns>
		unsafe public static string GetEUsbID()
		{
			KeyPath = new byte[256];
			// ポートを見つける
			int start = 0;
			fixed (byte* pKeyPath = KeyPath)
			{
				if (FindPort(start, pKeyPath) != 0)
				{
					return "";
				}
				else
				{
					int id1 = 0, id2 = 0;
					if (GetID(ref id1, ref id2, pKeyPath) != 0)
					{
						return "";
					}
					else
					{
						return Convert.ToString(id1, 16) + Convert.ToString(id2, 16);
					}
				}
			}
		}

		/// <summary>
		/// 複数eUSBキーを挿入されているかを判明する
		/// </summary>
		/// <returns></returns>
		unsafe public static bool IsMultiKeys()
		{
			bool bReturn = false;
			int nKeys = 0;
			try
			{
				// ポートを見つける
				for (int start = 0; start < 10; start++)
				{
					KeyPath = new byte[256];
					fixed (byte* pKeyPath = KeyPath)
					{
						if (FindPort(start, pKeyPath) == 0)
						{
							nKeys++;
							if (nKeys > 1)
							{
								bReturn = true;
								break;
							}
						}
					}
				}
			}
			catch
			{
			}
			return bReturn;
		}
	}
}
