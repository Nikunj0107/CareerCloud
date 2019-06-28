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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;

                foreach (CompanyLocationPoco item in items)
                {
                    Scmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Company_Locations]  
                        ([Id], [Company],[Country_Code],[State_Province_Code], [Street_Address],
                        [City_Town], [Zip_Postal_Code])
                        VALUES (@Id,@Company,@Country_Code,@State_Province_Code,@Street_Address,
                        @City_Town,@Zip_Postal_Code)";

                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Company", item.Company);
                    Scmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);

                    Scmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    Scmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    Scmd.Parameters.AddWithValue("@City_Town", item.City);
                    Scmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);


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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {

            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [Id], [Company],[Country_Code],[State_Province_Code], [Street_Address],
                        [City_Town], [Zip_Postal_Code],[Time_Stamp] FROM
                                [JOB_PORTAL_DB].[dbo].[Company_Locations]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                CompanyLocationPoco[] appPocos = new CompanyLocationPoco[1000];
                while (Rdr.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Company = Rdr.GetGuid(1);
                    poco.CountryCode = Rdr.GetString(2);
                    //poco.Province = Rdr.GetString(3);
                    //poco.Street = Rdr.GetString(4);
                    //poco.City = Rdr.GetString(5);
                    //poco.PostalCode = Rdr.GetString(6);
                    poco.Province = (String)(Rdr.IsDBNull(3) ? null : Rdr[3]);
                    poco.Street = (String)(Rdr.IsDBNull(4) ? null : Rdr[4]);
                    poco.City = (String)(Rdr.IsDBNull(5) ? null : Rdr[5]);
                    poco.PostalCode = (String)(Rdr.IsDBNull(6) ? null : Rdr[6]);
                    poco.TimeStamp = (byte[])Rdr[7];
             
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyLocationPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Company_Locations] WHERE [ID] = @Id";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(Str);
            using (SqlCon)
            {

                foreach (CompanyLocationPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(@"UPDATE [dbo].[Company_Locations] SET
                                      [Company]= @Company, 
                                      [Country_Code]= @Country_Code, 
                                      [State_Province_Code] = @State_Province_Code,
                                      [Street_Address]= @Street_Address, 
                                      [City_Town] = @City_Town,
                                      [Zip_Postal_Code]= @Zip_Postal_Code
                                      where[ID] = @ID", SqlCon);

                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Company", item.Company);
                    Scmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);

                    Scmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    Scmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    Scmd.Parameters.AddWithValue("@City_Town", item.City);
                    Scmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
 
}
