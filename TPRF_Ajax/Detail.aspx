<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="Detail" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
     Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TPRF System</title>
    <link href="style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-family: Arial">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="wrapper">
            <div style="margin-left: 0;">
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
                                            <asp:LinkButton runat="server" ID="lbDefault" Text="TPRF New Request" Font-Underline="true" ForeColor="Blue"
                                                OnClick="lbDefault_Click"></asp:LinkButton>
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
                            <asp:DetailsView AllowPaging="false" runat="server" ID="dvDetail" AutoGenerateRows="false"
                                CellPadding="0" CellSpacing="0">
                                <Fields>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td colspan="4">
                                                        <table border="0" width="990" cellpadding="0" cellspacing="0">
                                                            <tr style="height: 45px">
                                                                <td style="width:320px">
                                        <div style="margin-left: 8%; font-size: 16px; font-weight: bold">
                                            Please Select Release Type:</div>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" ID="txtReleaseType" Width="650px" Font-Size="16px" ReadOnly="true" Text='<%# Eval("ReleaseType") %>'></asp:TextBox>
                                    </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                                            height: 30px">
                                                            Customer: &nbsp; &nbsp; &nbsp;&nbsp;<asp:TextBox runat="server" ID="txtCustomer" Width="250px" ReadOnly="true"
                                                                Text='<%# Eval("Customer") %>'></asp:TextBox></div>
                                                    </td>
                                                    <td>
                                                        <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                                            height: 30px">
                                                            Tester Type: &nbsp; &nbsp; &nbsp;&nbsp;<asp:TextBox runat="server" ID="txtTesterType" Width="250px" ReadOnly="true"
                                                                Text='<%# Eval("TesterType") %>'></asp:TextBox></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                                            height: 30px">
                                                            Device: &nbsp; &nbsp; &nbsp;&nbsp;<asp:TextBox runat="server" ID="TextBox1" Width="250px" ReadOnly="true"
                                                                Text='<%# Eval("Device") %>'></asp:TextBox></div>
                                                    </td>
                                                    <td>
                                                        <div style="width: 445px; text-align: right; font-size: 14px; font-weight: bold;
                                                            height: 30px">
                                                            Stage: &nbsp; &nbsp; &nbsp;&nbsp;<asp:TextBox runat="server" ID="TextBox2" Width="250px" ReadOnly="true"
                                                                Text='<%# Eval("Stage") %>'></asp:TextBox></div>
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
                                                                <td style="width: 322px">
                                                                    <asp:TextBox runat="server" ID="TextBox3" Width="320px" ReadOnly="true"
                                                                Text='<%# Eval("ProgramPath") %>'></asp:TextBox></td>
                                                                <td style="width: 122px" align="right">
                                                                    <asp:Label runat="server" ID="lblProgramName" Text="Program Name:"></asp:Label>
                                                                </td>
                                                                <td style="width: 322px">
                                                                    <asp:TextBox runat="server" ID="txtProgramName" Width="305px" ReadOnly="true" Text='<%# Eval("ProgramName") %>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 35px; display: none">
                                                                <td align="right" style="width: 122px">
                                                                    Original Path:</td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtOriginalPath" runat="server" Width="645px" ReadOnly="true"></asp:TextBox></td>
                                                            </tr>
                                                            <tr style="height: 35px; display: none">
                                                                <td align="right" style="width: 122px">
                                                                    Target Path:</td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtTargetPath" runat="server" Width="645px" ReadOnly="true"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 122px" align="right" valign="top">
                                                                    Action:</td>
                                                                <td style="width: 322px">
                                                                    <asp:TextBox runat="server" ID="TextBox4" Width="250px" ReadOnly="true"
                                                                Text='<%# Eval("Action") %>'></asp:TextBox></td>
                                                                <td style="width: 122px" align="right" valign="top">
                                                                    Piggyback Check:</td>
                                                                <td style="width: 322px" valign="top">
                                                                    <asp:TextBox runat="server" ID="txtPiggybackCheck" ReadOnly="true" Text='<%# Eval("PiggybackCheck") %>'></asp:TextBox>
                                                                    <asp:HyperLink runat="server" ID="hlPiggybackCheckAttach" ForeColor="blue" Font-Underline="true" Text='<%# Eval("PiggybackCheckAttach") %>' NavigateUrl='<%# System.Configuration.ConfigurationManager.AppSettings["AttachNavigateUrlSuffix"]+Eval("PiggybackCheckAttach") %>'></asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 122px" align="right" valign="top">
                                                                    Attachment:</td>
                                                                <td style="width: 322px" valign="top" colspan="3">
                                                                    <asp:HyperLink runat="server" ID="hlAttach" ForeColor="blue" Font-Underline="true" Text='<%# Eval("Attachment") %>' NavigateUrl='<%# System.Configuration.ConfigurationManager.AppSettings["AttachNavigateUrlSuffix"]+Eval("Attachment") %>'></asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="trQUL0" style="height: 35px">
                                                                <td style="width: 122px" align="right" valign="top" rowspan="3">
                                                                    Remark for QUL:</td>
                                                                <td colspan="3" valign="top">
                                                                    <span id="Span1" style="margin-left: 8px; width: 200px" runat="server">MCN Check: &nbsp;
                                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span>
                                                                    <asp:TextBox runat="server" ID="txtMCNCheck" Width="345px" ReadOnly="true" Text='<%# Eval("RemarkForCustomer0") %>'></asp:TextBox></td>
                                                            </tr>
                                                            <tr runat="server" id="trQUL1" style="height: 35px">
                                                                <td colspan="3" valign="top">
                                                                    <span style="margin-left: 8px; width: 200px">QFPROM Check: &nbsp; &nbsp; &nbsp; </span>
                                                                    <asp:TextBox runat="server" ID="txtQFPROMCheck" Width="345px" ReadOnly="true" Text='<%# Eval("RemarkForCustomer1") %>'></asp:TextBox></td>
                                                            </tr>
                                                            <tr runat="server" id="trQUL2" style="height: 35px">
                                                                <td colspan="3" valign="top">
                                                                    <span style="margin-left: 8px; width: 200px">Global_overon: &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                                    </span>
                                                                    <asp:TextBox runat="server" ID="txtGlobalOveron" Width="345px" ReadOnly="true" Text='<%# Eval("RemarkForCustomer2") %>'></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 122px; height: 75px;" valign="top">
                                                                    Remark:</td>
                                                                <td colspan="3" style="height: 75px">
                                                                    <asp:TextBox runat="server" ID="txtRemark" TextMode="multiline" Width="95%" Height="60px"
                                                                        ReadOnly="true" Text='<%# Eval("Remark") %>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
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
