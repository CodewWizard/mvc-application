using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class UserBuisnessLayer
    {
        public IEnumerable<User1> Users
        {
            get
            {
                string connectionString =
                    ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;

                List<User1> employees = new List<User1>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        User1 user = new User1();
                        user.Id = Convert.ToInt32(rdr["Id"]);
                        user.Name =rdr["Name"].ToString();
                        user.UserName = rdr["UserName"].ToString();
                        user.Password = rdr["Password"].ToString();
                        user.Gender = rdr["Gender"].ToString();
                        user.UserType = rdr["UserType"].ToString();
                        user.DOB = Convert.ToDateTime(rdr["DOB"]);
                        employees.Add(user);
                    }
                }

                return employees;
            }
        }

        public void AddUser(User1 user)
        {
            string connectionString =
            ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = user.Id;
                cmd.Parameters.Add(paramId);

                SqlParameter paramName = new SqlParameter();
                paramName.ParameterName = "@Name";
                paramName.Value = user.Name;
                cmd.Parameters.Add(paramName);

                SqlParameter paramDOB = new SqlParameter();
                paramDOB.ParameterName = "@DOB";
                paramDOB.Value = user.DOB;
                cmd.Parameters.Add(paramDOB);

                SqlParameter paramUserName = new SqlParameter();
                paramUserName.ParameterName = "@UserName";
                paramUserName.Value = user.UserName;
                cmd.Parameters.Add(paramUserName);


                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = user.Password;
                cmd.Parameters.Add(paramPassword);

                SqlParameter paramGender = new SqlParameter();
                paramGender.ParameterName = "@Gender";
                paramGender.Value = user.Gender;
                cmd.Parameters.Add(paramGender);

                SqlParameter paramUserType = new SqlParameter();
                paramUserType.ParameterName = "@UserType";
                paramUserType.Value = user.UserType;
                cmd.Parameters.Add(paramUserType);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
