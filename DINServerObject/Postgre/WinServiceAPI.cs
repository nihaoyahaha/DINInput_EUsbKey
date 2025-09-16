using System;
using System.Collections.Generic;
using System.Text;
using DI.NCFrameWork;
using System.Data;
using NpgsqlTypes;
using Npgsql;
using DI.NCFrameWork.Utility;
using System.ServiceModel;
using System.Reflection;

namespace DINServerObject
{
	[ServiceContract(Namespace = "http://DINServerObject")]
	public interface IWinServiceAPI
	{
		void SetLog(string logSource);

		void SetConnectionString(string strConnectionString);

		string GetAssemblyVersion();

		[OperationContract]
		bool CheckeUsbExists(string usbId);

		[OperationContract]
		bool KeyInitialization(string usbId);

		[OperationContract]
		UsbId GetEUsb(string usbId);

		[OperationContract]
		List<UsbId> GetEUsbList(UsbIdQueryDto dto);

		[OperationContract]
		bool AddEUsb(UsbId usb);

		[OperationContract]
		bool UpdateEUsb(UsbId usb);
	}

	public class WinServiceAPI : IWinServiceAPI
	{
		public static NCDataBase m_dataBase = null;
		private NCDataConnection m_connection = null;
		private static NCLogger Log = null;
		private static NCPublicFunction m_pFunc = new NCPublicFunction();
		public NCDataConnection Connect
		{
			set { m_connection = value; }
			get { return m_connection; }
		}

		/// <summary>
		/// インスタンス作成
		/// </summary>
		public WinServiceAPI()
		{
			try
			{
				if (Log == null)
				{
					NCLogger.SetLogType(LogTypeManager.AllUser);
					Log = NCLogger.GetInstance();
				}
				if (m_dataBase == null)
				{
					m_dataBase = new NCDataBase();
				}
			}
			catch (Exception e)
			{
				Log.WriteExceptionLog(e);
			}
		}

		/// <summary>
		/// イベントログの設定
		/// </summary>
		/// <param name="logSource">イベントログのインスタンス</param>
		public void SetLog(string logSource)
		{
			CmUtilities.SetLog(logSource);
		}

		/// <summary>
		/// データベース接続文字列を設定する
		/// </summary>
		/// <param name="strConnectionString">接続文字列</param>
		public void SetConnectionString(string strConnectionString)
		{
			NCDataConnection.ConnectionString = strConnectionString;
		}

		/// <summary>
		/// アセンブリバージョンを取得する
		/// </summary>
		/// <returns></returns>
		public string GetAssemblyVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		/// <summary>
		/// クエリー文の実行
		/// </summary>
		/// <param name="sql">sql文</param>
		/// <param name="tableName">テーブル名</param>
		/// <param name="parms">クエリーのパラメータ</param>
		/// <returns>結果セット</returns>
		private DataSet ExecuteQuery(string sql, string tableName, NCArrayList parms)
		{
			m_connection = new NCDataConnection();
			m_connection.OpenConnection();
			DataSet dataSet = null;
			try
			{
				m_dataBase.ExecSql(sql, parms, ref dataSet, tableName, m_connection.Connection);
			}
			catch (Exception exp)
			{
				Log.WriteExceptionLog(exp);
				throw exp;
			}
			finally
			{
				m_connection.CloseConnection();
			}
			return dataSet;
		}

		/// <summary>
		/// 結果セットを返さずにSQLを実行する
		/// </summary>
		/// <param name="sql">sql文</param>
		/// <param name="parms">パラメータ</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse</returns>
		private bool ExecuteNonQuery(string sql, NCArrayList parms)
		{
			m_connection = new NCDataConnection();
			m_connection.OpenConnection();
			bool bReturn = false;
			try
			{
				bReturn = m_dataBase.ExecSql(sql, parms, m_connection.Connection, m_connection.Transaction);
			}
			catch (Exception exp)
			{
				Log.WriteExceptionLog(exp);
			}
			finally
			{
				if (bReturn)
				{
					m_connection.Commit();
				}
				else
				{
					m_connection.Rollback();
				}
				m_connection.CloseConnection();
			}
			return bReturn;
		}

