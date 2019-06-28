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
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (SecurityRolePoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                                       ([ID], [Role], [Is_Inactive]) VALUES
                                        (@ID, @Role, @Is_Inactive)";
                    SCmd.Parameters.AddWithValue("@ID", item.Id);
                    SCmd.Parameters.AddWithValue("@Role", item.Role);
                    SCmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [ID],[Role], [Is_Inactive] FROM
                                    [JOB_PORTAL_DB].[dbo].[Security_Roles]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                SecurityRolePoco[] appPocos = new SecurityRolePoco[1000];
                while (Rdr.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Role = Rdr.GetString(1);
                    poco.IsInactive = Rdr.GetBoolean(2);
                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
                
            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SecurityRolePoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Security_Roles] 
                    WHERE[ID] = @ID";
                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)

            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SecurityRolePoco item in items)
                {
                    Scmd.CommandText = @"UPDATE [JOB_PORTAL_DB].[dbo].[Security_Roles] SET
                                      [ID]=@ID,
                                      [Role]=@Role, 
                                      [Is_Inactive] = @Is_Inactive                                                                      
                                      where [ID] = @ID";


                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    Scmd.Parameters.AddWithValue("@Role", item.Role);
                    Scmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }

        }
    }
}
