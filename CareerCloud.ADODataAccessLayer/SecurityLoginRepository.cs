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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (SecurityLoginPoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                ([ID], [Login], [Password],[Created_Date],[Password_Update_Date],
                                [Agreement_Accepted_Date],[Is_Locked],[Is_Inactive], [Email_Address],
                                [Phone_Number], [Full_Name],[Force_Change_Password], [Prefferred_Language]) VALUES
                             (@Id, @Login,@Password, @Created_Date,@Password_Update_Date,@Agreement_Accepted_Date,
                    @Is_Locked, @Is_Inactive,@Email_Address, @Phone_Number,@Full_Name, @Force_Change_Password,
                    @Prefferred_Language)";


                    SCmd.Parameters.AddWithValue("@Id", item.Id);
                    SCmd.Parameters.AddWithValue("@Login", item.Login);
                    SCmd.Parameters.AddWithValue("@Password", item.Password);

                    SCmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    SCmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    SCmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    SCmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);

                    SCmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    SCmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    SCmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);

                    SCmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    SCmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    SCmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [ID], [Login], [Password],[Created_Date],[Password_Update_Date],
                                [Agreement_Accepted_Date],[Is_Locked],[Is_Inactive], [Email_Address],
                                [Phone_Number], [Full_Name],[Force_Change_Password], [Prefferred_Language], 
                               [Time_Stamp] from [JOB_PORTAL_DB].[dbo].[Security_Logins]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                SecurityLoginPoco[] appPocos = new SecurityLoginPoco[2000];
                while (Rdr.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Login = Rdr.GetString(1);
                    poco.Password = Rdr.GetString(2);
                    poco.Created = Rdr.GetDateTime(3);
                    poco.PasswordUpdate = (DateTime?)(Rdr.IsDBNull(4) ? null : Rdr[4]);
                    poco.AgreementAccepted = (DateTime?)(Rdr.IsDBNull(5) ? null : Rdr[5]);
                    poco.IsLocked = Rdr.GetBoolean(6);
                    poco.IsInactive = Rdr.GetBoolean(7);
                    poco.EmailAddress = Rdr.GetString(8);
                    poco.PhoneNumber = (String)(Rdr.IsDBNull(9) ? null : Rdr[9]);
                    poco.FullName = (String)(Rdr.IsDBNull(10) ? null : Rdr[10]);
                    poco.ForceChangePassword = Rdr.GetBoolean(11);
                    poco.PrefferredLanguage = (String)(Rdr.IsDBNull(12) ? null : Rdr[12]);
                    poco.TimeStamp = poco.TimeStamp = (byte[])Rdr[13];
                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (SecurityLoginPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Security_Logins] 
                    WHERE [ID] = @ID";
                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)

            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (SecurityLoginPoco item in items)
                {
                    SCmd.CommandText = @"UPDATE [JOB_PORTAL_DB].[dbo].[Security_Logins] SET
                                      [Login]=@Login,
                                      [Password] = @Password,[Created_Date]= @Created_Date,
                                       [Password_Update_Date] = @Password_Update_Date,
                                [Agreement_Accepted_Date]= @Agreement_Accepted_Date,
                                [Is_Locked] = @Is_Locked,[Is_Inactive] = @Is_Inactive,
                           [Email_Address]= @Email_Address,[Phone_Number]= @Phone_Number,
                        [Full_Name] = @Full_Name,[Force_Change_Password]= @Force_Change_Password,
                        [Prefferred_Language]= @Prefferred_Language
                        where[ID] = @ID";

                    SCmd.Parameters.AddWithValue("@Login", item.Login);
                    SCmd.Parameters.AddWithValue("@Password", item.Password);

                    SCmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    SCmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    SCmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    SCmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);

                    SCmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    SCmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    SCmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);

                    SCmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    SCmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    SCmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    SCmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rowEffected = SCmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
 }
