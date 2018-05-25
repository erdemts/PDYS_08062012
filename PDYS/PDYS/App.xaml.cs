using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using PDYS.InfraStructure;
using PDYS.Services;
using PDYS.Helper;
using System.Windows.Shapes;
using System.Xml.Linq;
using PDYS.Models;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PDYS.Interfaces;

namespace PDYS
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        

        IDialogService AppService;

       

        protected override void OnStartup(StartupEventArgs e)
        {
            
            base.OnStartup(e);

            System.Globalization.CultureInfo trCulture =  new System.Globalization.CultureInfo("tr-TR");

            System.Threading.Thread.CurrentThread.CurrentCulture = trCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = trCulture;

            AppService = DialogServiceReceiver.Initialize();

            GlobalHandler.SetEventListener();
            //Database.DefaultConnectionFactory = new SqlConnectionFactory("System.Data.SqlServerCe.4.0");

            this.Dispatcher.UnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Dispatcher_UnhandledException);
        }

        void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = (e.Exception.InnerException!=null) ? e.Exception.InnerException.ToString() : e.Exception.Message;
            MessageBox.Show("Hata Mesajı : " + errorMessage, "Hata Oluştu !..", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        

        
    }
}