		/// <summary>
		/// データにeUSBが存在するかどうかをチェック
		/// </summary>
		/// <param name="usbId">キーＩＤ</param>
		/// <returns></returns>
		public bool CheckeUsbExists(string usbId)
		{
			NCArrayList list = new NCArrayList();
			NCPara para = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT COUNT(*) FROM USBID ");
			sql.Append(" WHERE EUSB_ID = :EUSB_ID ");
			para = new NCPara(":EUSB_ID", NpgsqlDbType.Char, 16, usbId);
			list.Add(para);
			DataSet ds = ExecuteQuery(sql.ToString(), "USBID", list);
			int count = m_pFunc.ObjectToInt(ds.Tables[0].Rows[0]["count"]);
			return count > 0 ? true : false;
		}

		/// <summary>
		/// eUSB初期化
		/// </summary>
		/// <param name="usbId">キーＩＤ</param>
		public bool KeyInitialization(string usbId)
		{
			NCArrayList list = new NCArrayList();
			NCPara para = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" UPDATE USBID ");
			sql.Append(" SET USESTARTDAY = NULL,");//加工帳入力利用開始日
			sql.Append(" USEENDDAY = NULL,");//加工帳入力利用終了日
			sql.Append(" USEEFUSTARTDAY = NULL, ");//加工帳絵符利用開始日
			sql.Append(" USEEFUENDDAY = NULL, ");//加工帳絵符利用終了日
			sql.Append(" CADOPUSEENDDAY = NULL,");//CAD・加工帳オプション機能使用終了日
			sql.Append(" FUNCTIONS = '00000000', ");//加工帳入力システム機能
			sql.Append(" CADOPFUNCTIONS = '0000000000', ");//CAD・加工帳オプション機能
			sql.Append(" KEYUPDATEDATE = CURRENT_TIMESTAMP, ");//Key更新日
			sql.Append(" DINCAD ='' , ");//CADグレードコントロール
			sql.Append(" CADVERSION =  0, ");//CADバージョンコントロール
			sql.Append(" CADTARGET = -1, ");//DINCAD2ターゲットCAD
			sql.Append(" TMP = 0, ");//TPMシステム
			sql.Append(" DINSUBCON =0, ");//アイコーサブコンフラグ
			sql.Append(" DINUSEFLAG = 0 ");//DIN加工帳入力フラグ
			sql.Append("WHERE EUSB_ID = :EUSB_ID");
			para = new NCPara(":EUSB_ID", NpgsqlDbType.Char, 16, usbId);
			list.Add(para);
			return ExecuteNonQuery(sql.ToString(), list);
		}

		/// <summary>
		/// eUSBを取得
		/// </summary>
		/// <param name="usbId">キーＩＤ</param>
		/// <returns></returns>
		public UsbId GetEUsb(string usbId)
		{
			NCArrayList list = new NCArrayList();
			NCPara para = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT EUSB_ID,USERNAME,MAIL,COMPANY,MOBILETEL,ACTFLAG,KEYPUBLISHERDATE,  ");
			sql.Append(" KEYUPDATEDATE,USESTARTDAY,USEENDDAY,FUNCTIONS,USEEFUSTARTDAY,USEEFUENDDAY, ");
			sql.Append(" DINCAD,CADVERSION,CADTARGET,CADOPUSEENDDAY,CADOPFUNCTIONS,TMP,DINSUBCON, ");
			sql.Append(" NOTES,UPDATEFLG,DINUSEFLAG ");
			sql.Append(" FROM USBID ");
			sql.Append(" WHERE EUSB_ID = :EUSB_ID ");
			para = new NCPara(":EUSB_ID", NpgsqlDbType.Char, 16, usbId);
			list.Add(para);
			DataSet ds = ExecuteQuery(sql.ToString(), "USBID", list);
			if (ds == null || ds.Tables.Count ==0 || ds.Tables[0].Rows.Count ==0 ) return null;
			List<UsbId> usbIds= ds.Tables[0].ToList<UsbId>();
			return usbIds[0];
		}

