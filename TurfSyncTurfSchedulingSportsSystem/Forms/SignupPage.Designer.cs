namespace singup
{
    partial class SignupPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignupPage));
            this.fullName = new System.Windows.Forms.Label();
            this.eMail = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.passWord = new System.Windows.Forms.Label();
            this.repeatPass = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.signUp = new System.Windows.Forms.Label();
            this.signUpp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // fullName
            // 
            this.fullName.AutoSize = true;
            this.fullName.BackColor = System.Drawing.Color.Transparent;
            this.fullName.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullName.ForeColor = System.Drawing.Color.White;
            this.fullName.Image = ((System.Drawing.Image)(resources.GetObject("fullName.Image")));
            this.fullName.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.fullName.Location = new System.Drawing.Point(41, 183);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(175, 40);
            this.fullName.TabIndex = 1;
            this.fullName.Text = "Full Name";
            this.fullName.Click += new System.EventHandler(this.label1_Click);
            // 
            // eMail
            // 
            this.eMail.AutoSize = true;
            this.eMail.BackColor = System.Drawing.Color.Transparent;
            this.eMail.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eMail.ForeColor = System.Drawing.Color.White;
            this.eMail.Image = ((System.Drawing.Image)(resources.GetObject("eMail.Image")));
            this.eMail.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.eMail.Location = new System.Drawing.Point(41, 271);
            this.eMail.Name = "eMail";
            this.eMail.Size = new System.Drawing.Size(101, 40);
            this.eMail.TabIndex = 2;
            this.eMail.Text = "Email";
            this.eMail.Click += new System.EventHandler(this.eMail_Click);
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.BackColor = System.Drawing.Color.Transparent;
            this.userName.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.ForeColor = System.Drawing.Color.White;
            this.userName.Image = ((System.Drawing.Image)(resources.GetObject("userName.Image")));
            this.userName.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.userName.Location = new System.Drawing.Point(37, 374);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(179, 40);
            this.userName.TabIndex = 3;
            this.userName.Text = "Username";
            this.userName.Click += new System.EventHandler(this.userName_Click);
            // 
            // passWord
            // 
            this.passWord.AutoSize = true;
            this.passWord.BackColor = System.Drawing.Color.Transparent;
            this.passWord.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passWord.ForeColor = System.Drawing.Color.White;
            this.passWord.Image = ((System.Drawing.Image)(resources.GetObject("passWord.Image")));
            this.passWord.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.passWord.Location = new System.Drawing.Point(41, 475);
            this.passWord.Name = "passWord";
            this.passWord.Size = new System.Drawing.Size(169, 40);
            this.passWord.TabIndex = 4;
            this.passWord.Text = "Password";
            this.passWord.Click += new System.EventHandler(this.passWord_Click);
            // 
            // repeatPass
            // 
            this.repeatPass.AutoSize = true;
            this.repeatPass.BackColor = System.Drawing.Color.Transparent;
            this.repeatPass.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repeatPass.ForeColor = System.Drawing.Color.White;
            this.repeatPass.Image = ((System.Drawing.Image)(resources.GetObject("repeatPass.Image")));
            this.repeatPass.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.repeatPass.Location = new System.Drawing.Point(41, 571);
            this.repeatPass.Name = "repeatPass";
            this.repeatPass.Size = new System.Drawing.Size(301, 40);
            this.repeatPass.TabIndex = 5;
            this.repeatPass.Text = "Repeat Password";
            this.repeatPass.Click += new System.EventHandler(this.repeatPass_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Snow;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox1.Location = new System.Drawing.Point(43, 315);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(338, 28);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.Snow;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox2.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox2.Location = new System.Drawing.Point(43, 519);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(338, 28);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.Color.Snow;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox3.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox3.Location = new System.Drawing.Point(36, 418);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(338, 28);
            this.textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.BackColor = System.Drawing.Color.Snow;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox4.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox4.Location = new System.Drawing.Point(43, 615);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(338, 28);
            this.textBox4.TabIndex = 9;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.BackColor = System.Drawing.Color.Snow;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.textBox5.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox5.Location = new System.Drawing.Point(43, 227);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(338, 28);
            this.textBox5.TabIndex = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(124, 705);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(257, 25);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "I agree to the terms of user";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // signUp
            // 
            this.signUp.AutoSize = true;
            this.signUp.BackColor = System.Drawing.Color.Transparent;
            this.signUp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.signUp.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signUp.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.signUp.Image = global::TurfSyncTurfSchedulingSportsSystem.Properties.Resources._5335765;
            this.signUp.Location = new System.Drawing.Point(536, 115);
            this.signUp.Name = "signUp";
            this.signUp.Size = new System.Drawing.Size(370, 42);
            this.signUp.TabIndex = 13;
            this.signUp.Text = "Create Your Account";
            this.signUp.Click += new System.EventHandler(this.signUp_Click);
            // 
            // signUpp
            // 
            this.signUpp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.signUpp.BackColor = System.Drawing.Color.SeaGreen;
            this.signUpp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.signUpp.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signUpp.ForeColor = System.Drawing.Color.White;
            this.signUpp.Image = global::TurfSyncTurfSchedulingSportsSystem.Properties.Resources._5335765;
            this.signUpp.Location = new System.Drawing.Point(672, 669);
            this.signUpp.Name = "signUpp";
            this.signUpp.Size = new System.Drawing.Size(269, 61);
            this.signUpp.TabIndex = 14;
            this.signUpp.Text = "SIGN UP";
            this.signUpp.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::TurfSyncTurfSchedulingSportsSystem.Properties.Resources._5335765;
            this.pictureBox1.Location = new System.Drawing.Point(1, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1530, 933);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // SignupPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(1533, 929);
            this.Controls.Add(this.signUp);
            this.Controls.Add(this.signUpp);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.repeatPass);
            this.Controls.Add(this.passWord);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.eMail);
            this.Controls.Add(this.fullName);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SignupPage";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label fullName;
        private System.Windows.Forms.Label eMail;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label passWord;
        private System.Windows.Forms.Label repeatPass;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label signUp;
        private System.Windows.Forms.Button signUpp;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

