using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using Oracle.ManagedDataAccess.Client;


/// <summary>
/// DBTran 的摘要说明
/// </summary>
public class DBTran
{
    public DBTran()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    //private static string TPRFDBConnection = System.Configuration.ConfigurationManager.AppSettings["TPRFDBConnection"].ToString().Trim();
    private static string TPRFConnection = System.Configuration.ConfigurationManager.AppSettings["TPRFConnection"].ToString().Trim();
    private static string eTIDBConnection = System.Configuration.ConfigurationManager.AppSettings["eTIDBConnection"].ToString().Trim();
    private static string ErrorLogPath = System.Configuration.ConfigurationManager.AppSettings["ErrorLogPath"].ToString().Trim();

    public static object IsLogin(string UserName, string Password)
    {
        object obj = null;
        using (OracleConnection conn = new OracleConnection(TPRFConnection))
        {
            using (OracleCommand cmd = new OracleCommand("TPRFISLOGIN", conn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] myParas = new OracleParameter[] {
                        new OracleParameter("p_UserName", UserName),
                        new OracleParameter("p_Password", Password),
                        new OracleParameter("ReturnInt", OracleDbType.Int16)
                    };
                    myParas[0].Direction = ParameterDirection.Input;
                    myParas[1].Direction = ParameterDirection.Input;
                    myParas[2].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddRange(myParas);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    switch (myParas[2].Value.ToString().Trim())
                    {
                        case "0": // Account Not Exists
                            obj = "0";
                            break;
                        case "-1": // Password Wrong 
                            obj = "-1";
                            break;
                        default:
                            obj = LoginInfo(UserName, Password);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // UtilSelf.WriteFile.Log("IsLogin Error: " + ex.Message, ErrorLogPath);
                    Console.WriteLine("IsLogin Error: " + ex.Message); // 这里简单打印错误信息，你可以根据实际情况处理
                    obj = null;
                }
            }
        }
        return obj;
    }

    public static DataSet LoginInfo(string UserName, string Password)
    {
        DataSet ds = new DataSet();
        using (OracleConnection conn = new OracleConnection(TPRFConnection))
        {
            using (OracleCommand cmd = new OracleCommand("TPRFLOGININFO", conn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter[] myParas = new OracleParameter[] {
                        new OracleParameter("p_UserName", UserName),
                        new OracleParameter("p_Password", Password),
                        new OracleParameter("V_CS", OracleDbType.RefCursor)
                    };
                    myParas[0].Direction = ParameterDirection.Input;
                    myParas[1].Direction = ParameterDirection.Input;
                    myParas[2].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddRange(myParas);

                    conn.Open();
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    oda.Fill(ds);
                }
                catch (Exception ex)
                {
                    // UtilSelf.WriteFile.Log("LoginInfo Error: " + ex.Message, ErrorLogPath);
                    Console.WriteLine("LoginInfo Error: " + ex.Message); // 这里简单打印错误信息，你可以根据实际情况处理
                    ds = null;
                }
            }
        }
        return ds;
    }

    public static string ddlSQLStatement(object Flag)
    {
        string strSQL = "";
        switch (Flag.ToString().Trim().ToLower())
        {
            case "customer":
                strSQL = "select * from customer(nolock) order by customer";
                break;
            case "stage":
                strSQL = "select replace(name,'programname_','') Stage from syscolumns where id=object_id('productinfo') and name like 'programname%' order by name";
                break;
        }
        return strSQL;
    }

    public static string ddlSQLStatement(string Customer)
    {
        return "select distinct tester_ft1 TesterType from productinfo where tester_ft1!='N/A' and customer='" + Customer + "' order by tester_ft1";
    }

    public static string ddlSQLStatement(string Customer, string TesterType)
    {
        return "select distinct DeviceName from productinfo where customer='" + Customer + "' and tester_ft1='" + TesterType + "' order by devicename";
    }

    public static string ddlSQLStatement(string Customer, string DeviceName, string ProgramPath, string Stage)
    {
        string strSQL = "";
        switch (ProgramPath)
        {
            case "":
                strSQL = "select distinct programdirectory_" + Stage + " ProgramPath,programname_" + Stage + " ProgramName,programrevision_" + Stage + " ProgramRevision from productinfo(nolock) where customer='" + Customer + "' and devicename='" + DeviceName + "'";
                break;
            default:
                strSQL = "select distinct programname_" + Stage + " ProgramName,programrevision_" + Stage + " ProgramRevision from productinfo(nolock) where customer='" + Customer + "' and devicename='" + DeviceName + "' and Programdirectory_ft1='" + ProgramPath + "'";
                break;
        }
        return strSQL;
    }

