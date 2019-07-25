using Anh.BeginUnicode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Anh.Translate
{
	public partial class Form1 : Form
	{
		public ActionF1 AcF1 { get; set; }
		public Form1()
		{
			AcF1 = new ActionF1();
			InitializeComponent();
		}

		private async void btnTranslateCJKUnifiedIdeographs_Click(object sender, EventArgs e)
		{
			UnicodeData[] array1D = null;

			try
			{
				array1D = await LoadUniCode("cjk-unified-ideographs");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			string res = await GETTranslateAPI(array1D);
			textBox2.Text = res;
		}

		private async  Task<string> GETTranslateAPI_Input()
		{
			string res = "";
			string s = textBox1.Text.Trim();
			//in javascript \r\n = \n
			//string s1 = string.Join(((char)10).ToString(), s.Split(new char[] { (char)10, (char)13 },StringSplitOptions.RemoveEmptyEntries));
			string s1 = s;
			JArray jj = await AcF1.GetSingle(s);

			return res;
		}

		private async Task<string> GETTranslateAPI(UnicodeData[] array1D)
		{
			int iLen = array1D.Length;
			iLen = 3;
			StringBuilder sb = new StringBuilder();
			StringBuilder sb1 = new StringBuilder();
			for (int i = 0; i < iLen; i++)
			{
				UnicodeData d = array1D[i];
				int c = d.DataCode;
				string s = ((char)c).ToString();
				sb.AppendLine(s);
				if (i%500==0 || i == iLen - 1)
				{
					sb1.Append( await AcF1.GetSingle(sb.ToString()));
					sb.Clear();
				}
			}

			return sb1.ToString(); ;
		}

		private async Task<UnicodeData[]> LoadUniCode(string unicodeRegion)
		{
			string fileName = string.Format(@".\{0}.xml", unicodeRegion);
			UnicodeData[] array1D = await XmlUnicodeRegion.Main(fileName, false);
			return array1D;
		}

		private async Task<string[][]> TEST_ZENAPI()
		{
			string[][] res = new string[1][];
			string encodeQ = "10000";
			HttpResponseMessage rmes = await AcF1.GetTEST(encodeQ);
			var readTask = rmes.Content.ReadAsAsync<string>();
			readTask.Wait();
			res[0] = new string[] { readTask.Result, readTask.Result };
			return res;
		}

		private async void btnTEST_Click(object sender, EventArgs e)
		{
			string res = await GETTranslateAPI_Input();
			textBox2.Text = res;
		}

		private void btnGetMulti_Click(object sender, EventArgs e)
		{
			//string[][] res = await TEST_GETTranslatesAPI();
			//MessageBox.Show(res[0][0]);
			string tk = ActionF1.GetTk("月曜日");
			MessageBox.Show(tk);
		}
	}
}