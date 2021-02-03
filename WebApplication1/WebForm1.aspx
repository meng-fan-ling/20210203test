<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="帳號"></asp:Label>
            &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="密碼"></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登入" />
            &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="註冊" />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="沒有此帳號!!" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                &lt;註冊&gt;<br />
                <br />
                帳號:
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <br />
                密碼:
                <asp:TextBox ID="TextBox4" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="確定" />
            </asp:Panel>
            <br />
        </div>
    </form>
</body>
</html>
