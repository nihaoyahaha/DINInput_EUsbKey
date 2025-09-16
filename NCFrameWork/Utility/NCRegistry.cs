using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace NC.NCFrameWork
{
    public class NCRegistry
    {
        /// <summary>
        /// レジストリルートキーの名前
        /// </summary>
        private static string _regRoot = @"Software\Newcon\NCFramework\";

        public NCRegistry(string strRegRoot)
        {
            _regRoot = strRegRoot;
        }

        /// <summary>
        /// 指定したレジストリ キーに名前/値ペアの値を取得する
        /// </summary>
        /// <param name="regPath">レジストリ キー</param>
        /// <returns></returns>
        public string GetRegistryKeyValue(string regKey, string defaultValue)
        {
            object obj = null;
            try
            {
                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(_regRoot + regKey))
                {
                    obj = key.GetValue("string");
                    if (null == obj)
                    {
                        key.SetValue("string", defaultValue);
                        obj = defaultValue;
                    }
                    key.Close();
                }
            }
            catch(Exception ex)
            {
				return defaultValue;
            }
            return (string)obj;
        }

        /// <summary>
        /// 指定したレジストリ キーに名前/値ペアの値を設定する
        /// </summary>
        /// <param name="regPath"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetRegistryKeyValue(string regKey, string val)
        {
            bool bReturn = false;
            try
            {
                using (RegistryKey key = Registry.LocalMachine.CreateSubKey(_regRoot + regKey))
                {
                    key.SetValue("string", val);
                    key.Close();
                    bReturn = true;
                }
            }
            catch
            {
            }
            return bReturn;
        }
    }
}
