using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DeathByCaptcha;
using System.Threading.Tasks;
using ChomikBot_UI;
using System.Diagnostics;
using System.Windows.Forms;

namespace ChomikBot_Framework {
    /// <summary>
    /// Performs actions that affect other users
    /// </summary>
    class action {
        private static Captcha sendChatBoxMessageCaptcha;
        private static Captcha sendPMCaptcha;

        /// <summary>
        /// Send message to public chatbox if avaliable
        /// </summary>
        /// <param name="recipient">profile to post to</param>
        /// <param name="message">post content (html tags allowed)</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool sendChatBoxMessage(string recipient, string message) {
            main.client.CookieContainer = main._cook;

            // first check if we can even post there
            var checkChatbox = new RestRequest("/" + recipient, Method.GET);
            string checkResponse = main.client.Execute(checkChatbox).Content;

            HtmlAgilityPack.HtmlDocument checkResponseDoc = new HtmlAgilityPack.HtmlDocument();
            checkResponseDoc.LoadHtml(checkResponse);

            try {
                HtmlAgilityPack.HtmlNode chatNode = checkResponseDoc.DocumentNode.SelectSingleNode("//div[@id='messageForm']");
                string ghettoIsReal = chatNode.InnerHtml;
            } catch {
                //Console.WriteLine(">>Message was not sent because user disabled comments");
                return false;
            }

            // then post
            var sendmsg = new RestRequest("/action/ChomikChat/SendMessage", Method.POST);
            string token = helpers.fetchToken("/" + recipient);

            sendmsg.AddParameter("Message", message);
            sendmsg.AddParameter("Mode", "Last");
            sendmsg.AddParameter("TargetChomikId", helpers.getUserID(recipient));
            sendmsg.AddParameter("__RequestVerificationToken", token);
            sendmsg.AddHeader("X-Requested-With", "XMLHttpRequest");

            string response = main.client.Execute(sendmsg).Content;

            try {
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                // captcha
                #region Captcha 
                if (json.ContainsCaptcha == "true") {

                    // scrape captcha adress
                    HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                    string content = json.Content;
                    webSrc.LoadHtml(content);

                    MessageBox.Show(content);

                    HtmlAgilityPack.HtmlNode cptchScrape = webSrc.DocumentNode.SelectSingleNode("//img[@alt='captcha']");
                    string captchaURL = cptchScrape.GetAttributeValue("src", "NULL");
                    
                    // create path for captcha saving
                    Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\chomikbot\");
                    string captchaPath = System.IO.Path.GetTempPath() + @"\chomikbot\" + helpers.sha256(captchaURL) + ".jpg";

                    // download it
                    File.Delete(captchaPath);

                    using (var writer = File.OpenWrite(captchaPath)) {
                        var downloadCaptcha = new RestRequest(captchaURL, Method.GET);
                        downloadCaptcha.ResponseWriter = (responseStream) => responseStream.CopyTo(writer);
                        var downloadCaptchaResponse = main.client.DownloadData(downloadCaptcha);
                    }

                    sendChatBoxMessageCaptcha = dbc.resolver.resolve(captchaPath);
                    string captchaSolved = sendChatBoxMessageCaptcha.Text;
                    
                    //rerun
                    var resendCaptcha = new RestRequest("/action/ChomikChat/SendMessage", Method.POST);

                    resendCaptcha.AddParameter("Message", message);
                    resendCaptcha.AddParameter("Mode", "Last");
                    resendCaptcha.AddParameter("TargetChomikId", helpers.getUserID(recipient));
                    resendCaptcha.AddParameter("__RequestVerificationToken", token);
                    resendCaptcha.AddParameter("recaptcha_response_field", captchaSolved);
                    resendCaptcha.AddHeader("X-Requested-With", "XMLHttpRequest");
                    
                    string responseCaptcha = main.client.Execute(resendCaptcha).Content;
                    System.IO.File.Delete(captchaPath);

                    if (responseCaptcha.Contains("Wpisany tekst jest niepoprawny")) {
                        var ini = new iniHandler();
                        Client dbcClient = (Client)new SocketClient(ini.Read("dbc_username", "CREDENTIALS"), ini.Read("dbc_password", "CREDENTIALS"));
                        dbcClient.Report(sendChatBoxMessageCaptcha);
                        main.solvedCaptchas--;
                        main.reportedCaptchas++;
                        return action.sendChatBoxMessage(recipient, message);
                    }

                    main.commentsSent++;
                    return true;
                }
                #endregion Captcha

                if (json.IsSuccess == "true") {
                    main.commentsSent++;
                    return true;
                }

                // cool down for a while, we're posting too fast
                if (json.Message == "Musisz chwilę poczekać, aby dodać następną wiadomość") {
                    System.Threading.Thread.Sleep(5000);
                    sendChatBoxMessage(recipient, message);
                }

            } catch {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Send private message to certain user
        /// BE WARNED: repetetive usage of this method can lead to spam ban fast
        /// </summary>
        /// <param name="recipient">profile to send to</param>
        /// <param name="subject">message subject</param>
        /// <param name="body">mesage content (html tags allowed)</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool sendPrivateMessage(string recipient, string subject, string body) {
            var sendmsg = new RestRequest("/action/PrivateMessage/SaveMessage", Method.POST);
            string token = helpers.fetchToken("/" + recipient);

            sendmsg.AddParameter("Body", body);
            sendmsg.AddParameter("FromName", account.getCurrentUserName());
            sendmsg.AddParameter("MessageId", "");
            sendmsg.AddParameter("RecipientId", helpers.getUserID(recipient));
            sendmsg.AddParameter("Subject", subject);
            sendmsg.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + recipient));
            sendmsg.AddHeader("X-Requested-With", "XMLHttpRequest");
            sendmsg.AddHeader("Referer", "http://chomikuj.pl/" + recipient);
            