    public static DataSet ddlDataBind(string SQLStatement)
    {
        DataSet ds = new DataSet();
        UtilSelf.SQLHelp sql = new UtilSelf.SQLHelp();
        sql.ConnectionStr = eTIDBConnection;
        try
        {
            sql.OpenConnection();
            sql.SelectCommand.CommandText = SQLStatement;
            sql.SelectCommand.CommandType = CommandType.Text;
            ds = sql.QueryData();
        }
        catch (Exception ex)
        { UtilSelf.WriteFile.Log(ex.Message, ErrorLogPath); ds = null; }
        finally
        { sql.CloseConnection(); sql = null; GC.Collect(); }
        return ds;
    }

    public static Boolean TPRFInfo(Model objModel, ref string ID)
    {
        UtilSelf.OracleHelp orl = new UtilSelf.OracleHelp();
        orl.ConnectionStr = TPRFConnection;
        try
        {
            orl.OpenConnection();
            orl.Command.CommandText = "InsertTPRFInfo";
            orl.Command.CommandType = CommandType.StoredProcedure;
            OracleParameter[] myParas = new OracleParameter[]{  new OracleParameter("p_ReleaseType",objModel.ReleaseType),
                                                        new OracleParameter("p_Customer",objModel.Customer),
                                                        new OracleParameter("p_TesterType",objModel.TesterType),     
                                                        new OracleParameter("p_Device",objModel.Device),
                                                        new OracleParameter("p_Stage",objModel.Stage),
                                                        new OracleParameter("p_ProgramPath",objModel.ProgramPath),
                                                        new OracleParameter("p_ProgramName",objModel.ProgramName),
                                                        new OracleParameter("p_ProgramRevision",objModel.ProgramRevision),
                                                        new OracleParameter("p_Action",objModel.Action),
                                                        new OracleParameter("p_PiggybackCheck",objModel.PiggybackCheck),
                                                        new OracleParameter("p_PiggybackCheckAttach",objModel.PiggybackCheckAttach),
                                                        new OracleParameter("p_Attachment",objModel.Attachment),
                                                        new OracleParameter("p_AttachReleaseDate",objModel.AttachReleaseDate),
                                                        new OracleParameter("p_AttachDevice",objModel.AttachDevice),
                                                        new OracleParameter("p_AttachFT1ProgramFlow",objModel.AttachFT1ProgramFlow),
                                                        new OracleParameter("p_AttachQA1ProgramFlow",objModel.AttachQA1ProgramFlow),
                                                        new OracleParameter("p_RemarkForCustomer0",objModel.RemarkForCustomer0),
                                                        new OracleParameter("p_RemarkForCustomer1",objModel.RemarkForCustomer1),
                                                        new OracleParameter("p_RemarkForCustomer2",objModel.RemarkForCustomer2),
                                                        new OracleParameter("p_Remark",objModel.Remark),
                                                        new OracleParameter("p_SubmitUser",objModel.SubmitUser),
                                                        new OracleParameter("p_AttachFT1Program",objModel.AttachFT1Program),
                                                        new OracleParameter("p_AttachQA1Program",objModel.AttachQA1Program),
                                                        new OracleParameter("p_AttachDeviceVersion",objModel.AttachDeviceVersion),
                                                        new OracleParameter("p_OldProgram",objModel.OldProgram),
                                                        new OracleParameter("p_DeleteFlag",objModel.DeleteFlag),
                                                        new OracleParameter("p_ReleasedTime",objModel.ReleasedTime),
                                                        new OracleParameter("ReturnInt",System.Data.OracleClient.OracleType.Int16)
                                                    };
            myParas[26].Direction = ParameterDirection.Output;
            orl.Command.Parameters.AddRange(myParas);
            orl.Command.ExecuteNonQuery();
            ID = myParas[25].Value.ToString().Trim();
            return true;
        }
        catch (Exception ex)
        { UtilSelf.WriteFile.Log(ex.Message, ErrorLogPath); return false; }
        finally
        { orl.CloseConnection(); orl = null; GC.Collect(); }
    }

