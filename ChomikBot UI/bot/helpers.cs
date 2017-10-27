using ChomikBot_UI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChomikBot_Framework {
    /// <summary>
    /// Misc methods utilized by others
    /// </summary>
    class helpers {

        /// <summary>
        /// Validates email format
        /// </summary>
        /// <param name="email">input to verify</param>
        /// <returns>indicates whether email is valid</returns>
        public static bool IsValidEmail(string email) {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Prints &#9608; char (i) times
        /// </summary>
        /// <param name="i">How much boxes to print</param>
        /// <returns>boxes as string</returns>
        public static string gBox(int i) {
            string boxes = "";
            for (int count = 1; count <= i; count++) {
                boxes += "█";
            }
            return boxes;
        }

        /// <summary>
        /// Fetch token used to verify most of actions performed on website
        /// </summary>
        /// <param name="resource">resource adress from which we want to fetch token</param>
        /// <returns>verification token as string</returns>
        public static string fetchToken(string resource) {
            main.client.CookieContainer = main._cook;

            try {
                var getToken = new RestRequest(resource, Method.GET);

                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(main.client.Execute(getToken).Content);

                HtmlAgilityPack.HtmlNode token = htmlDocument.DocumentNode.SelectSingleNode("//input[@name='__RequestVerificationToken']");

                return token.GetAttributeValue("value", "NULL");
            } catch {
                return "NULL";
            }
        }

        /// <summary>
        /// Fetch internal ID of given user
        /// </summary>
        /// <param name="username">nickname to fetch id for</param>
        /// <returns>userid as string</returns>
        public static string getUserID(string username) {
            var id = new RestRequest(username, Method.GET);
            string result = main.client.Execute(id).Content;

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(result);

            HtmlAgilityPack.HtmlNode name = htmlDocument.DocumentNode.SelectSingleNode("//input[@name='__accno']");
            return name.GetAttributeValue("value", "NULL");
        }

        /// <summary>
        /// Fetch random polish name
        /// </summary>
        /// <param name="firstNameOnly">removes surname from response</param>
        /// <param name="female">what sex should name correspond</param>
        /// <returns>name as string</returns>
        public static string randomName(bool firstNameOnly = true, bool female = false) {
            var client = new RestClient("http://behindthename.com");

            string gender = "m";
            if (female == true) {
                gender = "f";
            }

            var name = new RestRequest("/random/random.php?number=2&gender=" + gender + "&surname=&all=no&usage_pol=1", Method.GET);
            string result = client.Execute(name).Content;

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(result);

            string fullname = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='heavyhuge']").InnerText;
            fullname = fullname.Replace("&#322;", "l");
            fullname = fullname.Replace("&#281;", "e");
            fullname = fullname.Replace("&#346;", "s");
            fullname = fullname.Replace("&#380;", "z");
            fullname = fullname.Replace("&#321;", "l");
            
            if (firstNameOnly == true) {
                return fullname.Split(' ')[1].Replace(" ", "");
            }

            return fullname.Replace(" ", "");
        }

        /// <summary>
        /// Validate .ini settings
        /// </summary>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool validateSettings() {
            var ini = new iniHandler();
            bool failed = false;

            if (!ini.KeyExists("chomik_username", "CREDENTIALS")) {
                ini.Write("chomik_username", "", "CREDENTIALS");
                failed = true;
            }

            if (!ini.KeyExists("chomik_password", "CREDENTIALS")) {
                ini.Write("chomik_password", "", "CREDENTIALS");
                failed = true;
            }

            if (!ini.KeyExists("dbc_username", "CREDENTIALS")) {
                ini.Write("dbc_username", "", "CREDENTIALS");
                failed = true;
            }

            if (!ini.KeyExists("dbc_password", "CREDENTIALS")) {
                ini.Write("dbc_password", "", "CREDENTIALS");
                failed = true;
            }

            if (failed == true) {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Get random number in certain range
        /// </summary>
        /// <param name="min">lower boundry of range</param>
        /// <param name="max">upper boundry of range</param>
        /// <returns>random int</returns>
        private static readonly Random randomInt = new Random();
        private static readonly object IntSyncLock = new object();
        public static int GetRandomNumber(int min, int max) {
            lock (IntSyncLock) { // synchronize
                return randomInt.Next(min, max);
            }
        }

        /// <summary>
        /// Get random string with certain length
        /// </summary>
        /// <param name="length">Desired random generated string length</param>
        /// <returns>Random string</returns>
        private static Random randomStr = new Random();
        private static readonly object StrSyncLock = new object();
        public static string RandomString(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            lock (StrSyncLock) { // synchronize
                return new string(Enumerable.Repeat(chars, length).Select(s => s[randomStr.Next(s.Length)]).ToArray());
            }
        }

        /// <summary>
        /// Get mime type based on filename
        /// </summary>
        /// <param name="fileName">filename to get mime of</param>
        /// <returns>mimetype as string</returns>
        public static string GetMimeType(string fileName) {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        /// <summary>
        /// Generate SHA256 hash for a string 
        /// </summary>
        /// <param name="str">Clean string to generate hash for</param>
        /// <returns>Hashed string</returns>
        public static string sha256(string str) {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str), 0, Encoding.UTF8.GetByteCount(str));
            foreach (byte theByte in crypto) {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

    }
}