using DI.NCFrameWork;
using System.Windows.Forms;
using DINServerObject;
using Microsoft.Extensions.Configuration;

namespace DINInput_EUsbKey
{
	public partial class BaseForm : Form
	{
		protected readonly string MSG_CAPTION = "eUSBキー管理システム";
		private ErrorMessage _errMsg = null;
		private static NCPublicFunction _pFunc = new NCPublicFunction();
		private static NCLogger _log = NCLogger.GetInstance();

		/// <summary>
		/// エラーメッセージ
		/// </summary>
		protected ErrorMessage ErrMsg
		{
			get { return _errMsg; }
			set { _errMsg = value; }
		}

		/// <summary>
		/// サービスAPIを取得または設定する。
		/// </summary>
		public static IWinServiceAPI ServiceApi
		{
			get { return Program.serviceApi; }
		}

		/// <summary>
		/// 共通関数のインスタンス
		/// </summary>
		public static NCPublicFunction PFunc
		{
			get { return _pFunc; }
		}

		/// <summary>
		/// ログ出力
		/// </summary>
		protected static NCLogger Log
		{
			get { return _log; }
		}

		public BaseForm()
		{
			InitializeComponent();
		}
	}
}
