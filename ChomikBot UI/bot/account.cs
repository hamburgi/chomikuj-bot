using ChomikBot_Framework.bot;
using ChomikBot_UI;
using DeathByCaptcha;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChomikBot_Framework {
    /// <summary>
    /// Performs actions assosiated with account managament 
    /// </summary>
    class account {
        private static Captcha logCaptcha;
        private static Captcha regCaptcha;
        private static string logResponse;
        private static string regResponseCaptcha;

        public static string loginError = null;
        public static string regisError = null;

        /// <summary>
        /// Login new user account
        /// </summary>
        /// <param name="login">username to login with</param>
        /// <param name="password">password to login with</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool login(string login, string password) {
            main.client.CookieContainer = main._cook;
            var request = new RestRequest("/action/Login/TopBarLogin", Method.POST);
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            request.AddParameter("Login", login);
            request.AddParameter("Password", password);

            try {
                string response = main.client.Execute(request).Content;
                logResponse = response;

                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.IsSuccess == "true") {
                    #region Captcha
                    string repContent = json.Content + " ";
                    if (repContent.Contains("<input id=\"RequireCaptcha\" name=\"RequireCaptcha\" type=\"hidden\" value=\"True\" />")) { // this is due to a bug: ContainsCaptcha = false here

                        HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                        webSrc.LoadHtml(repContent);

                        HtmlAgilityPack.HtmlNode cptchScrape = webSrc.DocumentNode.SelectSingleNode("//img[@alt='captcha']");
                        string captchaURL = cptchScrape.GetAttributeValue("src", "NULL");

                        // download it
                        string filename = "chomikbotLogCaptcha.jpg";
                        System.IO.File.Delete(System.IO.Path.GetTempPath() + filename);

                        using (var writer = File.OpenWrite(System.IO.Path.GetTempPath() + filename)) {
                            var downloadCaptcha = new RestRequest(captchaURL, Method.GET);
                            downloadCaptcha.ResponseWriter = (responseStream) => responseStream.CopyTo(writer);
                            var downloadCaptchaResponse = main.client.DownloadData(downloadCaptcha);
                        }

                        logCaptcha = dbc.resolver.resolve(System.IO.Path.GetTempPath() + filename);
                        string captchaSolved = logCaptcha.Text;

                        // send final register request
                        var resendCaptcha = new RestRequest("/action/Login/Login", Method.POST);

                        resendCaptcha.AddParameter("CaptchaEntered", captchaSolved);
                        resendCaptcha.AddParameter("FileId", "0");
                        resendCaptcha.AddParameter("IsWatchOnline", "False");
                        resendCaptcha.AddParameter("Login", login);
                        resendCaptcha.AddParameter("Password", password);
                        resendCaptcha.AddParameter("Redirect", "True");
                        resendCaptcha.AddParameter("RequireCaptcha", "True");
                        resendCaptcha.AddHeader("X-Requested-With", "XMLHttpRequest");

                        string logResponseCaptcha = main.client.Execute(resendCaptcha).Content;

                        if (logResponseCaptcha.Contains("Wpisany tekst jest niepoprawny")) {
                            var ini = new iniHandler();
                            Client dbcClient = (Client)new SocketClient(ini.Read("dbc_username", "CREDENTIALS"), ini.Read("dbc_password", "CREDENTIALS"));
                            dbcClient.Report(logCaptcha);
                            return account.login(login, password);
                        }

                        System.IO.File.Delete(System.IO.Path.GetTempPath() + filename);
                    }
                        #endregion
                    return true;
                } else {
                    loginError = json.Message;
                    return false;
                }

            } catch {
                return false;
            }
        }

        /// <summary>
        /// Register new user account
        /// </summary>
        /// <param name="login">desired username</param>
        /// <param name="password">desired password</param>
        /// <param name="confirmEmail">shall we confirm our registration using recieved link?</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool register(string login, string password, bool confirmEmail = true) {
            string email = bot.email.email_get_adress();

            var registerClient = new RestClient("http://chomikuj.pl");
            CookieContainer _Regcook = new CookieContainer();
            registerClient.CookieContainer = _Regcook;

            #region validation
            if (!string.IsNullOrEmpty(email) && !helpers.IsValidEmail(email)) {
                regisError = "Incorrect email";
                return false;
            }

            if (login == password) {
                regisError = "Login equals Password";
                return false;
            }

            if (login.Length > 20 || password.Length > 25) {
                regisError = "Login / Pass too long";
                return false;
            }

            // check if name avaliable
            var checkLoginrequest = new RestRequest("/action/Registration/CheckName", Method.POST);
            checkLoginrequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            checkLoginrequest.AddParameter("name", login);

            string checkLoginResponse = registerClient.Execute(checkLoginrequest).Content;
            var checkLoginjson = JsonConvert.DeserializeObject<dynamic>(checkLoginResponse);

            if (checkLoginjson.IsSuccess == "false") {
                regisError = "Invalid Login";
                return false;
            }
            
            // check if password is ok
            var checkPassRequest = new RestRequest("/action/Registration/CheckPassword", Method.POST);
            checkPassRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            checkPassRequest.AddParameter("password", password);

            string checkPassResponse = registerClient.Execute(checkPassRequest).Content;
            var checkPassjson = JsonConvert.DeserializeObject<dynamic>(checkPassResponse);

            if (checkPassjson.IsSuccess == "false") {
                regisError = "Invalid Password";
                return false;
            }
            #endregion

            #region token fetching

            var getToken = new RestRequest("/action/MobilePromo/ReturnToPage", Method.GET);
            getToken.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0");
            string tokenRep = registerClient.Execute(getToken).Content;

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(tokenRep);

            HtmlAgilityPack.HtmlNode tokenNode = doc.DocumentNode.SelectSingleNode("//input[@name='__RequestVerificationToken']");

            string token = tokenNode.GetAttributeValue("value", "NULL");

            #endregion
          
            // registration

            var request = new RestRequest("/action/Registration/RegisterAccount", Method.POST);
            request.AddHeader("X-Requested-With", "XMLHttpRequest");

            request.AddParameter("AccountName", login);
            request.AddParameter("Email", email);
            request.AddParameter("Password", password);
            request.AddParameter("__RequestVerificationToken", token);
            
            try {
                string response = registerClient.Execute(request).Content;
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.IsSuccess == "true") {
                    string registerToken = response.Substring(response.IndexOf("mqoucwkuqwbc21kj") + "mqoucwkuqwbc21kj".Length).Split('/')[0];

                    registerToken = Regex.Replace(registerToken, "[^0-9.]", "");
                    registerToken = registerToken.Substring(2, registerToken.Length - 2);

                    // accept terms and conditions 
                    var acceptTos = new RestRequest("/action/Registration/AcceptTermsAndRegister", Method.POST);
                   acceptTos.AddHeader("X-Requested-With", "XMLHttpRequest");
                    
                    acceptTos.AddParameter("AllowProcessPrivateData", "true");
                    acceptTos.AddParameter("AllowProcessPrivateData", "false");
                    acceptTos.AddParameter("TermsAccepted", "true");
                    acceptTos.AddParameter("TermsAccepted", "false");
                    acceptTos.AddParameter("__RequestVerificationToken", token);
                    acceptTos.AddParameter("mqoucwkuqwbc21kj", registerToken);
                    acceptTos.AddParameter("redirectOnSuccess", "true");

                    string captchaToRespond = registerClient.Execute(acceptTos).Content;
                    var jsonCaptcha = JsonConvert.DeserializeObject<dynamic>(captchaToRespond);

                    #region Captcha
                    if (jsonCaptcha.ContainsCaptcha == "true") {

                        HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                        string content = jsonCaptcha.Content;
                        webSrc.LoadHtml(content);

                        HtmlAgilityPack.HtmlNode cptchScrape = webSrc.DocumentNode.SelectSingleNode("//img[@alt='captcha']");
                        string captchaURL = cptchScrape.GetAttributeValue("src", "NULL");

                        // download it
                        string filename = "chomikbotRegCaptcha.jpg";
                        System.IO.File.Delete(System.IO.Path.GetTempPath() + filename);

                        using (var writer = File.OpenWrite(System.IO.Path.GetTempPath() + filename)) {
                            var downloadCaptcha = new RestRequest(captchaURL, Method.GET);
                            downloadCaptcha.ResponseWriter = (responseStream) => responseStream.CopyTo(writer);
                            var downloadCaptchaResponse = registerClient.DownloadData(downloadCaptcha);
                        }

                        regCaptcha = dbc.resolver.resolve(System.IO.Path.GetTempPath() + filename);
                        string captchaSolved = regCaptcha.Text;

                        // send final register request
                        var resendCaptcha = new RestRequest("/action/Registration/AcceptTermsAndRegister", Method.POST);

                        resendCaptcha.AddParameter("AllowProcessPrivateData", "true");
                        resendCaptcha.AddParameter("TermsAccepted", "true");
                        resendCaptcha.AddParameter("__RequestVerificationToken", token);
                        resendCaptcha.AddParameter("recaptcha_response_field", captchaSolved);
                        resendCaptcha.AddParameter("redirectOnSuccess", "true");
                        resendCaptcha.AddHeader("X-Requested-With", "XMLHttpRequest");
                        
                        regResponseCaptcha = registerClient.Execute(resendCaptcha).Content;
                        System.IO.File.Delete(System.IO.Path.GetTempPath() + filename);

                        var finalRepJson = JsonConvert.DeserializeObject<dynamic>(regResponseCaptcha);

                        if (finalRepJson.Type == "Redirect" && finalRepJson.redirectUrl == "/action/Info/ActionBlocked") {
                            regisError = "You are blocked from creating new accounts";
                            return false;
                        }

                        if (finalRepJson.Type == "Redirect" && finalRepJson.trackingCodeJS == "ch.UI.Analytics.RegistrationSignup();") {

                            if (confirmEmail) {
                                System.Threading.Thread.Sleep(5000);
                                string emailBody = bot.email.email_readLastMsg();
                                HtmlAgilityPack.HtmlDocument docEmail = new HtmlAgilityPack.HtmlDocument();
                                docEmail.LoadHtml(emailBody);

                                HtmlAgilityPack.HtmlNode msgBodyNode = docEmail.DocumentNode.SelectSingleNode("//a[@style='color: #339999; font-size: 17px']");
                                string msgBodyHref = msgBodyNode.GetAttributeValue("href", "NULL");
                                msgBodyHref = msgBodyHref.Replace("http://chomikuj.pl/ChomikConfirm.aspx?", "").Replace(" ", "");

                                var sendActivation = new RestRequest("/ChomikConfirm.aspx", Method.GET);
                                sendActivation.AddParameter("id", msgBodyHref.Split('&')[0].Replace("id=", "").Replace("amp;", ""));
                                sendActivation.AddParameter("email", msgBodyHref.Split('&')[1].Replace("email=", "").Replace("amp;", ""));
                                sendActivation.AddParameter("confirm", msgBodyHref.Split('&')[2].Replace("confirm=", "").Replace("amp;", ""));
                                registerClient.Execute(sendActivation);

                                //string actvResponse = registerClient.Execute(sendActivation).Content;
                                // docEmail.LoadHtml(actvResponse);
                                // HtmlAgilityPack.HtmlNode rpNode = docEmail.DocumentNode.SelectSingleNode("//div[@class='infoFrameContent']");
                                // rpNode.InnerHtml <== use this to check if confirmation was ok
                            }

                            main._main.writeActionLog("STWORZONO KONTO, LOGIN: " + login + ", HASŁO: " + password);

                            bot.email.email_destroy_adress();
                            return true;
                        }

                        return false;
                    }

                    #endregion Captcha
                }

            } catch {
                var finalRepJson2 = JsonConvert.DeserializeObject<dynamic>(regResponseCaptcha);
                if (finalRepJson2.IsSuccess == "false" && finalRepJson2.Message == "Wpisany tekst jest niepoprawny") {
                    var ini = new iniHandler();
                    Client dbcClient = (Client)new SocketClient(ini.Read("dbc_username", "CREDENTIALS"), ini.Read("dbc_password", "CREDENTIALS"));
                    dbcClient.Report(regCaptcha);
                    return account.register(login, password, confirmEmail);
                }
                return false;
            }

            return false;
        }

        /// <summary>
        /// Change various local user profile details
        /// </summary>
        /// <param name="AboutMe">information displayed on avatar zoom-in</param>
        /// <param name="BirthDay">self explenatory</param>
        /// <param name="BirthMonth">self explenatory</param>
        /// <param name="BirthYear">self explenatory</param>
        /// <param name="Email">self explenatory</param>
        /// <param name="Name">one's full name</param>
        /// <param name="Sex">one's gender; true = female, false = male</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool changeDetails(string AboutMe, int BirthDay, int BirthMonth, int BirthYear, string Email, string Name, bool Sex) {
            // email validation
            if (!string.IsNullOrEmpty(Email) && !helpers.IsValidEmail(Email)) {
                return false;
            }

            // birth date validation
            if (BirthDay < 1 || BirthDay > 31) {
                return false;
            }

            if (BirthMonth < 1 || BirthMonth > 12) {
                return false;
            }

            if (BirthYear < 1900 || BirthYear > 2016) {
                return false;
            }

            if (string.IsNullOrEmpty(Name)) {
                return false;
            }

            // all good, send request
            main.client.CookieContainer = main._cook;
            var changedesc = new RestRequest("/action/Account/EditOwnerInfo", Method.POST);
            changedesc.AddParameter("__RequestVerificationToken", helpers.fetchToken("/action/Account/Edit"));
            changedesc.AddParameter("AboutMe", AboutMe);
            changedesc.AddParameter("BirthDay.Day", BirthDay.ToString());
            changedesc.AddParameter("BirthDay.Month", BirthMonth.ToString());
            changedesc.AddParameter("BirthDay.Year", BirthYear.ToString());
            changedesc.AddParameter("Email", Email);
            changedesc.AddParameter("Name", Name);
            changedesc.AddParameter("Sex", Sex.ToString());
            changedesc.AddHeader("X-Requested-With", "XMLHttpRequest");

            try {
                string response = main.client.Execute(changedesc).Content;
                Console.WriteLine(response);
                Console.ReadKey();
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.Type == "Redirect") {
                    return true;
                } else {
                    return false;
                }
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Change local user profile description
        /// </summary>
        /// <param name="description">desired description to be set</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool changeDesc(string description) {
            main.client.CookieContainer = main._cook;
            var changedesc = new RestRequest("/action/Account/ChangeDescription", Method.POST);
            changedesc.AddParameter("__RequestVerificationToken", helpers.fetchToken("/action/Account/Edit"));
            changedesc.AddParameter("description", description);
            changedesc.AddHeader("X-Requested-With", "XMLHttpRequest");

            try {
                string response = main.client.Execute(changedesc).Content;
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.Type == "Redirect") {
                    return true;
                } else {
                    return false;
                }
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Change local user avatar image
        /// </summary>
        /// <param name="filepath">local image file path</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool changeAvatar(string filepath) {
            try {
                main.client.CookieContainer = main._cook;
                string token = helpers.fetchToken("/" + getCurrentUserName());

                // collect upload data
                var request = new RestRequest("/action/Account/SetAvatarWindow", Method.POST);
                request.AddParameter("__RequestVerificationToken", token);
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                string response = main.client.Execute(request).Content;

                var json = JsonConvert.DeserializeObject<dynamic>(response);
                string content = json.Content;

                HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                webSrc.LoadHtml(content);

                HtmlAgilityPack.HtmlNode uploadAdressNode = webSrc.DocumentNode.SelectSingleNode("//form[@id='ChangeAvatarForm']");
                HtmlAgilityPack.HtmlNode c_Node = webSrc.DocumentNode.SelectSingleNode("//input[@id='c']");
                HtmlAgilityPack.HtmlNode ia_Node = webSrc.DocumentNode.SelectSingleNode("//input[@id='ia']");
                HtmlAgilityPack.HtmlNode p_Node = webSrc.DocumentNode.SelectSingleNode("//input[@id='p']");
                HtmlAgilityPack.HtmlNode ct_Node = webSrc.DocumentNode.SelectSingleNode("//input[@id='ct']");
                HtmlAgilityPack.HtmlNode loc_Node = webSrc.DocumentNode.SelectSingleNode("//input[@id='loc']");
                HtmlAgilityPack.HtmlNode s_Node = webSrc.DocumentNode.SelectSingleNode("//input[@id='s']");
                HtmlAgilityPack.HtmlNode t_Node = webSrc.DocumentNode.SelectSingleNode("//input[@id='t']");
                HtmlAgilityPack.HtmlNode avatarIdNode = webSrc.DocumentNode.SelectSingleNode("//input[@id='AvatarPhotoId']");


                string uploadAdress = uploadAdressNode.GetAttributeValue("action", "NULL").Replace("/UploadAvatar.aspx", "");
                string c_val = c_Node.GetAttributeValue("value", "NULL");
                string ia_val = ia_Node.GetAttributeValue("value", "NULL");
                string p_val = p_Node.GetAttributeValue("value", "NULL");
                string ct_val = ct_Node.GetAttributeValue("value", "NULL");
                string loc_val = loc_Node.GetAttributeValue("value", "NULL");
                string s_val = s_Node.GetAttributeValue("value", "NULL");
                string t_val = t_Node.GetAttributeValue("value", "NULL");
                string avatarId = avatarIdNode.GetAttributeValue("value", "NULL");

                // start upload
                RestClient uploadClient = new RestClient(uploadAdress);
                uploadClient.CookieContainer = main._cook;

                var requestUpload = new RestRequest("/UploadAvatar.aspx", Method.POST);
                requestUpload.AddHeader("Referer", "http://chomikuj.pl/" + getCurrentUserName());
                requestUpload.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:47.0) Gecko/20100101 Firefox/47.0");
                requestUpload.AddParameter("c", c_val);
                requestUpload.AddParameter("ia", ia_val);
                requestUpload.AddParameter("p", p_val);
                requestUpload.AddParameter("ct", ct_val);
                requestUpload.AddParameter("loc", loc_val);
                requestUpload.AddParameter("s", s_val);
                requestUpload.AddParameter("t", t_val);
                requestUpload.AddFile("file", File.ReadAllBytes(filepath), Path.GetFileName(filepath), helpers.GetMimeType(filepath));

                string uploadResponse = uploadClient.Execute(requestUpload).Content;
                uploadClient.Execute(new RestRequest("/UploadAvatar.aspx", Method.OPTIONS));

                var uploadResponseJson = JsonConvert.DeserializeObject<dynamic>(uploadResponse);
                string realid = uploadResponseJson.realId;
                string upToken = uploadResponseJson.token;

                var requestConfirmation = new RestRequest("/action/Account/SetAvatarConfirmWindow", Method.POST);
                requestUpload.AddHeader("Referer", "http://chomikuj.pl/" + getCurrentUserName());
                requestConfirmation.AddHeader("X-Requested-With", "XMLHttpRequest");
                requestConfirmation.AddParameter("AvatarPhotoId", avatarId);
                requestConfirmation.AddParameter("SubfolderId", remote_file.getSubTreeEnum("Galeria"));
                requestConfirmation.AddParameter("__RequestVerificationToken", token);
                requestConfirmation.AddParameter("realId", realid);
                requestConfirmation.AddParameter("token", upToken);

                string confirmResponse = main.client.Execute(requestConfirmation).Content;
                var finalResponseJson = JsonConvert.DeserializeObject<dynamic>(confirmResponse);

                string finalRepContent = finalResponseJson.Content;
                if (finalResponseJson.Type == "Window" && finalRepContent.Contains("Możesz go zaakceptować lub wgrać kolejny")) {
                    main._main.writeActionLog("POMYŚLNIE ZMIENIONO AVATAR");
                    return true;
                }

                main._main.writeActionLog("NIE UDAŁO SIĘ ZMIENIĆ AVATARA");
                return false;

            } catch {
                main._main.writeActionLog("NIE UDAŁO SIĘ ZMIENIĆ AVATARA");
                return false;
            }
        }

        /// <summary>
        /// Fetch current local username
        /// </summary>
        /// <returns>local username as displayed on website</returns>
        public static string getCurrentUserName() {
            main.client.CookieContainer = main._cook;

            var request = new RestRequest("/action/Account/Edit", Method.GET);
            string response = main.client.Execute(request).Content;

            HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
            webSrc.LoadHtml(response);

            HtmlAgilityPack.HtmlNode node = webSrc.DocumentNode.SelectSingleNode("//a[@id='ChangeName']");
            string name = node.GetAttributeValue("href", "NULL");
            name = name.Replace("/EditName.aspx?id=", "");

            return name;
        }

        public static bool unlockProfile() {
            try {
                main.client.CookieContainer = main._cook;
                string token = helpers.fetchToken("/action/Account/Edit#passwords");

                var request = new RestRequest("/action/account/DisableReadProtected", Method.POST);
                request.AddParameter("__RequestVerificationToken", token);
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                string response = main.client.Execute(request).Content;

                var json = JsonConvert.DeserializeObject<dynamic>(response);
                string content = json.Content;

                if (content == "Zapisano ustawienie") {
                    main._main.writeActionLog("ODBLOKOWANO PROFIL");
                    return true;
                }

                main._main.writeActionLog("NIE UDAŁO SIĘ ODBLOKOWAĆ PROFILU");
                return false;
            } catch {
                main._main.writeActionLog("NIE UDAŁO SIĘ ODBLOKOWAĆ PROFILU");
                return false;
            }
        }

        public static bool lockProfile(string password) {
            if(password.Length < 6) {
                main._main.writeActionLog("NIE UDAŁO SIĘ ZABLOKOWAĆ PROFILU");
                return false;
            }

            try {
                main.client.CookieContainer = main._cook;
                string token = helpers.fetchToken("/action/Account/Edit");

                var request = new RestRequest("/action/account/ChangeReadPassword", Method.POST);
                request.AddParameter("__RequestVerificationToken", token);
                request.AddParameter("ConfirmPassword", password);
                request.AddParameter("Password", password);
                request.AddParameter("read.Access", "ByPassword");
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                string response = main.client.Execute(request).Content;

                var json = JsonConvert.DeserializeObject<dynamic>(response);
                string content = json.Content;

                if (content == "Hasło zostało zmienione") {
                    main._main.writeActionLog("ZABLOKOWANO PROFIL, HASŁEM: " + password);
                    return true;
                }

                main._main.writeActionLog("NIE UDAŁO SIĘ ZABLOKOWAĆ PROFILU");
                return false;
            } catch {
                main._main.writeActionLog("NIE UDAŁO SIĘ ZABLOKOWAĆ PROFILU");
                return false;
            }
        }

    }
}