using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;

                foreach (ApplicantProfilePoco item in items)
                {
                    Scmd.CommandText = @"INSERT INTO [JOB_PORTAL_DB].[dbo].[Applicant_Profiles]  
                        ([Id], [Login],[Current_Salary], [Current_Rate],
                        [Currency], [Country_Code],[State_Province_Code], [Street_Address],
                        [City_Town], [Zip_Postal_Code])
                        VALUES (@Id,@Login,@Current_Salary,@Current_Rate,
                        @Currency,@Country_Code,@State_Province_Code,@Street_Address,
                        @City_Town,@Zip_Postal_Code)";

                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Login", item.Login);
                    Scmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    Scmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    Scmd.Parameters.AddWithValue("@Currency", item.Currency);
                    Scmd.Parameters.AddWithValue("@Country_Code", item.Country);
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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [Id], [Login],[Current_Salary], [Current_Rate],
                                [Currency], [Country_Code],[State_Province_Code], [Street_Address],
                                [City_Town], [Zip_Postal_Code],[Time_Stamp] FROM
                                [JOB_PORTAL_DB].[dbo].[Applicant_Profiles]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                ApplicantProfilePoco[] appPocos = new ApplicantProfilePoco[1000];
                while (Rdr.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Login = Rdr.GetGuid(1);
                    poco.CurrentSalary = Rdr.GetDecimal(2);
                    poco.CurrentRate = Rdr.GetDecimal(3);
                    poco.Currency = (String)(Rdr.IsDBNull(4) ? null : Rdr[4]);
                    poco.Country = (String)(Rdr.IsDBNull(5) ? null : Rdr[5]);
                    poco.Province = (String)(Rdr.IsDBNull(6) ? null : Rdr[6]);
                    poco.Street = (String)(Rdr.IsDBNull(7) ? null : Rdr[7]);
                    poco.City = (String)(Rdr.IsDBNull(8) ? null : Rdr[8]);
                    poco.PostalCode = (String)(Rdr.IsDBNull(9) ? null : Rdr[9]);
                    poco.TimeStamp = (byte[])Rdr[10];
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (ApplicantProfilePoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Applicant_Profiles] WHERE [ID] = @Id";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            String Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(Str);
            using (SqlCon)
            {

                foreach (ApplicantProfilePoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(@"UPDATE [dbo].[Applicant_Profiles] SET
                                      [Login]= @Login, 
                                      [Current_Salary] = @Current_Salary,[Current_Rate]= @Current_Rate, 
                                      [Currency] = @Currency,
                                      [Country_Code]= @Country_Code, 
                                      [State_Province_Code] = @State_Province_Code,
                                      [Street_Address]= @Street_Address, 
                                      [City_Town] = @City_Town,
                                      [Zip_Postal_Code]= @Zip_Postal_Code
                                      where[ID] = @ID", SqlCon);

                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Login", item.Login);
                    Scmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    Scmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    Scmd.Parameters.AddWithValue("@Currency", item.Currency);
                    Scmd.Parameters.AddWithValue("@Country_Code", item.Country);
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
