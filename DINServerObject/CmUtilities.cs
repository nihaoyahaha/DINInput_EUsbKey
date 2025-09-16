using System;
using System.Diagnostics;

namespace DINServerObject
{
    /// <summary>
    /// ツールを表すクラス
    /// </summary>
    public class CmUtilities
    {
        #region フィールド

        /// <summary>
        /// イベントログ出力用EventLogオブジェクト
        /// </summary>
        private static string s_logSource = "DiNetWorks";

        #endregion

        #region 公開　メソッド

        /// <summary>
        ///　イベントログの設定
        /// </summary>
        /// <param name="logSource">イベントログのインスタンス</param>
        public static void SetLog(string logSource) {
            s_logSource = logSource;
            try {
                if (!EventLog.SourceExists(s_logSource)) {
                    EventLog.CreateEventSource(s_logSource, "Application");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Windowsのイベント ログにログを書き出す
        /// </summary>
        public static void Log(string msg)
        {
            EventLog.WriteEntry(s_logSource, msg);
        }

        /// <summary>
        /// Windowsのイベント ログにログを書き出す
        /// </summary>
        public static void Log(Exception exp)
        {
            EventLog.WriteEntry(s_logSource, "Exception: " + exp.Message + "\n" + exp.GetType() +
                                             "\nStack Trace:\n" + exp.StackTrace);
        }

        #endregion

        #region プライベート　メソッド

        /// <summary>
        /// 新規インスタンスの作成
        /// </summary>
        private CmUtilities()
        {
        }

        #endregion
    }
}