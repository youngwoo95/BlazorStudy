using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace YLManager.Web
{
    public class WebControl
    {
        /// <summary>
        /// GET REQUEST 전송
        /// </summary>
        /// <param name="url">GET 기능 전송할 URL</param>
        /// <param name="ReturnValue">RETURN 받을지 여부</param>
        /// <returns></returns>
        public static string RequestURL(string url, bool ReturnValue)
        {
            try
            {
                string message = string.Empty;

                // 매개변수로 들어온 GET전송 보낼 URL으로 Uri 객체 생성
                Uri uri = new Uri(url);
                HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(uri);
                wReq.Method = "GET";

                // 보내고 RETURN 받을지 여부
                if (ReturnValue) 
                {
                    // 응답 객체 생성
                    using (HttpWebResponse wRes = (HttpWebResponse)wReq.GetResponse())
                    {
                        Stream respPostStream = wRes.GetResponseStream();
                        
                        // StreamReader 객체 생성
                        StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("UTF-8"), true);

                        if(readerPost != null)
                        {
                            // 처음부터 끝까지 읽는다.
                            message = readerPost.ReadToEnd(); 
                        }
                        
                        // StreamReader 객체 해제
                        readerPost.Close(); 

                        return message;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.ToString();
            }
        }

    }

}
