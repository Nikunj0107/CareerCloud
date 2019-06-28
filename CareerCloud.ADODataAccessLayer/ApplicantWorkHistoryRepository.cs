using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;

                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    Scmd.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                        ([Id], [Applicant], [Company_Name], [Country_Code], [Location],
                        [Job_Title],[Job_Description],[Start_Month],[Start_Year],[End_Month],
                        [End_Year]) VALUES
                        (@Id,@Applicant,@Company_Name,@Country_Code,@Location,
                         @Job_Title,@Job_Description,@Start_Month,@Start_Year,@End_Month,
                        @End_Year)";

                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    Scmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    Scmd.Parameters.AddWithValue("@Location", item.Location);

                    Scmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    Scmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    Scmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);

                    Scmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    Scmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    Scmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [Id], [Applicant], [Company_Name], [Country_Code], [Location],
                        [Job_Title],[Job_Description],[Start_Month],[Start_Year],[End_Month],
                        [End_Year], [Time_Stamp] FROM
                        [JOB_PORTAL_DB].[dbo].[Applicant_Work_History]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                ApplicantWorkHistoryPoco[] appPocos = new ApplicantWorkHistoryPoco[1000];
                while (Rdr.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Applicant = Rdr.GetGuid(1);
                    poco.CompanyName = Rdr.GetString(2);
                    poco.CountryCode = Rdr.GetString(3);
                    poco.Location = Rdr.GetString(4);
                    poco.JobTitle = Rdr.GetString(5);
                    poco.JobDescription = Rdr.GetString(6);
                    poco.StartMonth = Rdr.GetInt16(7);
                    poco.StartYear = Rdr.GetInt32(8);
                    poco.EndMonth = Rdr.GetInt16(9);
                    poco.EndYear = Rdr.GetInt32(10);
                    poco.TimeStamp = (byte[])Rdr[11];
                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Applicant_Work_History] WHERE [ID] = @Id";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }

            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)
            {
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(
                                      @"UPDATE [JOB_PORTAL_DB].[dbo].[Applicant_Work_History] SET
                                      [Applicant]=@Applicant, 
                                      [Company_Name] = @Company_Name, 
                                      [Country_Code]= @Country_Code, 
                                      [Location] = @Location,
                                      [Job_Title] = @Job_Title,
                                      [Job_Description] = @Job_Description,
                                      [Start_Month]= @Start_Month, 
                                      [Start_Year] = @Start_Year,
                                      [End_Month] = @End_Month,
                                      [End_Year] = @End_Year
                                      where [Id] = @Id", SqlCon);


                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    Scmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    Scmd.Parameters.AddWithValue("@Location", item.Location);

                    Scmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    Scmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    Scmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);

                    Scmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    Scmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    Scmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
   
}
