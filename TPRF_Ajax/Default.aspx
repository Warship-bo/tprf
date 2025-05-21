<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
     Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<%@ Register Src="~/Processing.ascx" TagName="Processing" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TPRF System</title>
    <link href="style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-family: Arial">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="wrapper">
            <div style="margin-left: 0;">
                <uc1:Processing runat="server" ID="Waiting" />
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="4" style="height: 116px">
                            <table border="0" width="990" cellpadding="0" cellspacing="0" style="height: 100px">
                                <tr>
                                    <td align="right" background="img/Corp_Header.jpg" valign="top" style="height: 100px">
                                        <div style="margin-right: 12%; margin-top: 2%; font-family: Arial; font-weight: 900;
                                            font-size: 36px">
                                            TPRF System</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <div style="margin-right: 5%; font-weight: bold">
                                            Current User: &nbsp;<asp:Label runat="server" ID="lblCurrentUser" ForeColor="blue"
                                                Font-Bold="true"></asp:Label>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton runat="server" ID="lbDefault" Text="TPRF New Request" Font-Underline="true"
                                                ForeColor="Blue" OnClick="lbDefault_Click"></asp:LinkButton>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                            <asp:LinkButton runat="server" ID="lbIndex" Text="TPRF List" Font-Underline="true"
                                                ForeColor="Blue" OnClick="lbIndex_Click"></asp:LinkButton>
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                            <asp:LinkButton runat="server" ID="lbLogout" Text="Logout" Font-Underline="true"
                                                ForeColor="Blue" OnClick="btnLogout_Click"></asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" width="990" cellpadding="0" cellspacing="0">
                                <tr style="height: 45px">
                                    <td>
                                        <div style="margin-left: 8%; font-size: 16px; font-weight: bold">
                                            Please Select Release Type:</div>
                                    </td>
                                    <td colspan="3">
                                        <asp:RadioButtonList runat="server" ID="rblReleaseType" RepeatDirection="horizontal"
                                            Font-Size="16px" Font-Bold="true" ForeColor="Brown" Width="100%" AutoPostBack="true"
                                            OnSelectedIndexChanged="rblReleaseType_SelectedIndexChanged">
                                            <asp:ListItem Text="Full Directory Release" Value="1" Selected="true"></asp:ListItem>
                                            <asp:ListItem Text="Partial Folder Release" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Single File Release" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                height: 30px">
                                Customer: &nbsp; &nbsp; &nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlCustomer"
                                    AutoPostBack="true" Width="250px" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                </asp:DropDownList></div>
                        </td>
                        <td>
                            <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                height: 30px">
                                Tester Type: &nbsp; &nbsp; &nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlTesterType"
                                    AutoPostBack="true" Width="250px" OnSelectedIndexChanged="ddlTesterType_SelectedIndexChanged">
                                </asp:DropDownList></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                height: 30px">
                                Device: &nbsp; &nbsp; &nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlDevice"
                                    AutoPostBack="true" Width="250px" OnSelectedIndexChanged="ddlDevice_SelectedIndexChanged">
                                </asp:DropDownList></div>
                        </td>
                        <td>
                            <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                height: 30px">
                                Stage: &nbsp; &nbsp; &nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlStage" AutoPostBack="true"
                                    Width="250px" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">
                                </asp:DropDownList></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" width="990" cellpadding="0" cellspacing="0" style="font-size: 14px">
                                <tr style="height: 35px">
                                        <td style="width: 122px" align="right">
                                            Program Path:</td>
                                        <td colspan="3">
                                            <asp:DropDownList runat="server" ID="ddlProgramPath" AutoPostBack="true" Width="307px"
                                                OnSelectedIndexChanged="ddlProgramPath_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="height: 35px">
                                        <td align="right" style="width: 122px">
                                            <asp:Label runat="server" ID="lblProgramName" Text="Program Name:"></asp:Label></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtProgramName" Width="302px"></asp:TextBox></td>
                                        <td><asp:Label runat="server" ID="lblProgramRevision" Text="Program Revision:"></asp:Label></td>
                                        <td><asp:TextBox runat="server" ID="txtProgramRevision" Width="302px"></asp:TextBox></td>
                                    </tr>
                                <tr style="height: 35px; display: none">
                                    <td align="right" style="width: 122px">
                                        Original Path:</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtOriginalPath" runat="server" Width="645px"></asp:TextBox></td>
                                </tr>
                                <tr style="height: 35px; display: none">
                                    <td align="right" style="width: 122px">
                                        Target Path:</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtTargetPath" runat="server" Width="645px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 122px" align="right" valign="top">
                                        Action:</td>
                                    <td style="width: 322px">
                                        <asp:RadioButtonList runat="server" ID="rblAction">
                                            <asp:ListItem Text="Release To Prod" Value="Release To Prod" Selected="true"></asp:ListItem>
                                            <asp:ListItem Text="Release To STR" Value="Release To STR"></asp:ListItem>
                                        </asp:RadioButtonList></td>
                                    <td align="right" style="width: 122px" valign="top">
                                        Piggyback Check:</td>
                                    <td style="width: 322px" valign="top">
                                        <asp:CheckBox runat="server" ID="ckboxPiggybackCheck" AutoPostBack="true" OnCheckedChanged="ckboxPiggybackCheck_CheckedChanged" />
                                        <br />
                                        <asp:FileUpload runat="server" ID="fuPiggybackCheck" /><asp:Button runat="server"
                                            ID="btnPiggybackCheck" Text="Upload" OnClick="btnPiggybackCheck_Click" />
                                        <br />
                                        <asp:HyperLink runat="server" ID="hlPiggybackCheck" ForeColor="Blue" Font-Underline="true"
                                            Text="N/A"></asp:HyperLink>
                                    </td>
                                </tr>
                                    <tr>
                                        <td align="right" style="width: 122px" valign="middle">
                                            Old Program:</td>
                                        <td>
                                            <asp:TextBox ID="txtOldProgram" runat="server" Width="275px"></asp:TextBox></td>
                                        <td align="left" valign="top" colspan="2">
                                            <!--0 means no need delete;  1 means Delete immediately;  2 means Delete 7 days later-->                                        
                                            <asp:RadioButtonList runat="server" ID="rblDeleteOption" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="Never Delete" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Delete Right Now" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Delete 7 Days Later" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList></td>
                                    </tr>
                                <tr>
                                    <td style="width: 122px" align="right" valign="top">
                                        Attachment:</td>
                                    <td valign="top" colspan="3">
                                        <asp:FileUpload runat="server" ID="fuAttach" /><asp:Button runat="server" ID="btnUpload"
                                            Text="Upload" OnClick="btnUpload_Click" />
                                        <br />
                                        <asp:HyperLink runat="server" ID="hlAttach" ForeColor="blue" Font-Underline="true"
                                            Text="N/A"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr runat="server" id="trQUL0" style="height: 35px">
                                    <td style="width: 122px" align="right" valign="top" rowspan="3">
                                        Remark for QUL:</td>
                                    <td colspan="3" valign="top">
                                        <span style="margin-left: 8px; width: 200px" runat="server">MCN Check: &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span>
                                        <asp:TextBox runat="server" ID="txtMCNCheck" Width="345px"></asp:TextBox></td>
                                </tr>
                                <tr runat="server" id="trQUL1" style="height: 35px">
                                    <td colspan="3" valign="top">
                                        <span style="margin-left: 8px; width: 200px">QFPROM Check: &nbsp; &nbsp; &nbsp; </span>
                                        <asp:TextBox runat="server" ID="txtQFPROMCheck" Width="345px"></asp:TextBox></td>
                                </tr>
                                <tr runat="server" id="trQUL2" style="height: 35px">
                                    <td colspan="3" valign="top">
                                        <span style="margin-left: 8px; width: 200px">Global_overon: &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                        </span>
                                        <asp:TextBox runat="server" ID="txtGlobalOveron" Width="345px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 122px; height: 75px;" valign="top">
                                        Remark:</td>
                                    <td colspan="3" style="height: 75px">
                                        <asp:TextBox runat="server" ID="txtRemark" TextMode="multiline" Width="95%" Height="60px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="center" style="height: 38px">
                        <td colspan="4">
                            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" />
                        </td>
                    </tr>
                    <tr align="center" style="height: 38px">
                        <td colspan="4">
                            <asp:Label runat="server" ID="lblMessage" Font-Names="Arial" Font-Size="16px" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div id="copyright" style="margin-left: 0">
                                <table border="0" cellspacing="0" cellpadding="0" width="990">
                                    <tr>
                                        <td class="bottom" width="990" align="center" style="height: 20px">
                                            Copyright 2025 BY JSAC Test 
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
