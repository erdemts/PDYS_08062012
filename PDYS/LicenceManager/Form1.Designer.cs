namespace LicenceManager
{
    partial class Form1
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
            this.btnCodeGenerate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLicenceNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLicenceCode = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCodeGenerate
            // 
            this.btnCodeGenerate.Location = new System.Drawing.Point(173, 166);
            this.btnCodeGenerate.Name = "btnCodeGenerate";
            this.btnCodeGenerate.Size = new System.Drawing.Size(143, 23);
            this.btnCodeGenerate.TabIndex = 0;
            this.btnCodeGenerate.Text = "Kod Üret";
            this.btnCodeGenerate.UseVisualStyleBackColor = true;
            this.btnCodeGenerate.Click += new System.EventHandler(this.btnCodeGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLicenceNumber);
            this.groupBox1.Location = new System.Drawing.Point(28, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 62);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Üretilen Lisan Numarası";
            // 
            // lblLicenceNumber
            // 
            this.lblLicenceNumber.AutoSize = true;
            this.lblLicenceNumber.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLicenceNumber.Location = new System.Drawing.Point(185, 25);
            this.lblLicenceNumber.Name = "lblLicenceNumber";
            this.lblLicenceNumber.Size = new System.Drawing.Size(75, 23);
            this.lblLicenceNumber.TabIndex = 0;
            this.lblLicenceNumber.Text = "XXXXX";
            this.lblLicenceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lisans Kodu : ";
            // 
            // txtLicenceCode
            // 
            this.txtLicenceCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLicenceCode.Location = new System.Drawing.Point(109, 28);
            this.txtLicenceCode.Name = "txtLicenceCode";
            this.txtLicenceCode.Size = new System.Drawing.Size(369, 23);
            this.txtLicenceCode.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 201);
            this.Controls.Add(this.txtLicenceCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCodeGenerate);
            this.Name = "Form1";
            this.Text = "Lisans Yöneticisi";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCodeGenerate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLicenceNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLicenceCode;
    }
}

