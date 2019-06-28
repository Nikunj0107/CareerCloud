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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {

            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                foreach (CompanyJobSkillPoco item in items)
                {
                    SCmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                   ([ID],[Job],[Skill], [Skill_Level], [Importance]) VALUES
                                   (@ID, @Job, @Skill,@Skill_Level,@Importance)";

                    SCmd.Parameters.AddWithValue("@ID", item.Id);
                    SCmd.Parameters.AddWithValue("@Job", item.Job);
                    SCmd.Parameters.AddWithValue("@Skill", item.Skill);
                    SCmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    SCmd.Parameters.AddWithValue("@Importance", item.Importance);

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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand SCmd = new SqlCommand();
                SCmd.Connection = SqlCon;
                SCmd.CommandText = @"SELECT [ID],[Job],[Skill], [Skill_Level], [Importance],
                [Time_Stamp] FROM [dbo].Company_Job_Skills";
                SqlCon.Open();
                CompanyJobSkillPoco[] SecPoco = new CompanyJobSkillPoco[6000];
                int x = 0;
                SqlDataReader rdr = SCmd.ExecuteReader();

                while (rdr.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.Skill = rdr.GetString(2);
                    poco.SkillLevel = rdr.GetString(3);
                    poco.Importance = rdr.GetInt32(4);
                    poco.TimeStamp = (byte[])rdr[5];

                    SecPoco[x] = poco;
                    x++;
                }
                SqlCon.Close();
                return SecPoco.Where(a => a != null).ToList();
            }
        }
        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);

            using (SqlCon)
            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyJobSkillPoco item in items)
                {
                    Scmd.CommandText = @"DELETE FROM [JOB_PORTAL_DB].[dbo].[Company_Job_Skills] 
                    WHERE [ID] = @ID";
                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    SqlCon.Open();
                    int rowsEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();

                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            String cnstr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(cnstr);
            using (SqlCon)

            {
                SqlCommand Scmd = new SqlCommand();
                Scmd.Connection = SqlCon;
                foreach (CompanyJobSkillPoco item in items)
                {
                    Scmd.CommandText = @"UPDATE [JOB_PORTAL_DB].[dbo].[Company_Job_Skills] SET
                                      [Job]= @Job,
                                      [Skill] = @Skill, [Skill_Level]= @Skill_Level, 
                                      [Importance] = @Importance                                                                    
                                      where [ID] = @ID";

                    Scmd.Parameters.AddWithValue("@ID", item.Id);
                    Scmd.Parameters.AddWithValue("@Job", item.Job);
                    Scmd.Parameters.AddWithValue("@Skill", item.Skill);
                    Scmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                    Scmd.Parameters.AddWithValue("@Importance", item.Importance);

                    SqlCon.Open();
                    int rowEffected = Scmd.ExecuteNonQuery();
                    SqlCon.Close();
                }
            }
        }  
    }
}
