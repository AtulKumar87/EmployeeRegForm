using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace EmployeeRegForm1.Models
{
    public class ClsClass
    {
        SqlConnection con;
        public SqlConnection ClsConn()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            return con;
        }
    }
}