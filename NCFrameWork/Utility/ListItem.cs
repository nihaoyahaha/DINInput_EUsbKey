using System;
using System.Collections.Generic;
using System.Text;

namespace DI.NCFrameWork
{
    public class ListItem
    {
        // ���X�g���ڂ̕\��������
        private string _strDisplayName = "";

        // ���X�g���ڂ̒l
        private string _strValue = "";

        /// <summary>
        /// ���X�g���ڂ̕\��������
        /// </summary>
        public string DisplayName
        {
            get { return _strDisplayName; }
            set { _strDisplayName = value; }
        }

        /// <summary>
        /// ���X�g���ڂ̒l
        /// </summary>
        public string Value
        {
            get { return _strValue; }
            set { _strValue = value; }
        }

        /// <summary>
        /// ���X�g���ڂ���������
        /// </summary>
        public ListItem()
        {
        }

        /// <summary>
        /// ���X�g���ڂ���������
        /// </summary>
        /// <param name="strDisplayName">�\��������</param>
        public ListItem(string strDisplayName)
        {
            _strDisplayName = strDisplayName;
        }

        /// <summary>
        /// ���X�g���ڂ���������
        /// </summary>
        /// <param name="strDisplayName">�\��������</param>
        /// <param name="strValue">���ڂ̒l</param>
        public ListItem(string strDisplayName, string strValue)
        {
            _strDisplayName = strDisplayName;
            _strValue = strValue;
        }

        /// <summary>
        /// object��\��String��Ԃ�
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _strDisplayName;
        }
    }
}
