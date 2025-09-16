using DI.NCFrameWork;
using DINServerObject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DINInput_EUsbKey
{
	public partial class EUsb : BaseForm
	{
		/// <summary>
		/// 处理区分:true:新規作成, false:変更の編集
		/// </summary>
		public bool IsNew { get; set; }

		/// <summary>
		/// キーＩＤ
		/// </summary>
		public string EUsbId { get; set; }

		/// <summary>
		/// コントロールの有効化の設定
		/// </summary>
		public bool DisabledFalg { get; set; } = false;

		/// <summary>
		/// 表示データのみ編集不可
		/// </summary>
		public bool IsReadOnly { get; set; } = false;

		/// <summary>
		/// フォーム名
		/// </summary>
		public string FormText { get; set; }

		public event Action CallParetnRefreshData;

		public ParaConfig Config { get; set; }

		public EUsb()
		{
			InitializeComponent();
		}

		private void EUsb_Load(object sender, EventArgs e)
		{
			try
			{
				// フォーム名
				this.Text = FormText;

				InitCmbDinLevelItems();
				InitCmbDinCADItems();

				if (IsReadOnly)
				{
					ReadDataFromUsbKey();
					SetFormReadOnly();
				}
				else
				{
					InitControls();
				}
			}
			catch (Exception ex)
			{
				Log.WriteExceptionLog(ex);
			}
		}

		// usbkeyから設定を読み込む
		private void ReadDataFromUsbKey()
		{
			txt_KeyID.Text = EUsbId;
			UsbId usb = ServiceApi.GetEUsb(EUsbId);
			if (usb != null)
			{
				SetEUsbState(usb.ActFlag);
				txt_UserName.Text = usb.UserName;
				txt_Company.Text = usb.Company;
				txt_Mail.Text = usb.Mail;
				txt_Tel.Text = usb.MobileTel;
				txt_Notes.Text = usb.Notes;
			}

			byte nLen = 0;
			string strBuf = "";
			DateTime parsedDate = new DateTime();

			//加工帳入力利用開始日を読み取る
			if (!NCSecurity.GetData(1536, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!NCSecurity.GetData(1537, nLen, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (DateTime.TryParseExact(strBuf, "yyyyMMdd", null, DateTimeStyles.None, out parsedDate))
			{
				date_UseStartDay.Value = parsedDate;
			}
			else
			{
				date_UseStartDay.Visible = false;
			}

			nLen = 0;
			strBuf = "";
			parsedDate = new DateTime();
			//加工帳入力利用終了日を読み取る
			if (!NCSecurity.GetData(1556, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!NCSecurity.GetData(1557, nLen, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (DateTime.TryParseExact(strBuf, "yyyyMMdd", null, DateTimeStyles.None, out parsedDate))
			{
				date_UseEndDay.Value = parsedDate;
			}
			else
			{
				date_UseEndDay.Visible = false;
			}

			strBuf = "";
			//加工帳入力システム機能
			if (!NCSecurity.GetData(1567, 8, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			SetFunctions(strBuf);

			nLen = 0;
			strBuf = "";
			parsedDate = new DateTime();
			//加工帳絵符利用開始日を読み取る
			if (!NCSecurity.GetData(1575, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!NCSecurity.GetData(1576, nLen, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (DateTime.TryParseExact(strBuf, "yyyyMMdd", null, DateTimeStyles.None, out parsedDate))
			{
				date_UseEFStartDay.Value = parsedDate;
			}
			else
			{
				date_UseEFStartDay.Visible = false;
			}

			nLen = 0;
			strBuf = "";
			parsedDate = new DateTime();
			//加工帳絵符利用終了日を読み取る
			if (!NCSecurity.GetData(1595, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!NCSecurity.GetData(1596, nLen, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (DateTime.TryParseExact(strBuf, "yyyyMMdd", null, DateTimeStyles.None, out parsedDate))
			{
				date_UseEFEndDay.Value = parsedDate;
			}
			else
			{
				date_UseEFEndDay.Visible = false;
			}

			strBuf = "";
			//TPMシステム
			if (!NCSecurity.GetData(1792, 1, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			check_TPM.Checked = strBuf == "1" ? true : false;


			strBuf = "";
			//level
			if (!NCSecurity.GetData(1793, 1, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			SetLevel(strBuf);

			strBuf = "";
			//アイコーサブコンフラグ
			if (!NCSecurity.GetData(1594, 1, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			check_DINsubcon.Checked = strBuf == "1" ? true : false;

			nLen = 0;
			//CADバージョンコントロール
			if (!NCSecurity.GetData(1535, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (nLen != 255) txt_CADVersion.Text = nLen.ToString("D2");

			nLen = 0;
			//DINCAD2ターゲットCAD
			if (!NCSecurity.GetData(1797, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			//CAD
			SetCad(nLen.ToString() == "255" ? "-1" : nLen.ToString());

			nLen = 0;
			strBuf = "";
			parsedDate = new DateTime();
			//CAD・加工帳オプション機能使用終了日
			if (!NCSecurity.GetData(1083, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!NCSecurity.GetData(1084, nLen, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (DateTime.TryParseExact(strBuf, "yyyyMMdd", null, DateTimeStyles.None, out parsedDate))
			{
				date_CADOPUseEndDay.Value = parsedDate;
			}
			else
			{
				date_CADOPUseEndDay.Visible = false;
			}

			strBuf = "";
			//CAD・加工帳オプション機能
			if (!NCSecurity.GetData(1092, 10, ref strBuf))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			SetCADOPFunctions(strBuf);

			//DIN加工帳入力フラグ
			nLen = 0;
			if (!NCSecurity.GetData(1791, ref nLen))
			{
				ErrMsg = new ErrorMessage("CM00012");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			check_DINUseFlag.Checked = nLen == 1 ? true : false;
		}

		//フォームコントロールを読み取り専用に設定する
		private void SetFormReadOnly()
		{
			rdb_Active.Enabled = false;
			rdb_malfunction.Enabled = false;
			rdb_loss.Enabled = false;
			rdb_cancel.Enabled = false;
			txt_KeyID.Enabled = false;
			txt_Company.Enabled = false;
			txt_UserName.Enabled = false;
			txt_Mail.Enabled = false;
			txt_Tel.Enabled = false;
			date_UseStartDay.Enabled = false;
			date_UseEndDay.Enabled = false;
			check_CADGetInfo.Enabled = false;
			check_Weight.Enabled = false;
			check_HunchInput.Enabled = false;
			check_SpecialCalculation.Enabled = false;
			check_DatxRecovery.Enabled = false;
			check_QR.Enabled = false;
			check_CreateSumFile.Enabled = false;
			check_SymbolOptions.Enabled = false;
			date_UseEFStartDay.Enabled = false;
			date_UseEFEndDay.Enabled = false;
			cmb_Level.Enabled = false;
			cmb_Cad.Enabled = false;
			txt_CADVersion.Enabled = false;
			date_CADOPUseEndDay.Enabled = false;
			check_01.Enabled = false;
			check_02.Enabled = false;
			check_03.Enabled = false;
			check_04.Enabled = false;
			check_05.Enabled = false;
			check_06.Enabled = false;
			check_07.Enabled = false;
			check_08.Enabled = false;
			check_09.Enabled = false;
			check_10.Enabled = false;
			check_TPM.Enabled = false;
			check_DINsubcon.Enabled = false;
			txt_Notes.Enabled = false;
			btn_OK.Enabled = false;
			btn_UpdateKeyRelease.Enabled = false;
			check_DINUseFlag.Enabled = false;
		}

		//コントロールの初期化
		private void InitControls()
		{
			txt_KeyID.Text = EUsbId;
			if (DisabledFalg)
			{
				check_CADGetInfo.Checked = false;
				date_UseStartDay.Enabled = check_CADGetInfo.Checked;
				date_UseEndDay.Enabled = check_CADGetInfo.Checked;
				check_Weight.Checked = false;
				check_QR.Checked = false;
				check_SpecialCalculation.Checked = false;
				check_HunchInput.Checked = false;
				check_DatxRecovery.Checked = false;
				check_CreateSumFile.Checked = false;
				check_TPM.Checked = false;
				check_TPM.Enabled = false;
				check_SymbolOptions.Checked = false;
				date_UseEFStartDay.Enabled = check_SymbolOptions.Checked;
				date_UseEFEndDay.Enabled = check_SymbolOptions.Checked;
				check_DINsubcon.Checked = false;
			}
			if (IsNew)
			{
				date_UseStartDay.Enabled = check_CADGetInfo.Checked;
				date_UseEndDay.Enabled = check_CADGetInfo.Checked;
				check_Weight.Checked = true;
				check_HunchInput.Checked = true;
				check_SpecialCalculation.Checked = true;
				check_DatxRecovery.Checked = true;
				date_UseEFStartDay.Enabled = check_SymbolOptions.Checked;
				date_UseEFEndDay.Enabled = check_SymbolOptions.Checked;
				check_DINUseFlag.Checked = true;
			}
			else
			{
				LoadDataToControls();
			}
		}

		//DBから値を取得して、取得した値を画面にセットする
		private void LoadDataToControls()
		{
			UsbId usb = ServiceApi.GetEUsb(EUsbId);
			//活動フラグ
			SetEUsbState(usb.ActFlag);
			//会社名
			txt_Company.Text = usb.Company.Trim();
			//利用者名
			txt_UserName.Text = usb.UserName.Trim();
			//メール
			txt_Mail.Text = usb.Mail.Trim();
			//Tel
			txt_Tel.Text = usb.MobileTel.Trim();
			//DIN加工帳入力フラグ
			check_DINUseFlag.Checked = usb.DINUseFlag == 1 ? true : false;
			//加工帳入力利用開始日
			date_UseStartDay.Text = usb.UseStartDay == null ? null : usb.UseStartDay.Value.ToString("yyyy/MM/dd");
			//加工帳入力利用終了日
			date_UseEndDay.Text = usb.UseEndDay == null ? null : usb.UseEndDay.Value.ToString("yyyy/MM/dd");
			//加工帳入力システム機能の設定
			SetFunctions(usb.Functions);
			//加工帳絵符利用開始日
			date_UseEFStartDay.Text = usb.UseEFUStartDay == null ? null : usb.UseEFUStartDay.Value.ToString("yyyy/MM/dd");
			//加工帳絵符利用終了日
			date_UseEFEndDay.Text = usb.UseEFUEndDay == null ? null : usb.UseEFUEndDay.Value.ToString("yyyy/MM/dd");
			//Level
			SetLevel(usb.DINCAD);
			//CAD
			SetCad(usb.CADTarget.ToString());

			//CADバージョンコントロール
			txt_CADVersion.Text = usb.CADVersion.ToString("D2");

			//CAD・加工帳オプション機能使用終了日
			date_CADOPUseEndDay.Text = usb.CADOPUseEndDay == null ? null : usb.CADOPUseEndDay.Value.ToString("yyyy/MM/dd");
			//CAD・加工帳オプション機能
			SetCADOPFunctions(usb.CADOPFunctions);
			//TPMシステム
			check_TPM.Checked = usb.TMP == 0 ? false : true;
			//アイコーサブコンフラグ
			check_DINsubcon.Checked = usb.DINsubcon == 0 ? false : true;
			//備考
			txt_Notes.Text = usb.Notes;
			if (!check_DINUseFlag.Checked)
			{
				SetDINSystemControlReadOnly();
			}
		}

		//活動フラグの設定
		private void SetEUsbState(int actFlag)
		{
			switch (actFlag)
			{
				case 1:
					rdb_Active.Checked = true;
					break;
				case 2:
					rdb_malfunction.Checked = true;
					break;
				case 3:
					rdb_loss.Checked = true;
					break;
				case 4:
					rdb_cancel.Checked = true;
					break;
				default:
					rdb_Active.Checked = true;
					break;
			}
		}

		//活動フラグを取得
		private int GetEUsbState()
		{
			if (rdb_Active.Checked) return 1;
			else if (rdb_malfunction.Checked) return 2;
			else if (rdb_loss.Checked) return 3;
			else return 4;
		}

		//加工帳入力システム機能の設定
		private void SetFunctions(string functions)
		{
			if (string.IsNullOrEmpty(functions)) return;
			char[] arry = functions.ToCharArray();
			//CAD
			check_CADGetInfo.Checked = arry[0] == '1' ? true : false;
			date_UseStartDay.Enabled = check_CADGetInfo.Checked;
			date_UseEndDay.Enabled = check_CADGetInfo.Checked;
			//重量表　
			check_Weight.Checked = arry[1] == '1' ? true : false;
			//QR
			check_QR.Checked = arry[2] == '1' ? true : false;
			//特殊計算
			check_SpecialCalculation.Checked = arry[3] == '1' ? true : false;
			//ハンチ入力
			check_HunchInput.Checked = arry[4] == '1' ? true : false;
			//Datx復元
			check_DatxRecovery.Checked = arry[5] == '1' ? true : false;
			//SUMファイル
			check_CreateSumFile.Checked = arry[6] == '1' ? true : false;
			//加工帳絵符OP
			check_SymbolOptions.Checked = arry[7] == '1' ? true : false;
			date_UseEFStartDay.Enabled = check_SymbolOptions.Checked;
			date_UseEFEndDay.Enabled = check_SymbolOptions.Checked;
		}

		//加工帳入力システム機能を取得
		private string GetFunctions()
		{
			StringBuilder functions = new StringBuilder();
			//CAD
			functions.Append(check_CADGetInfo.Checked ? "1" : "0");
			//重量表　
			functions.Append(check_Weight.Checked ? "1" : "0");
			//QR
			functions.Append(check_QR.Checked ? "1" : "0");
			//特殊計算
			functions.Append(check_SpecialCalculation.Checked ? "1" : "0");
			//ハンチ入力
			functions.Append(check_HunchInput.Checked ? "1" : "0");
			//Datx復元
			functions.Append(check_DatxRecovery.Checked ? "1" : "0");
			//SUMファイル
			functions.Append(check_CreateSumFile.Checked ? "1" : "0");
			//加工帳絵符OP
			functions.Append(check_SymbolOptions.Checked ? "1" : "0");
			return functions.ToString();
		}

		//Levelグレードコントロールの設定
		private void SetLevel(string dinLevel)
		{
			int index = Config.Level.FindIndex(x => x.StartsWith(dinLevel + "="));
			cmb_Level.SelectedIndex = index;
		}

		//CADグレードコントロールの設定
		private void SetCad(string dinCad)
		{
			int index = Config.CAD.FindIndex(x => x.StartsWith(dinCad + "="));
			cmb_Cad.SelectedIndex = index;

		}

		//CAD・加工帳オプション機能の設定
		private void SetCADOPFunctions(string cadopFunctions)
		{
			if (string.IsNullOrEmpty(cadopFunctions)) return;
			char[] arry = cadopFunctions.ToCharArray();
			check_01.Checked = arry[0] == '1' ? true : false;
			check_02.Checked = arry[1] == '1' ? true : false;
			check_03.Checked = arry[2] == '1' ? true : false;
			check_04.Checked = arry[3] == '1' ? true : false;
			check_05.Checked = arry[4] == '1' ? true : false;
			check_06.Checked = arry[5] == '1' ? true : false;
			check_07.Checked = arry[6] == '1' ? true : false;
			check_08.Checked = arry[7] == '1' ? true : false;
			check_09.Checked = arry[8] == '1' ? true : false;
			check_10.Checked = arry[9] == '1' ? true : false;
		}

		//CAD・加工帳オプション機能を取得
		private string GetCADOPFunctions()
		{
			StringBuilder cadopFunctions = new StringBuilder();
			cadopFunctions.Append(check_01.Checked ? "1" : "0");
			cadopFunctions.Append(check_02.Checked ? "1" : "0");
			cadopFunctions.Append(check_03.Checked ? "1" : "0");
			cadopFunctions.Append(check_04.Checked ? "1" : "0");
			cadopFunctions.Append(check_05.Checked ? "1" : "0");
			cadopFunctions.Append(check_06.Checked ? "1" : "0");
			cadopFunctions.Append(check_07.Checked ? "1" : "0");
			cadopFunctions.Append(check_08.Checked ? "1" : "0");
			cadopFunctions.Append(check_09.Checked ? "1" : "0");
			cadopFunctions.Append(check_10.Checked ? "1" : "0");
			return cadopFunctions.ToString();
		}

		// Level
		private void InitCmbDinLevelItems()
		{
			foreach (string item in Config.Level)
			{
				cmb_Level.Items.Add(item.Split('=')[0]);
			}
		}
		// Cad
		private void InitCmbDinCADItems()
		{
			foreach (string item in Config.CAD)
			{
				cmb_Cad.Items.Add(item.Split('=')[0]);
			}
		}

		//データの保存
		private bool SaveData()
		{
			if (!ValidateBeforeSave()) return false;
			if (IsNew)
			{
				//EUsbを追加
				if (!AddEUSB()) return false;
			}
			else
			{
				ErrMsg = new ErrorMessage("CM00010");
				if (DialogResult.Yes == MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION,
						   MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					//EUsbを修正
					if (!UpdateEUSB()) return false;
				}
			}
			CallParetnRefreshData?.Invoke();
			return true;
		}

		//EUsbを追加
		private bool AddEUSB()
		{
			UsbId usb = CreateUsbIdFromForm();
			if (!WriteDataEUsb(usb)) { Cursor = Cursors.Default; return false; }
			if (!ServiceApi.AddEUsb(usb)) return false;
			ErrMsg = new ErrorMessage("CM00009");
			MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK);
			return true;
		}

		//EUsbを修正
		private bool UpdateEUSB()
		{
			UsbId usb = CreateUsbIdFromForm();
			if (!WriteDataEUsb(usb)) { Cursor = Cursors.Default; return false; }
			if (!ServiceApi.UpdateEUsb(usb)) return false;
			ErrMsg = new ErrorMessage("CM00011");
			MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK);
			return true;
		}

		//データの正当性の検証
		private bool ValidateBeforeSave()
		{
			//KeyIdが一致していることを確認します
			if (txt_KeyID.Text != NCSecurity.GetEUsbID())
			{
				ErrMsg = new ErrorMessage("CM00006");
				if (DialogResult.Yes != MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return false;
				}
			}
			//加工帳入力利用日チェック
			if (date_UseStartDay.Value > date_UseEndDay.Value)
			{
				ErrMsg = new ErrorMessage("CM00007");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				date_UseEndDay.Focus();
				return false;
			}
			//加工帳絵符利用日チェック
			if (check_SymbolOptions.Checked)
			{
				if (date_UseEFStartDay.Value > date_UseEFEndDay.Value)
				{
					ErrMsg = new ErrorMessage("CM00008");
					MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
					date_UseEFEndDay.Focus();
					return false;
				}
			}
			return true;
		}

		//UsbIdオブジェクトを作成し、フォームコントロールから値を取る
		private UsbId CreateUsbIdFromForm()
		{
			UsbId usb = new UsbId();
			//キーＩＤ
			usb.EUsb_Id = txt_KeyID.Text;
			//利用者
			usb.UserName = txt_UserName.Text.Trim();
			//メール
			usb.Mail = txt_Mail.Text.Trim();
			//会社名
			usb.Company = txt_Company.Text.Trim();
			//携帯電話
			usb.MobileTel = txt_Tel.Text.Trim();
			//活動フラグ
			usb.ActFlag = GetEUsbState();
			//DIN加工帳入力フラグ
			usb.DINUseFlag = check_DINUseFlag.Checked ? 1 : 0;
			//加工帳入力利用開始日
			usb.UseStartDay = date_UseStartDay.Value;
			//加工帳入力利用終了日
			usb.UseEndDay = date_UseEndDay.Value;
			//加工帳入力システム機能
			usb.Functions = GetFunctions();
			//加工帳絵符利用開始日
			usb.UseEFUStartDay = date_UseEFStartDay.Value;
			//加工帳絵符利用終了日
			usb.UseEFUEndDay = date_UseEFEndDay.Value;
			//CADグレードコントロール
			usb.DINCAD = string.IsNullOrEmpty(cmb_Level.Text) ? " " : cmb_Level.Text;
			//CADバージョンコントロール
			usb.CADVersion = string.IsNullOrEmpty(txt_CADVersion.Text) ? 00 : int.Parse(txt_CADVersion.Text);
			//DINCAD2ターゲットCAD
			usb.CADTarget = string.IsNullOrEmpty(cmb_Cad.Text) ? -1 : int.Parse(cmb_Cad.Text);
			//CAD・加工帳オプション機能使用終了日
			usb.CADOPUseEndDay = date_CADOPUseEndDay.Value;
			//CAD・加工帳オプション機能
			usb.CADOPFunctions = GetCADOPFunctions();
			//TPMシステム
			usb.TMP = check_TPM.Checked ? 1 : 0;
			//アイコーサブコンフラグ
			usb.DINsubcon = check_DINsubcon.Checked ? 1 : 0;
			//備考
			usb.Notes = txt_Notes.Text.Trim();
			return usb;
		}

		//データ書き込みEUsb
		private bool WriteDataEUsb(UsbId usb)
		{
			Cursor = Cursors.WaitCursor;
			//EUsbキーを存在するかどうかを判明する
			if (!NCSecurity.CheckKey())
			{
				ErrMsg = new ErrorMessage("CM99999");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳入力システム
			if (!NCSecurity.SetData(1536, new string(' ', 256)))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳入力利用開始日
			if (!NCSecurity.SetData(1536, 8))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!NCSecurity.SetData(1537, usb.UseStartDay.Value.ToString("yyyyMMdd")))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳入力最終使用日
			if (!NCSecurity.SetData(1546, 8))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!NCSecurity.SetData(1547, DateTime.Today.ToString("yyyyMMdd")))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳入力利用終了日
			if (!NCSecurity.SetData(1556, 8))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!NCSecurity.SetData(1557, usb.UseEndDay.Value.ToString("yyyyMMdd")))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳入力システム機能
			if (!NCSecurity.SetData(1567, usb.Functions))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//DIN加工帳入力フラグ(使用区分)
			if (!NCSecurity.SetData(1791, (byte)usb.DINUseFlag))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳絵符利用開始日
			if (!NCSecurity.SetData(1575, 8))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!NCSecurity.SetData(1576, usb.UseEFUStartDay.Value.ToString("yyyyMMdd")))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳絵符最終使用日
			if (!NCSecurity.SetData(1585, 8))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!NCSecurity.SetData(1586, DateTime.Today.ToString("yyyyMMdd")))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//加工帳絵符利用終了日
			if (!NCSecurity.SetData(1595, 8))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!NCSecurity.SetData(1596, usb.UseEFUEndDay.Value.ToString("yyyyMMdd")))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//TPMシステム
			if (!NCSecurity.SetData(1792, usb.TMP.ToString()))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//CADグレードコントロール
			if (!NCSecurity.SetData(1793, usb.DINCAD))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//アイコーサブコンフラグ
			if (!NCSecurity.SetData(1594, usb.DINsubcon.ToString()))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//CADバージョンコントロール
			if (!NCSecurity.SetData(1535, (byte)usb.CADVersion))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//DINCAD2ターゲットCAD
			if (!NCSecurity.SetData(1797, (byte)usb.CADTarget))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//CAD・加工帳オプション機能使用終了日
			if (!NCSecurity.SetData(1083, 8))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!NCSecurity.SetData(1084, usb.CADOPUseEndDay.Value.ToString("yyyyMMdd")))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			//CAD・加工帳オプション機能
			if (!NCSecurity.SetData(1092, usb.CADOPFunctions))
			{
				ErrMsg = new ErrorMessage("CM00003");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			Cursor = Cursors.Default;
			return true;
		}

		//キー 許可文字列の構築
		private string BuildLicenseDataString()
		{
			UsbId usb = CreateUsbIdFromForm();
			StringBuilder key = new StringBuilder();
			//DIN入力システム利用可
			key.Append($"{usb.DINUseFlag},");
			//キーＩＤ
			key.Append($"{usb.EUsb_Id},");
			//加工帳入力利用開始日
			key.Append($"{usb.UseStartDay.Value.ToString("yyyy/MM/dd")},");
			//加工帳入力利用終了日
			key.Append($"{usb.UseEndDay.Value.ToString("yyyy/MM/dd")},");
			//加工帳入力システム機能
			key.Append($"{usb.Functions},");
			//加工帳絵符利用開始日
			key.Append($"{usb.UseEFUStartDay.Value.ToString("yyyy/MM/dd")},");
			//加工帳絵符利用終了日
			key.Append($"{usb.UseEFUEndDay.Value.ToString("yyyy/MM/dd")},");
			//CADグレードコントロール
			key.Append($"{usb.DINCAD},");
			//アイコーサブコンフラグ
			key.Append($"{usb.DINsubcon},");
			//CADバージョンコントロール
			key.Append($"{usb.CADVersion},");
			//DINCAD2ターゲットCAD
			key.Append($"{(usb.CADTarget == -1 ? 255 : usb.CADTarget)},");
			//TPM
			//key.Append($"{usb.TMP},");
			//CAD・加工帳オプション機能使用終了日
			key.Append($"{usb.CADOPUseEndDay.Value.ToString("yyyy/MM/dd")},");
			//CAD・加工帳オプション機能
			key.Append($"{usb.CADOPFunctions}");

			return key.ToString();
		}

		//暗号化されたファイルの保存
		private void SaveCertFile(string fileName, string cert)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			saveFileDialog.DefaultExt = "cert";
			saveFileDialog.Filter = "認証ファイル(*.cert)|*";
			saveFileDialog.FileName = fileName;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = saveFileDialog.FileName;
				Encryption.EncryptToFile(cert, filePath);

				ErrMsg = new ErrorMessage("CM00013");
				MessageBox.Show(ErrMsg.ErrorMsg, MSG_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		//Level再描画
		private void cmb_Level_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			if (e.Index < 0) return;

			Rectangle rectangle = new Rectangle(2, e.Bounds.Top + 2,
					e.Bounds.Height, e.Bounds.Height - 4);
			System.Drawing.Color animalColor = new System.Drawing.Color();
			e.Graphics.FillRectangle(new SolidBrush(animalColor), rectangle);

			using (var brush = new SolidBrush(e.ForeColor))
			{
				e.Graphics.DrawString(Config.Level[e.Index], e.Font, brush, e.Bounds);
			}

			e.DrawFocusRectangle();
		}

		//キーボード入力を無効にする
		private void cmb_Level_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		//キーボード操作の禁止
		private void cmb_Level_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = true;
		}

		//checkedでenabled連動
		private void check_SymbolOptions_CheckedChanged(object sender, EventArgs e)
		{
			date_UseEFStartDay.Enabled = check_SymbolOptions.Checked;
			date_UseEFEndDay.Enabled = check_SymbolOptions.Checked;
		}

		//入力テキストを1桁の数字に制限する
		private void txt_CADTarget_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (char.IsControl(e.KeyChar))
				return;

			if (!char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
				return;
			}

			if (textBox.Text.Length >= 1 && textBox.SelectionLength == 0)
			{
				e.Handled = true;
			}
		}

		//テキスト入力処理
		private void txt_CADTarget_TextChanged(object sender, EventArgs e)
		{
			TextBox tb = sender as TextBox;
			if (tb.Text.Length > 1)
			{
				tb.Text = char.IsDigit(tb.Text[0]) ? tb.Text[0].ToString() : "";
				tb.SelectionStart = tb.Text.Length;
			}
		}

		//入力テキストを2桁に制限
		private void txt_CADVer_KeyPress(object sender, KeyPressEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (char.IsControl(e.KeyChar))
				return;

			if (!char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
				return;
			}

			if (textBox.Text.Length >= 2 && textBox.SelectionLength == 0)
			{
				e.Handled = true;
			}
		}

		//入力テキストを2桁に制限
		private void txt_CADVer_TextChanged(object sender, EventArgs e)
		{
			TextBox tb = sender as TextBox;

			if (tb.Text.Length > 2)
			{
				string firstTwo = tb.Text.Substring(0, 2);

				if (firstTwo.All(char.IsDigit))
				{
					tb.Text = firstTwo;
				}
				else if (char.IsDigit(firstTwo[0]))
				{
					tb.Text = firstTwo[0].ToString();
				}
				else
				{
					tb.Text = "";
				}

				tb.SelectionStart = tb.Text.Length;
			}
		}

		//実行
		private void btn_OK_Click(object sender, EventArgs e)
		{
			if (SaveData()) Close();
		}

		//更新キー発行
		private void btn_UpdateKeyRelease_Click(object sender, EventArgs e)
		{
			if (SaveData())
			{
				string strKeyAllow = BuildLicenseDataString();
				string fileName = $"{txt_KeyID.Text}_{DateTime.Now.ToString("yyyyMMdd")}.cert";
				//暗号化されたファイルの保存
				SaveCertFile(fileName, strKeyAllow);
			}
		}

		//閉じる
		private void btn_Close_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// CAD情報取込
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void check_CADGetInfo_CheckedChanged(object sender, EventArgs e)
		{
			date_UseStartDay.Enabled = check_CADGetInfo.Checked;
			date_UseEndDay.Enabled = check_CADGetInfo.Checked;
		}

		private void cmb_Cad_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			if (e.Index < 0) return;

			Rectangle rectangle = new Rectangle(2, e.Bounds.Top + 2,
					e.Bounds.Height, e.Bounds.Height - 4);
			System.Drawing.Color animalColor = new System.Drawing.Color();
			e.Graphics.FillRectangle(new SolidBrush(animalColor), rectangle);

			using (var brush = new SolidBrush(e.ForeColor))
			{
				e.Graphics.DrawString(Config.CAD[e.Index], e.Font, brush, e.Bounds);
			}

			e.DrawFocusRectangle();
		}

		private void cmb_Cad_KeyDown(object sender, KeyEventArgs e)
		{
			e.SuppressKeyPress = true;
		}

		private void cmb_Cad_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		private void check_DINUseFlag_CheckedChanged(object sender, EventArgs e)
		{
			if (!check_DINUseFlag.Checked)
			{
				SetDINSystemControlReadOnly();
			}
			else
			{
				SetDINSystemControlActive();
			}
		}

		private void SetDINSystemControlReadOnly()
		{
			date_UseStartDay.Enabled = false;
			date_UseEndDay.Enabled = false;
			check_CADGetInfo.Enabled = false;
			check_Weight.Enabled = false;
			check_HunchInput.Enabled = false;
			check_SpecialCalculation.Enabled = false;
			check_DatxRecovery.Enabled = false;
			check_QR.Enabled = false;
			check_CreateSumFile.Enabled = false;
			check_SymbolOptions.Enabled = false;
			date_UseEFStartDay.Enabled = false;
			date_UseEFEndDay.Enabled = false;
		}

		private void SetDINSystemControlActive()
		{
			check_CADGetInfo.Enabled = true;
			if (check_CADGetInfo.Checked)
			{
				date_UseStartDay.Enabled = true;
				date_UseEndDay.Enabled = true;
			}
			check_Weight.Enabled = true;
			check_HunchInput.Enabled = true;
			check_SpecialCalculation.Enabled = true;
			check_DatxRecovery.Enabled = true;
			check_QR.Enabled = true;
			check_CreateSumFile.Enabled = true;
			check_SymbolOptions.Enabled = true;
			if (check_SymbolOptions.Checked)
			{
				date_UseEFStartDay.Enabled = true;
				date_UseEFEndDay.Enabled = true;
			}
		}

	}
}
