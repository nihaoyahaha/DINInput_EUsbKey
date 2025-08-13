using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINInput_EUsbKey
{
	public class PostgreSqlConnectionConfig
	{
		public PostgreSqlConnectionConfig() { }

		public string Server { get; set; }
		public string Port { get; set; }
		public string UserId { get; set; }
		public string Password { get; set; }
		public string Database { get; set; }
	}

	
}
