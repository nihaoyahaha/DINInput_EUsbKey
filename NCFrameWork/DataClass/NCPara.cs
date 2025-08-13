//******************************************************************************
// ユーザ名					ニューコン
// システム名				フレームワーク
// サブシステム名			
// 作成者					蒋@NC
// 改版日					2009/05/25
// 改版内容					ORACLEとPOSTGRESQL共に対応
//******************************************************************************
namespace DI.NCFrameWork
{
	using System;
	using System.Data;
	using Npgsql;


	//************************************************************************
	/// <summary>
	/// NCParaクラス：DBから取得のデータを定義する
	/// </summary>
	//************************************************************************
	public class NCPara
	{
		/// <summary>
		/// キー
		/// </summary>
		private string _Key;
		/// <summary>
		/// データタイプ
		/// </summary>
		private NpgsqlTypes.NpgsqlDbType _Type;

		/// <summary>
		/// データのサイズ
		/// </summary>
		private int _Size;
		/// <summary>
		/// データの値
		/// </summary>
		private object _Value;
		/// <summary>
		/// データの戻るフラグ：true－戻る、false－戻らない
		/// </summary>
		private bool _OutPut;


		/// <summary>
		/// キー
		/// </summary>
		public string Key
		{
			get { return _Key; }
			set { _Key = value; }
		}

		/// <summary>
		/// データタイプ
		/// </summary>
		public NpgsqlTypes.NpgsqlDbType Type
		{
			get { return _Type; }
			set { _Type = value; }
		}

		/// <summary>
		/// データのサイズ
		/// </summary>
		public int Size
		{
			get { return _Size; }
			set { _Size = value; }
		}

		/// <summary>
		/// データの値
		/// </summary>
		public object Value
		{
			get { return _Value; }
			set { _Value = value; }
		}

		/// <summary>
		/// データの戻るフラグ：true－戻る、false－戻らない
		/// </summary>
		public bool OutPut
		{
			get { return _OutPut; }
			set { _OutPut = value; }
		}

		//************************************************************************
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="strKey">キー</param>
		/// <param name="dbType">データのタイプ</param>
		/// <param name="iSize">データのサイズ</param>
		/// <param name="objValue">データの値</param>
		//************************************************************************
		public NCPara(string strKey, NpgsqlTypes.NpgsqlDbType dbType, int iSize, object objValue)
		{
			Key = strKey;
			Type = dbType;
			Size = iSize;
			Value = objValue;
			OutPut = false;
		}

		//************************************************************************
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="strKey">キー</param>
		/// <param name="oraType">データのタイプ</param>
		/// <param name="iSize">データのサイズ</param>
		/// <param name="objValue">データの値</param>
		/// <param name="objDefault">ディフォルト値</param>
		//************************************************************************
		public NCPara(string strKey, NpgsqlTypes.NpgsqlDbType dbType, int iSize, object objValue, object objDefault)
		{
			Key = strKey;
			Type = dbType;
			Size = iSize;
			if (objValue == null || (objValue is string && objValue.ToString().Length == 0))
			{

				Value = objDefault;
			}
			else
			{
				Value = objValue;
			}

			OutPut = false;
		}

		//************************************************************************
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="strKey">キー</param>
		/// <param name="SqlType">データのタイプ</param>
		/// <param name="iSize">データのサイズ</param>
		/// <param name="objValue">データの値</param>
		/// <param name="bOutPut">データの戻るフラグ</param>
		//************************************************************************
		public NCPara(string strKey, NpgsqlTypes.NpgsqlDbType dbType, int iSize, object objValue, bool bOutPut)
		{
			Key = strKey;
			Type = dbType;
			Size = iSize;
			Value = objValue;
			OutPut = bOutPut;
		}

		//************************************************************************
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="strKey">キー</param>
		/// <param name="dbType">データのタイプ</param>
		/// <param name="iSize">データのサイズ</param>
		/// <param name="objValue">データの値</param>
		/// <param name="bOutPut">データの戻るフラグ</param>
		//************************************************************************
		public NCPara(string strKey, NpgsqlTypes.NpgsqlDbType dbType, int iSize, object objValue, object objDefault, bool bOutPut)
		{
			Key = strKey;
			Type = dbType;
			Size = iSize;
			if (objValue == null || (objValue is string && objValue.ToString().Length == 0))
			{
				Value = objDefault;
			}
			else
			{
				Value = objValue;
			}
			OutPut = bOutPut;
		}

	}
}
