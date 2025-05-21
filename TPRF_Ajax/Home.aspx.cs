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

using Oracle.ManagedDataAccess.Client;

public partial class Home : System.Web.UI.Page
{
    public static string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString().Trim();
    public static string AttachNavigateUrlSuffix = System.Configuration.ConfigurationManager.AppSettings["AttachNavigateUrlSuffix"].ToString().Trim();
    public static string ErrorLogPath = System.Configuration.ConfigurationManager.AppSettings["ErrorLogPath"].ToString().Trim();
    private static string eTIDBConnection = System.Configuration.ConfigurationManager.AppSettings["eTIDBConnection"].ToString().Trim();
    public static string AttachPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            lblCurrentUser.Text = Session["UserName"].ToString();
            if (!IsPostBack)
            {
                tbodyMain.Visible = false;
                trDeleteOldProgram.Visible = false;
                trReleaseType.Visible = false;
                trQULRemark.Visible = false;
                fuPiggyback.Visible = false;
                btnUploadPiggyback.Visible = false;
                hlPiggyback.Visible = false;
                tbodySPR.Visible = false;
                tbodyNoSPR.Visible = true;
            }
        }
        else { Response.Redirect("Login.aspx"); }
    }
    protected void lbDefault_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void lbIndex_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }
    protected void lbHelp_Click(object sender, EventArgs e)
    {
        Response.Redirect("tprfpath.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void rblReleaseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        tbodyNoSPR.Visible = true;
        tbodySPR.Visible = false;
        tbodyMain.Visible = true;
        rblAction.Items.Clear();
        if (rblAction.SelectedIndex == -1)
        {
            ddlCustomerBind();
        }
        else if (rblAction.SelectedIndex == 0)
        {
            ddlCustomerBind();
        }
        else if (rblAction.SelectedIndex == 1)
        {
            ddlCustomerBindtemp();
        }
        else if (rblAction.SelectedIndex == 2)
        {
            ddlCustomerBind();
        }

        ddlCustomer.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        // Tester, Device, Stage, Program Path, Program Name, Program Revision --> Empty
        ddlTester.Items.Clear(); ddlTester.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlDevice.Items.Clear(); ddlDevice.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlStage.Items.Clear(); ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlProgramPath.Items.Clear(); ddlProgramPath.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        txtProgramName.Text = string.Empty;
        txtProgramRevision.Text = string.Empty;
        //rblAction.SelectedIndex = 0;
        ckPiggyBack.Checked = false;
        txtMCNCheck.Text = string.Empty;
        txtQFPROMCheck.Text = string.Empty;
        txtGlobalOveron.Text = string.Empty;
        txtRemark.Text = string.Empty;


        if (!rblReleaseType.SelectedValue.Trim().Equals("NewRelease"))
        {
            trDeleteOldProgram.Visible = true;
            trReleaseType.Visible = true;

            rblUpdateType.SelectedValue = "1";
            if (rblDeleteOption.Items.Contains(new ListItem("DO NOT Delete/不删除", "0")))
            {
                rblDeleteOption.Items.RemoveAt(0);
                rblDeleteOption.SelectedValue = "2";
            }
            else { rblDeleteOption.SelectedValue = "2"; }
            rblAction.Items.Add(new ListItem("Release To Prod"));
            rblAction.Items.Add(new ListItem("Release To STR"));
            rblAction.Items.Add(new ListItem("Delete from Prod"));
            rblAction.SelectedIndex = 0; // 默认选择第一个选项
        }
        else
        {
            rblAction.Items.Add(new ListItem("Release To Prod"));
            rblAction.Items.Add(new ListItem("Release To STR"));

            rblAction.SelectedIndex = 0; // 默认选择第一个选项
            trDeleteOldProgram.Visible = false;
            trReleaseType.Visible = false;
        }
    }
    //protected void rblReleaseType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    tbodyNoSPR.Visible = true;
    //    tbodySPR.Visible = false;
    //    tbodyMain.Visible = true;
    //    if (rblAction.SelectedIndex == -1)
    //    {
    //        ddlCustomerBind();
    //    }
    //    else if (rblAction.SelectedIndex == 0 )
    //    {
    //        ddlCustomerBind();
    //    }
    //    else if (rblAction.SelectedIndex == 1)
    //    {
    //        ddlCustomerBindtemp();
    //    }
    //    else if (rblAction.SelectedIndex == 2)
    //    {
    //        ddlCustomerBind();
    //    }

    //    ddlCustomer.Items.Insert(0, new ListItem("-=Please Select=-","N/A"));
    //    // Tester, Device, Stage, Program Path, Program Name, Program Revision --> Empty
    //    ddlTester.Items.Clear(); ddlTester.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
    //    ddlDevice.Items.Clear(); ddlDevice.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
    //    ddlStage.Items.Clear(); ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
    //    ddlProgramPath.Items.Clear(); ddlProgramPath.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
    //    txtProgramName.Text = string.Empty;
    //    txtProgramRevision.Text = string.Empty;
    //    rblAction.SelectedIndex = 0;
    //    ckPiggyBack.Checked = false;
    //    txtMCNCheck.Text = string.Empty;
    //    txtQFPROMCheck.Text = string.Empty;
    //    txtGlobalOveron.Text = string.Empty;
    //    txtRemark.Text = string.Empty;


    //    if (!rblReleaseType.SelectedValue.Trim().Equals("NewRelease"))
    //    {
    //        trDeleteOldProgram.Visible = true;
    //        trReleaseType.Visible = true;

    //        rblUpdateType.SelectedValue = "1";
    //        if (rblDeleteOption.Items.Contains(new ListItem("DO NOT Delete/不删除", "0")))
    //        {
    //            rblDeleteOption.Items.RemoveAt(0);
    //            rblDeleteOption.SelectedValue = "2";
    //        }
    //        else { rblDeleteOption.SelectedValue = "2"; }
    //    }
    //    else
    //    {
    //        trDeleteOldProgram.Visible = false;
    //        trReleaseType.Visible = false;
    //    }
    //}
    protected void rblUpdateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!rblUpdateType.SelectedValue.Trim().Equals("1"))
        {
            if (rblDeleteOption.Items.Contains(new ListItem("DO NOT Delete/不删除", "0")))
            { rblDeleteOption.SelectedValue = "0"; }
            else
            {
                rblDeleteOption.Items.Insert(0, new ListItem("DO NOT Delete/不删除", "0"));
                rblDeleteOption.SelectedValue = "0";
            }
        }
        else
        {
            rblDeleteOption.Items.RemoveAt(0);
            rblDeleteOption.SelectedValue = "2";
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
            ddlTester.Items.Clear(); ddlTester.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
            ddlDevice.Items.Clear(); ddlDevice.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
            ddlStage.Items.Clear(); ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
            ddlProgramPath.Items.Clear(); ddlProgramPath.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
            txtProgramName.Text = string.Empty;
            txtProgramRevision.Text = string.Empty;

        if (rblAction.SelectedIndex == -1 || rblAction.SelectedIndex == 0 || rblAction.SelectedIndex == 2)
        {
            switch (ddlCustomer.SelectedValue.Trim().ToUpper())
            {
                case "N/A":
                    break;
                case "SPR-SAMMI":
                    tbodySPR.Visible = true;
                    tbodyNoSPR.Visible = false;
                    break;
                default:
                    tbodySPR.Visible = false;
                    tbodyNoSPR.Visible = true;
                    // Tester Bind
                    //string strSQL = "select distinct tester_ft1 TesterType from productinfo where tester_ft1!='N/A' and customer='" + ddlCustomer.SelectedValue.Trim() + "' order by tester_ft1";
                    //string strSQL = "select distinct TesterType from ( select tester_ft1 TesterType from ProductInfo where customer = '" + ddlCustomer.SelectedValue.Trim() + "' and tester_ft1!='N/A'  union all  select tester_ft2 TesterType from ProductInfo where releaseflag<990 and customer = '" + ddlCustomer.SelectedValue.Trim() + "' and tester_ft2!='N/A' union all  select tester_ft3 TesterType from ProductInfo where releaseflag<990 and customer = '" + ddlCustomer.SelectedValue.Trim() + "' and tester_ft3!='N/A') a";
                    string strSQL = "select  distinct a.Tester  from ProductInfo a left join ProductInfo_header b on a.ID = b.ID  where    b.releaseflag<990  and a.customer = '" + ddlCustomer.SelectedValue.Trim() + "' and a.Tester!='N/A' ";
                   //DataSet ds = DBTransfer.getDataSet(strSQL);
                    DataSet ds = DBTransfer.getEtiOraDataSet(strSQL);
                    ddlTester.DataSource = ds;
                    //ddlTester.DataTextField = "TesterType";
                    //ddlTester.DataValueField = "TesterType";
                    ddlTester.DataTextField = "Tester";
                    ddlTester.DataValueField = "Tester";
                    ddlTester.DataBind();
                    ddlTester.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));

                    if (ddlCustomer.SelectedValue.Trim().Equals("QUL"))
                    { trQULRemark.Visible = true; }
                    else
                    { trQULRemark.Visible = false; }
                    break;
            }
        }
        else if(rblAction.SelectedIndex ==1)
        {
            switch (ddlCustomer.SelectedValue.Trim().ToUpper())
            {
                case "N/A":
                    break;
                case "SPR-SAMMI":
                    tbodySPR.Visible = true;
                    tbodyNoSPR.Visible = false;
                    break;
                default:
                    tbodySPR.Visible = false;
                    tbodyNoSPR.Visible = true;
                    // Tester Bind
                    //string strSQL = "select distinct tester_ft1 TesterType from productinfo where releaseflag < 990 and tester_ft1!='N/A' and customer='" + ddlCustomer.SelectedValue.Trim() + "' order by tester_ft1";
                    string strSQL = "select distinct testermodelcode from VW_FT_TPAD_ESTR where SITEID = 'SCC'  and tplocation = 'STR' and custcode='" + ddlCustomer.SelectedValue.Trim() + "'";
                    DataSet ds = DBTransfer.getOraDataSet(strSQL);
                    ddlTester.DataSource = ds;
                    ddlTester.DataTextField = "testermodelcode";
                    ddlTester.DataValueField = "testermodelcode";
                    ddlTester.DataBind();
                    ddlTester.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));

                    if (ddlCustomer.SelectedValue.Trim().Equals("QUL"))
                    { trQULRemark.Visible = true; }
                    else
                    { trQULRemark.Visible = false; }
                    break;
            }
        }

    }
    protected void ddlTester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDevice.Items.Clear(); ddlDevice.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlStage.Items.Clear(); ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlProgramPath.Items.Clear(); ddlProgramPath.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        txtProgramName.Text = string.Empty;
        txtProgramRevision.Text = string.Empty;

        if (rblAction.SelectedIndex == -1 || rblAction.SelectedIndex == 0 || rblAction.SelectedIndex == 2)
        {
            if (ddlCustomer.SelectedValue.Trim().Equals("N/A") || ddlTester.SelectedValue.Trim().Equals("N/A")) { }
            else
            {
                // Device Bind
                //string strSQL = "select distinct DeviceName from productinfo where customer='" + ddlCustomer.SelectedValue.Trim() + (ddlCustomer.SelectedValue.Trim() == "ADI" ? ("' and tester_qa1='" + ddlTester.SelectedValue.Trim() + "'") : ("' and tester_ft1='" + ddlTester.SelectedValue.Trim() + "'"))+ " order by devicename";
                string strSQL = "select distinct a.DeviceName from ProductInfo a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' order by a.devicename";
                //DataSet ds = DBTransfer.getDataSet(strSQL);
                DataSet ds = DBTransfer.getEtiOraDataSet(strSQL);
                ddlDevice.DataSource = ds;
                ddlDevice.DataTextField = "DeviceName";
                ddlDevice.DataValueField = "DeviceName";
                ddlDevice.DataBind();
                ddlDevice.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
            }
        }
        else if (rblAction.SelectedIndex == 1)
        {
            if (ddlCustomer.SelectedValue.Trim().Equals("N/A") || ddlTester.SelectedValue.Trim().Equals("N/A")) { }
            else
            {
                // Device Bind
                //string strSQL = "select distinct DeviceName from productinfo where customer='" + ddlCustomer.SelectedValue.Trim() + (ddlCustomer.SelectedValue.Trim() == "ADI" ? ("' and tester_qa1='" + ddlTester.SelectedValue.Trim() + "'") : ("' and tester_ft1='" + ddlTester.SelectedValue.Trim() + "'"))+ " order by devicename";
                
                string strSQL = "select distinct custdeviceid  from VW_FT_TPAD_ESTR where SITEID = 'SCC' and custcode = '" + ddlCustomer.SelectedValue.Trim() + "' and tplocation = 'STR' and testermodelcode= '" + ddlTester.SelectedValue.Trim() + "'";
                DataSet ds = DBTransfer.getOraDataSet(strSQL);
                ddlDevice.DataSource = ds;
                ddlDevice.DataTextField = "custdeviceid";
                ddlDevice.DataValueField = "custdeviceid";
                ddlDevice.DataBind();
                ddlDevice.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
            }
        }
          
    }
    protected void ddlDevice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStage.Items.Clear(); ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlProgramPath.Items.Clear(); ddlProgramPath.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        txtProgramName.Text = string.Empty;
        txtProgramRevision.Text = string.Empty;

        if (ddlCustomer.SelectedValue.Trim().Equals("N/A") || ddlTester.SelectedValue.Trim().Equals("N/A")||ddlDevice.SelectedValue.Trim().Equals("N/A")) { }
        else if(rblAction.SelectedIndex == -1 || rblAction.SelectedIndex == 0 || rblAction.SelectedIndex == 2)
        {
            string strSQL = "";
            if (ddlCustomer.SelectedValue.Trim().ToUpper()=="AHM"||ddlCustomer.SelectedValue.Trim().ToUpper() == "CXN")
            {
                //string BI = "select 'BI' a.OPERATION from ProductInfo a left join ProductInfo_header b on a.ID = b.ID   where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string ft1 = "select 'FT1' a.OPERATION from ProductInfo  a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string ft2 = "select 'FT2' a.OPERATION from ProductInfo a left join ProductInfo_header b on a.ID = b.ID   where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string ft3 = "select 'FT3' a.OPERATION from ProductInfo a left join ProductInfo_header b on a.ID = b.ID   where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string ft4 = "select 'FT4' a.OPERATION from ProductInfo  a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string ft5 = "select 'FT5' a.OPERATION from ProductInfo  a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string ft6 = "select 'FT6' a.OPERATION from ProductInfo a left join ProductInfo_header b on a.ID = b.ID   where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string qa1 = "select 'QA1' a.OPERATION from ProductInfo  a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string qa2 = "select 'QA2' a.OPERATION from ProductInfo  a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string qa3 = "select 'QA3' a.OPERATION from ProductInfo a left join ProductInfo_header b on a.ID = b.ID   where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string ft9 = "select 'FT9' a.OPERATION from ProductInfo a left join ProductInfo_header b on a.ID = b.ID   where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //string qa4 = "select 'QA4' a.OPERATION from ProductInfo a left join ProductInfo_header b on a.ID = b.ID    where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester = '" + ddlTester.SelectedValue.Trim() + "'";
                //  strSQL = BI + " union " + ft1 + " union " + ft2 + " union " + ft3 + " union " + ft4 + " union " + ft5 + " union " + ft6 + " union " + ft9 + " union " + qa1 + " union " + qa2 + " union " + qa3 + " union " + qa4;
                strSQL = " SELECT distinct case  when  a.OPERATION='FT0' then 'BI'  else OPERATION end  as OPERATION  FROM ProductInfo a LEFT JOIN ProductInfo_header b ON a.ID = b.ID WHERE  b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "'and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "'and a.tester = '" + ddlTester.SelectedValue.Trim() + "' ";
            }
            else
            {
                  strSQL = "select distinct a.OPERATION ,a.customer from ProductInfo a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "' and a.tester  = '" + ddlTester.SelectedValue.Trim() + "'";
            }
           
            if(ddlTester.SelectedValue.Trim().ToUpper().Contains("PCT"))
            {
                //strSQL = " SELECT distinct case  when  a.OPERATION='FT0' then 'BI'  else OPERATION end  as OPERATION  FROM ProductInfo a LEFT JOIN ProductInfo_header b ON a.ID = b.ID WHERE  b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "'and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "'and a.tester = '" + ddlTester.SelectedValue.Trim() + "' ";
                strSQL = " SELECT distinct itms_call_procedure as OPERATION  FROM ProductInfo a LEFT JOIN ProductInfo_header b ON a.ID = b.ID WHERE  b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "'and a.devicename = '" + ddlDevice.SelectedValue.Trim() + "'and a.tester = '" + ddlTester.SelectedValue.Trim() + "' ";
            }
            // Stage Bind
            //string strSQL = "select replace(name,'programname_','') Stage from syscolumns where id=object_id('productinfo') and name like 'programname%' order by name";

            //DataSet ds = DBTransfer.getDataSet(strSQL);
            DataSet ds = DBTransfer.getEtiOraDataSet(strSQL);
            ddlStage.DataSource = ds;
            //ddlStage.DataTextField = "Stage";
            //ddlStage.DataValueField = "Stage";
            ddlStage.DataTextField = "OPERATION";
            ddlStage.DataValueField = "OPERATION";
            ddlStage.DataBind();
            ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        }
        else if(rblAction.SelectedIndex == 1)
        {
            string ft0 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '302T' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";

            string ft1 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3020' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string ft2 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3033' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string ft3 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3035' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string ft4 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3021' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string ft5 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '302B' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string ft6 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '302C' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string ft9 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '302F' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string qa1 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3020E' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string qa2 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3033E' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string qa3 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3035E' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string qa4 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3021E' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string slt1 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3027' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";
            string slt2 = "select teststage insertiontypename from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR' and teststage = '3028' and custcode='" + ddlCustomer.SelectedValue.Trim() + "' and custdeviceid = '" + ddlDevice.SelectedValue.Trim() + "' and testermodelcode = '" + ddlTester.SelectedValue.Trim() + "'";

            string strSQL = ft0 + " union " + ft1 + " union " + ft2 + " union " + ft3 + " union " + ft4 + " union " + ft5 + " union " + ft6 + " union " + ft9 + " union " + qa1 + " union " + qa2 + " union " + qa3 + " union " + qa4 + " union " + slt1 + " union " + slt2;

            DataSet ds = DBTransfer.getOraDataSet(strSQL);
            ddlStage.DataSource = ds;
            ddlStage.DataTextField = "insertiontypename";
            ddlStage.DataValueField = "insertiontypename";
            ddlStage.DataBind();
            ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        }
    }
    protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProgramPath.Items.Clear(); ddlProgramPath.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        txtProgramName.Text = string.Empty;
        txtProgramRevision.Text = string.Empty;

        if (ddlCustomer.SelectedValue.Trim().Equals("N/A") || ddlTester.SelectedValue.Trim().Equals("N/A")
            || ddlDevice.SelectedValue.Trim().Equals("N/A") || ddlStage.SelectedValue.Trim().Equals("N/A")) { }
        else if (rblAction.SelectedIndex == -1 || rblAction.SelectedIndex == 0 || rblAction.SelectedIndex == 2)
        {
            string operation = "";
            if(ddlCustomer.SelectedValue.Trim().ToUpper()=="AHM"&&ddlStage.SelectedValue.Trim().ToUpper()=="BI")
            { operation = "FT0"; }
            else if(ddlCustomer.SelectedValue.Trim().ToUpper() == "CXN" && ddlStage.SelectedValue.Trim().ToUpper() == "BI")
            { operation = "FT0"; }
            else { operation = ddlStage.SelectedValue.Trim(); }

            // Program Path Bind
            //string strSQL = "select distinct programdirectory_" + ddlStage.SelectedValue.Trim() + " ProgramPath,programname_" + ddlStage.SelectedValue.Trim() + " ProgramName,programrevision_" + ddlStage.SelectedValue.Trim() + " ProgramRevision from productinfo(nolock) where releaseflag < 990 and customer='" + ddlCustomer.SelectedValue.Trim() + "' and devicename='" + ddlDevice.SelectedValue.Trim() + "'" ;
            //string strSQL = "select distinct programdirectory_" + ddlStage.SelectedValue.Trim() + " ProgramPath,programname_" + ddlStage.SelectedValue.Trim() + " ProgramName,programrevision_" + ddlStage.SelectedValue.Trim() + " ProgramRevision from productinfo  a left join ProductInfo_QA4  b on ID = ETIID where releaseflag < 990 and customer='" + ddlCustomer.SelectedValue.Trim() + "' and devicename='" + ddlDevice.SelectedValue.Trim() + "'" ;
            string strSQL = "";
            if (ddlTester.SelectedValue.Trim().ToUpper().Contains("PCT"))
            {
                  strSQL = "select  distinct  a.programdirectory ProgramPath,a.programname ProgramName,a.programrevision ProgramRevision  from ProductInfo a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename='" + ddlDevice.SelectedValue.Trim() + "' and a.itms_call_procedure='" + operation + "' ";

            }
            else
            {
                  strSQL = "select  distinct  a.programdirectory ProgramPath,a.programname ProgramName,a.programrevision ProgramRevision  from ProductInfo a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename='" + ddlDevice.SelectedValue.Trim() + "' and a.operation='" + operation + "' ";
            }



            //DataSet ds = DBTransfer.getDataSet(strSQL);
            DataSet ds = DBTransfer.getEtiOraDataSet(strSQL);
            ddlProgramPath.DataSource = ds;
            ddlProgramPath.DataTextField = "ProgramPath";
            ddlProgramPath.DataBind();
            txtProgramName.Text = ds.Tables[0].Rows[0]["ProgramName"].ToString().Trim();
            txtProgramRevision.Text = ds.Tables[0].Rows[0]["ProgramRevision"].ToString().Trim();
        }
        else if (rblAction.SelectedIndex == 1)
        {
            string strSQL = "select distinct tpdirectory ,tpname,tprev from VW_FT_TPAD_ESTR where teststage = '" + ddlStage.SelectedValue.Trim() + "' and custcode = '" + ddlCustomer.SelectedValue.Trim() + "' and tplocation = 'STR' and testermodelcode= '" + ddlTester.SelectedValue.Trim() + "'and custdeviceid='" + ddlDevice.SelectedValue.Trim() + "'";
            DataSet ds = DBTransfer.getOraDataSet(strSQL);
            ddlProgramPath.DataSource = ds;
            ddlProgramPath.DataTextField = "tpdirectory";
            ddlProgramPath.DataValueField = "tpdirectory";
            ddlProgramPath.DataBind();
            txtProgramName.Text = ds.Tables[0].Rows[0]["tpname"].ToString().Trim();
            txtProgramRevision.Text = ds.Tables[0].Rows[0]["tprev"].ToString().Trim();
        }
    }
    protected void ddlProgramPath_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (rblAction.SelectedIndex == -1 || rblAction.SelectedIndex == 0 || rblAction.SelectedIndex == 2)
      {
            //string strSQL = "select distinct programname_" + ddlStage.SelectedValue.Trim() + " ProgramName,programrevision_" + ddlStage.SelectedValue.Trim() + " ProgramRevision from productinfo(nolock) where releaseflag < 990 and customer='" + ddlCustomer.SelectedValue.Trim() + "' and devicename='" + ddlDevice.SelectedValue.Trim() + "' and programdirectory_" + ddlStage.SelectedValue.Trim() + "='" + ddlProgramPath.SelectedValue.Trim() + "'" + "and tester_" + ddlStage.SelectedValue.Trim() + "= '" + ddlTester.SelectedValue.Trim() + "'" ;
            //string strSQL = "select distinct programname_" + ddlStage.SelectedValue.Trim() + " ProgramName,programrevision_" + ddlStage.SelectedValue.Trim() + " ProgramRevision from productInfo a left join ProductInfo_QA4 b on a.ID = b.ETIID where a.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename='" + ddlDevice.SelectedValue.Trim() + "' and programdirectory_" + ddlStage.SelectedValue.Trim() + "='" + ddlProgramPath.SelectedValue.Trim() + "'" + "and tester_" + ddlStage.SelectedValue.Trim() + "= '" + ddlTester.SelectedValue.Trim() + "'" ;
            // string strSQL = "select distinct a.programname  ProgramName,a.programrevision ProgramRevision from productInfo a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename='" + ddlDevice.SelectedValue.Trim() + "'  and a.tester = '" + ddlTester.SelectedValue.Trim() + "' and a.operation='" + ddlStage.SelectedValue.Trim() + "'";
            string operation = ddlStage.SelectedValue.Trim();
            if(operation.ToUpper()=="BI")
            { operation = "FT0"; }

            string strSQL = "select distinct a.programname  ProgramName,a.programrevision ProgramRevision from productInfo a left join ProductInfo_header b on a.ID = b.ID  where b.releaseflag < 990 and a.customer='" + ddlCustomer.SelectedValue.Trim() + "' and a.devicename='" + ddlDevice.SelectedValue.Trim() + "'  and a.tester = '" + ddlTester.SelectedValue.Trim() + "' and a.operation='" + operation + "' and a.programdirectory ='" + ddlProgramPath.SelectedValue.Trim() + "'    ";


            //DataSet ds = DBTransfer.getDataSet(strSQL);
            DataSet ds = DBTransfer.getEtiOraDataSet(strSQL);
        if (ds != null)
        {
            txtProgramName.Text = ds.Tables[0].Rows[0]["ProgramName"].ToString().Trim();
            txtProgramRevision.Text = ds.Tables[0].Rows[0]["ProgramRevision"].ToString().Trim();
        }
      }
    }
    protected void ckPiggyBack_CheckedChanged(object sender, EventArgs e)
    {
        if (ckPiggyBack.Checked)
        {
            fuPiggyback.Visible = true;
            btnUploadPiggyback.Visible = true;
            hlPiggyback.Visible = true;
        }
        else
        {
            fuPiggyback.Visible = false;
            btnUploadPiggyback.Visible = false;
            hlPiggyback.Visible = false;
        }
    }
    protected void btnUploadPiggyback_Click(object sender, EventArgs e)
    {
        string requstPath = Request.Path.Trim();
        if (fuPiggyback.HasFile)
        {
            string fileName = fuPiggyback.FileName.Trim();
            string suffix = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
            fileName = fileName.Replace(suffix, "_" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + suffix);
            if (!System.IO.File.Exists(UploadPath + fileName))
            {
                fuPiggyback.SaveAs(UploadPath + fileName);
                hlPiggyback.Visible = true;
                requstPath = requstPath.Substring(0, requstPath.LastIndexOf("/")) + "/PiggybackCheck/";
                hlPiggyback.Text = fileName;
                hlPiggyback.Target = "_blank";
                hlPiggyback.NavigateUrl = AttachNavigateUrlSuffix + requstPath + fileName;
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string requstPath = Request.Path.Trim();
        if (fuAttach.HasFile)
        {
            string fileName = fuAttach.FileName.Trim();
            string suffix = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
            fileName = fileName.Replace(suffix, "_" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + suffix);
            if (!System.IO.File.Exists(UploadPath + fileName))
            {
                fuAttach.SaveAs(UploadPath + fileName);
                hlAttach.Visible = true;
                requstPath = requstPath.Substring(0, requstPath.LastIndexOf("/")) + "/Upload/";
                hlAttach.Text = requstPath + fileName;
                hlAttach.Target = "_blank";
                hlAttach.NavigateUrl = AttachNavigateUrlSuffix + requstPath + fileName;
                AttachPath = UploadPath + fileName;
            }
            //System.Threading.Thread.Sleep(1000);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ID = "";
            Modeler objModel = new Modeler();
            objModel.ReleaseType = Convert.ToInt16(rblReleaseType.SelectedValue.Trim().Equals("NewRelease") ? "1" : rblUpdateType.SelectedValue.Trim());
            objModel.Status = Convert.ToInt16(0);
            objModel.Customer = ddlCustomer.SelectedValue.Trim();
            if (ddlCustomer.SelectedValue.Trim().ToLower().Equals("spr-sammi"))
            {
                if (ddlDirectory.SelectedValue.Trim().Equals("N/A"))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Result", @"<script language='javascript'>alert('Please Select SPR Program Directory!');</script>");
                }
                else
                {
                    Boolean bolCompare = false;
                    string SPRAttachPath = ViewState["SPRAttachPath"].ToString().Trim();
                    ArrayList arrs = new ArrayList();
                    arrs = DBTran.AttachContents(SPRAttachPath);
                    foreach (ArrayList arr in arrs)
                    {
                        string strDevice = arr[1].ToString().Trim();
                        DataSet ds = new DataSet();
                        string strSQL = "select programname_ft1,programdirectory_ft1,programrevision_ft1 from productinfo where devicename='" + strDevice + "'";
                        ds = DBTransfer.getDataSet(strSQL);
                        string strProgramName = ds.Tables[0].Rows[0][0].ToString().Trim();
                        string strProgramDirectory = ds.Tables[0].Rows[0][1].ToString().Trim();
                        string strProgramRevision = ds.Tables[0].Rows[0][2].ToString().Trim();
                        objModel.TesterType = "93k PS1600 Agilent";
                        objModel.Device = arr[1].ToString().Trim();
                        objModel.Stage = "FT1";
                        objModel.ProgramPath = strProgramDirectory;// ddlDirectory.SelectedValue.Trim() + "/" + arr[4].ToString().Trim();
                        objModel.ProgramName = strProgramName;// arr[4].ToString().Trim();
                        objModel.ProgramRevision = strProgramRevision;// "";
                        objModel.Action = "Release To Prod";
                        objModel.Attachment = hlAttachSPR.Text.Trim();
                        objModel.RemarkForCustomer0 = "";
                        objModel.RemarkForCustomer1 = "";
                        objModel.RemarkForCustomer2 = "";
                        objModel.Remark = "";
                        objModel.SubmitUser = lblCurrentUser.Text.Trim();
                        objModel.PiggybackCheck = "N";
                        objModel.PiggybackCheckAttach = "";
                        objModel.AttachReleaseDate = arr[0].ToString().Trim();
                        objModel.AttachDevice = arr[1].ToString().Trim();
                        objModel.AttachFT1ProgramFlow = arr[2].ToString().Trim();
                        objModel.AttachQA1ProgramFlow = arr[3].ToString().Trim();
                        objModel.AttachFT1Program = arr[4].ToString().Trim();
                        objModel.AttachQA1Program = arr[5].ToString().Trim();
                        objModel.AttachDeviceVersion = arr[6].ToString().Trim();
                        objModel.OldProgram = "";
                    
                        objModel.DeleteFlag = "0";
                        string s1 = strProgramDirectory.Substring(strProgramDirectory.IndexOf('/') + 1, strProgramDirectory.Length - strProgramDirectory.IndexOf('/') - 1);
                        string s2 = arr[4].ToString().Trim();


                        if (!strProgramName.Replace(".tp", "").Equals(arr[2].ToString().Trim().Replace(".flw", "")))
                        {
                            bolCompare = false;
                            break;
                        }
                        else
                        {
                            bolCompare = true;

                            Boolean bolReturn = DBTransfer.TPRFInfoSPR(objModel, ref ID);


                        }
                    }
                    if (bolCompare == false)
                    { ClientScript.RegisterStartupScript(this.GetType(), "Result", @"<script language='javascript'>alert('Program Name/Flow in Attach NOT Same AS eTI');</script>"); }
                    else
                    {
                        if (ID.Equals("-1"))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Result", @"<script language='javascript'>alert('Request already exists, DO NOT re-submit!');window.location.href='Index.aspx' </script>");
                        }
                        else
                        {
                            Response.Redirect("Index.aspx");
                        }
                    }
                }
            }
            else
            {
                if (this.ddlCustomer.SelectedValue == "-=Please Select=-")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language='javascript'>alert('Please select Customer!')</script>");
                }
                else if (this.ddlTester.SelectedValue == "-=Please Select=-")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language='javascript'>alert('Please select Tester Type!')</script>");
                }
                else if (ddlDevice.SelectedIndex < 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language='javascript'>alert('Please input Device!')</script>");
                }
                else if (this.ddlStage.SelectedValue == "-=Please Select=-")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language='javascript'>alert('Please input Stage!')</script>");
                }
                else if (this.ddlProgramPath.SelectedValue == "-=Please Select=-")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language='javascript'>alert('Please input ProgramPath!')</script>");
                }
                else if (this.txtProgramName.Text.Trim() == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language='javascript'>alert('Please input ProgramName!')</script>");
                }
                else if (this.txtProgramRevision.Text.Trim() == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", "<script language='javascript'>alert('Please input ProgramRevision!')</script>");
                }
                 
                else
                {
                    objModel.TesterType = ddlTester.SelectedValue.Trim();
                    objModel.Device = ddlDevice.SelectedValue.Trim();
                    objModel.Stage = ddlStage.SelectedValue.Trim();
                    objModel.ProgramPath = ddlProgramPath.SelectedValue.Trim();
                    objModel.ProgramName = txtProgramName.Text.Trim();
                    objModel.ProgramRevision = txtProgramRevision.Text.Trim();
                    objModel.Action = rblAction.SelectedValue.Trim();
                    objModel.Attachment = hlAttach.Text.Trim();
                    objModel.RemarkForCustomer0 = txtMCNCheck.Text.Trim();
                    objModel.RemarkForCustomer1 = txtQFPROMCheck.Text.Trim();
                    objModel.RemarkForCustomer2 = txtGlobalOveron.Text.Trim();
                    objModel.Remark = txtRemark.Text.Trim();
                    objModel.SubmitUser = lblCurrentUser.Text.Trim();
                    objModel.PiggybackCheck = ckPiggyBack.Checked == true ? "Y" : "N";
                    objModel.PiggybackCheckAttach = hlPiggyback.NavigateUrl.Replace(AttachNavigateUrlSuffix, "");
                    objModel.AttachReleaseDate = "";
                    objModel.AttachDevice = "";
                    objModel.AttachFT1ProgramFlow = "";
                    objModel.AttachQA1ProgramFlow = "";
                    if (this.rblAction.SelectedValue.Trim() == "Delete from Prod")
                    {
                        try
                        {
                            // 获取当前选择的客户和程序版本
                            string customer = ddlCustomer.SelectedValue.Trim();
                            string programRevision = txtProgramRevision.Text.Trim();

                            // 构建SQL查询语句，根据客户和程序版本查找记录
                            string selectSql = @"
            SELECT AttachFT1Program 
            FROM tprfinfo 
            WHERE customer = :customer 
            AND programrevision = :programRevision
            AND status = 1
            AND ROWNUM = 1"; // 假设我们只需要第一条匹配的记录

                            // 创建Oracle参数化查询
                            using (OracleConnection conn = new OracleConnection(eTIDBConnection))
                            {
                                using (OracleCommand cmd = new OracleCommand(selectSql, conn))
                                {
                                    // 添加参数防止SQL注入
                                    cmd.Parameters.Add(":customer", OracleDbType.Varchar2).Value = customer;
                                    cmd.Parameters.Add(":programRevision", OracleDbType.Varchar2).Value = programRevision;

                                    try
                                    {
                                        conn.Open();
                                        // 执行查询获取结果
                                        object result = cmd.ExecuteScalar();

                                        // 如果找到记录，设置AttachFT1Program属性
                                        if (result != null && result != DBNull.Value)
                                        {
                                            objModel.AttachFT1Program = result.ToString().Trim();
                                        }
                                        else
                                        {
                                            // 未找到匹配记录，提示用户
                                            ClientScript.RegisterStartupScript(this.GetType(), "Result",
                                                @"<script language='javascript'>alert('未找到匹配的程序记录!');</script>");
                                            return; // 停止处理
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        UtilSelf.WriteFile.Log("查询AttachFT1Program错误:" + ex.Message, ErrorLogPath);
                                        ClientScript.RegisterStartupScript(this.GetType(), "Result",
                                            @"<script language='javascript'>alert('查询程序记录时出错!');</script>");
                                        return;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            UtilSelf.WriteFile.Log("处理Delete from Prod错误:" + ex.Message, ErrorLogPath);
                            ClientScript.RegisterStartupScript(this.GetType(), "Result",
                                @"<script language='javascript'>alert('处理删除请求时出错!');</script>");
                            return;
                        }
                    }
                    objModel.AttachQA1Program = "";
                    objModel.AttachDeviceVersion = "";
                    objModel.OldProgram = txtOldProgram.Text.Trim();
                    objModel.ReleasedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    objModel.DeleteFlag = rblReleaseType.SelectedValue.Trim().Equals("NewRelease") ? "0" : rblDeleteOption.SelectedValue.Trim();


                    Boolean bolReturn = DBTransfer.TPRFInfo(objModel, ref ID);


                    if (ID.Equals("-1"))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Result", @"<script language='javascript'>alert('Request already exists, DO NOT re-submit!');window.location.href='Index.aspx' </script>");
                    }
                    else
                    {
                        Response.Clear(); // 清除缓冲区
                        Response.Redirect("Index.aspx", false);
                    }
                }
       
            }
        }
        catch (Exception ex)
        {
            UtilSelf.WriteFile.Log("Write SQL DataBase Error:" + ex.Message, ErrorLogPath);
            ClientScript.RegisterStartupScript(this.GetType(), "Result", @"<script language='javascript'>alert('System Error! Please contract with the engineer!'); </script>");
        }
    }

    //private void ddlCustomerBind()
    //{
    //        //string strSQL = "select * from customer(nolock) order by customer";
    //    string strSQL = "select distinct a.customer from productinfo a left join ProductInfo_header b on a.ID = b.ID  where b. releaseflag < 990  and a.customer is not null order by customer";
    //        //DataSet ds = DBTransfer.getDataSet(strSQL);
    //        DataSet ds = DBTransfer.getEtiOraDataSet(strSQL);
    //        ddlCustomer.DataSource = ds;
    //        ddlCustomer.DataTextField = "customer";
    //        ddlCustomer.DataValueField = "customer";
    //        ddlCustomer.DataBind();

    //}

    private void ddlCustomerBind()
    {
        //string strSQL = "select * from customer(nolock) order by customer";
        string strSQL = "select distinct a.customer from productinfo a left join ProductInfo_header b on a.ID = b.ID  where b. releaseflag < 990  and a.customer is not null order by customer";
        //DataSet ds = DBTransfer.getDataSet(strSQL);
        DataSet ds = DBTransfer.getEtiOraDataSet(strSQL);
        ddlCustomer.DataSource = ds;
        ddlCustomer.DataTextField = "customer";
        ddlCustomer.DataValueField = "customer";
        ddlCustomer.DataBind();

    }
    //private void ddlCustomerBindtemp()
    //{
    //    string strSQL = "select distinct custcode from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR'";
    //    DataSet ds = DBTransfer.getOraDataSet(strSQL);
    //    ddlCustomer.DataSource = ds;
    //    ddlCustomer.DataTextField = "custcode";
    //    ddlCustomer.DataValueField = "custcode";
    //    ddlCustomer.DataBind();

    //}
    private void ddlCustomerBindtemp()
    {
        string strSQL = "select distinct custcode from  VW_FT_TPAD_ESTR where SITEID = 'SCC' and tplocation = 'STR'";
        DataSet ds = DBTransfer.getOraDataSet(strSQL);
        ddlCustomer.DataSource = ds;
        ddlCustomer.DataTextField = "custcode";
        ddlCustomer.DataValueField = "custcode";
        ddlCustomer.DataBind();

    }
    protected void btnSPRUpload_Click(object sender, EventArgs e)
    {
        string fileName = "";
        string requstPath = Request.Path.Trim();
        if (fuSPR.HasFile)
        {
            fileName = fuSPR.FileName.Trim();
            string suffix = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
            fileName = fileName.Replace(suffix, "_" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + suffix);
            if (!System.IO.File.Exists(UploadPath + fileName))
            {
                fuSPR.SaveAs(UploadPath + fileName);
                hlAttachSPR.Visible = true;
                requstPath = requstPath.Substring(0, requstPath.LastIndexOf("/")) + "/Upload/";
                hlAttachSPR.Text = requstPath + fileName;
                hlAttachSPR.Target = "_blank";
                hlAttachSPR.NavigateUrl = AttachNavigateUrlSuffix + requstPath + fileName;
                ViewState["SPRAttachPath"] = UploadPath + fileName;
            }
            System.Threading.Thread.Sleep(1000);
            DataTable dt = new DataTable();
            dt.Columns.Add("Device");
            dt.Columns.Add("FTProgram");
            dt.Columns.Add("QAProgram");
            dt.Columns.Add("FTFlow");
            dt.Columns.Add("QAFlow");
            dt.Columns.Add("DeviceVersion");
            ArrayList arrs = new ArrayList();
            arrs = DBTran.AttachContents(UploadPath + fileName);
            foreach (ArrayList arr in arrs)
            {
                DataRow dr = dt.NewRow();
                dr[0] = arr[1].ToString();
                dr[1] = arr[4].ToString();
                dr[2] = arr[5].ToString();
                dr[3] = arr[2].ToString();
                dr[4] = arr[3].ToString();
                dr[5] = arr[6].ToString();
                dt.Rows.Add(dr);
            }
            dgAttach.DataSource = dt;
            dgAttach.DataBind();
        }
    }

    protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblAction.SelectedIndex == -1)
        {
            ddlCustomerBind();
            tempfun();
        }
        else if (rblAction.SelectedIndex == 0 || rblAction.SelectedIndex == 2)
        {
            ddlCustomerBind();
            tempfun();
        }
        else if (rblAction.SelectedIndex == 1)
        {
            ddlCustomerBindtemp();
            tempfun();
        }
        
    }
    public void tempfun()
    {
        ddlCustomer.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        // Tester, Device, Stage, Program Path, Program Name, Program Revision --> Empty
        ddlTester.Items.Clear(); ddlTester.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlDevice.Items.Clear(); ddlDevice.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlStage.Items.Clear(); ddlStage.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        ddlProgramPath.Items.Clear(); ddlProgramPath.Items.Insert(0, new ListItem("-=Please Select=-", "N/A"));
        txtProgramName.Text = string.Empty;
        txtProgramRevision.Text = string.Empty;
        ckPiggyBack.Checked = false;
        txtMCNCheck.Text = string.Empty;
        txtQFPROMCheck.Text = string.Empty;
        txtGlobalOveron.Text = string.Empty;
        txtRemark.Text = string.Empty;
    }
}