            string response = main.client.Execute(sendmsg).Content;

            try {
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                // captcha
                #region Captcha
                if (json.ContainsCaptcha == "true") {
                    // scrape captcha adress
                    HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                    string content = json.Content;
                    webSrc.LoadHtml(content);

                    HtmlAgilityPack.HtmlNode cptchScrape = webSrc.DocumentNode.SelectSingleNode("//img[@alt='captcha']");
                    string captchaURL = cptchScrape.GetAttributeValue("src", "NULL");

                    // create path for captcha saving
                    Directory.CreateDirectory(System.IO.Path.GetTempPath() + @"\chomikbot\");
                    string captchaPath = System.IO.Path.GetTempPath() + @"\chomikbot\" + helpers.sha256(captchaURL) + ".jpg";

                    // download it
                    System.IO.File.Delete(captchaPath);

                    using (var writer = File.OpenWrite(captchaPath)) {
                        var downloadCaptcha = new RestRequest(captchaURL, Method.GET);
                        downloadCaptcha.ResponseWriter = (responseStream) => responseStream.CopyTo(writer);
                        var downloadCaptchaResponse = main.client.DownloadData(downloadCaptcha);
                    }

                    sendPMCaptcha = dbc.resolver.resolve(captchaPath);
                    string captchaSolved = sendPMCaptcha.Text;

                    //rerun
                    var resendCaptcha = new RestRequest("/action/PrivateMessage/SaveMessage", Method.POST);

                    resendCaptcha.AddParameter("Body", body);
                    resendCaptcha.AddParameter("FromName", account.getCurrentUserName());
                    resendCaptcha.AddParameter("RecipientId", helpers.getUserID(recipient));
                    resendCaptcha.AddParameter("Subject", subject);
                    resendCaptcha.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + recipient));
                    resendCaptcha.AddParameter("recaptcha_response_field", captchaSolved);
                    resendCaptcha.AddHeader("X-Requested-With", "XMLHttpRequest");

                    string responseCaptcha = main.client.Execute(resendCaptcha).Content;
                    System.IO.File.Delete(captchaPath);

                    if (responseCaptcha.Contains("Wpisany tekst jest niepoprawny")) {
                        var ini = new iniHandler();
                        Client dbcClient = (Client)new SocketClient(ini.Read("dbc_username", "CREDENTIALS"), ini.Read("dbc_password", "CREDENTIALS"));
                        dbcClient.Report(sendPMCaptcha);
                        main.solvedCaptchas--;
                        main.reportedCaptchas++;
                        return action.sendPrivateMessage(recipient, subject, body);
                    }

                    var verifyCaptchaResponse = JsonConvert.DeserializeObject<dynamic>(responseCaptcha);

                    if (verifyCaptchaResponse.IsSuccess == "true" && verifyCaptchaResponse.Type == "Growl" && verifyCaptchaResponse.Content == "Wiadomość została wysłana!") {
                        main.sentPMs++;
                        return true;
                    } else {
                        return false;
                    }
                }
                #endregion Captcha

                if (json.IsSuccess == "true" && json.Type == "Growl" && json.Content == "Wiadomość została wysłana!") {
                    main.sentPMs++;
                    return true;
                }

            } catch {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Add certain user to recommended list
        /// </summary>
        /// <param name="username">username to add to</param>
        /// <param name="description"> (optional)</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool addFriend(string username, List<string> initList, string description = "") {
            main.client.CookieContainer = main._cook;
            var addFriend = new RestRequest("/action/Friends/NewFriend", Method.POST);
            addFriend.AddParameter("ChomikFriendId", helpers.getUserID(username));
            addFriend.AddParameter("Descr", description);
            addFriend.AddParameter("FromPMBox", "");
            addFriend.AddParameter("Group", "0");
            addFriend.AddParameter("Msg", "");
            addFriend.AddParameter("Page", "1");
            addFriend.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + username));
            addFriend.AddHeader("X-Requested-With", "XMLHttpRequest");

            try {
                string response = main.client.Execute(addFriend).Content;

                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.Type == "Growl" && json.IsSuccess == "true") {
                    main.friendsAdded++;
                    main._main.writeActionLog("DODANO DO ZNAJOMYCH: " + username);
                    return true;
                } else {

                    return action.addFriend(initList[helpers.GetRandomNumber(1, initList.Count)], initList);
                    // main._main.writeActionLog("NIE UDAŁO SIĘ DODAĆ DO ZNAJOMYCH: " + username);
                    //return false;
                }
            } catch {
                main._main.writeActionLog("NIE UDAŁO SIĘ DODAĆ DO ZNAJOMYCH: " + username);
                return false;
            }
           
        }

    }
}