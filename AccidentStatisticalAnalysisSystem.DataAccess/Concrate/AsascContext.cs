
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.EntityFreamwork.Mapping;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using Microsoft.EntityFrameworkCore;


namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate
{
    public class AsascContext : DbContext
    {
        public AsascContext() : base()
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<DangerousMaterialIncident> dangerousMaterialIncidents { get; set; }
        public DbSet<FinanceIncident> FinanceIncidents { get; set; }
        public DbSet<FormOfTheChemicalIncident> FormOfTheChemicalIncidents { get; set; }
        public DbSet<IgnitionIncident> IgnitionIncidents { get; set; }
        public DbSet<IncidentTypeIncident> IncidentTypeIncidents { get; set; }
        public DbSet<Proces> Proces { get; set; }
        public DbSet<ProcesIncident> ProcesIncidents { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RootIncident> RootIncidents { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<DangerousMaterial> DangerousMaterials { get; set; }
        public DbSet<EmployerType> EmployerTypes { get; set; }
        public DbSet<EnvironmentalDamage> EnvironmentalDamages { get; set; }
        public DbSet<EnvironmentalDamageCategory> EnvironmentalDamageCategories { get; set; }
        public DbSet<Finance> Finances { get; set; }
        public DbSet<FormOfTheChemical> FormOfTheChemicals { get; set; }
        public DbSet<Ignition> Ignitions { get; set; }
        public DbSet<IncidentType> IncidentTypes { get; set; }
        public DbSet<IncidentTypeCategory> IncidentTypeCategories { get; set; }
        public DbSet<Nace> Naces { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OperatingMode> OperatingModes { get; set; }
        public DbSet<ProcesCategory1> ProcesCategory1s { get; set; }
        public DbSet<ProcesCategory2> ProcesCategory2s { get; set; }
        public DbSet<ProcesCategory3> ProcesCategory3s { get; set; }
        public DbSet<RootCategory1> RootCategory1s { get; set; }
        public DbSet<RootCategory2> RootCategory2s { get; set; }
        public DbSet<RootCategory3> RootCategory3s { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CityMap());
            modelBuilder.ApplyConfiguration(new DangerousMaterialIncidentsMap());
            modelBuilder.ApplyConfiguration(new EnvironmentalDamageIncidentsMap());
            modelBuilder.ApplyConfiguration(new FinanceIncidentMap());
            modelBuilder.ApplyConfiguration(new FormOfTheChemicalIncidentMap());
            modelBuilder.ApplyConfiguration(new IgnitionIncidentMap());
            modelBuilder.ApplyConfiguration(new IncidentMap());
            modelBuilder.ApplyConfiguration(new IncidentTypeIncidentMap());
            modelBuilder.ApplyConfiguration(new ProcesIncidentMap());
            modelBuilder.ApplyConfiguration(new ProcesMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new RootIncidentMap());
            modelBuilder.ApplyConfiguration(new RootMap());
            modelBuilder.ApplyConfiguration(new SuggestionMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new DangerousMaterialMap());
            modelBuilder.ApplyConfiguration(new EmployerTypeMap());
            modelBuilder.ApplyConfiguration(new EnvironmentalDamageCategoryMap());
            modelBuilder.ApplyConfiguration(new EnvironmentalDamageMap());
            modelBuilder.ApplyConfiguration(new FinanceMap());
            modelBuilder.ApplyConfiguration(new FormOfChemicalMap());
            modelBuilder.ApplyConfiguration(new IgnitionMap());
            modelBuilder.ApplyConfiguration(new IncidentTypeMap());
            modelBuilder.ApplyConfiguration(new IncidentTypeCategoryMap());
            modelBuilder.ApplyConfiguration(new NaceMap());
            modelBuilder.ApplyConfiguration(new NotificationMap());
            modelBuilder.ApplyConfiguration(new OperatingModeMap());
            modelBuilder.ApplyConfiguration(new ProcessCategory1Map());
            modelBuilder.ApplyConfiguration(new ProcessCategory2Map());
            modelBuilder.ApplyConfiguration(new ProcessCategory3Map());
            modelBuilder.ApplyConfiguration(new RootCategory1Map());
            modelBuilder.ApplyConfiguration(new RootCategory2Map());
            modelBuilder.ApplyConfiguration(new RootCategory3Map());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new CompanyTypeMap());
        }
    }
}
