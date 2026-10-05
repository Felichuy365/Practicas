<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="consultaEmpleado.aspx.cs" Inherits="wProveedores.consultaEmpleado" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:1024px; margin:0 auto;">
        <table >
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCodigoEmpleado" runat="server" Text="Codigo de pago:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtCodigoEmpleado" Runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmpleado" runat="server" Text="Empleado:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtEmpleado" Runat="server" Width="300px" ReadOnly="true" BackColor="Silver">
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
            <tr>
                <td colspan="3">
                    <div style="text-align:center;">

                        <telerik:RadButton ID="cmdBuscar" runat="server" Text="Buscar" OnClick="cmdBuscar_Click">
                        </telerik:RadButton>

                        &nbsp;<telerik:RadButton ID="cmbLimpiar" runat="server" Text="Limpiar" OnClick="cmbLimpiar_Click">
                        </telerik:RadButton>
                                            

                    </div>
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
    <div style="width:1024px; margin:0 auto;">

        <table>
            <tr>
                <td>
                    <asp:Label ID="lblFrecuencia" runat="server" Text="Frecuencia:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtFrecuencia" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLimiteCredito" runat="server" Text="Limite de credito mensual:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtLimiteCredito" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaIngreso" runat="server" Text="Fecha de ingreso:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtFechaIngreso" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtEstado" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtCodTipoNonina" Runat="server" ReadOnly="true" Visible="False">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblVales" runat="server" Text="Total en vales:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtVales" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPlanesPago" runat="server" Text="Total en planes de pago (Pendiente):"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtPlanesPago" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            </table>

    </div>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
        ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true">
    </telerik:RadNotification>
</asp:Content>
