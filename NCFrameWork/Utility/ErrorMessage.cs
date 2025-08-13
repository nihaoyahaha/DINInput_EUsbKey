//*****************************************************************************
// ユーザ名					ニューコン
// システム名				
// サブシステム名			エラーメッセージクラス
// 作成者					蒋@NC
// 改版日					2007/07/19
// 改版内容					新規作成
//*****************************************************************************
using System;

namespace DI.NCFrameWork
{
	#region エラーメッセージ
	/// <summary>
	/// エラーメッセージ
	/// エラーメッセージのデータ・オブジェクト

	/// </summary>
	public class ErrorMessage
	{
		private string[] _Parms=null;
		private string _ErrorCode;
		private string _ErrorMsg;
		private string _ErrorItemName;
		private string _ErrorItemName2;
		private string _ErrorItemName3;

		/// <summary>
		/// エラーコード
		/// </summary>
		public string ErrorCode
		{
			get{return _ErrorCode;}
			set{_ErrorCode = value;}
		}

		/// <summary>
		/// エラーParms名
		/// </summary>
		public string[] Parms
		{
			get{return _Parms;}
			set{_Parms = value;}
		}
		
		/// <summary>
		/// エラー項目名S
		/// </summary>
		public string ErrorItemName
		{
			get{return _ErrorItemName;}
			set{_ErrorItemName = value;}
		}
		/// <summary>
		/// エラー項目名2
		/// </summary>
		public string ErrorItemName2
		{
			get{return _ErrorItemName2;}
			set{_ErrorItemName2 = value;}
		}
		/// <summary>
		/// エラー項目名3
		/// </summary>
		public string ErrorItemName3
		{
			get{return _ErrorItemName3;}
			set{_ErrorItemName3 = value;}
		}
		/// <summary>
		/// エラーメッセージ
		/// それが準備ができているとき、それを一般的なコンポーネントに取り替えるべきです。
		/// </summary>
		public string ErrorMsg
		{
			get
			{
				MessageManager messageManager = MessageManager.NewInstance();
				_ErrorMsg = messageManager.GetMessage(_ErrorCode);

				if(_ErrorMsg != null && !_ErrorMsg.Equals(""))
				{
					if(this._Parms != null)
					{
						try
						{
							_ErrorMsg = string.Format(_ErrorMsg, this._Parms);
						}
						catch
						{
						}
					}
                    return _ErrorMsg.Replace("\\r\\n", "\r\n");// + ":" + "ダミーメッセージ内容"
				}
				else
					return _ErrorCode+":" +"ダミーメッセージ内容";
			}

		}
		
		public ErrorMessage()
		{
		}

        /// <summary>
        /// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
        /// </summary>
        /// <param name="ErrorCode">エラーコード</param>
        public ErrorMessage(string ErrorCode)
        {
            this.ErrorCode = ErrorCode;
        }

        /// <summary>
        /// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
        /// </summary>
        /// <param name="ErrorCode">エラーコード</param>
        /// <param name="parms">書式対象を含んだ文字列の配列</param>
        public ErrorMessage(string ErrorCode, string[] parms)
        {
            this.ErrorCode = ErrorCode;
            this.Parms = parms;
        }

		/// <summary>
		/// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
		/// </summary>
		/// <param name="ErrorCode">エラーコード</param>
		/// <param name="ErrorItemName">エラー項目名</param>
		public ErrorMessage(string ErrorCode, string ErrorItemName)
		{
			this.ErrorCode = ErrorCode;
			this.ErrorItemName = ErrorItemName;
		}

		/// <summary>
		/// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
		/// </summary>
		/// <param name="ErrorCode">エラーコード</param>
		/// <param name="ErrorItemName">エラー項目名</param>
        /// <param name="parms">書式対象を含んだ文字列の配列</param>
		public ErrorMessage(string ErrorCode, string ErrorItemName, string[] parms)
		{
			this.ErrorCode = ErrorCode;
			this.ErrorItemName = ErrorItemName;
			this._Parms = parms;
		}

		/// <summary>
		/// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
		/// </summary>
		/// <param name="ErrorCode">エラーコード</param>
		/// <param name="ErrorItemName">エラー項目名</param>
		/// <param name="ErrorItemName2">エラー項目名2</param>
		public ErrorMessage(string ErrorCode, string ErrorItemName, string ErrorItemName2)
		{
			this.ErrorCode = ErrorCode;
			this.ErrorItemName = ErrorItemName;
			this.ErrorItemName2 = ErrorItemName2;
		}

        /// <summary>
        /// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
        /// </summary>
        /// <param name="ErrorCode">エラーコード</param>
        /// <param name="ErrorItemName">エラー項目名</param>
        /// <param name="ErrorItemName2">エラー項目名2</param>
        /// <param name="parms">書式対象を含んだ文字列の配列</param>
        public ErrorMessage(string ErrorCode, string ErrorItemName, string ErrorItemName2, string[] parms)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorItemName = ErrorItemName;
            this.ErrorItemName2 = ErrorItemName2;
            this._Parms = parms;
        }

		/// <summary>
		/// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
		/// </summary>
		/// <param name="ErrorCode">エラーコード</param>
		/// <param name="ErrorItemName">エラー項目名</param>
		/// <param name="ErrorItemName2">エラー項目名2</param>
		/// <param name="ErrorItemName3">エラー項目名3</param>
		public ErrorMessage(string ErrorCode, string ErrorItemName, string ErrorItemName2, string ErrorItemName3)
		{
			this.ErrorCode = ErrorCode;
			this.ErrorItemName = ErrorItemName;
			this.ErrorItemName2 = ErrorItemName2;
			this.ErrorItemName3 = ErrorItemName3;
		}

        /// <summary>
        /// エラーコードと誤り項目名のエラーメッセージのクラスを初期化してください。
        /// </summary>
        /// <param name="ErrorCode">エラーコード</param>
        /// <param name="ErrorItemName">エラー項目名</param>
        /// <param name="ErrorItemName2">エラー項目名2</param>
        /// <param name="ErrorItemName3">エラー項目名3</param>
        /// <param name="parms">書式対象を含んだ文字列の配列</param>
        public ErrorMessage(string ErrorCode, string ErrorItemName, string ErrorItemName2, string ErrorItemName3, string[] parms)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorItemName = ErrorItemName;
            this.ErrorItemName2 = ErrorItemName2;
            this.ErrorItemName3 = ErrorItemName3;
            this.Parms = parms;
        }
	}
	#endregion
}
