using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    internal static class Util
    {
        internal static DataTable Query(string sql, SqlParameter[] p = null)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand(sql, cn);
            if (!sql.StartsWith("SELECT"))
                cmd.CommandType = CommandType.StoredProcedure;
            if (p != null)
                cmd.Parameters.AddRange(p);
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds);
            cn.Close();
            return ds.Tables[0];
        }

        internal static void Execute(string sql, SqlParameter[] p = null)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            var cn = new SqlConnection(connectionString);
            cn.Open();
            var cmd = new SqlCommand(sql, cn);
            if (!sql.StartsWith("SELECT"))
                cmd.CommandType = CommandType.StoredProcedure;
            if (p != null)
                cmd.Parameters.AddRange(p);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
