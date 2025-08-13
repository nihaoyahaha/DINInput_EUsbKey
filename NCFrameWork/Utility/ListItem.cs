using System;
using System.Collections.Generic;
using System.Text;

namespace DI.NCFrameWork
{
    public class ListItem
    {
        // リスト項目の表示文字列
        private string _strDisplayName = "";

        // リスト項目の値
        private string _strValue = "";

        /// <summary>
        /// リスト項目の表示文字列
        /// </summary>
        public string DisplayName
        {
            get { return _strDisplayName; }
            set { _strDisplayName = value; }
        }

        /// <summary>
        /// リスト項目の値
        /// </summary>
        public string Value
        {
            get { return _strValue; }
            set { _strValue = value; }
        }

        /// <summary>
        /// リスト項目を実装する
        /// </summary>
        public ListItem()
        {
        }

        /// <summary>
        /// リスト項目を実装する
        /// </summary>
        /// <param name="strDisplayName">表示文字列</param>
        public ListItem(string strDisplayName)
        {
            _strDisplayName = strDisplayName;
        }

        /// <summary>
        /// リスト項目を実装する
        /// </summary>
        /// <param name="strDisplayName">表示文字列</param>
        /// <param name="strValue">項目の値</param>
        public ListItem(string strDisplayName, string strValue)
        {
            _strDisplayName = strDisplayName;
            _strValue = strValue;
        }

        /// <summary>
        /// objectを表すStringを返す
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _strDisplayName;
        }
    }
}
