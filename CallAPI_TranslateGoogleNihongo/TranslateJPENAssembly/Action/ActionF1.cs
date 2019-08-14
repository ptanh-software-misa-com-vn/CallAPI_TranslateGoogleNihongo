using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Anh.Translate
{
	public class ActionF1 : ActionBase
	{
		public const string ecoEscapeBlank = "'";
		/// <summary>
		/// Translate Single Text
		/// </summary>
		/// <param name="tk_q"></param>
		/// <returns></returns>
		public async Task<JArray> GetSingle(string q, string sl = "ja", string tl = "en")
		{
			string tk = ActionF1.GetTk(q);
			string encodeQ = Uri.EscapeDataString(q);
			string tk_q = tk + "&q=" + encodeQ;
			string res = "";
			HttpResponseMessage response = null;
			try
			{
				//HttpClientHandler handler = new HttpClientHandler()
				//{
				//    Proxy = new WebProxy("http://192.168.0.12"),
				//    UseProxy = true,
				//};
				//using (var client = new HttpClient(handler))
				using (var client = new HttpClient())
				{
					//specify to use TLS 1.2 as default connection
					System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
					client.BaseAddress = new Uri(ActionBase.UrlTranslateGoogleWebAPI);
					HttpRequestHeaders reqH = client.DefaultRequestHeaders;
					Authen(ref reqH);
					reqH.Add(HttpRequestHeader.Cookie.ToString(), "NID=188=" + RenCookie());
					//HTTP GET
					string[] paths = new string[] { "/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&otf=2&ssel=0&tsel=0&kc=1{2}",
													"/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&otf=2&ssel=3&tsel=3&kc=4{2}",
													"/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&otf=2&ssel=3&tsel=3&kc=1{2}",
													"/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&otf=1&ssel=3&tsel=3&kc=5{2}",
													"/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&otf=2&ssel=3&tsel=3&kc=6{2}",
													"/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&otf=2&ssel=3&tsel=3&kc=2{2}",
													"/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&otf=1&ssel=3&tsel=3&kc=7{2}"};
					//Random r = new Random();
					//int i = r.Next(0, 7);
					string path = string.Format(paths[0], sl, tl, tk_q);
					response = await client.GetAsync(path);
				}
			}
			catch (Exception ex)
			{
				Debug.Fail("エラー: {0}", ex.ToString());
			}
			finally
			{
			}
			//try
			//{
			//    response.EnsureSuccessStatusCode();
			//    // Handle success
			//    res = await response.Content.ReadAsStringAsync();
			//}
			//catch (HttpRequestException ex)
			//{
			//    throw ex;
			//    // Handle failure
			//}
			if (response.IsSuccessStatusCode)
			{
				res = await response.Content.ReadAsStringAsync();
			}
			JArray jj = JsonConvert.DeserializeObject(res) as JArray;

			return jj;
		}

		public string RenCookie()
		{
			Random r = new Random();
			int i = r.Next(0, 1);
			return cookies[i];
		}

		static string[] cookies = new string[]
		{
			"188=XFYB2Lnp4nax9yrsHueGO_c5X5mbBYLo5L95ZRPwDB-dvpyttWrVYyabPHPoHv_ItYhmdUOwlR1vE3lFvG7BgzEMxNgjjFX6Uv9gHmz6bvK-IdzbeNeq3oz8b60ZMSEahO97uW3ws2kIzvHQRgR9l_Dl9afmTtzRr3q2IIRWJlU",
		};
		private async Task<string> LogGetSingle(string q)
		{

			string res = "";
			HttpResponseMessage response = null;
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(ActionBase.UrlPlayGoogleWebAPI);
					HttpRequestHeaders reqH = client.DefaultRequestHeaders;
					Authen(ref reqH);
					//client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
					//client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
					//HTTP POST
					string path = "/log?format=json&hasfast=true&authuser=0";
					JArray data = GetLogData(q);
					//var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
					//var byteContent = new ByteArrayContent(buffer);
					//byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
					StringContent ct = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
					response = await client.PostAsync(path, ct);
					//var Tresponse = client.PostAsJsonAsync<JArray>(path, data);
				}
			}
			catch (Exception ex)
			{
				Debug.Fail("エラー: {0}", ex.ToString());
			}
			finally
			{
			}

			if (response.IsSuccessStatusCode)
			{
				res = await response.Content.ReadAsStringAsync();
			}

			return res;
		}

		public async Task<HttpResponseMessage> GetTEST(string encodeQ)
		{
			HttpResponseMessage res = null;
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(ActionBase.UrlZenWebAPI);
					HttpRequestHeaders reqH = client.DefaultRequestHeaders;
					//Authen(ref reqH);
					//HTTP GET
					string path = "Kokyaku/Get新規登録番号?asBumonCd={0}";
					path = string.Format(path, encodeQ);
					res = await client.GetAsync(path);
				}
			}
			catch (Exception ex)
			{
				Debug.Fail("エラー: {0}", ex.ToString());
			}
			finally
			{
			}
			return res;
		}
		private void Authen(ref HttpRequestHeaders reqH)
		{
			reqH.Add("authority", "translate.google.com.vn");
			//reqH.Add("accept", "*/*");
			//reqH.Accept.Clear();
			//reqH.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			//reqH.Add("scheme", "https");
			//reqH.Add("method", "GET");
			//reqH.Add("accept-encoding", "gzip, deflate, br");
			//reqH.Add("accept-language", "en-US,en;q=0.9,vi;q=0.8,fr-FR;q=0.7,fr;q=0.6");
			//reqH.Add("referer", "https://translate.google.com.vn/?hl=vi");
			string chrome = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36";
			//string edge = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";
			reqH.Add("user-agent", chrome);
			//reqH.GetCookies().Add(new CookieHeaderValue("NID", "188=XFYB2Lnp4nax9yrsHueGO_c5X5mbBYLo5L95ZRPwDB-dvpyttWrVYyabPHPoHv_ItYhmdUOwlR1vE3lFvG7BgzEMxNgjjFX6Uv9gHmz6bvK-IdzbeNeq3oz8b60ZMSEahO97uW3ws2kIzvHQRgR9l_Dl9afmTtzRr3q2IIRWJlU"));
			//reqH.Add("Cache-Control", new string[] { "no-cache, no-store, must-revalidate" });
			//reqH.Add("Pragma","no-cache");
			//reqH.Add("x-client-data", "CIq2yQEIo7bJAQjBtskBCNG3yQEIqZ3KAQioo8oBCLGnygEI4qjKAQigqcoBCPGpygEIl63KAQjNrcoB");
			//reqH.Add("x-client-data", "aaaCIq2yQEIo7bJAQjBtskBCNG3yQEIqZ3KAQioo8oBCLGnygEI4qjKAQigqcoBCPGpygEIl63KAQjNrcoB");
		}


		public JArray GetLogData(string q)
		{
			JArray j_0 = new JArray(1, null, null, null, null, null, null, null, null, null, new JArray(null, null, null, null, "vi"));
			string j_2_0_0 = GetTime().ToString();
			JArray j_2_0_7 = new JArray("en", null, null, null, null, null, null, null, null, null, null, null, null, "", null, "ja", null, null, null, null, null, null, null, null, null, new JArray(), null, null, null, null, 1, 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "vi", null, "日本", 0, null, null, null, null, null, null, null, null, null, null, null, 2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, new JArray());
			j_2_0_7[51] = q;
			JArray j_2_0 = new JArray(j_2_0_0, null, new JArray(), null, null, null, null, JsonConvert.SerializeObject(j_2_0_7), null, null, null, null, null, null, null, new JArray(null, new JArray(), null, JsonConvert.SerializeObject(new JArray(new JArray(), new JArray(), new JArray(17259, 15700023, 15700186, 15700190, 15700256, 15700259, 15700262), new JArray()))), null, null, null, new JArray(), 18, null, null, null, null, null, new JArray());
			j_2_0[20] = q.Length;
			JArray j_2 = new JArray();
			j_2.Add(j_2_0);
			string j_3 = (GetTime() + 368).ToString();
			JArray j = new JArray(j_0, 375, j_2, j_3, new JArray(), null, null, null, null, null, null, null, null, 0);

			return j;
		}

		private Int64 GetTime()
		{
			Int64 retval = 0;
			DateTime st = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
			retval = (Int64)t.TotalMilliseconds;
			return retval;
		}

		public List<string> ReadJArrayRes(JArray jj)
		{
			const string startCellCode = "((";
			const string endCellCode = "))";
			if (jj == null)
			{
				return null;
			}
			List<string> li = new List<string>();
			bool bStartCell = false;
			bool bEndCell = false;
			JToken j_0 = jj[0];
			StringBuilder sb = new StringBuilder();
			int iLen = j_0.Count();
			for (int i = 0; i < iLen; i++)
			{
				JToken item = j_0[i];
				if (item != null)
				{
					if (item[0] != null)
					{
						string j_0_0 = item[0].ToString();
						if (!bStartCell && j_0_0.StartsWith(startCellCode))
						{
							bStartCell = true;
							j_0_0 = j_0_0.Substring(2, j_0_0.Length - 2);
						}
						if (!bStartCell)
						{
							if (j_0_0.EndsWith("\n"))
							{
								j_0_0 = j_0_0.Substring(0, j_0_0.Length - 1);
								if (j_0_0 == ecoEscapeBlank)
								{
									j_0_0 = "";
								}
								string[] rowpp = j_0_0.Split(new string[] { "\n" }, StringSplitOptions.None);
								rowpp[0] = rowpp[0] + sb.ToString();
								li.AddRange(rowpp);
								sb.Clear();
							}
							else
							{
								sb.Append(j_0_0);
							}

						}
						else
						{
							if (j_0_0.EndsWith("\n"))
							{
								j_0_0 = j_0_0.Substring(0, j_0_0.Length - 1);
							}

							if (bStartCell && j_0_0.EndsWith(endCellCode))
							{
								bEndCell = true;
								j_0_0 = j_0_0.Substring(0, j_0_0.Length - 2);
								if (j_0_0 == ecoEscapeBlank)
								{
									j_0_0 = "";
								}
							}
							else
							{
								if (j_0_0 == ecoEscapeBlank)
								{
									j_0_0 = "";
								}
								sb.AppendLine(j_0_0);
							}


							if (bEndCell)
							{
								string[] rowpp = j_0_0.Split(new string[] { "\n" }, StringSplitOptions.None);
								rowpp[0] = rowpp[0] + sb.ToString();
								li.AddRange(rowpp);
								sb.Clear();
								bStartCell = false;
								bEndCell = false;
							}
						}
					}

				}
			}

			return li;
		}
	}
}
