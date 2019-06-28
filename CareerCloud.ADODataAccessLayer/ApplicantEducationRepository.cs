using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using ( SqlCon )
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                 
                foreach (ApplicantEducationPoco item in items)
                {
                    Scmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                        ([Id], [Applicant], [Major], [Certificate_Diploma], [Start_Date],
                        [Completion_Date],[Completion_Percent]) VALUES
                        (@Id,@Applicant,@Major,@Certificate_Diploma,@Start_Date,
                         @Completion_Date,@Completion_Percent)";
         
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Major", item.Major);
                    Scmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    Scmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    Scmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    Scmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [ID],[Applicant], [Major],[Certificate_Diploma],[Start_Date], 
                                    [Completion_Date],[Completion_Percent], [Time_Stamp] FROM
                                    [JOB_PORTAL_DB].[dbo].[Applicant_Educations]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                ApplicantEducationPoco[] appPocos = new ApplicantEducationPoco[500];
                while (Rdr.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Applicant = Rdr.GetGuid(1);
                    poco.Major = Rdr.GetString(2);
                    poco.CertificateDiploma = (String)(Rdr.IsDBNull(3) ? null : Rdr[3]);
                    poco.StartDate = (DateTime)(Rdr.IsDBNull(4) ? null : Rdr[4]);
                    poco.CompletionDate = (DateTime)(Rdr.IsDBNull(5) ? null : Rdr[5]);
                    poco.CompletionPercent = (byte?)Rdr[6];
                    poco.TimeStamp = (byte[])Rdr[7];
                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }
        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (ApplicantEducationPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Applicant_Educations] WHERE [ID] = @Id";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                    
                }   

            }  
                       
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            //SqlCon.ConnectionString = cnstr;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)
           {

                //Scmd.Connection = SqlCon;
                foreach (ApplicantEducationPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(
                                     @"UPDATE [dbo].[Applicant_Educations] SET  
                                      [Applicant] = @Applicant,[Major] = @Major,
                                      [Certificate_Diploma] = @Certificate_Diploma,
                                      [Start_Date] = @Start_Date,
                                      [Completion_Date] = @Completion_Date,
                                      [Completion_Percent] = @Completion_Percent
                                      where[ID] = @ID", SqlCon);


                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Major", item.Major);
                    Scmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                    Scmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                    Scmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                    Scmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
}
