using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChomikBot_Framework.bot {
    /// <summary>
    /// Allows to utilise temporary email adresses
    /// </summary>
     class email {

         /// <summary>
         /// initialize temporary email adress
         /// </summary>
         /// <returns>email adress as presented to others</returns>
         private static CookieContainer emailContainer = new CookieContainer();
         private static RestClient emailClient = new RestClient("https://temp-mail.org/en/");
         public static string email_get_adress() {
            emailClient.CookieContainer = emailContainer;

            var request = new RestRequest("/", Method.GET);
            string response = emailClient.Execute(request).Content;

            HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
            webSrc.LoadHtml(response);

            HtmlAgilityPack.HtmlNode emailScrapeNode = webSrc.DocumentNode.SelectSingleNode("//input[@id='mail']");
            string emailAdress = emailScrapeNode.GetAttributeValue("value", "NULL");

            return emailAdress;
        }

         /// <summary>
         /// fetch last recieved message body as HTML code
         /// </summary>
         /// <returns>msg body in html</returns>
         public static string email_readLastMsg() {
            emailClient.CookieContainer = emailContainer;

            var request = new RestRequest("/", Method.GET);
            string response = emailClient.Execute(request).Content;

            HtmlAgilityPack.HtmlDocument webSrc = new HtmlAgilityPack.HtmlDocument();
            webSrc.LoadHtml(response);

            string emailsTbody = webSrc.DocumentNode.SelectSingleNode("//tbody").InnerHtml;
            webSrc.LoadHtml(emailsTbody);

            HtmlAgilityPack.HtmlNode linkNode = webSrc.DocumentNode.SelectSingleNode("//a");
            string bodyHref = linkNode.GetAttributeValue("href", "NULL");
            bodyHref = bodyHref.Replace("https://temp-mail.org/en/", "").Replace(" ", "");

            var requestBody = new RestRequest("/" + bodyHref, Method.GET);
            string responseBody = emailClient.Execute(requestBody).Content;

            webSrc.LoadHtml(responseBody);
            HtmlAgilityPack.HtmlNode msgBodyNode = webSrc.DocumentNode.SelectSingleNode("//div[@class='pm-text']");
            string msgBody = msgBodyNode.InnerHtml;

            return msgBody;
        }

         /// <summary>
         /// destroy previously initialized email adress
         /// </summary>
         public static void email_destroy_adress() {
            emailContainer = new CookieContainer();
         }

     }
}