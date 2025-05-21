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

public partial class tprfpath : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // 获取数据项
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            // 转换 status 字段（假设状态列是第10列，从0开始）
            int status = Convert.ToInt32(rowView["status"]);
            string statusText = GetStatusText(status);
            e.Row.Cells[10].Text = statusText;

            // 转换 releasetype 字段（假设类型列是第11列）
            int releaseType = Convert.ToInt32(rowView["releasetype"]);
            string releaseTypeText = GetReleaseTypeText(releaseType);
            e.Row.Cells[11].Text = releaseTypeText;
        }
    }

    private string GetStatusText(int status)
    {
        switch (status)
        {
            case 0: return "Close";
            case 1: return "Complete";
            case 2: return "PathError";
            case 3: return "Buy off";
            default: return "Unknown";
        }
    }

    private string GetReleaseTypeText(int releaseType)
    {
        switch (releaseType)
        {
            case 1: return "Final Test";
            case 2: return "Partial Folder Release";
            case 3: return "Single File Release";
            default: return "Unknown";
        }
    }
}
