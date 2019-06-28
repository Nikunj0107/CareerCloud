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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (CompanyDescriptionPoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                   ([ID],[Company],[LanguageID], [Company_Name], [Company_Description]) VALUES
                                   (@ID, @Company, @LanguageID,@Company_Name,@Company_Description)";

                    SCmd.Parameters.AddWithValue("@ID", item.Id);
                    SCmd.Parameters.AddWithValue("@Company", item.Company);
                    SCmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    SCmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    SCmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

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

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [ID],[Company], [LanguageID], [Company_Name], [Company_Description],
                                  [Time_Stamp] FROM [JOB_PORTAL_DB].[dbo].[Company_Descriptions]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                CompanyDescriptionPoco[] appPocos = new CompanyDescriptionPoco[1000];
                while (Rdr.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Company = Rdr.GetGuid(1);
                    poco.LanguageId = Rdr.GetString(2);
                    poco.CompanyName = Rdr.GetString(3);
                    poco.CompanyDescription = Rdr.GetString(4);
                    poco.TimeStamp = (byte[])Rdr[5];

                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }
        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyDescriptionPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Company_Descriptions] 
                    WHERE [ID] = @ID";
                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)

            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyDescriptionPoco item in items)
                {
                    Scmd.CommandText = @"UPDATE [JOB_PORTAL_DB].[dbo].[Company_Descriptions] SET
                                      [Company]= @Company,
                                      [LanguageID] = @LanguageID, [Company_Name]= @Company_Name, 
                                      [Company_Description] = @Company_Description                                                                    
                                      where [ID] = @ID";


                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    Scmd.Parameters.AddWithValue("@Company", item.Company);
                    Scmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    Scmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    Scmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }

        }
    }
    
}
