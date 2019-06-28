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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (SecurityLoginsRolePoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Security_Logins_Roles]
                                       ([ID], [Login],[Role]) VALUES
                                        (@ID, @Login, @Role)";
                    SCmd.Parameters.AddWithValue("@ID", item.Id);
                    SCmd.Parameters.AddWithValue("@Login", item.Login);
                    SCmd.Parameters.AddWithValue("@Role", item.Role);
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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                SCmd.CommandText = @"SELECT [ID],[Login],[Role],[Time_Stamp] 
                FROM [dbo].Security_Logins_Roles";
                SqlCon.Open();
                SecurityLoginsRolePoco[] SecPoco = new SecurityLoginsRolePoco[1000];
                int x = 0;
                SqlDataReader rdr = SCmd.ExecuteReader();

                while(rdr.Read())
                {
                    SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.Role = rdr.GetGuid(2);
                    poco.TimeStamp = (byte[])(rdr.IsDBNull(3) ? null : rdr[3]);

                    SecPoco[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return SecPoco.Where(a => a != null).ToList();

            }

        }
        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }


        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            string Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SecurityLoginsRolePoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Roles] WHERE Id = @ID";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rows = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                foreach (SecurityLoginsRolePoco item in items)
                {
                    Scmd.CommandText = @"UPDATE[JOB_PORTAL_DB].[dbo].[Security_Logins_Roles] SET
                                     [ID] = @ID,
                                      [Login]=@Login, 
                                      [Role] = @Role where[ID] = @ID";

                    Scmd.Parameters.AddWithValue("@ID",item.Id);
                    Scmd.Parameters.AddWithValue("@Login",item.Login);
                    Scmd.Parameters.AddWithValue("@Role", item.Role);
                                       
                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
}
