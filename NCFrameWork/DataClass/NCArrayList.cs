namespace DI.NCFrameWork
{
	using System;
	using System.Collections;


	/// <summary>
	/// NCArrayList �̊T�v�̐����ł��B
	/// </summary>
	public class NCArrayList:CollectionBase 
	{
		public NCArrayList()
		{
			// 
			// TODO: �R���X�g���N�^ ���W�b�N�������ɒǉ����Ă��������B
			//
		}

		public NCPara this[int index] 
		{
			get {return (NCPara)List[index];}
			set {List[index] = value;}
		}

		public int Add(NCPara value) 
		{
			return (List.Add(value));
		}

		public int IndexOf(NCPara value) 
		{
			return (List.IndexOf(value));
		}

		public void Insert(int index, NCPara value) 
		{
			List.Insert(index, value);
		}

		public void Remove(NCPara value) 
		{
			List.Remove(value);
		}


		public bool Contains(NCPara value) 
		{
			return (List.Contains(value));
		}

		public bool Contains(string key)
		{
			foreach (NCPara para in List) 
			{
				if (para.Key == key)
				{
					return true;
				}
			}

			return false;
		}

	}
}
