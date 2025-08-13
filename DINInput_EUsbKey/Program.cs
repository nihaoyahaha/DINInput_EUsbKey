using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows.Forms;

namespace DINInput_EUsbKey
{
    static class Program
    {
		public static string strDataConnectionString;
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			//jsonプロファイルからpgデータベース接続情報を読み込む
			var settings = new PostgreSqlConnectionConfig();
			new ConfigurationBuilder()
				.AddJsonFile("DBConfig/PostgreSqlConnectionConfig.json", optional: false, reloadOnChange: true)
				.Build()
				.Bind(settings);
			strDataConnectionString = $"Server={settings.Server};Port={settings.Port};User Id={settings.UserId};Password={settings.Password};Database={settings.Database};CommandTimeout=300";
			Application.Run(new MainForm());
        }
    }
}
