using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;

namespace LicenceManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCodeGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtLicenceCode.Text))
                return;

            try
            {
                string Key = this.txtLicenceCode.Text;

                string encryptKeyToken = CyrptoTool.CyrptoText(Key, CyrptoTool.CyrptoKey);

                string keyToken = GetRegularChar(encryptKeyToken, 10);

                this.lblLicenceNumber.Text = keyToken;
            }
            catch 
            {
                MessageBox.Show("Hatalı Lisans Kodu !...");
            }
        }


        string GetRegularChar(string text, int txtLength)
        {
            string Key = "";

            foreach (Char chracter in text)
            {
                if (Char.IsDigit(chracter) || Char.IsLetter(chracter))
                {
                    Key += chracter.ToString().ToUpperInvariant();
                }

                if (Key.Length >= txtLength)
                    break;
            }

            return Key;
        }

    }
}
