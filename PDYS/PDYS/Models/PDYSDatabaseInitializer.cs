using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Reflection;
using System.Xml.Linq;


namespace PDYS.Models
{

    public class PDYSDatabaseInitializer : DropCreateDatabaseIfModelChanges<PDYSEntities>
    {
        protected override void Seed(PDYSEntities context)
        {


            #region Parameter

            new List<Parameter>() { 
                // Gender
                new Parameter(){ Name="Gender", Text="Erkek", Value=0 },
                new Parameter(){ Name="Gender", Text="Kadın", Value=1 },
                // MaritalStatus
                new Parameter(){ Name="MaritalStatus", Text="Bekar", Value=0 },
                new Parameter(){ Name="MaritalStatus", Text="Evli", Value=1 },

                 // State
                new Parameter(){ Name="State", Text="Aktif", Value=0 },
                new Parameter(){ Name="State", Text="Pasif", Value=1 },

                new Parameter(){ Name="InputOutputType", Text="Giriş & Çıkış", Value=0 },
                new Parameter(){ Name="InputOutputType", Text="Giriş", Value=1 },
                new Parameter(){ Name="InputOutputType", Text="Çıkış", Value=2 },

                new Parameter(){ Name="ProcessState", Text="Hatalı İşlem", Value=0 },
                new Parameter(){ Name="ProcessState", Text="Tamamlandı", Value=1 },
                new Parameter(){ Name="ProcessState", Text="Eksik Bilgi", Value=2 },

                new Parameter(){ Name="HolidayType", Text="Diğer İzinler", Value=0 },
                new Parameter(){ Name="HolidayType", Text="Yıllık Ücretli İzin", Value=1 },
                new Parameter(){ Name="HolidayType", Text="Yeni İş Arama İzni", Value=2},
                new Parameter(){ Name="HolidayType", Text="Doğum İzni", Value=3 },
                new Parameter(){ Name="HolidayType", Text="Periyodik Kontrol İzni", Value=4 },
                new Parameter(){ Name="HolidayType", Text="Süt İzni", Value=5 },
                new Parameter(){ Name="HolidayType", Text="Evlenme İzni", Value=6 },
                new Parameter(){ Name="HolidayType", Text="Ölüm İzni", Value=7 },
                new Parameter(){ Name="HolidayType", Text="Yol İzni", Value=8 },
                new Parameter(){ Name="HolidayType", Text="Doğum İzni Sonrası Ücretsiz İzin", Value=9 },
                new Parameter(){ Name="HolidayType", Text="Hastalık ve Dinlenme İzni", Value=10 },
                
                
            }.ForEach(p => context.ParameterSet.Add(p));

            #endregion

            #region Employee Accounting Type Defination

            new List<AccountingProcessType>()
            {
                new AccountingProcessType() {Code="00101", Name="Maaş Hakediş", IsDebit = false, IsCredit=true,  IsSystem = true},
                new AccountingProcessType() {Code="00102", Name="Maaş Ödemesi", IsDebit = true, IsCredit=false,  IsSystem = true},
                new AccountingProcessType() {Code="00202", Name="Avans Ödemesi", IsDebit = true, IsCredit=false,  IsSystem = true},
                new AccountingProcessType() {Code="00301", Name="Prim Hakediş", IsDebit = false, IsCredit=true,  IsSystem = true},
                new AccountingProcessType() {Code="00302", Name="Prim Ödemesi", IsDebit = true, IsCredit=false,  IsSystem = true},
                new AccountingProcessType() {Code="00403", Name="Diğer", IsDebit = true, IsCredit=true, IsSystem = false},

            }.ForEach(p => context.AccountingProcessTypeSet.Add(p));

            #endregion

            #region City & County

            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase) + "\\CityCounty.xml";

            XDocument xDoc = XDocument.Load(path);

            XElement xRoot = xDoc.Element("root");

            if (xRoot != null)
            {

                foreach (var cityElement in xRoot.Elements())
                {
                    City city = new City();
                    city.Code = cityElement.Attribute("code").Value;
                    city.Name = cityElement.Attribute("name").Value;

                    foreach (var countyElement in cityElement.Elements())
                    {
                        County county = new County();
                        county.City = city;
                        county.Code = countyElement.Attribute("code").Value;
                        county.Name = countyElement.Attribute("name").Value;

                        city.Counties.Add(county);
                    }

                    context.CitySet.Add(city);
                }


            }
            #endregion
            User admin = new User();
            admin.UserName = User.AdminUser;
            admin.FullName = "Sistem Yöneticisi";
            admin.IsLogon = true;

            context.UserSet.Add(admin);

            //User system = new User();
            //system.UserName = User.SystemUser;
            //system.FullName = "Sistem";
            //system.IsLogon = false;

            //context.UserSet.Add(system);

            #region User

            #endregion

            context.SaveChanges();
        }
    }
}
