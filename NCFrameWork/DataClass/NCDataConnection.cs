using System;
using System.Collections.Generic;
using System.Text;
using DI.NCFrameWork;

namespace DI.NCFrameWork
{
	using System;
	using System.Data;
	using Npgsql;
	using System.Collections;

	//************************************************************************
	/// <summary>
	/// NCDataConnection:データベースコネクション処理クラス
	/// </summary>
	//************************************************************************
	public class NCDataConnection
	{
		#region フィールド
		// データベース接続文字列
		private static string s_connectionString = "";
		// リモーティングを利用するフラグ
		private static bool s_bRemoting = true;

		/// <summary>
		/// データベースのコネクション
		/// </summary>
		private NpgsqlConnection s_conn = null;

		/// <summary>
		/// データベースのトランザクション
		/// </summary>

		private NpgsqlTransaction s_transaction = null;

		/// <summary>
		/// データベースのコネクション
		/// </summary>

		public NpgsqlConnection Connection
		{
			get { return s_conn; }
		}

		/// <summary>
		/// データベースのトランザクション
		/// </summary>
		public NpgsqlTransaction Transaction
		{
			get { return s_transaction; }
		}


		/// <summary>
		/// データベース接続文字列
		/// </summary>
		public static string ConnectionString
		{
			set { s_connectionString = value; }
		}

		/// <summary>
		/// リモーティングを利用するフラグ
		/// </summary>
		public static bool Remoting
		{
			set { s_bRemoting = value; }
		}

		/// <summary>
		/// データベースコネクションの構造体
		/// </summary>
		public NCDataConnection()
		{
			s_conn = null;
		}

		#endregion

		#region データベースのコネクションを閉じる
		//************************************************************************
		/// <summary>
		/// データベースのコネクションを閉じる
		/// </summary>
		//************************************************************************
		public void CloseConnection()
		{
			try
			{
				if (s_conn != null)
				{
					//DBクローズ
					s_conn.Close();
					NCLogger.GetInstance().WriteDebugLog("Postgresql Connection is closed.");
				}
			}
			finally
			{
				//初期値を設定
				s_conn = null;
			}
		}
		#endregion

		#region 指定のデータベースのコネクションを開く
		//************************************************************************
		/// <summary>
		/// 指定のデータベースのコネクションを開く
		/// </summary>
		/// <returns>データベースのコネクションを返す</returns>
		//************************************************************************
		public NpgsqlConnection OpenConnection()
		{
			try
			{
				// データベース接続パラメータを取得				
				string m_strConnectionString = GetConnectionString();
				s_conn = new NpgsqlConnection(m_strConnectionString);
				// DBオープン
				s_conn.Open();
				NCLogger.GetInstance().WriteDebugLog("Postgresql Connection is opened.");
			}
			catch (NpgsqlException oex)
			{
				s_conn = null;
				NCLogger.GetInstance().WriteExceptionLog(oex);
				throw oex;
			}
			catch (Exception err)
			{
				s_conn = null;
				NCLogger.GetInstance().WriteExceptionLog(err);
				throw err;
			}
			return s_conn;
		}

		//************************************************************************
		/// <summary>
		/// データベースでトランザクションを開始する
		/// </summary>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool Begin()
		{
			bool bReturn = false;
			try
			{
				if (s_conn != null)
				{
					if (s_transaction == null)
					{
						s_transaction = s_conn.BeginTransaction();

						NCLogger.GetInstance().WriteDebugLog("Transaction is started.");
					}
					bReturn = true;
				}
			}
			catch (Exception exp)
			{
				NCLogger.GetInstance().WriteExceptionLog(exp);
			}
			return bReturn;
		}

		//************************************************************************
		/// <summary>
		/// トランザクションを保留中の状態からコミットする
		/// </summary>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool Commit()
		{
			bool bReturn = false;
			try
			{
				if (s_transaction != null)
				{
					s_transaction.Commit();

					NCLogger.GetInstance().WriteDebugLog("Transaction is commited.");
					bReturn = true;
				}
			}
			catch (Exception exp)
			{
				NCLogger.GetInstance().WriteExceptionLog(exp);
			}
			finally
			{
				s_transaction = null;
			}
			return bReturn;
		}

		//************************************************************************
		/// <summary>
		/// トランザクションを保留中の状態からロールバックする
		/// </summary>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool Rollback()
		{
			bool bReturn = false;
			try
			{
				if (s_transaction != null)
				{
					s_transaction.Rollback();
					NCLogger.GetInstance().WriteDebugLog("Transaction is rollback.");
					bReturn = true;
				}
			}
			catch (Exception exp)
			{
				NCLogger.GetInstance().WriteExceptionLog(exp);
			}
			finally
			{
				s_transaction = null;
			}

			return bReturn;
		}

		//************************************************************************
		/// <summary>
		/// トランザクションが起動するかどうかを判断します。
		/// </summary>
		/// <returns>起動された場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool IsBegin()
		{
			if (s_transaction != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region private メソッド
		//************************************************************************
		/// <summary>
		/// データベースの接続に必要な情報を取得します
		/// </summary>
		/// <returns>データベースのConnectStringを返す</returns>
		//************************************************************************
		private static string GetConnectionString()
		{
			return s_connectionString;
		}
		#endregion
	}
}

