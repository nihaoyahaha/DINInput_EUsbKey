using CommonLib;
using DINInput_EUsbKey.Properties;
using DINServerObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Windows.Forms;

namespace DINInput_EUsbKey
{
	static class Program
	{
		public static IWinServiceAPI serviceApi = null;
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			InitWinServiceApi();
			Application.Run(new MainForm());
		}

		private static void InitWinServiceApi()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("App.json", optional: false, reloadOnChange: true)
				.Build();

			string connectionMode = configuration["ConnectionMode"];
			
			//连接服务端
			if (connectionMode == "service")
			{
				string remoteServer = configuration["RemoteServer"];
				int remotePort = int.Parse(configuration["RemotePort"]);
				int remotePortSSL = int.Parse(configuration["RemotePortSSL"]);
				int timeout = int.Parse(configuration["TimeOut"]);

				CheckCertificate(remoteServer, remotePortSSL.ToString());

				BasicHttpBinding binding = new BasicHttpBinding();
				binding.SendTimeout = new TimeSpan(0, 0, 0, timeout);
				binding.MaxReceivedMessageSize = 1073741824;
#if ENCRYPTION
				binding.Security.Mode = BasicHttpSecurityMode.Transport;
				binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

				string certFile = String.Format(@"{0}\{1}", Application.StartupPath, remoteServer + ".cer");
				X509Certificate certificate = new X509Certificate(certFile);
				var certificate2 = new X509Certificate2(certificate);
#endif

#if ENCRYPTION
				string serverUri = $"https://{remoteServer}:{remotePortSSL}/{CmConst.EUsbKeyServiceRoute}";
#else
				string serverUri = $"http://{remoteServer}:{remotePort}/{CmConst.EUsbKeyServiceRoute}";
#endif
				EndpointAddress address = new EndpointAddress(serverUri);
				ChannelFactory<IWinServiceAPI> factory = new ChannelFactory<IWinServiceAPI>(binding, address);

#if ENCRYPTION
				factory.Credentials.ClientCertificate.Certificate = certificate2;
#endif
				serviceApi = factory.CreateChannel();
			}
			else//直连数据库
			{
				PostgresOptions pgOpt = configuration.GetSection("PostgresOptions").Get<PostgresOptions>();
				string strDataConnectionString = $"Server={pgOpt.Server};Port={pgOpt.Port};User Id={pgOpt.UserId};Password={pgOpt.Password};Database={pgOpt.Database};CommandTimeout=300";
				serviceApi = new WinServiceAPI();
				serviceApi.SetConnectionString(strDataConnectionString);
			}
		}

		/// <summary>
		/// 验证证书
		/// </summary>
		/// <param name="certificate_Client"></param>
		/// <returns></returns>
		private static void CheckCertificate(string remote,string port)
		{
			string strRemoteServer = remote;
			string strRemotePort = port;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
			{
				string strServer = "";
				// WCF
				if (((System.Net.HttpWebRequest)sender).Address.Authority.Split(':').Length > 1)
				{
					strServer = strRemoteServer.ToLower().Trim() + ":" + strRemotePort;
				}
				else
				{
					strServer = strRemoteServer.ToLower().Trim();
				}

				if (((System.Net.HttpWebRequest)sender).Address.Authority == strServer)
				{
					if (errors == System.Net.Security.SslPolicyErrors.None)
					{
						string certFile = String.Format(@"{0}\{1}", Application.StartupPath, strRemoteServer + ".cer");
						X509Certificate x509certificate = new X509Certificate(certFile);
						var certificate2 = new X509Certificate2(x509certificate);

						// 验证证书是否在有效期内
						if (((X509Certificate2)certificate).NotBefore > DateTime.Now || ((X509Certificate2)certificate).NotAfter < DateTime.Now)
						{
							return false;
						}
						// 証明書の拇印の比較
						else if (((X509Certificate2)certificate).Thumbprint != certificate2.Thumbprint)
						{
							return false;
						}

						return true;
					}
					else
					{
						return false;
					}
				}
				// webDav
				else
				{
					return true;
				}
			};
		}
	}
}
