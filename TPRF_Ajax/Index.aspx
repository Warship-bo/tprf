<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
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
                            <asp:DataGrid runat="server" ID="dgList" AutoGenerateColumns="False" Width="100%"
                                Font-Names="Arial" PageSize="25" AllowPaging="True" PagerStyle-Mode="numericpages"
                                PagerStyle-PageButtonCount="9" CellPadding="4" ForeColor="#333333" GridLines="None"
                                OnPageIndexChanged="dgList_PageIndexChanged" OnItemCommand="dgList_ItemCommand" OnItemDataBound="dgList_ItemDataBound">
                                <Columns>
                                    <asp:HyperLinkColumn Target="_blank" DataTextField="ID" DataTextFormatString="View"
                                        DataNavigateUrlField="ID" DataNavigateUrlFormatString="Detail.aspx?ID={0}">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle ForeColor="Blue" Width="30px" />
                                    </asp:HyperLinkColumn>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="25px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ReleaseType" HeaderText="ReleaseType">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="60px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Customer" HeaderText="CUST">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="50px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TesterType" HeaderText="Tester Type">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="110px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Device" HeaderText="Device">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Stage" HeaderText="Stage">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="50px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ProgramName" HeaderText="ProgramName">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="250px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Action" HeaderText="Action">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="100px" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Status" HeaderText="Status">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle Width="80px" />
                                    </asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="linkbutton" Text="Buy Off" CommandName="BuyOff">
                                        <HeaderStyle Font-Bold="True" BackColor="#FF9A62" Font-Size="14px" />
                                        <ItemStyle ForeColor="Blue" Width="50px" />
                                    </asp:ButtonColumn>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditItemStyle BackColor="#999999" />
                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#F7F6F3" ForeColor="Black" HorizontalAlign="Left" PageButtonCount="9"
                                    Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            </asp:DataGrid>
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
