using ChomikBot_UI;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;


namespace DeathByCaptcha {

    public delegate void DecodeDelegate(Captcha captcha);

    abstract public class Client {

        public const string Version = "DBC/.NET v4.1.2";
        public const int SoftwareVendorId = 0;
        public const int DefaultTimeout = 60;
        public const int PollsInterval = 5;
        public bool Verbose = false;


        protected string _username = "";
        protected string _password = "";
        protected Object _callLock = new Object();

        public Hashtable Credentials
        {
            get
            {
                Hashtable userpwd = new Hashtable();
                userpwd["username"] = _username;
                userpwd["password"] = _password;
                return userpwd;
            }
        }

        public User User
        {
            get
            {
                return GetUser();
            }
        }

        public double Balance
        {
            get
            {
                return GetBalance();
            }
        }


        protected void Log(string call, string msg)
        {
            if (this.Verbose) {
                Console.WriteLine(DateTime.Now.Ticks + " " + call + (null != msg ? ": " + msg : ""));
            }
        }

        protected void Log(string call)
        {
            this.Log(call, null);
        }

		protected byte[] Load(Object data)
		{
			if(data is string){
				return this.Load ((string)data);
			}else if (data is Stream) {
				return this.Load ((Stream)data);
			}else if (data is byte[]) {
				return (byte[])data;
			}
			this.Log ("Loading data with invalid type.");
			return null;
		}

        protected byte[] Load(Stream st)
        {
            long pos = -1;
            if (st.CanSeek) {
                pos = st.Position;
                st.Position = 0;
            }
            int n = 0, offset = 0, chunk_size = 1024;
            byte[] buf = new byte[chunk_size];
            while (st.CanRead && 0 < (n = st.Read(buf, offset, chunk_size))) {
                offset += n;
                Array.Resize(ref buf, offset + chunk_size);
            }
            if (-1 < pos) {
                st.Position = pos;
            }
            Array.Resize(ref buf, offset);
            return buf;
        }

        protected byte[] Load(string fn)
        {
            if (!File.Exists(fn)) {
                throw new FileNotFoundException(
                    "CAPTCHA image file " + fn + " not found"
                );
            } else {
                using (FileStream st = File.OpenRead(fn)) {
                    return this.Load(st);
                }
            }
        }


        protected Captcha Poll(Captcha captcha, int timeout)
        {
            if (null != captcha) {
                DateTime deadline =
                    DateTime.Now.AddSeconds(0 < timeout
                        ? timeout
                        : Client.DefaultTimeout);
                while (deadline > DateTime.Now && !captcha.Solved) {
                    Thread.Sleep(Client.PollsInterval * 1000);
                    try {
                        captcha = this.GetCaptcha(captcha);
                    } catch (System.Exception e) {
                        if (this.Verbose) {
                            Console.WriteLine(DateTime.Now.Ticks + " POLL " + e.Message);
                        }
                        return null;
                    }
                }
                if (captcha.Solved && captcha.Correct) {
                    return captcha;
                }
            }
            return null;
        }

        protected void PollWithCallback(object state)
        {
            PollPayload payload = (PollPayload)state;
            payload.Callback(this.Poll(payload.Captcha, payload.Timeout));
        }


        public Client(string username, string password)
        {
            this._username = username;
            this._password = password;
        }

        abstract public void Close();


        abstract public User GetUser();


        public double GetBalance()
        {
            return this.GetUser().Balance;
        }



        abstract public Captcha GetCaptcha(int id);


        public Captcha GetCaptcha(Captcha captcha)
        {
            return this.GetCaptcha(captcha.Id);
        }


        public string GetText(int id)
        {
            return this.GetCaptcha(id).Text;
        }


        public string GetText(Captcha captcha)
        {
            return this.GetCaptcha(captcha).Text;
        }



		abstract public Captcha Upload(byte[] img, Hashtable ext_data = null);


		public Captcha Upload(Stream st, Hashtable ext_data = null)
        {
			return this.Upload(this.Load(st), ext_data);
        }


		public Captcha Upload(string fn, Hashtable ext_data = null)
        {
			return this.Upload(this.Load(fn), ext_data);
        }



        abstract public bool Report(int id);


        public bool Report(Captcha captcha)
        {
            main.solvedCaptchas--;
            return this.Report(captcha.Id);
        }


		public Captcha Decode(byte[] img, int timeout, Hashtable ext_data = null)
        {
			return this.Poll(this.Upload(img, ext_data), timeout);
        }



		public void Decode(DecodeDelegate callback, byte[] img, int timeout, Hashtable ext_data = null)
        {
            PollPayload payload = new PollPayload();
            payload.Callback = callback;
			payload.Captcha = this.Upload(img, ext_data);
            payload.Timeout = timeout;
            new Thread(PollWithCallback).Start(payload);
        }


		public Captcha Decode(byte[] img, Hashtable ext_data = null)
        {
			return this.Decode(img, 0, ext_data);
        }


		public void Decode(DecodeDelegate callback, byte[] img, Hashtable ext_data = null)
        {
			this.Decode(callback, img, 0, ext_data);
        }


		public Captcha Decode(Stream st, int timeout, Hashtable ext_data = null)
        {
			return this.Decode(this.Load(st), timeout, ext_data);
        }


		public void Decode(DecodeDelegate callback, Stream st, int timeout, Hashtable ext_data = null)
        {
			this.Decode(callback, this.Load(st), timeout, ext_data);
        }


		public Captcha Decode(Stream st, Hashtable ext_data = null)
        {
			return this.Decode(st, 0, ext_data);
        }


		public void Decode(DecodeDelegate callback, Stream st, Hashtable ext_data = null)
        {
			this.Decode(callback, this.Load(st), 0, ext_data);
        }


		public Captcha Decode(string fn, int timeout, Hashtable ext_data = null)
        {
			return this.Decode(this.Load(fn), timeout, ext_data);
        }


		public void Decode(DecodeDelegate callback, string fn, int timeout, Hashtable ext_data = null)
        {
			this.Decode(callback, this.Load(fn), timeout, ext_data);
        }


		public Captcha Decode(string fn, Hashtable ext_data = null)
        {
			return this.Decode(fn, 0, ext_data);
        }


		public void Decode(DecodeDelegate callback, string fn, Hashtable ext_data = null)
        {
			this.Decode(callback, this.Load(fn), 0, ext_data);
        }
    }
}
