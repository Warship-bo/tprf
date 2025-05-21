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

public partial class Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            lblCurrentUser.Text = Session["UserName"].ToString();
            if (Request.QueryString.HasKeys())
            {
                string ID = Request.QueryString["ID"].ToString().Trim();
                DetailByID(ID);
                DetailsView dv = (DetailsView)this.FindControl("dvDetail");
                TextBox txt = (TextBox)dv.FindControl("txtCustomer");
                string str = txt.Text.Trim();
                string s = ((TextBox)dv.FindControl("txtCustomer")).Text.Trim().ToUpper();
                if (((TextBox)dv.FindControl("txtCustomer")).Text.Trim().ToUpper() == "QUL")
                {
                    ((HtmlTableRow)dv.FindControl("trQUL0")).Visible = true;
                    ((HtmlTableRow)dv.FindControl("trQUL1")).Visible = true;
                    ((HtmlTableRow)dv.FindControl("trQUL2")).Visible = true;
                }
                else
                {
                    ((HtmlTableRow)dv.FindControl("trQUL0")).Visible = false;
                    ((HtmlTableRow)dv.FindControl("trQUL1")).Visible = false;
                    ((HtmlTableRow)dv.FindControl("trQUL2")).Visible = false;
                }
            }
        }
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
        Response.Redirect("Default.aspx");
    }

    private void DetailByID(string ID)
    {
        DataSet ds = new DataSet();
        ds = DBTran.TPRFDetail(ID);
        dvDetail.DataSource = ds;
        dvDetail.DataBind();
        //Model objModel = new Model();
        //objModel = DBTran.TPRFDetail(ID);
        //dvDetail.DataSource = objModel;
    }
}
