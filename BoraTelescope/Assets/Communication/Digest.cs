using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// refer to https://stackoverflow.com/a/13500822
//
// Usage:
// DigestAuthFixer digest = new DigestAuthFixer(url, username, password);
// string strReturn = digest.GrabResponse(url);

namespace SunAPITest.Communication
{
    public class DigestAuthFixer
    {
        private static string _host;
        private static string _user;
        private static string _password;
        private static string _realm;
        private static string _nonce;
        private static string _qop;
        private static string _cnonce;
        private static DateTime _cnonceDate;
        private static int _nc;

        public DigestAuthFixer(string host, string user, string password)
        {
            // TODO: Complete member initialization
            _host = host;
            _user = user;
            _password = password;
        }

        private static string CalculateMd5Hash(string input)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = MD5.Create().ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private static string GrabHeaderVar(
            string varName,
            string header)
        {
            var regHeader = new Regex($@"{varName}=""([^""]*)""");
            var matchHeader = regHeader.Match(header);
            if (matchHeader.Success)
                return matchHeader.Groups[1].Value;
            throw new ApplicationException($"Header {varName} not found");
        }

        private string GetDigestHeader(string dir)
        {
            _nc = _nc + 1;

            var ha1 = CalculateMd5Hash($"{_user}:{_realm}:{_password}");
            var ha2 = CalculateMd5Hash(string.Format("{0}:{1}", "GET", dir));
            var digestResponse =
                CalculateMd5Hash(string.Format("{0}:{1}:{2:00000000}:{3}:{4}:{5}", ha1, _nonce, _nc, _cnonce, _qop, ha2));

            return string.Format("Digest username=\"{0}\", realm=\"{1}\", nonce=\"{2}\", uri=\"{3}\", " +
                "algorithm=MD5, response=\"{4}\", qop={5}, nc={6:00000000}, cnonce=\"{7}\"",
                _user, _realm, _nonce, dir, digestResponse, _qop, _nc, _cnonce);
        }

        public string GetDigestHeader(string dir, string wwwAuthenticateHeader)
        {
            if (string.IsNullOrEmpty(wwwAuthenticateHeader))
            {
                throw new ArgumentException("The wwwAuthenticateHeader parameter is required if the authentication header cannot be reused.");
            }

            _realm = GrabHeaderVar("realm", wwwAuthenticateHeader);
            _nonce = GrabHeaderVar("nonce", wwwAuthenticateHeader);
            _qop = GrabHeaderVar("qop", wwwAuthenticateHeader);

            _nc = 0;
            _cnonce = new Random().Next(123400, 9999999).ToString();
            _cnonceDate = DateTime.Now;

            return GetDigestHeader(dir);
        }
    }
}
