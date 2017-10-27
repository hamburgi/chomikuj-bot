using ChomikBot_Framework.bot;
using ChomikBot_UI;
using DeathByCaptcha;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChomikBot_Framework.bot {
    /// <summary>
    /// Deals with everything regarding remote file operations, such as create new folders, upload files etc.
    /// </summary>
    class remote_file {

        /// <summary>
        /// Create new directory
        /// </summary>
        /// <param name="foldername">directory name</param>
        /// <param name="password">directory password (optional)</param>
        /// <param name="parentFolder">folder which will be parentdir to our directory (optional)</param>
        /// <param name="adult">mark directory as +18? (optional)</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool createNewFolder(string foldername, string password = "", int parentFolder = 0, bool adult = false) {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/action/FolderOptions/NewFolderAction", Method.POST);
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                request.AddParameter("AdultContent", adult.ToString());
                request.AddParameter("ChomikId", helpers.getUserID(account.getCurrentUserName()));
                request.AddParameter("FolderId", parentFolder.ToString());
                request.AddParameter("FolderName", Uri.EscapeDataString(foldername));
                if (!string.IsNullOrEmpty(password)) {
                    request.AddParameter("NewFolderSetPassword", "true");
                    request.AddParameter("NewFolderSetPassword", "false");
                }
                request.AddParameter("Password", password);
                request.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + account.getCurrentUserName()));

                string response = main.client.Execute(request).Content;
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.Type == "Growl" && json.Content == "Folder został dodany") {
                    return true;
                }
            } catch {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Remove certain directory
        /// </summary>
        /// <param name="dirEnum">desired dir enum to be removed</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool removeFolder(int dirEnum) {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/action/FolderOptions/DeleteFolderAction", Method.POST);
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                request.AddParameter("ChomikId", helpers.getUserID(account.getCurrentUserName()));
                request.AddParameter("FolderId", dirEnum);
                request.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + account.getCurrentUserName()));

                string response = main.client.Execute(request).Content;
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                string content = json.Content;
                if (json.Type == "Growl" && content.Contains("został usunięty")) {
                    return true;
                }
            } catch {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Rename certain folder
        /// </summary>
        /// <param name="oldEnum">old directory enum</param>
        /// <param name="newName">new directory name</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool renameFolder(int oldEnum, string newName) {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/action/FolderOptions/ChangeNameAction", Method.POST);
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                request.AddParameter("ChomikId", helpers.getUserID(account.getCurrentUserName()));
                request.AddParameter("FolderId", oldEnum.ToString());
                //request.AddParameter("FolderName", Uri.EscapeDataString(newName));
                request.AddParameter("FolderName", Uri.EscapeDataString(newName));
                request.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + account.getCurrentUserName()));

                string response = main.client.Execute(request).Content;
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.Type == "Growl" && json.Content == "Nazwa została zmieniona.") {
                    return true;
                }
            } catch {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Reset (or set) password for certain directory
        /// </summary>
        /// <param name="dirEnum">directory enum to set pass for</param>
        /// <param name="password">desired password</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool setFolderPassword(int dirEnum, string password) {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/action/FolderOptions/ChangePasswordAction", Method.POST);
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                request.AddParameter("ChomikId", helpers.getUserID(account.getCurrentUserName()));
                request.AddParameter("FolderId", dirEnum);
                request.AddParameter("Password", password);
                request.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + account.getCurrentUserName()));

                string response = main.client.Execute(request).Content;
                var json = JsonConvert.DeserializeObject<dynamic>(response);

                if (json.Type == "Growl" && json.Content == "Hasło folderu zostało ustalone.") {
                    return true;
                }
            } catch {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Upload files to a directory
        /// </summary>
        /// <param name="files">list of local file paths to be uploaded</param>
        /// <param name="iDirEnum">enum of directory to be uploaded to</param>
        /// <returns>indicates whether operation suceeded</returns>
        public static bool uploadFile(List<string> files, int iDirEnum) {
            main.client.CookieContainer = main._cook;

            try {
                string uid = helpers.getUserID(account.getCurrentUserName());
                string dirEnum = iDirEnum.ToString();
                string token = helpers.fetchToken("/" + account.getCurrentUserName());
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                foreach (var entry in files) {
                    var request = new RestRequest("/action/Upload/GetUrl", Method.POST);
                    request.AddHeader("X-Requested-With", "XMLHttpRequest");
                    request.AddParameter("folderid", dirEnum);
                    request.AddParameter("accountid", uid);
                    request.AddParameter("__RequestVerificationToken", token);

                    string response = main.client.Execute(request).Content;
                    var json = JsonConvert.DeserializeObject<dynamic>(response);

                    string callBackAdress = json.Url;
                    callBackAdress = callBackAdress + "&ms=" + unixTimestamp.ToString();

                    RestClient uploadClient = new RestClient(callBackAdress);
                    uploadClient.CookieContainer = main._cook;

                    var requestUpload = new RestRequest("/UploadHandler.aspx", Method.POST);
                    requestUpload.AddFile("files[]", File.ReadAllBytes(entry), Path.GetFileName(entry), helpers.GetMimeType(entry));

                    string uploadResponse = uploadClient.Execute(requestUpload).Content;
                    var checkJson = JsonConvert.DeserializeObject<dynamic>(uploadResponse);

                    foreach (dynamic sizes in checkJson.files) {
                        // if (Int32.Parse(sizes.size) < 5)
                        string csize = sizes.size;
                        if (csize.Length < 2) {
                            return false;
                        }
                    }

                }
            } catch {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get certain subtree enum based on subtree name from local user account
        /// </summary>
        /// <param name="subTreeName">Desired subtree name as presented on website</param>
        /// <returns>directory enum</returns>
        public static int getSubTreeEnum(string subTreeName) {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/" + account.getCurrentUserName(), Method.GET);
                string response = main.client.Execute(request).Content;

                HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                webSrc.LoadHtml(response);

                HtmlAgilityPack.HtmlNode node = webSrc.DocumentNode.SelectSingleNode("//a[@title='" + subTreeName + "']");
                string number = node.GetAttributeValue("rel", "NULL");

                return Int32.Parse(number);
            } catch {
                return -1;
            }
        }

        /// <summary>
        /// Get certain subtree name based on enum from local user account
        /// </summary>
        /// <param name="treeEnum">Targeted subtree enum</param>
        /// <returns>directory name</returns>
        public static string getSubTreeString(int treeEnum) {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/" + account.getCurrentUserName(), Method.GET);
                string response = main.client.Execute(request).Content;

                HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                webSrc.LoadHtml(response);

                HtmlAgilityPack.HtmlNode node = webSrc.DocumentNode.SelectSingleNode("//a[@rel='" + treeEnum.ToString() + "']");
                return node.GetAttributeValue("title", "NULL");
            } catch {
                return "NULL";
            }
        }

        public static List<string> castTreeView() {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/" + account.getCurrentUserName(), Method.GET);
                string response = main.client.Execute(request).Content;
                response = response.Split(new string[] { "frameRight" }, StringSplitOptions.None)[0];

                HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
                HtmlDocument webSrc2 = new HtmlAgilityPack.HtmlDocument();
                webSrc.LoadHtml(response);

                HtmlNode treeNode = webSrc.DocumentNode.SelectSingleNode("//table[@class='T_table accountTree']");

                List<string> list = new List<string>();

                bool firstOccurance = false;
                string username = account.getCurrentUserName();
                foreach (HtmlNode node in treeNode.SelectNodes("//td")) {
                    if (node.InnerHtml.Contains("a href") && node.InnerHtml.Length < 250 && node.InnerHtml.Length > 25) {
                        string link = node.InnerHtml.Replace("<td>", "").Replace("</td>", "");

                        webSrc2.LoadHtml(link);
                        foreach (HtmlNode node2 in webSrc2.DocumentNode.SelectNodes("//a")) {
                            if (firstOccurance) {
                                string href = node2.GetAttributeValue("href", "").Replace("/" + username + "/", "");
                                //string dirEnum = node2.GetAttributeValue("rel", "");
                                list.Add(href);
                            }

                            firstOccurance = true;
                        }

                    }
                }

                return list;
            } catch {
                //return "NULL";
                return new List<string>();
            }
        }

        public static List<string> castFileView(int treeEnum, int pageNumber = 1) {
            main.client.CookieContainer = main._cook;

            try {
                var request = new RestRequest("/action/Files/FilesList", Method.POST);
                request.AddHeader("X-Requested-With", "XMLHttpRequest");
                request.AddParameter("__RequestVerificationToken", helpers.fetchToken("/" + remote_file.getSubTreeString(treeEnum)));
                request.AddParameter("chomikId", helpers.getUserID(account.getCurrentUserName()));
                request.AddParameter("fileListAscending", "False");
                request.AddParameter("fileListSortType", "Date");
                request.AddParameter("folderChanged", "false");
                request.AddParameter("folderId", treeEnum.ToString());
                request.AddParameter("galleryAscending", "False");
                request.AddParameter("gallerySortType", "Name");
                request.AddParameter("isGallery", "False");
                request.AddParameter("pageNr", pageNumber.ToString());
                request.AddParameter("requestedFolderMode", "filesList");
                string response = main.client.Execute(request).Content;
                
                string filesHTML = response.Split(new string[] { "fileInfoSmallFrame" }, StringSplitOptions.None)[0]; // holds file names, dates, sizes
                string fileInfoHTML = response.Split(new string[] { "fileInfoSmallFrame" }, StringSplitOptions.None)[1]; // holds overall file count and file size
                HtmlDocument webSrc = new HtmlDocument();
                webSrc.LoadHtml(filesHTML);

                List<string> filenames = new List<string>();
                HtmlNodeCollection nodes = webSrc.DocumentNode.SelectNodes("//span[@class='bold']");

                foreach(HtmlNode node in nodes) {
                    filenames.Add(node.InnerText);
                }
                
                return filenames;
            } catch {
                return new List<string>();
            }
        }


    }
}