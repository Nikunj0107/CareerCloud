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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (CompanyJobPoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Company_Jobs]
                                       ([ID], [Company],[Profile_Created],[Is_Inactive], [Is_Company_Hidden])
                                        VALUES
                                        (@ID, @Company,@Profile_Created, @Is_Inactive,@Is_Company_Hidden)";

                    SCmd.Parameters.AddWithValue("@ID", item.Id);
                    SCmd.Parameters.AddWithValue("@Company", item.Company);
                    SCmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    SCmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    SCmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                SCmd.CommandText = @"SELECT [ID],[Company],[Profile_Created],[Is_Inactive],
                [Is_Company_Hidden],[Time_Stamp] 
                FROM [dbo].Company_Jobs";
                SqlCon.Open();
                CompanyJobPoco[] SecPoco = new CompanyJobPoco[2000];
                int x = 0;
                SqlDataReader rdr = SCmd.ExecuteReader();

                while (rdr.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.ProfileCreated = rdr.GetDateTime(2);
                    poco.IsInactive = rdr.GetBoolean(3);
                    poco.IsCompanyHidden = rdr.GetBoolean(4);
                    poco.TimeStamp = (byte[])(rdr.IsDBNull(5) ? null : rdr[5]);
                    //(String)
                    SecPoco[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return SecPoco.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            string Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyJobPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs] WHERE Id = @ID";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rows = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(Str);

            using (SqlCon)
            {
                foreach (CompanyJobPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(@"UPDATE [dbo].[Company_Jobs] SET
                                      [Company]= @Company, 
                                      [Profile_Created] = @Profile_Created,[Is_Inactive]= @Is_Inactive, 
                                      [Is_Company_Hidden] = @Is_Company_Hidden
                                      where[ID] = @ID" , SqlCon);
                                     
                    Scmd.Parameters.AddWithValue("@Company", item.Company);
                    Scmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    Scmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    Scmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
                    Scmd.Parameters.AddWithValue("@ID", item.Id);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
    
}
