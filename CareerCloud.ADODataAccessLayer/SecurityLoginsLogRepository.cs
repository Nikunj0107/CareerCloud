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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Security_Logins_Log]
                                       ([ID], [Login],[Source_IP],[Logon_Date],[Is_Succesful]) VALUES
                                        (@ID, @Login, @Source_IP,@Logon_Date,@Is_Succesful)";

                    SCmd.Parameters.AddWithValue("@ID", item.Id);
                    SCmd.Parameters.AddWithValue("@Login", item.Login);
                    SCmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    SCmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    SCmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                SCmd.CommandText = @"SELECT [ID],[Login],[Source_IP],[Logon_Date], [Is_Succesful]
                FROM [dbo].Security_Logins_Log";
                SqlCon.Open();
                SecurityLoginsLogPoco[] SecPoco = new SecurityLoginsLogPoco[2000];
                int x = 0;
                SqlDataReader rdr = SCmd.ExecuteReader();

                while (rdr.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.SourceIP = rdr.GetString(2);
                    poco.LogonDate = rdr.GetDateTime(3);
                    poco.IsSuccesful = rdr.GetBoolean(4);

                    SecPoco[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return SecPoco.Where(a => a != null).ToList();

            }
        }

            public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            string Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Log] WHERE Id = @ID";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rows = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;

            using (SqlCon)
            {
                SqlCon.Open();
                foreach (SecurityLoginsLogPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(@"UPDATE[JOB_PORTAL_DB].[dbo].[Security_Logins_Log] SET
                                     [ID] = @ID,[Login]=@Login,
                                     [Source_IP] = @Source_IP,[Logon_Date] = @Logon_Date, 
                                     [Is_Succesful] = @Is_Succesful
                                     where[ID] = @ID", SqlCon);

                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    Scmd.Parameters.AddWithValue("@Login", item.Login);
                    Scmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    Scmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    Scmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                    int rowEffected = Scmd.ExecuteNonQuery();
                    
                }
                SqlCon.Close();
            }
        }
    }
}
