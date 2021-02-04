<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        table, td, th {
            border: 1px solid black;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <h3>~~登入~~</h3>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="帳號"></asp:Label></td>
                    <td>&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="密碼"></asp:Label></td>
                    <td>&nbsp;<asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
            </table>

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登入" />
            &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="註冊" Height="21px" Width="40px" />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="沒有此帳號!!" Visible="False"></asp:Label>
            <br />
            <asp:Panel ID="Panel1" runat="server" Visible="False">
                <h3>~~註冊~~</h3>
                <table>
                    <tr>
                        <td>帳號:</td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>密碼:</td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" TextMode="Password"></asp:TextBox></td>
                    </tr>
                </table>

                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="確定" />
            </asp:Panel>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
