using System;

using System.Text;


namespace Parsers.Parser
{
    class Parser
    {
        public string Post(string url, string data)
        {
            var req = System.Net.WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            var sentData = Encoding.GetEncoding(1251).GetBytes(data);
            req.ContentLength = sentData.Length;
            var sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            var res = req.GetResponse();
            var receiveStream = res.GetResponseStream();
            if (receiveStream != null)
            {
                var sr = new System.IO.StreamReader(receiveStream, Encoding.UTF8);
                //Кодировка указывается в зависимости от кодировки ответа сервера
                var read = new Char[256];
                var count = sr.Read(read, 0, 256);
                var Out = String.Empty;
                while (count > 0)
                {
                    var str = new String(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
                return Out;
            }
            return null;
        }

        public  string Get(string url, string data)
        {
            var req = System.Net.WebRequest.Create(url + "?" + data);
            var resp = req.GetResponse();
            var stream = resp.GetResponseStream();
            if (stream != null)
            {
                var sr = new System.IO.StreamReader(stream);
                var Out = sr.ReadToEnd();
                sr.Close();
                return Out;
            }
            return null;
        }
    }
}