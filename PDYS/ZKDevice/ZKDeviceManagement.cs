using System;
using System.Collections.Generic;
using System.Text;
using zkemkeeper;
using DeviceManagement;

namespace ZKDevice
{

    public class ZKDeviceManagement : IDeviceManagement, IDisposable
    {
        private CZKEMClass zkDevice;
        private int iMachineNumber = 1;
        public bool IsConnected { get; private set; }
        private Exception requiredConnectionException = null;


        public ZKDeviceManagement()
        {
            zkDevice = new CZKEMClass();
            requiredConnectionException = new Exception("Önce baglanti kurunuz");
            LOG = new StringBuilder();
        }

        ~ZKDeviceManagement()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (zkDevice != null && IsConnected)
            {
                this.DisconnectDevice();
                zkDevice = null;
            }
        }


        public string GetLastError()
        {
            int errorcode = 0;
            zkDevice.GetLastError(ref errorcode);

            return GetLastError(errorcode);
        }


        public string GetLastError(int errorcode)
        {
            Dictionary<int, string> errors = new Dictionary<int, string>();

            errors.Add(-100, "Islem basarisiz veya veri yok."); //  Operation failed or data not exist 
            errors.Add(-10, "Iletilen veri uzunlugu yanlis."); // Transmitted data length is incorrect
            errors.Add(-7, "Baglanti Hatasi."); //Unable to connect Device
            errors.Add(-5, "Veri zaten var."); //Data already exists 
            errors.Add(-4, "Yetersiz bos alan."); //Space is not enough 
            errors.Add(-3, "Veri uzunlugu hatali."); //Error size 
            errors.Add(-2, "Dosya Yazma/Okuma hatasi"); //Error in file read/write 
            errors.Add(-1, "Cihaz baslatilamadi yeniden baglanti kurunuz."); //SDK is not initialized and needs to be reconnected 
            errors.Add(0, "Veri bulunamadi yada tekrarladi"); //Data not found or data repeated 
            errors.Add(1, "Operasyon Hatası."); //Operation is correct
            errors.Add(4, "Parametre Hatasi"); //Parameter is incorrect 
            errors.Add(101, "Hafiza düzenlenemedi"); //Error in allocating buffer

            if (errors.ContainsKey(errorcode))
                return errors[errorcode];
            else
                return "Tanimlanamayan Hata.";
        }


      

        public bool ConnectDevice(string IP, int Port, int ComKey)
        {
            if (string.IsNullOrEmpty(IP) || Port < 1)
            {
                throw new Exception("IP Adresi ve Port Numrasi bos olamaz");
            }


            if (ComKey > 0)
            {
                bool result = zkDevice.SetCommPassword(ComKey);
            }

            IsConnected = zkDevice.Connect_Net(IP, Port);

            

            if (zkDevice.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            {
                this.zkDevice.OnVerify += new _IZKEMEvents_OnVerifyEventHandler(zkDevice_OnVerify);
                this.zkDevice.OnAttTransactionEx += new _IZKEMEvents_OnAttTransactionExEventHandler(zkDevice_OnAttTransactionEx);
                this.zkDevice.OnNewUser += new _IZKEMEvents_OnNewUserEventHandler(zkDevice_OnNewUser);
                this.zkDevice.OnHIDNum += new _IZKEMEvents_OnHIDNumEventHandler(zkDevice_OnHIDNum);
                this.zkDevice.OnWriteCard += new _IZKEMEvents_OnWriteCardEventHandler(zkDevice_OnWriteCard);
                this.zkDevice.OnEmptyCard += new _IZKEMEvents_OnEmptyCardEventHandler(zkDevice_OnEmptyCard);
            }

            return IsConnected;
        }

      

        public void DisconnectDevice()
        {
            zkDevice.Disconnect();
            IsConnected = false;
        }

        public DateTime RefreshDeviceTime()
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            if (zkDevice.SetDeviceTime(iMachineNumber))
            {
                zkDevice.RefreshData(iMachineNumber);//the data in the device should be refreshed

                int idwYear = 0;
                int idwMonth = 0;
                int idwDay = 0;
                int idwHour = 0;
                int idwMinute = 0;
                int idwSecond = 0;
                if (zkDevice.GetDeviceTime(iMachineNumber, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond))//show the time
                {
                    return new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                }

            }
            else
                ThrowError();

            return DateTime.MinValue;
        }

