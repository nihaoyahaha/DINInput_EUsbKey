using DI.NCFrameWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using DINServerObject;
using System.Text.Json;

namespace DINInput_EUsbKey
{
	public partial class BaseForm : Form
	{
		protected readonly string MSG_CAPTION = "eUSBキー管理システム";
		private ErrorMessage _errMsg = null;
		private static Configuration _config = null;
		private static NCPublicFunction _pFunc = new NCPublicFunction();
		private static WinServiceAPI _serviceApi = null;
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
		/// アプリ構成ファイル
		/// </summary>
		public static Configuration Config
		{
			get { return _config; }
		}

		/// <summary>
		/// サービスAPIを取得または設定する。
		/// </summary>
		public static WinServiceAPI ServiceApi
		{
			get { return _serviceApi; }
			set { _serviceApi = value; }
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
			if (_config == null)
			{
				Directory.SetCurrentDirectory(Directory.GetParent(Application.StartupPath).FullName);
				_config = _pFunc.GetCurrentConfigFile();
			}
			_serviceApi = new WinServiceAPI();
			WinServiceAPI.SetConnectionString(Program.strDataConnectionString);
		}
	}
}