		/// <summary>
		/// eUSBデータセットの取得
		/// </summary>
		/// <param name="usbId">キーＩＤ</param>
		/// <returns></returns>
		public List<UsbId> GetEUsbList(UsbIdQueryDto dto)
		{
			NCArrayList list = new NCArrayList();
			NCPara para = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT EUSB_ID,USERNAME,MAIL,COMPANY,MOBILETEL,ACTFLAG,KEYPUBLISHERDATE,  ");
			sql.Append(" KEYUPDATEDATE,USESTARTDAY,USEENDDAY,FUNCTIONS,USEEFUSTARTDAY,USEEFUENDDAY, ");
			sql.Append(" DINCAD,CADVERSION,CADTARGET,CADOPUSEENDDAY,CADOPFUNCTIONS,TMP,DINSUBCON, ");
			sql.Append(" NOTES,UPDATEFLG,DINUSEFLAG ");
			sql.Append(" FROM USBID WhERE 1=1 ");
			//キーＩＤ
			if (!string.IsNullOrEmpty(dto.KeyId))
			{
				sql.Append(" AND EUSB_ID LIKE :EUSB_ID ");
				para = new NCPara(":EUSB_ID", NpgsqlDbType.Char, 18, $"%{dto.KeyId}%");
				list.Add(para);
			}
			//会社名
			if (!string.IsNullOrEmpty(dto.Company))
			{
				sql.Append(" AND COMPANY LIKE :COMPANY ");
				para = new NCPara(":COMPANY", NpgsqlDbType.Varchar, 52,$"%{dto.Company}%");
				list.Add(para);
			}
			//利用者名
			if (!string.IsNullOrEmpty(dto.UserName))
			{
				sql.Append(" AND USERNAME LIKE :USERNAME ");
				para = new NCPara(":USERNAME", NpgsqlDbType.Varchar, 52, $"%{dto.UserName}%");
				list.Add(para);
			}

			//Key更新日
			if (dto.KeyUpdateDateFrom != null && dto.KeyUpdateDateTo != null)
			{
				sql.Append(" AND KEYUPDATEDATE::DATE BETWEEN :KEYUPDATEDATEFROM AND :KEYUPDATEDATETO ");
				para = new NCPara(":KEYUPDATEDATEFROM", NpgsqlDbType.Timestamp, 0, dto.KeyUpdateDateFrom.Value.Date);
				list.Add(para);
				para = new NCPara(":KEYUPDATEDATETO", NpgsqlDbType.Timestamp, 0, dto.KeyUpdateDateTo.Value.Date);
				list.Add(para);
			}
			else if (dto.KeyUpdateDateFrom != null && dto.KeyUpdateDateTo == null)
			{
				sql.Append(" AND KEYUPDATEDATE::DATE >= :KEYUPDATEDATE ");
				para = new NCPara(":KEYUPDATEDATE", NpgsqlDbType.Timestamp, 0, dto.KeyUpdateDateFrom.Value.Date);
				list.Add(para);
			}
			else if (dto.KeyUpdateDateFrom == null && dto.KeyUpdateDateTo != null)
			{
				sql.Append(" AND KEYUPDATEDATE::DATE <= :KEYUPDATEDATE ");
				para = new NCPara(":KEYUPDATEDATE", NpgsqlDbType.Timestamp, 0, dto.KeyUpdateDateTo.Value.Date);
				list.Add(para);
			}

			//加工帳利用終了日
			if (dto.UseEndDayFrom != null && dto.UseEndDayTo != null)
			{
				sql.Append(" AND USEENDDAY::DATE BETWEEN :USEENDDAYFROM AND :USEENDDAYTO ");
				para = new NCPara(":USEENDDAYFROM", NpgsqlDbType.Timestamp, 0, dto.UseEndDayFrom.Value.Date);
				list.Add(para);
				para = new NCPara(":USEENDDAYTO", NpgsqlDbType.Timestamp, 0, dto.UseEndDayTo.Value.Date);
				list.Add(para);
			}
			else if (dto.UseEndDayFrom != null && dto.UseEndDayTo == null)
			{
				sql.Append(" AND USEENDDAY::DATE >= :USEENDDAY ");
				para = new NCPara(":USEENDDAY", NpgsqlDbType.Timestamp, 0, dto.UseEndDayFrom.Value.Date);
				list.Add(para);
			}
			else if (dto.UseEndDayFrom == null && dto.UseEndDayTo != null)
			{
				sql.Append(" AND USEENDDAY::DATE <= :USEENDDAY ");
				para = new NCPara(":USEENDDAY", NpgsqlDbType.Timestamp, 0, dto.UseEndDayTo.Value.Date);
				list.Add(para);
			}

			//絵符オプション利用終了日
			if (dto.UseEFUEndDayFrom != null && dto.UseEFUEndDayTo != null)
			{
				sql.Append(" AND USEEFUENDDAY::DATE BETWEEN :USEEFUENDDAYFROM AND :USEEFUENDDAYTO ");
				para = new NCPara(":USEEFUENDDAYFROM", NpgsqlDbType.Timestamp, 0, dto.UseEFUEndDayFrom.Value.Date);
				list.Add(para);
				para = new NCPara(":USEEFUENDDAYTO", NpgsqlDbType.Timestamp, 0, dto.UseEFUEndDayTo.Value.Date);
				list.Add(para);
			}
			else if (dto.UseEFUEndDayFrom != null && dto.UseEFUEndDayTo == null)
			{
				sql.Append(" AND USEEFUENDDAY::DATE >= :USEEFUENDDAY ");
				para = new NCPara(":USEEFUENDDAY", NpgsqlDbType.Timestamp, 0, dto.UseEFUEndDayFrom.Value.Date);
				list.Add(para);
			}
			else if (dto.UseEFUEndDayFrom == null && dto.UseEFUEndDayTo != null)
			{
				sql.Append(" AND USEEFUENDDAY::DATE <= :USEEFUENDDAY ");
				para = new NCPara(":USEEFUENDDAY", NpgsqlDbType.Timestamp, 0, dto.UseEFUEndDayTo.Value.Date);
				list.Add(para);
			}

			sql.Append(" ORDER BY EUSB_ID ASC ");

			DataSet ds = ExecuteQuery(sql.ToString(), "USBID", list);
			if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) return null;
			List<UsbId> usbIds = ds.Tables[0].ToList<UsbId>();
			return usbIds;
		}

		/// <summary>
		/// eUSBを追加
		/// </summary>
		/// <param name="usb"></param>
		/// <returns></returns>
		public bool AddEUsb(UsbId usb)
		{
			NCArrayList list = new NCArrayList();
			NCPara para = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" INSERT INTO USBID (EUSB_ID,USERNAME,MAIL,COMPANY,MOBILETEL,ACTFLAG,KEYPUBLISHERDATE");
			sql.Append(" ,USESTARTDAY,USEENDDAY,FUNCTIONS,USEEFUSTARTDAY,USEEFUENDDAY,DINCAD,CADVERSION ");
			sql.Append(" ,CADTARGET,CADOPUSEENDDAY,CADOPFUNCTIONS,TMP,DINSUBCON,NOTES,DINUSEFLAG ) ");
			sql.Append(" VALUES ( :EUSB_ID,:USERNAME,:MAIL,:COMPANY,:MOBILETEL,:ACTFLAG,CURRENT_TIMESTAMP");
			sql.Append(" ,:USESTARTDAY,:USEENDDAY,:FUNCTIONS,:USEEFUSTARTDAY,:USEEFUENDDAY,:DINCAD,:CADVERSION ");
			sql.Append(" ,:CADTARGET,:CADOPUSEENDDAY,:CADOPFUNCTIONS,:TMP,:DINSUBCON,:NOTES,:DINUSEFLAG) ");

			para = new NCPara(":EUSB_ID", NpgsqlDbType.Varchar, 16, usb.EUsb_Id);
			list.Add(para);
			para = new NCPara(":USERNAME",NpgsqlDbType.Varchar, 50,usb.UserName);
			list.Add(para);
			para = new NCPara(":MAIL",NpgsqlDbType.Varchar, 50,usb.Mail);
			list.Add(para);
			para = new NCPara(":COMPANY",NpgsqlDbType.Varchar, 50,usb.Company);
			list.Add(para);
			para = new NCPara(":MOBILETEL",NpgsqlDbType.Varchar, 50,usb.MobileTel);
			list.Add(para);
			para = new NCPara(":ACTFLAG",NpgsqlDbType.Numeric,1,usb.ActFlag);
			list.Add(para);
			para = new NCPara(":USESTARTDAY",NpgsqlDbType.Timestamp,0,usb.UseStartDay);
			list.Add(para);
			para = new NCPara(":USEENDDAY",NpgsqlDbType.Timestamp,0,usb.UseEndDay);
			list.Add(para);
			para = new NCPara(":FUNCTIONS",NpgsqlDbType.Varchar, 8,usb.Functions);
			list.Add(para);
			para = new NCPara(":USEEFUSTARTDAY",NpgsqlDbType.Timestamp,0,usb.UseEFUStartDay);
			list.Add(para);
			para = new NCPara(":USEEFUENDDAY",NpgsqlDbType.Timestamp,0,usb.UseEFUEndDay);
			list.Add(para);
			para = new NCPara(":DINCAD",NpgsqlDbType.Varchar, 1,usb.DINCAD);
			list.Add(para);
			para = new NCPara(":CADVERSION",NpgsqlDbType.Numeric,2,usb.CADVersion);
			list.Add(para);
			para = new NCPara(":CADTARGET",NpgsqlDbType.Numeric,1,usb.CADTarget);
			list.Add(para);
			para = new NCPara(":CADOPUSEENDDAY",NpgsqlDbType.Timestamp,0,usb.CADOPUseEndDay);
			list.Add(para);
			para = new NCPara(":CADOPFUNCTIONS",NpgsqlDbType.Varchar, 10,usb.CADOPFunctions);
			list.Add(para);
			para = new NCPara(":TMP",NpgsqlDbType.Numeric,1,usb.TMP);
			list.Add(para);
			para = new NCPara(":DINSUBCON",NpgsqlDbType.Numeric,1,usb.DINsubcon);
			list.Add(para);
			para = new NCPara(":NOTES", NpgsqlDbType.Varchar, 1200, usb.Notes);
			list.Add(para);
			para = new NCPara(":DINUSEFLAG", NpgsqlDbType.Numeric, 1, usb.DINUseFlag);
			list.Add(para);
			return ExecuteNonQuery(sql.ToString(),list);
		}

		/// <summary>
		/// eUSBを修正
		/// </summary>
		/// <returns></returns>
		public bool UpdateEUsb(UsbId usb)
		{
			NCArrayList list = new NCArrayList();
			NCPara para = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" UPDATE USBID SET ");
			sql.Append(" USERNAME = :USERNAME, ");
			sql.Append(" MAIL = :MAIL, ");
			sql.Append(" COMPANY = :COMPANY, ");
			sql.Append(" MOBILETEL = :MOBILETEL, ");
			sql.Append(" ACTFLAG = :ACTFLAG, ");
			sql.Append(" KEYUPDATEDATE = CURRENT_TIMESTAMP,");
			sql.Append(" USESTARTDAY = :USESTARTDAY, ");
			sql.Append(" USEENDDAY = :USEENDDAY, ");
			sql.Append(" FUNCTIONS = :FUNCTIONS, ");
			sql.Append(" USEEFUSTARTDAY = :USEEFUSTARTDAY, ");
			sql.Append(" USEEFUENDDAY = :USEEFUENDDAY, ");
			sql.Append(" DINCAD = :DINCAD, ");
			sql.Append(" CADVERSION = :CADVERSION, ");
			sql.Append(" CADTARGET = :CADTARGET, ");
			sql.Append(" CADOPUSEENDDAY = :CADOPUSEENDDAY, ");
			sql.Append(" CADOPFUNCTIONS = :CADOPFUNCTIONS, ");
			sql.Append(" TMP = :TMP, ");
			sql.Append(" DINSUBCON = :DINSUBCON, ");
			sql.Append(" NOTES = :NOTES, ");
			sql.Append(" DINUSEFLAG = :DINUSEFLAG ");
			sql.Append("WHERE EUSB_ID = :EUSB_ID");

			para = new NCPara(":EUSB_ID", NpgsqlDbType.Varchar, 16, usb.EUsb_Id);
			list.Add(para);
			para = new NCPara(":USERNAME", NpgsqlDbType.Varchar, 50, usb.UserName);
			list.Add(para);
			para = new NCPara(":MAIL", NpgsqlDbType.Varchar, 50, usb.Mail);
			list.Add(para);
			para = new NCPara(":COMPANY", NpgsqlDbType.Varchar, 50, usb.Company);
			list.Add(para);
			para = new NCPara(":MOBILETEL", NpgsqlDbType.Varchar, 50, usb.MobileTel);
			list.Add(para);
			para = new NCPara(":ACTFLAG", NpgsqlDbType.Numeric, 1, usb.ActFlag);
			list.Add(para);
			para = new NCPara(":USESTARTDAY", NpgsqlDbType.Timestamp, 0, usb.UseStartDay);
			list.Add(para);
			para = new NCPara(":USEENDDAY", NpgsqlDbType.Timestamp, 0, usb.UseEndDay);
			list.Add(para);
			para = new NCPara(":FUNCTIONS", NpgsqlDbType.Varchar, 8, usb.Functions);
			list.Add(para);
			para = new NCPara(":USEEFUSTARTDAY", NpgsqlDbType.Timestamp, 0, usb.UseEFUStartDay);
			list.Add(para);
			para = new NCPara(":USEEFUENDDAY", NpgsqlDbType.Timestamp, 0, usb.UseEFUEndDay);
			list.Add(para);
			para = new NCPara(":DINCAD", NpgsqlDbType.Varchar, 1, usb.DINCAD);
			list.Add(para);
			para = new NCPara(":CADVERSION", NpgsqlDbType.Numeric, 2, usb.CADVersion);
			list.Add(para);
			para = new NCPara(":CADTARGET", NpgsqlDbType.Numeric, 1, usb.CADTarget);
			list.Add(para);
			para = new NCPara(":CADOPUSEENDDAY", NpgsqlDbType.Timestamp, 0, usb.CADOPUseEndDay);
			list.Add(para);
			para = new NCPara(":CADOPFUNCTIONS", NpgsqlDbType.Varchar, 10, usb.CADOPFunctions);
			list.Add(para);
			para = new NCPara(":TMP", NpgsqlDbType.Numeric, 1, usb.TMP);
			list.Add(para);
			para = new NCPara(":DINSUBCON", NpgsqlDbType.Numeric, 1, usb.DINsubcon);
			list.Add(para);
			para = new NCPara(":NOTES", NpgsqlDbType.Varchar, 1200, usb.Notes);
			list.Add(para);
			para = new NCPara(":DINUSEFLAG", NpgsqlDbType.Numeric, 1, usb.DINUseFlag);
			list.Add(para);

			return ExecuteNonQuery(sql.ToString(), list);
		}


	}
}
