using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anh.Translate
{
    public class ActionBase
    {
        public static readonly string UrlTranslateGoogleWebAPI = ConfigurationManager.AppSettings["UrlTransalteGoogleWebAPI"] !=null? ConfigurationManager.AppSettings["UrlTransalteGoogleWebAPI"].ToString():"https://translate.google.com.vn/";
        public static readonly string UrlPlayGoogleWebAPI = ConfigurationManager.AppSettings["UrlPlayGoogleWebAPI"] !=null? ConfigurationManager.AppSettings["UrlPlayGoogleWebAPI"].ToString(): "https://play.google.com/";
        public static readonly string UrlZenWebAPI = ConfigurationManager.AppSettings["UrlZenWebAPI"]!=null?ConfigurationManager.AppSettings["UrlZenWebAPI"].ToString(): "http://localhost/ZenWebAPI/api/";
        public static readonly string TKK = ConfigurationManager.AppSettings["TKK"] !=null?ConfigurationManager.AppSettings["TKK"].ToString(): "434288.3805658817";
		public static bool _bOutputPronunciation = string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("OutputPronunciation")) ? true : bool.Parse(ConfigurationManager.AppSettings.Get("OutputPronunciation"));

		public static string GetTk(string transateText)
        {
            /*
             javascript translate_m_vi.js
             function gt_ec(a){
                        var 
                       uo = function(a, b) {
                        for (var c = 0; c < b.length - 2; c += 3) {
                            var d = b.charAt(c + 2);
                            d = "a" <= d ? d.charCodeAt(0) - 87 : Number(d);
                            d = "+" == b.charAt(c + 1) ? a >>> d : a << d;
                            a = "+" == b.charAt(c) ? a + d & 4294967295 : a ^ d
                        }
                        return a
                    }
                      ,  to = function(a) {
                        return function() {
                            return a
                        }
                        }
                     , wo = function(a) {
                        if (null !== vo)
                            var b = vo;
                        else {
                            b = to(String.fromCharCode(84));
                            var c = to(String.fromCharCode(75));
                            b = [b(), b()];
                            b[1] = c();
                            b = (vo = window[b.join(c())] || "") || ""
                        }
                        var d = to(String.fromCharCode(116));
                        c = to(String.fromCharCode(107));
                        d = [d(), d()];
                        d[1] = c();
                        c = "&" + d.join("") + "=";
                        d = b.split(".");
                        b = Number(d[0]) || 0;
                        for (var e = [], f = 0, g = 0; g < a.length; g++) {
                            var h = a.charCodeAt(g);
                            128 > h ? e[f++] = h : (2048 > h ? e[f++] = h >> 6 | 192 : (55296 == (h & 64512) && g + 1 < a.length && 56320 == (a.charCodeAt(g + 1) & 64512) ? (h = 65536 + ((h & 1023) << 10) + (a.charCodeAt(++g) & 1023),
                            e[f++] = h >> 18 | 240,
                            e[f++] = h >> 12 & 63 | 128) : e[f++] = h >> 12 | 224,
                            e[f++] = h >> 6 & 63 | 128),
                            e[f++] = h & 63 | 128)
                        }
                        a = b;
                        for (f = 0; f < e.length; f++)
                            a += e[f],
                            a = uo(a, "+-a^+6");
                        a = uo(a, "+-3^+b+-f");
                        a ^= Number(d[1]) || 0;
                        0 > a && (a = (a & 2147483647) + 2147483648);
                        a %= 1E6;
                        return c + (a.toString() + "." + (a ^ b))
                    };
                    return wo(a);
                    }
             */
            string res = "";
            string vo = null;
            Func<string, string> tkk = (b) =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                //dic.Add("TKK", "434265.203221577");
                //dic.Add("TKK", "434266.1379349442");
                dic.Add("TKK", TKK);
                if (dic.ContainsKey(b))
                {
                    return dic[b];
                }
                return null;
            };
            Func<long, string, long> uo = (a, b) =>
              {
                  for (int c = 0; c < b.Length - 2; c += 3)
                  {
                      char d = b[c + 2];
                      int ld = 'a' <= d ? Convert.ToInt32(d) - 87 : Convert.ToInt32(d.ToString());
                      long md = '+' == b[c + 1] ? a >> ld : a << ld;
                      a = '+' == b[c] ? a + md & 4294967295 : a ^ md;
                  }
                  return a;
              };
            Func<long, string> sc = (a) =>
             {
                 return ((char)a).ToString();
             };
            Func<string, string> wo = (a) =>
             {
                 string b, c;
                 string[] bb = new string[2];
                 if (vo != null)
                 {
                     b = vo;
                 }
                 else
                 {
                     b = sc(84);
                     c = sc(75);
                     bb[0] = bb[1] = b;
                     bb[1] = c;
                     b = vo = tkk(string.Join(c, bb)) ?? "";
                 }
                 string d = sc(116);
                 c = sc(107);
                 string[] dd = new string[2];
                 dd[0] = dd[1] = d;
                 dd[1] = c;
                 c = "&" + string.Join("", dd) + "=";
                 dd = b.Split(new char[] { '.' });
                 long nb = 0;
                 if (!long.TryParse(dd[0], out nb))
                 {
                     nb = 0;
                 }
                 List<long> es;
                 int f;
                 int g;
                 for (es = new List<long>(), f = 0, g = 0; g < a.Length; g++)
                 {
                     long h = Convert.ToInt64(a[g]);
                     if (128 > h)
                     {
                         es.Add(h);
                         f++;
                     }
                     else if (2048 > h)
                     {
                         es.Add(h >> 6 | 192);
						 f++;
                     }
                     else if (55296 == (h & 64512) && g + 1 < a.Length && 56320 == (Convert.ToInt64(a[g + 1]) & 64512))
                     {
                         h = 65536 + ((h & 1023) << 10) + Convert.ToInt64(a[++g]) & 1023;
                         es.Add(h >> 18 | 240);
                         f++;
                         es.Add(h >> 12 & 63 | 128);
                         f++;
                     }
                     else
                     {
                         es.Add(h >> 12 | 224);
                         f++;
                         es.Add(h >> 6 & 63 | 128);
                         f++;
                         es.Add(h & 63 | 128);
                         f++;
                     }
                 }
                 long na = nb;
                 for (f = 0; f < es.Count; f++)
                 {
                     na += es[f];
                     na = uo(na, "+-a^+6");
                 }
                 na = uo(na, "+-3^+b+-f");
                 long oa = 0;
                 if (long.TryParse(dd[1], out oa))
                 {
                     na ^= oa;
                 }
                 else
                 {
                     na = 0;
                 }
                 if (0 > na)
                 {
                     na = (na & 2147483647) + 2147483648;
                 }
                 na %= 1000000;
                 return c + (na.ToString() + "." + (na ^ nb));
             };
            res = wo(transateText);
            return res;
        }
    }
}
