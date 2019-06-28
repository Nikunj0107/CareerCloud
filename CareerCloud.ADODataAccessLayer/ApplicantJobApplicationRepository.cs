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
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;

                foreach (ApplicantJobApplicationPoco item in items)
                {
                    Scmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Applicant_Job_Applications]  
                        ([Id], [Applicant],[Job], [Application_Date])
                        VALUES (@Id,@Applicant,@Job,@Application_Date)";

                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Job", item.Job);
                    Scmd.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);


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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [ID],[Applicant], [Job],[Application_Date], 
                                    [Time_Stamp] FROM
                                    [JOB_PORTAL_DB].[dbo].[Applicant_Job_Applications]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                ApplicantJobApplicationPoco[] appPocos = new ApplicantJobApplicationPoco[500];
                while (Rdr.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Applicant = Rdr.GetGuid(1);
                    poco.Job = Rdr.GetGuid(2);
                    poco.ApplicationDate = Rdr.GetDateTime(3);
                    poco.TimeStamp = (byte[])Rdr[4];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Applicant_Job_Applications] WHERE [ID] = @Id";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(Str);

            using (SqlCon)
            {

                foreach (ApplicantJobApplicationPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(@"UPDATE [dbo].[Applicant_Job_Applications] SET
                                      [Applicant]= @Applicant, 
                                      [Job] = @Job,
                                      [Application_Date] = @Application_Date
                                      where[ID] = @ID", SqlCon);

                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Job", item.Job);
                    Scmd.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
                    Scmd.Parameters.AddWithValue("@ID", item.Id);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    } 
    
}
