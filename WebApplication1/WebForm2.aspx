<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="登入成功!"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="ID:"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox1" runat="server" ReadOnly="True"></asp:TextBox>
            &nbsp;<br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="姓名:"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            &nbsp;<br />
            <asp:Label ID="Label5" runat="server" Text="就讀大學:"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="就讀科系: "></asp:Label>
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server">
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="新增資料" style="margin-top: 0px" />
                &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登出" />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                已有資料!是否進行修改 
                <br />
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="修改" Width="36px" />
            </asp:Panel>
            <br />
            &nbsp;
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="School" HeaderText="School" SortExpression="School" />
                    <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Person] WHERE ([Id] = @Id)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="TextBox1" Name="Id" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="檢視全部" />
&nbsp;<asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="匯入Excel" />
            <br />
        </div>
    </form>
</body>
</html>
