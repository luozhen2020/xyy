using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class fileManage_fileList : System.Web.UI.Page
{
    BaseClass bc = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.DataSource= bc.GetDataSet("select * from [file]", "file");
        GridView1.DataKeyNames=new string[] {"fileID"};
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //清除相片文件（服务器）
        DataSet ds = bc.GetDataSet("select * from [file] where fileID='" + this.GridView1.DataKeys[e.RowIndex].Value.ToString() + "'", "file");
        DataRow[] row = ds.Tables[0].Select();
        foreach (DataRow rs in row)  //将检索到的数据逐一,循环添加到Listbox1中
        {
            FileInfo file = new FileInfo(Server.MapPath(rs["Path"].ToString()));
            file.Delete();
        }
        //清除数据
        bc.ExecSQL("delete  from [file] where fileID='" + this.GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
        GridView1.DataSource = bc.GetDataSet("select * from [file]", "[file]");
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }
}
