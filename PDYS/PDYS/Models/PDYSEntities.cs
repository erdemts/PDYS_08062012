using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Xml.Linq;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data;
using PDYS.ViewModels;

namespace PDYS.Models
{
    public class PDYSEntities : DbContext
    {
        private PDYSEntities()
            : base("PDYSEntities")
        {

            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<Employee> EmployeeSet { get; set; }
        public DbSet<Parameter> ParameterSet { get; set; }
        public DbSet<City> CitySet { get; set; }
        public DbSet<County> CountySet { get; set; }
        public DbSet<Department> DepartmentSet { get; set; }
        public DbSet<EmployeeHoliday> EmployeeHolidaySet { get; set; }
        public DbSet<PublicHoliday> PublicHolidaySet { get; set; }
        public DbSet<Transport> TransportSet { get; set; }
        public DbSet<WeeklyOvertime> WeeklyOvertimeSet { get; set; }
        public DbSet<OutSourceOvertime> OutSourceOvertimeSet { get; set; }
        public DbSet<OvertimeAssignment> OvertimeAssignmentSet { get; set; }
        public DbSet<EmployeeInputOutput> EmployeeInputOutputSet { get; set; }
        public DbSet<EmployeeAccounting> EmployeeAccountingSet { get; set; }
        public DbSet<AccountingProcessType> AccountingProcessTypeSet { get; set; }
        public DbSet<ReaderDevice> ReaderDeviceSet { get; set; }
        public DbSet<EmployeeInOutScoring> EmployeeInOutScoringSet { get; set; }
        public DbSet<EmployeeSalaryScoring> EmployeeSalaryScoringSet { get; set; }
        public DbSet<EmployeeFingerPrint> EmployeeFingerPrints { get; set; }
        
        public DbSet<User> UserSet { get; set; }

        #region DataContext

        private static PDYSEntities _dataContext;
        private static readonly object lockObj = new object();
        public static PDYSEntities DataContext
        {
            get
            {
                lock (lockObj)
                {
                    if (_dataContext == null)
                        _dataContext = new PDYSEntities();
                }

                return _dataContext;
            }

        }

        #endregion

        #region Configuration

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer(new PDYSDatabaseInitializer());
            base.OnModelCreating(modelBuilder);
        }

        //protected override bool ShouldValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
        //{
        //    if (entityEntry.Entity is Employee)
        //    {
        //        return false;
        //    }

        //    return base.ShouldValidateEntity(entityEntry);
        //}

        #endregion

        public override int SaveChanges()
        {
             var items =  this.ChangeTracker.Entries<Author>();

             foreach (var item in items)
             {
                 if (item.State == EntityState.Added)
                 {
                     item.Entity.CreatedBy = ApplicationDataModel.CurrentUser;
                     item.Entity.CreatedOn = DateTime.Now;
                     item.Entity.ModifiedBy = ApplicationDataModel.CurrentUser;
                     item.Entity.ModifiedOn = DateTime.Now;
                 }
                 else if (item.State == EntityState.Modified)
                 {
                     item.Entity.ModifiedBy = ApplicationDataModel.CurrentUser;
                     item.Entity.ModifiedOn = DateTime.Now;
                 }
                 
             }

            return base.SaveChanges();
        }
    }




}
