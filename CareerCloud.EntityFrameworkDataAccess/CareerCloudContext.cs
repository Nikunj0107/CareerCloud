using CareerCloud.Pocos;        
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext : DbContext
    {
        public CareerCloudContext() :
            base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString())
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantProfilePoco>()
                 .HasMany(c => c.ApplicantEducations)
                 .WithRequired(d => d.ApplicantProfiles)
                 .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantResumes)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantSkills)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(e=> e.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantWorkHistory)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
            .HasMany(c => c.ApplicantJobApplications)
            .WithRequired(d => d.ApplicantProfiles)
            .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.CompanyJobEducations)
                .WithRequired(d => d.CompanyJobs)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.CompanyJobDescriptions)
                .WithRequired(d => d.CompanyJobs)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.CompanyJobSkills)
                .WithRequired(d => d.CompanyJobs)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyJobPoco>()
            .HasMany(c => c.ApplicantJobApplications)
            .WithRequired(d => d.CompanyJobs)
            .HasForeignKey(e => e.Job);


            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(c => c.CompanyJobs)
                .WithRequired(d => d.CompanyProfiles)
                .HasForeignKey(d => d.Company);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(c => c.CompanyLocations)
                .WithRequired(d => d.CompanyProfiles)
                .HasForeignKey(d => d.Company);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(c => c.CompanyDescriptions)
                .WithRequired(d => d.CompanyProfiles)
                .HasForeignKey(d => d.Company);

            //Security login
            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(c => c.ApplicantProfiles)
                .WithRequired(d => d.SecurityLogin)
                .HasForeignKey(d => d.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(c => c.SecurityLoginsRoles)
                .WithRequired(d => d.SecurityLogins)
                .HasForeignKey(d => d.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(c => c.SecurityLoginsLogs)
                .WithRequired(d => d.SecurityLogins)
                .HasForeignKey(d => d.Login);


            //security role
            modelBuilder.Entity<SecurityRolePoco>()
               .HasMany(c => c.SecurityLoginsRoles)
               .WithRequired(d => d.SecurityRoles)
                .HasForeignKey(d => d.Login);

            //System countrycode
            modelBuilder.Entity<SystemCountryCodePoco>()
               .HasMany(c => c.ApplicantWorkHistory)
               .WithRequired(d => d.SystemCountryCodes)
                .HasForeignKey(d => d.CountryCode);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(c => c.ApplicantProfiles)
                .WithRequired(d => d.SystemCountryCodes)
                .HasForeignKey(d => d.Country);

            //system language
            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasMany(c => c.CompanyDescriptions)
                .WithRequired(d => d.SystemLanguageCodes)
                .HasForeignKey(d => d.LanguageId);

            base.OnModelCreating(modelBuilder);
        }

         DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
         DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
         DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
         DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
         DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
         DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }

         DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
         DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
         DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
         DbSet<CompanyJobPoco> CompanyJobs { get; set; }
         DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
         DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
         DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }

         DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
         DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
         DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
         DbSet<SecurityRolePoco> SecurityRoles { get; set; }

         DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
         DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


    }
}
