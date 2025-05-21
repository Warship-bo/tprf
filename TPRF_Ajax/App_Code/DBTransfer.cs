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
using System.Data.OleDb;
using Oracle.ManagedDataAccess.Client;
/// <summary>
/// DBTransfer 的摘要说明
/// </summary>
public class DBTransfer
{
    public DBTransfer()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    //private static string TPRFDBConnection = System.Configuration.ConfigurationManager.AppSettings["TPRFDBConnection"].ToString().Trim();
    private static string TPRFConnection = System.Configuration.ConfigurationManager.AppSettings["TPRFConnection"].ToString().Trim();
    private static string eTIDBConnection = System.Configuration.ConfigurationManager.AppSettings["eTIDBConnection"].ToString().Trim();
    private static string ErrorLogPath = System.Configuration.ConfigurationManager.AppSettings["ErrorLogPath"].ToString().Trim();

    public static DataSet getDataSet(string SQLStatement)
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
    public static DataSet getOraDataSet(string SQLStatement)
    {
        using (OracleConnection conn = new OracleConnection(TPRFConnection))
        {
            using (OracleCommand cmd = new OracleCommand(SQLStatement, conn))
            {
                try
                {
                    conn.Open();//打开数据源连接
                    DataSet ds = new DataSet();
                    OracleDataAdapter myAdapter = new OracleDataAdapter(cmd);
                    myAdapter.Fill(ds);
                    return ds;
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    conn.Close();//出异常,关闭数据源连接
                    throw new Exception(string.Format("执行{0}失败:{1}", SQLStatement, ex.Message));
                }
            }
        }
    }
    public static DataSet getEtiOraDataSet(string SQLStatement)
    {
        using (OracleConnection conn = new OracleConnection(eTIDBConnection))
        {
            using (OracleCommand cmd = new OracleCommand(SQLStatement, conn))
            {
                try
                {
                    conn.Open();//打开数据源连接
                    DataSet ds = new DataSet();
                    OracleDataAdapter myAdapter = new OracleDataAdapter(cmd);
                    myAdapter.Fill(ds);
                    return ds;
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    conn.Close();//出异常,关闭数据源连接
                    throw new Exception(string.Format("执行{0}失败:{1}", SQLStatement, ex.Message));
                }
            }
        }
    }
    public static Boolean TPRFInfo(Modeler objModel, ref string ID)
    {
        using (OracleConnection conn = new OracleConnection(TPRFConnection))
        {
            using (OracleCommand cmd = new OracleCommand("InsertTPRFInfo", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter[] myParas = new OracleParameter[]{
                new OracleParameter("p_ReleaseType", objModel.ReleaseType),
                 new OracleParameter("p_ReleasedTime", objModel.ReleasedTime),
                new OracleParameter("p_Stasus", objModel.Status),
                new OracleParameter("p_Customer", objModel.Customer ?? (object)DBNull.Value),
                new OracleParameter("p_TesterType", objModel.TesterType ?? (object)DBNull.Value),
                new OracleParameter("p_Device", objModel.Device ?? (object)DBNull.Value),
                new OracleParameter("p_Stage", objModel.Stage ?? (object)DBNull.Value),
                new OracleParameter("p_ProgramPath", objModel.ProgramPath ?? (object)DBNull.Value),
                new OracleParameter("p_ProgramName", objModel.ProgramName ?? (object)DBNull.Value),
                new OracleParameter("p_ProgramRevision", objModel.ProgramRevision ?? (object)DBNull.Value),
                new OracleParameter("p_Action", objModel.Action ?? (object)DBNull.Value),
                new OracleParameter("p_PiggybackCheck", objModel.PiggybackCheck ?? (object)DBNull.Value),
                new OracleParameter("p_PiggybackCheckAttach", objModel.PiggybackCheckAttach ?? (object)DBNull.Value),
                new OracleParameter("p_Attachment", objModel.Attachment ?? (object)DBNull.Value),
                new OracleParameter("p_AttachReleaseDate", objModel.AttachReleaseDate ?? (object)DBNull.Value),
                new OracleParameter("p_AttachDevice", objModel.AttachDevice ?? (object)DBNull.Value),
                new OracleParameter("p_AttachFT1ProgramFlow", objModel.AttachFT1ProgramFlow ?? (object)DBNull.Value),
                new OracleParameter("p_AttachQA1ProgramFlow", objModel.AttachQA1ProgramFlow ?? (object)DBNull.Value),
                new OracleParameter("p_RemarkForCustomer0", objModel.RemarkForCustomer0 ?? (object)DBNull.Value),
                new OracleParameter("p_RemarkForCustomer1", objModel.RemarkForCustomer1 ?? (object)DBNull.Value),
                new OracleParameter("p_RemarkForCustomer2", objModel.RemarkForCustomer2 ?? (object)DBNull.Value),
                new OracleParameter("p_Remark", objModel.Remark ?? (object)DBNull.Value),
                new OracleParameter("p_SubmitUser", objModel.SubmitUser ?? (object)DBNull.Value),
                new OracleParameter("p_OldProgram", objModel.OldProgram ?? (object)DBNull.Value),
                new OracleParameter("p_DeleteFlag", objModel.DeleteFlag ?? (object)DBNull.Value),
                new OracleParameter("p_AttachFT1Program", objModel.AttachFT1Program ?? (object)DBNull.Value),
                new OracleParameter("ReturnInt", OracleDbType.Int32) // 修改为 Int32
            };
                myParas[26].Direction = ParameterDirection.Output;
                cmd.Parameters.AddRange(myParas);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    ID = myParas[26].Value.ToString().Trim();
                    return true;
                }
                catch (Oracle.ManagedDataAccess.Client.OracleException ex)
                {
                    UtilSelf.WriteFile.Log(ex.Message, ErrorLogPath);
                    return false;
                }
            }
        }
    }

