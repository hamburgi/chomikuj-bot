using ChomikBot_UI;
using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChomikBot_Framework {
    /// <summary>
    /// Scrape nicknames with ease
    /// </summary>
    class usr_scraper {

        /// <summary>
        /// Scrape so called recommended users
        /// </summary>
        /// <param name="count">desired list size</param>
        /// <param name="merge">if provided output will be merged with it (optional)</param>
        /// <returns>collection of fetched username nicknames</returns>
        public static List<string> scrapeRecommendedAccounts(int count, List<string> merge = null) {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            try {
                string url = "http://chomikuj.pl/action/LastAccounts/RecommendedAccounts?TimeStamp=" + unixTimestamp.ToString() + "&itemsCount=" + count.ToString();
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);

                var names = new List<string>();
                if (merge != null) {
                    names = merge;
                }

                int dupes = 0;

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]")) {
                    string name = link.InnerText;
                    bool isPresent = names.Any(s => name.Contains(s));

                    if (validateNickname(name)) { // no empty entries
                        if (isPresent == false) { // no duplicates
                            names.Add(name);
                        } else {
                            dupes++;
                        }
                    }

                }

                return names;

            } catch (Exception ex) {
                if (ex.Message != null) {
                 // Error occured on scrape attempt: " + ex.Message
                } else {
                 // Unknown error occured on scrape attempt
                }
                return null;
            }

        }

        /// <summary>
        /// Scrape users recommended by certain user so basicly 'friends' of certain user
        /// </summary>
        /// <param name="username">desired username to fetch from</param>
        /// <param name="page">desired pagination number</param>
        /// <param name="merge">if provided output will be merged with it (optional)</param>
        /// <returns>collection of fetched username nicknames</returns>
        public static List<string> scrapeFriends(string username, int page, List<string> merge = null) {
            try {
                main.client.CookieContainer = main._cook;
                var changedesc = new RestRequest("/action/Friends/ShowAllFriends", Method.POST);
                changedesc.AddParameter("chomikName", username);
                changedesc.AddParameter("page", page.ToString());
                changedesc.AddHeader("X-Requested-With", "XMLHttpRequest");
                string responseString = main.client.Execute(changedesc).Content;

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(responseString);

                var names = new List<string>();
                if (merge != null) {
                    names = merge;
                }

                int dupes = 0;
                int scrapedAmount = 0;

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]")) {
                    string name = link.InnerText;
                    bool isPresent = names.Any(s => name.Contains(s));

                    if (validateNickname(name) == true) { // validate nickname
                        if (isPresent == false) { // no duplicates
                            names.Add(name);
                            scrapedAmount++;
                        } else {
                            dupes++;
                        }
                    }

                }

                return names;


            } catch {
               // Error while scraping friends from " + username
                return null;
            }

        }

        /// <summary>
        /// Scrape last seen users
        /// </summary>
        /// <param name="count">desired list size</param>
        /// <param name="merge">if provided output will be merged with it (optional)</param>
        /// <returns>collection of last seen username nicknames</returns>
        public static List<string> scrapeLastSeen(int count, List<string> merge = null) {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            try {
               // console._konsola.log("Scraping " + count.ToString() + " recommended nicknames...");
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load("http://chomikuj.pl/action/LastAccounts/LastSeen?TimeStamp=" + unixTimestamp.ToString() + "&itemsCount=" + count.ToString());

                var names = new List<string>();
                if (merge != null) {
                    names = merge;
                }

                int dupes = 0;
                int scrapedAmount = 0;

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]")) {
                    string name = link.InnerText;
                    bool isPresent = names.Any(s => name.Contains(s));

                    if (validateNickname(name)) { // validate nickname
                        if (isPresent == false) { // no duplicates
                            names.Add(name);
                            scrapedAmount++;
                        } else {
                            dupes++;
                        }
                    }

                }

                return names;

            } catch (Exception ex) {
                if (ex.Message != null) {
                    // Error occured on scrape attempt: " + ex.Message
                } else {
                   // Unknown error occured on scrape attempt
                }
                return null;
            }

        }

        /// <summary>
        /// Helper func used to validate proper nickanmes
        /// </summary>
        /// <param name="nickname">nickname to validate</param>
        /// <returns>indicates whether nickname is valid</returns>
        private static bool validateNickname(string nickname) {

            if (nickname.Length <= 1) { // no empty entries
                return false;
            }

            if ((nickname.Contains("nia strona") || nickname.Contains("pna strona"))) { // no misc trash
                return false;
            }

            string isPageNum = nickname.Replace(" ", "").Replace(".", ""); // deny page numbers
            if (Regex.IsMatch(isPageNum, @"^\d+$")) {
                return false;
            }

            return true; // valid
        }

    }
}