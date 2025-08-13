using C1.Win.C1FlexGrid;
using DI.NCFrameWork;
using DINServerObject;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DINInput_EUsbKey
{
	public partial class MainForm : BaseForm
	{
		private string[] _strCols = new string[] { "状態", "Key_ID", "利用者名", "会社名","Key作成日", "Key更新日",
												   "加工帳", "加工帳","絵符オプション", "絵符オプション",
												   "DINCAD", "DINCAD","DINCAD",
												   "DINCAD_EX","備考" };

		private string[] _strNames = new string[] { "ActFlag", "EUSB_ID","UserName", "Company", "KeyPublisherDate", "KeyUpdateDate",
													"UseStartDay","UseEndDay","UseEFUStartDay","UseEFUEndDay",
													"DINCAD","CADTarget","CADVersion",
													"CADOPUseEndDay","Notes"};

		private int[] _colWidths = new int[] { 80,150,100,150,100,100,
									   100,100,100,100,
									   100,100,100,
		                               100,200};

		private TextAlignEnum[] _alignCols = new TextAlignEnum[] {
			TextAlignEnum.CenterCenter, TextAlignEnum.CenterCenter,
			TextAlignEnum.CenterCenter, TextAlignEnum.CenterCenter,
			TextAlignEnum.CenterCenter, TextAlignEnum.CenterCenter,
			TextAlignEnum.CenterCenter,TextAlignEnum.CenterCenter,
			TextAlignEnum.CenterCenter,TextAlignEnum.CenterCenter,
			TextAlignEnum.CenterCenter,TextAlignEnum.CenterCenter,
			TextAlignEnum.CenterCenter,TextAlignEnum.CenterCenter,
			TextAlignEnum.CenterCenter,};

		//クエリー条件
		private UsbIdQueryDto _queryCriteria = new UsbIdQueryDto();

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			InitfgMainHeader();
			SearchData();
		}

		/// <summary>
		/// テーブルヘッダーの初期化
		/// </summary>
		private void InitfgMainHeader()
		{
			try
			{
				fg_main.AllowMerging = AllowMergingEnum.Custom;
				//タイトルフォーマット設定
				SetDetailTitleFormat(_strCols, _strNames, _colWidths, _alignCols, fg_main);
				fg_main.Rows.DefaultSize = 20;
				fg_main.Rows.Fixed = 2;

				CellRange cr = fg_main.GetCellRange(0, 0, 1, 0);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(0, "ActFlag", "状態");

				cr = fg_main.GetCellRange(0, 1, 1, 1);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(0, "EUSB_ID", "Key_ID");

				cr = fg_main.GetCellRange(0, 2, 1, 2);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(0, "UserName", "利用者名");

				cr = fg_main.GetCellRange(0, 3, 1, 3);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(0, "Company", "会社名");

				cr = fg_main.GetCellRange(0, 4, 1, 4);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(0, "KeyPublisherDate", "Key作成日");

				cr = fg_main.GetCellRange(0, 5, 1, 5);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(0, "KeyUpdateDate", "Key更新日");

				cr = fg_main.GetCellRange(0, 6, 0, 7);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(1, "UseStartDay", "利用開始日");
				fg_main.SetData(1, "UseEndDay", "利用終了日");

				cr = fg_main.GetCellRange(0, 8, 0, 9);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(1, "UseEFUStartDay", "利用開始日");
				fg_main.SetData(1, "UseEFUEndDay", "利用終了日");

				cr = fg_main.GetCellRange(0, 10, 0, 12);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(1, "DINCAD", "Level");
				fg_main.SetData(1, "CADTarget", "TargetCAD");
				fg_main.SetData(1, "CADVersion", "TargetVer");

				fg_main.SetData(1, "CADOPUseEndDay", "利用終了日");

				cr = fg_main.GetCellRange(0, 14, 1, 14);
				fg_main.MergedRanges.Add(cr);
				fg_main.SetData(0, "Notes", "備考");


			}
			catch (Exception ex)
			{
				Log.WriteExceptionLog(ex);
			}
			finally
			{
				fg_main.Redraw = true;
			}
		}

		/// <summary>
		/// タイトルフォーマット設定
		/// </summary>
		/// <param name="strCols">列のヘッドテキストの配列</param>
		/// <param name="strNames">列の名前の配列</param>
		/// <param name="nWidths">列の幅の配列</param>
		/// <param name="alignCols">テキスト配置方法の配列</param>
		/// <param name="fg">グリッド</param>
		private void SetDetailTitleFormat(string[] strCols, string[] strNames, int[] nWidths,
			TextAlignEnum[] alignCols, C1FlexGrid fg)
		{
			fg.DrawMode = DrawModeEnum.Normal;
			fg.AllowEditing = true;
			fg.Cols.Count = strCols.Length;
			fg.Cols.Frozen = 0;
			for (int nCol = 0; nCol < strCols.Length; nCol++)
			{
				fg.Cols[nCol].Visible = true;
				fg.Cols[nCol].AllowEditing = false;
				fg.Cols[nCol].Name = strNames[nCol];
				fg.Cols[nCol].Width = nWidths[nCol];
				fg.Cols[nCol].Caption = strCols[nCol];
				fg.Cols[nCol].TextAlign = alignCols[nCol];
				fg.Cols[nCol].TextAlignFixed = TextAlignEnum.CenterCenter;
			}
		}

		/// <summary>
		/// 活動フラグの名前を取得
		/// </summary>
		/// <param name="actFlag">活動フラグcode</param>
		/// <returns></returns>
		private string GetActFlagName(int actFlag)
		{
			switch (actFlag)
			{
				case 1: return "活動中";
				case 2: return "故障";
				case 3: return "紛失";
				case 4: return "失効など";
				default: return "活動中";
			}
		}

		/// <summary>
		/// DINCADの名前を取得
		/// </summary>
		/// <param name="dincad">DINCADcode</param>
		/// <returns></returns>
		private string GetDINCADName(string dincad)
		{
			switch (dincad)
			{
				case "2": return "DINCAD30";
				case "4": return "DINCAD50";
				case "6": return "DINCAD100";
				default: return "";
			}
		}

		/// <summary>
		/// 検索
		/// </summary>
		private void SearchData()
		{
			//条件付きクエリー
			AddQueryCriteria();
			List<UsbId> usbIds = ServiceApi.GetEUsbList(_queryCriteria);
			fg_main.Rows.Count = 2;
			if (usbIds != null && usbIds.Count > 0)
			{
				int num = 2;
				fg_main.Rows.Count = usbIds.Count + 2;
				usbIds.ForEach(x =>
				{
					//状態
					string actFlag = GetActFlagName(x.ActFlag);
					fg_main.SetData(num, "ActFlag", actFlag);
					//Key_ID
					fg_main.SetData(num, "EUSB_ID", x.EUsb_Id);
					//利用者名
					fg_main.SetData(num, "UserName", x.UserName);
					//会社名
					fg_main.SetData(num, "Company", x.Company);
					//Key作成日
					fg_main.SetData(num, "KeyPublisherDate", x.KeyPublisherDate.ToString("yyyy/MM/dd"));
					//Key更新日
					fg_main.SetData(num, "KeyUpdateDate", x.KeyUpdateDate.ToString("yyyy/MM/dd"));
					//加工帳入力利用開始日
					fg_main.SetData(num, "UseStartDay", x.UseStartDay == null ? "" : x.UseStartDay.Value.ToString("yyyy/MM/dd"));
					//加工帳入力利用終了日
					fg_main.SetData(num, "UseEndDay", x.UseEndDay == null ? "" : x.UseEndDay.Value.ToString("yyyy/MM/dd"));
					//加工帳絵符利用開始日
					fg_main.SetData(num, "UseEFUStartDay", x.UseEFUStartDay.Value.ToString("yyyy/MM/dd"));
					//加工帳絵符利用終了日
					fg_main.SetData(num, "UseEFUEndDay", x.UseEFUEndDay.Value.ToString("yyyy/MM/dd"));
					//Level
					string dincadName = GetDINCADName(x.DINCAD);
					fg_main.SetData(num, "DINCAD", dincadName);
					//TargetCAD
					fg_main.SetData(num, "CADTarget", x.CADTarget);
					//CADVersion
					fg_main.SetData(num, "CADVersion", x.CADVersion);
					//利用終了日
					fg_main.SetData(num, "CADOPUseEndDay", x.CADOPUseEndDay.Value.ToString("yyyy/MM/dd"));
					//備考
					fg_main.SetData(num, "Notes", x.Notes);
					num++;
				});
			}
		}

		//クエリー条件の追加
		private void AddQueryCriteria()
		{
			_queryCriteria = new UsbIdQueryDto();
			//KeyID
			if (!string.IsNullOrEmpty(txt_keyID.Text.Trim()))
			{
				_queryCriteria.KeyId = txt_keyID.Text.Trim();
			}
			//会社名
			if (!string.IsNullOrEmpty(txt_Company.Text.Trim()))
			{
				_queryCriteria.Company = txt_Company.Text.Trim();
			}
			//利用者名
			if (!string.IsNullOrEmpty(txt_UserName.Text.Trim()))
			{
				_queryCriteria.UserName = txt_UserName.Text.Trim();
			}
			//Key更新日_開始時間
			if (date_KeyUpdateDateFrom.Checked)
			{
				_queryCriteria.KeyUpdateDateFrom = date_KeyUpdateDateFrom.Value;
			}
			//Key更新日_終了時間
			if (date_KeyUpdateDateTo.Checked)
			{
				_queryCriteria.KeyUpdateDateTo = date_KeyUpdateDateTo.Value;
			}
			//加工帳利用終了日_開始時間
			if (date_UseEndDayFrom.Checked)
			{
				_queryCriteria.UseEndDayFrom = date_UseEndDayFrom.Value;
			}
			//加工帳利用終了日_終了時間
			if (date_UseEndDayTo.Checked)
			{
				_queryCriteria.UseEndDayTo = date_UseEndDayTo.Value;
			}
			//絵符オプション利用終了日_開始時間
			if (date_UseEFUEndDayFrom.Checked)
			{
				_queryCriteria.UseEFUEndDayFrom = date_UseEFUEndDayFrom.Value;
			}
			//絵符オプション利用終了日_終了時間
			if (date_UseEFUEndDayTo.Checked)
			{
				_queryCriteria.UseEFUEndDayTo = date_UseEFUEndDayTo.Value;
			}
		}

		// ESubキーを初期化
		private bool InitKey()
		{
			//EUsbキーを存在するかどうかを判明する
			if (!NCSecurity.CheckKey())
			{
				ErrMsg = new ErrorMessage("CM99999");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			//ESubキーを初期化する
			bool initResult = NCSecurity.InitKey();
			if (!initResult)
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			//Excel加工帳入力システム(1024～1082)
			if (!NCSecurity.SetData(1024, new string(' ', 256)))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			//DINCAD(1535)
			if (!NCSecurity.SetData(1535, 1))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			//加工帳入力システム(1536～1791)
			if (!NCSecurity.SetData(1536, new string(' ', 256)))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			//TPM(1792)
			if (!NCSecurity.SetData(1536, new string(' ', 64)))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			ErrMsg = new ErrorMessage("CM00004");
			MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK);
			return true;
		}

		//初期化は実行かとうか判断
		public bool CheckInitialization()
		{
			byte nLen = 0;
			if (!NCSecurity.GetData(1024, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00005");
				if (DialogResult.Yes == MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION,
						  MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return InitKey();
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		//検索
		private void btn_search_Click(object sender, EventArgs e)
		{
			SearchData();
		}

		//Key認識
		private void btn_KeyRecognition_Click(object sender, EventArgs e)
		{	
			if (!NCSecurity.CheckKey())
			{
				ErrMsg = new ErrorMessage("CM99999");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			//EUsbキーのIDを取得する
			string eUsbId = NCSecurity.GetEUsbID();
			EUsb usb = new EUsb();
			usb.EUsbId = eUsbId;
			usb.IsReadOnly = true;
			usb.ShowDialog();
		}

		//Key初期化
		private void btn_KeyInitialization_Click(object sender, EventArgs e)
		{
			if (!NCSecurity.CheckKey())
			{
				ErrMsg = new ErrorMessage("CM99999");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string eUsbId = NCSecurity.GetEUsbID();
			ErrMsg = ServiceApi.CheckeUsbExists(eUsbId) ? new ErrorMessage("CM00002") : new ErrorMessage("CM00001");
			bool updateDbFlag = ErrMsg.ErrorCode == "CM00002";
			if (DialogResult.Yes == MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION,
					   MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				Cursor = Cursors.WaitCursor;
				if (updateDbFlag) ServiceApi.KeyInitialization(eUsbId);
				InitKey();
				Cursor = Cursors.Default;
			}
		}

		//Key作成
		private void btn_KeyCreate_Click(object sender, EventArgs e)
		{
			//EUsbキーのIDを取得する
			string eUsbId = NCSecurity.GetEUsbID();
			if (string.IsNullOrEmpty(eUsbId))
			{
				ErrMsg = new ErrorMessage("CM99999");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!CheckInitialization()) return;
			bool isKeyExist = ServiceApi.CheckeUsbExists(eUsbId);
			EUsb form = new EUsb();
			form.EUsbId = eUsbId;
			form.IsNew = !isKeyExist;
			form.CallParetnRefreshData += () => {
				int selectedRow = fg_main.Row;
				SearchData();
				fg_main.Row = selectedRow;
			};
			form.ShowDialog();
		}

		//Key更新
		private void btn_KeyUpdate_Click(object sender, EventArgs e)
		{
			if (fg_main.Rows.Count <= 1) return;
			if (fg_main.Row <= 0) fg_main.Row = 1;
			string selectedKeyId = PFunc.ObjectToString(fg_main.GetData(fg_main.Row, "EUSB_ID"));
			//USB挿入キー存在チェック 挿入している場合
			bool disabledFlg =false;
			string localKeyId = NCSecurity.GetEUsbID();
			//’存在しない場合:利用開始日と利用終了日が編集不可
			if (string.IsNullOrEmpty(localKeyId)) disabledFlg = true;
			//存在する場合 一覧選択されたUSBキと挿入している場合が不一致の場合:利用開始日と利用終了日が編集不可
			else if (selectedKeyId != localKeyId) disabledFlg = true;

			EUsb usb = new EUsb();
			usb.EUsbId = selectedKeyId;
			usb.IsNew = false;
			usb.DisabledFalg = disabledFlg;
			usb.CallParetnRefreshData += () => {
				int selectedRow = fg_main.Row;
				SearchData();
				fg_main.Row = selectedRow;
			};
			usb.ShowDialog();
		}
	
	}
}
