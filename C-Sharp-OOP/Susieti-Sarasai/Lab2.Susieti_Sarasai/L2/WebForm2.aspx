<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="L2.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>L2-2</title>
    <link runat="server" rel="stylesheet" href="StyleSheet1.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 1726px">
           
            <asp:Label CssClass="centered" ID="Label8" runat="server" Text="Pradiniai kelionių duomenys:"></asp:Label>
            <br />
            <asp:Table ID="Table3" runat="server" BackColor="#FFDBDB" BorderColor="Black" BorderWidth="1px" GridLines="Both" Height="192px" Width="376px" BorderStyle="Solid">
            </asp:Table>
            <br />
            <asp:Label CssClass="centered" ID="Label9" runat="server" Text="Pradiniai kainų duomenys:"></asp:Label>
            <br />
            <asp:Table ID="Table4" runat="server" BackColor="#FFDBDB" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Height="229px" Width="214px">
            </asp:Table>
            <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ForeColor="Red" />
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Esamas miestas: "></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Savaitės diena:"></asp:Label>
            <br />
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Pasirinkite savaitės dieną" MaximumValue="7" MinimumValue="1"></asp:RangeValidator>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Norimas miestas:"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList2" ErrorMessage="Pasirinkite norimą miestą" InitialValue="-"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Išvykimo laikas:"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList3" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList3" ErrorMessage="Pasirinkite išvykimo laiką" InitialValue="-"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Atvykimo laikas:"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList4" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList4" ErrorMessage="Pasirinkite atvykimo laiką" InitialValue="-"></asp:RequiredFieldValidator>
            <br />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" CssClass="button" OnClick="Button1_Click" Text="Ieškoti kelionės" />
            <br />
            <br />
            <asp:Label ID="Label5" CssClass="centered" runat="server" Text="Rasta pigiausia kelionė:"></asp:Label>
            <asp:Table ID="Table1" runat="server" BackColor="#FFDBDB" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Height="134px" Width="230px">
            </asp:Table>
            <br />
            <asp:Label ID="Label6" CssClass="centered" runat="server" Text="Autobusų sąrašas, kurie daugiausiai važiuoja į: "></asp:Label>
            <asp:Label ID="Label10" runat="server"></asp:Label>
            <asp:Table ID="Table2" runat="server" Height="176px" Width="364px" BackColor="#FFDBDB" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both">
            </asp:Table>
            <br />
            <asp:Label ID="Label11" CssClass="centered" runat="server" Text="Po tranzitinių autobusų pašalinimo:"></asp:Label>
            <asp:Table ID="Table5" runat="server" Height="175px" Width="362px" BackColor="#FFDBDB" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" GridLines="Both">
            </asp:Table>
        
        </div>
        
    </form>
</body>
</html>
