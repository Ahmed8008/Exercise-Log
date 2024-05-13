using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ExcerciseLog.Models
{
    public class Database
    {
        public static string constr = @"Data Source=DESKTOP-68SGKHV\SQLEXPRESS;Initial Catalog=ExerciseLog;Integrated Security=true";
        //public static string constr = @"workstation id=ExerciseLog.mssql.somee.com;packet size=4096;user id=ahmed69_SQLLogin_1;pwd=wlq66phuan;data source=ExerciseLog.mssql.somee.com;persist security info=False;initial catalog=ExerciseLog";
        public SqlConnection con = new SqlConnection(constr);
    }
}