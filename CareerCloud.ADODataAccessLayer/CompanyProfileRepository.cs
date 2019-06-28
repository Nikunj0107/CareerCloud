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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyProfilePoco item in items)
                {
                    Scmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Company_Profiles]
                                       ( [ID],[Registration_Date],[Company_Website],[Contact_Phone],
                                        [Contact_Name],[Company_Logo])
                                        VALUES
                                        (@ID, @Registration_Date,@Company_Website, @Contact_Phone
                                        ,@Contact_Name
                                       , @Company_Logo)";

                    Scmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    Scmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    Scmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    Scmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    Scmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    Scmd.Parameters.AddWithValue("@ID", item.Id);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                SCmd.CommandText = @"SELECT [ID],[Registration_Date],[Company_Website],[Contact_Phone],
                [Contact_Name],[Company_Logo],[Time_Stamp] 
                FROM [dbo].Company_Profiles";
                SqlCon.Open();
                CompanyProfilePoco[] SecPoco = new CompanyProfilePoco[2000];
                int x = 0;
                SqlDataReader rdr = SCmd.ExecuteReader();

                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    poco.CompanyWebsite = (String)(rdr.IsDBNull(2) ? null : rdr[2]);
                    poco.ContactPhone = rdr.GetString(3);
                    poco.ContactName = (String)(rdr.IsDBNull(4) ? null : rdr[4]);
                    poco.CompanyLogo = (byte[])(rdr.IsDBNull(5) ? null : rdr[5]);
                    poco.TimeStamp = (byte[])rdr[6];

                    SecPoco[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return SecPoco.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

            public void Remove(params CompanyProfilePoco[] items)
        {
            string Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyProfilePoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles] WHERE Id = @ID";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rows = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(Str);

            using (SqlCon)
            {
                foreach (CompanyProfilePoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(@"UPDATE [dbo].[Company_Profiles] SET
                                      [Registration_Date]= @Registration_Date, 
                                      [Company_Website] = @Company_Website,
                                      [Contact_Phone]= @Contact_Phone, 
                                      [Contact_Name] = @Contact_Name,
                                      [Company_Logo]= @Company_Logo
                                       where[ID] = @ID", SqlCon);

                    Scmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    Scmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    Scmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    Scmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    Scmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);
                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
    
}
