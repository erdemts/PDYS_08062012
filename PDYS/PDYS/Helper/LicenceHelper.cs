using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.IO;
using Common;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PDYS.Helper
{
    public class LicenceHelper
    {
        public const int LicenceKeyLength = 10;

        [Serializable]
        public class LicenceData
        {
            public string AppNumber { get; set; }
            public string Token { get; set; }

            public string LicenceKey { get; set; }
            public string KeyToken { get; set; }

        }



        public LicenceData GenerateLicenceData()
        {
            string pcserialNumber = this.GenerateSerialNumber();
            string token = CyrptoTool.CyrptoKey + DateTime.Now.Ticks.ToString();


            string encryptKey = CyrptoTool.CyrptoText(pcserialNumber, token);


            string Key = GetRegularChar(encryptKey, 10);

           

            string encryptKeyToken = CyrptoTool.CyrptoText(Key, CyrptoTool.CyrptoKey);

            string keyToken = GetRegularChar(encryptKeyToken, 10);

            LicenceData lcd = new LicenceData() { AppNumber = encryptKey, Token = token, LicenceKey = Key, KeyToken = keyToken };


            return lcd;
        }

        string GetRegularChar(string text,int txtLength)
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





        public bool IsValid()
        {
            LicenceData licencedata = LoadLicenceData();

            if (licencedata == null)
                return false;

            try
            {
                string savedpcAppNumber = CyrptoTool.DeCryptoText(licencedata.AppNumber, licencedata.Token);
                string currentAppNumber = this.GenerateSerialNumber();

                if (savedpcAppNumber == currentAppNumber)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public LicenceData LoadLicenceData()
        {
            try
            {
                string fileName = this.FileName;
                FileInfo fInf = new FileInfo(fileName);

                if (!fInf.Exists)
                    return null;

                FileStream fs = fInf.OpenRead();
                StreamReader reader = new StreamReader(fs, Encoding.Unicode);
                string base64Data = reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                fs.Close();
                fs.Dispose();

                byte[] bufferbase64Data = System.Convert.FromBase64String(base64Data);
                LicenceData licencedata = null;


                using (MemoryStream membuffer = new MemoryStream(bufferbase64Data))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    licencedata = formatter.Deserialize(membuffer) as LicenceData;
                }

                return licencedata;
            }
            catch
            {
                return null;
            }


        }


        public void CreateLicenceFile(LicenceData licencedata)
        {
            if (licencedata == null)
                return;
            try
            {

                string base64Data = "";

                using (MemoryStream membuffer = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(membuffer, licencedata);

                    membuffer.Seek(0, SeekOrigin.Begin);

                    byte[] bufferdata = membuffer.ToArray();

                    base64Data = System.Convert.ToBase64String(bufferdata);

                    membuffer.Close();

                }



                string fileName = this.FileName;
                FileStream fs = new FileStream(fileName, FileMode.Create);

                StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                sw.Write(base64Data);

                sw.Close();
                sw.Dispose();

                fs.Close();
                fs.Dispose();
            }
            catch
            { }
        }


        public string FileName
        {
            get
            {
                string folder = DirectoryName();
                var fileName = Path.Combine(folder, "Licence.lic");

                return fileName;
            }

        }



        public string DirectoryName()
        {
            
            string folder = Directory.GetCurrentDirectory();

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return folder;
        }


        private string GenerateSerialNumber()
        {
            string[] serialTypes = new string[]
            {
                "Win32_BIOS",
                "Win32_DiskDrive  Where InterfaceType=\"IDE\""
            };


            string ID = string.Empty;
            foreach (var serialType in serialTypes)
            {
                ID += GetSystemIndetifierNumber(serialType);
                ID += "#";
            }

            return ID;
        }

        private string GetSystemIndetifierNumber(string managementobject)
        {
            //ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");
            ObjectQuery query = new ObjectQuery(string.Format("SELECT * FROM {0}", managementobject));

            //create object searcher
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            //get collection of WMI objects
            ManagementObjectCollection queryCollection = searcher.Get();

            string ID = string.Empty;

            foreach (ManagementObject m in queryCollection)
            {
                try
                {
                    ID += m["SerialNumber"].ToString().Trim();
                }
                catch
                { }
            }

            return ID;
        }
    }
}
