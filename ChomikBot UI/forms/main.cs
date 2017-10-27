using ChomikBot_;
using ChomikBot_Framework;
using MaterialSkin;
using MaterialSkin.Controls;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChomikBot_UI {

    public partial class main : MaterialForm {

        #region globals
        public static RestClient client = new RestClient("http://chomikuj.pl");
        public static CookieContainer _cook = new CookieContainer();

        // fur UI
        public static int solvedCaptchas = 0;
        public static int reportedCaptchas = 0;
        public static int sentPMs = 0;
        public static int commentsSent = 0;
        public static int friendsAdded = 0;
        #endregion

        #region UI
        public main() {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightGreen500, Primary.LightGreen600, Primary.LightBlue200, Accent.LightBlue200, TextShade.WHITE);
            _main = this;

            settingsBtn.TabStop = false;
            settingsBtn.FlatStyle = FlatStyle.Flat;
            settingsBtn.FlatAppearance.BorderSize = 0;

            settingsBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            settingsBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);

            logBox.BackColor = Color.FromArgb(59, 61, 64);
            logBox.SendToBack();
        }

        private void sendCRcount_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void crCount_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void addFriendCount_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void logBox_TextChanged(object sender, EventArgs e) {
            // auto-scroll
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }

        public static main _main;
        public bool firstAppend = true;
        public void writeActionLog(string message) {
            message = "> " + message.ToUpper();
            if (string.IsNullOrWhiteSpace(message)) {
                return;
            }

            if (firstAppend == false) {
                if (logBox.InvokeRequired) {
                    logBox.Invoke(new MethodInvoker(delegate { logBox.AppendText(Environment.NewLine); }));
                } else {
                    logBox.AppendText(Environment.NewLine);
                }
            }

            if (logBox.InvokeRequired) {
                RichTextBoxExtensions.AppendText(logBox, message, Color.FromArgb(103, 146, 214));
            } else {
                RichTextBoxExtensions.AppendText(logBox, message, Color.FromArgb(103, 146, 214));
            }

            firstAppend = false;
        }

        private void refreshStats_Tick(object sender, EventArgs e) {
            statCaptcha.Text = "Solved captchas: " + solvedCaptchas.ToString();
            reportedLabel.Text = "Wrongly solved captchas: " + reportedCaptchas.ToString();
            statPM.Text = "Sent PMs: " + sentPMs.ToString();
            statFriend.Text = "Sent comments: " + commentsSent.ToString();
            statComment.Text = "Recommends added: " + friendsAdded.ToString();
        }
        #endregion

        private void loginBtn_Click(object sender, EventArgs e) {
            if (!account.login(loginTxt.Text, passTxt.Text)) {
                writeActionLog("PRZELOGOWANIE NIEPOMYŚLNE: " + account.loginError);
            } else {
                writeActionLog("PRZELOGOWANIE POMYŚLNIE");
            }
        }

        private void main_Load(object sender, EventArgs e) {
            // Inits
            client.CookieContainer = _cook;
            var ini = new iniHandler();

            // Validate .ini Settings
            if (!helpers.validateSettings()) {
                 MessageBox.Show("Nie można było załadować niektórych ustawień, popraw je w: " + Assembly.GetExecutingAssembly().GetName().Name + ".ini");
                 Environment.Exit(0);
            }

            if (!account.login(ini.Read("chomik_username", "CREDENTIALS"), ini.Read("chomik_password", "CREDENTIALS"))) {
                writeActionLog("LOGOWANIE NIEPOMYŚLNE: " + account.loginError);
            } else {
                writeActionLog("ZALOGOWANO POMYŚLNIE JAKO " + ini.Read("chomik_username", "CREDENTIALS"));
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e) {
            settings frm = new settings();
            frm.ShowDialog();
        }

        public static string previewFrom = "";
        public static string previewTo = "";
        public static string previewTitle = "";
        public static string previewBody = "";
        private void sendPMpreview_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(pmBodyTxt.Text)) {
                return;
            }
            previewBody = pmBodyTxt.Text;
            preview prv = new preview();
            prv.ShowDialog();
        }

        private void sendPMBtn_Click(object sender, EventArgs e) {
            if (pmRandomRadio.Checked == false && pmSpecificRadio.Checked == false) {
                MessageBox.Show("Brakuje danych");
                return;
            }

            if (pmSpecificRadio.Checked == true && string.IsNullOrWhiteSpace(pmSpecificTxt.Text)) {
                MessageBox.Show("Brakuje danych");
                return;
            }

            if (string.IsNullOrWhiteSpace(pmSubjectTxt.Text) || string.IsNullOrWhiteSpace(pmCountTxt.Text)) {
                MessageBox.Show("Brakuje danych");
                return;
            }

            int count = int.Parse(pmCountTxt.Text);

            writeActionLog("WYSYŁANIE...");
            if (pmRandomRadio.Checked == true) {
                List<string> scrapeLastSeen = usr_scraper.scrapeLastSeen(count);
                foreach (var user in scrapeLastSeen) {
                    if (action.sendPrivateMessage(user, pmSubjectTxt.Text, pmBodyTxt.Text)) {
                        writeActionLog("WYSŁANO WIADOMOŚĆ DO " + user);
                    } else {
                        writeActionLog("NIE UDAŁO SIĘ WYSŁAĆ WIADOMOŚCI");
                    }
                }

            } else if (pmSpecificRadio.Checked == true) {
                for (int i = 0; i < count; i++) {
                    if (action.sendPrivateMessage(pmSpecificTxt.Text, pmSubjectTxt.Text, pmBodyTxt.Text)) {
                        writeActionLog("WYSŁANO WIADOMOŚĆ DO " + pmSpecificTxt.Text);
                    } else {
                        writeActionLog("NIE UDAŁO SIĘ WYSŁAĆ WIADOMOŚCI");
                    }
                }
            }
        
        }

        private void createAcc_Click(object sender, EventArgs e) {
            string random = helpers.GetRandomNumber(1, 100000).ToString();
            string random2 = helpers.GetRandomNumber(1000000, 10000000).ToString();

            writeActionLog("TWORZENIE KONTA ROZPOCZĘTE...");
            Task.Factory.StartNew(() => account.register(helpers.randomName() + random, random2));

            //    writeActionLog("NIE UDAŁO SIĘ STWORZYĆ KONTA: " + account.regisError);
            
        }

        private void changeAvatar_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Obrazki (*.BMP;*.JPG;*.GIF,*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Title = "Wybierz obrazek";
            if (dialog.ShowDialog() == DialogResult.OK) {
                Task.Factory.StartNew(() => account.changeAvatar(dialog.FileName));
            }
        }

        private void prvBtn_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(crBodyTxt.Text)) {
                return;
            }
            previewBody = crBodyTxt.Text;
            preview prv = new preview();
            prv.ShowDialog();
        }

        private void crGoBtn_Click(object sender, EventArgs e) {
            if (crRadioRandom.Checked == false && crRadioSpecific.Checked == false) {
                MessageBox.Show("Missing data");
                return;
            }

            if (crRadioSpecific.Checked == true && string.IsNullOrWhiteSpace(crSpecificTxt.Text)) {
                MessageBox.Show("Missing data");
                return;
            }

            if (string.IsNullOrWhiteSpace(crCount.Text)) {
                MessageBox.Show("Missing data");
                return;
            }

            int count = int.Parse(crCount.Text);

            writeActionLog("Sending...");
            if (crRadioRandom.Checked == true) {
                List<string> scrapeLastSeen = usr_scraper.scrapeLastSeen(count);
                foreach (var user in scrapeLastSeen) {
                    if (action.sendChatBoxMessage(user, crBodyTxt.Text)) {
                        writeActionLog("Sent to profile comments of: " + user);
                    } else {
                        writeActionLog("Couldn't send a message");
                    }
                }

            } else if (crRadioSpecific.Checked == true) {
                for (int i = 0; i < count; i++) {
                    if (action.sendChatBoxMessage(crSpecificTxt.Text, crBodyTxt.Text)) {
                        writeActionLog("Sent to profile comments of: " + crSpecificTxt.Text);
                    } else {
                        writeActionLog("Couldn't send a message");
                    }
                }
            }

        }

        private void addFriend_Click(object sender, EventArgs e) {

            if(string.IsNullOrWhiteSpace(addFriendCount.Text)) {
                MessageBox.Show("Missing data");
                return;
            }

            int count = Int32.Parse(addFriendCount.Text);

            if(count >= 250) {
                MessageBox.Show("Obecny algorytm nie pozwala na dodawanie wiecej niz 250 uzytkownikow naraz.");
                return;
            }

            writeActionLog("DODAWANIE ZNAJOMYCH...");

            List<string> scrapeLastSeen = usr_scraper.scrapeLastSeen(500);
            for (int i = 0; i < count; i++) {
                Task.Factory.StartNew(() => action.addFriend(scrapeLastSeen[helpers.GetRandomNumber(1, scrapeLastSeen.Count)], scrapeLastSeen));
            }
        }

        private void uploadFile_Click(object sender, EventArgs e) {
            //MessageBox.Show("Ta funkcja jest w trakcie rozwoju.");
            fileManager frm = new fileManager();
            frm.ShowDialog();
        }

        private void setLegalNote_Click(object sender, EventArgs e) {
            Task.Factory.StartNew(() => account.changeDesc(@"<div style='border: 12px solid #FFFFFF; background-color: #ffffff; text-align: center;'><center><h3>Pobierając pliki z mojego chomika, akceptujesz poniższy regulamin: ! U W A G A ! Zgodnie z polskim prawem ściągnięte pliki posiadające prawa autorskie można przechowywać na dysku przez 24h. Administrator tego chomika nie odpowiada za znajdujące się tu pliki. Jeśli chcesz pobrać pliki musisz po 24h (od momentu pobrania ich na dysk) skasować je ze swojego komputera! Pliki są tu umieszczone w celach edukacyjnych i promocyjnych. Pliki umieszczane na chomiku są własności nie jednej osoby, których poprzez udostępnianie loginu i hasła tożsamość właściciela plików ciężko stwierdzić. R E G U L A M I N: Pliki można pobierać jeśli posiada się ich oryginalną wersję, a ściągnięty plik będzie służył jako kopia zapasowa w razie uszkodzenia oryginalnej... Możesz pobierać pliki w celach edukacyjnych jeśli nie posiadasz ich oryginalnej wersji pod warunkiem, że po 24 godzinach zostaną one usunięte z Twojego dysku. Nie ponoszę żadnej odpowiedzialności za udostępnione pliki i za ich niepożądane działanie. Jeżeli jesteś związany w jakikolwiek sposób z jakąś organizacją rządowa do walki z piractwem komputerowym lub jakąkolwiek inną grupą spełniającą tę funkcję lub jeśli jesteś pracownikiem tych grup nie możesz ściągać żadnych plików z tej strony. Jeśli pobierzesz jakiś plik złamiesz międzynarodowe prawo do prywatności (Internet Privacy Act podpisany w 1995) co będzie znaczyło, że nie możesz rościć praw do ukarania firm (serwerów), na których przetrzymywane są pliki. Nie możesz także sądownie oskarżać żadnej osoby związanej z tą stroną, włączając w to rodzinę, przyjaciół autorów strony oraz wszystkich, którzy tę stronę internetową odwiedzają. Wszystkie pliki MP3, aplikacje oraz pliki filmowe, zdjęcia znajdujące się na tej stronie, prezentowane są wyłącznie w celach promocyjnych. Są to jedynie próbki, które mają Was zachęcić do zakupienia całego albumu, gry, filmu lub aplikacji. Od momentu ściągnięcia na dysk, pliki należy usunąć w przeciągu 24 godzin. Wykorzystywanie ich w celach komercyjnych jest niezgodne z polskim prawem. Autor serwisu nie ponosi żadnej odpowiedzialności za niezgodne z prawem wykorzystywanie zasobów znajdujących się na stronie. Wszystkie pliki ściągacie na własną odpowiedzialność!!! USTAWA z dnia 4 lutego 1994 r. o prawie autorskim i prawach pokrewnych: Art. 23. 1. Bez zezwolenia twórcy wolno nieodpłatnie korzystać z już rozpowszechnionego utworu w zakresie własnego użytku osobistego. Art. 231. Nie wymaga zezwolenia twórcy przejściowe lub incydentalne zwielokrotnianie utworów, niemające samodzielnego znaczenia gospodarczego. Art. 34. Można korzystać z utworów w granicach dozwolonego użytku pod warunkiem wymienienia imienia i nazwiska twórcy oraz źródła. Podanie twórcy i źródła powinno uwzględniać istniejące możliwości. Twórcy nie przysługuje prawo do wynagrodzenia.</h3></center></div>"));
            writeActionLog("USTAWIONO LEGALNĄ NOTKĘ");
        }

        private void lockProfile_Click(object sender, EventArgs e) {
            Task.Factory.StartNew(() => account.lockProfile(helpers.RandomString(7) + "@"));
        }

        private void unlockProfile_Click(object sender, EventArgs e) {
            Task.Factory.StartNew(() => account.unlockProfile());
        }
    }

    #region UI Class
    public static class RichTextBoxExtensions {
        public static void AppendText(this RichTextBox box, string text, Color color) {

            if (box.InvokeRequired) {

                box.Invoke(new MethodInvoker(delegate { box.SelectionStart = box.TextLength; }));
                box.Invoke(new MethodInvoker(delegate { box.SelectionLength = 0; }));

                box.Invoke(new MethodInvoker(delegate { box.SelectionColor = color; }));
                box.Invoke(new MethodInvoker(delegate { box.AppendText(text); }));
                box.Invoke(new MethodInvoker(delegate { box.SelectionColor = box.ForeColor; }));

            } else {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            }

        }
    }
    #endregion
}