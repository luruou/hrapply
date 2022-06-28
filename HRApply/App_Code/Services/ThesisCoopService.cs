using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// ThesisCoopService 的摘要描述
/// </summary>
public class ThesisCoopService
{
    private SQLDB db = SQLDB.HRApply();
    public ThesisCoopService()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public VThesisCoop Get(int id)
    {
        return db.Query<VThesisCoop>("SELECT * FROM [HRApply].[dbo].[ThesisCoop] WHERE ID = @ID", new { ID = id }).FirstOrDefault();
    }

    //public List<VThesisCoop> GetByAppSn(int appSn)
    //{
    //    return db.Query<VThesisCoop>("SELECT * FROM [HRApply].[dbo].[ThesisCoop] WHERE AppSn = @AppSn", new { AppSn = appSn }).ToList();        
    //}

    public DataTable GetDataTableByAppSn(int appSn)
    {
        return db.ExecuteDataTable("SELECT * FROM [HRApply].[dbo].[ThesisCoop] WHERE AppSn = @AppSn", new { AppSn = appSn });
    }

    public List<VThesisCoop> GetByAppSn(int appSn)
    {
        return db.Query<VThesisCoop>("SELECT * FROM [HRApply].[dbo].[ThesisCoop] WHERE AppSn = @AppSn", new { AppSn = appSn }).ToList();
    }


    public int Create(VThesisCoop model)
    {
        model.CreatedDate = DateTime.Now;  
        model.UpdatedDate = DateTime.Now;  
        return db.Execute(@"INSERT INTO [dbo].[ThesisCoop]
           ([AppSn],[EmpSn],[ProjectContent],[Classification],[RD],[Contribute],[Total],[UploadFileName],[CreatedDate],[UpdatedDate])
     VALUES
           (@AppSn,@EmpSn,@ProjectContent,@Classification,@RD,@Contribute,@Total,@UploadFileName,@CreatedDate,@UpdatedDate)", model);
    }

    public int Update(VThesisCoop model)
    {
        model.UpdatedDate = DateTime.Now;
        return db.Execute(@"UPDATE [dbo].[ThesisCoop]
   SET [AppSn] = @AppSn
      ,[EmpSn] = @EmpSn
      ,[ProjectContent] = @ProjectContent
      ,[Classification] = @Classification
      ,[RD] = @RD
      ,[Contribute] = @Contribute
      ,[Total] = @Total
      ,[UploadFileName] = @UploadFileName
      ,[UpdatedDate] = @UpdatedDate
 WHERE [ID] = @ID", model);
    }

    public int Delete(int id)
    {
        return db.Execute(@"DELETE FROM [HRApply].[dbo].[ThesisCoop] WHERE ID = @ID", new { ID = id });
    }

}