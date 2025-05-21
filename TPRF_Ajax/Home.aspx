<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
     Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                                            <asp:LinkButton runat="server" ID="lbHelp" Text="TPRF Path Help" Font-Underline="true"
                                                ForeColor="Blue" OnClick="lbHelp_Click"></asp:LinkButton>
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
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" align="center" style="width: 350px; font-weight: bold; font-size: 14px;">
                                        TPRF Release Program Type:<br />
                                        发布程序类型</td>
                                    <td colspan="3" style="width: 630px; font-weight: bold; font-size: 14px;" align="left">
                                        <asp:RadioButtonList runat="server" ID="rblReleaseType" RepeatDirection="horizontal"
                                            TextAlign="right" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="rblReleaseType_SelectedIndexChanged">
                                            <asp:ListItem Value="NewRelease" Text="New Program Release<br>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 发布新程序"></asp:ListItem>
                                            <asp:ListItem Value="Update" Text="Update Current Program<br>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 更新现有程序"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tbody runat="server" id="tbodyMain">
                        <tr runat="server" id="trReleaseType">
                            <td style="font-weight: bold; font-size: 14px;" align="center">
                                Update Program Type:
                                <br />
                                更新程序类型</td>
                            <td colspan="3">
                                <asp:RadioButtonList runat="server" ID="rblUpdateType" RepeatDirection="vertical"
                                    Font-Size="14px" Font-Bold="true" ForeColor="Brown" Width="360px" AutoPostBack="true"
                                    OnSelectedIndexChanged="rblUpdateType_SelectedIndexChanged">
                                    <asp:ListItem Text="Full Directory Release/全文件夹发布" Value="1" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="Partial Folder Release/指定文件夹发布" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Single File Release/单个文件发布" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table style="width: 100%">
                                    <tr>
                                         <td>
                                                Action:<br />
                                                发布方式</td>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rblAction" OnSelectedIndexChanged="rblAction_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Text="Release To Prod" Value="Release To Prod"></asp:ListItem>
                                                    <asp:ListItem Text="Release To STR" Value="Release To STR"></asp:ListItem>
                                                    <asp:ListItem Text="Delete from Prod" Value="Delete from Prod"></asp:ListItem>
                                                </asp:RadioButtonList></td>
                                        <td style="width: 120px">
                                            Customer:<br />
                                            客户</td>
                                        <td style="width: 220px">
                                            <asp:DropDownList runat="server" ID="ddlCustomer" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"
                                                Width="210px">
                                            </asp:DropDownList></td>
                                        <td style="width: 120px">
                                        </td>
                                        <td style="width: 220px">
                                        </td>
                                    </tr>
                                    <tbody runat="server" id="tbodySPR">
                                        <tr>
                                            <td style="width: 120px">
                                                Attachment:<br />
                                                附件
                                            </td>
                                            <td colspan="3">
                                                <asp:FileUpload runat="server" ID="fuSPR" Width="350px" />
                                                <asp:Button runat="server" ID="btnSPRUpload" Text=" Upload " Width="80" OnClick="btnSPRUpload_Click" />
                                                <br />
                                                <asp:HyperLink ID="hlAttachSPR" runat="server" Font-Underline="true" ForeColor="blue"
                                                    Text="N/A"></asp:HyperLink></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px">
                                                Directory:<br />
                                                目录</td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlDirectory" runat="server" Width="282px">
                                                    <asp:ListItem Value="N/A">	-=Please Select=-	</asp:ListItem>
                                                    <asp:ListItem>	AVENGER	</asp:ListItem>
                                                    <asp:ListItem>	Bagheera	</asp:ListItem>
                                                    <asp:ListItem>	Dino	</asp:ListItem>
                                                    <asp:ListItem>	Dolphin	</asp:ListItem>
                                                    <asp:ListItem>	Frodo_8x_btm	</asp:ListItem>
                                                    <asp:ListItem>	Frodo_8x_top	</asp:ListItem>
                                                    <asp:ListItem>	Gimli	</asp:ListItem>
                                                    <asp:ListItem>	Gimli_Bottom	</asp:ListItem>
                                                    <asp:ListItem>	Gimli_Top	</asp:ListItem>
                                                    <asp:ListItem>	PIKE	</asp:ListItem>
                                                    <asp:ListItem>	PIKEL	</asp:ListItem>
                                                    <asp:ListItem>	SC2723E	</asp:ListItem>
                                                    <asp:ListItem>	SC2723S	</asp:ListItem>
                                                    <asp:ListItem>	SC9620A	</asp:ListItem>
                                                    <asp:ListItem>	SR3592	</asp:ListItem>
                                                    <asp:ListItem>	SR3593S	</asp:ListItem>
                                                    <asp:ListItem>	SharkA	</asp:ListItem>
                                                    <asp:ListItem>	SharkD	</asp:ListItem>
                                                    <asp:ListItem>	SharkL	</asp:ListItem>
                                                    <asp:ListItem>	Shere	</asp:ListItem>
                                                    <asp:ListItem>	SR3533D	</asp:ListItem>
                                                    <asp:ListItem>	T-Shark	</asp:ListItem>
                                                    <asp:ListItem>	T-SharkA+D	</asp:ListItem>
                                                    <asp:ListItem>	TShark2	</asp:ListItem>
                                                    <asp:ListItem>	TShark3	</asp:ListItem>
                                                    <asp:ListItem>	TigerA+D	</asp:ListItem>
                                                    <asp:ListItem>	TigerD	</asp:ListItem>
                                                    
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:DataGrid ID="dgAttach" runat="server" ForeColor="Black" GridLines="Vertical"
                                                    CellPadding="4" BorderWidth="1px" BorderStyle="None" BorderColor="#DEDFDE" BackColor="White"
                                                    AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundColumn DataField="Device" HeaderText="Device"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="FTProgram" HeaderText="FT Program"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="QAProgram" HeaderText="QA Program"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="FTFlow" HeaderText="FT Program Flow"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="QAFlow" HeaderText="QA Program Flow"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DeviceVersion" HeaderText="Device Version"></asp:BoundColumn>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="left" Mode="NumericPages" />
                                                    <AlternatingItemStyle BackColor="White" />
                                                    <ItemStyle BackColor="#F7F7DE" HorizontalAlign="left" Font-Size="10px" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="left" />
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tbody runat="server" id="tbodyNoSPR">
                                        <tr>     
                                            <td style="width: 120px">
                                                Tester:<br />
                                                测试机</td>
                                            <td style="width: 220px">
                                                <asp:DropDownList runat="server" ID="ddlTester" Width="210px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlTester_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                            <td style="width: 120px">
                                                Device:<br />
                                                产品</td>
                                            <td style="width: 220px">
                                                <asp:DropDownList runat="server" ID="ddlDevice" Width="210px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlDevice_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Stage:<br />
                                                阶段</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlStage" Width="210px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Program Path:<br />
                                                程序路径</td>
                                            <td colspan="3">
                                                <asp:DropDownList runat="server" ID="ddlProgramPath" Width="440px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlProgramPath_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Program Name:<br />
                                                程序名</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtProgramName" Width="210px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Program Revision:<br />
                                                程序版本</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtProgramRevision" Width="210px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                           <%-- <td>
                                                Action:<br />
                                                发布方式</td>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rblAction">
                                                    <asp:ListItem Text="Release To Prod" Value="Release To Prod"></asp:ListItem>
                                                    <asp:ListItem Text="Release To STR" Value="Release To STR"></asp:ListItem>
                                                    <asp:ListItem Text="Delete from Prod" Value="Delete from Prod"></asp:ListItem>
                                                </asp:RadioButtonList></td>--%>
                                            <td>
                                                Piggyback Check:<br />
                                                负载检测</td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="ckPiggyBack" AutoPostBack="True" OnCheckedChanged="ckPiggyBack_CheckedChanged" /><br />
                                                <asp:FileUpload runat="server" ID="fuPiggyback" /><asp:Button runat="server" ID="btnUploadPiggyback"
                                                    Text=" Upload " OnClick="btnUploadPiggyback_Click" />
                                                <br />
                                                <asp:HyperLink runat="server" ID="hlPiggyback" ForeColor="blue" Font-Underline="true"
                                                    Text="N/A"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trDeleteOldProgram">
                                            <td colspan="4">
                                                <table style="width: 100%;">
                                                    <tr style="font-size: 14px; font-weight: bold; background-color: #FFF9E9">
                                                        <td colspan="4">
                                                            How To Deal With Old Program/如何处理旧有程序</td>
                                                    </tr>
                                                    <tr style="background-color: #FFF9E9">
                                                        <td style="width: 120px">
                                                            Old Program:<br />
                                                            旧程序名</td>
                                                        <td style="width: 220px">
                                                            <asp:TextBox ID="txtOldProgram" runat="server" Width="210px"></asp:TextBox></td>
                                                        <td style="width: 120px">
                                                            Delete Type:<br />
                                                            删除类型
                                                        </td>
                                                        <td style="width: 220px" align="left" valign="top">
                                                            <!--0 means no need delete;  1 means Delete immediately;  2 means Delete 7 days later-->
                                                            <asp:RadioButtonList runat="server" ID="rblDeleteOption" RepeatDirection="vertical"
                                                                TextAlign="right">
                                                                <asp:ListItem Text="Delete Right Now/立即删除" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Delete 7 Days Later/7天后删除" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Attachment:<br />
                                                附件</td>
                                            <td colspan="3" style="width: 460px">
                                                <asp:FileUpload runat="server" ID="fuAttach" Width="380px" /><asp:Button runat="server"
                                                    ID="btnUpload" Text=" Upload " OnClick="btnUpload_Click" />
                                                <br />
                                                <asp:HyperLink runat="server" ID="hlAttach" ForeColor="blue" Font-Underline="true"
                                                    Text="N/A"></asp:HyperLink></td>
                                        </tr>
                                        <tr runat="server" id="trQULRemark">
                                            <td colspan="4">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 120px" align="left" valign="top" rowspan="3">
                                                            Remark for QUL:<br />
                                                            QUL的说明</td>
                                                        <td style="width: 120px" align="right">
                                                            MCN Check:</td>
                                                        <td colspan="2" style="width: 440px">
                                                            <asp:TextBox runat="server" ID="txtMCNCheck" Width="400px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px" align="right">
                                                            QFPROM Check:</td>
                                                        <td colspan="2" style="width: 440px">
                                                            <asp:TextBox runat="server" ID="txtQFPROMCheck" Width="400px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 120px" align="right">
                                                            Global_overon:</td>
                                                        <td colspan="2" style="width: 440px">
                                                            <asp:TextBox runat="server" ID="txtGlobalOveron" Width="400px"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Remark:<br />
                                                说明</td>
                                            <td colspan="3">
                                                <asp:TextBox runat="server" ID="txtRemark" TextMode="multiline" Width="550px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tr>
                                        <td colspan="4" align="center" style="height: 30px">
                                            <asp:Button runat="server" ID="btnSubmit" Text=" Submit " OnClick="btnSubmit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
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