    public static Boolean TPRFInfoSPR(Modeler objModel, ref string ID)
    {
        UtilSelf.OracleHelp orl = new UtilSelf.OracleHelp();
        orl.ConnectionStr = TPRFConnection;
        try
        {
            orl.OpenConnection();
            orl.Command.CommandText = "InsertTPRFInfoSPR";
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
                                                        new OracleParameter("ReturnInt",System.Data.OracleClient.OracleType.Int16)
                                                    };
            myParas[26].Direction = ParameterDirection.Output;
            orl.Command.Parameters.AddRange(myParas);
            orl.Command.ExecuteNonQuery();
            ID = myParas[26].Value.ToString().Trim();
            return true;
        }
        catch (Exception ex)
        { UtilSelf.WriteFile.Log(ex.Message, ErrorLogPath); return false; }
        finally
        { orl.CloseConnection(); orl = null; GC.Collect(); }
    }
}


public class Modeler
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


//using System;
//using System.Data;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Data.SqlClient;
//using System.Collections;
//using System.Data.OleDb;
//using System.Data.OracleClient;

///// <summary>
///// DBTransfer 的摘要说明
///// </summary>
//public class DBTransfer
//{
//    public DBTransfer()
//    {
//        //
//        // TODO: 在此处添加构造函数逻辑
//        //
//    }

//    // 模拟数据的方法，返回一个包含数据的 DataSet
//    private static DataSet GetMockDataSet()
//    {
//        DataSet ds = new DataSet();
//        DataTable dt = new DataTable();
//        dt.Columns.Add("Column1", typeof(string));
//        dt.Columns.Add("Column2", typeof(int));

//        DataRow dr = dt.NewRow();
//        dr["Column1"] = "Mock Data";
//        dr["Column2"] = 123;
//        dt.Rows.Add(dr);

//        ds.Tables.Add(dt);
//        return ds;
//    }

//    public static DataSet getDataSet(string SQLStatement)
//    {
//        // 直接返回模拟数据
//        return GetMockDataSet();
//    }

//    public static DataSet getOraDataSet(string SQLStatement)
//    {
//        // 直接返回模拟数据
//        return GetMockDataSet();
//    }

//    public static DataSet getEtiOraDataSet(string SQLStatement)
//    {
//        // 直接返回模拟数据
//        return GetMockDataSet();
//    }

//    public static Boolean TPRFInfo(Modeler objModel, ref string ID)
//    {
//        // 模拟输出参数的值
//        ID = "1";
//        return true;
//    }

//    public static Boolean TPRFInfoSPR(Modeler objModel, ref string ID)
//    {
//        // 模拟输出参数的值
//        ID = "1";
//        return true;
//    }
//}

