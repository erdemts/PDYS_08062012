using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Services.ServiceParam;

namespace PDYS.ViewModels
{
    class LogViewModel : BaseWindowViewModelBase
    {
        public LogViewModel()
        {
            
        }

        #region Property Message

        private string _Message;

        public string Message
        {
            get { return this._Message; }
            set
            {
                if (!object.Equals(this._Message, value))
                {
                    this._Message = value;
                    this.OnPropertyChanged(() => this.Message);
                    this.Validator.Validate(() => this.Message);
                }
            }
        }

        #endregion

        public static void OpenWindow(string Title, string Message)
        {
            LogViewModel modelview = new LogViewModel();
            modelview.Title = Title;
            modelview.Message = Message;

            DialogWindowParam windowParam = new DialogWindowParam();
            windowParam.ModelView = modelview;
            windowParam.OnClose = (result) =>
            {
                if (result)
                {
                    
                }
            };

            modelview.ServicePresenter.OpenBaseWindow(windowParam);

        }
    }
}
