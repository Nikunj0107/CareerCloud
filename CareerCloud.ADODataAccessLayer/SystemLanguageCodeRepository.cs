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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (SystemLanguageCodePoco item in items )
                {
                    SCmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                       ([LanguageID], [Name], [Native_Name]) VALUES
                                        (@LanguageID, @Name, @Native_Name)";
                    SCmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    SCmd.Parameters.AddWithValue("@Name", item.Name);
                    SCmd.Parameters.AddWithValue("@Native_Name", item.NativeName);
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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [LanguageID],[Name], [Native_Name] FROM
                                    [JOB_PORTAL_DB].[dbo].[System_Language_Codes]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                SystemLanguageCodePoco[] appPocos = new SystemLanguageCodePoco[500];
                while (Rdr.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = Rdr.GetString(0);
                    poco.Name = Rdr.GetString(1);
                    poco.NativeName = Rdr.GetString(2);
                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SystemLanguageCodePoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[System_Language_Codes] 
                    WHERE [LanguageID] = @LanguageID";
                    Scmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)

            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SystemLanguageCodePoco item in items)
                {
                    Scmd.CommandText = @"UPDATE [JOB_PORTAL_DB].[dbo].[System_Language_Codes] SET
                                      [LanguageID]=@LanguageID,
                                      [Name]=@Name, 
                                      [Native_Name] = @Native_Name                                                                      
                                      where [LanguageID] = @LanguageID";


                    Scmd.Parameters.AddWithValue("@Name", item.Name);
                    Scmd.Parameters.AddWithValue("@Native_Name", item.NativeName);
                    Scmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }

        }
    }
}
