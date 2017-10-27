using ChomikBot_UI;
using DeathByCaptcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChomikBot_Framework.dbc {
    class resolver {

        public static Captcha resolve(string filepath) {
            var ini = new iniHandler();
            Client dbcClient = new SocketClient(ini.Read("dbc_username", "CREDENTIALS"), ini.Read("dbc_password", "CREDENTIALS"));
            Captcha captcha = dbcClient.Decode(filepath, Client.DefaultTimeout);

            if (null != captcha) {
                main.solvedCaptchas++;
                return captcha;
            }

            return null;
        }


    }
}