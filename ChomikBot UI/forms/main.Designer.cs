namespace ChomikBot_UI {
    partial class main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.loginBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.statsGroupBox = new System.Windows.Forms.GroupBox();
            this.reportedLabel = new MaterialSkin.Controls.MaterialLabel();
            this.statCaptcha = new MaterialSkin.Controls.MaterialLabel();
            this.statPM = new MaterialSkin.Controls.MaterialLabel();
            this.statComment = new MaterialSkin.Controls.MaterialLabel();
            this.statFriend = new MaterialSkin.Controls.MaterialLabel();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.passTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.loginTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.ReLoginGroupBox = new System.Windows.Forms.GroupBox();
            this.refreshStats = new System.Windows.Forms.Timer(this.components);
            this.actionGroupBox = new System.Windows.Forms.GroupBox();
            this.addFriendGroupBox = new System.Windows.Forms.GroupBox();
            this.addFriend = new MaterialSkin.Controls.MaterialRaisedButton();
            this.addFriendCount = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.miscGroupBox = new System.Windows.Forms.GroupBox();
            this.unlockProfile = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lockProfile = new MaterialSkin.Controls.MaterialRaisedButton();
            this.setLegalNote = new MaterialSkin.Controls.MaterialRaisedButton();
            this.uploadFile = new MaterialSkin.Controls.MaterialRaisedButton();
            this.changeAvatar = new MaterialSkin.Controls.MaterialRaisedButton();
            this.createAcc = new MaterialSkin.Controls.MaterialRaisedButton();
            this.crGroupBox = new System.Windows.Forms.GroupBox();
            this.crBodyTxt = new System.Windows.Forms.TextBox();
            this.crBodyLabel = new MaterialSkin.Controls.MaterialLabel();
            this.prvBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.crCount = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.crSpecificTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.crGoBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.crRadioRandom = new MaterialSkin.Controls.MaterialRadioButton();
            this.crRadioSpecific = new MaterialSkin.Controls.MaterialRadioButton();
            this.pmSendGroupBox = new System.Windows.Forms.GroupBox();
            this.pmSubjectTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.pmBodyTxt = new System.Windows.Forms.TextBox();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.sendPMpreview = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pmCountTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.pmSpecificTxt = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.sendPMBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.pmRandomRadio = new MaterialSkin.Controls.MaterialRadioButton();
            this.pmSpecificRadio = new MaterialSkin.Controls.MaterialRadioButton();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.statsGroupBox.SuspendLayout();
            this.ReLoginGroupBox.SuspendLayout();
            this.actionGroupBox.SuspendLayout();
            this.addFriendGroupBox.SuspendLayout();
            this.miscGroupBox.SuspendLayout();
            this.crGroupBox.SuspendLayout();
            this.pmSendGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginBtn
            // 
            this.loginBtn.Depth = 0;
            this.loginBtn.Location = new System.Drawing.Point(6, 77);
            this.loginBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Primary = true;
            this.loginBtn.Size = new System.Drawing.Size(152, 34);
            this.loginBtn.TabIndex = 1;
            this.loginBtn.Text = "LOGIN";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // statsGroupBox
            // 
            this.statsGroupBox.Controls.Add(this.reportedLabel);
            this.statsGroupBox.Controls.Add(this.statCaptcha);
            this.statsGroupBox.Controls.Add(this.statPM);
            this.statsGroupBox.Controls.Add(this.statComment);
            this.statsGroupBox.Controls.Add(this.statFriend);
            this.statsGroupBox.Location = new System.Drawing.Point(190, 74);
            this.statsGroupBox.Name = "statsGroupBox";
            this.statsGroupBox.Size = new System.Drawing.Size(226, 119);
            this.statsGroupBox.TabIndex = 2;
            this.statsGroupBox.TabStop = false;
            this.statsGroupBox.Text = "Statystyki";
            // 
            // reportedLabel
            // 
            this.reportedLabel.AutoSize = true;
            this.reportedLabel.Depth = 0;
            this.reportedLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.reportedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.reportedLabel.Location = new System.Drawing.Point(6, 38);
            this.reportedLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.reportedLabel.Name = "reportedLabel";
            this.reportedLabel.Size = new System.Drawing.Size(179, 19);
            this.reportedLabel.TabIndex = 8;
            this.reportedLabel.Text = "Źle rozwiązanych Captch:";
            // 
            // statCaptcha
            // 
            this.statCaptcha.AutoSize = true;
            this.statCaptcha.Depth = 0;
            this.statCaptcha.Font = new System.Drawing.Font("Roboto", 11F);
            this.statCaptcha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statCaptcha.Location = new System.Drawing.Point(6, 19);
            this.statCaptcha.MouseState = MaterialSkin.MouseState.HOVER;
            this.statCaptcha.Name = "statCaptcha";
            this.statCaptcha.Size = new System.Drawing.Size(205, 19);
            this.statCaptcha.TabIndex = 4;
            this.statCaptcha.Text = "Dobrze rozwiązanych Captch:";
            // 
            // statPM
            // 
            this.statPM.AutoSize = true;
            this.statPM.Depth = 0;
            this.statPM.Font = new System.Drawing.Font("Roboto", 11F);
            this.statPM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statPM.Location = new System.Drawing.Point(6, 57);
            this.statPM.MouseState = MaterialSkin.MouseState.HOVER;
            this.statPM.Name = "statPM";
            this.statPM.Size = new System.Drawing.Size(133, 19);
            this.statPM.TabIndex = 5;
            this.statPM.Text = "Wysłanych PM\'ów:";
            // 
            // statComment
            // 
            this.statComment.AutoSize = true;
            this.statComment.Depth = 0;
            this.statComment.Font = new System.Drawing.Font("Roboto", 11F);
            this.statComment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statComment.Location = new System.Drawing.Point(6, 77);
            this.statComment.MouseState = MaterialSkin.MouseState.HOVER;
            this.statComment.Name = "statComment";
            this.statComment.Size = new System.Drawing.Size(167, 19);
            this.statComment.TabIndex = 6;
            this.statComment.Text = "Wysłanych Komentarzy:";
            // 
            // statFriend
            // 
            this.statFriend.AutoSize = true;
            this.statFriend.Depth = 0;
            this.statFriend.Font = new System.Drawing.Font("Roboto", 11F);
            this.statFriend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.statFriend.Location = new System.Drawing.Point(6, 96);
            this.statFriend.MouseState = MaterialSkin.MouseState.HOVER;
            this.statFriend.Name = "statFriend";
            this.statFriend.Size = new System.Drawing.Size(160, 19);
            this.statFriend.TabIndex = 7;
            this.statFriend.Text = "Dodanych Polecanych:";
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(422, 74);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(388, 119);
            this.logBox.TabIndex = 3;
            this.logBox.Text = "";
            // 
            // passTxt
            // 
            this.passTxt.Depth = 0;
            this.passTxt.Hint = "Hasło";
            this.passTxt.Location = new System.Drawing.Point(6, 48);
            this.passTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.passTxt.Name = "passTxt";
            this.passTxt.PasswordChar = '\0';
            this.passTxt.SelectedText = "";
            this.passTxt.SelectionLength = 0;
            this.passTxt.SelectionStart = 0;
            this.passTxt.Size = new System.Drawing.Size(152, 23);
            this.passTxt.TabIndex = 4;
            this.passTxt.UseSystemPasswordChar = false;
            // 
            // loginTxt
            // 
            this.loginTxt.Depth = 0;
            this.loginTxt.Hint = "Login";
            this.loginTxt.Location = new System.Drawing.Point(6, 19);
            this.loginTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.loginTxt.Name = "loginTxt";
            this.loginTxt.PasswordChar = '\0';
            this.loginTxt.SelectedText = "";
            this.loginTxt.SelectionLength = 0;
            this.loginTxt.SelectionStart = 0;
            this.loginTxt.Size = new System.Drawing.Size(152, 23);
            this.loginTxt.TabIndex = 5;
            this.loginTxt.UseSystemPasswordChar = false;
            // 
            // ReLoginGroupBox
            // 
            this.ReLoginGroupBox.Controls.Add(this.loginBtn);
            this.ReLoginGroupBox.Controls.Add(this.loginTxt);
            this.ReLoginGroupBox.Controls.Add(this.passTxt);
            this.ReLoginGroupBox.Location = new System.Drawing.Point(12, 74);
            this.ReLoginGroupBox.Name = "ReLoginGroupBox";
            this.ReLoginGroupBox.Size = new System.Drawing.Size(172, 119);
            this.ReLoginGroupBox.TabIndex = 0;
            this.ReLoginGroupBox.TabStop = false;
            this.ReLoginGroupBox.Text = "In-site Login";
            // 
            // refreshStats
            // 
            this.refreshStats.Enabled = true;
            this.refreshStats.Interval = 5000;
            this.refreshStats.Tick += new System.EventHandler(this.refreshStats_Tick);
            // 
            // actionGroupBox
            // 
            this.actionGroupBox.Controls.Add(this.addFriendGroupBox);
            this.actionGroupBox.Controls.Add(this.miscGroupBox);
            this.actionGroupBox.Controls.Add(this.setLegalNote);
            this.actionGroupBox.Controls.Add(this.crGroupBox);
            this.actionGroupBox.Controls.Add(this.pmSendGroupBox);
            this.actionGroupBox.Location = new System.Drawing.Point(12, 199);
            this.actionGroupBox.Name = "actionGroupBox";
            this.actionGroupBox.Size = new System.Drawing.Size(798, 314);
            this.actionGroupBox.TabIndex = 4;
            this.actionGroupBox.TabStop = false;
            this.actionGroupBox.Text = "Akcje";
            // 
            // addFriendGroupBox
            // 
            this.addFriendGroupBox.Controls.Add(this.addFriend);
            this.addFriendGroupBox.Controls.Add(this.addFriendCount);
            this.addFriendGroupBox.Location = new System.Drawing.Point(610, 242);
            this.addFriendGroupBox.Name = "addFriendGroupBox";
            this.addFriendGroupBox.Size = new System.Drawing.Size(182, 61);
            this.addFriendGroupBox.TabIndex = 15;
            this.addFriendGroupBox.TabStop = false;
            this.addFriendGroupBox.Text = "Add to recommended";
            // 
            // addFriend
            // 
            this.addFriend.Depth = 0;
            this.addFriend.Location = new System.Drawing.Point(69, 19);
            this.addFriend.MouseState = MaterialSkin.MouseState.HOVER;
            this.addFriend.Name = "addFriend";
            this.addFriend.Primary = true;
            this.addFriend.Size = new System.Drawing.Size(107, 33);
            this.addFriend.TabIndex = 2;
            this.addFriend.Text = "ADD";
            this.addFriend.UseVisualStyleBackColor = true;
            this.addFriend.Click += new System.EventHandler(this.addFriend_Click);
            // 
            // addFriendCount
            // 
            this.addFriendCount.Depth = 0;
            this.addFriendCount.Hint = "count";
            this.addFriendCount.Location = new System.Drawing.Point(16, 24);
            this.addFriendCount.MouseState = MaterialSkin.MouseState.HOVER;
            this.addFriendCount.Name = "addFriendCount";
            this.addFriendCount.PasswordChar = '\0';
            this.addFriendCount.SelectedText = "";
            this.addFriendCount.SelectionLength = 0;
            this.addFriendCount.SelectionStart = 0;
            this.addFriendCount.Size = new System.Drawing.Size(45, 23);
            this.addFriendCount.TabIndex = 14;
            this.addFriendCount.UseSystemPasswordChar = false;
            this.addFriendCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.addFriendCount_KeyPress);
            // 
            // miscGroupBox
            // 
            this.miscGroupBox.Controls.Add(this.unlockProfile);
            this.miscGroupBox.Controls.Add(this.lockProfile);
            this.miscGroupBox.Controls.Add(this.uploadFile);
            this.miscGroupBox.Controls.Add(this.changeAvatar);
            this.miscGroupBox.Controls.Add(this.createAcc);
            this.miscGroupBox.Location = new System.Drawing.Point(610, 19);
            this.miscGroupBox.Name = "miscGroupBox";
            this.miscGroupBox.Size = new System.Drawing.Size(182, 217);
            this.miscGroupBox.TabIndex = 7;
            this.miscGroupBox.TabStop = false;
            this.miscGroupBox.Text = "Misc";
            // 
            // unlockProfile
            // 
            this.unlockProfile.Depth = 0;
            this.unlockProfile.Location = new System.Drawing.Point(6, 178);
            this.unlockProfile.MouseState = MaterialSkin.MouseState.HOVER;
            this.unlockProfile.Name = "unlockProfile";
            this.unlockProfile.Primary = true;
            this.unlockProfile.Size = new System.Drawing.Size(170, 33);
            this.unlockProfile.TabIndex = 5;
            this.unlockProfile.Text = "UNLOCK PROFILE";
            this.unlockProfile.UseVisualStyleBackColor = true;
            this.unlockProfile.Click += new System.EventHandler(this.unlockProfile_Click);
            // 
            // lockProfile
            // 
            this.lockProfile.Depth = 0;
            this.lockProfile.Location = new System.Drawing.Point(6, 139);
            this.lockProfile.MouseState = MaterialSkin.MouseState.HOVER;
            this.lockProfile.Name = "lockProfile";
            this.lockProfile.Primary = true;
            this.lockProfile.Size = new System.Drawing.Size(170, 33);
            this.lockProfile.TabIndex = 4;
            this.lockProfile.Text = "LOCK PROFILE";
            this.lockProfile.UseVisualStyleBackColor = true;
            this.lockProfile.Click += new System.EventHandler(this.lockProfile_Click);
            // 
            // setLegalNote
            // 
            this.setLegalNote.Depth = 0;
            this.setLegalNote.Location = new System.Drawing.Point(616, 0);
            this.setLegalNote.MouseState = MaterialSkin.MouseState.HOVER;
            this.setLegalNote.Name = "setLegalNote";
            this.setLegalNote.Primary = true;
            this.setLegalNote.Size = new System.Drawing.Size(170, 33);
            this.setLegalNote.TabIndex = 3;
            this.setLegalNote.Text = "USTAW LEGALNĄ NOTKĘ";
            this.setLegalNote.UseVisualStyleBackColor = true;
            this.setLegalNote.Visible = false;
            this.setLegalNote.Click += new System.EventHandler(this.setLegalNote_Click);
            // 
            // uploadFile
            // 
            this.uploadFile.Depth = 0;
            this.uploadFile.Location = new System.Drawing.Point(6, 100);
            this.uploadFile.MouseState = MaterialSkin.MouseState.HOVER;
            this.uploadFile.Name = "uploadFile";
            this.uploadFile.Primary = true;
            this.uploadFile.Size = new System.Drawing.Size(170, 33);
            this.uploadFile.TabIndex = 2;
            this.uploadFile.Text = "FILE MANAGER";
            this.uploadFile.UseVisualStyleBackColor = true;
            this.uploadFile.Click += new System.EventHandler(this.uploadFile_Click);
            // 
            // changeAvatar
            // 
            this.changeAvatar.Depth = 0;
            this.changeAvatar.Location = new System.Drawing.Point(6, 61);
            this.changeAvatar.MouseState = MaterialSkin.MouseState.HOVER;
            this.changeAvatar.Name = "changeAvatar";
            this.changeAvatar.Primary = true;
            this.changeAvatar.Size = new System.Drawing.Size(170, 33);
            this.changeAvatar.TabIndex = 1;
            this.changeAvatar.Text = "CHANGE AVATAR";
            this.changeAvatar.UseVisualStyleBackColor = true;
            this.changeAvatar.Click += new System.EventHandler(this.changeAvatar_Click);
            // 
            // createAcc
            // 
            this.createAcc.Depth = 0;
            this.createAcc.Location = new System.Drawing.Point(6, 22);
            this.createAcc.MouseState = MaterialSkin.MouseState.HOVER;
            this.createAcc.Name = "createAcc";
            this.createAcc.Primary = true;
            this.createAcc.Size = new System.Drawing.Size(170, 33);
            this.createAcc.TabIndex = 0;
            this.createAcc.Text = "CREATE ACCOUNT";
            this.createAcc.UseVisualStyleBackColor = true;
            this.createAcc.Click += new System.EventHandler(this.createAcc_Click);
            // 
            // crGroupBox
            // 
            this.crGroupBox.Controls.Add(this.crBodyTxt);
            this.crGroupBox.Controls.Add(this.crBodyLabel);
            this.crGroupBox.Controls.Add(this.prvBtn);
            this.crGroupBox.Controls.Add(this.crCount);
            this.crGroupBox.Controls.Add(this.crSpecificTxt);
            this.crGroupBox.Controls.Add(this.crGoBtn);
            this.crGroupBox.Controls.Add(this.crRadioRandom);
            this.crGroupBox.Controls.Add(this.crRadioSpecific);
            this.crGroupBox.Location = new System.Drawing.Point(308, 19);
            this.crGroupBox.Name = "crGroupBox";
            this.crGroupBox.Size = new System.Drawing.Size(296, 284);
            this.crGroupBox.TabIndex = 14;
            this.crGroupBox.TabStop = false;
            this.crGroupBox.Text = "Spam in profile comments";
            // 
            // crBodyTxt
            // 
            this.crBodyTxt.Location = new System.Drawing.Point(6, 101);
            this.crBodyTxt.Multiline = true;
            this.crBodyTxt.Name = "crBodyTxt";
            this.crBodyTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.crBodyTxt.Size = new System.Drawing.Size(283, 141);
            this.crBodyTxt.TabIndex = 13;
            // 
            // crBodyLabel
            // 
            this.crBodyLabel.AutoSize = true;
            this.crBodyLabel.Depth = 0;
            this.crBodyLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.crBodyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.crBodyLabel.Location = new System.Drawing.Point(6, 79);
            this.crBodyLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.crBodyLabel.Name = "crBodyLabel";
            this.crBodyLabel.Size = new System.Drawing.Size(66, 19);
            this.crBodyLabel.TabIndex = 12;
            this.crBodyLabel.Text = "Content:";
            // 
            // prvBtn
            // 
            this.prvBtn.Depth = 0;
            this.prvBtn.Location = new System.Drawing.Point(116, 248);
            this.prvBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.prvBtn.Name = "prvBtn";
            this.prvBtn.Primary = true;
            this.prvBtn.Size = new System.Drawing.Size(84, 29);
            this.prvBtn.TabIndex = 11;
            this.prvBtn.Text = "PREVIEW";
            this.prvBtn.UseVisualStyleBackColor = true;
            this.prvBtn.Click += new System.EventHandler(this.prvBtn_Click);
            // 
            // crCount
            // 
            this.crCount.Depth = 0;
            this.crCount.Hint = "count";
            this.crCount.Location = new System.Drawing.Point(10, 254);
            this.crCount.MouseState = MaterialSkin.MouseState.HOVER;
            this.crCount.Name = "crCount";
            this.crCount.PasswordChar = '\0';
            this.crCount.SelectedText = "";
            this.crCount.SelectionLength = 0;
            this.crCount.SelectionStart = 0;
            this.crCount.Size = new System.Drawing.Size(46, 23);
            this.crCount.TabIndex = 0;
            this.crCount.UseSystemPasswordChar = false;
            this.crCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.crCount_KeyPress);
            // 
            // crSpecificTxt
            // 
            this.crSpecificTxt.Depth = 0;
            this.crSpecificTxt.Hint = "login";
            this.crSpecificTxt.Location = new System.Drawing.Point(119, 52);
            this.crSpecificTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.crSpecificTxt.Name = "crSpecificTxt";
            this.crSpecificTxt.PasswordChar = '\0';
            this.crSpecificTxt.SelectedText = "";
            this.crSpecificTxt.SelectionLength = 0;
            this.crSpecificTxt.SelectionStart = 0;
            this.crSpecificTxt.Size = new System.Drawing.Size(104, 23);
            this.crSpecificTxt.TabIndex = 9;
            this.crSpecificTxt.UseSystemPasswordChar = false;
            // 
            // crGoBtn
            // 
            this.crGoBtn.Depth = 0;
            this.crGoBtn.Location = new System.Drawing.Point(206, 248);
            this.crGoBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.crGoBtn.Name = "crGoBtn";
            this.crGoBtn.Primary = true;
            this.crGoBtn.Size = new System.Drawing.Size(84, 29);
            this.crGoBtn.TabIndex = 6;
            this.crGoBtn.Text = "SEND";
            this.crGoBtn.UseVisualStyleBackColor = true;
            this.crGoBtn.Click += new System.EventHandler(this.crGoBtn_Click);
            // 
            // crRadioRandom
            // 
            this.crRadioRandom.AutoSize = true;
            this.crRadioRandom.Depth = 0;
            this.crRadioRandom.Font = new System.Drawing.Font("Roboto", 10F);
            this.crRadioRandom.Location = new System.Drawing.Point(3, 19);
            this.crRadioRandom.Margin = new System.Windows.Forms.Padding(0);
            this.crRadioRandom.MouseLocation = new System.Drawing.Point(-1, -1);
            this.crRadioRandom.MouseState = MaterialSkin.MouseState.HOVER;
            this.crRadioRandom.Name = "crRadioRandom";
            this.crRadioRandom.Ripple = true;
            this.crRadioRandom.Size = new System.Drawing.Size(118, 30);
            this.crRadioRandom.TabIndex = 7;
            this.crRadioRandom.TabStop = true;
            this.crRadioRandom.Text = "Random users";
            this.crRadioRandom.UseVisualStyleBackColor = true;
            // 
            // crRadioSpecific
            // 
            this.crRadioSpecific.AutoSize = true;
            this.crRadioSpecific.Depth = 0;
            this.crRadioSpecific.Font = new System.Drawing.Font("Roboto", 10F);
            this.crRadioSpecific.Location = new System.Drawing.Point(3, 49);
            this.crRadioSpecific.Margin = new System.Windows.Forms.Padding(0);
            this.crRadioSpecific.MouseLocation = new System.Drawing.Point(-1, -1);
            this.crRadioSpecific.MouseState = MaterialSkin.MouseState.HOVER;
            this.crRadioSpecific.Name = "crRadioSpecific";
            this.crRadioSpecific.Ripple = true;
            this.crRadioSpecific.Size = new System.Drawing.Size(113, 30);
            this.crRadioSpecific.TabIndex = 8;
            this.crRadioSpecific.TabStop = true;
            this.crRadioSpecific.Text = "Specific user:";
            this.crRadioSpecific.UseVisualStyleBackColor = true;
            // 
            // pmSendGroupBox
            // 
            this.pmSendGroupBox.Controls.Add(this.pmSubjectTxt);
            this.pmSendGroupBox.Controls.Add(this.pmBodyTxt);
            this.pmSendGroupBox.Controls.Add(this.materialLabel2);
            this.pmSendGroupBox.Controls.Add(this.sendPMpreview);
            this.pmSendGroupBox.Controls.Add(this.pmCountTxt);
            this.pmSendGroupBox.Controls.Add(this.pmSpecificTxt);
            this.pmSendGroupBox.Controls.Add(this.sendPMBtn);
            this.pmSendGroupBox.Controls.Add(this.pmRandomRadio);
            this.pmSendGroupBox.Controls.Add(this.pmSpecificRadio);
            this.pmSendGroupBox.Location = new System.Drawing.Point(6, 19);
            this.pmSendGroupBox.Name = "pmSendGroupBox";
            this.pmSendGroupBox.Size = new System.Drawing.Size(296, 284);
            this.pmSendGroupBox.TabIndex = 5;
            this.pmSendGroupBox.TabStop = false;
            this.pmSendGroupBox.Text = "Spam in private message";
            // 
            // pmSubjectTxt
            // 
            this.pmSubjectTxt.Depth = 0;
            this.pmSubjectTxt.Hint = "Message subject";
            this.pmSubjectTxt.Location = new System.Drawing.Point(6, 92);
            this.pmSubjectTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.pmSubjectTxt.Name = "pmSubjectTxt";
            this.pmSubjectTxt.PasswordChar = '\0';
            this.pmSubjectTxt.SelectedText = "";
            this.pmSubjectTxt.SelectionLength = 0;
            this.pmSubjectTxt.SelectionStart = 0;
            this.pmSubjectTxt.Size = new System.Drawing.Size(273, 23);
            this.pmSubjectTxt.TabIndex = 8;
            this.pmSubjectTxt.UseSystemPasswordChar = false;
            // 
            // pmBodyTxt
            // 
            this.pmBodyTxt.Location = new System.Drawing.Point(6, 149);
            this.pmBodyTxt.Multiline = true;
            this.pmBodyTxt.Name = "pmBodyTxt";
            this.pmBodyTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pmBodyTxt.Size = new System.Drawing.Size(283, 93);
            this.pmBodyTxt.TabIndex = 13;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(6, 127);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(66, 19);
            this.materialLabel2.TabIndex = 12;
            this.materialLabel2.Text = "Content:";
            // 
            // sendPMpreview
            // 
            this.sendPMpreview.Depth = 0;
            this.sendPMpreview.Location = new System.Drawing.Point(116, 248);
            this.sendPMpreview.MouseState = MaterialSkin.MouseState.HOVER;
            this.sendPMpreview.Name = "sendPMpreview";
            this.sendPMpreview.Primary = true;
            this.sendPMpreview.Size = new System.Drawing.Size(84, 29);
            this.sendPMpreview.TabIndex = 11;
            this.sendPMpreview.Text = "PREVIEW";
            this.sendPMpreview.UseVisualStyleBackColor = true;
            this.sendPMpreview.Click += new System.EventHandler(this.sendPMpreview_Click);
            // 
            // pmCountTxt
            // 
            this.pmCountTxt.Depth = 0;
            this.pmCountTxt.Hint = "count";
            this.pmCountTxt.Location = new System.Drawing.Point(10, 254);
            this.pmCountTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.pmCountTxt.Name = "pmCountTxt";
            this.pmCountTxt.PasswordChar = '\0';
            this.pmCountTxt.SelectedText = "";
            this.pmCountTxt.SelectionLength = 0;
            this.pmCountTxt.SelectionStart = 0;
            this.pmCountTxt.Size = new System.Drawing.Size(46, 23);
            this.pmCountTxt.TabIndex = 0;
            this.pmCountTxt.UseSystemPasswordChar = false;
            this.pmCountTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sendCRcount_KeyPress);
            // 
            // pmSpecificTxt
            // 
            this.pmSpecificTxt.Depth = 0;
            this.pmSpecificTxt.Hint = "login";
            this.pmSpecificTxt.Location = new System.Drawing.Point(119, 52);
            this.pmSpecificTxt.MouseState = MaterialSkin.MouseState.HOVER;
            this.pmSpecificTxt.Name = "pmSpecificTxt";
            this.pmSpecificTxt.PasswordChar = '\0';
            this.pmSpecificTxt.SelectedText = "";
            this.pmSpecificTxt.SelectionLength = 0;
            this.pmSpecificTxt.SelectionStart = 0;
            this.pmSpecificTxt.Size = new System.Drawing.Size(104, 23);
            this.pmSpecificTxt.TabIndex = 9;
            this.pmSpecificTxt.UseSystemPasswordChar = false;
            // 
            // sendPMBtn
            // 
            this.sendPMBtn.Depth = 0;
            this.sendPMBtn.Location = new System.Drawing.Point(206, 248);
            this.sendPMBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.sendPMBtn.Name = "sendPMBtn";
            this.sendPMBtn.Primary = true;
            this.sendPMBtn.Size = new System.Drawing.Size(84, 29);
            this.sendPMBtn.TabIndex = 6;
            this.sendPMBtn.Text = "SEND";
            this.sendPMBtn.UseVisualStyleBackColor = true;
            this.sendPMBtn.Click += new System.EventHandler(this.sendPMBtn_Click);
            // 
            // pmRandomRadio
            // 
            this.pmRandomRadio.AutoSize = true;
            this.pmRandomRadio.Depth = 0;
            this.pmRandomRadio.Font = new System.Drawing.Font("Roboto", 10F);
            this.pmRandomRadio.Location = new System.Drawing.Point(3, 19);
            this.pmRandomRadio.Margin = new System.Windows.Forms.Padding(0);
            this.pmRandomRadio.MouseLocation = new System.Drawing.Point(-1, -1);
            this.pmRandomRadio.MouseState = MaterialSkin.MouseState.HOVER;
            this.pmRandomRadio.Name = "pmRandomRadio";
            this.pmRandomRadio.Ripple = true;
            this.pmRandomRadio.Size = new System.Drawing.Size(118, 30);
            this.pmRandomRadio.TabIndex = 7;
            this.pmRandomRadio.TabStop = true;
            this.pmRandomRadio.Text = "Random users";
            this.pmRandomRadio.UseVisualStyleBackColor = true;
            // 
            // pmSpecificRadio
            // 
            this.pmSpecificRadio.AutoSize = true;
            this.pmSpecificRadio.Depth = 0;
            this.pmSpecificRadio.Font = new System.Drawing.Font("Roboto", 10F);
            this.pmSpecificRadio.Location = new System.Drawing.Point(3, 49);
            this.pmSpecificRadio.Margin = new System.Windows.Forms.Padding(0);
            this.pmSpecificRadio.MouseLocation = new System.Drawing.Point(-1, -1);
            this.pmSpecificRadio.MouseState = MaterialSkin.MouseState.HOVER;
            this.pmSpecificRadio.Name = "pmSpecificRadio";
            this.pmSpecificRadio.Ripple = true;
            this.pmSpecificRadio.Size = new System.Drawing.Size(113, 30);
            this.pmSpecificRadio.TabIndex = 8;
            this.pmSpecificRadio.TabStop = true;
            this.pmSpecificRadio.Text = "Specific user:";
            this.pmSpecificRadio.UseVisualStyleBackColor = true;
            // 
            // settingsBtn
            // 
            this.settingsBtn.BackColor = System.Drawing.Color.Transparent;
            this.settingsBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("settingsBtn.BackgroundImage")));
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.ForeColor = System.Drawing.Color.Transparent;
            this.settingsBtn.Location = new System.Drawing.Point(777, 28);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(33, 30);
            this.settingsBtn.TabIndex = 6;
            this.settingsBtn.UseVisualStyleBackColor = false;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 526);
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.actionGroupBox);
            this.Controls.Add(this.ReLoginGroupBox);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.statsGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main";
            this.Sizable = false;
            this.Text = "CHOMIK BOT";
            this.Load += new System.EventHandler(this.main_Load);
            this.statsGroupBox.ResumeLayout(false);
            this.statsGroupBox.PerformLayout();
            this.ReLoginGroupBox.ResumeLayout(false);
            this.actionGroupBox.ResumeLayout(false);
            this.addFriendGroupBox.ResumeLayout(false);
            this.miscGroupBox.ResumeLayout(false);
            this.crGroupBox.ResumeLayout(false);
            this.crGroupBox.PerformLayout();
            this.pmSendGroupBox.ResumeLayout(false);
            this.pmSendGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton loginBtn;
        private System.Windows.Forms.GroupBox statsGroupBox;
        private System.Windows.Forms.RichTextBox logBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField passTxt;
        private MaterialSkin.Controls.MaterialSingleLineTextField loginTxt;
        private System.Windows.Forms.GroupBox ReLoginGroupBox;
        private MaterialSkin.Controls.MaterialLabel statCaptcha;
        private MaterialSkin.Controls.MaterialLabel statPM;
        private MaterialSkin.Controls.MaterialLabel statComment;
        private MaterialSkin.Controls.MaterialLabel statFriend;
        private System.Windows.Forms.Timer refreshStats;
        private System.Windows.Forms.GroupBox actionGroupBox;
        private System.Windows.Forms.GroupBox pmSendGroupBox;
        private MaterialSkin.Controls.MaterialRaisedButton sendPMBtn;
        private MaterialSkin.Controls.MaterialRadioButton pmRandomRadio;
        private MaterialSkin.Controls.MaterialRadioButton pmSpecificRadio;
        private MaterialSkin.Controls.MaterialSingleLineTextField pmSpecificTxt;
        private System.Windows.Forms.Button settingsBtn;
        private MaterialSkin.Controls.MaterialSingleLineTextField pmCountTxt;
        private MaterialSkin.Controls.MaterialRaisedButton sendPMpreview;
        private System.Windows.Forms.TextBox pmBodyTxt;
        private MaterialSkin.Controls.MaterialSingleLineTextField pmSubjectTxt;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.GroupBox miscGroupBox;
        private MaterialSkin.Controls.MaterialRaisedButton createAcc;
        private MaterialSkin.Controls.MaterialRaisedButton changeAvatar;
        private MaterialSkin.Controls.MaterialLabel reportedLabel;
        private System.Windows.Forms.GroupBox addFriendGroupBox;
        private System.Windows.Forms.GroupBox crGroupBox;
        private System.Windows.Forms.TextBox crBodyTxt;
        private MaterialSkin.Controls.MaterialLabel crBodyLabel;
        private MaterialSkin.Controls.MaterialRaisedButton prvBtn;
        private MaterialSkin.Controls.MaterialSingleLineTextField crCount;
        private MaterialSkin.Controls.MaterialSingleLineTextField crSpecificTxt;
        private MaterialSkin.Controls.MaterialRaisedButton crGoBtn;
        private MaterialSkin.Controls.MaterialRadioButton crRadioRandom;
        private MaterialSkin.Controls.MaterialRadioButton crRadioSpecific;
        private MaterialSkin.Controls.MaterialRaisedButton addFriend;
        private MaterialSkin.Controls.MaterialSingleLineTextField addFriendCount;
        private MaterialSkin.Controls.MaterialRaisedButton uploadFile;
        private MaterialSkin.Controls.MaterialRaisedButton setLegalNote;
        private MaterialSkin.Controls.MaterialRaisedButton unlockProfile;
        private MaterialSkin.Controls.MaterialRaisedButton lockProfile;
    }
}

