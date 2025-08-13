//******************************************************************************
// ���[�U��					�j���[�R��
// �V�X�e����				�t���[�����[�N
// �T�u�V�X�e����			
// �쐬��					��@NC
// ���œ�					2009/05/25
// ���œ��e					ORACLE��POSTGRESQL���ɑΉ�
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
	/// NCDataBase�N���X�F�f�[�^�x�[�X���A�N�Z�X����
	/// </summary>
	//************************************************************************
	[Serializable]
	public class NCDataBase
	{

		#region private ���\�b�h
		//************************************************************************
		/// <summary>
		/// DB�A�N�Z�X�G���[���������鎞�A��O��������������
		/// </summary>
		/// <param name="exp">DB�A�N�Z�X��O</param>
		//************************************************************************

		private void ThrowNpgsqlException(NpgsqlException exp)
		{
			{
				throw new Exception(exp.Message, exp);
			}
		}
		#endregion

		#region public ���\�b�h

		//#if ORACLE
		//        #region �v���V�[�W�������s����
		//        //************************************************************************
		//        /// <summary>
		//        /// �v���V�[�W�������s���܂�
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            ExecSp(strSPName, null, connection);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region �v���V�[�W�������s����
		//        //************************************************************************
		//        /// <summary>
		//        /// �v���V�[�W�������s���܂�
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <param name="transaction">DB�g�����U�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, OracleConnection connection, OracleTransaction transaction)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, null, connection, transaction);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region �w�肵���p�����[�^�v���V�[�W�������s����
		//        //************************************************************************
		//        /// <summary>
		//        /// �w�肵���p�����[�^�v���V�[�W�������s����
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="list">DB�A�N�Z�X�p�����[�^���X�g</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, list, connection, null);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region �w�肵���p�����[�^�v���V�[�W�������s����
		//        //************************************************************************
		//        /// <summary>
		//        /// �w�肵���p�����[�^�v���V�[�W�������s����
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="list">DB�A�N�Z�X�p�����[�^���X�g</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <param name="transaction">DB�g�����U�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
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


		//        #region �v���V�[�W�������s���āA�I�u�W�F�N�g��Ԃ�
		//        //************************************************************************
		//        /// <summary>
		//        /// �v���V�[�W�������s���āA�I�u�W�F�N�g��Ԃ�
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="list">DB�A�N�Z�X�p�����[�^���X�g</param>
		//        /// <param name="objReturn">�߂�̃I�u�W�F�N�g</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, ref object objReturn, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, list, ref objReturn, connection, null);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region �v���V�[�W�������s���āA�I�u�W�F�N�g��Ԃ�
		//        //************************************************************************
		//        /// <summary>
		//        /// �v���V�[�W�������s���āA�I�u�W�F�N�g��Ԃ�
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="list">DB�A�N�Z�X�p�����[�^���X�g</param>
		//        /// <param name="objReturn">�߂�̃I�u�W�F�N�g</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <param name="transaction">DB�g�����U�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
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

		//        #region �v���V�[�W�������s���āA�f�[�^�Z�b�g��Ԃ�
		//        //************************************************************************
		//        /// <summary>
		//        /// �v���V�[�W�������s���āA�f�[�^�Z�b�g��Ԃ�
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="list">DB�A�N�Z�X�p�����[�^���X�g</param>
		//        /// <param name="dataSet">�߂�̃f�[�^�Z�b�g</param>
		//        /// <param name="strTable">�e�[�u���̖��O</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//        //************************************************************************
		//        public bool ExecSp(string strSPName, NCArrayList list, ref DataSet dataSet, string strTable, OracleConnection connection)
		//        {
		//            bool bReturn = false;
		//            bReturn = ExecSp(strSPName, list, ref dataSet, strTable, connection, null);
		//            return bReturn;
		//        }
		//        #endregion

		//        #region �v���V�[�W�������s���āA�f�[�^�Z�b�g��Ԃ�
		//        //************************************************************************
		//        /// <summary>
		//        /// �v���V�[�W�������s���āA�f�[�^�Z�b�g��Ԃ�
		//        /// </summary>
		//        /// <param name="strSPName">�v���V�[�W���̖���</param>
		//        /// <param name="list">DB�A�N�Z�X�p�����[�^���X�g</param>
		//        /// <param name="dataSet">�߂�̃f�[�^�Z�b�g</param>
		//        /// <param name="strTable">�e�[�u���̖��O</param>
		//        /// <param name="connection">DB�R�l�N�V����</param>
		//        /// <param name="transaction">DB�g�����U�N�V����</param>
		//        /// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
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
		//                //�A�_�v�^�[�̍\��
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
		/// SQL�������s����B
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="dataSet">�߂�̃f�[�^�Z�b�g</param>
		/// <param name="maxRecords">�f�[�^�Z�b�g�̍ő吔</param>
		/// <param name="strTable">�e�[�u���̖��O</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, int maxRecords, string strTable, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, ref dataSet, maxRecords, strTable, connection, null);
			return bReturn;
		}

		//************************************************************************
		/// <summary>
		/// SQL�������s����B
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="dataSet">�߂�̃f�[�^�Z�b�g</param>
		/// <param name="maxRecords">�f�[�^�Z�b�g�̍ő吔</param>
		/// <param name="strTable">�e�[�u���̖��O</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <param name="transaction">DB�g�����U�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, int maxRecords, string strTable, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;

			if (dataSet == null) dataSet = new DataSet();
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// �g�����U�N�V�������N�����邩�ǂ����𔻒f����B
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

		#region SQL�X�e�[�g�����g�����s����
		//************************************************************************
		/// <summary>
		/// SQL�������s����B
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, null, connection);
			return bReturn;
		}
		#endregion

		#region SQL�X�e�[�g�����g�����s����
		//************************************************************************
		/// <summary>
		/// SQL�������s����B
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <param name="transaction">DB�g�����U�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, null, connection, transaction);
			return bReturn;
		}
		#endregion

		#region �w��p�����[�^��SQL�X�e�[�g�����g�����s����
		//************************************************************************
		/// <summary>
		/// �w��p�����[�^��SQL�X�e�[�g�����g�����s����
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, connection, null);
			return bReturn;
		}
		#endregion

		#region �w��p�����[�^��SQL�X�e�[�g�����g�����s����
		//************************************************************************
		/// <summary>
		/// �w��p�����[�^��SQL�X�e�[�g�����g�����s����
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <param name="transaction">DB�g�����U�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// �g�����U�N�V�������N�����邩�ǂ����𔻒f����B
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

		#region �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�I�u�W�F�N�g��Ԃ�
		//************************************************************************
		/// <summary>
		/// �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�I�u�W�F�N�g��Ԃ�
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="objReturn">�߂�̃I�u�W�F�N�g</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref object objReturn, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, ref objReturn, connection, null);
			return bReturn;
		}
		#endregion

		#region �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�I�u�W�F�N�g��Ԃ�
		//************************************************************************
		/// <summary>
		/// �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�I�u�W�F�N�g��Ԃ�
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="objReturn">�߂�̃I�u�W�F�N�g</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <param name="transaction">DB�g�����U�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref object objReturn, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// �g�����U�N�V�������N�����邩�ǂ����𔻒f����B
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

		#region �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�f�[�^�Z�b�g��Ԃ�
		//************************************************************************
		/// <summary>
		/// �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�f�[�^�Z�b�g��Ԃ�
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="dataSet">�߂�̃f�[�^�Z�b�g</param>
		/// <param name="strTable">�e�[�u���̖��O</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, string strTable, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = ExecSql(strCommandText, list, ref dataSet, strTable, connection, null);
			return bReturn;
		}
		#endregion

		#region �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�f�[�^�Z�b�g��Ԃ�
		//************************************************************************
		/// <summary>
		/// �w��p�����[�^��SQL�X�e�[�g�����g�����s���āA�f�[�^�Z�b�g��Ԃ�
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="dataSet">�߂�̃f�[�^�Z�b�g</param>
		/// <param name="strTable">�e�[�u���̖��O</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <param name="transaction">DB�g�����U�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool ExecSql(string strCommandText, NCArrayList list, ref DataSet dataSet, string strTable, NpgsqlConnection connection, NpgsqlTransaction transaction)
		{
			bool bReturn = false;

			if (dataSet == null) dataSet = new DataSet();
			NpgsqlCommand dbCommand = new NpgsqlCommand(strCommandText, connection);

			// �g�����U�N�V�������N�����邩�ǂ����𔻒f����B
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

		#region �f�[�^�x�[�X�Ƀf�[�^�Z�b�g�ŕύX����
		//************************************************************************
		/// <summary>
		/// �f�[�^�x�[�X�Ƀf�[�^�Z�b�g�ŕύX����
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="dataSet">�f�[�^�Z�b�g</param>
		/// <param name="strTable">�e�[�u���̖��O</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
		//************************************************************************
		public bool UpdateTable(string strCommandText, NCArrayList list, DataSet dataSet, string strTable, NpgsqlConnection connection)
		{
			bool bReturn = false;
			bReturn = UpdateTable(strCommandText, list, dataSet, strTable, connection, null);
			return bReturn;
		}
		#endregion

		#region �f�[�^�x�[�X�Ƀf�[�^�Z�b�g�ŕύX����
		//************************************************************************
		/// <summary>
		/// �f�[�^�x�[�X�Ƀf�[�^�Z�b�g�ŕύX����
		/// </summary>
		/// <param name="strCommandText">SQL��</param>
		/// <param name="list">�p�����[�^���X�g</param>
		/// <param name="dataSet">�f�[�^�Z�b�g</param>
		/// <param name="strTable">�e�[�u���̖��O</param>
		/// <param name="connection">DB�R�l�N�V����</param>
		/// <param name="transaction">DB�g�����U�N�V����</param>
		/// <returns>����̏ꍇ��true��Ԃ��A����ȊO�̏ꍇ��false�B</returns>
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

		#region �v���V�W���[�p�����[�^���擾����
		//************************************************************************
		/// <summary>
		/// �v���V�W���[�p�����[�^���擾����
		/// </summary>
		/// <param name="strName">�p�����[�^�̖��O</param>
		/// <param name="dbType">�p�����[�^�̃^�C�v</param>
		/// <param name="iSize">�p�����[�^�̃T�C�Y</param>
		/// <param name="objValue">�p�����[�^�̒l</param>
		/// <param name="bOutput">�p�����[�^�̏o�̓t���O</param>
		/// <returns>�v���V�[�W���p�����[�^</returns>
		//************************************************************************
		public NpgsqlParameter GetNpgsqlParameter(string strName, NpgsqlTypes.NpgsqlDbType dbType, int iSize, object objValue, bool bOutput)
		{
			NpgsqlParameter parameter = new NpgsqlParameter(strName, dbType);

			if (iSize > 0)
			{
				parameter.Size = iSize;
			}
			//�p�����[�^�[�͏o�͂̏ꍇ�A�p�����[�^�[�̌^��ݒ�
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

		#region �v���V�W���[�p�����[�^���擾����
		//************************************************************************
		/// <summary>
		/// �v���V�W���[�p�����[�^���擾����
		/// </summary>
		/// <param name="strName">�p�����[�^�̖��O</param>
		/// <param name="objValue">�p�����[�^�̒l</param>
		/// <returns>�v���V�[�W���p�����[�^��߂�</returns>
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
