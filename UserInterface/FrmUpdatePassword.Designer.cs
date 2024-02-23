namespace UserInterface
{
    partial class FrmUpdatePassword
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnNewPassword = new System.Windows.Forms.Button();
            this.ChkLookPassword = new System.Windows.Forms.CheckBox();
            this.TxtPassword2 = new System.Windows.Forms.TextBox();
            this.TxtPassword1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnNewPassword);
            this.groupBox1.Controls.Add(this.ChkLookPassword);
            this.groupBox1.Controls.Add(this.TxtPassword2);
            this.groupBox1.Controls.Add(this.TxtPassword1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 221);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bilgiler";
            // 
            // BtnNewPassword
            // 
            this.BtnNewPassword.Location = new System.Drawing.Point(151, 171);
            this.BtnNewPassword.Name = "BtnNewPassword";
            this.BtnNewPassword.Size = new System.Drawing.Size(130, 44);
            this.BtnNewPassword.TabIndex = 5;
            this.BtnNewPassword.Text = "Şifreyi Yenile";
            this.BtnNewPassword.UseVisualStyleBackColor = true;
            this.BtnNewPassword.Click += new System.EventHandler(this.BtnNewPassword_Click);
            // 
            // ChkLookPassword
            // 
            this.ChkLookPassword.AutoSize = true;
            this.ChkLookPassword.Location = new System.Drawing.Point(181, 115);
            this.ChkLookPassword.Name = "ChkLookPassword";
            this.ChkLookPassword.Size = new System.Drawing.Size(117, 22);
            this.ChkLookPassword.TabIndex = 4;
            this.ChkLookPassword.Text = "Şifreyi Göster";
            this.ChkLookPassword.UseVisualStyleBackColor = true;
            this.ChkLookPassword.CheckedChanged += new System.EventHandler(this.ChkLookPassword_CheckedChanged);
            // 
            // TxtPassword2
            // 
            this.TxtPassword2.Location = new System.Drawing.Point(138, 81);
            this.TxtPassword2.Name = "TxtPassword2";
            this.TxtPassword2.Size = new System.Drawing.Size(189, 24);
            this.TxtPassword2.TabIndex = 3;
            this.TxtPassword2.UseSystemPasswordChar = true;
            // 
            // TxtPassword1
            // 
            this.TxtPassword1.Location = new System.Drawing.Point(138, 40);
            this.TxtPassword1.Name = "TxtPassword1";
            this.TxtPassword1.Size = new System.Drawing.Size(189, 24);
            this.TxtPassword1.TabIndex = 2;
            this.TxtPassword1.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Yeni Şifre(Tekrar):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yeni Şifre:";
            // 
            // FrmUpdatePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(433, 246);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmUpdatePassword";
            this.ShowIcon = false;
            this.Text = "Şifre Güncelleme Paneli";
            this.Load += new System.EventHandler(this.FrmUpdatePassword_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnNewPassword;
        private System.Windows.Forms.CheckBox ChkLookPassword;
        private System.Windows.Forms.TextBox TxtPassword2;
        private System.Windows.Forms.TextBox TxtPassword1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}