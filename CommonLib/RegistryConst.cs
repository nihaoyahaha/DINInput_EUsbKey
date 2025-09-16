using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	[Serializable]
	public class RegistryConst
	{
		/// <summary>
		/// レジストリルートキー
		/// </summary>
		public const string Reg_Root = @"Software\DINEUsbKey\EUsbKeyService\";
		/// <summary>
		/// データベース接続文字列
		/// </summary>
		public const string Reg_DataConnectionString = "DataConnectionString";
		/// <summary>
		/// サービスのバージョン
		/// </summary>
		public const string Reg_ServiceVersion = "ServiceVersion";
		/// <summary>
		/// リモーティングオブジェクトのバージョン
		/// </summary>
		public const string Reg_RemotingObjectVersion = "RemotingObjectVersion";
	}
}
