<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Getsession.aspx.cs" Inherits="DeliveryPrintService.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>无标题页</title>

</head>
<script type="text/javascript" language="javascript">
function OnCopy()
{
    form1.TextBox1.focus();
    document.execCommand("selectAll");
    document.execCommand("copy");
}
</script>

<body>
    <form id="form1" runat="server">
    <div>    
        
        <asp:TextBox ID="TextBox1" runat="server" Width="605px"></asp:TextBox>    
<asp:Button ID="Button1" runat="server" Text="Button" Height="21px" Width="74px" />
    </div>    
    </form>
</body>
</html>
