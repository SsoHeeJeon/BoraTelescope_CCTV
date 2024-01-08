using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SunAPITest.Communication
{
    public class HttpRequest
    {
        private CancellationTokenSource _cancelSrc;
        private HttpWebResponse _webReswebRes;

        public event EventHandler<HttpRequestResponseArgs> Response;
        public event EventHandler<HttpRequestErrorArgs> Error;
        public event EventHandler ConnectionClosed;

        public bool CancelRequested
        {
            get => _cancelSrc.IsCancellationRequested;
        }
        public void Cancel()
        {
            _cancelSrc?.Cancel();

            if (_webReswebRes != null)
            {
                _webReswebRes.Close();
                _webReswebRes.Dispose();
                _webReswebRes = null;
            }
        }

        enum State
        {
            Preamble,
            Header,
            Body,
            Epilogue
        }

        public void Request(string method, string url, string id, string password, bool acceptJson = false)
        {
            Cancel();

            _cancelSrc = new CancellationTokenSource();

            bool retry = false;
            Uri uri = new Uri(url);
            HttpWebRequest req;
            DigestAuthFixer digestAuth = new DigestAuthFixer(uri.GetLeftPart(UriPartial.Authority), id, password);

            while (true)
            {
                req = WebRequest.CreateHttp(uri);
                req.Method = method;
                if (acceptJson) req.Accept = "application/json";

                if (retry && _webReswebRes != null)
                {
                    var wwwAuthenticateHeader = _webReswebRes.Headers["WWW-Authenticate"];
                    req.Headers.Add(HttpRequestHeader.Authorization, digestAuth.GetDigestHeader(uri.PathAndQuery, wwwAuthenticateHeader));

                    _webReswebRes.Close();
                    _webReswebRes = null;
                }

                try
                {
                    _webReswebRes = req.GetResponse() as HttpWebResponse;
                }
                catch (WebException e)
                {
                    _webReswebRes = e.Response as HttpWebResponse;
                    if (_webReswebRes == null)
                    {
                        RaiseErrorEvent(e.Message);
                        return;
                    }
                }

                // Try to fix a 401 exception by adding a Authorization header
                if (_webReswebRes.StatusCode == HttpStatusCode.Unauthorized && !retry)
                {
                    retry = true;
                    continue;
                }

                break;
            }

            bool bCloseRes = true;

            if (_webReswebRes.StatusCode != HttpStatusCode.OK)
            {
                RaiseErrorEvent("Request failed", _webReswebRes.StatusCode, ReadString(_webReswebRes), _webReswebRes.ContentType);
                if (bCloseRes)
                {
                    _webReswebRes.Close();
                    _webReswebRes.Dispose();
                }
                ConnectionClosed?.Invoke(this, EventArgs.Empty);
                return;
            }

            switch (_webReswebRes.ContentType)
            {
                case var _test when _test.IndexOf("text/plain", StringComparison.InvariantCultureIgnoreCase) != -1:
                    {
                        RaiseResponseEvent(ReadString(_webReswebRes), _webReswebRes.ContentType, string.Empty, _webReswebRes.StatusCode);
                        break;
                    }
                case var _test when _test.IndexOf("text/xml", StringComparison.InvariantCultureIgnoreCase) != -1:
                    {
                        RaiseResponseEvent(ReadString(_webReswebRes), _webReswebRes.ContentType, string.Empty, _webReswebRes.StatusCode);
                        break;
                    }
                case var _test when _test.IndexOf("application/json", StringComparison.InvariantCultureIgnoreCase) != -1:
                    {
                        RaiseResponseEvent(ReadString(_webReswebRes), _webReswebRes.ContentType, string.Empty, _webReswebRes.StatusCode);
                        break;
                    }
                case var _test when _test.IndexOf("image/jpeg", StringComparison.InvariantCultureIgnoreCase) != -1:
                    {
                        using (var stm = _webReswebRes.GetResponseStream())
                        {
                            var bytes = ReadBytes(stm, (int)_webReswebRes.ContentLength);
                            RaiseResponseEvent(bytes, _webReswebRes.ContentType, string.Empty, _webReswebRes.StatusCode);
                        }
                        break;
                    }
                case var _test when _test.IndexOf("multipart/x-mixed-replace", StringComparison.InvariantCultureIgnoreCase) != -1:
                    {
                        bCloseRes = false;
                        string boundary = _test.Substring(_test.IndexOf("boundary=", StringComparison.InvariantCultureIgnoreCase) + 9);
                        using (var stm = _webReswebRes.GetResponseStream())
                        {
                            StringBuilder response = new StringBuilder();
                            string subContentType = _webReswebRes.ContentType;
                            int subContentLength = 0;
                            State state = State.Preamble;

                            string boundaryStart = $"--{boundary}";
                            string boundaryEndofStream = $"{boundary}--";

                            while (!_cancelSrc.IsCancellationRequested)
                            {
                                if (stm.CanRead)
                                {
                                    try
                                    {
                                        string str = string.Empty;

                                        if (state != State.Body)
                                        {
                                            str = ReadLine(stm);
                                            response.AppendLine(str);
                                        }

                                        switch (state)
                                        {
                                            case State.Preamble:
                                                if (str.StartsWith(boundaryStart))
                                                {
                                                    state = State.Header;
                                                }
                                                else if (str.EndsWith(boundaryEndofStream))
                                                {
                                                    // It is end of multipart data and following next data is epilogue
                                                    state = State.Epilogue;
                                                }
                                                break;
                                            case State.Epilogue:
                                                // Ignore
                                                break;
                                            case State.Header:
                                                if (string.IsNullOrWhiteSpace(str))
                                                {
                                                    state = State.Body;
                                                }
                                                else if (str.StartsWith("Content-Type:", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    subContentType = str.Substring(13).Trim();
                                                }
                                                else if (str.StartsWith("Content-Length:", StringComparison.InvariantCultureIgnoreCase))
                                                {
                                                    subContentLength = int.Parse(str.Substring(15).Trim());
                                                }
                                                break;
                                            case State.Body:
                                                if (subContentLength > 0)
                                                {
                                                    var bytes = ReadBytes(stm, subContentLength);
                                                    RaiseResponseEvent(bytes, _webReswebRes.ContentType, subContentType, _webReswebRes.StatusCode);
                                                    state = State.Preamble;
                                                }
                                                else
                                                {
                                                    str = ReadLine(stm);
                                                    response.AppendLine(str);

                                                    if (str.StartsWith(boundaryStart))
                                                    {
                                                        RaiseResponseEvent(response.ToString(), _webReswebRes.ContentType, subContentType, _webReswebRes.StatusCode);
                                                        state = State.Header;
                                                    }
                                                    else if (str.EndsWith(boundaryEndofStream))
                                                    {
                                                        RaiseResponseEvent(response.ToString(), _webReswebRes.ContentType, subContentType, _webReswebRes.StatusCode);
                                                        // It is end of multipart data and following next data is epilogue
                                                        state = State.Epilogue;
                                                    }
                                                    else if (string.IsNullOrWhiteSpace(str))
                                                    {
                                                        RaiseResponseEvent(response.ToString(), _webReswebRes.ContentType, subContentType, _webReswebRes.StatusCode);
                                                        state = State.Preamble;
                                                    }
                                                }
                                                break;
                                            default:
                                                throw new ApplicationException($"Invalid state : {state}");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Trace.WriteLine(e.Message);
                                        if (!_cancelSrc.IsCancellationRequested)
                                        {
                                            RaiseErrorEvent(e.Message, _webReswebRes?.StatusCode, string.Empty, _webReswebRes?.ContentType);
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    Thread.Sleep(10);
                                }
                            }
                        }

                        break;
                    }
                default:
                    {
                        RaiseResponseEvent(ReadString(_webReswebRes), _webReswebRes.ContentType, string.Empty, _webReswebRes.StatusCode);
                        break;
                    }
            }

            if (bCloseRes)
            {
                _webReswebRes.Close();
                _webReswebRes.Dispose();
            }

            ConnectionClosed?.Invoke(this, EventArgs.Empty);
        }

        public string QueryRequest(string method, string url, string id, string password, bool acceptJson = false)
        {
            Cancel();

            _cancelSrc = new CancellationTokenSource();

            bool retry = false;
            Uri uri = new Uri(url);
            HttpWebRequest req;
            DigestAuthFixer digestAuth = new DigestAuthFixer(uri.GetLeftPart(UriPartial.Authority), id, password);

            while (true)
            {
                req = WebRequest.CreateHttp(uri);
                req.Method = method;
                if (acceptJson) req.Accept = "application/json";

                if (retry && _webReswebRes != null)
                {
                    var wwwAuthenticateHeader = _webReswebRes.Headers["WWW-Authenticate"];
                    req.Headers.Add(HttpRequestHeader.Authorization, digestAuth.GetDigestHeader(uri.PathAndQuery, wwwAuthenticateHeader));

                    _webReswebRes.Close();
                    _webReswebRes = null;
                }

                try
                {
                    _webReswebRes = req.GetResponse() as HttpWebResponse;
                }
                catch (WebException e)
                {
                    _webReswebRes = e.Response as HttpWebResponse;
                    if (_webReswebRes == null)
                    {
                        return "Failed";
                    }
                }

                // Try to fix a 401 exception by adding a Authorization header
                if (_webReswebRes.StatusCode == HttpStatusCode.Unauthorized && !retry)
                {
                    retry = true;
                    continue;
                }

                break;
            }

            bool bCloseRes = true;

            if (_webReswebRes.StatusCode != HttpStatusCode.OK)
            {
                if (bCloseRes)
                {
                    _webReswebRes.Close();
                    _webReswebRes.Dispose();
                }
                return "Failed";
            }

            string returnString = "";
            switch (_webReswebRes.ContentType)
            {
                case var _test when _test.IndexOf("text/plain", StringComparison.InvariantCultureIgnoreCase) != -1:
                    {
                        var tempString = ReadString(_webReswebRes);

                        returnString = tempString;
                        break;
                    }
                default:
                    {
                        returnString = "Failed";
                        break;
                    }
            }

            if (bCloseRes)
            {
                _webReswebRes.Close();
                _webReswebRes.Dispose();
            }

            return returnString;

        }

        private void RaiseResponseEvent(string res, string contentType, string subContentType, HttpStatusCode code)
        {
            Response?.Invoke(this, new HttpRequestTextResponseArgs(res, contentType, subContentType, code));
        }

        private void RaiseResponseEvent(byte[] res, string contentType, string subContentType, HttpStatusCode code)
        {
            Response?.Invoke(this, new HttpRequestBinaryResponseArgs(res, contentType, subContentType, code));
        }

        private void RaiseErrorEvent(string message, HttpStatusCode? code = null, string content = "", string contentType = "")
        {
            Error?.Invoke(this, new HttpRequestErrorArgs(message, code, content, contentType));
        }

        public static string ReadString(HttpWebResponse res)
        {
            using (var stm = res.GetResponseStream())
            {
                var sr = new StreamReader(stm);
                var readString = sr.ReadToEnd();
                return readString;
            }
        }

        public static string ReadLine(Stream stm)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                byte prev = 0;
                byte curr = 0;
                while ((read = stm.ReadByte()) != -1)
                {
                    prev = curr;
                    curr = (byte)read;

                    ms.WriteByte(curr);

                    if (prev == 0x0d && curr == 0x0a)
                    {
                        break;
                    }
                }
                return Encoding.Default.GetString(ms.ToArray()).TrimEnd(new char[] { '\r', '\n' });
            }
        }

        public static byte[] ReadAllBytes(Stream stm)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stm.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] ReadBytes(Stream stm, int size)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int totalRead = 0;
                int read;
                while ((read = stm.Read(buffer, 0, Math.Min(buffer.Length, size - totalRead))) > 0)
                {
                    totalRead += read;
                    ms.Write(buffer, 0, read);
                    if (size - totalRead == 0)
                    {
                        break;
                    }
                }
                return ms.ToArray();
            }
        }
    }

    public class HttpRequestTextResponseArgs : HttpRequestResponseArgs
    {
        public string Response { get; }
        public HttpRequestTextResponseArgs(string response, string contentType, string subContentType, HttpStatusCode statusCode)
            : base(contentType, subContentType, statusCode)
        {
            Response = response;
        }
    }

    public class HttpRequestBinaryResponseArgs : HttpRequestResponseArgs
    {
        public byte[] Response { get; }
        public HttpRequestBinaryResponseArgs(byte[] response, string contentType, string subContentType, HttpStatusCode statusCode)
            : base(contentType, subContentType, statusCode)
        {
            Response = response;
        }
    }

    public class HttpRequestResponseArgs : EventArgs
    {
        public string ContentType { get; }
        public string SubContentType { get; }
        public HttpStatusCode StatusCode { get; }
        public HttpRequestResponseArgs(string contentType, string subContentType, HttpStatusCode statusCode)
        {
            ContentType = contentType;
            SubContentType = subContentType;
            StatusCode = statusCode;
        }
    }

    public class HttpRequestErrorArgs : EventArgs
    {
        public string Content { get; }
        public string ContentType { get; }
        public HttpStatusCode? StatusCode { get; }
        public string ErrorMessage { get; }

        public HttpRequestErrorArgs(string message, HttpStatusCode? code = null, string content = "", string type = "")
        {
            StatusCode = code;
            ErrorMessage = message;
            Content = content;
            ContentType = type;
        }
    }
}
