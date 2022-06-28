using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dapper;

/// <summary>
/// SQLDB 的摘要描述
/// </summary>
public class SQLDB
{
    public string ConnectionString { get; set; }

    public SQLDB(string conectionString)
    {
        this.ConnectionString = conectionString;
    }

    public int Execute(object transaction, string sql, object param )
    {
        return ((SqlTransaction)transaction).Connection.Execute(sql, param, (SqlTransaction)transaction);
    }

    public int Execute(string sql, object param )
    {
        try
        {
            int result;
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                result = conn.Execute(sql, param);
            }
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public bool Exit(string sql, object param)
    //{
    //    throw new NotImplementedException();
    //}

    public IEnumerable<T> Query<T>(string sql, object param)
    {
        try
        {
            IEnumerable<T> result;
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                result = conn.Query<T>(sql, param);
            }
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable ExecuteDataTable(string sql, object param)
    {
        try
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                var dr = conn.ExecuteReader(sql, param);
                dt.Load(dr);
            }
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public static SQLDB HRApply()
    {
        ConnectionStringSettings Conn = WebConfigurationManager.ConnectionStrings["tmuConnectionString"];
        return new SQLDB(Conn.ConnectionString);
    }
}
