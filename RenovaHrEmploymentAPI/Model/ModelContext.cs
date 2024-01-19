using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RenovaHrEmploymentAPI
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
        public virtual DbSet<CoBusinessUnitsMaster> CoBusinessUnitsMasters { get; set; }
        public virtual DbSet<CoCompanyMaster> CoCompanyMasters { get; set; }
        public virtual DbSet<CoCompanyMasterLogo> CoCompanyMasterLogos { get; set; }
        public virtual DbSet<HrRcrtApplicant> HrRcrtApplicants { get; set; }
        public virtual DbSet<HrRcrtApplicantDocument> HrRcrtApplicantDocuments { get; set; }
        public virtual DbSet<HrRcrtApplicantQuery> HrRcrtApplicantQueries { get; set; }
        public virtual DbSet<HrRcrtApplicantsStatus> HrRcrtApplicantsStatuses { get; set; }
        public virtual DbSet<HrRcrtAudit> HrRcrtAudits { get; set; }
        public virtual DbSet<HrRcrtCompanyConfiguration> HrRcrtCompanyConfigurations { get; set; }
        public virtual DbSet<HrRcrtExternalWebsiteInfo> HrRcrtExternalWebsiteInfos { get; set; }
        public virtual DbSet<HrRcrtInterview> HrRcrtInterviews { get; set; }
        public virtual DbSet<HrRcrtInterviewer> HrRcrtInterviewers { get; set; }
        public virtual DbSet<HrRcrtOfccpText> HrRcrtOfccpTexts { get; set; }
        public virtual DbSet<HrRcrtPositionProfileType> HrRcrtPositionProfileTypes { get; set; }
        public virtual DbSet<HrRcrtPositionRequest> HrRcrtPositionRequests { get; set; }
        public virtual DbSet<HrRcrtRequestQuestChoice> HrRcrtRequestQuestChoices { get; set; }
        public virtual DbSet<HrRcrtRequestQuestion> HrRcrtRequestQuestions { get; set; }
        public virtual DbSet<HrRcrtRequestQuestnAnswer> HrRcrtRequestQuestnAnswers { get; set; }
        public virtual DbSet<HrRcrtRequestStatusMaster> HrRcrtRequestStatusMasters { get; set; }
        public virtual DbSet<HrRcrtTestAnswer> HrRcrtTestAnswers { get; set; }
        public virtual DbSet<HrRcrtTestQuestion> HrRcrtTestQuestions { get; set; }
        public virtual DbSet<HrRcrtTestRecord> HrRcrtTestRecords { get; set; }
        public virtual DbSet<HrRcrtTestRegistration> HrRcrtTestRegistrations { get; set; }
        public virtual DbSet<HrRcrtTestRequest> HrRcrtTestRequests { get; set; }
        public virtual DbSet<HrRcrtTestResult> HrRcrtTestResults { get; set; }
        public virtual DbSet<HrRcrtUser> HrRcrtUsers { get; set; }
        public virtual DbSet<HrRcrtUserDocument> HrRcrtUserDocuments { get; set; }
        public virtual DbSet<HrRcrtUserEducation> HrRcrtUserEducations { get; set; }
        public virtual DbSet<HrRcrtUserEmploymentHist> HrRcrtUserEmploymentHists { get; set; }
        public virtual DbSet<HrRcrtUserSecurityQuestion> HrRcrtUserSecurityQuestions { get; set; }
        public virtual DbSet<HrRcrtUsersProfile> HrRcrtUsersProfiles { get; set; }

        public virtual DbSet<SysStatesMaster> SysStatesMasters { get; set; }
        public virtual DbSet<SysZipcode> SysZipcodes { get; set; }

        public virtual DbSet<CoSecurityDefinition> CoSecurityDefinitions { get; set; }

        public virtual DbSet<SysCultureMaster> SysCultureMasters { get; set; }
        public virtual DbSet<SysCurrency> SysCurrencies { get; set; }

        public virtual DbSet<HrEducationDegree> HrEducationDegrees { get; set; }
        public virtual DbSet<SysCountryMaster> SysCountryMasters { get; set; }
        public virtual DbSet<CoReportsService> CoReportsServices { get; set; }
        public virtual DbSet<SysSecurityQuestion> SysSecurityQuestions { get; set; }
        public virtual DbSet<HrRcrtUserLanguage> HrRcrtUserLanguages { get; set; }
        public virtual DbSet<SysLanguage> SysLanguages { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUsersCompany> SysUsersCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle(Startup.GetInstance().GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var schema = Startup.GetInstance().Configuration["Schema"];
            modelBuilder.HasDefaultSchema(schema)
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");
            modelBuilder.Entity<SysSecurityQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("SYS_SECURITY_QUESTIONS_PK");

                entity.ToTable("SYS_SECURITY_QUESTIONS");

                entity.Property(e => e.QuestionId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QUESTION_ID");

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

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("QUESTION");

                entity.Property(e => e.QuestionEnglish)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("QUESTION_ENGLISH");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");
            });

            modelBuilder.Entity<HrEducationDegree>(entity =>
            {
                entity.HasKey(e => e.DegreeId)
                    .HasName("PK_HR_EUCATION_DEGREE_ID");

                entity.ToTable("HR_EDUCATION_DEGREE");

                entity.Property(e => e.DegreeId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DEGREE_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Weight)
                    .HasColumnType("NUMBER")
                    .HasColumnName("WEIGHT")
                    .HasDefaultValueSql("0");
            });


            modelBuilder.Entity<CoReportsService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK_CO_REPORTS_SERVICE_ID");

                entity.ToTable("CO_REPORTS_SERVICE");

                entity.HasIndex(e => e.CompanyId, "UC_CO_REPORTS_SERVICE_ID")
                    .IsUnique();

                entity.Property(e => e.ServiceId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SERVICE_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.FromEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FROM_EMAIL");

                entity.Property(e => e.LastUpdDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_UPD_DATE");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.LastUpdUserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_USER_NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Port)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PORT");

                entity.Property(e => e.SmtpServer)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("SMTP_SERVER");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE")
                    .HasDefaultValueSql("'S'")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });


            modelBuilder.Entity<SysCountryMaster>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK_COUNTRY_ID");

                entity.ToTable("SYS_COUNTRY_MASTER");

                entity.HasIndex(e => e.CountryCode, "CO_SYS_COUNTRY_MASTER_CODE")
                    .IsUnique();

                entity.HasIndex(e => e.CountryName, "CO_SYS_COUNTRY_MASTER_NAME")
                    .IsUnique();

                entity.Property(e => e.CountryId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_CODE");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_NAME");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");
            });


            modelBuilder.Entity<SysCurrency>(entity =>
            {
                entity.HasKey(e => e.CurrencyId)
                    .HasName("PK_SYS_CURRENCY_ID");

                entity.ToTable("SYS_CURRENCY");

                entity.HasIndex(e => e.CurrencyCode, "CO_SYS_CURRENCY_CODE")
                    .IsUnique();

                entity.HasIndex(e => e.CountryCurrency, "CO_SYS_CURRENCY_COUNTRY")
                    .IsUnique();

                entity.Property(e => e.CurrencyId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CURRENCY_ID");

                entity.Property(e => e.CountryCurrency)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY_CURRENCY");

                entity.Property(e => e.CultureCode)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CULTURE_CODE");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CURRENCY_CODE");

                entity.Property(e => e.CurrencyConvertion)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CURRENCY_CONVERTION");

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

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("SYMBOL");
            });

            modelBuilder.Entity<CoBusinessUnitsMaster>(entity =>
            {
                entity.HasKey(e => e.BusinessUnitId)
                    .HasName("PK_CO_BUSINESS_UNITS_MASTER_ID");

                entity.ToTable("CO_BUSINESS_UNITS_MASTER");

                entity.HasIndex(e => new { e.CompanyId, e.BusinessUnit1Code, e.BusinessUnit2Code, e.BusinessUnit3Code, e.BusinessUnit4Code, e.BusinessUnit5Code, e.BusinessUnit6Code }, "CO_BUSINESS_UNITS_MASTER_R01")
                    .IsUnique();

                entity.HasIndex(e => new { e.CompanyId, e.BusinessUnit1Code }, "INDX_CO_BUSINESS_UNIT_MASTR_B1");

                entity.HasIndex(e => new { e.CompanyId, e.BusinessUnit2Code }, "INDX_CO_BUSINESS_UNIT_MASTR_B2");

                entity.HasIndex(e => new { e.CompanyId, e.BusinessUnit3Code }, "INDX_CO_BUSINESS_UNIT_MASTR_B3");

                entity.HasIndex(e => new { e.CompanyId, e.BusinessUnit4Code }, "INDX_CO_BUSINESS_UNIT_MASTR_B4");

                entity.HasIndex(e => new { e.CompanyId, e.BusinessUnit5Code }, "INDX_CO_BUSINESS_UNIT_MASTR_B5");

                entity.HasIndex(e => new { e.CompanyId, e.BusinessUnit6Code }, "INDX_CO_BUSINESS_UNIT_MASTR_B6");

                entity.Property(e => e.BusinessUnitId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BUSINESS_UNIT_ID")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.BusinessUnit1Code)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT1_CODE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.BusinessUnit2Code)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT2_CODE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.BusinessUnit3Code)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT3_CODE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.BusinessUnit4Code)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT4_CODE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.BusinessUnit5Code)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT5_CODE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.BusinessUnit6Code)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT6_CODE")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.BusinessUnitOrder)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BUSINESS_UNIT_ORDER")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.CostCenterId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COST_CENTER_ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.LaborCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LABOR_CODE");

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

                entity.Property(e => e.LocationId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LOCATION_ID");

                entity.Property(e => e.PayrollRuleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PAYROLL_RULE_ID");

                entity.Property(e => e.PositionId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("POSITION_ID");

                entity.Property(e => e.Refference)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("REFFERENCE");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.TaxDisabilityInsurance)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TAX_DISABILITY_INSURANCE")
                    .HasDefaultValueSql("'P'");

                entity.Property(e => e.TimekeeperId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TIMEKEEPER_ID");

                entity.Property(e => e.Type1Id)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TYPE1_ID");

                entity.Property(e => e.Type2Id)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TYPE2_ID");

                entity.Property(e => e.Type3Id)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TYPE3_ID");
            });

            modelBuilder.Entity<CoCompanyMaster>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK_COMPANY_MASTER_ID");

                entity.ToTable("CO_COMPANY_MASTER");

                entity.HasIndex(e => e.CompanyCode, "CO_COMPANY_MASTER_COMP_CODE")
                    .IsUnique();

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS2");

                entity.Property(e => e.AgencyAeelaCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("AGENCY_AEELA_CODE");

                entity.Property(e => e.AgencyCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("AGENCY_CODE");

                entity.Property(e => e.AnalyticsDbid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ANALYTICS_DBID");

                entity.Property(e => e.AnalyticsGenId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ANALYTICS_GEN_ID");

                entity.Property(e => e.AnalyticsHrId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ANALYTICS_HR_ID");

                entity.Property(e => e.AnalyticsPrId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ANALYTICS_PR_ID");

                entity.Property(e => e.AnalyticsTaId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ANALYTICS_TA_ID");

                entity.Property(e => e.BusinessModel)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_MODEL")
                    .HasDefaultValueSql("'O'")
                    .IsFixedLength(true);

                entity.Property(e => e.BusinessUnit1Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT1_NAME")
                    .HasDefaultValueSql("'BUSINESS UNIT1'");

                entity.Property(e => e.BusinessUnit2Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT2_NAME")
                    .HasDefaultValueSql("'BUSINESS UNIT2'");

                entity.Property(e => e.BusinessUnit3Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT3_NAME")
                    .HasDefaultValueSql("'BUSINESS UNIT3'");

                entity.Property(e => e.BusinessUnit4Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT4_NAME")
                    .HasDefaultValueSql("'BUSINESS UNIT4'");

                entity.Property(e => e.BusinessUnit5Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT5_NAME")
                    .HasDefaultValueSql("'BUSINESS UNIT5'");

                entity.Property(e => e.BusinessUnit6Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("BUSINESS_UNIT6_NAME")
                    .HasDefaultValueSql("'BUSINESS UNIT6'");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.CompanyCode)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_CODE");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NAME");

                entity.Property(e => e.ConstantAeelaCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CONSTANT_AEELA_CODE");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATED_DATE")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.CryCode)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("CRY_CODE");

                entity.Property(e => e.CultureId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CULTURE_ID");

                entity.Property(e => e.Currency)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CURRENCY")
                    .HasDefaultValueSql("1 ");

                entity.Property(e => e.DefaultPayFrequency)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("DEFAULT_PAY_FREQUENCY");

                entity.Property(e => e.DefaultStandardHrsDay)
                    .HasColumnType("NUMBER(10,4)")
                    .HasColumnName("DEFAULT_STANDARD_HRS_DAY");

                entity.Property(e => e.DefaultStandardHrsPeriod)
                    .HasColumnType("NUMBER(10,4)")
                    .HasColumnName("DEFAULT_STANDARD_HRS_PERIOD");

                entity.Property(e => e.DefaultStandardHrsWeek)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DEFAULT_STANDARD_HRS_WEEK");

                entity.Property(e => e.DefaultStateId)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("DEFAULT_STATE_ID")
                    .HasDefaultValueSql("'PR'");

                entity.Property(e => e.DisabilityInsuranceId)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("DISABILITY_INSURANCE_ID");

                entity.Property(e => e.DisabilityInsurerAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISABILITY_INSURER_ACCOUNT");

                entity.Property(e => e.DisabilityInsurerAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISABILITY_INSURER_ADDRESS");

                entity.Property(e => e.DisabilityInsurerName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISABILITY_INSURER_NAME");

                entity.Property(e => e.Ein)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("EIN");

                entity.Property(e => e.EmployerChaufferId)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOYER_CHAUFFER_ID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FAX");

                entity.Property(e => e.GovernmentAgency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GOVERNMENT_AGENCY")
                    .HasDefaultValueSql("'0'")
                    .IsFixedLength(true);

                entity.Property(e => e.LaborDepartmentId)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LABOR_DEPARTMENT_ID");

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

                entity.Property(e => e.LdapDomain1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LDAP_DOMAIN1");

                entity.Property(e => e.LdapDomain2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LDAP_DOMAIN2");

                entity.Property(e => e.LdapDomain3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LDAP_DOMAIN3");

                entity.Property(e => e.LdapGroup)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LDAP_GROUP");

                entity.Property(e => e.LdapServer1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LDAP_SERVER1");

                entity.Property(e => e.LdapServer2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LDAP_SERVER2");

                entity.Property(e => e.LdapServer3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LDAP_SERVER3");

                entity.Property(e => e.LmsToken)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LMS_TOKEN");

                entity.Property(e => e.MarketId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MARKET_ID");

                entity.Property(e => e.MinimumStateSalary)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MINIMUM_STATE_SALARY")
                    .HasDefaultValueSql("8.50");

                entity.Property(e => e.ParentCompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PARENT_COMPANY_ID");

                entity.Property(e => e.ProcessorId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PROCESSOR_ID");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StateTaxIdentification)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("STATE_TAX_IDENTIFICATION");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEPHONE");

                entity.Property(e => e.TimeZoneId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TIME_ZONE_ID")
                    .HasDefaultValueSql("24");

                entity.Property(e => e.TimeoutMinutes)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TIMEOUT_MINUTES")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.Type1Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("TYPE1_NAME")
                    .HasDefaultValueSql("'TYPE1 NAME'");

                entity.Property(e => e.Type2Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("TYPE2_NAME")
                    .HasDefaultValueSql("'TYPE2 NAME'");

                entity.Property(e => e.Type3Name)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("TYPE3_NAME")
                    .HasDefaultValueSql("'TYPE3 NAME'");

                entity.Property(e => e.UseJobCodes)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USE_JOB_CODES")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.UseLdapAuthentication)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USE_LDAP_AUTHENTICATION")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCODE");
            });

            modelBuilder.Entity<CoCompanyMasterLogo>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK_COMPANY_MASTER_LOGO_ID");

                entity.ToTable("CO_COMPANY_MASTER_LOGOS");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.BrandedLogo)
                    .HasColumnType("BLOB")
                    .HasColumnName("BRANDED_LOGO");

                entity.Property(e => e.CheckLogo)
                    .HasColumnType("BLOB")
                    .HasColumnName("CHECK_LOGO");

                entity.Property(e => e.LastUpdDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_UPD_DATE");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.LastUpdUserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_USER_NAME");

                entity.Property(e => e.Logo)
                    .HasColumnType("BLOB")
                    .HasColumnName("LOGO");

                entity.HasOne(d => d.Company)
                    .WithOne(p => p.CoCompanyMasterLogo)
                    .HasForeignKey<CoCompanyMasterLogo>(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CO_COMPANY_MASTER_LOG_COID");
            });


            modelBuilder.Entity<HrRcrtApplicantDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentId)
                    .HasName("PK_HR_RCRT_APPLICANT_DOC_ID");

                entity.ToTable("HR_RCRT_APPLICANT_DOCUMENTS");

                entity.Property(e => e.DocumentId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DOCUMENT_ID");

                entity.Property(e => e.ApplicantId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("APPLICANT_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.Document)
                    .HasColumnType("BLOB")
                    .HasColumnName("DOCUMENT");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENT_TYPE")
                    .HasDefaultValueSql("'R'")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtApplicantDocuments)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HR_RCRT_APPLICANT_DOC_COMPID");
            });

            modelBuilder.Entity<HrRcrtApplicantQuery>(entity =>
            {
                entity.HasKey(e => e.QueryId)
                    .HasName("PK_HR_RCRT_APPL_QRY_USERID");

                entity.ToTable("HR_RCRT_APPLICANT_QUERY");

                entity.Property(e => e.QueryId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QUERY_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.QueryStatement)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("QUERY_STATEMENT");

                entity.Property(e => e.RunDate)
                    .HasColumnType("DATE")
                    .HasColumnName("RUN_DATE");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtApplicantQueries)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HR_RCRT_APPLICANT_QUERY_COMPID");
            });

            modelBuilder.Entity<HrRcrtApplicantsStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK_HR_RCRT_APPLI_STATUS_RECID");

                entity.ToTable("HR_RCRT_APPLICANTS_STATUS");

                entity.HasIndex(e => e.Description, "UNQ_HR_RCRT_APPLI_STATUS_DESC")
                    .IsUnique();

                entity.Property(e => e.StatusId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

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
            });

            modelBuilder.Entity<HrRcrtAudit>(entity =>
            {
                entity.HasKey(e => e.AuditId)
                    .HasName("PK_HR_RCRT_AUDIT_ID");

                entity.ToTable("HR_RCRT_AUDIT");

                entity.Property(e => e.AuditId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AUDIT_ID");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ACTION_DATE");

                entity.Property(e => e.ActionDescription)
                    .IsUnicode(false)
                    .HasColumnName("ACTION_DESCRIPTION");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.NewValue)
                    .IsUnicode(false)
                    .HasColumnName("NEW_VALUE");

                entity.Property(e => e.OldValue)
                    .IsUnicode(false)
                    .HasColumnName("OLD_VALUE");

                entity.Property(e => e.Terminal)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("TERMINAL")
                    .HasDefaultValueSql("'Unknown' ");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");
            });
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

            modelBuilder.Entity<HrRcrtCompanyConfiguration>(entity =>
            {
                entity.HasKey(e => e.ConfigurationId)
                    .HasName("PK_HR_RCRT_COMPANY_CONFIG");

                entity.ToTable("HR_RCRT_COMPANY_CONFIGURATION");

                entity.HasIndex(e => e.CompanyId, "UC_RCRT_COMPANY_CONFIG_COMP_ID")
                    .IsUnique();

                entity.Property(e => e.ConfigurationId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONFIGURATION_ID");

                entity.Property(e => e.ButtonsColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS_COLOR");

                entity.Property(e => e.ButtonsHoverColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS_HOVER_COLOR");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.ContentAreaBackgroundColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("CONTENT_AREA_BACKGROUND_COLOR");

                entity.Property(e => e.ContentAreaTextColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("CONTENT_AREA_TEXT_COLOR");

                entity.Property(e => e.EmailText1)
                    .HasColumnType("CLOB")
                    .HasColumnName("EMAIL_TEXT1");

                entity.Property(e => e.EmailText2)
                    .HasColumnType("CLOB")
                    .HasColumnName("EMAIL_TEXT2");

                entity.Property(e => e.PrivacyPolicy )
                    .HasColumnType("CLOB")
                    .HasColumnName("PRIVACY_POLICY");

                entity.Property(e => e.PrivacyPolicyEs)
                    .HasColumnType("CLOB")
                    .HasColumnName("PRIVACY_POLICY_ES");

                entity.Property(e => e.FacebookLink)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("FACEBOOK_LINK");

                entity.Property(e => e.HeaderBackgroundColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("HEADER_BACKGROUND_COLOR");

                entity.Property(e => e.HeaderImage)
                    .HasColumnType("BLOB")
                    .HasColumnName("HEADER_IMAGE");
                entity.Property(e => e.Favicon)
                    .HasColumnType("BLOB")
                    .HasColumnName("FAVICON");

                entity.Property(e => e.MainSiteImage)
                    .HasColumnType("BLOB")
                    .HasColumnName("MAIN_SITE_IMAGE");

                entity.Property(e => e.HomeImageText)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("HOME_IMAGE_TEXT");

                entity.Property(e => e.SiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SITE_NAME");

                entity.Property(e => e.HoverTabBackground)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("HOVER_TAB_BACKGROUND");

                entity.Property(e => e.InstagramLink)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("INSTAGRAM_LINK");

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

                entity.Property(e => e.LinkedinLink)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LINKEDIN_LINK");

                entity.Property(e => e.LogoImage)
                    .HasColumnType("BLOB")
                    .HasColumnName("LOGO_IMAGE");

                entity.Property(e => e.LogoUrl)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("LOGO_URL");

                entity.Property(e => e.NormalTabTextColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NORMAL_TAB_TEXT_COLOR");

                entity.Property(e => e.OfccpRequired)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OFCCP_REQUIRED")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);
                entity.Property(e => e.FooterBackgroundColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("FOOTER_BACKGROUND_COLOR");

                entity.Property(e => e.FooterTextColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("FOOTER_TEXT_COLOR");

                entity.Property(e => e.UseCustomColors)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("USE_CUSTOM_COLORS")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.SelectedTabTextColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("SELECTED_TAB_TEXT_COLOR");

                entity.Property(e => e.SendResumeToEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("SEND_RESUME_TO_EMAIL");

                entity.Property(e => e.SubheaderBackgroundColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("SUBHEADER_BACKGROUND_COLOR");
                entity.Property(e => e.RequestSocialNetworks)
                   .HasMaxLength(1)
                   .IsUnicode(false)
                   .HasColumnName("REQUEST_SOCIAL_NETWORKS");

                entity.Property(e => e.TitlesColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("TITLES_COLOR");

                entity.Property(e => e.TwitterLink)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TWITTER_LINK");

                entity.Property(e => e.VersionToShow)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VERSION_TO_SHOW")
                    .HasDefaultValueSql("'F'")
                    .IsFixedLength(true);
                entity.Property(e => e.Buttons2Color)
                   .HasMaxLength(32)
                   .IsUnicode(false)
                   .HasColumnName("BUTTONS2_COLOR");

                entity.Property(e => e.Buttons2HoverColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS2_HOVER_COLOR");

                entity.Property(e => e.Buttons2TextColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS2_TEXT_COLOR");

                entity.Property(e => e.Buttons3Color)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS3_COLOR");

                entity.Property(e => e.Buttons3HoverColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS3_HOVER_COLOR");

                entity.Property(e => e.Buttons3TextColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS3_TEXT_COLOR");

                entity.Property(e => e.ButtonsTextColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("BUTTONS_TEXT_COLOR");
                entity.Property(e => e.StepperColor)
                  .HasMaxLength(32)
                  .IsUnicode(false)
                  .HasColumnName("STEPPER_COLOR");

                entity.Property(e => e.StepperFontColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("STEPPER_FONT_COLOR");
                entity.Property(e => e.LangButtonColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LANG_BUTTON_COLOR");
                entity.Property(e => e.SwitchBackColor)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("SWITCH_BACK_COLOR");
            });


            modelBuilder.Entity<HrRcrtExternalWebsiteInfo>(entity =>
            {
                entity.HasKey(e => e.ConfigurationId)
                    .HasName("PK_HR_RCRT_EXTERNAL_WEBSITE");

                entity.ToTable("HR_RCRT_EXTERNAL_WEBSITE_INFO");

                entity.HasIndex(e => new { e.CompanyId, e.PageName }, "UC_HR_RCRT_EXT_WEBSITE_INF_U01")
                    .IsUnique();

                entity.Property(e => e.ConfigurationId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONFIGURATION_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.Image1)
                    .HasColumnType("BLOB")
                    .HasColumnName("IMAGE1");

                entity.Property(e => e.Image2)
                    .HasColumnType("BLOB")
                    .HasColumnName("IMAGE2");

                entity.Property(e => e.Image3)
                    .HasColumnType("BLOB")
                    .HasColumnName("IMAGE3");

                entity.Property(e => e.Image4)
                    .HasColumnType("BLOB")
                    .HasColumnName("IMAGE4");

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

                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PAGE_NAME");

                entity.Property(e => e.Text1English)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1_ENGLISH");

                entity.Property(e => e.Text1Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1_SPANISH");

                entity.Property(e => e.Text2English)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2_ENGLISH");

                entity.Property(e => e.Text2Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2_SPANISH");

                entity.Property(e => e.Text3English)
                    .IsUnicode(false)
                    .HasColumnName("TEXT3_ENGLISH");

                entity.Property(e => e.Text3Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TEXT3_SPANISH");

                entity.Property(e => e.Text4English)
                    .IsUnicode(false)
                    .HasColumnName("TEXT4_ENGLISH");

                entity.Property(e => e.Text4Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TEXT4_SPANISH");

                entity.Property(e => e.Title1English)
                    .IsUnicode(false)
                    .HasColumnName("TITLE1_ENGLISH");

                entity.Property(e => e.Title1Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TITLE1_SPANISH");

                entity.Property(e => e.Title2English)
                    .IsUnicode(false)
                    .HasColumnName("TITLE2_ENGLISH");

                entity.Property(e => e.Title2Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TITLE2_SPANISH");

                entity.Property(e => e.Title3English)
                    .IsUnicode(false)
                    .HasColumnName("TITLE3_ENGLISH");

                entity.Property(e => e.Title3Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TITLE3_SPANISH");

                entity.Property(e => e.Title4English)
                    .IsUnicode(false)
                    .HasColumnName("TITLE4_ENGLISH");

                entity.Property(e => e.Title4Spanish)
                    .IsUnicode(false)
                    .HasColumnName("TITLE4_SPANISH");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtExternalWebsiteInfos)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_EXT_WEBSITE_COMP_ID");
            });

            modelBuilder.Entity<HrRcrtInterview>(entity =>
            {
                entity.HasKey(e => e.InterviewId)
                    .HasName("PK_HR_RCRT_INTERVIEWS_USERID");

                entity.ToTable("HR_RCRT_INTERVIEWS");

                entity.Property(e => e.InterviewId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("INTERVIEW_ID");

                entity.Property(e => e.ApplicantId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("APPLICANT_ID");

                entity.Property(e => e.Attachment)
                    .HasColumnType("BLOB")
                    .HasColumnName("ATTACHMENT");

                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FILE_NAME");

                entity.Property(e => e.InterviewFromDate)
                    .HasColumnType("DATE")
                    .HasColumnName("INTERVIEW_FROM_DATE");

                entity.Property(e => e.InterviewToDate)
                    .HasColumnType("DATE")
                    .HasColumnName("INTERVIEW_TO_DATE");

                entity.Property(e => e.InterviewerId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("INTERVIEWER_ID");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("NOTES");

                entity.Property(e => e.Rating)
                    .HasColumnType("NUMBER")
                    .HasColumnName("RATING");

                entity.Property(e => e.RequestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REQUEST_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.Applicant)
                    .WithMany(p => p.HrRcrtInterviews)
                    .HasForeignKey(d => d.ApplicantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_INTERVIEWS_APPL_ID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtInterviews)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_INTERVIEWS_COMP_ID");

                entity.HasOne(d => d.Interviewer)
                    .WithMany(p => p.HrRcrtInterviews)
                    .HasForeignKey(d => d.InterviewerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_INTERVIEWS_INTWR_ID");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.HrRcrtInterviews)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_INTERVIEWS_RQST_ID");
            });

            modelBuilder.Entity<HrRcrtInterviewer>(entity =>
            {
                entity.HasKey(e => e.InterviewerId)
                    .HasName("PK_HR_RCRT_INTERVIEWERS_USERID");

                entity.ToTable("HR_RCRT_INTERVIEWERS");

                entity.HasIndex(e => new { e.CompanyId, e.UserId }, "UC_HR_RCRT_INTERVIEWER_CID_UID")
                    .IsUnique();

                entity.Property(e => e.InterviewerId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("INTERVIEWER_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEPHONE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");
            });

            modelBuilder.Entity<HrRcrtOfccpText>(entity =>
            {
                entity.HasKey(e => e.OfccpId)
                    .HasName("PK_HHR_RCRT_OFCCP_TEXT");

                entity.ToTable("HR_RCRT_OFCCP_TEXT");

                entity.Property(e => e.OfccpId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OFCCP_ID");

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

                entity.Property(e => e.OfccpDisabilityEnglish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_DISABILITY_ENGLISH");

                entity.Property(e => e.OfccpDisabilitySpanish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_DISABILITY_SPANISH");

                entity.Property(e => e.OfccpEthnicEnglish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_ETHNIC_ENGLISH");

                entity.Property(e => e.OfccpEthnicSpanish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_ETHNIC_SPANISH");

                entity.Property(e => e.OfccpGenderEnglish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_GENDER_ENGLISH");

                entity.Property(e => e.OfccpGenderSpanish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_GENDER_SPANISH");

                entity.Property(e => e.OfccpVeteranEnglish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_VETERAN_ENGLISH");

                entity.Property(e => e.OfccpVeteranSpanish)
                    .HasColumnType("CLOB")
                    .HasColumnName("OFCCP_VETERAN_SPANISH");
            });

            modelBuilder.Entity<HrRcrtPositionProfileType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK_POS_PROF_TYPE_COMP_ID");

                entity.ToTable("HR_RCRT_POSITION_PROFILE_TYPES");

                entity.Property(e => e.TypeId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TYPE_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.EnglishDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ENGLISH_DESCRIPTION");

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

                entity.Property(e => e.ProfileType)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("PROFILE_TYPE")
                    .IsFixedLength(true);

                entity.Property(e => e.SpanishDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("SPANISH_DESCRIPTION");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtPositionProfileTypes)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RT_POS_PROF_TYPES_COID");
            });

            modelBuilder.Entity<HrRcrtPositionRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK_HR_RCRT_POSITION_REQUEST_ID");

                entity.ToTable("HR_RCRT_POSITION_REQUESTS");

                entity.Property(e => e.RequestId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REQUEST_ID");

                entity.Property(e => e.BusinessUnitId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BUSINESS_UNIT_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.CompanyProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_PROFILE_TYPE");

                entity.Property(e => e.EducationProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EDUCATION_PROFILE_TYPE");

                entity.Property(e => e.ExperienceProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EXPERIENCE_PROFILE_TYPE");

                entity.Property(e => e.ExpertiseProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EXPERTISE_PROFILE_TYPE");

                entity.Property(e => e.ExternalPostingEnd)
                    .HasColumnType("DATE")
                    .HasColumnName("EXTERNAL_POSTING_END");

                entity.Property(e => e.ExternalPostingStart)
                    .HasColumnType("DATE")
                    .HasColumnName("EXTERNAL_POSTING_START");

                entity.Property(e => e.InternalPostingEnd)
                    .HasColumnType("DATE")
                    .HasColumnName("INTERNAL_POSTING_END");

                entity.Property(e => e.InternalPostingStart)
                    .HasColumnType("DATE")
                    .HasColumnName("INTERNAL_POSTING_START");

                entity.Property(e => e.JobLevelProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("JOB_LEVEL_PROFILE_TYPE");

                entity.Property(e => e.TimesViewed)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TIMES_VIEWED");

                entity.Property(e => e.JobTypeProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("JOB_TYPE_PROFILE_TYPE");

                entity.Property(e => e.LanguageProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LANGUAGE_PROFILE_TYPE");

                entity.Property(e => e.LastUpdDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_UPD_DATE");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.LastUpdUserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_USER_NAME");

                entity.Property(e => e.LocationProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LOCATION_PROFILE_TYPE");

                entity.Property(e => e.PositionDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("POSITION_DESCRIPTION");

                entity.Property(e => e.PositionDetailsEnglish)
                    .HasColumnType("CLOB")
                    .HasColumnName("POSITION_DETAILS_ENGLISH");

                entity.Property(e => e.PositionDetailsSpanish)
                    .HasColumnType("CLOB")
                    .HasColumnName("POSITION_DETAILS_SPANISH");

                entity.Property(e => e.PositionsNeeded)
                    .HasColumnType("NUMBER")
                    .HasColumnName("POSITIONS_NEEDED");

                entity.Property(e => e.PostingType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("POSTING_TYPE")
                    .HasDefaultValueSql("'B'")
                    .IsFixedLength(true);

                entity.Property(e => e.RequestDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REQUEST_DATE");

                entity.Property(e => e.RequestStatusId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REQUEST_STATUS_ID");

                entity.Property(e => e.SalaryProfileType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SALARY_PROFILE_TYPE");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtPositionRequests)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_POSITION_REQ_COMPID");

                entity.HasOne(d => d.CompanyProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestCompanyProfileTypeNavigations)
                    .HasForeignKey(d => d.CompanyProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_CO_TYPE");

                entity.HasOne(d => d.EducationProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestEducationProfileTypeNavigations)
                    .HasForeignKey(d => d.EducationProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_ED_TYPE");

                entity.HasOne(d => d.ExperienceProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestExperienceProfileTypeNavigations)
                    .HasForeignKey(d => d.ExperienceProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_YE_TYPE");

                entity.HasOne(d => d.ExpertiseProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestExpertiseProfileTypeNavigations)
                    .HasForeignKey(d => d.ExpertiseProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_EX_TYPE");

                entity.HasOne(d => d.JobLevelProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestJobLevelProfileTypeNavigations)
                    .HasForeignKey(d => d.JobLevelProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_JL_TYPE");

                entity.HasOne(d => d.JobTypeProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestJobTypeProfileTypeNavigations)
                    .HasForeignKey(d => d.JobTypeProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_JT_TYPE");

                entity.HasOne(d => d.LanguageProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestLanguageProfileTypeNavigations)
                    .HasForeignKey(d => d.LanguageProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_LG_TYPE");

                entity.HasOne(d => d.LocationProfileTypeNavigation)
                    .WithMany(p => p.HrRcrtPositionRequestLocationProfileTypeNavigations)
                    .HasForeignKey(d => d.LocationProfileType)
                    .HasConstraintName("FK_HR_RCRT_POS_REQUEST_LO_TYPE");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.HrRcrtPositionRequests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .HasConstraintName("FK_RT_USER_REQ_STATUSID");
            });

            modelBuilder.Entity<HrRcrtRequestQuestChoice>(entity =>
            {
                entity.HasKey(e => e.ChoiceId)
                    .HasName("PK_HR_RCRT_REQUEST_CHOICE_ID");

                entity.ToTable("HR_RCRT_REQUEST_QUEST_CHOICE");

                entity.Property(e => e.ChoiceId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CHOICE_ID");

                entity.Property(e => e.Choice)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("CHOICE");

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

                entity.Property(e => e.QuestionId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTION_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.HrRcrtRequestQuestChoices)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_REQUEST_CHOICE_Q_ID");
            });

            modelBuilder.Entity<HrRcrtRequestQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK_HR_RCRT_REQUEST_QUESTION_ID");

                entity.ToTable("HR_RCRT_REQUEST_QUESTIONS");

                entity.Property(e => e.QuestionId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QUESTION_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

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

                entity.Property(e => e.Question)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("QUESTION");

                entity.Property(e => e.QuestionType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("QUESTION_TYPE")
                    .IsFixedLength(true);

                entity.Property(e => e.RequestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REQUEST_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtRequestQuestions)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_REQUEST_QUES_COMPID");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.HrRcrtRequestQuestions)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_REQUEST_QUEST_REQID");
            });

            modelBuilder.Entity<HrRcrtRequestQuestnAnswer>(entity =>
            {
                entity.HasKey(e => e.AnswerId)
                    .HasName("PK_HR_RCRT_REQUEST_ANSWER_ID");

                entity.ToTable("HR_RCRT_REQUEST_QUESTN_ANSWERS");

                entity.Property(e => e.AnswerId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ANSWER_ID");

                entity.Property(e => e.Answer)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER");

                entity.Property(e => e.ApplicantId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("APPLICANT_ID");

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

                entity.Property(e => e.QuestionId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTION_ID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.HrRcrtRequestQuestnAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_REQUEST_ANSWER_Q_ID");
            });

            modelBuilder.Entity<HrRcrtRequestStatusMaster>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK_HR_RCRT_STATUS_MASTER_ID");

                entity.ToTable("HR_RCRT_REQUEST_STATUS_MASTER");

                entity.Property(e => e.StatusId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.EnglishDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ENGLISH_DESCRIPTION");

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

                entity.Property(e => e.SpanishDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("SPANISH_DESCRIPTION");

                entity.Property(e => e.TypeCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_CODE")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtRequestStatusMasters)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_REQ_STATU_MSTR_COID");
            });

            modelBuilder.Entity<HrRcrtTestAnswer>(entity =>
            {
                entity.HasKey(e => e.AnswerId);

                entity.ToTable("HR_RCRT_TEST_ANSWERS");

                entity.HasIndex(e => new { e.AnswerId, e.QuestionId }, "UC_HR_RCRT_TEST_ANSWERS")
                    .IsUnique();

                entity.Property(e => e.AnswerId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ANSWER_ID");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER");

                entity.Property(e => e.AnswerValue)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ANSWER_VALUE");

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

                entity.Property(e => e.QuestionId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTION_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.HrRcrtTestAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_ANSWERS_QSTID");
            });

            modelBuilder.Entity<HrRcrtTestQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.ToTable("HR_RCRT_TEST_QUESTIONS");

                entity.HasIndex(e => new { e.QuestionId, e.TestId }, "UC_HR_RCRT_TEST_QUESTIONS")
                    .IsUnique();

                entity.Property(e => e.QuestionId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QUESTION_ID");

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

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("QUESTION");

                entity.Property(e => e.QuestionOrder)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTION_ORDER");

                entity.Property(e => e.QuestionValue)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTION_VALUE");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.TestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TEST_ID");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.HrRcrtTestQuestions)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_QUEST_TESTID");
            });

            modelBuilder.Entity<HrRcrtTestRecord>(entity =>
            {
                entity.HasKey(e => e.TestId);

                entity.ToTable("HR_RCRT_TEST_RECORD");

                entity.Property(e => e.TestId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TEST_ID");

                entity.Property(e => e.Author)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("AUTHOR");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.ExpirationDays)
                    .HasColumnType("NUMBER")
                    .HasColumnName("EXPIRATION_DAYS")
                    .HasDefaultValueSql("180");

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

                entity.Property(e => e.MinimumScore)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MINIMUM_SCORE")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.TimePerQuestion)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TIME_PER_QUESTION");

                entity.Property(e => e.TimePerTest)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TIME_PER_TEST");

                entity.Property(e => e.TimerBy)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TIMER_BY")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtTestRecords)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_RECORD_COMPID");
            });

            modelBuilder.Entity<HrRcrtTestRegistration>(entity =>
            {
                entity.HasKey(e => e.RegistrationId);

                entity.ToTable("HR_RCRT_TEST_REGISTRATION");

                entity.HasIndex(e => new { e.RegistrationId, e.TestId }, "UC_HR_RCRT_TEST_REGISTRATION")
                    .IsUnique();

                entity.Property(e => e.RegistrationId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REGISTRATION_ID");

                entity.Property(e => e.ApplicantFullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("APPLICANT_FULL_NAME");

                entity.Property(e => e.ApplicantId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("APPLICANT_ID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REGISTRATION_DATE")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.RequestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REQUEST_ID");

                entity.Property(e => e.Score)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SCORE");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.TestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TEST_ID");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.HrRcrtTestRegistrations)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_REG_REQID");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.HrRcrtTestRegistrations)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_REG_TESTID");
            });

            modelBuilder.Entity<HrRcrtTestRequest>(entity =>
            {
                entity.HasKey(e => new { e.TestId, e.RequestId });

                entity.ToTable("HR_RCRT_TEST_REQUESTS");

                entity.Property(e => e.TestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TEST_ID");

                entity.Property(e => e.RequestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REQUEST_ID");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.HrRcrtTestRequests)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_REQUESTS_REQID");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.HrRcrtTestRequests)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_REQUESTS_TSTID");
            });

            modelBuilder.Entity<HrRcrtTestResult>(entity =>
            {
                entity.HasKey(e => e.ResultId);

                entity.ToTable("HR_RCRT_TEST_RESULTS");

                entity.HasIndex(e => new { e.ResultId, e.RegistrationId }, "UC_HR_RCRT_TEST_RESULTS")
                    .IsUnique();

                entity.Property(e => e.ResultId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RESULT_ID");

                entity.Property(e => e.AnswerIndex)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ANSWER_INDEX");

                entity.Property(e => e.AnswerValue)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ANSWER_VALUE");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.QuestionId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QUESTION_ID");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REGISTRATION_DATE")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.RegistrationId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REGISTRATION_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.HrRcrtTestResults)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_RESULTS_QSTID");

                entity.HasOne(d => d.Registration)
                    .WithMany(p => p.HrRcrtTestResults)
                    .HasForeignKey(d => d.RegistrationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_TEST_RESULTS_REGID");
            });

            modelBuilder.Entity<HrRcrtApplicant>(entity =>
            {
                entity.HasKey(e => e.ApplicantId)
                    .HasName("PK_HR_RCRT_APPLICANTS_RECID");

                entity.ToTable("HR_RCRT_APPLICANTS");

                entity.HasIndex(e => new { e.RequestId, e.UserName }, "UNQ_HR_RCRT_APPLICANTS_ID")
                    .IsUnique();

                entity.Property(e => e.ApplicantId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("APPLICANT_ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS1");

                entity.Property(e => e.Address1Street)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS1_STREET");

                entity.Property(e => e.Address2)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS2");

                entity.Property(e => e.Address2Street)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS2_STREET");

                entity.Property(e => e.ApplicantStatus)
                    .HasColumnType("NUMBER")
                    .HasColumnName("APPLICANT_STATUS")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.CityStreet)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY_STREET");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.MiddleName)
                    .IsUnicode(false)
                    .HasColumnName("MIDDLE_NAME");

                entity.Property(e => e.FridayAm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FRIDAY_AM");

                entity.Property(e => e.FridayPm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("FRIDAY_PM");

                entity.Property(e => e.LastName1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME1");

                entity.Property(e => e.LastName2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME2");

                entity.Property(e => e.MobileTelephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MOBILE_TELEPHONE");

                entity.Property(e => e.MondayAm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MONDAY_AM");

                entity.Property(e => e.MondayPm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MONDAY_PM");

                entity.Property(e => e.OfccpDisability)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OFCCP_DISABILITY")
                    .IsFixedLength(true);

                entity.Property(e => e.OfccpEthnicity)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OFCCP_ETHNICITY")
                    .IsFixedLength(true);

                entity.Property(e => e.OfccpGender)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OFCCP_GENDER")
                    .IsFixedLength(true);

                entity.Property(e => e.OfccpVeteran)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("OFCCP_VETERAN")
                    .IsFixedLength(true);

                entity.Property(e => e.RequestDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REQUEST_DATE");

                entity.Property(e => e.RequestId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("REQUEST_ID");

                entity.Property(e => e.SaturdayAm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SATURDAY_AM");

                entity.Property(e => e.SaturdayPm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SATURDAY_PM");

                entity.Property(e => e.SocialNetworkAddress1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_NETWORK_ADDRESS1");

                entity.Property(e => e.SocialNetworkAddress2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_NETWORK_ADDRESS2");

                entity.Property(e => e.SocialNetworkAddress3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_NETWORK_ADDRESS3");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StateStreet)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE_STREET");

                entity.Property(e => e.SundayAm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUNDAY_AM");

                entity.Property(e => e.SundayPm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUNDAY_PM");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEPHONE");

                entity.Property(e => e.ThursdayAm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("THURSDAY_AM");

                entity.Property(e => e.ThursdayPm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("THURSDAY_PM");

                entity.Property(e => e.TuesdayAm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TUESDAY_AM");

                entity.Property(e => e.TuesdayPm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TUESDAY_PM");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.WednesdayAm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("WEDNESDAY_AM");

                entity.Property(e => e.WednesdayPm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("WEDNESDAY_PM");

                entity.Property(e => e.MorningShift)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MORNING_SHIFT");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCODE");

                entity.Property(e => e.ZipcodeStreet)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCODE_STREET");
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtApplicants)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.HrRcrtPositionRequest)
                    .WithMany(p => p.HrRcrtApplicants)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<HrRcrtUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_HR_RCRT_USERS_USERID");

                entity.ToTable("HR_RCRT_USERS");

                entity.HasIndex(e => new { e.UserName, e.CompanyId }, "CO_HR_RCRT_USERS_USER_NAME")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS1");

                entity.Property(e => e.Address1Street)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS1_STREET");

                entity.Property(e => e.Address2)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS2");

                entity.Property(e => e.Address2Street)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS2_STREET");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.CityStreet)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY_STREET");

                entity.Property(e => e.Comments)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATION_DATE")
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.Email2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL2");

                entity.Property(e => e.EmailFormat)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_FORMAT")
                    .HasDefaultValueSql("'H'")
                    .IsFixedLength(true);

                entity.Property(e => e.EmailFrequency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_FREQUENCY")
                    .HasDefaultValueSql("'D'")
                    .IsFixedLength(true);

                entity.Property(e => e.FailedLoginCount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FAILED_LOGIN_COUNT")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("MIDDLE_NAME");

                entity.Property(e => e.InactiveDate)
                    .HasColumnType("DATE")
                    .HasColumnName("INACTIVE_DATE");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_LOGIN");

                entity.Property(e => e.LastName1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME1");

                entity.Property(e => e.LastName2)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("LAST_NAME2");

                entity.Property(e => e.LastPasswordChange)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_PASSWORD_CHANGE");

                entity.Property(e => e.MobileTelephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MOBILE_TELEPHONE");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RemainAnonymous)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("REMAIN_ANONYMOUS")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.SendEmailMatching)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEND_EMAIL_MATCHING")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.SendFutureInformation)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEND_FUTURE_INFORMATION")
                    .HasDefaultValueSql("'Y'")
                    .IsFixedLength(true);

                entity.Property(e => e.SocialNetworkAddress1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_NETWORK_ADDRESS1");

                entity.Property(e => e.SocialNetworkAddress2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_NETWORK_ADDRESS2");

                entity.Property(e => e.SocialNetworkAddress3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SOCIAL_NETWORK_ADDRESS3");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StateStreet)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE_STREET");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEPHONE");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USER_NAME");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCODE");

                entity.Property(e => e.ZipcodeStreet)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCODE_STREET");
            });

            modelBuilder.Entity<HrRcrtUserDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentId)
                    .HasName("PK_HR_RCRT_USER_DOC_USERID");

                entity.ToTable("HR_RCRT_USER_DOCUMENTS");

                entity.Property(e => e.DocumentId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DOCUMENT_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.Document)
                    .HasColumnType("BLOB")
                    .HasColumnName("DOCUMENT");

                entity.Property(e => e.DocumentFormat)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENT_FORMAT");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENT_NAME");

                entity.Property(e => e.DocumentTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENT_TITLE");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENT_TYPE")
                    .HasDefaultValueSql("'R'")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtUserDocuments)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HR_RCRT_USER_DOCUMENTS_COMPID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HrRcrtUserDocuments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HR_RCRT_USER_DOCUMENTS_USERID");
            });

            modelBuilder.Entity<HrRcrtUserEducation>(entity =>
            {
                entity.HasKey(e => e.EducationId)
                    .HasName("PK_HR_RCRT_USER_EDUCATION_ID");

                entity.ToTable("HR_RCRT_USER_EDUCATION");

                entity.Property(e => e.EducationId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EDUCATION_ID");

                entity.Property(e => e.Comments)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.CountryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.DegreeId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DEGREE_ID");

                entity.Property(e => e.Gpa)
                    .HasColumnType("NUMBER(5,2)")
                    .HasColumnName("GPA");

                entity.Property(e => e.Graduated)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GRADUATED")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.GraduatedYear)
                    .HasPrecision(4)
                    .HasColumnName("GRADUATED_YEAR");

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

                entity.Property(e => e.Major)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MAJOR");

                entity.Property(e => e.SchoolName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("SCHOOL_NAME");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.YearsCompleted)
                    .HasPrecision(2)
                    .HasColumnName("YEARS_COMPLETED");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtUserEducations)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_USER_EDUC_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HrRcrtUserEducations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_RCRT_USER_EDUC_EMPL_ID");
            });

            modelBuilder.Entity<HrRcrtUserEmploymentHist>(entity =>
            {
                entity.HasKey(e => e.EmploymentId)
                    .HasName("PK_HR_USER_EMPLOYMENT_HIST_ID");

                entity.ToTable("HR_RCRT_USER_EMPLOYMENT_HIST");

                entity.Property(e => e.EmploymentId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMPLOYMENT_ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS2");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Comments)
                    .IsUnicode(false)
                    .HasColumnName("COMMENTS");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("COMPANY_NAME");

                entity.Property(e => e.CurrentJob)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CURRENT_JOB")
                    .HasDefaultValueSql("'N'")
                    .IsFixedLength(true);

                entity.Property(e => e.EndDate)
                    .HasColumnType("DATE")
                    .HasColumnName("END_DATE");

                entity.Property(e => e.EndSalary)
                    .HasColumnType("NUMBER")
                    .HasColumnName("END_SALARY");

                entity.Property(e => e.JobDescription)
                    .IsUnicode(false)
                    .HasColumnName("JOB_DESCRIPTION");

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

                entity.Property(e => e.PositionTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("POSITION_TITLE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("DATE")
                    .HasColumnName("START_DATE");

                entity.Property(e => e.StartSalary)
                    .HasColumnType("NUMBER")
                    .HasColumnName("START_SALARY");

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.SupervisorName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUPERVISOR_NAME");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEPHONE");

                entity.Property(e => e.TerminationReason)
                    .IsUnicode(false)
                    .HasColumnName("TERMINATION_REASON");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCODE");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtUserEmploymentHists)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_USER_EMPLOYMENT_HIST_CID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HrRcrtUserEmploymentHists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HR_USER_EMPLOYMENT_HIST_UID");
            });

            modelBuilder.Entity<HrRcrtUserSecurityQuestion>(entity =>
            {
                entity.HasKey(e => e.SecurityId)
                    .HasName("PK_RT_EMP_SEC_QUESTION_ID");

                entity.ToTable("HR_RCRT_USER_SECURITY_QUESTION");

                entity.Property(e => e.SecurityId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SECURITY_ID");

                entity.Property(e => e.Answer)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ANSWER");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

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

                entity.Property(e => e.Question)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("QUESTION");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.HrRcrtUserSecurityQuestions)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RT_EMP_SEC_QUESCTION_COMPID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HrRcrtUserSecurityQuestions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_SEC_QUESTION_USERID");
            });

            modelBuilder.Entity<HrRcrtUsersProfile>(entity =>
            {
                entity.HasKey(e => e.ProfileId)
                    .HasName("PK_HR_RCRT_USERS_PROFILE_ID");

                entity.ToTable("HR_RCRT_USERS_PROFILE");

                entity.HasIndex(e => new { e.UserId, e.TypeId }, "UC_HR_RCRT_USERS_PROFILE_IDS")
                    .IsUnique();

                entity.Property(e => e.ProfileId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PROFILE_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

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
                    .HasColumnName("LAST_UPD_USER_NAME");

                entity.Property(e => e.ProfileType)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("PROFILE_TYPE")
                    .IsFixedLength(true);

                entity.Property(e => e.TypeId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TYPE_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");
            });


            modelBuilder.Entity<SysStatesMaster>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK_STATE_ID");

                entity.ToTable("SYS_STATES_MASTER");

                entity.HasIndex(e => e.Description, "CO_SYS_STATES_MASTER_DESC")
                    .IsUnique();

                entity.Property(e => e.StateId)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");
            });

            modelBuilder.Entity<SysZipcode>(entity =>
            {
                entity.HasKey(e => e.Zipcode)
                    .HasName("PK_ZIPCODES");

                entity.ToTable("SYS_ZIPCODES");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ZIPCODE");

                entity.Property(e => e.Areacode)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("AREACODE");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.County)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("COUNTY");

                entity.Property(e => e.Countyansi)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("COUNTYANSI");

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

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.Stateansi)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("STATEANSI");

                entity.Property(e => e.Timezone)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TIMEZONE");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.SysZipcodes)
                    .HasForeignKey(d => d.State)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZIPCODE_STATE");
            });


            modelBuilder.Entity<CoSecurityDefinition>(entity =>
            {
                entity.HasKey(e => e.SecurityId)
                    .HasName("PK_SECURITY_DEFINITION_ID");

                entity.ToTable("CO_SECURITY_DEFINITION");

                entity.HasIndex(e => e.CompanyId, "UNQ_CO_SECURITY_DEFINIT_COMPID")
                    .IsUnique();

                entity.Property(e => e.SecurityId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SECURITY_ID");

                entity.Property(e => e.CanRepeatePassword)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CAN_REPEATE_PASSWORD")
                    .HasDefaultValueSql("'Y'");

                entity.Property(e => e.ChangeEveryDays)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CHANGE_EVERY_DAYS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.MinLowerCharacters)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MIN_LOWER_CHARACTERS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MinNumericCharacters)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MIN_NUMERIC_CHARACTERS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MinSymbolCharacters)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MIN_SYMBOL_CHARACTERS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MinUpperCharacters)
                    .HasColumnType("NUMBER")
                    .HasColumnName("MIN_UPPER_CHARACTERS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PasswordCanBeTheUsername)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD_CAN_BE_THE_USERNAME")
                    .HasDefaultValueSql("'N'");

                entity.Property(e => e.PasswordLength)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PASSWORD_LENGTH")
                    .HasDefaultValueSql("8");

                entity.Property(e => e.ReqUpperLowerCharacters)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("REQ_UPPER_LOWER_CHARACTERS")
                    .HasDefaultValueSql("'Y'");
            });

            modelBuilder.Entity<SysCultureMaster>(entity =>
            {
                entity.HasKey(e => e.CultureId)
                    .HasName("PK_SYS_CULTURE_MASTER_ID");

                entity.ToTable("SYS_CULTURE_MASTER");

                entity.HasIndex(e => e.CultureCode, "UC_SYS_CULTURE_MASTER_CODE")
                    .IsUnique();

                entity.Property(e => e.CultureId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CULTURE_ID")
                    .HasDefaultValueSql("1                     ");

                entity.Property(e => e.CultureCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CULTURE_CODE");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.LastUpdDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_UPD_DATE")
                    .HasDefaultValueSql("SYSDATE\n");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.LastUpdUserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_USER_NAME")
                    .HasDefaultValueSql("USER");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");
            });

            modelBuilder.Entity<HrRcrtUserLanguage>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_HR_RCRT_USER_RECORD_ID");

                entity.ToTable("HR_RCRT_USER_LANGUAGES");

                entity.Property(e => e.RecordId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RECORD_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("COMPANY_ID");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LANGUAGE_ID");

                entity.Property(e => e.LastUpdDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_UPD_DATE")
                    .HasDefaultValueSql("SYSDATE\n");

                entity.Property(e => e.LastUpdTerminal)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_TERMINAL");

                entity.Property(e => e.LastUpdUserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("LAST_UPD_USER_NAME")
                    .HasDefaultValueSql("USER");

                entity.Property(e => e.ReadingProficiency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("READING_PROFICIENCY")
                    .IsFixedLength(true);

                entity.Property(e => e.SpeakingProficiency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SPEAKING_PROFICIENCY")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.WritingProficiency)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("WRITING_PROFICIENCY")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SysLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("SYS_LANGUAGES");

                entity.ToTable("SYS_LANGUAGES");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LANGUAGE_ID");

                entity.Property(e => e.Culture)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CULTURE")
                    .HasDefaultValueSql("'EN'");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LANGUAGE");

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

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .HasDefaultValueSql("'A'");
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
