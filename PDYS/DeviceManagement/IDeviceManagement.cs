using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceManagement
{
    public interface IDeviceManagement : IDisposable
    {
        

        string GetLastError();

        bool ConnectDevice(string IP, int Port, int ComKey);
        void DisconnectDevice();

        string GetSerialNumber();
        DateTime RefreshDeviceTime();

        string GetLastCardNumber();

        //User
        IEnumerable<DeviceUser> GetUsers();
        void CreateUser(DeviceUser deviceuser);
        void DeleteUser(DeviceUser deviceuser); //Backup number : 12
        

        //FingerPrint
        IEnumerable<FingerPrint> GetFingerPrintData(int UserID);
        void CrateFingerPrintData(FingerPrint fingerprint);
        void DeleteFingerPrintData(FingerPrint fingerprint);

        //Attendance Log
        int GetAttendanceLogCount();
        IEnumerable<AttendanceLog> GetAttendanceLogs();
        void ClearAttendanceLogs();


        StringBuilder LOG {get;}

    }
}
