<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="consultaSaldo.aspx.cs" Inherits="wProveedores.proveedores.consultaSaldo" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

                    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
                        <Items>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/search.png" Text="Consultar">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="Button 1" IsSeparator="True">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="Excel" ImageUrl="~/Images/16/excel.png">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="Word" ImageUrl="~/Images/16/word.png">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="PDF" ImageUrl="~/Images/16/pdf.png">
                            </telerik:RadToolBarButton>
                        </Items>
                    </telerik:RadToolBar>

    </div>
    <div style="width:712px; margin:0 auto;">

        <table>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodigo" runat="server" Text="Codigo de pago:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtCodCliente" Runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadGrid ID="gEmpleados" runat="server" GroupPanelPosition="Top">            
        </telerik:RadGrid>
    </div>
</asp:Content>
