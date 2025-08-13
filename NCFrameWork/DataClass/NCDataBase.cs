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
	using System.Text;
	using Npgsql;
	using System.Collections;

	//************************************************************************
	/// <summary>
	/// NCDataBaseクラス：データベースをアクセスする
	/// </summary>
	//************************************************************************
	[Serializable]
	public class NCDataBase
	{

		#region private メソッド
		//************************************************************************
		/// <summary>
		/// DBアクセスエラーが発生する時、例外事件を処理する
		/// </summary>
		/// <param name="exp">DBアクセス例外</param>
		//************************************************************************

		private void ThrowNpgsqlException(NpgsqlException exp)
		{
			{
				throw new Exception(exp.Message, exp);
			}
		}
		#endregion

		#region public メソッド

		//#if ORACLE
		//        #region プロシージャを実行する
		//        //************************************************************************
		//        /// <summary>
		//        /// プロシージャを実行します
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            ExecSp(strSPName, null, connection);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region プロシージャを実行する
		//        //************************************************************************
		//        /// <summary>
		//        /// プロシージャを実行します
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <param name="transaction">DBトランザクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, OracleConnection connection, OracleTransaction transaction)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, null, connection, transaction);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region 指定したパラメータプロシージャを実行する
		//        //************************************************************************
		//        /// <summary>
		//        /// 指定したパラメータプロシージャを実行する
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="list">DBアクセスパラメータリスト</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, list, connection, null);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region 指定したパラメータプロシージャを実行する
		//        //************************************************************************
		//        /// <summary>
		//        /// 指定したパラメータプロシージャを実行する
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="list">DBアクセスパラメータリスト</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <param name="transaction">DBトランザクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, OracleConnection connection, OracleTransaction transaction)
		//        {
		//            bool bReturn = false;

		//            OracleCommand dbCommand = new OracleCommand(strSPName, connection);
		//            if (transaction != null)
		//            {
		//                dbCommand.Transaction = transaction;	
		//            }
		//            dbCommand.CommandType = CommandType.StoredProcedure;

		//            try
		//            {
		//                if (list != null)
		//                {
		//                    IEnumerator Enumerator;
		//                    Enumerator = list.GetEnumerator();

		//                    NCPara para = null;
		//                    while(Enumerator.MoveNext())
		//                    {
		//                        para = (NCPara)Enumerator.Current;
		//                        dbCommand.Parameters.Add(GetOracleParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
		//                    }
		//                }

		//                dbCommand.ExecuteNonQuery();
		//                bReturn = true;
		//            }
		//            catch( Exception err)
		//            {
		//                if (list != null)
		//                {
		//                    IEnumerator Enumerator;
		//                    Enumerator = list.GetEnumerator();

		//                    NCPara para = null;
		//                    StringBuilder strInfo = new StringBuilder();

		//                    while (Enumerator.MoveNext())
		//                    {
		//                        para = (NCPara)Enumerator.Current;
		//                        if (!para.OutPut)
		//                        {
		//                            strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
		//                        }                        
		//                    }
		//                    NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
		//                }
		//                // NCPublicFunction.WriteLog(m_strLogFileName, err.Message);
		//                NCLogger.GetInstance().WriteExceptionLog(err);
		//            }
		//            finally
		//            {   
		//                dbCommand.Parameters.Clear();
		//            }
		//            return bReturn;
		//        }
		//        #endregion


		//        #region プロシージャを実行して、オブジェクトを返す
		//        //************************************************************************
		//        /// <summary>
		//        /// プロシージャを実行して、オブジェクトを返す
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="list">DBアクセスパラメータリスト</param>
		//        /// <param name="objReturn">戻るのオブジェクト</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, ref object objReturn, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, list, ref objReturn, connection, null);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region プロシージャを実行して、オブジェクトを返す
		//        //************************************************************************
		//        /// <summary>
		//        /// プロシージャを実行して、オブジェクトを返す
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="list">DBアクセスパラメータリスト</param>
		//        /// <param name="objReturn">戻るのオブジェクト</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <param name="transaction">DBトランザクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, ref object objReturn, OracleConnection connection, OracleTransaction transaction)
		//        {
		//            bool bReturn = false;
		//            OracleCommand dbCommand = new OracleCommand(strSPName, connection);
		//            if (transaction != null)
		//            {
		//                dbCommand.Transaction = transaction;
		//            }
		//            dbCommand.CommandType = CommandType.StoredProcedure;

		//            OracleParameter SqlParam = null;
		//            try
		//            {
		//                if (list != null)
		//                {
		//                    IEnumerator Enumerator;
		//                    Enumerator = list.GetEnumerator();
		//                    NCPara para = null;
		//                    while(Enumerator.MoveNext())
		//                    {
		//                        para = (NCPara)Enumerator.Current;
		//                        if (para.OutPut == true)
		//                        {
		//                            SqlParam = dbCommand.Parameters.Add(GetOracleParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));					
		//                        }
		//                        else
		//                        {
		//                            dbCommand.Parameters.Add(GetOracleParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
		//                        }
		//                    }
		//                }

		//                dbCommand.ExecuteNonQuery();
		//                if (SqlParam != null && objReturn != null) objReturn = SqlParam.Value;
		//                bReturn = true;
		//            }
		//            catch( Exception err)
		//            {
		//                if (list != null)
		//                {
		//                    IEnumerator Enumerator;
		//                    Enumerator = list.GetEnumerator();

		//                    NCPara para = null;
		//                    StringBuilder strInfo = new StringBuilder();

		//                    while (Enumerator.MoveNext())
		//                    {
		//                        para = (NCPara)Enumerator.Current;
		//                        if (!para.OutPut)
		//                        {
		//                            strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
		//                        }
		//                    }
		//                    NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
		//                }
		//                // NCPublicFunction.WriteLog(m_strLogFileName, err.Message);
		//                NCLogger.GetInstance().WriteExceptionLog(err);
		//            }
		//            finally
		//            {   
		//                dbCommand.Parameters.Clear();
		//            }
		//            return bReturn;
		//        }
		//        #endregion

		//        #region プロシージャを実行して、データセットを返す
		//        //************************************************************************
		//        /// <summary>
		//        /// プロシージャを実行して、データセットを返す
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="list">DBアクセスパラメータリスト</param>
		//        /// <param name="dataSet">戻るのデータセット</param>
		//        /// <param name="strTable">テーブルの名前</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, ref DataSet dataSet, string strTable, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, list, ref dataSet, strTable, connection, null);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region プロシージャを実行して、データセットを返す
		//        //************************************************************************
		//        /// <summary>
		//        /// プロシージャを実行して、データセットを返す
		//        /// </summary>
		//        /// <param name="strSPName">プロシージャの名称</param>
		//        /// <param name="list">DBアクセスパラメータリスト</param>
		//        /// <param name="dataSet">戻るのデータセット</param>
		//        /// <param name="strTable">テーブルの名前</param>
		//        /// <param name="connection">DBコネクション</param>
		//        /// <param name="transaction">DBトランザクション</param>
		//        /// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, ref DataSet dataSet, string strTable, OleDbConnection connection, OleDbTransaction transaction)
		//        {
		//            bool bReturn = false;
		//            if (dataSet == null) dataSet = new DataSet();
		//            OleDbCommand dbCommand = new OleDbCommand(strSPName, connection);
		//            if (transaction != null)
		//            {
		//                dbCommand.Transaction = transaction;
		//            }
		//            dbCommand.CommandType = CommandType.StoredProcedure;
		//            OleDbDataAdapter dbAdapter = null;
		//            try
		//            {
		//                if (list != null)
		//                {
		//                    IEnumerator Enumerator;
		//                    Enumerator = list.GetEnumerator();
		//                    NCPara para = null;
		//                    while(Enumerator.MoveNext())
		//                    {
		//                        para = (NCPara)Enumerator.Current;
		//                        dbCommand.Parameters.Add(GetOleDbParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
		//                    }
		//                }
		//                //アダプターの構成
		//                dbAdapter = new OleDbDataAdapter(dbCommand);

		//                dbAdapter.Fill(dataSet, strTable);
		//                bReturn = true;
		//            }
		//            catch( Exception err)
		//            {
		//                if (list != null)
		//                {
		//                    IEnumerator Enumerator;
		//                    Enumerator = list.GetEnumerator();

		//                    NCPara para = null;
		//                    StringBuilder strInfo = new StringBuilder();

		//                    while (Enumerator.MoveNext())
		//                    {
		//                        para = (NCPara)Enumerator.Current;
		//                        if (!para.OutPut)
		//                        {
		//                            strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
		//                        }
		//                    }
		//                    NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
		//                }

		//                NCLogger.GetInstance().WriteExceptionLog(err);
		//            }
		//            finally
		//            {   
		//                dbCommand.Parameters.Clear();
		//                dbAdapter.Dispose();
		//            }
		//            return bReturn;
		//        }	
		//        #endregion
		//#endif


		//************************************************************************
		/// <summary>
		/// SQL文を実行する。
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="dataSet">戻るのデータセット</param>
		/// <param name="maxRecords">データセットの最大数</param>
		/// <param name="strTable">テーブルの名前</param>
		/// <param name="connection">DBコネクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, int maxRecords, string strTable, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, ref dataSet, maxRecords, strTable, connection, null);
			return bReturn;
		}

		//************************************************************************
		/// <summary>
		/// SQL文を実行する。
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="dataSet">戻るのデータセット</param>
		/// <param name="maxRecords">データセットの最大数</param>
		/// <param name="strTable">テーブルの名前</param>
		/// <param name="connection">DBコネクション</param>
		/// <param name="transaction">DBトランザクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, int maxRecords, string strTable, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;

			if (dataSet == null) dataSet = new DataSet();
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// トランザクションが起動するかどうかを判断する。
			if (transaction != null)
			{
				dbCommand.Transaction = transaction;
			}
			NpgsqlDataAdapter dbAdapter = null;
			try
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();
					NCPara para = null;
					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						dbCommand.Parameters.Add(GetNpgsqlParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
					}
				}
				dbAdapter = new NpgsqlDataAdapter(dbCommand);
				dbAdapter.Fill(dataSet, 0, maxRecords, strTable);
				bReturn = true;
			}
			catch (NpgsqlException oex)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}
				NCLogger.GetInstance().WriteExceptionLog(oex);
				ThrowNpgsqlException(oex);
			}
			catch (Exception err)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}
				NCLogger.GetInstance().WriteExceptionLog(err);

				throw new Exception(err.Message, err);
			}
			finally
			{
				dbCommand.Dispose();
				dbCommand.Parameters.Clear();
				dbAdapter.Dispose();
			}
			return bReturn;
		}

		#region SQLステートメントを実行する
		//************************************************************************
		/// <summary>
		/// SQL文を実行する。
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="connection">DBコネクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, null, connection);
			return bReturn;
		}
		#endregion

		#region SQLステートメントを実行する
		//************************************************************************
		/// <summary>
		/// SQL文を実行する。
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="connection">DBコネクション</param>
		/// <param name="transaction">DBトランザクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, null, connection, transaction);
			return bReturn;
		}
		#endregion

		#region 指定パラメータのSQLステートメントを実行する
		//************************************************************************
		/// <summary>
		/// 指定パラメータのSQLステートメントを実行する
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="connection">DBコネクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, connection, null);
			return bReturn;
		}
		#endregion

		#region 指定パラメータのSQLステートメントを実行する
		//************************************************************************
		/// <summary>
		/// 指定パラメータのSQLステートメントを実行する
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="connection">DBコネクション</param>
		/// <param name="transaction">DBトランザクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// トランザクションが起動するかどうかを判断する。
			if (transaction != null)
			{
				dbCommand.Transaction = transaction;
			}

			try
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();
					NCPara para = null;
					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						dbCommand.Parameters.Add(GetNpgsqlParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
					}
				}

				dbCommand.ExecuteNonQuery();
				bReturn = true;
			}
			catch (NpgsqlException oex)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}
				NCLogger.GetInstance().WriteExceptionLog(oex);
				ThrowNpgsqlException(oex);
			}
			catch (Exception err)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}

				NCLogger.GetInstance().WriteExceptionLog(err);
				throw new Exception(err.Message, err);
			}
			finally
			{
				dbCommand.Parameters.Clear();
			}
			return bReturn;
		}
		#endregion

		#region 指定パラメータのSQLステートメントを実行して、オブジェクトを返す
		//************************************************************************
		/// <summary>
		/// 指定パラメータのSQLステートメントを実行して、オブジェクトを返す
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="objReturn">戻るのオブジェクト</param>
		/// <param name="connection">DBコネクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref object objReturn, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, ref objReturn, connection, null);
			return bReturn;
		}
		#endregion

		#region 指定パラメータのSQLステートメントを実行して、オブジェクトを返す
		//************************************************************************
		/// <summary>
		/// 指定パラメータのSQLステートメントを実行して、オブジェクトを返す
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="objReturn">戻るのオブジェクト</param>
		/// <param name="connection">DBコネクション</param>
		/// <param name="transaction">DBトランザクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref object objReturn, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// トランザクションが起動するかどうかを判断する。
			if (transaction != null)
			{
				dbCommand.Transaction = transaction;
			}
			try
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();
					NCPara para = null;
					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						dbCommand.Parameters.Add(GetNpgsqlParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
					}
				}
				objReturn = dbCommand.ExecuteScalar();

				bReturn = true;
			}
			catch (NpgsqlException oex)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}
				NCLogger.GetInstance().WriteExceptionLog(oex);
				ThrowNpgsqlException(oex);
			}
			catch (Exception err)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}

				NCLogger.GetInstance().WriteExceptionLog(err);

				throw new Exception(err.Message, err);
			}
			finally
			{
				dbCommand.Parameters.Clear();
			}
			return bReturn;
		}
		#endregion

		#region 指定パラメータのSQLステートメントを実行して、データセットを返す
		//************************************************************************
		/// <summary>
		/// 指定パラメータのSQLステートメントを実行して、データセットを返す
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="dataSet">戻るのデータセット</param>
		/// <param name="strTable">テーブルの名前</param>
		/// <param name="connection">DBコネクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, string strTable, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, ref dataSet, strTable, connection, null);
			return bReturn;
		}
		#endregion

		#region 指定パラメータのSQLステートメントを実行して、データセットを返す
		//************************************************************************
		/// <summary>
		/// 指定パラメータのSQLステートメントを実行して、データセットを返す
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="dataSet">戻るのデータセット</param>
		/// <param name="strTable">テーブルの名前</param>
		/// <param name="connection">DBコネクション</param>
		/// <param name="transaction">DBトランザクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, string strTable, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;

			if (dataSet == null) dataSet = new DataSet();
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// トランザクションが起動するかどうかを判断する。
			if (transaction != null)
			{
				dbCommand.Transaction = transaction;
			}

			NpgsqlDataAdapter dbAdapter = null;
			try
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();
					NCPara para = null;
					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						dbCommand.Parameters.Add(GetNpgsqlParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
					}
				}
				dbAdapter = new NpgsqlDataAdapter(dbCommand);
				dbAdapter.Fill(dataSet, strTable);
				bReturn = true;
			}
			catch (NpgsqlException oex)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}
				NCLogger.GetInstance().WriteExceptionLog(oex);
				ThrowNpgsqlException(oex);
			}
			catch (Exception err)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}
				NCLogger.GetInstance().WriteExceptionLog(err);

				throw new Exception(err.Message, err);
			}
			finally
			{
				dbCommand.Parameters.Clear();
				dbAdapter.Dispose();
			}
			return bReturn;
		}
		#endregion

		#region データベースにデータセットで変更する
		//************************************************************************
		/// <summary>
		/// データベースにデータセットで変更する
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="dataSet">データセット</param>
		/// <param name="strTable">テーブルの名前</param>
		/// <param name="connection">DBコネクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool UpdateTable(string strCommandText, NCArrayList list, DataSet dataSet, string strTable, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = UpdateTable(strCommandText, list, dataSet, strTable, connection, null);
			return bReturn;
		}
		#endregion

		#region データベースにデータセットで変更する
		//************************************************************************
		/// <summary>
		/// データベースにデータセットで変更する
		/// </summary>
		/// <param name="strCommandText">SQL文</param>
		/// <param name="list">パラメータリスト</param>
		/// <param name="dataSet">データセット</param>
		/// <param name="strTable">テーブルの名前</param>
		/// <param name="connection">DBコネクション</param>
		/// <param name="transaction">DBトランザクション</param>
		/// <returns>正常の場合はtrueを返す、それ以外の場合はfalse。</returns>
		//************************************************************************
		public bool UpdateTable(string strCommandText, NCArrayList list, DataSet dataSet, string strTable, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);
			if (transaction != null)
			{
				dbCommand.Transaction = transaction;
			}
			try
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();
					NCPara para = null;
					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						dbCommand.Parameters.Add(GetNpgsqlParameter(para.Key, para.Type, para.Size, para.Value, para.OutPut));
					}
				}
				NpgsqlDataAdapter dbAdapter = new NpgsqlDataAdapter(dbCommand);
				NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(dbAdapter);

				dbAdapter.Update(dataSet, strTable);
				bReturn = true;
			}
			catch (Exception err)
			{
				if (list != null)
				{
					IEnumerator Enumerator;
					Enumerator = list.GetEnumerator();

					NCPara para = null;
					StringBuilder strInfo = new StringBuilder();

					while (Enumerator.MoveNext())
					{
						para = (NCPara)Enumerator.Current;
						if (!para.OutPut)
						{
							strInfo.AppendFormat("{0}:{1};", para.Key, para.Value);
						}
					}
					NCLogger.GetInstance().WriteInfoLog(strInfo.ToString());
				}
				NCLogger.GetInstance().WriteExceptionLog(err);
			}
			finally
			{
				dbCommand.Parameters.Clear();
			}
			return bReturn;
		}
		#endregion

		#region プロシジャーパラメータを取得する
		//************************************************************************
		/// <summary>
		/// プロシジャーパラメータを取得する
		/// </summary>
		/// <param name="strName">パラメータの名前</param>
		/// <param name="dbType">パラメータのタイプ</param>
		/// <param name="iSize">パラメータのサイズ</param>
		/// <param name="objValue">パラメータの値</param>
		/// <param name="bOutput">パラメータの出力フラグ</param>
		/// <returns>プロシージャパラメータ</returns>
		//************************************************************************
		public NpgsqlParameter GetNpgsqlParameter(string strName, NpgsqlTypes.NpgsqlDbType dbType, int iSize, object objValue, bool bOutput)
		{
			NpgsqlParameter parameter = new NpgsqlParameter(strName, dbType);

			if (iSize > 0)
			{
				parameter.Size = iSize;
			}
			//パラメーターは出力の場合、パラメーターの型を設定
			if (bOutput)
			{
				if (strName == null || strName == "")
				{
					parameter.Direction = ParameterDirection.ReturnValue;
				}
				else
				{
					parameter.Direction = ParameterDirection.Output;
				}
			}
			else
			{
				parameter.Value = objValue;
			}
			return parameter;
		}
		#endregion

		#region プロシジャーパラメータを取得する
		//************************************************************************
		/// <summary>
		/// プロシジャーパラメータを取得する
		/// </summary>
		/// <param name="strName">パラメータの名前</param>
		/// <param name="objValue">パラメータの値</param>
		/// <returns>プロシージャパラメータを戻る</returns>
		//************************************************************************
		public NpgsqlParameter GetNpgsqlParameter(string strName, object objValue)
		{
			NpgsqlParameter parameter = new NpgsqlParameter(strName, (NpgsqlTypes.NpgsqlDbType)objValue);
			return parameter;
		}
		#endregion
		#endregion
	}
}
