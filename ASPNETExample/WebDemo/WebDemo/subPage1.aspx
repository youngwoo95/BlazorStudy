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

            <asp:TextBox ID="txtName" runat="server" width="200px" Height="30px" placeholder="이름을 입력해주세요"/>
            <br />
            <asp:Button ID="Button1" runat="server" Text="사원정보 검색" Width="200px" Height="30px" OnClick="Button1_Click"/>

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
