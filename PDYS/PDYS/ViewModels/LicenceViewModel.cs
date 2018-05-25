using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDYS.InfraStructure;
using PDYS.Services.ServiceParam;
using PDYS.Helper;

namespace PDYS.ViewModels
{
    public class LicenceViewModel : BaseWindowViewModelBase
    {
        public LicenceViewModel()
        {
            this.Title = "Lisans Yönetimi";

            LicenceHelper = new LicenceHelper();
            this.Loaded += new Action(LicenceViewModel_Loaded);
        }

        void LicenceViewModel_Loaded()
        {
            
           this.TempLicenceData =  this.LicenceHelper.GenerateLicenceData();
           this.LicenceKey = this.TempLicenceData.LicenceKey;
        }

        public LicenceHelper.LicenceData TempLicenceData { get; set; }
        public LicenceHelper LicenceHelper { get; set; }


        #region Property LicenceKey

        private string _LicenceKey;
        /// <summary>
        /// 
        /// </summary>
        public string LicenceKey
        {
            get { return this._LicenceKey; }
            set
            {
                if (!object.Equals(this._LicenceKey, value))
                {
                    this._LicenceKey = value;
                    this.OnPropertyChanged(() => this.LicenceKey);
                }
            }
        }

        #endregion


        #region Property LicenceNumber

        private string _LicenceNumber;

        public string LicenceNumber
        {
            get { return this._LicenceNumber; }
            set
            {
                if (!object.Equals(this._LicenceNumber, value))
                {
                    this._LicenceNumber = value;
                    this.OnPropertyChanged(() => this.LicenceNumber);
                    this.Validator.Validate(() => this.LicenceNumber);
                }
            }
        }

        #endregion

        

        public bool IsValidLicence()
        {
            return this.LicenceHelper.IsValid();
        }

        protected override void ExcuteAcceptCommand()
        {
            if (this.TempLicenceData.KeyToken == this.LicenceNumber)
            {
                this.ServicePresenter.ShowInformationMessage("Lisans Başarılı Şekilde Oluşturuldu!..");

                this.LicenceHelper.CreateLicenceFile(this.TempLicenceData);
                this.ServicePresenter.CloseWindow(true);
            }
            else
            {
                this.ServicePresenter.ShowErrorMessage("Hatalı Lisans Kodu !..");
            }

            
        }

        
        

       
    }
}
