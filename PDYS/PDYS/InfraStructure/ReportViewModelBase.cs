using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.Printing;
using DevExpress.XtraReports;
using System.Windows.Forms;
using PDYS.Models;
using System.Data.Entity;
using Mvvm;
using DevExpress.Data;
using System.Collections;
using PDYS.Services;

namespace PDYS.InfraStructure
{
    public abstract class ReportViewModelBase<T> : ViewModelBase where T : IReport
    {
        public abstract IEnumerable LoadReportDataSource();

        public ReportViewModelBase()
        {
            this.Report = Activator.CreateInstance<T>();
        }

        #region Property Report

        private IReport _Report;

        public IReport Report
        {
            get { return this._Report; }
            set
            {
                if (!object.Equals(this._Report, value))
                {
                    this._Report = value;
                    this.OnPropertyChanged(() => this.Report);
                }
            }
        }

        #endregion

        #region Property PreviewModel

        private XtraReportPreviewModel _PreviewModel;

        public XtraReportPreviewModel  PreviewModel
        {
            get { return this._PreviewModel; }
            set
            {
                if (!object.Equals(this._PreviewModel, value))
                {
                    this._PreviewModel = value;
                    this.OnPropertyChanged(() => this.PreviewModel);
                }
            }
        }

        #endregion

        void InternalExcuteExecuteReport()
        {
            if (PreviewModel==null)
                PreviewModel = new XtraReportPreviewModel(this.Report);

            BindingSource bs = (BindingSource)((IDataContainerBase)this.Report).DataSource;
            bs.DataSource = this.LoadReportDataSource();

            this.Report.CreateDocument(true);
        }


        #region Property ExecuteReportCommand

        private RelayCommand _ExecuteReportCommand;
        public RelayCommand ExecuteReportCommand
        {
            get
            {
                if (this._ExecuteReportCommand == null)
                    this._ExecuteReportCommand = new RelayCommand(ExcuteExecuteReportCommand);
                return this._ExecuteReportCommand;
            }
        }

        protected virtual void ExcuteExecuteReportCommand()
        {
            ProcessManager.Execute("Rapor Çalıştırılıyor...", new Action(InternalExcuteExecuteReport));
        }
        #endregion
    }
}
