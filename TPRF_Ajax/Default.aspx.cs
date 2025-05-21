using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    public static string UploadPath = System.Configuration.ConfigurationManager.AppSettings["UploadPath"].ToString().Trim();
    public static string Host = System.Configuration.ConfigurationManager.AppSettings["Host"].ToString().Trim();
    public static string Userr = System.Configuration.ConfigurationManager.AppSettings["User"].ToString().Trim();
    public static string Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString().Trim();
    public static string ScriptPath = System.Configuration.ConfigurationManager.AppSettings["ScriptPath"].ToString().Trim();
    public static string ScriptName = System.Configuration.ConfigurationManager.AppSettings["ScriptName"].ToString().Trim();
    public static string ScriptResultFilePath = System.Configuration.ConfigurationManager.AppSettings["ScriptResultFilePath"].ToString().Trim();
    public static string ScriptResultFileName = System.Configuration.ConfigurationManager.AppSettings["ScriptResultFileName"].ToString().Trim();
    public static string LogPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"].ToString().Trim();
    public static string ErrorLogPath = System.Configuration.ConfigurationManager.AppSettings["ErrorLogPath"].ToString().Trim();
    public static string AttachNavigateUrlSuffix = System.Configuration.ConfigurationManager.AppSettings["AttachNavigateUrlSuffix"].ToString().Trim();
    public static string IfProduction = System.Configuration.ConfigurationManager.AppSettings["IfProduction"].ToString().Trim();

    public static string MailFrom = System.Configuration.ConfigurationManager.AppSettings["MailFrom"].ToString().Trim();
    public static string MailTo = System.Configuration.ConfigurationManager.AppSettings["MailTo"].ToString().Trim();
    public static string MailCC = System.Configuration.ConfigurationManager.AppSettings["MailCC"].ToString().Trim();
    public static string MailBcc = System.Configuration.ConfigurationManager.AppSettings["MailBcc"].ToString().Trim();
    public static string MailSubject = System.Configuration.ConfigurationManager.AppSettings["MailSubject"].ToString().Trim();

    public static string AttachPath = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            lblCurrentUser.Text = Session["UserName"].ToString();
            if (!IsPostBack)
            {
                //Waiting.Visible = false;
                //trQUL0.Attributes.Add("display", "none"); trQUL1.Attributes.Add("display", "none"); trQUL2.Attributes.Add("display", "none");
                ddlDataBind(((object)"Customer"));
                ddlCustomer.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
                ddlCustomer.SelectedValue = "0";
                ddlTesterType.Items.Add(new ListItem("-==Please Select==-", "0"));
                ddlDevice.Items.Add(new ListItem("-==Please Select==-", "0"));
                ddlStage.Items.Add(new ListItem("-==Please Select==-", "0"));
                //btn.Attributes.Add("style", "display:none");
                //this.btnUpload.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(this.btn, ""));
                //lblProgramName.Visible = false;
                //txtProgramName.Visible = false;
                fuPiggybackCheck.Visible = false;
                btnPiggybackCheck.Visible = false;
                hlPiggybackCheck.Visible = false;
                lblMessage.Visible = false;
            }
            if (ddlCustomer.SelectedValue.Trim().ToLower().Equals("qul"))
            { trQUL0.Visible = true; trQUL1.Visible = true; trQUL2.Visible = true; }
            else if (ddlCustomer.SelectedValue.Trim().ToLower().Equals("lsi") || ddlCustomer.SelectedValue.Trim().ToLower().Equals("sgs"))
            {
                lblProgramName.Visible = true;
                txtProgramName.Visible = true;
            }
            else
            { trQUL0.Visible = false; trQUL1.Visible = false; trQUL2.Visible = false; }
        }
        else { Response.Redirect("Login.aspx"); }
    }
    protected void rblReleaseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (rblReleaseType.SelectedValue.Trim())
        {
            case "1": // Folder
                lblProgramName.Visible = false;
                txtProgramName.Visible = false;
                break;
            case "2": // Partial
                lblProgramName.Text = "Folder Name:";
                lblProgramName.Visible = true;
                txtProgramName.Visible = true;
                break;
            case "3": // Single
                lblProgramName.Text = "File Name:";
                lblProgramName.Visible = true;
                txtProgramName.Visible = true;
                break;
        }
        ddlDataBind((object)"Customer");
        ddlCustomer.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
        ddlDataBind((DropDownList)this.Page.FindControl("ddlCustomer"));
        ddlTesterType.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
        ddlDataBind((DropDownList)this.Page.FindControl("ddlTesterType"));
        ddlDevice.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
        ddlDataBind((DropDownList)this.Page.FindControl("ddlDevice"));
        ddlStage.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
        //ddlDataBind((object)"ddlProgramPath");
        //ddlDataBind((object)"ddlProgramName");
        txtRemark.Text = string.Empty;
        txtProgramName.Text = string.Empty;
        txtMCNCheck.Text = string.Empty;
        txtQFPROMCheck.Text = string.Empty;
        txtGlobalOveron.Text = string.Empty;
        hlAttach.Text = "N/A";
        hlAttach.NavigateUrl = "#";
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDataBind(sender);
        ddlTesterType.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
        ddlDevice.Items.Clear();
        ddlDevice.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
        ddlStage.Items.Clear();
        ddlStage.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
    }
    protected void ddlTesterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDataBind(sender);
        ddlDevice.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
        ddlStage.Items.Clear();
        ddlStage.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
    }
    protected void ddlDevice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDataBind(sender);
        ddlStage.Items.Insert(0, new ListItem("-==Please Select==-", "0"));
    }
    protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDataBind((object)"ddlProgramPath");
        ddlDataBind((object)"ddlProgramName");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Write Data To SQL
        if (IsScriptExists())
        {
            string ID = "";
            try
            {
                Model objModel = new Model();
                objModel.ReleaseType = Convert.ToInt16(rblReleaseType.SelectedValue.Trim());
                objModel.Customer = ddlCustomer.SelectedValue.Trim();
                objModel.TesterType = ddlTesterType.SelectedValue.Trim();
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
                objModel.PiggybackCheck = ckboxPiggybackCheck.Checked == true ? "Y" : "N";
                objModel.PiggybackCheckAttach = hlPiggybackCheck.NavigateUrl.Replace(AttachNavigateUrlSuffix, "");
                objModel.OldProgram = txtOldProgram.Text.Trim();
                objModel.DeleteFlag = rblDeleteOption.SelectedValue.Trim();
                objModel.ReleasedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Get Attach Content For Customer SPR
                if (ddlCustomer.SelectedValue.Trim().ToLower().Equals("spr-sammi") && AttachPath.Trim() != "")
                {
                    ArrayList AttachContent = DBTran.AttachContents(AttachPath);
                    objModel.AttachReleaseDate = AttachContent[0].ToString();
                    objModel.AttachDevice = AttachContent[1].ToString();
                    objModel.AttachFT1ProgramFlow = AttachContent[2].ToString();
                    objModel.AttachQA1ProgramFlow = AttachContent[3].ToString();
                }
                else
                {
                    objModel.AttachReleaseDate = "";
                    objModel.AttachDevice = "";
                    objModel.AttachFT1ProgramFlow = "";
                    objModel.AttachQA1ProgramFlow = "";
                }

                Boolean bolReturn = DBTran.TPRFInfo(objModel, ref ID);
                UtilSelf.WriteFile.Log("New Created TPRF Request, ID: " + ID, LogPath);
                Response.Redirect("Index.aspx");
            }
            catch (Exception ex)
            {
                UtilSelf.WriteFile.Log("Write SQL DataBase Error:" + ex.Message, ErrorLogPath);
                ClientScript.RegisterStartupScript(this.GetType(), "Result", @"<script language='javascript'>alert('System Error! Please contract with the engineer!'); </script>");
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string requstPath = Request.Path.Trim();
        if (fuAttach.HasFile)
        {
            string fileName = fuAttach.FileName.Trim();
            string suffix = fileName.Substring(fileName.LastIndexOf(".") , fileName.Length - fileName.LastIndexOf(".") );
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
            System.Threading.Thread.Sleep(1000);
        }
    }
    protected void ckboxPiggybackCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (ckboxPiggybackCheck.Checked)
        {
            fuPiggybackCheck.Visible = true;
            btnPiggybackCheck.Visible = true;
            hlPiggybackCheck.Visible = true;
        }
        else
        {
            fuPiggybackCheck.Visible = false;
            btnPiggybackCheck.Visible = false;
            hlPiggybackCheck.Visible = false;
        }
    }
    protected void btnPiggybackCheck_Click(object sender, EventArgs e)
    {
        string requstPath = Request.Path.Trim();
        if (fuPiggybackCheck.HasFile)
        {
            string fileName = fuPiggybackCheck.FileName.Trim();
            string suffix = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
            fileName = fileName.Replace(suffix, "_" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + suffix);
            if (!System.IO.File.Exists(UploadPath + fileName))
            {
                fuPiggybackCheck.SaveAs(UploadPath + fileName);
                hlPiggybackCheck.Visible = true;
                requstPath = requstPath.Substring(0, requstPath.LastIndexOf("/")) + "/PiggybackCheck/";
                hlPiggybackCheck.Text = fileName;
                hlPiggybackCheck.Target = "_blank";
                hlPiggybackCheck.NavigateUrl = AttachNavigateUrlSuffix + requstPath + fileName;
            }
            System.Threading.Thread.Sleep(1000);
        }
    }

    protected void ddlProgramPath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDataBind((object)"ddlProgramName");
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


    #region " Private Events "

    private void ddlDataBind(object sender)
    {
        DataSet ds = new DataSet();
        if (sender.GetType().Equals(typeof(DropDownList)))
        {
            switch (((DropDownList)sender).ID.Trim().ToLower())
            {
                case "ddlcustomer":
                    ds = DBTran.ddlDataBind(DBTran.ddlSQLStatement(ddlCustomer.SelectedValue.Trim()));
                    if (ds != null)
                    {
                        ddlTesterType.DataSource = ds;
                        ddlTesterType.DataTextField = "TesterType";
                        ddlTesterType.DataValueField = "TesterType";
                        ddlTesterType.DataBind();
                    }
                    break;
                case "ddltestertype":
                    ds = DBTran.ddlDataBind(DBTran.ddlSQLStatement(ddlCustomer.SelectedValue.Trim(), ddlTesterType.SelectedValue.Trim()));
                    if (ds != null)
                    {
                        ddlDevice.DataSource = ds;
                        ddlDevice.DataTextField = "DeviceName";
                        ddlDevice.DataValueField = "DeviceName";
                        ddlDevice.DataBind();
                    }
                    break;
                case "ddldevice":
                    ds = DBTran.ddlDataBind(DBTran.ddlSQLStatement(((object)"Stage")));
                    if (ds != null)
                    {
                        ddlStage.DataSource = ds;
                        ddlStage.DataTextField = "Stage";
                        ddlStage.DataValueField = "Stage";
                        ddlStage.DataBind();
                    }
                    break;

            }
        }
        if (sender.ToString().Equals("Customer"))
        {
            ds = DBTran.ddlDataBind(DBTran.ddlSQLStatement(((object)"Customer")));
            if (ds != null)
            {
                ddlCustomer.DataSource = ds;
                ddlCustomer.DataTextField = "Customer";
                ddlCustomer.DataValueField = "Customer";
                ddlCustomer.DataBind();
            }
        }
        if (sender.ToString().Equals("ddlProgramPath"))
        {
            ds = DBTran.ddlDataBind(DBTran.ddlSQLStatement(ddlCustomer.SelectedValue.Trim(), ddlDevice.SelectedValue.Trim(), "", ddlStage.SelectedValue.Trim()));
            if (ds != null)
            {
                ddlProgramPath.DataSource = ds;
                ddlProgramPath.DataTextField = "ProgramPath";
                ddlProgramPath.DataValueField = "ProgramPath";
                ddlProgramPath.DataBind();
                txtProgramName.Text = ds.Tables[0].Rows[0][1].ToString().Trim();
               txtProgramRevision.Text = ds.Tables[0].Rows[0][2].ToString().Trim();
            }
        }
    }

    //private Boolean IsScriptExists()
    //{
    //    bool bol = false;
    //    try
    //    {
    //        FTPNEw ftp = new FTPNEw();
    //        ftp.FtpUpDown(Host, Userr, Password);
    //        string[] list = ftp.GetFileList(ScriptPath.ToString());

    //        if (list != null)
    //        {
    //            foreach (string file in list)
    //            {
    //                if (file == ScriptName.Replace("{*}", ddlCustomer.SelectedValue.Trim()))
    //                {
    //                    bol = true;
    //                }
    //            }
    //        }

    //        switch (bol)
    //        {
    //            case true:
    //                lblMessage.Visible = false;
    //                break;
    //            case false:
    //                lblMessage.Text = "The program can not be AUTOMATICALLY Released for now, please contact with TPAG group.";
    //                lblMessage.Visible = true;
    //                break;
    //        }
    //    }
    //    catch (Exception ex)
    //    { UtilSelf.WriteFile.Log("Check If Script Exists Error:" + ex.Message, ErrorLogPath); }
    //    return bol;
    //}
    private Boolean IsScriptExists()
    {
        bool bol = false;
        try
        {
            // 使用SFTP客户端替代FTP客户端
            using (var sftp = new SftpHelper(Host, 22, Userr, Password))
            {
                // 获取SFTP服务器上指定路径的文件列表
                string[] list = sftp.GetFileList(ScriptPath.ToString());

                if (list != null)
                {
                    // 构建目标文件名（替换占位符）
                    string targetFileName = ScriptName.Replace("{*}", ddlCustomer.SelectedValue.Trim());

                    // 检查文件是否存在
                    foreach (string file in list)
                    {
                        if (file == targetFileName)
                        {
                            bol = true;
                            break; // 找到后可以提前退出循环
                        }
                    }
                }
            }

            // 根据检查结果更新UI
            if (bol)
            {
                lblMessage.Visible = false;
            }
            else
            {
                lblMessage.Text = "The program can not be AUTOMATICALLY Released for now, please contact with TPAG group.";
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            UtilSelf.WriteFile.Log("Check If Script Exists Error:" + ex.Message, ErrorLogPath);
        }
        return bol;
    }
    #endregion
}