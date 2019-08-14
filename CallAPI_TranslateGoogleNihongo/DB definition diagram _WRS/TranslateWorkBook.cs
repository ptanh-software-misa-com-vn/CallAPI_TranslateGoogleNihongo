using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetGear;
using System.Configuration;
using System.IO;
using Anh.Translate;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Anh.DB_definition_diagram__WRS
{
	public partial class TranslateWorkBook : Form
	{
		public int _iMaxRequest = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("MaxRequest")) ? 200 : int.Parse(ConfigurationManager.AppSettings.Get("MaxRequest"));
		public string _fromLang = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("SL")) ? "ja" : ConfigurationManager.AppSettings.Get("SL");
		public string _toLang = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("TL")) ? "en" : ConfigurationManager.AppSettings.Get("TL");
		public int _iMaxLenPerRequest = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("MaxLenPerRequest")) ? 1000 : int.Parse(ConfigurationManager.AppSettings.Get("MaxLenPerRequest"));
		public const string ecoEscapeBlank = "'";
		private Dictionary<string, string> _dicTableName;
		private string[] _arraySplitString = new string[] { "=", "＝", "||", "（+）", "(+)", "+", "-", "*", "/", " " };
		private bool _widthChange;
		public TranslateWorkBook()
		{
			_dicTableName = new Dictionary<string, string>();
			InitializeComponent();
			_widthChange = false;
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			if (txtExcelName.Text.Length == 0)
			{
				linkLabel1.Focus();
				return;
			}
			try
			{
				string sFileNameDBMapping = @".\DB定義図＿WRS.xlsx";
				//Excelファイル作成
				IWorkbook xlBookDB = SpreadsheetGear.Factory.GetWorkbook(sFileNameDBMapping);
				CreateDicTableName(xlBookDB);

				string sFileNameTarget = txtExcelName.Text;
				IWorkbook xlBookTarget = SpreadsheetGear.Factory.GetWorkbook(sFileNameTarget);
				string[] arraySheetName = ConfigurationManager.AppSettings.Get("ArraySheetConvert").Split(',');
				foreach (var xlXheetNm in arraySheetName)
				{
					ConvertSheet(xlBookTarget.Worksheets[xlXheetNm], xlBookDB);
				}
				string sFilePath = Path.GetDirectoryName(txtExcelName.Text) + @"\" + Path.GetFileNameWithoutExtension(sFileNameTarget) + "_" + DateTime.Now.ToString("yyyyMMdd") + Path.GetExtension(sFileNameTarget);

				//保存
				xlBookTarget.SaveAs(sFilePath, FileFormat.OpenXMLWorkbook);
				MessageBox.Show("Finished");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

		}

		private void CreateDicTableName(IWorkbook xlBookDB)
		{
			_dicTableName.Clear();
			string[] excludeSheet = new string[] { "テーブル一覧", "M044 仕入先別職種マスタ2","W081 見積テンプレート大工種ワーク", "W082 見積テンプレート小工種ワーク", "W084 見積テンプレート原価内訳ワーク"
				,"W305 ポータル申請・承認顧客物件","W308B ポータル当月成績（資金繰り係数）"
			};
			for (int i = 0; i < xlBookDB.Worksheets.Count; i++)
			{
				IWorksheet xlSheet = xlBookDB.Worksheets[i];
				if (xlSheet.Name.Length >= 5 && !excludeSheet.Contains(xlSheet.Name) && !xlSheet.Name.EndsWith("マスタ取込ログファイル"))
				{
					_dicTableName.Add(xlSheet.Name.Substring(0, 4).ToUpper() + ".", xlSheet.Name);
				}
			}
		}

		private IRange SearchSheet(IWorksheet xlSheet, string what)
		{
			IRange findedRange = xlSheet.Range.Find(what, null, FindLookIn.Values, LookAt.Whole, SearchOrder.ByColumns, SearchDirection.Next, true);
			return findedRange;
		}
		#region "convert"
		private void ConvertSheet(IWorksheet xlSheet, IWorkbook xlBookDB)
		{
			Dictionary<IRange, string[][]> dicS = ExtractDataSheet(xlSheet);
			IWorksheet newSheet = xlSheet.CopyAfter(xlSheet) as IWorksheet;
			newSheet.Name = xlSheet.Name + "_DB";
			OutputDataToSheet(newSheet, dicS, xlBookDB);
		}

		private void OutputDataToSheet(IWorksheet newSheet, Dictionary<IRange, string[][]> dicS, IWorkbook xlBookDB)
		{
			foreach (IRange whatIR in dicS.Keys)
			{
				string addre = whatIR.Address;
				string resVal = "";
				for (int i = 0; i < dicS[whatIR].Length; i++)
				{
					if (dicS[whatIR][i] != null)
					{
						string sheetNm = dicS[whatIR][i][0];
						string sheetCd = dicS[whatIR][i][1];
						string cellData = dicS[whatIR][i][2];
						string splitString = dicS[whatIR][i][3];
						if (sheetNm.Length > 0)
						{
							IRange findedIR = SearchSheet(xlBookDB.Worksheets[sheetNm], cellData);
							if (findedIR != null)
							{
								IRange mapIR = xlBookDB.Worksheets[sheetNm].Cells[findedIR.Row, findedIR.Column + 8];
								resVal = resVal + sheetCd + mapIR.Value.ToString() + splitString;
							}
							else
							{
								resVal = resVal + sheetCd + cellData + splitString;
							}



						}
						else
						{
							resVal = resVal + cellData + splitString;
						}

					}

				}
				newSheet.Cells[addre].Value = resVal;
			}
		}

		private bool IsDataReq(string value)
		{
			string[] arrayData = value.Split(_arraySplitString, StringSplitOptions.None);
			for (int i = 0; i < arrayData.Length; i++)
			{
				string ku = arrayData[i].Trim();
				if (ku.Length >= 5)
				{
					if (_dicTableName.ContainsKey(ku.Substring(0, 5).ToUpper()))
					{
						return true;
					}
				}
			}
			return false;
		}
		private Dictionary<IRange, string[][]> ExtractDataSheet(IWorksheet xlSheet)
		{
			Dictionary<IRange, string[][]> dicRa = new Dictionary<IRange, string[][]>();
			IRange rMax = xlSheet.UsedRange;
			for (int i = 0; i < rMax.Columns.ColumnCount; i++)
			{
				for (int j = 0; j < rMax.Rows.RowCount; j++)
				{
					IRange item = rMax.Cells[j, i];
					if (item.Value != null && item.Value.ToString().Length >= 5 && IsDataReq(item.Value.ToString()))
					{
						string[] arrayData = item.Value.ToString().Split(_arraySplitString, StringSplitOptions.None);
						string curPosOffset = "";
						string[][] arrayRes = new string[arrayData.Length][];
						for (int k = 0; k < arrayData.Length; k++)
						{
							curPosOffset = curPosOffset + arrayData[k];
							string ku = arrayData[k].Trim();
							if (ku.Length >= 5)
							{
								string sheetCd = ku.Substring(0, 5).ToUpper();
								if (_dicTableName.ContainsKey(sheetCd))
								{
									string sheetName = _dicTableName[sheetCd];
									arrayRes[k] = new string[] { sheetName, sheetCd, CleanInput(ku.Substring(5)), "" };

								}
								else
								{
									arrayRes[k] = new string[] { "", "", ku, "" };
								}
							}
							else
							{
								arrayRes[k] = new string[] { "", "", ku, "" };
							}
							foreach (string spl in _arraySplitString)
							{
								if (curPosOffset.Length + spl.Length <= item.Value.ToString().Length && item.Value.ToString().Substring(0, curPosOffset.Length + spl.Length).Equals(curPosOffset + spl))
								{
									curPosOffset = curPosOffset + spl;
									arrayRes[k][3] = spl;
									break;
								}
							}
						}
						dicRa.Add(item, arrayRes);
					}
				}
			}
			return dicRa;
		}

		private string CleanInput(string v)
		{
			return v.Replace("ｺｰﾄﾞ", "コード");
		}
		#endregion

		#region Translate
		private async void CreateTranslateSheet(IWorksheet xlSheet)
		{
			List<IRange> arRange = ExtractTranslateDataSheet(xlSheet);
			IWorksheet newSheet = xlSheet.CopyAfter(xlSheet) as IWorksheet;
			newSheet.Name = xlSheet.Name + "_EN";
			await TranslateSheet(newSheet, arRange);
		}

		private List<IRange> ExtractTranslateDataSheet(IWorksheet xlSheet)
		{
			List<IRange> arRange = new List<IRange>();
			IRange rMax = xlSheet.UsedRange;
			for (int i = 0; i < rMax.Columns.ColumnCount; i++)
			{
				for (int j = 0; j < rMax.Rows.RowCount; j++)
				{
					IRange item = rMax.Cells[j, i];
					object v = item.Value;

					if (v != null)
					{
						arRange.Add(item);
					}
				}
			}

			return arRange;
		}

		private async Task TranslateSheet(IWorksheet newSheet, List<IRange> arRange)
		{
			ActionF1 ActionF1 = new ActionF1();
			var ienum = arRange.AsEnumerable();
			TaskScheduler tsc = TaskScheduler.Current;
			int i = 0;
			IRange whatIR = null;
			for (i = 0; i < arRange.Count; i++)
			{
				whatIR = arRange[i];
				string originalText = whatIR.Value.ToString();
				if (originalText.Length > 2500)
				{
					newSheet.Range[whatIR.Address].Value = whatIR.Value;
					continue;
				}
				JArray res = await ActionF1.GetSingle(originalText);

				string transateText = ActionF1.ReadJArrayRes(res).Aggregate((m, n) => m + n);
				newSheet.Range[whatIR.Address].Value = transateText;
			}
		}
		#endregion

		#region Translate
		private async Task<int> CreateTranslateSheet2(IWorksheet xlSheet, int numPrevRequest)
		{
			List<IRange> arRange = ExtractTranslateDataSheet2(xlSheet);
			IWorksheet newSheet = xlSheet.CopyAfter(xlSheet) as IWorksheet;
			newSheet.Name = xlSheet.Name + "_EN";
			int i = await TranslateSheet2(newSheet, arRange, numPrevRequest);
			return i;
		}

		private List<IRange> ExtractTranslateDataSheet2(IWorksheet xlSheet)
		{
			int step = 0;
			if (!int.TryParse(ConfigurationManager.AppSettings.Get("RowRange"), out step)) step = 10;
			List<IRange> arRange = new List<IRange>();
			IRange rMax = xlSheet.UsedRange;
			int mC = rMax.Columns.ColumnCount;
			int mR = rMax.Rows.RowCount;
			//5000 max tring google can translate per request (i will send request has maxlen enough small)
			for (int j = 0; j < mC; j++)
			{
				IRange item = null;
				int lenIR = 0;
				int i = 0, ist = 0;
				while (i < mR)
				{
					ist = i;
					while (lenIR < _iMaxLenPerRequest)
					{
						if (i >= mR)
						{
							break;
						}
						item = rMax.Cells[i, j, i++, j];
						lenIR = GetLength(lenIR, item);
					}
					if (lenIR > _iMaxLenPerRequest)
					{
						arRange.Add(rMax.Cells[ist, j, i - 1, j]);
						lenIR = 0;
					}
					else if (lenIR > 0)
					{
						arRange.Add(rMax.Cells[ist, j, i, j]);
					}
				}
			}

			return arRange;
		}

		private int GetLength(int offLen, IRange item)
		{
			object v = item.Value;
			if (v == null)
			{
				return offLen;
			}
			return offLen + v.ToString().Length;
		}

		private async Task<int> TranslateSheet2(IWorksheet newSheet, List<IRange> arRange, int numPrevRequest)
		{
			ActionF1 ActionF1 = new ActionF1();
			var ienum = arRange.AsEnumerable();
			TaskScheduler tsc = TaskScheduler.Current;
			int i = 0;
			IRange whatIR = null;
			int limit = arRange.Count;

			limit = Math.Min(limit, _iMaxRequest - numPrevRequest);
			for (i = 0; i < limit; i++)
			{
				whatIR = arRange[i];
				//DataTable orgTa = whatIR.GetDataTable(SpreadsheetGear.Data.GetDataFlags.NoColumnHeaders); //error convert type double (set columntype = type of first cell
				DataTable orgTa = GetTableFromIrange(whatIR);
				string originalText = GetTextFromTable(orgTa);
				if (originalText.Length > 5000)
				{
					continue;
				}
				JArray jarr = await ActionF1.GetSingle(originalText, _fromLang, _toLang);
				toolStripProgressBar1.Value = (int)((i + 1) * 100 / limit);
				Thread.Sleep(480);
				List<string> transateText = ActionF1.ReadJArrayRes(jarr);
				////fake start
				//JArray jarr = await FakeTrans(originalText);
				//List<string> transateText = null;
				////fake end
				DataTable traTa = GetTableFromText(transateText, orgTa);
				//translate sucess
				if (traTa != null)
				{
					for (int im = 0; im < traTa.Rows.Count; im++)
					{
						object vv = traTa.Rows[im][0];
						if (vv != null && vv.ToString().Trim().Length > 0)
						{
							newSheet.Range[whatIR.Address].Cells[im, 0].Value = vv;
						}
					}
				}
				else
				{
					//remain originalText
				}
			}
			return limit + numPrevRequest;
		}

		private async Task<JArray> FakeTrans(string t)
		{
			Task<JArray> tt = Task.Run(() =>
			{
				return new JArray();
			});
			var m = await tt;
			return m;
		}


		private string GetTextFromTable(DataTable orgTa)
		{
			if (orgTa == null)
			{
				return null;
			}
			StringBuilder sb = new StringBuilder();
			List<string> l = orgTa.AsEnumerable().Select((r) => r[0].ToString()).ToList();
			for (int i = 0; i < l.Count; i++)
			{
				if (l[i].Trim() == "")
				{
					l[i] = ecoEscapeBlank;
				}

				if (l[i].Contains("\n"))
				{
					if (l[i].EndsWith("\n"))
					{
						l[i] = l[i] + ecoEscapeBlank;
					}
					sb.Append("（（" + l[i].Trim().Trim('\n') + "））\n");
				}
				else
				{
					sb.Append(l[i].Trim().Trim('\n') + "\n");
				}
			}
			string t = sb.ToString();
			return t;
		}

		private DataTable GetTableFromIrange(IRange range)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Column1", typeof(string));
			object[,] arr = range.Value as object[,];
			if (arr != null)
			{
				foreach (var item in arr)
				{
					if (item != null)
					{
						dt.Rows.Add(item.ToString().TrimStart());
					}
					else
					{
						dt.Rows.Add("");
					}
				}
			}
			dt.AcceptChanges();
			return dt;
		}

		private DataTable GetTableFromText(List<string> orgtex, DataTable orgTa)
		{
			if (orgtex == null || orgtex.Count == 0)
			{
				return null;
			}
			DataTable dt = new DataTable();
			dt.Columns.Add(orgTa.Columns[0].ColumnName, typeof(string));
			foreach (DataRow item in orgTa.Rows)
			{
				dt.Rows.Add(item[0]);
			}
			int iLen = Math.Min(orgtex.Count, dt.Rows.Count);
			for (int i = 0; i < iLen; i++)
			{
				dt.Rows[i][0] = orgtex[i];
			}
			dt.AcceptChanges();
			return dt;
		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			openFileDialog1.Filter = "*.xls|*.xlsx";
			DialogResult res = openFileDialog1.ShowDialog();
			if (res == DialogResult.OK)
			{
				txtExcelName.Text = openFileDialog1.FileName;
				string sFileNameTarget = txtExcelName.Text;
				IWorkbook xlBookTarget = SpreadsheetGear.Factory.GetWorkbook(sFileNameTarget);
				string[] arraySheetName = ConfigurationManager.AppSettings.Get("ArraySheetConvert").Split(',');
				int cSheet = xlBookTarget.Worksheets.Count;
				cbSheetName.Items.Clear();
				cbSheetName.ResetText();
				numF.Value = 1;
				numT.Value = 1;
				numF.Maximum = cSheet;
				numT.Maximum = cSheet;
				foreach (IWorksheet xlXheet in xlBookTarget.Worksheets)
				{
					cbSheetName.Items.Add(xlXheet.Name);
				}
				_widthChange = true;
			}
		}

		private void btnTranslate_Click(object sender, EventArgs e)
		{
			if (txtExcelName.Text.Length == 0)
			{
				linkLabel1.Focus();
				return;
			}
			if (cbSheetName.SelectedValue == null)
			{
				cbSheetName.Focus();
				return;
			}
			try
			{
				//Excelファイル作成

				string sFileNameTarget = txtExcelName.Text;
				IWorkbook xlBookTarget = SpreadsheetGear.Factory.GetWorkbook(sFileNameTarget);
				string[] arraySheetName = ConfigurationManager.AppSettings.Get("ArraySheetConvert").Split(',');
				foreach (var xlXheetNm in arraySheetName)
				{
					if (xlBookTarget.Worksheets[xlXheetNm] != null)
					{
						CreateTranslateSheet(xlBookTarget.Worksheets[xlXheetNm]);
					}
				}
				string sFilePath = Path.GetDirectoryName(txtExcelName.Text) + @"\" + Path.GetFileNameWithoutExtension(sFileNameTarget) + "_" + DateTime.Now.ToString("yyyyMMdd") + Path.GetExtension(sFileNameTarget);

				//保存
				xlBookTarget.SaveAs(sFilePath, FileFormat.OpenXMLWorkbook);
				MessageBox.Show("Finished");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private async void btnPre_Click(object sender, EventArgs e)
		{
			if (txtExcelName.Text.Length == 0)
			{
				linkLabel1.Focus();
				return;
			}
			if (rbSelectSheet.Checked && cbSheetName.SelectedItem == null)
			{
				cbSheetName.Focus();
				return;
			}
			try
			{
				//Excelファイル作成

				string sFileNameTarget = txtExcelName.Text;
				IWorkbook xlBookTarget = SpreadsheetGear.Factory.GetWorkbook(sFileNameTarget);
				int cc = 0;
				if (rbSelectSheet.Checked)
				{
					string xlXheetNm = cbSheetName.SelectedItem.ToString();
					if (xlBookTarget.Worksheets[xlXheetNm] != null)
					{
						toolStripProgressBar1.Value = 1;
						toolStripStatusLabel1.Text = "1/1 Sheet.";
						toolStripStatusLabel2.Text = string.Format("{0} requests.", cc);
						cc = await CreateTranslateSheet2(xlBookTarget.Worksheets[xlXheetNm], 0);
						toolStripStatusLabel2.Text = string.Format("{0} requests.", cc);
					}

				}

				if (rbRangeSheet.Checked)
				{
					int startSheet = (int)numF.Value;
					int endSheet = (int)numT.Value < startSheet ? startSheet : (int)numT.Value;
					int iNumSheet = endSheet - startSheet + 1;
					List<string> sheetNames = new List<string>();
					for (int im = 0; im < xlBookTarget.Worksheets.Count; im++)
					{
						if (im >= startSheet - 1 && im <= endSheet - 1)
						{
							sheetNames.Add(xlBookTarget.Worksheets[im].Name);
						}

					}
					int iS = 0;
					foreach (IWorksheet xlXheetNm in xlBookTarget.Worksheets)
					{
						if (sheetNames.IndexOf(xlXheetNm.Name) < 0)
						{
							continue;
						}
						iS++;
						if (cc < 0 || cc >= _iMaxRequest)
						{
							break;
						}
						toolStripProgressBar1.Value = 1;
						toolStripStatusLabel1.Text = string.Format("{0}/{1} Sheet.", iS, iNumSheet);
						toolStripStatusLabel2.Text = string.Format("{0} requests.", cc);
						cc = await CreateTranslateSheet2(xlXheetNm, cc);
						Thread.Sleep(1000 + 100 * iS);
						toolStripStatusLabel2.Text = string.Format("{0} requests.", cc);
					}
				}

				if (rbAllSheet.Checked)
				{
					int iNumSheet = xlBookTarget.Worksheets.Count;
					int iS = 0;
					foreach (IWorksheet xlXheetNm in xlBookTarget.Worksheets)
					{
						iS++;
						if (cc < 0 || cc >= _iMaxRequest)
						{
							break;
						}
						toolStripProgressBar1.Value = 1;
						toolStripStatusLabel1.Text = string.Format("{0}/{1} Sheet.", iS, iNumSheet);
						toolStripStatusLabel2.Text = string.Format("{0} requests.", cc);
						cc = await CreateTranslateSheet2(xlXheetNm, cc);
						Thread.Sleep(1000 + 50 * iS);
						toolStripStatusLabel2.Text = string.Format("{0} requests.", cc);
					}
				}
				string sFilePath = Path.GetDirectoryName(txtExcelName.Text) + @"\" + Path.GetFileNameWithoutExtension(sFileNameTarget) + "_" + DateTime.Now.ToString("yyyyMMdd") + Path.GetExtension(sFileNameTarget);
				bool bW = Helper.CanReadFile(sFilePath);
				if (!bW)
				{
					bW = MessageBox.Show(sFilePath + " is open. Should you close before continue ?", "!ݲ", MessageBoxButtons.YesNo) == DialogResult.Yes;
					if (bW)
					{
						int pp = 10;
						while (!(bW = Helper.CanReadFile(sFilePath)) && pp > 0)
						{
							pp--;
							Thread.Sleep(5000);
						}
					}
				}
				if (bW)
				{
					//保存
					xlBookTarget.SaveAs(sFilePath, FileFormat.OpenXMLWorkbook);
				}
				MessageBox.Show("Finished");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void cbSheetName_DropDown(object sender, EventArgs e)
		{
			if (!_widthChange)
			{
				return;
			}
			else
			{
				_widthChange = false;
			}
			ComboBox senderComboBox = (ComboBox)sender;
			int width = senderComboBox.DropDownWidth;
			Graphics g = senderComboBox.CreateGraphics();
			Font font = senderComboBox.Font;
			int vertScrollBarWidth =
				(senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
				? SystemInformation.VerticalScrollBarWidth : 0;

			int newWidth;
			foreach (string s in ((ComboBox)sender).Items)
			{
				newWidth = (int)g.MeasureString(s, font).Width
					+ vertScrollBarWidth;
				if (width < newWidth)
				{
					width = newWidth;
				}
			}
			senderComboBox.DropDownWidth = width;
		}

		private void rbAllSheet_CheckedChanged(object sender, EventArgs e)
		{
			cbSheetName.Enabled = false;
			numF.Enabled = false;
			numT.Enabled = false;
		}

		private void rbSelectSheet_CheckedChanged(object sender, EventArgs e)
		{
			cbSheetName.Enabled = true;
			numF.Enabled = false;
			numT.Enabled = false;
		}

		private void rbRangeSheet_CheckedChanged(object sender, EventArgs e)
		{
			cbSheetName.Enabled = false;
			numF.Enabled = true;
			numT.Enabled = true;
		}

	}

	internal static class Helper
	{
		const int ERROR_SHARING_VIOLATION = 32;
		const int ERROR_LOCK_VIOLATION = 33;

		public static bool IsFileLocked(Exception exception)
		{
			int errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(exception) & ((1 << 16) - 1);
			return errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION;
		}

		public static bool CanReadFile(string filePath)
		{
			//Try-Catch so we dont crash the program and can check the exception
			try
			{
				//The "using" is important because FileStream implements IDisposable and
				//"using" will avoid a heap exhaustion situation when too many handles  
				//are left undisposed.
				using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
				{
					if (fileStream != null) fileStream.Close();  //This line is me being overly cautious, fileStream will never be null unless an exception occurs... and I know the "using" does it but its helpful to be explicit - especially when we encounter errors - at least for me anyway!
				}
			}
			catch (IOException ex)
			{
				//THE FUNKY MAGIC - TO SEE IF THIS FILE REALLY IS LOCKED!!!
				if (IsFileLocked(ex))
				{
					// do something, eg File.Copy or present the user with a MsgBox - I do not recommend Killing the process that is locking the file
					return false;
				}
			}
			finally
			{ }
			return true;
		}
	}
}
