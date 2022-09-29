using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApiCrud.Models
{
    public class BALWebApi
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NLRDVF2;Initial Catalog=WebApi;Integrated Security=True");

        public SqlDataReader Login(CustomerViewModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("WebApi", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Login");
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.Parameters.AddWithValue("@Password", obj.Password);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

    }
}