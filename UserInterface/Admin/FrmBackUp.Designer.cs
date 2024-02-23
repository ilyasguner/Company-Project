namespace UserInterface.Admin
{
    partial class FrmBackUp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPath = new System.Windows.Forms.TextBox();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.BtnGozAt = new System.Windows.Forms.Button();
            this.BtnYedekle = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dosya Yolu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dosya Adı:";
            // 
            // TxtPath
            // 
            this.TxtPath.Location = new System.Drawing.Point(106, 24);
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.Size = new System.Drawing.Size(314, 24);
            this.TxtPath.TabIndex = 2;
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(106, 67);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(151, 24);
            this.TxtName.TabIndex = 3;
            // 
            // BtnGozAt
            // 
            this.BtnGozAt.Location = new System.Drawing.Point(436, 24);
            this.BtnGozAt.Name = "BtnGozAt";
            this.BtnGozAt.Size = new System.Drawing.Size(84, 24);
            this.BtnGozAt.TabIndex = 4;
            this.BtnGozAt.Text = "Göz At";
            this.BtnGozAt.UseVisualStyleBackColor = true;
            this.BtnGozAt.Click += new System.EventHandler(this.BtnGozAt_Click);
            // 
            // BtnYedekle
            // 
            this.BtnYedekle.Location = new System.Drawing.Point(198, 121);
            this.BtnYedekle.Name = "BtnYedekle";
            this.BtnYedekle.Size = new System.Drawing.Size(177, 39);
            this.BtnYedekle.TabIndex = 5;
            this.BtnYedekle.Text = "Yedek Dosyayı Al";
            this.BtnYedekle.UseVisualStyleBackColor = true;
            this.BtnYedekle.Click += new System.EventHandler(this.BtnYedekle_Click);
            // 
            // FrmBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 209);
            this.Controls.Add(this.BtnYedekle);
            this.Controls.Add(this.BtnGozAt);
            this.Controls.Add(this.TxtName);
            this.Controls.Add(this.TxtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBackUp";
            this.ShowIcon = false;
            this.Text = "Yedekleme Paneli";
            this.Load += new System.EventHandler(this.FrmBackUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPath;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Button BtnGozAt;
        private System.Windows.Forms.Button BtnYedekle;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}