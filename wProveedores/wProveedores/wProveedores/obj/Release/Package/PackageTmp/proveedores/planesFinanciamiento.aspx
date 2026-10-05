<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="planesFinanciamiento.aspx.cs" Inherits="wProveedores.proveedores.planesFinanciamiento" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

    </div>
    <div style="width:712px; margin:0 auto;">
        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
                <td class="auto-style1"></td>
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
                    <asp:Label ID="lblCodigo" runat="server" Text="Codigo de pago:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtCodEmpleado" Runat="server">
                    </telerik:RadTextBox>
                    <telerik:RadButton ID="cmdBuscar" runat="server" Text="Buscar" OnClick="cmdBuscar_Click">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmpleado" runat="server" Text="Empleado:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtEmpleado" Runat="server" Width="300px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFrecuencia" runat="server" Text="Frecuencia:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtFrecuencia" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLimiteCredito" runat="server" Text="Limite de credito:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtLimiteCredito" Runat="server" ReadOnly="true" BackColor="Silver">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtEstado" Runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtDescripcion" Runat="server" TextMode="MultiLine" Width="300px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadDatePicker ID="dtpFecha" Runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMonto" runat="server" Text="Monto:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtMonto" Runat="server">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNumeroCuotas" runat="server" Text="No. de cuotas:"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadTextBox ID="txtNoCuotas" Runat="server">
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
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div style="width:712px; margin:0 auto; text-align:center;">
         <telerik:RadButton ID="cmdGuardar" runat="server" Text="Guardar" Width="300px" OnClick="cmdGuardar_Click">
                    </telerik:RadButton>
    </div>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
                            ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true" >
    </telerik:RadNotification>
</asp:Content>
