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
        <p>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </p>
        <p>
            <asp:CheckBox ID="CheckBox1" runat="server" Text="체크박스" />
        </p>
        <p>
            <asp:Image ID="Image1" runat="server" Height="436px" ImageUrl="~/Images/Image1.jpg" Width="680px" />
        </p>
    </form>
</body>
</html>
