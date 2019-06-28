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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Company_Jobs_Descriptions]
                                       ([ID], [Job],[Job_Name],[Job_Descriptions])
                                        VALUES
                                        (@ID, @Job,@Job_Name, @Job_Descriptions)";

                    SCmd.Parameters.AddWithValue("@ID", item.Id);
                    SCmd.Parameters.AddWithValue("@Job", item.Job);
                    SCmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    SCmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
                    SqlCon.Open();
                    int rowEffected = SCmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                SCmd.CommandText = @"SELECT [ID], [Job],[Job_Name],[Job_Descriptions],[Time_Stamp] 
                FROM [dbo].Company_Jobs_Descriptions";
                SqlCon.Open();
                CompanyJobDescriptionPoco[] SecPoco = new CompanyJobDescriptionPoco[2000];
                int x = 0;
                SqlDataReader rdr = SCmd.ExecuteReader();

                while (rdr.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.JobName = (String)(rdr.IsDBNull(2) ? null : rdr[2]);
                    poco.JobDescriptions = (String)(rdr.IsDBNull(3) ? null : rdr[3]);
                    poco.TimeStamp = (byte[])(rdr.IsDBNull(4) ? null : rdr[4]);

                    SecPoco[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return SecPoco.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            string Str = System.Configuration.ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions] WHERE Id = @ID";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rows = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(Str);

            using (SqlCon)
            {
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(@"UPDATE [dbo].[Company_Jobs_Descriptions] SET
                                      [Job]= @Job, 
                                      [Job_Name] = @Job_Name,
                                      [Job_Descriptions]= @Job_Descriptions
                                      where[ID] = @ID", SqlCon);

                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    Scmd.Parameters.AddWithValue("@Job", item.Job);
                    Scmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    Scmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
}