//public class Modeler
//{
//    private int _id, _releasetype, _statut;
//    private string _customer, _testertype, _device, _stage, _programpath, _programname, _action, _attachment, _remarkforcustomer0, _remarkforcustomer1,
//        _remarkforcustomer2, _remark, _submituser, _submittime, _releasedtime, _piggybackcheck, _piggybackcheckattach;
//    private string _attachdevice, _attachreleasedate, _attachft1programflow, _attachqa1programflow, _attachft1program, _attachqa1program, _attachdeviceversion;
//    private string _programrevision, _oldprogram, _deleteflag;

//    public int ID
//    {
//        set { _id = value; }
//        get { return _id; }
//    }
//    public int ReleaseType
//    {
//        set { _releasetype = value; }
//        get { return _releasetype; }
//    }
//    public string Customer
//    {
//        set { _customer = value; }
//        get { return _customer; }
//    }
//    public string TesterType
//    {
//        set { _testertype = value; }
//        get { return _testertype; }
//    }
//    public string Device
//    {
//        set { _device = value; }
//        get { return _device; }
//    }
//    public string Stage
//    {
//        set { _stage = value; }
//        get { return _stage; }
//    }
//    public string ProgramPath
//    {
//        set { _programpath = value; }
//        get { return _programpath; }
//    }
//    public string ProgramName
//    {
//        set { _programname = value; }
//        get { return _programname; }
//    }
//    public string Action
//    {
//        set { _action = value; }
//        get { return _action; }
//    }
//    public string Attachment
//    {
//        set { _attachment = value; }
//        get { return _attachment; }
//    }
//    public string RemarkForCustomer0
//    {
//        set { _remarkforcustomer0 = value; }
//        get { return _remarkforcustomer0; }
//    }
//    public string RemarkForCustomer1
//    {
//        set { _remarkforcustomer1 = value; }
//        get { return _remarkforcustomer1; }
//    }
//    public string RemarkForCustomer2
//    {
//        set { _remarkforcustomer2 = value; }
//        get { return _remarkforcustomer2; }
//    }
//    public string Remark
//    {
//        set { _remark = value; }
//        get { return _remark; }
//    }
//    public string SubmitUser
//    {
//        set { _submituser = value; }
//        get { return _submituser; }
//    }
//    public string SubmitTime
//    {
//        set { _submittime = value; }
//        get { return _submittime; }
//    }
//    public int Status
//    {
//        set { _statut = value; }
//        get { return _statut; }
//    }
//    public string ReleasedTime
//    {
//        set { _releasedtime = value; }
//        get { return _releasedtime; }
//    }

//    public string AttachDevice
//    {
//        set { _attachdevice = value; }
//        get { return _attachdevice; }
//    }
//    public string AttachReleaseDate
//    {
//        set { _attachreleasedate = value; }
//        get { return _attachreleasedate; }
//    }
//    public string AttachFT1ProgramFlow
//    {
//        set { _attachft1programflow = value; }
//        get { return _attachft1programflow; }
//    }
//    public string AttachQA1ProgramFlow
//    {
//        set { _attachqa1programflow = value; }
//        get { return _attachqa1programflow; }
//    }
//    public string PiggybackCheck
//    {
//        get { return _piggybackcheck; }
//        set { _piggybackcheck = value; }
//    }
//    public string PiggybackCheckAttach
//    {
//        get { return _piggybackcheckattach; }
//        set { _piggybackcheckattach = value; }
//    }
//    public string AttachFT1Program
//    {
//        set { _attachft1program = value; }
//        get { return _attachft1program; }
//    }
//    public string AttachQA1Program
//    {
//        set { _attachqa1program = value; }
//        get { return _attachqa1program; }
//    }
//    public string AttachDeviceVersion
//    {
//        set { _attachdeviceversion = value; }
//        get { return _attachdeviceversion; }
//    }
//    public string ProgramRevision
//    {
//        set { _programrevision = value; }
//        get { return _programrevision; }
//    }
//    public string OldProgram
//    {
//        set { _oldprogram = value; }
//        get { return _oldprogram; }
//    }
//    public string DeleteFlag
//    {
//        set { _deleteflag = value; }
//        get { return _deleteflag; }
//    }
//}