using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// QuestionnaireService 的摘要描述
/// </summary>
public class QuestionnaireService
{
    private SQLDB db = SQLDB.HRApply();
    public QuestionnaireService()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public List<VRefQuestionnaire> GetAll()
    {
        return db.Query<VRefQuestionnaire>("SELECT * FROM [HRApply].[dbo].[RefQuestionnaire]", null).ToList();
    }

    //public List<VThesisCoop> GetByAppSn(int appSn)
    //{
    //    return db.Query<VThesisCoop>("SELECT * FROM [HRApply].[dbo].[ThesisCoop] WHERE AppSn = @AppSn", new { AppSn = appSn }).ToList();        
    //}

    //   public DataTable GetDataTableByAppSn(int appSn)
    //   {
    //       return db.ExecuteDataTable("SELECT * FROM [HRApply].[dbo].[ThesisCoop] WHERE AppSn = @AppSn", new { AppSn = appSn });
    //   }

    //   public List<VThesisCoop> GetByAppSn(int appSn)
    //   {
    //       return db.Query<VThesisCoop>("SELECT * FROM [HRApply].[dbo].[ThesisCoop] WHERE AppSn = @AppSn", new { AppSn = appSn }).ToList();
    //   }
    public List<VApplyQuestionnaire> Get(int appSn)
    {
        return db.Query<VApplyQuestionnaire>("SELECT * FROM [HRApply].[dbo].[ApplyQuestionnaire] WHERE AppSn = @AppSn", new { AppSn = appSn}).ToList();
    }

    public int Create(VApplyQuestionnaire model)
    {
        return db.Execute(@"INSERT INTO [dbo].[ApplyQuestionnaire]
              ([AppSn],[QuestionnaireID],[ItemContent])
        VALUES
              (@AppSn,@QuestionnaireID,@ItemContent)", model);
    }

    public int Update(VApplyQuestionnaire model)
    {
        return db.Execute(@"UPDATE [dbo].[ApplyQuestionnaire]
   SET [AppSn] = @AppSn
      ,[QuestionnaireID] = @QuestionnaireID
      ,[ItemContent] = @ItemContent
 WHERE [ID] = @ID", model);
    }

    public int Delete(int appSn)
    {
        return db.Execute(@"DELETE FROM [dbo].[ApplyQuestionnaire] WHERE AppSn = @AppSn", new { AppSn = appSn });
    }

}