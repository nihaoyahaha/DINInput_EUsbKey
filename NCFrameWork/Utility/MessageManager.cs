//*****************************************************************************
// ユーザ名					ニューコン
// システム名				
// サブシステム名			エラーメッセージマネジャークラス
// 作成者					蒋@NC
// 改版日					2007/07/19
// 改版内容					新規作成
//*****************************************************************************
using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using DI.NCFrameWork;

namespace DI.NCFrameWork
{
	#region メッセージマネージャー
	/// <summary>
	/// メッセージマネージャー。
	/// </summary>
	public class MessageManager
	{
        /// <summary>
        /// アプリケーションID
        /// </summary>
		public static string application_id = "MessageID";

		/// <summary>
		/// インスタンスオブジェクト
		/// </summary>
		private static MessageManager messageManager = null;

        private static Hashtable hashMessage = null;

		public MessageManager()
		{
			// 
			// TODO: コンストラクタ ロジックをここに追加してください。
			//
		}

		/// <summary>
		/// 新しいインスタンスを作成してください
		/// </summary>
		/// <returns>メッセージマネージャーインスタンス</returns>
		public static MessageManager NewInstance()
		{
			if(messageManager == null)
			{
				messageManager = new MessageManager();
                hashMessage = new Hashtable();
                string strFileName = Path.Combine(Application.StartupPath, "Message","NCMessage.xml");
                GetALLMessage(strFileName);
				return messageManager;
			}
			else
			{
				return messageManager;
			}
		}

		/// <summary>
		/// メッセージを取得
		/// </summary>
		/// <param name="application">アプリケーション</param>
		/// <param name="strFileName">ファイル名</param>
		/// <returns>bool</returns>
		private static bool GetALLMessage(string strFileName)
		{
			XmlTextReader reader = null;
			try
			{
				if (File.Exists(strFileName))
				{
					reader = new XmlTextReader(strFileName);
					//ループでXMLファイル情報を取得する
					while (reader.Read())
					{
						//ノードタイプを判断する
						string strMessage = "";
						string strCode="";
						switch (reader.NodeType)
						{
								//XMLタグの場合
							case XmlNodeType.Element:
								if (reader.LocalName.ToUpper() == "MESSAGE") 
								{
									if( reader.MoveToFirstAttribute() )
									{
										do
										{
											//メッセージIDを取得
											if (reader.Name.ToUpper() == "ID") 
											{
												strCode = reader.Value;
											}
										
											//メッセージを取得
											if (reader.Name.ToUpper() == "MSG") 
											{
												strMessage = reader.Value;
											
											}
										} while( reader.MoveToNextAttribute() );
                                        if (!hashMessage.Contains(strCode))
                                        {
                                            hashMessage.Add(strCode, strMessage);
                                        }
									}						
								}

								break;
								//テキストの場合
							case XmlNodeType.Text:
								break;
								//終了タグの場合
							case XmlNodeType.EndElement:
								break;
						}
					}
				}
			}
			catch(Exception exp)
			{
                NCLogger.GetInstance().WriteExceptionLog(exp);
			}	
			finally
			{
				if (reader != null) 
				{
					reader.Close();
				}
			}
			return true;

		}

		/// <summary>
		/// メッセージコードに応じて、メッセージの詳細を得てください。
		/// </summary>
		/// <param name="argMessageCode">メッセージコード</param>
		/// <returns>メッセージ</returns>
		public string GetMessage(string argMessageCode)
		{
			string strMessage = "";

            if (hashMessage != null)
			{
                if (hashMessage.Contains(argMessageCode))
				{
                    object objmessage = hashMessage[argMessageCode];
					if(objmessage != null)
					{
                        strMessage = hashMessage[argMessageCode].ToString();
					}
				}
			}
			return strMessage;
		}
	}
	#endregion
}
