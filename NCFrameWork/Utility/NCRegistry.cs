using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace NC.NCFrameWork
{
    public class NCRegistry
    {
        /// <summary>
        /// ���W�X�g�����[�g�L�[�̖��O
        /// </summary>
        private static string _regRoot = @"Software\Newcon\NCFramework\";

        public NCRegistry(string strRegRoot)
        {
            _regRoot = strRegRoot;
        }

        /// <summary>
        /// �w�肵�����W�X�g�� �L�[�ɖ��O/�l�y�A�̒l���擾����
        /// </summary>
        /// <param name="regPath">���W�X�g�� �L�[</param>
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
        /// �w�肵�����W�X�g�� �L�[�ɖ��O/�l�y�A�̒l��ݒ肷��
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
