<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="consultaPlanesFinanciamiento.aspx.cs" Inherits="wProveedores.proveedores.consultaPlanesFinanciamiento" %>
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
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRubro" runat="server" Text="Rubro:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadComboBox ID="cmbRubro" Runat="server" Filter="Contains" Width="300px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha de inicio:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadDatePicker ID="dtpFechaInicio" Runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadDatePicker ID="dtpFechaFinal" Runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodEmpleado" runat="server" Text="Codigo empleado:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtCodEmpleado" Runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadGrid ID="gPlanesPago" runat="server" GroupPanelPosition="Top">            
        </telerik:RadGrid>
    </div>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
                            ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true" >
    </telerik:RadNotification>
</asp:Content>

