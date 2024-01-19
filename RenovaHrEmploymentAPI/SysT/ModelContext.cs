using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RenovaHrEmploymentAPI;

#nullable disable

namespace RenovaHrEmploymentAPI.SysT
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUsersCompany> SysUsersCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.101.81)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=RENOVA)));User Id=RENOVA_SALES;Password=RenovaSales; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RENOVA_SALES")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_SYS_USERS_USERID");

                entity.ToTable("SYS_USERS");

                entity.HasIndex(e => e.UserName, "CO_SYS_USERS_USER_NAME")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UC_SYS_USERS_EMAILS")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.CompensationPermission)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("COMPENSATION_PERMISSION")
                    .HasDefaultValueSql("'W'");

                entity.Property(e => e.ContactInformationPermission)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CONTACT_INFORMATION_PERMISSION")
                    .HasDefaultValueSql("'W'");

                entity.Property(e => e.CreateNewEmployeePermission)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_NEW_EMPLOYEE_PERMISSION")
                    .HasDefaultValueSql("'W'");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATION_DATE")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.DefaultCompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DEFAULT_COMPANY_ID")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.DefaultProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DEFAULT_PRODUCT_ID");

                entity.Property(e => e.DisplayTaHoursDecimals)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DISPLAY_TA_HOURS_DECIMALS")
                    .HasDefaultValueSql("'H'")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRED_DATE");

                entity.Property(e => e.ExpiredDays)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EXPIRED_DAYS");

                entity.Property(e => e.ExpiredPassword)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EXPIRED_PASSWORD")
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.ExportExcel)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EXPORT_EXCEL")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.FailedLoginAnswerCount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FAILED_LOGIN_ANSWER_COUNT")
                    .HasDefaultValueSql("3");

                entity.Property(e => e.FailedLoginCount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FAILED_LOGIN_COUNT")
                    .HasDefaultValueSql("3");

                entity.Property(e => e.FirstLogin)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_LOGIN")
                    .HasDefaultValueSql("'Y'");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FULL_NAME");

                entity.Property(e => e.InactiveDate)
                    .HasColumnType("DATE")
                    .HasColumnName("INACTIVE_DATE");

                entity.Property(e => e.InviteDate)
                    .HasColumnType("DATE")
                    .HasColumnName("INVITE_DATE");

                entity.Property(e => e.InviteStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("INVITE_STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.LastBrowser)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LAST_BROWSER");

                entity.Property(e => e.LastCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_CITY");

                entity.Property(e => e.LastCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_COUNTRY");

                entity.Property(e => e.LastDevice)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_DEVICE");

                entity.Property(e => e.LastHostname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_HOSTNAME");

                entity.Property(e => e.LastIp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_IP");

                entity.Property(e => e.LastLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_LOCATION");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_LOGIN");

                entity.Property(e => e.LastOrg)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_ORG");

                entity.Property(e => e.LastPasswordChange)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_PASSWORD_CHANGE");

                entity.Property(e => e.LastPostal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_POSTAL");

                entity.Property(e => e.LastRegion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_REGION");

                entity.Property(e => e.LastTimezone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST_TIMEZONE");

                entity.Property(e => e.LastUpdDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_UPD_DATE")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.LastUpdUserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_USER_NAME")
                    .HasDefaultValueSql("USER");

                entity.Property(e => e.MenuType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MENU_TYPE")
                    .HasDefaultValueSql("'P' ")
                    .IsFixedLength(true);

                entity.Property(e => e.NewTabForReports)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("NEW_TAB_FOR_REPORTS")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.OtherCompensationPermision)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OTHER_COMPENSATION_PERMISION")
                    .HasDefaultValueSql("'W'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.ProcessorId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROCESSOR_ID");

                entity.Property(e => e.ShowHrDashboard)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SHOW_HR_DASHBOARD")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.ShowPayrollDashboard)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SHOW_PAYROLL_DASHBOARD")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.ShowTaDashboard)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SHOW_TA_DASHBOARD")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.SocialIdCodePermission)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_ID_CODE_PERMISSION")
                    .HasDefaultValueSql("'W'");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.VoluntaryDeductionPermission)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VOLUNTARY_DEDUCTION_PERMISSION")
                    .HasDefaultValueSql("'W'");
            });

            modelBuilder.Entity<SysUsersCompany>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_SYS_USERS_COMPANIES_RECID");

                entity.ToTable("SYS_USERS_COMPANIES");

                entity.HasIndex(e => new { e.UserId, e.CompanyId }, "UNQ_SYS_USERS_COMPANIES_ID")
                    .IsUnique();

                entity.Property(e => e.RecordId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RECORD_ID");

                entity.Property(e => e.BusinessUnit1CodeFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT1_CODE_FILTER");

                entity.Property(e => e.BusinessUnit1CodeIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT1_CODE_INC_EXC");

                entity.Property(e => e.BusinessUnit2CodeFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT2_CODE_FILTER");

                entity.Property(e => e.BusinessUnit2CodeIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT2_CODE_INC_EXC");

                entity.Property(e => e.BusinessUnit3CodeFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT3_CODE_FILTER");

                entity.Property(e => e.BusinessUnit3CodeIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT3_CODE_INC_EXC");

                entity.Property(e => e.BusinessUnit4CodeFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT4_CODE_FILTER");

                entity.Property(e => e.BusinessUnit4CodeIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT4_CODE_INC_EXC");

                entity.Property(e => e.BusinessUnit5CodeFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT5_CODE_FILTER");

                entity.Property(e => e.BusinessUnit5CodeIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT5_CODE_INC_EXC");

                entity.Property(e => e.BusinessUnit6CodeFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT6_CODE_FILTER");

                entity.Property(e => e.BusinessUnit6CodeIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT6_CODE_INC_EXC");

                entity.Property(e => e.ByPassArchiveTimecardLock)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BY_PASS_ARCHIVE_TIMECARD_LOCK")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.EffectiveFromDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EFFECTIVE_FROM_DATE");

                entity.Property(e => e.EffectiveToDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EFFECTIVE_TO_DATE");

                entity.Property(e => e.EmployeeFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOYEE_FILTER");

                entity.Property(e => e.EmployeeIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOYEE_INC_EXC")
                    .HasDefaultValueSql("NULL")
                    .IsFixedLength(true);

                entity.Property(e => e.LastUpdDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_UPD_DATE")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.LastUpdUserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_USER_NAME")
                    .HasDefaultValueSql("USER");

                entity.Property(e => e.LocationIdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_ID_FILTER");

                entity.Property(e => e.LocationIdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_ID_INC_EXC");

                entity.Property(e => e.PayFrequencyFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("PAY_FREQUENCY_FILTER");

                entity.Property(e => e.PayFrequencyIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PAY_FREQUENCY_INC_EXC");

                entity.Property(e => e.PayrollRuleIdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("PAYROLL_RULE_ID_FILTER");

                entity.Property(e => e.PayrollRuleIdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PAYROLL_RULE_ID_INC_EXC");

                entity.Property(e => e.PositionIdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("POSITION_ID_FILTER");

                entity.Property(e => e.PositionIdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("POSITION_ID_INC_EXC");

                entity.Property(e => e.PrEditBusinessUnit)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_BUSINESS_UNIT")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditEarningAmount)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_EARNING_AMOUNT")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditEmployeerDedAmount)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_EMPLOYEER_DED_AMOUNT")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditPayrollTaxParameters)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_PAYROLL_TAX_PARAMETERS")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditPayrollWithholding)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_PAYROLL_WITHHOLDING")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditPosition)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_POSITION")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditRate)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_RATE")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditState)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_STATE")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditStatutoryDedAmount)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_STATUTORY_DED_AMOUNT")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.PrEditVoluntaryDedAmount)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PR_EDIT_VOLUNTARY_DED_AMOUNT")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.ShowTaAmountColumn)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SHOW_TA_AMOUNT_COLUMN")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.StatusFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_FILTER");

                entity.Property(e => e.StatusIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_INC_EXC");

                entity.Property(e => e.SupervisorIdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("SUPERVISOR_ID_FILTER");

                entity.Property(e => e.SupervisorIdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUPERVISOR_ID_INC_EXC");

                entity.Property(e => e.TimekeeperIdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TIMEKEEPER_ID_FILTER");

                entity.Property(e => e.TimekeeperIdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIMEKEEPER_ID_INC_EXC");

                entity.Property(e => e.Type1IdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TYPE1_ID_FILTER");

                entity.Property(e => e.Type1IdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE1_ID_INC_EXC");

                entity.Property(e => e.Type2IdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TYPE2_ID_FILTER");

                entity.Property(e => e.Type2IdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE2_ID_INC_EXC");

                entity.Property(e => e.Type3IdFilter)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TYPE3_ID_FILTER");

                entity.Property(e => e.Type3IdIncExc)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE3_ID_INC_EXC");

                entity.Property(e => e.UseCategoryFilterTimecard)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USE_CATEGORY_FILTER_TIMECARD")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.UseLeaveFilterTimecard)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USE_LEAVE_FILTER_TIMECARD")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.ViewRestricted)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VIEW_RESTRICTED")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.VoidDeleteChecks)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VOID_DELETE_CHECKS")
                    .HasDefaultValueSql("'D'")
                    .IsFixedLength(true);
            });

            modelBuilder.HasSequence("DBOBJECTID_SEQUENCE").IncrementsBy(50);

            modelBuilder.HasSequence("SEQ_CO_ADDRESS_ID");

            modelBuilder.HasSequence("SEQ_CO_COMMON_ID");

            modelBuilder.HasSequence("SEQ_CO_EMPLOYEE_GENER_FIELD_ID");

            modelBuilder.HasSequence("SEQ_CO_EMPS_ID");

            modelBuilder.HasSequence("SEQ_CO_LEAVE_ACCR_TRAN_ID");

            modelBuilder.HasSequence("SEQ_GL_ERRORS_ID");

            modelBuilder.HasSequence("SEQ_HR_ATT_ATTENDANCE_TOTAL_ID");

            modelBuilder.HasSequence("SEQ_HR_ERRORS_ID");

            modelBuilder.HasSequence("SEQ_PAYROLL_W2_CORRECTIONS");

            modelBuilder.HasSequence("SEQ_PR_ACH_TRACE_CODE").IsCyclic();

            modelBuilder.HasSequence("SEQ_PR_CHECKS_REGISTER_ID");

            modelBuilder.HasSequence("SEQ_PR_CHECKS_REGISTER_IMAG_ID");

            modelBuilder.HasSequence("SEQ_PR_EMPL_PAYROLL_LOANS_ID");

            modelBuilder.HasSequence("SEQ_PR_EMPLOYEE_PAYROLL_TYP_ID");

            modelBuilder.HasSequence("SEQ_PR_ETF_TRANSACTIONS_ID");

            modelBuilder.HasSequence("SEQ_PR_GL_TRANSACTIONS_ID");

            modelBuilder.HasSequence("SEQ_PR_HEADER_TRANSACTION_ID");

            modelBuilder.HasSequence("SEQ_PR_PAYROLL_ERRORS_ID");

            modelBuilder.HasSequence("SEQ_PR_PAYROLL_IMPORT_TEMP_ID");

            modelBuilder.HasSequence("SEQ_PR_PAYROLL_TRANSA_AUDIT_ID");

            modelBuilder.HasSequence("SEQ_PR_PAYROLL_TRANSACTIONS_ID");

            modelBuilder.HasSequence("SEQ_PR_PAYROLL_W2_ID");

            modelBuilder.HasSequence("SEQ_SYS_USERS_MENU_ID");

            modelBuilder.HasSequence("SEQ_TA_CLOCK_AUDIT_ID");

            modelBuilder.HasSequence("SEQ_TA_EMPLOYEE_BIOMETRICS_ID");

            modelBuilder.HasSequence("SEQ_TA_INDIVIDUAL_SCHEDULE_ID");

            modelBuilder.HasSequence("SEQ_TA_PUNCHES_DETAIL_ID");

            modelBuilder.HasSequence("SEQ_TA_PUNCHES_MATCH_ID");

            modelBuilder.HasSequence("SEQ_TA_PUNCHES_PHOTO");

            modelBuilder.HasSequence("SEQ_TA_TIMECARD_APPROVALS_ID");

            modelBuilder.HasSequence("SEQ_TA_TIMECARD_DETAIL_ID");

            modelBuilder.HasSequence("SEQ_TA_TIMECARD_EXCEPTIONS_ID");

            modelBuilder.HasSequence("SEQ_TA_TIMECARD_TOTALS_ID");

            modelBuilder.HasSequence("SEQ_TA_TIMECARD_TRANSACTION_ID");

            modelBuilder.HasSequence("SEQ_TRG_SYS_AUDIT_ID");

            modelBuilder.HasSequence("SEQ_TRG_TA_PUNCHES_TEMP_ID");

            modelBuilder.HasSequence("SEQ_TRG_TA_TIMECARD_AUDIT_ID");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
