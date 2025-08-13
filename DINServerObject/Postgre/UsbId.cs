using System;

namespace DINServerObject
{
	public class UsbId
	{
		public UsbId() { }

		//キーＩＤ
		public string EUsb_Id { get; set; }

		//利用者
		public string UserName { get; set; }

		//メール
		public string Mail { get; set; }

		//会社名
		public string Company { get; set; }

		//携帯電話
		public string MobileTel { get; set; }

		//活動フラグ
		public int ActFlag { get; set; }

		//Key作成日
		public DateTime KeyPublisherDate { get; set; }

		//Key更新日
		public DateTime KeyUpdateDate { get; set; }

		//加工帳入力利用開始日
		public DateTime? UseStartDay { get; set; }
	

		//加工帳入力利用終了日
		public DateTime? UseEndDay { get; set; }

		//加工帳入力システム機能
		public string Functions { get; set; }

		//加工帳絵符利用開始日
		public DateTime? UseEFUStartDay { get; set; }

		//加工帳絵符利用終了日
		public DateTime? UseEFUEndDay { get; set; }

		//CADグレードコントロール
		public string DINCAD { get; set; }

		//CADバージョンコントロール
		public int CADVersion { get; set; }

		//DINCAD2ターゲットCAD
		public int CADTarget { get; set; }

		//CAD・加工帳オプション機能使用終了日
		public DateTime? CADOPUseEndDay { get; set; }

		//CAD・加工帳オプション機能
		public string CADOPFunctions { get; set; }

		//TPMシステム
		public int TMP { get; set; }

		//アイコーサブコンフラグ
		public int DINsubcon { get; set; }

		//備考
		public string Notes { get; set; }

		//自働更新
		public int UpdateFlg { get; set; }

	}
}
