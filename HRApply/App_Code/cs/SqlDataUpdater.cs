using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Web.Configuration;

/// <summary>
/// 文件名稱:SqlDataUpdater.cs
/// 摘 要:SQL數據更新器,可方便對單數據表進行讀,寫,刪操作,構造函數按引用傳出結果集.
/// SqlDataUpdater du = new SqlDataUpdater(sqlHere,ref yourDataSet)
/// 調用Update(ds)更新數據,自動事務處理,出錯信息存在 ErrorMessage.
///
/// 完成日期:2015­5­01
/// 當前版本:1.0
/// </summary>
public class SqlDataUpdater
{
    private SqlConnection cn;
    private SqlCommand cmd;
    private SqlDataAdapter da;
    private SqlTransaction tran;
    private SqlCommandBuilder cmdBuilder;
    private string err;

    public string ErrorMessage
    {
        get
        {
        return err;
        }
    }

    public SqlConnection cnObject
    {
        get
        {
            return cn;
        }
    }

    public SqlCommand cmdObject
    {
        get
        {
            return cmd;
        }
    }

    public SqlConnection GetConn(int cnt)
    {
        switch (cnt)
        {
            case 1:
                cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString.ToString());
                break;
            case 2:
                cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HrConnectionString"].ConnectionString.ToString());
                break;
            case 3:
                cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HrApplyConnectionString"].ConnectionString.ToString());
                break;
        }
        return cn;

    }

    public SqlDataUpdater( SqlCommand cmd, ref DataSet dataSet, string mode)
    {
        err = "";
        if (dataSet == null) dataSet = new DataSet();

        cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString.ToString());
        da = new SqlDataAdapter(cmd);
        cmdBuilder = new SqlCommandBuilder(da);
        if ("A".Equals(mode))
        {
            //da.InsertCommand = cmdBuilder.GetInsertCommand(true);
            da.Fill(dataSet);
        }
        if ("S".Equals(mode))
        {
            cmd.Connection.Open();
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            dataSet.Tables.Add(table);
            cmd.Connection.Close();
        }
    }


    /// <summary>
    /// 初始化數據更新器
    /// </summary>
    /// <param name="sql">用於返回schema的SQL</param>
    /// <param name="dataSet">按引用傳遞的數據集</param>
    public SqlDataUpdater(string sql,ref DataSet dataSet, string mode)
    {
        err = "";
        if (dataSet == null) dataSet = new DataSet();
        cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString.ToString());
        cmd = new SqlCommand(sql,cn);
        da = new SqlDataAdapter(cmd);
        cmdBuilder = new SqlCommandBuilder(da);
        if ("A".Equals(mode))
        { 
            //da.InsertCommand = cmdBuilder.GetInsertCommand(true);
            da.Fill(dataSet);
        }
        if ("S".Equals(mode))
        {            
            cmd.Connection.Open();
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            dataSet.Tables.Add(table);
            cmd.Connection.Close();
        }
    }


    public SqlDataUpdater(string sql, ref DataSet dataSet, string mode, int cnt)
    {
        err = "";        
        if (dataSet == null) dataSet = new DataSet();
        if ( cnt == 0)
            cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["HrConnectionString"].ConnectionString.ToString());
        if (cnt == 1)
            cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AcadConnectionString"].ConnectionString.ToString());
        if (cnt == 2)
            cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["RPIConnectionString"].ConnectionString.ToString());
        if (cnt == 3)
            cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["SCIConnectionString"].ConnectionString.ToString());
        if (cnt == 4)
            cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString.ToString());
        if (cnt == 5)
            cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["AcerDBConnectionString"].ConnectionString.ToString());

        cmd = new SqlCommand(sql, cn);
        da = new SqlDataAdapter(cmd);
        cmdBuilder = new SqlCommandBuilder(da);
        if ("A".Equals(mode))
        {
            //da.InsertCommand = cmdBuilder.GetInsertCommand(true);
            da.Fill(dataSet);
        }
        if ("S".Equals(mode))
        {
            cmd.Connection.Open();
            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());
            dataSet.Tables.Add(table);
            cmd.Connection.Close();
        }
    }

    /// <summary>
    /// 初始化數據更新器
    /// </summary>
    /// <param name="sql">用於返回schema的SQL</param>
    /// <param name="dataSet">按引用傳遞的數據集</param>
    public SqlDataUpdater(string sql, ref DataSet dataSet, string mode, string tableName)
    {
        err = "";
        if (dataSet == null) dataSet = new DataSet();
        cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString.ToString());
        cmd = new SqlCommand(sql, cn);
        da = new SqlDataAdapter(cmd);
        cmdBuilder = new SqlCommandBuilder(da);
        if ("M".Equals(mode) || "D".Equals(mode))
        {
            cmd.Connection.Open();
            DataTable table = new DataTable(tableName);
            table.Load(cmd.ExecuteReader());
            dataSet.Tables.Add(table);
            cmd.Connection.Close();
            da.Fill(dataSet, tableName);
        }
    }

    public SqlDataUpdater(SqlCommand cmd, ref DataSet dataSet, string mode, string tableName)
    {
        err = "";
        if (dataSet == null) dataSet = new DataSet();

        cn = new SqlConnection(WebConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString.ToString());
        da = new SqlDataAdapter(cmd);
        cmdBuilder = new SqlCommandBuilder(da);
        if ("M".Equals(mode) || "D".Equals(mode))
        {
            cmd.Connection.Open();
            DataTable table = new DataTable(tableName);
            table.Load(cmd.ExecuteReader());
            dataSet.Tables.Add(table);
            cmd.Connection.Close();
            da.Fill(dataSet, tableName);
        }
    }

    public bool Update(DataSet ds)
    {
        bool success = false;
        cn.Open();
        tran = cn.BeginTransaction();
        cmd.Transaction = tran;
        try
        {
            if(ds.GetChanges() != null)
            {
                da.Update(ds.GetChanges());
            }
            success = true;
            tran.Commit();
        }
        catch(Exception ex)
        {
            tran.Rollback();
            err = ex.Message;
            success = false;
        }
        cn.Close();
        return success;
     }

    public bool Update(DataSet ds, String tableName)
    {
        bool success = false;
        cn.Open();
        //tran = cn.BeginTransaction();
        //cmd.Transaction = tran;
        try
        {
            da.Update(ds.Tables[tableName]);
            success = true;
        //    tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
            err = ex.Message;
            success = false;
        }
        cn.Close();
        return success;
    }


    public bool Delete(DataSet ds, String tableName)
    {
        bool success = false;
        cn.Open();
        tran = cn.BeginTransaction();
        cmd.Transaction = tran;
        try
        {
            foreach (DataRow row in ds.Tables[tableName].Rows)
            {
                row.Delete();
            }
            //da.Update(ds, "mydata");
            tran.Commit();
            success = true;
        }
        catch (Exception ex)
        {
            tran.Rollback();
            err = ex.Message;
            success = false;
        }
        cn.Close();
        return success;
    }

     ~SqlDataUpdater()
     {
        da.Dispose();
        //cmd.Dispose();
       //cn.Dispose();
     }


     protected void EmpBaseParameterSet()
     {

         da.UpdateCommand = new SqlCommand(
        "UPDATE EmployeeBase SET EmpBirthDay = @EmpBirthDay " +
         ", EmpPassportNo = @EmpPassportNo " +
         ", EmpNameENFirst = @EmpNameENFirst " +
         ", EmpNameENLast = @EmpNameENLast " +
         ", EmpNameCN = @EmpNameCN " +
         ", EmpSex = @EmpSex " +
         ", EmpCountry = @EmpCountry " +
         ", EmpHomeTown = @EmpHomeTown " +
         ", EmpBornProvince = @EmpBornProvince " +
         ", EmpBornCity = @EmpBornCity " +
         ", EmpTelPub = @EmpTelPub " +
         ", EmpTelPri = @EmpTelPri " +
         ", EmpEmail = @EmpEmail " +
         ", EmpTownAddressCode = @EmpTownAddressCode " +
         ", EmpTownAddress = @EmpTownAddress " +
         ", EmpAddressCode = @EmpAddressCode " +
         ", EmpAddress = @EmpAddress " +
         ", EmpCell = @EmpCell " +
         ", EmpExpertResearch = @EmpExpertResearch " +
         ", EmpPhotoUpload = @EmpPhotoUpload " +
         ", EmpPhotoUploadName = @EmpPhotoUploadName " +
         ", EmpIdnoUpload = @EmpIdnoUpload " +
         ", EmpIdnoUploadName = @EmpIdnoUploadFUName " +
         ", EmpDegreeUpload = @EmpDegreeUpload " +
         ", EmpDegreeUploadName = @EmpDegreeUploadName " +
         ", EmpSelfTeachExperience = @EmpSelfTeachExperience " +
         ", EmpSelfReach = @EmpSelfReach " +
         ", EmpSelfDevelope = @EmpSelfDevelope " +
         ", EmpSelfSpecial = @EmpSelfSpecia " +
         ", EmpSelfImprove = @EmpSelfImprove " +
         ", EmpSelfContribute = @EmpSelfContribute " +
         ", EmpSelfCooperate = @EmpSelfCooperate " +
         ", EmpSelfTeachPlan = @EmpSelfTeachPlan " +
         ", EmpSelfLifePlan = @EmpSelfLifePlan " +
         ", EmpStatus = @EmpStatus " +
         " WHERE EmpId = @EmpId", cn);

         da.UpdateCommand.Parameters.Add("@EmpBirthDay", SqlDbType.NVarChar, 7, "EmpBirthDay");
         da.UpdateCommand.Parameters.Add("@EmpPassportNo", SqlDbType.NVarChar, 20, "EmpPassportNo");
         da.UpdateCommand.Parameters.Add("@EmpNameENFirst", SqlDbType.NVarChar, 50, "EmpNameENFirst");
         da.UpdateCommand.Parameters.Add("@EmpNameENLast", SqlDbType.NVarChar, 50, "EmpNameENLast");
         da.UpdateCommand.Parameters.Add("@EmpNameCN", SqlDbType.NVarChar, 50, "EmpNameCN");
         da.UpdateCommand.Parameters.Add("@EmpSex", SqlDbType.Char, 1, "EmpSex");
         da.UpdateCommand.Parameters.Add("@EmpCountry", SqlDbType.NVarChar, 50, "EmpCountry");
         da.UpdateCommand.Parameters.Add("@EmpHomeTown", SqlDbType.NVarChar, 50, "EmpHomeTown");
         da.UpdateCommand.Parameters.Add("@EmpBornProvince", SqlDbType.NVarChar, 50, "EmpBornProvince");
         da.UpdateCommand.Parameters.Add("@EmpBornCity", SqlDbType.NVarChar, 50, "EmpBornCity");
         da.UpdateCommand.Parameters.Add("@EmpTelPub", SqlDbType.NVarChar, 50, "EmpTelPub");
         da.UpdateCommand.Parameters.Add("@EmpTelPri", SqlDbType.NVarChar, 50, "EmpTelPri");
         da.UpdateCommand.Parameters.Add("@EmpEmail", SqlDbType.NVarChar, 50, "EmpEmail");
         da.UpdateCommand.Parameters.Add("@EmpTownAddressCode", SqlDbType.NVarChar, 5, "EmpTownAddressCode");
         da.UpdateCommand.Parameters.Add("@EmpTownAddress", SqlDbType.NVarChar, 100, "EmpTownAddress");
         da.UpdateCommand.Parameters.Add("@EmpAddressCode", SqlDbType.NVarChar, 5, "EmpAddressCode");
         da.UpdateCommand.Parameters.Add("@EmpAddress", SqlDbType.NVarChar, 100, "EmpAddress");
         da.UpdateCommand.Parameters.Add("@EmpCell", SqlDbType.NVarChar, 50, "EmpCell");
         da.UpdateCommand.Parameters.Add("@EmpExpertResearch", SqlDbType.NVarChar, 100, "EmpExpertResearch");
         da.UpdateCommand.Parameters.Add("@EmpPhotoUpload", SqlDbType.Bit, 1, "EmpPhotoUpload");
         da.UpdateCommand.Parameters.Add("@EmpPhotoUploadName", SqlDbType.NVarChar, 200, "EmpPhotoUploadName");
         da.UpdateCommand.Parameters.Add("@EmpIdnoUpload", SqlDbType.Bit, 1, "EmpIdnoUpload");
         da.UpdateCommand.Parameters.Add("@EmpIdnoUploadName", SqlDbType.NVarChar, 200, "EmpIdnoUploadName");
         da.UpdateCommand.Parameters.Add("@EmpDegreeUpload", SqlDbType.Bit, 1, "EmpDegreeUpload");
         da.UpdateCommand.Parameters.Add("@EmpDegreeUploadName", SqlDbType.NVarChar, 200, "EmpDegreeUploadName");
         da.UpdateCommand.Parameters.Add("@EmpSelfTeachExperience", SqlDbType.NVarChar, 500, "EmpSelfTeachExperience");
         da.UpdateCommand.Parameters.Add("@EmpSelfReach", SqlDbType.NVarChar, 500, "EmpSelfReach");
         da.UpdateCommand.Parameters.Add("@EmpSelfDevelope", SqlDbType.NVarChar, 500, "EmpSelfDevelope");
         da.UpdateCommand.Parameters.Add("@EmpSelfSpecial", SqlDbType.NVarChar, 500, "EmpSelfSpecia");
         da.UpdateCommand.Parameters.Add("@EmpSelfImprove", SqlDbType.NVarChar, 500, "EmpSelfImprove");
         da.UpdateCommand.Parameters.Add("@EmpSelfContribute", SqlDbType.NVarChar, 500, "EmpSelfContribute");
         da.UpdateCommand.Parameters.Add("@EmpSelfCooperate", SqlDbType.NVarChar, 500, "EmpSelfCooperate");
         da.UpdateCommand.Parameters.Add("@EmpSelfTeachPlan", SqlDbType.NVarChar, 500, "EmpSelfTeachPlan");
         da.UpdateCommand.Parameters.Add("@EmpSelfLifePlan", SqlDbType.NVarChar, 500, "EmpSelfLifePlan");
         da.UpdateCommand.Parameters.Add("@EmpStatus", SqlDbType.Bit, 1, "EmpStatus");

         SqlParameter parameter = da.UpdateCommand.Parameters.Add("@EmpIdno", SqlDbType.NVarChar);
         parameter.SourceColumn = "EmpIdno";
         parameter.SourceVersion = DataRowVersion.Original;

     }
}