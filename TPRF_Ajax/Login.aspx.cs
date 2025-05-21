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
using System.Drawing;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { txtUserName.Focus(); }
        lblMessage.Visible = false;
    }
    protected void btn_Click(object sender, CommandEventArgs e)
    {
        string btnID = ((Button)sender).ID.Trim();
        switch (btnID.Trim().ToLower())
        {
            case "btnlogin":
                object obj = DBTran.IsLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                if (obj != null && obj.GetType().Equals(typeof(DataSet)))
                {
                    GetSession((DataSet)obj);
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    if (obj != null)
                    {
                        switch (obj.ToString().Trim())
                        {
                            case "-1":
                                lblMessage.Text = "Wrong Password!";
                                break;
                            case "0":
                                lblMessage.Text = "UserName Not Exists!";
                                break;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "An error occurred during login. Please try again later.";
                    }
                }
                break;
            case "btnexit":
                //Response.Write("<script language=javascript>window.opener=null;window.close();</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language='javascript'>window.opener=null;window.close();</script>");
                break;
        }
    }
    private void GetSession(DataSet ds)
    {
        Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
        Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
        Session["Password"] = ds.Tables[0].Rows[0]["UserPwd"].ToString();
        Session["Name"] = ds.Tables[0].Rows[0]["ChineseName"].ToString();
        Session["Shift"] = ds.Tables[0].Rows[0]["UserShift"].ToString();
        Session["RoleFlag"] = ds.Tables[0].Rows[0]["UserRole"].ToString();
        Session["Title"] = ds.Tables[0].Rows[0]["UserTitle"].ToString();
        Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
        Session["Role"] = ds.Tables[0].Rows[0]["RoleName"].ToString();
        Session["GroupID"] = ds.Tables[0].Rows[0]["UserGroup"].ToString();
        Session["Group"] = ds.Tables[0].Rows[0]["GroupName"].ToString();
    }
}

//public partial class Login : System.Web.UI.Page
//{
//    // 页面加载事件处理方法
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        // 判断是否为首次加载页面
//        if (!IsPostBack)
//        {
//            // 若为首次加载，将焦点设置到用户名输入框
//            txtUserName.Focus();
//        }
//        // 隐藏消息提示标签
//        lblMessage.Visible = false;
//    }

//    // 按钮点击事件处理方法
//    protected void btn_Click(object sender, CommandEventArgs e)
//    {
//        // 获取点击按钮的 ID 并去除首尾空格
//        string btnID = ((Button)sender).ID.Trim();
//        // 根据按钮 ID 进行不同操作
//        switch (btnID.Trim().ToLower())
//        {
//            case "btnlogin":
//                // 调用 IsLogin 方法，传入用户名和密码，判断是否登录成功
//                object obj = IsLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
//                // 判断返回对象的类型是否为 DataSet
//                if (obj.GetType().Equals(typeof(DataSet)))
//                {
//                    // 若为 DataSet 类型，调用 GetSession 方法将用户信息存入会话
//                    GetSession((DataSet)obj);
//                    // 重定向到 Home.aspx 页面
//                    Response.Redirect("Home.aspx");
//                }
//                else
//                {
//                    // 若不是 DataSet 类型，显示消息提示标签
//                    lblMessage.Visible = true;
//                    // 设置消息提示标签的文字颜色为红色
//                    lblMessage.ForeColor = Color.Red;
//                    // 根据返回对象的字符串值进行不同提示
//                    switch (obj.ToString().Trim())
//                    {
//                        case "-1":
//                            // 密码错误提示
//                            lblMessage.Text = "Wrong Password!";
//                            break;
//                        case "0":
//                            // 用户名不存在提示
//                            lblMessage.Text = "UserName Not Exists!";
//                            break;
//                    }
//                }
//                break;
//            case "btnexit":
//                // 注册一段 JavaScript 脚本，用于关闭当前窗口
//                ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language='javascript'>window.opener=null;window.close();</script>");
//                break;
//        }
//    }

//    // 将用户信息存入会话的方法
//    private void GetSession(DataSet ds)
//    {
//        // 将 DataSet 中用户信息存入会话
//        Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
//        Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
//        Session["Password"] = ds.Tables[0].Rows[0]["UserPwd"].ToString();
//        Session["Name"] = ds.Tables[0].Rows[0]["ChineseName"].ToString();
//        Session["Shift"] = ds.Tables[0].Rows[0]["UserShift"].ToString();
//        Session["RoleFlag"] = ds.Tables[0].Rows[0]["UserRole"].ToString();
//        Session["Title"] = ds.Tables[0].Rows[0]["UserTitle"].ToString();
//        Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
//        Session["Role"] = ds.Tables[0].Rows[0]["RoleName"].ToString();
//        Session["GroupID"] = ds.Tables[0].Rows[0]["UserGroup"].ToString();
//        Session["Group"] = ds.Tables[0].Rows[0]["GroupName"].ToString();
//    }

//    // 硬编码用户信息并验证登录的方法
//    public static object IsLogin(string UserName, string Password)
//    {
//        // 硬编码用户信息
//        DataSet ds = new DataSet();
//        DataTable dt = new DataTable("UserInfo");
//        dt.Columns.Add("UserID", typeof(string));
//        dt.Columns.Add("UserName", typeof(string));
//        dt.Columns.Add("UserPwd", typeof(string));
//        dt.Columns.Add("ChineseName", typeof(string));
//        dt.Columns.Add("UserShift", typeof(string));
//        dt.Columns.Add("UserRole", typeof(string));
//        dt.Columns.Add("UserTitle", typeof(string));
//        dt.Columns.Add("Email", typeof(string));
//        dt.Columns.Add("RoleName", typeof(string));
//        dt.Columns.Add("UserGroup", typeof(string));
//        dt.Columns.Add("GroupName", typeof(string));

//        // 这里添加一个示例用户信息
//        dt.Rows.Add("1", "testuser", "88888888", "测试用户", "白班", "普通用户", "员工", "test@example.com", "普通角色", "1", "测试组");
//        ds.Tables.Add(dt);

//        // 查找匹配的用户名
//        string filter = "UserName = '" + UserName + "'";
//        DataRow[] rows = dt.Select(filter);
//        if (rows.Length == 0)
//        {
//            return "0"; // 用户名不存在
//        }
//        DataRow userRow = rows[0];
//        if (userRow["UserPwd"].ToString() != Password)
//        {
//            return "-1"; // 密码错误
//        }
//        return ds; // 登录成功，返回用户信息的 DataSet
//    }
//}