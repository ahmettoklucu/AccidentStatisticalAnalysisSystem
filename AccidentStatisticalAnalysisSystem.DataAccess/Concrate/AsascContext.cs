
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.EntityFreamwork.Mapping;
using AccidentStatisticalAnalysisSystem.DataAccess.Concrate.Mapping;
using AccidentStatisticalAnalysisSystem.Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccidentStatisticalAnalysisSystem.DataAccess.Concrate
{
    public class AsascContext : DbContext
    {
        public AsascContext() 
        {
            Database.SetInitializer<AsascContext>(null);
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new DangerousMaterialIncidentsMap());
            modelBuilder.Configurations.Add(new EnvironmentalDamageIncidentsMap());
            modelBuilder.Configurations.Add(new FinanceIncidentMap());
            modelBuilder.Configurations.Add(new FormOfTheChemicalIncidentMap());
            modelBuilder.Configurations.Add(new IgnitionIncidentMap());
            modelBuilder.Configurations.Add(new IncidentMap());
            modelBuilder.Configurations.Add(new IncidentTypeIncidentMap());
            modelBuilder.Configurations.Add(new ProcesIncidentMap());
            modelBuilder.Configurations.Add(new ProcesMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RootIncidentMap());
            modelBuilder.Configurations.Add(new RootMap());
            modelBuilder.Configurations.Add(new SuggestionMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new DangerousMaterialMap());
            modelBuilder.Configurations.Add(new EmployerTypeMap());
            modelBuilder.Configurations.Add(new EnvironmentalDamageCategoryMap());
            modelBuilder.Configurations.Add(new EnvironmentalDamageMap());
            modelBuilder.Configurations.Add(new FinanceMap());
            modelBuilder.Configurations.Add(new FormOfChemicalMap());
            modelBuilder.Configurations.Add(new IgnıtıonMap());
            modelBuilder.Configurations.Add(new IncidentTypeMap());
            modelBuilder.Configurations.Add(new IncidentTypeCategoryMap());
            modelBuilder.Configurations.Add(new NaceMap());
            modelBuilder.Configurations.Add(new NotificationMap());
            modelBuilder.Configurations.Add(new OperatingModeMap());
            modelBuilder.Configurations.Add(new ProcessCategory1Map());
            modelBuilder.Configurations.Add(new ProcessCategory2Map());
            modelBuilder.Configurations.Add(new ProcessCategory3Map());
            modelBuilder.Configurations.Add(new RootCategory1Map());
            modelBuilder.Configurations.Add(new RootCategory2Map());
            modelBuilder.Configurations.Add(new RootCategory3Map());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyTypeMap());
        }
    }
}
