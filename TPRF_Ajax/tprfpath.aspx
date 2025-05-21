<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tprfpath.aspx.cs" Inherits="tprfpath" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
        Style="position: relative" CellPadding="1" Font-Size="Smaller" HorizontalAlign="Center" PageSize="20" SkinID="gvSkin" Width="100%">
        <Columns>
            <asp:BoundField DataField="DEPT" HeaderText="DEPT" SortExpression="DEPT" />
            <asp:BoundField DataField="CUST" HeaderText="CUST" SortExpression="CUST" />
            <asp:BoundField DataField="TESTER" HeaderText="TESTER" SortExpression="TESTER" />
            <asp:BoundField DataField="BUYOFF_DIR" HeaderText="BUYOFF_DIR" SortExpression="BUYOFF_DIR" />
            <asp:BoundField DataField="BF" HeaderText="BF" SortExpression="BF" />
            <asp:BoundField DataField="PROD_DIR" HeaderText="PROD_DIR" SortExpression="PROD_DIR" />
            <asp:BoundField DataField="STR_DIR" HeaderText="STR_DIR" SortExpression="STR_DIR" />
            <asp:BoundField DataField="WORKORDER" HeaderText="WORKORDER" SortExpression="WORKORDER" />
            <asp:BoundField DataField="DELETE_DIR" HeaderText="DELETE_DIR" SortExpression="DELETE_DIR" />
        </Columns>
        <HeaderStyle Font-Bold="False" HorizontalAlign="Right" />
        <PagerSettings FirstPageText="" />
        <RowStyle HorizontalAlign="Justify" />
        <PagerStyle HorizontalAlign="Center" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:OracleConnstring %>" ProviderName="<%$ ConnectionStrings:OracleConnstring.ProviderName %>" SelectCommand='SELECT "DEPT", "CUST", "TESTER", "BUYOFF_DIR", "BF", "PROD_DIR", "STR_DIR", "WORKORDER", "DELETE_DIR" FROM "euser.tprfinfo_dir"'></asp:SqlDataSource>
</asp:Content>


