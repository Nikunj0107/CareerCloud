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
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (SystemCountryCodePoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                       ([Code], [Name]) VALUES
                                        (@Code, @Name)";
                    SCmd.Parameters.AddWithValue("@Code", item.Code);
                    SCmd.Parameters.AddWithValue("@Name", item.Name);
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

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [Code],[Name] FROM
                                    [JOB_PORTAL_DB].[dbo].[System_Country_Codes]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                SystemCountryCodePoco[] appPocos = new SystemCountryCodePoco[500];
                while (Rdr.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = Rdr.GetString(0);
                    poco.Name = Rdr.GetString(1);
                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SystemCountryCodePoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[System_Country_Codes] 
                    WHERE [Code] = @Code";
                    Scmd.Parameters.AddWithValue("@Code", item.Code);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)

            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SystemCountryCodePoco item in items)
                {
                    Scmd.CommandText = @"UPDATE [JOB_PORTAL_DB].[dbo].[System_Country_Codes] SET
                                      [Code]=@Code,
                                      [Name]=@Name                                                               
                                      where [Code] = @Code";


                    Scmd.Parameters.AddWithValue("@Name", item.Name);
                    Scmd.Parameters.AddWithValue("@Code", item.Code);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
}
