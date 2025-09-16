using CommonLib;
using DI.NCFrameWork;
using DINServerObject;
using Microsoft.Win32;
using NC.NCFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace DINEUsbWinService
{
	public partial class CmMainService : ServiceBase
    {
        /// <summary>
        /// イベントログ出力用EventLogオブジェクト
        /// </summary>
        private static string s_logSource = "EUsbKeyService";
		private readonly List<ServiceHost> _httpHosts = new List<ServiceHost>();
		private readonly List<ServiceHost> _httpsHosts = new List<ServiceHost>();
		private string oldCertificateName = null;

		public CmMainService()
        {
            InitializeComponent();

            new WinServiceAPI().SetLog(s_logSource);
        }

        /// <summary>
        /// サービスを開始する
        /// </summary>
        /// <param name="args"></param>
        protected override  void OnStart(string[] args)
        {
			if (args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0]) )
			{
				oldCertificateName = args[0];
			}
			Load();
			BindingPort();
		}

		private void Load()
		{
			try
			{
				NCRegistry registry = new NCRegistry(RegistryConst.Reg_Root);

				string strBuf = "";
				strBuf = registry.GetRegistryKeyValue(RegistryConst.Reg_DataConnectionString,
				"Server=127.0.0.1;Port=5433;User Id=postgres;Password=postgre123456;Database=postgres;Encoding=UTF8;CommandTimeout=300");

				new WinServiceAPI().SetConnectionString(strBuf);

				registry.SetRegistryKeyValue(RegistryConst.Reg_ServiceVersion, AssemblyVersion);

				registry.SetRegistryKeyValue(RegistryConst.Reg_RemotingObjectVersion, new WinServiceAPI().GetAssemblyVersion());

				NCLogger.SetLogType(LogTypeManager.AllUser);

				foreach (var host in _httpHosts.Concat(_httpsHosts))
				{
					try { host.Close(); } catch { }
				}
				_httpHosts.Clear();
				_httpsHosts.Clear();

				XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
				readerQuotas.MaxDepth = 32;
				readerQuotas.MaxArrayLength = 1073741824;

				BasicHttpBinding myBinding = new BasicHttpBinding();
				myBinding.Security.Mode = BasicHttpSecurityMode.None;
				myBinding.MaxReceivedMessageSize = 1073741824;
				myBinding.MessageEncoding = WSMessageEncoding.Text;
				myBinding.CloseTimeout = new TimeSpan(0, 1, 0);
				myBinding.OpenTimeout = new TimeSpan(0, 1, 0);
				myBinding.ReceiveTimeout = new TimeSpan(0, 10, 0);
				myBinding.SendTimeout = new TimeSpan(0, 10, 0);
				myBinding.ReaderQuotas = readerQuotas;

				BasicHttpBinding myBinding_SSL = new BasicHttpBinding();
				myBinding_SSL.Security.Mode = BasicHttpSecurityMode.Transport;
				myBinding_SSL.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
				myBinding_SSL.MaxReceivedMessageSize = (long)1073741824;
				myBinding_SSL.MessageEncoding = WSMessageEncoding.Text;
				myBinding_SSL.CloseTimeout = new TimeSpan(0, 1, 0);
				myBinding_SSL.OpenTimeout = new TimeSpan(0, 1, 0);
				myBinding_SSL.ReceiveTimeout = new TimeSpan(0, 10, 0);
				myBinding_SSL.SendTimeout = new TimeSpan(0, 10, 0);
				myBinding_SSL.ReaderQuotas = readerQuotas;

				string[] ports = ConfigurationManager.AppSettings["channelport"].Split(',');
				string[] portsSSL = ConfigurationManager.AppSettings["channelportSSL"].Split(',');

				foreach (var portStr in ports)
				{
					if (!int.TryParse(portStr.Trim(), out int port)) continue;
					_httpHosts.Add(CreateServiceHost<WinServiceAPI, IWinServiceAPI>(myBinding, port, CmConst.EUsbKeyServiceRoute));
				}

				foreach (var portStr in portsSSL)
				{
					if (!int.TryParse(portStr.Trim(), out int port)) continue;
					_httpsHosts.Add(CreateServiceHost<WinServiceAPI, IWinServiceAPI>(myBinding_SSL, port, CmConst.EUsbKeyServiceRoute, true));
				}
			}
			catch (Exception exp)
			{
				string srvCfgFileName = Process.GetCurrentProcess().MainModule.FileName + ".config";
				CmUtilities.Log("wcfのサービスのOpenにエラー configファイル" + srvCfgFileName);
				CmUtilities.Log("wcfのサービスのOpenにエラー " + exp);
			}
		}

		/// <summary>
		/// 生成wcf链接
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <typeparam name="TContract"></typeparam>
		/// <param name="binding"></param>
		/// <param name="port"></param>
		/// <param name="serviceName"></param>
		/// <param name="isHttps"></param>
		/// <returns></returns>
		private ServiceHost CreateServiceHost<TService, TContract>(BasicHttpBinding binding, int port, string serviceName, bool isHttps = false)
		{
			string scheme = isHttps ? "https" : "http";
			string baseUrl = $"{scheme}://localhost:{port}/{serviceName}";

			var host = new ServiceHost(typeof(TService));
			host.AddServiceEndpoint(typeof(TContract), binding, new Uri(baseUrl));

			var behavior = new ServiceMetadataBehavior();
			if (isHttps)
			{
				behavior.HttpsGetEnabled = true;
				behavior.HttpsGetUrl = new Uri($"{scheme}://localhost:{port}/{serviceName}/metadata");
			}
			else
			{
				behavior.HttpGetEnabled = true;
				behavior.HttpGetUrl = new Uri($"{scheme}://localhost:{port}/{serviceName}/metadata");
			}

			host.Description.Behaviors.Add(behavior);

			CmUtilities.Log($"{serviceName} をポート {port} で作成");
			host.Open();
			CmUtilities.Log($"{serviceName} をポート {port} でOpen");

			return host;
		}

		/// <summary>
		/// サービスを終了する
		/// </summary>
		protected override void OnStop()
		{
			try
			{
				foreach (var host in _httpsHosts)
				{
					host.Close();
				}

				_httpsHosts.Clear();
			}
			catch (Exception exp)
			{
				string srvCfgFileName = Process.GetCurrentProcess().MainModule.FileName + ".config";
				CmUtilities.Log("wcfのサービスのStopにエラー configファイル" + srvCfgFileName);
				CmUtilities.Log("wcfのサービスのStopにエラー " + exp);
			}
		}

		#region アセンブリ属性アクセサ

		/// <summary>
		/// アセンブリタイトル
		/// </summary>
		public string AssemblyTitle
        {
            get
            {
                // このアセンブリ上のタイトル属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // 少なくとも 1 つのタイトル属性がある場合
                if (attributes.Length > 0)
                {
                    // 最初の項目を選択します
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // 空の文字列の場合、その項目を返します
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // タイトル属性がないか、またはタイトル属性が空の文字列の場合、.exe 名を返します
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// アセンブリバージョン
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// アセンブリの説明
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                // このアセンブリ上の説明属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // 説明属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 説明属性がある場合、その値を返します
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// アセンブリ製品
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                // このアセンブリ上の製品属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // 製品属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 製品属性がある場合、その値を返します
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// アセンブリの著作権
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                // このアセンブリ上の著作権属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // 著作権属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 著作権属性がある場合、その値を返します
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// アセンブリの会社
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                // このアセンブリ上の会社属性をすべて取得します
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // 会社属性がない場合、空の文字列を返します
                if (attributes.Length == 0)
                    return "";
                // 会社属性がある場合、その値を返します
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

		/// <summary>
		/// 証明書バインドポート
		/// </summary>
		private void BindingPort()
		{
			#region 实现C#操作Dos命令
			Process p = new Process();
			p.StartInfo.FileName = "cmd.exe";            // 要执行的程序
			p.StartInfo.UseShellExecute = false;         // 不使用系统外壳程序启动
			p.StartInfo.RedirectStandardInput = true;    // 重定向输入
			p.StartInfo.RedirectStandardOutput = true;   // 重定向输出
			p.StartInfo.RedirectStandardError = true;
			p.StartInfo.CreateNoWindow = true;           // 不创建窗口
			p.Start();
			try
			{
				string certificateName = ConfigurationManager.AppSettings["certificateName"];
				string[] portsSSL = ConfigurationManager.AppSettings["channelportSSL"].Split(',');

				X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);//获取本地计算机受信任的根证书的储存区

				store.Open(OpenFlags.ReadOnly);
				string certhash = store.Certificates.OfType<X509Certificate2>().OrderByDescending(cert => cert.NotBefore).FirstOrDefault(cert => cert.SubjectName.Name.EndsWith("CN=" + certificateName)).Thumbprint;
				CmUtilities.Log("certhash " + certhash);

				List<string> oldPorts = GetPortsBoundWithCert(certhash);

				if (!string.IsNullOrEmpty(oldCertificateName))
				{
					string hash = store.Certificates.OfType<X509Certificate2>().OrderByDescending(cert => cert.NotBefore).FirstOrDefault(cert => cert.SubjectName.Name.EndsWith("CN=" + oldCertificateName)).Thumbprint;
					oldPorts.AddRange(GetPortsBoundWithCert(hash));
				}

				if (p.Start())//开始进程  
				{
					p.StandardInput.WriteLine("cd C:\\WINDOWS\\system32");

					// 先删除
					foreach (var port in oldPorts)
					{
						p.StandardInput.WriteLine($"Netsh http delete sslcert ipport=0.0.0.0:{port}");
					}

					// 再绑定
					foreach (var port in portsSSL)
					{
						string portTrim = port.Trim();
						if (string.IsNullOrEmpty(portTrim)) continue;
						string guid = "{" + Guid.NewGuid().ToString() + "}";
						p.StandardInput.WriteLine($"Netsh http add sslcert ipport=0.0.0.0:{portTrim} certhash={certhash} appid={guid}");
					}
					p.StandardInput.WriteLine("exit");
					string output = p.StandardOutput.ReadToEnd();//读取进程的输出  

					CmUtilities.Log("暗号化の証明書バインドポート " + output);
				}
			}
			catch (Exception ex)
			{
				CmUtilities.Log("暗号化の証明書バインドポートにエラー " + ex);
			}
			finally
			{
				if (p != null)
					p.Close();
			}
			#endregion
		}

		/// <summary>
		/// 获取该证书已经存在的端口
		/// </summary>
		/// <param name="targetCertHash"></param>
		/// <returns></returns>
		private List<string> GetPortsBoundWithCert(string targetCertHash)
		{
			List<string> ports = new List<string>();
			ProcessStartInfo psi = new ProcessStartInfo
			{
				FileName = "netsh",
				Arguments = "http show sslcert",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};

			using (Process p = Process.Start(psi))
			{
				string output = p.StandardOutput.ReadToEnd();
				p.WaitForExit();

				string currentIpPort = null;
				string currentCertHash = null;

				var lines = output.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var line in lines)
				{
					string trimmed = line.Trim();

					// 1. 尝试提取 IP:Port，判断是否是 IP:port 格式
					// 一般形式是 0.0.0.0:数字 或 [::]:数字
					var colonMatch = Regex.Match(trimmed, @":\s*(\S+)$");
					if (colonMatch.Success)
					{
						string candidate = colonMatch.Groups[1].Value; // 0.0.0.0:44302
																	   // 提取端口号
						var portMatch = Regex.Match(candidate, @":(\d+)$");
						if (portMatch.Success)
						{
							string port = portMatch.Groups[1].Value;  // 端口号，如 "44302"
							currentIpPort = port;  // 只保存端口号
							continue;
						}
					}

					// 2. 尝试提取证书哈希，证书哈希是40位16进制字符串（SHA-1指纹）
					// 查找行里是否包含连续40位十六进制字符
					var hashMatch = System.Text.RegularExpressions.Regex.Match(trimmed, @"\b[a-fA-F0-9]{40}\b");
					if (hashMatch.Success)
					{
						currentCertHash = hashMatch.Value.ToUpper();

						// 如果都取到了，则判断并存储
						if (!string.IsNullOrEmpty(currentIpPort) && currentCertHash == targetCertHash)
						{
							ports.Add(currentIpPort);
							currentIpPort = null;
							currentCertHash = null;
						}
					}
				}
			}
			return ports;
		}
		#endregion

	}

	public class MyX509CertificateValidator : X509CertificateValidator
	{
		string allowedIssuerName;

		public MyX509CertificateValidator(string allowedIssuerName)
		{

			NCLogger.GetInstance().WriteDebugLog($"in MyX509CertificateValidator ,allowedIssuerName is {allowedIssuerName}");
			if (allowedIssuerName == null)
			{
				throw new ArgumentNullException("allowedIssuerName");
			}

			this.allowedIssuerName = allowedIssuerName;
		}

		public override void Validate(X509Certificate2 certificate)
		{
			
			// Check that there is a certificate.
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
				NCLogger.GetInstance().WriteDebugLog("certificate is null");
			}
			NCLogger.GetInstance().WriteDebugLog($"in MyX509CertificateValidator.Validate ,certificate is {certificate.SerialNumber.ToString()}");
			// Check that the certificate issuer matches the configured issuer.
			if (allowedIssuerName != certificate.IssuerName.Name)
			{
				throw new SecurityTokenValidationException
				  ("Certificate was not issued by a trusted issuer");
				NCLogger.GetInstance().WriteDebugLog("Certificate was not issued by a trusted issuer");

			}
		}
	}


}
