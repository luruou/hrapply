using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// VThesisCoop 的摘要描述
/// </summary>
public class VThesisCoop
{
    public int ID { get; set; }
    public int AppSn { get; set; }
    public int EmpSn { get; set; }
    public string ProjectContent { get; set; }
    public int Classification { get; set; }
    public int RD { get; set; }
    public int Contribute { get; set; }
    public int Total { get; set; }
    public string UploadFileName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}