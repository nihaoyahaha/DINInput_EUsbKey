//*****************************************************************************
// ユーザ名					ニューコン
// システム名				
// サブシステム名			ベースフォーム
// 作成者					蒋@NC
// 改版日					2007/07/19
// 改版内容					新規作成
//*****************************************************************************
namespace DI.NCFrameWork
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// ネットワークアクセスクラス。
    /// ユーザＩＤ・パスワードを利用してネットワーク共有フォルダをアクセスする。
    /// </summary>
    public class NetWorkAccess
    {
        [DllImport("mpr.dll", CharSet = CharSet.Ansi)]
        private static extern int WNetAddConnection2(NetResource netResource, string password, string Username, int Flag);
        [DllImport("mpr.dll", CharSet = CharSet.Ansi)]
        private static extern int WNetGetConnection(string strLocalName, string strRemotName, int Length);
        [DllImport("mpr.dll", CharSet = CharSet.Ansi)]
        private static extern int WNetCancelConnection2(string LocalName, int Flag, int Force);


        /// <summary>
        /// ネットワーク共有フォルダアクセス結果－－正常
        /// </summary>
        public const int NO_ERROR = 0;

        #region ネットワーク共有フォルダアクセス
        //**********************************************************************
        /// <summary>
        /// static int Connect(string remoteName, string userName, string password)
        /// </summary>
        /// <remarks>
        /// ユーザＩＤ・パスワードを利用してネットワーク共有フォルダをアクセスする。
        /// </remarks>
        /// <param name="remoteName">ネットワーク共有フォルダパス</param>
        /// <param name="userName">ユーザＩＤ</param>
        /// <param name="password">パスワード</param>
        /// <returns>int アクセス結果</returns>
        /// <example>
        /// <code></code>
        /// </example>
        //**********************************************************************
        public static int Connect(string remoteName, string userName, string password)
        {
            NetResource mappedDrive = new NetResource();
            mappedDrive.dwScope = 2;
            mappedDrive.dwType = 1;
            mappedDrive.dwDisplayType = 3;
            mappedDrive.dwUsage = 1;
            mappedDrive.LocalName = null;
            mappedDrive.RemoteName = remoteName;
            mappedDrive.Provider = null;

            return WNetAddConnection2(mappedDrive, password, userName, 0);
        }
        #endregion

        #region 既存のネットワーク接続を切断する
        //**********************************************************************
        /// <summary>
        /// static int DisConnect(string remoteName)
        /// </summary>
        /// <remarks>
        /// 既存のネットワーク接続を切断する
        /// </remarks>
        /// <param name="remoteName">資源名</param>
        /// <returns>成功は「0」</returns>
        /// <example>
        /// <code></code>
        /// </example>
        //**********************************************************************
        public static int DisConnect(string remoteName)
        {
            return WNetCancelConnection2(remoteName, 0, 1);
        }
        #endregion
    }

    /// <summary>
    /// ネットワークリソース
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    class NetResource
    {
        /// <summary>
        /// リソースの範囲
        /// </summary>
        public int dwScope;
        /// <summary>
        /// リソースの種類
        /// </summary>
        public int dwType;
        /// <summary>
        /// リソースの表示タイプ
        /// </summary>
        public int dwDisplayType;
        /// <summary>
        /// リソースの用途
        /// </summary>
        public int dwUsage;
        /// <summary>
        /// ローカル名
        /// </summary>
        public String LocalName = null;
        /// <summary>
        /// リモートリソース名
        /// </summary>
        public String RemoteName = null;
        /// <summary>
        /// ネットワークプロバイダのコメント
        /// </summary>
        public String Comment = null;
        /// <summary>
        /// ネットワークプロバイダの名前
        /// </summary>
        public String Provider = null;
    }

}

