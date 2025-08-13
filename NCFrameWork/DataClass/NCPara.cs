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
	using Npgsql;


	//************************************************************************
	/// <summary>
	/// NCPara�N���X�FDB����擾�̃f�[�^���`����
	/// </summary>
	//************************************************************************
	public class NCPara
	{
		/// <summary>
		/// �L�[
		/// </summary>
		private string _Key;
		/// <summary>
		/// �f�[�^�^�C�v
		/// </summary>
		private NpgsqlTypes.NpgsqlDbType _Type;

		/// <summary>
		/// �f�[�^�̃T�C�Y
		/// </summary>
		private int _Size;
		/// <summary>
		/// �f�[�^�̒l
		/// </summary>
		private object _Value;
		/// <summary>
		/// �f�[�^�̖߂�t���O�Ftrue�|�߂�Afalse�|�߂�Ȃ�
		/// </summary>
		private bool _OutPut;


		/// <summary>
		/// �L�[
		/// </summary>
		public string Key
		{
			get { return _Key; }
			set { _Key = value; }
		}

		/// <summary>
		/// �f�[�^�^�C�v
		/// </summary>
		public NpgsqlTypes.NpgsqlDbType Type
		{
			get { return _Type; }
			set { _Type = value; }
		}

		/// <summary>
		/// �f�[�^�̃T�C�Y
		/// </summary>
		public int Size
		{
			get { return _Size; }
			set { _Size = value; }
		}

		/// <summary>
		/// �f�[�^�̒l
		/// </summary>
		public object Value
		{
			get { return _Value; }
			set { _Value = value; }
		}

		/// <summary>
		/// �f�[�^�̖߂�t���O�Ftrue�|�߂�Afalse�|�߂�Ȃ�
		/// </summary>
		public bool OutPut
		{
			get { return _OutPut; }
			set { _OutPut = value; }
		}

		//************************************************************************
		/// <summary>
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="strKey">�L�[</param>
		/// <param name="dbType">�f�[�^�̃^�C�v</param>
		/// <param name="iSize">�f�[�^�̃T�C�Y</param>
		/// <param name="objValue">�f�[�^�̒l</param>
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
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="strKey">�L�[</param>
		/// <param name="oraType">�f�[�^�̃^�C�v</param>
		/// <param name="iSize">�f�[�^�̃T�C�Y</param>
		/// <param name="objValue">�f�[�^�̒l</param>
		/// <param name="objDefault">�f�B�t�H���g�l</param>
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
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="strKey">�L�[</param>
		/// <param name="SqlType">�f�[�^�̃^�C�v</param>
		/// <param name="iSize">�f�[�^�̃T�C�Y</param>
		/// <param name="objValue">�f�[�^�̒l</param>
		/// <param name="bOutPut">�f�[�^�̖߂�t���O</param>
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
		/// �R���X�g���N�^
		/// </summary>
		/// <param name="strKey">�L�[</param>
		/// <param name="dbType">�f�[�^�̃^�C�v</param>
		/// <param name="iSize">�f�[�^�̃T�C�Y</param>
		/// <param name="objValue">�f�[�^�̒l</param>
		/// <param name="bOutPut">�f�[�^�̖߂�t���O</param>
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
