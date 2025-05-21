<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
     Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TPRF System</title>
    <link href="style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="wrapper">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="wrapper">
                        <div style="margin-left: 0;">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td colspan="4" style="height: 116px">
                                        <table border="0" width="990" cellpadding="0" cellspacing="0" style="height: 100px">
                                            <tr>
                                                <td align="right" background="img/Corp_Header.jpg" valign="top">
                                                    <div style="margin-right: 12%; margin-top: 2%; font-family: Arial; font-weight: 900;
                                                        font-size: 36px">
                                                        TPRF System</div>
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
                                <tr style="background-image: url(img/index.jpg); background-repeat: no-repeat; background-position: bottom left">
                                    <td colspan="4" align="center" style="margin-right: 10px">
                                        <table border="0" width="530" cellpadding="0" cellspacing="0" style="height: 200px">
                                            <tr valign="bottom">
                                                <td style="width: 240px">
                                                    <asp:Label runat="server" ID="lbl0" Width="230px"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <img alt="" src="img/user.ico" />
                                                </td>
                                                <td align="left">
                                                    UserName:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtUserName" Width="150px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label runat="server" ID="lblUserNameMessage" Visible="false" ForeColor="red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 240px">
                                                </td>
                                                <td align="left">
                                                    <img alt="" src="img/key.ico" />
                                                </td>
                                                <td align="left">
                                                    Password:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtPassword" Width="150px" TextMode="Password"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label runat="server" ID="lblPasswordMessage" Visible="false" ForeColor="red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 240px">
                                                </td>
                                                <td align="center" colspan="4" style="font-size: 16px">
                                                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td colspan="4">
                                                    <asp:Button runat="server" ID="btnLogin" Text="Login" Width="80px" OnCommand="btn_Click" />
                                                    <asp:Button runat="server" ID="btnExit" Text="Exit" Width="80px" OnCommand="btn_Click" />
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
                                        <div id="copyright" style="margin-left: 0">
                                            <table border="0" cellspacing="0" cellpadding="0" width="990">
                                                <tr>
                                                    <td class="bottom" width="990" height="31" align="center">
                                                        Copyright 2025 BY JSAC Test </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
