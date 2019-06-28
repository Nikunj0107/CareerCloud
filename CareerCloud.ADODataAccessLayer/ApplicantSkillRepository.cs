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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;

                foreach (ApplicantSkillPoco item in items)
                {
                    Scmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                        ([Id], [Applicant], [Skill], [Skill_Level], [Start_Month],
                        [Start_Year],[End_Month],[End_Year]) VALUES
                        (@Id,@Applicant,@Skill,@Skill_Level,@Start_Month,
                         @Start_Year,@End_Month,@End_Year)";

                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Skill", item.Skill);
                    Scmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    Scmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    Scmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    Scmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    Scmd.Parameters.AddWithValue("@End_Year", item.EndYear);

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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {

            SqlConnection SqlCon = new SqlConnection();
            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            SqlCon.ConnectionString = cnstr;

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                Scmd.CommandText = @"SELECT [Id], [Applicant], [Skill], [Skill_Level], [Start_Month],
                        [Start_Year],[End_Month],[End_Year], [Time_Stamp] FROM
                                    [JOB_PORTAL_DB].[dbo].[Applicant_Skills]";
                SqlCon.Open();
                int x = 0;
                SqlDataReader Rdr = Scmd.ExecuteReader();
                ApplicantSkillPoco[] appPocos = new ApplicantSkillPoco[500];
                while (Rdr.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = Rdr.GetGuid(0);
                    poco.Applicant = Rdr.GetGuid(1);
                    poco.Skill = Rdr.GetString(2);
                    poco.SkillLevel = Rdr.GetString(3);
                    poco.StartMonth = (byte)Rdr[4];
                    poco.StartYear = Rdr.GetInt32(5);
                    poco.EndMonth = (byte)Rdr[6];
                    poco.EndYear = Rdr.GetInt32(7);
                    poco.TimeStamp = (byte[])Rdr[8];
                    appPocos[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            string Str = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection();
            SqlCon.ConnectionString = Str;
            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (ApplicantSkillPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [dbo].[Applicant_Skills] WHERE Id = @ID";
                    Scmd.Parameters.AddWithValue("@Id", item.Id);
                    SqlCon.Open();
                    int rows = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {

            string cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString.ToString();
            //SqlCon.ConnectionString = cnstr;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)
            {

                //Scmd.Connection = SqlCon;
                foreach (ApplicantSkillPoco item in items)
                {
                    SqlCommand Scmd = new SqlCommand(
                                      @"UPDATE [JOB_PORTAL_DB].[dbo].[Applicant_Skills] SET
                                      [Applicant]=@Applicant, 
                                      [Skill] = @Skill, 
                                      [Skill_Level]= @Skill_Level, 
                                      [Start_Month] = @Start_Month,
                                      [Start_Year] = @Start_Year,
                                      [End_Month] = @End_Month,
                                      [End_Year] = @End_Year
                                      where [Id] = @Id", SqlCon);


                    Scmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    Scmd.Parameters.AddWithValue("@Skill", item.Skill);
                    Scmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    Scmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    Scmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    Scmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    Scmd.Parameters.AddWithValue("@End_Year", item.EndYear);
                    Scmd.Parameters.AddWithValue("@Id", item.Id);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }
    }
}
