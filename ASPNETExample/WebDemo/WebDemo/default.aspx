<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebDemo._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Hello C# 웹프로그래밍</h2>
            <p> CSharp 스터디 </p>
        </div>
        
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <asp:CheckBox ID="CheckBox1" runat="server" Text="체크박스" />
        <asp:Image ID="Image1" runat="server" Height="436px" ImageUrl="~/Images/Image1.jpg" Width="680px" />

        <br />
        <!--
        <a href="subPage1.aspx">다음 페이지로</a>
        -->
        <asp:Button ID="Button2" runat="server" Text="다음 페이지" Width="169" OnClick="Button2_Click"/>
    </form>
</body>
</html>
