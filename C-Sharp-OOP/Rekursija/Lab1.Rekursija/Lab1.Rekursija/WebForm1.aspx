<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Lab1.Rekursija.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="Black" Height="41px" Text="Kryžiažodis: " Width="143px"></asp:Label>
        </div>
        <asp:Table ID="Table1" runat="server" BorderColor="Black" BorderWidth="1px" GridLines="Both" Height="174px" Width="519px" BorderStyle="Solid" Font-Bold="True">
        </asp:Table>
        <asp:Label ID="Label3" runat="server"></asp:Label>
        <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Spręsti" BorderColor="Black" BorderWidth="1px" ForeColor="Black" Height="36px" Width="110px" />
        </p>
        <asp:Label ID="Label4" runat="server"></asp:Label>
        <asp:Table ID="Table4" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="Black" GridLines="Both" Height="174px" Width="519px">
        </asp:Table>
    </form>
</body>
</html>