        public string GetSerialNumber()
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            string serialNumber = "";

            bool isError = zkDevice.GetSerialNumber(iMachineNumber, out serialNumber);

            if (!isError)
            {
                ThrowError();
            }

            return serialNumber;
        }

       



        public string GetLastCardNumber()
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            string sstrHIDEventCardNum = "";
            
            if (zkDevice.GetHIDEventCardNumAsStr(out sstrHIDEventCardNum))
            {
                return sstrHIDEventCardNum;
            }
            else
                ThrowError();

            return string.Empty;
        }

        #region User

        public IEnumerable<DeviceUser> GetUsers()
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);
            zkDevice.ReadAllUserID(iMachineNumber);

            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string sCardnumber = "";

            List<DeviceUser> list = new List<DeviceUser>();

            while (zkDevice.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get user information from memory
            {
                DeviceUser user = new DeviceUser();
                
                if (!string.IsNullOrEmpty(sdwEnrollNumber))
                    user.UserID = int.Parse(sdwEnrollNumber);

                user.UserName = sName;
                user.Password = sPassword;
                user.Privilege = (UserPrivilege)iPrivilege;
                user.Enabled = bEnabled;

                if (zkDevice.GetStrCardNumber(out sCardnumber))//get the card number from the memory
                {
                    user.CardNumber = sCardnumber;
                }
                
                list.Add(user);
            }

            zkDevice.EnableDevice(iMachineNumber, true);

            return list;
        }

        public void CreateUser(DeviceUser deviceuser)
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);

            if (!zkDevice.SetStrCardNumber(deviceuser.CardNumber))//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
                ThrowError();

            if (!zkDevice.SSR_SetUserInfo(iMachineNumber, deviceuser.UserID.ToString(), deviceuser.UserName, deviceuser.Password, (int)deviceuser.Privilege, deviceuser.Enabled))//upload the user's information(card number included)
                ThrowError();
            

            zkDevice.RefreshData(iMachineNumber);
            zkDevice.EnableDevice(iMachineNumber, true);
        }

        public void DeleteUser(DeviceUser deviceuser)
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);

            bool result = zkDevice.SSR_DeleteEnrollData(iMachineNumber, deviceuser.UserID.ToString(), 12);

            zkDevice.RefreshData(iMachineNumber);
            zkDevice.EnableDevice(iMachineNumber, true);

            if (!result)
                ThrowError();
        }

        #endregion


        #region Attendance Log

        public int GetAttendanceLogCount()
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            int iCount = 0;
            bool result = zkDevice.GetDeviceStatus(iMachineNumber, 6, ref iCount);

            if (!result)
                ThrowError();

            return iCount;
        }

        public IEnumerable<AttendanceLog> GetAttendanceLogs()
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);

            string sdwEnrollNumber = "";
            
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            bool result = zkDevice.ReadGeneralLogData(iMachineNumber);
            if (!result)
                ThrowError();

            List<AttendanceLog> resultlogs = new List<AttendanceLog>();

            while (zkDevice.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                          out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
            {
                AttendanceLog log = new AttendanceLog();

                if (!string.IsNullOrEmpty(sdwEnrollNumber))
                    log.UserID = int.Parse(sdwEnrollNumber);

                log.VerifyMode = (AttendanceVerifyMode)idwVerifyMode;
                log.InOutMode = (AttendanceInOutMode)idwInOutMode;
                log.Date = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);

                resultlogs.Add(log);
            }

            zkDevice.EnableDevice(iMachineNumber, true);

            return resultlogs;
        }

        public void ClearAttendanceLogs()
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);
             bool result = zkDevice.ClearGLog(iMachineNumber);

             if (!result)
                 ThrowError();

             zkDevice.EnableDevice(iMachineNumber, true);
        }

        #endregion


        public IEnumerable<FingerPrint> GetFingerPrintData(int UserID)
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);
            
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;

            zkDevice.ReadAllTemplate(iMachineNumber);

            List<FingerPrint> resultList = new List<FingerPrint>();

            for (int idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
            {

                if (zkDevice.GetUserTmpExStr(iMachineNumber, UserID.ToString(), idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                {
                    FingerPrint fingerprint = new FingerPrint();
                    fingerprint.UserID = UserID;
                    fingerprint.FingerIndex = idwFingerIndex;
                    fingerprint.Flag = (FingerPrintFlag)iFlag;
                    fingerprint.TemplateData = sTmpData;

                    resultList.Add(fingerprint);

                }
            }

            zkDevice.EnableDevice(iMachineNumber, true);

            return resultList;
        }

        public void CrateFingerPrintData(FingerPrint fingerprint)
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);

            bool result = zkDevice.SetUserTmpExStr(iMachineNumber, fingerprint.UserID.ToString(), fingerprint.FingerIndex, (int)fingerprint.Flag, fingerprint.TemplateData);

            if (!result)
                ThrowError();

            zkDevice.EnableDevice(iMachineNumber, true);
        }

        public void DeleteFingerPrintData(FingerPrint fingerprint)
        {
            if (IsConnected == false)
                throw requiredConnectionException;

            zkDevice.EnableDevice(iMachineNumber, false);

            bool result = zkDevice.SSR_DelUserTmpExt(iMachineNumber, fingerprint.UserID.ToString(), fingerprint.FingerIndex);
            
            if (!result)
                ThrowError();
            else
                zkDevice.RefreshData(iMachineNumber);

            zkDevice.EnableDevice(iMachineNumber, true);
        }

        

        
        private void ThrowError()
        {
            int idwErrorCode = 0;
            zkDevice.GetLastError(ref idwErrorCode);

            zkDevice.EnableDevice(iMachineNumber, true);

            throw new Exception(this.GetLastError(idwErrorCode));
        }


        #region LOG

        void zkDevice_OnEmptyCard(int ActionResult)
        {
            LOG.AppendLine("OnEmptyCard : ");

            if (ActionResult == 0)
            {
                LOG.Append("Empty Mifare Card OK.");
            }
            else
            {
                LOG.Append("Empty Failed.");
            }
        }


        void zkDevice_OnWriteCard(int EnrollNumber, int ActionResult, int Length)
        {
            LOG.AppendLine("OnWriteCard : ");

            if (ActionResult == 0)
            {
                LOG.Append("Write Mifare Card OK.");
                LOG.Append(" EnrollNumber=" + EnrollNumber.ToString());
                LOG.Append(" ,TmpLength=" + Length.ToString());
            }
            else
            {
                LOG.Append("Write Failed.");
            }
        }

        void zkDevice_OnAttTransactionEx(string EnrollNumber, int IsInValid, int AttState, int VerifyMethod, int Year, int Month, int Day, int Hour, int Minute, int Second, int WorkCode)
        {
            LOG.AppendLine("OnAttTransactionEx : UserID is " + EnrollNumber.ToString());
        }

        void zkDevice_OnNewUser(int EnrollNumber)
        {
            LOG.AppendLine("OnNewUser : UserID is " + EnrollNumber.ToString());
        }

        void zkDevice_OnHIDNum(int CardNumber)
        {
            LOG.AppendLine("OnHIDNum : CardNumber is " + CardNumber.ToString());
        }


        void zkDevice_OnVerify(int UserID)
        {
            if (UserID != -1)
            {
                LOG.AppendLine("OnVerify : Verified OK,the UserID is " + UserID.ToString());
            }
            else
            {
                LOG.AppendLine("OnVerify : Verified Failed... ");
            }
        }

        public StringBuilder LOG { get; private set; }


        #endregion

    }

}
