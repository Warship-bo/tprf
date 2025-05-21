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

public partial class Index : System.Web.UI.Page
{
    protected string GetStatusText(string statusValue)
    {
        switch (statusValue)
        {
            case "0": return "Close";
            case "1": return "Completed";
            case "2": return "PathError";
            case "3": return "Buy Off";
            default: return statusValue;
        }
    }

    // 发布类型转换方法
    protected string GetReleaseTypeText(string releaseTypeValue)
    {
        switch (releaseTypeValue)
        {
            case "1": return "Final Test";
            case "2": return "Partial Folder Release";
            case "3": return "Single File Release";
            default: return releaseTypeValue;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            lblCurrentUser.Text = Session["UserName"].ToString();
            if (!IsPostBack)
            {
                dgList.Attributes.Add("style", "word-wrap:break-word; word-break:break-all;");
                dgDataBind();
            }
        }
        else
        { Response.Redirect("Login.aspx"); }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void lbIndex_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }
    protected void lbDefault_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgList.CurrentPageIndex = e.NewPageIndex;
        dgDataBind();
    }

    private void dgDataBind()
    {
        DataSet ds = DBTran.TPRFList();
        dgList.DataSource = ds;
        dgList.DataBind();
    }
    protected void dgList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.Trim().Equals("BuyOff"))
        {
            string strID = e.Item.Cells[1].Text.ToString().Trim();
            DBTran.BuyOff(strID);
        }
        dgDataBind();
    }
    protected void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // 获取状态和发布类型的值
            string statusValue = e.Item.Cells[9].Text.Trim();
            string releaseTypeValue = e.Item.Cells[2].Text.Trim();

            // 转换状态值
            e.Item.Cells[9].Text = GetStatusText(statusValue);

            // 转换发布类型值
            e.Item.Cells[2].Text = GetReleaseTypeText(releaseTypeValue);

            // 原有的逻辑保持不变
            if (statusValue.Equals("1")) // 数据库中的1对应界面上的Complete
            {
                e.Item.Cells[0].Visible = true;
            }
            else
            {
                e.Item.Cells[10].Visible = false;
            }
        }
    }
}