    public static Boolean UpdateStatus(string ID)
    {
        UtilSelf.OracleHelp orl = new UtilSelf.OracleHelp();
        orl.ConnectionStr = TPRFConnection;
        try
        {
            orl.OpenConnection();
            orl.Command.CommandText = "update TPRFInfo set status=1,releasedtime=to_char(sysdate,'yyyy-mm-dd hh24:mi:ss') where ID=" + ID;
            orl.Command.CommandType = CommandType.Text;
            orl.Command.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        { UtilSelf.WriteFile.Log("Update Status Error: " + ex.Message, ErrorLogPath); return false; }
        finally
        { orl.CloseConnection(); orl = null; GC.Collect(); }
    }

    public static DataSet TPRFList()
    {
        using (OracleConnection conn = new OracleConnection(TPRFConnection))
        {
            using (OracleCommand cmd = new OracleCommand("TPRFInfoList", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter[] myParas = new OracleParameter[] {
                new OracleParameter("p_ID", Oracle.ManagedDataAccess.Client.OracleDbType.Int16) { Value = 0, Direction = ParameterDirection.Input },
                new OracleParameter("V_CS", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor) { Direction = ParameterDirection.Output } // 或 OracleType.Cursor
            };
                cmd.Parameters.AddRange(myParas);

                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    OracleDataAdapter myAdapter = new OracleDataAdapter(cmd);
                    myAdapter.Fill(ds);
                    return ds;
                }
                catch (OracleException ex)
                {
                    UtilSelf.WriteFile.Log("TPRFList Error: " + ex.Message, ErrorLogPath);
                    throw new Exception("执行存储过程失败: {ex.Message}");
                }
            }
        }
    }

    public static DataSet TPRFDetail(string ID)
    {
        //Model objModel = new Model();
        DataSet ds = new DataSet();
        UtilSelf.OracleHelp orl = new UtilSelf.OracleHelp();
        orl.ConnectionStr = TPRFConnection;
        try
        {
            orl.OpenConnection();
            orl.SelectCommand.CommandText = "TPRFInfoList";
            orl.SelectCommand.CommandType = CommandType.StoredProcedure;
            OracleParameter[] myParas = new OracleParameter[] { 
                //new OracleParameter("p_ID",Convert.ToInt16(ID)),
                  new OracleParameter("p_ID",Convert.ToInt16(ID)),
                new OracleParameter("V_CS", System.Data.OracleClient.OracleType.Cursor)
            };
            //myParas[0].Direction = ParameterDirection.Input;
            //myParas[0].Direction = ParameterDirection.Output;
            myParas[0].Direction = ParameterDirection.Input;
            myParas[1].Direction = ParameterDirection.Output;
            orl.SelectCommand.Parameters.AddRange(myParas);

            IDbDataAdapter oda = new System.Data.OracleClient.OracleDataAdapter(orl.SelectCommand as System.Data.OracleClient.OracleCommand);
            oda.Fill(ds);
        }
        catch (Exception ex)
        { UtilSelf.WriteFile.Log("TPRFDetail Error: " + ex.Message, ErrorLogPath); ds = null; }
        finally
        { orl.CloseConnection(); orl = null; GC.Collect(); }
        return ds;
    }

    public static DataSet TPRFDirectorys()
    {
        string strSQL = "select * from SPR_Directory order by Directorys";
        DataSet ds = new DataSet();
        UtilSelf.OracleHelp orl = new UtilSelf.OracleHelp();
        orl.ConnectionStr = TPRFConnection;
        try
        {
            orl.OpenConnection();
            orl.SelectCommand.CommandText = strSQL;
            orl.SelectCommand.CommandType = CommandType.Text;
            IDbDataAdapter oda = new System.Data.OracleClient.OracleDataAdapter(orl.SelectCommand as System.Data.OracleClient.OracleCommand);
            oda.Fill(ds);
        }
        catch (Exception ex)
        { UtilSelf.WriteFile.Log("TPRFDirectorys Error: " + ex.Message, ErrorLogPath); ds = null; }
        finally
        { orl.CloseConnection(); orl = null; GC.Collect(); }
        return ds;
    }

    public static ArrayList AttachContents(string ExcelPath, string TestType, string Device)
    {
        ArrayList Attach = new ArrayList();
        ArrayList Sheets = new ArrayList();
        string OLEDBConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelPath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
        System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(OLEDBConn);
        try
        {
            Conn.Open();
            DataTable dt = Conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            foreach (DataRow dr in dt.Rows)
            {
                Sheets.Add(dr[2]);
                if (!dr[2].ToString().Trim().ToLower().Contains("rev history") && !dr[2].ToString().Trim().ToLower().Contains("sum"))
                {
                    string strSheet = dr[2].ToString().Trim();
                    strSheet = strSheet.Substring(0, 1) == "'" ? strSheet.Substring(1, strSheet.Length - 1) : strSheet;
                    strSheet = strSheet.Substring(strSheet.Length - 1, 1) == "'" ? strSheet.Substring(0, strSheet.Length - 1) : strSheet;
                    System.Data.OleDb.OleDbDataAdapter odaSingle = new System.Data.OleDb.OleDbDataAdapter("select * from [" + strSheet + "]", Conn);
                    DataSet dsSingle = new DataSet();
                    odaSingle.Fill(dsSingle);
                    if (TestType != "")
                    {
                        if (strSheet.Trim().Contains(TestType))
                        {
                            if (dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim().Contains(Device))
                            {
                                Attach.Add(dsSingle.Tables[0].Rows[3]["F23"].ToString().Trim());  // Attach Release Date
                                Attach.Add(dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim());  // Attach Device
                                Attach.Add(dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[6]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim());  // Attach FT1 Program Flow Name
                                Attach.Add(dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[8]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim());  // Attach QA1 Program Flow Name
                            }
                        }
                    }
                    else
                    {
                        if (!strSheet.Trim().Contains("800"))
                        {
                            if (dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim().Contains(Device))
                            {
                                Attach.Add(dsSingle.Tables[0].Rows[3]["F23"].ToString().Trim());  // Attach Release Date
                                Attach.Add(dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim());  // Attach Device
                                Attach.Add(dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[6]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim());  // Attach FT1 Program Flow Name
                                Attach.Add(dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[8]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim());  // Attach QA1 Program Flow Name
                            }
                        }
                    }
                }
            }

            //string SheetName = Sheets[0].ToString().Trim();
            //SheetName = SheetName.Substring(0, 1) == "'" ? SheetName.Substring(1, SheetName.Length - 1) : SheetName;
            //SheetName = SheetName.Substring(SheetName.Length - 1, 1) == "'" ? SheetName.Substring(0, SheetName.Length - 1) : SheetName;
            //OleDbDataAdapter oda = new OleDbDataAdapter("select * from [" + SheetName + "]", Conn);
            //DataSet ds = new DataSet();
            //oda.Fill(ds);
            //if (ds != null)
            //{
            //    Attach.Add(ds.Tables[0].Rows[3]["F23"].ToString().Trim());  // Attach Release Date
            //    Attach.Add(ds.Tables[0].Rows[4]["F8"].ToString().Trim());  // Attach Device
            //    Attach.Add(ds.Tables[0].Rows[6]["F11"].ToString().Trim() == "" ? ds.Tables[0].Rows[6]["F8"].ToString().Trim() : ds.Tables[0].Rows[6]["F11"].ToString().Trim());  // Attach FT1 Program Flow Name
            //    Attach.Add(ds.Tables[0].Rows[8]["F11"].ToString().Trim() == "" ? ds.Tables[0].Rows[8]["F8"].ToString().Trim() : ds.Tables[0].Rows[8]["F11"].ToString().Trim());  // Attach QA1 Program Flow Name
            //}

        }
        catch (Exception ex)
        {
            UtilSelf.WriteFile.Log("Get Attach Content Error: " + ex.Message, ErrorLogPath);
        }
        finally
        {
            Conn.Close();
            Conn = null;
            Sheets.Clear();
            Sheets = null;
            GC.Collect();
        }
        return Attach;
    }

    public static ArrayList AttachContents(string ExcelPath)
    {
        ArrayList Attachs = new ArrayList();
        string OLEDBConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelPath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
        System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(OLEDBConn);
        try
        {
            Conn.Open();
            DataTable dt = Conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            foreach (DataRow dr in dt.Rows)
            {
                ArrayList Attach;
                if (!dr[2].ToString().Trim().ToLower().Contains("rev history") && !dr[2].ToString().Trim().ToLower().Contains("sum")
                    && !dr[2].ToString().Trim().ToLower().Contains("print_area"))
                {
                    string strSheet = dr[2].ToString().Trim();
                    strSheet = strSheet.Substring(0, 1) == "'" ? strSheet.Substring(1, strSheet.Length - 1) : strSheet;
                    strSheet = strSheet.Substring(strSheet.Length - 1, 1) == "'" ? strSheet.Substring(0, strSheet.Length - 1) : strSheet;
                    System.Data.OleDb.OleDbDataAdapter odaSingle = new System.Data.OleDb.OleDbDataAdapter("select * from [" + strSheet + "]", Conn);
                    DataSet dsSingle = new DataSet();
                    odaSingle.Fill(dsSingle);

                    if (dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim() != "")
                    {
                        if (dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim().Contains(",")
                            || dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim().Contains("，")
                            || dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim().Contains("/"))
                        {
                            string strDevice = dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim().Replace("，", ",");
                            strDevice = strDevice.Replace("/", ",");
                            string[] strDevices = strDevice.Split(Convert.ToChar(","));
                            string strDeviceSuffix = string.Empty;
                            if (strDevices[0].ToString().Trim().Contains("_"))
                            {
                                strDeviceSuffix = strDevices[0].ToString().Trim().Substring(0, (strDevices[0].ToString().Trim().IndexOf("_")) + 1);
                            }
                            foreach (string s in strDevices)
                            {
                                Attach = new ArrayList();
                                //if (s.Length < 3) { s = strDevice + s; }
                                Attach.Add(dsSingle.Tables[0].Rows[3]["F23"].ToString().Trim());  // Attach Release Date
                                //Attach.Add(s.ToString().Trim());  // Attach Device
                                Attach.Add(s.Length < 3 ? (strDeviceSuffix + s.ToString().Trim()) : s.ToString().Trim());  // Attach Device
                                Attach.Add(dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[6]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim());  // Attach FT1 Program Flow Name
                                Attach.Add(dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[8]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim());  // Attach QA1 Program Flow Name
                                Attach.Add(dsSingle.Tables[0].Rows[5]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[5]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[5]["F11"].ToString().Trim());  // Attach FT1 Program Name
                                Attach.Add(dsSingle.Tables[0].Rows[7]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[7]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[7]["F11"].ToString().Trim());  // Attach QA1 Program Name
                                Attach.Add(dsSingle.Tables[0].Rows[4]["F23"].ToString().Trim());  // Attach Device Version
                                Attachs.Add(Attach);
                            }
                        }
                        else
                        {
                            Attach = new ArrayList();
                            Attach.Add(dsSingle.Tables[0].Rows[3]["F23"].ToString().Trim());  // Attach Release Date
                            Attach.Add(dsSingle.Tables[0].Rows[4]["F8"].ToString().Trim());  // Attach Device
                            Attach.Add(dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[6]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[6]["F11"].ToString().Trim());  // Attach FT1 Program Flow Name
                            Attach.Add(dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[8]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[8]["F11"].ToString().Trim());  // Attach QA1 Program Flow Name
                            Attach.Add(dsSingle.Tables[0].Rows[5]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[5]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[5]["F11"].ToString().Trim());  // Attach FT1 Program Name
                            Attach.Add(dsSingle.Tables[0].Rows[7]["F11"].ToString().Trim() == "" ? dsSingle.Tables[0].Rows[7]["F8"].ToString().Trim() : dsSingle.Tables[0].Rows[7]["F11"].ToString().Trim());  // Attach QA1 Program Name
                            Attach.Add(dsSingle.Tables[0].Rows[4]["F23"].ToString().Trim());  // Attach Device Version
                            Attachs.Add(Attach);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UtilSelf.WriteFile.Log("Get Attachs Content Error: " + ex.Message, ErrorLogPath);
        }
        finally
        {
            Conn.Close();
            Conn = null;
            GC.Collect();
        }
        return Attachs;
    }

    public static void InsertDirectorys(string strSQL)
    {
        UtilSelf.OracleHelp orl = new UtilSelf.OracleHelp();
        orl.ConnectionStr = TPRFConnection;
        try
        {
            orl.OpenConnection();
            orl.Command.CommandText = strSQL;
            orl.Command.CommandType = CommandType.Text;
            orl.Command.ExecuteNonQuery();
        }
        catch (Exception ex)
        { UtilSelf.WriteFile.Log("InsertDirectorys Error: " + ex.Message, ErrorLogPath); }
        finally
        { orl.CloseConnection(); orl = null; GC.Collect(); }
    }

    public static void BuyOff(string strID)
    {
        using (OracleConnection conn = new OracleConnection(TPRFConnection))
        {
            using (OracleCommand cmd = new OracleCommand("UPDATE tprfinfo SET Status = 3 WHERE ID = :ID", conn))
            {
                try
                {
                    // 设置参数化查询，防止SQL注入
                    cmd.Parameters.Add(":ID", OracleDbType.Varchar2).Value = strID;

                    // 打开连接并执行更新
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    UtilSelf.WriteFile.Log("BuyOff Error: " + ex.Message, ErrorLogPath);
                }
            }
        }
    }
}

    public class Model
{
    private int _id, _releasetype, _status;
    private string _customer, _testertype, _device, _stage, _programpath, _programname, _action, _attachment, _remarkforcustomer0, _remarkforcustomer1,
        _remarkforcustomer2, _remark, _submituser, _submittime, _releasedtime, _piggybackcheck, _piggybackcheckattach;
    private string _attachdevice, _attachreleasedate, _attachft1programflow, _attachqa1programflow, _attachft1program, _attachqa1program, _attachdeviceversion;
    private string _programrevision, _oldprogram, _deleteflag;

    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    public int ReleaseType
    {
        set { _releasetype = value; }
        get { return _releasetype; }
    }
    public string Customer
    {
        set { _customer = value; }
        get { return _customer; }
    }
    public string TesterType
    {
        set { _testertype = value; }
        get { return _testertype; }
    }
    public string Device
    {
        set { _device = value; }
        get { return _device; }
    }
    public string Stage
    {
        set { _stage = value; }
        get { return _stage; }
    }
    public string ProgramPath
    {
        set { _programpath = value; }
        get { return _programpath; }
    }
    public string ProgramName
    {
        set { _programname = value; }
        get { return _programname; }
    }
    public string Action
    {
        set { _action = value; }
        get { return _action; }
    }
    public string Attachment
    {
        set { _attachment = value; }
        get { return _attachment; }
    }
    public string RemarkForCustomer0
    {
        set { _remarkforcustomer0 = value; }
        get { return _remarkforcustomer0; }
    }
    public string RemarkForCustomer1
    {
        set { _remarkforcustomer1 = value; }
        get { return _remarkforcustomer1; }
    }
    public string RemarkForCustomer2
    {
        set { _remarkforcustomer2 = value; }
        get { return _remarkforcustomer2; }
    }
    public string Remark
    {
        set { _remark = value; }
        get { return _remark; }
    }
    public string SubmitUser
    {
        set { _submituser = value; }
        get { return _submituser; }
    }
    public string SubmitTime
    {
        set { _submittime = value; }
        get { return _submittime; }
    }
    public int Status
    {
        set { _status = value; }
        get { return _status; }
    }
    public string ReleasedTime
    {
        set { _releasedtime = value; }
        get { return _releasedtime; }
    }

    public string AttachDevice
    {
        set { _attachdevice = value; }
        get { return _attachdevice; }
    }
    public string AttachReleaseDate
    {
        set { _attachreleasedate = value; }
        get { return _attachreleasedate; }
    }
    public string AttachFT1ProgramFlow
    {
        set { _attachft1programflow = value; }
        get { return _attachft1programflow; }
    }
    public string AttachQA1ProgramFlow
    {
        set { _attachqa1programflow = value; }
        get { return _attachqa1programflow; }
    }
    public string PiggybackCheck
    {
        get { return _piggybackcheck; }
        set { _piggybackcheck = value; }
    }
    public string PiggybackCheckAttach
    {
        get { return _piggybackcheckattach; }
        set { _piggybackcheckattach = value; }
    }
    public string AttachFT1Program
    {
        set { _attachft1program = value; }
        get { return _attachft1program; }
    }
    public string AttachQA1Program
    {
        set { _attachqa1program = value; }
        get { return _attachqa1program; }
    }
    public string AttachDeviceVersion
    {
        set { _attachdeviceversion = value; }
        get { return _attachdeviceversion; }
    }
    public string ProgramRevision
    {
        set { _programrevision = value; }
        get { return _programrevision; }
    }
    public string OldProgram
    {
        set { _oldprogram = value; }
        get { return _oldprogram; }
    }
    public string DeleteFlag
    {
        set { _deleteflag = value; }
        get { return _deleteflag; }
    }
}