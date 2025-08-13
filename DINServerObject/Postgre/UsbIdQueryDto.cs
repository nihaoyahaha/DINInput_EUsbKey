using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINServerObject
{
	public class UsbIdQueryDto
	{
		//キーＩＤ
		public string KeyId { get; set; }
		//会社名
		public string Company { get; set; }
		//利用者名
		public string UserName { get; set; }
		//Key更新日_開始時間
		public DateTime? KeyUpdateDateFrom { get; set; }
		//Key更新日_終了時間
		public DateTime? KeyUpdateDateTo { get; set; }
		//加工帳利用終了日_開始時間
		public DateTime? UseEndDayFrom { get; set; }
		//加工帳利用終了日_終了時間
		public DateTime? UseEndDayTo { get; set; }
		//絵符オプション利用終了日_開始時間
		public DateTime? UseEFUEndDayFrom { get; set; }
		//絵符オプション利用終了日_終了時間
		public DateTime? UseEFUEndDayTo { get; set; }
	}
}
