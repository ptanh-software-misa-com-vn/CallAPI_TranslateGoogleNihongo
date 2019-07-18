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
        /// <summary>
        /// Translate Single Text
        /// </summary>
        /// <param name="tk_q"></param>
        /// <returns></returns>
        public async Task<string> GetSingle(string q, string sl = "ja", string tl = "en")
        {
            string tk = ActionF1.GetTk(q);
            string encodeQ = Uri.EscapeDataString(q);
            string tk_q = tk + "&q=" + encodeQ;
            string res = "";
            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    //specify to use TLS 1.2 as default connection
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    client.BaseAddress = new Uri(ActionBase.UrlTransalteGoogleWebAPI);
                    HttpRequestHeaders reqH = client.DefaultRequestHeaders;
                    Authen(ref reqH);
                    //HTTP GET
                    string path = "/translate_a/single?client=webapp&{0}=ja&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&source=bh&ssel=0&tsel=0&kc=1{2}";
                    path = string.Format(path, sl, tl, tk_q);
                    //reqH.Add("path", path);
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
            if (jj==null)
            {
                return "";
            }
            JToken nmt = jj.First;
            res = "";
            foreach (var item in nmt)
            {
                if (item.First != null)
                {
                    res += item.First.ToString();
                }
                else
                {
                    res += Environment.NewLine;
                }
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
            //reqH.Add("authority", "translate.google.com.vn");
            //reqH.Add("accept", "*/*");
            //reqH.Accept.Clear();
            //reqH.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //reqH.Add("scheme", "https");
            //reqH.Add("method", "GET");
            //reqH.Add("accept-encoding", "gzip, deflate, br");
            //reqH.Add("accept-language", "en-US,en;q=0.9,vi;q=0.8,fr-FR;q=0.7,fr;q=0.6");
            //reqH.Add("referer", "https://translate.google.com.vn/?hl=vi");
            string chrome = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36";
            string edge = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";
            reqH.Add("user-agent",chrome );
            reqH.GetCookies().Add(new CookieHeaderValue("NID", "188=XFYB2Lnp4nax9yrsHueGO_c5X5mbBYLo5L95ZRPwDB-dvpyttWrVYyabPHPoHv_ItYhmdUOwlR1vE3lFvG7BgzEMxNgjjFX6Uv9gHmz6bvK-IdzbeNeq3oz8b60ZMSEahO97uW3ws2kIzvHQRgR9l_Dl9afmTtzRr3q2IIRWJlU"));
            //reqH.Add("Cache-Control", new string[] { "no-cache, no-store, must-revalidate" });
            //reqH.Add("Pragma","no-cache");

            //reqH.Add("x-client-data", "CIq2yQEIo7bJAQjBtskBCNG3yQEIqZ3KAQioo8oBCLGnygEI4qjKAQigqcoBCPGpygEIl63KAQjNrcoB");
            //reqH.Add("x-client-data", "aaaCIq2yQEIo7bJAQjBtskBCNG3yQEIqZ3KAQioo8oBCLGnygEI4qjKAQigqcoBCPGpygEIl63KAQjNrcoB");
        }
    }
}
