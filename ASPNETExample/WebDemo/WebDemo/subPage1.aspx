<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="subPage1.aspx.cs" Inherits="WebDemo.subPage1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>서브 페이지</h2>

            <asp:Button ID="Button1" runat="server" Text="사원정보 보기" Width="200px" Height="100px" OnClick="Button1_Click"/>

            <br />
            <br />
            <br />

            <p> [사원 리스트] </p>

            <asp:GridView runat="server" ID="GridView1">
            </asp:GridView>

        </div>
    </form>
</body>
</html>
