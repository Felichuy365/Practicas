<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="valesCafeteria.aspx.cs" Inherits="wProveedores.cafeteria.valesCafeteria" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
        .hidden-field {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:1024px; margin:0 auto;">

        <table>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblRubro" runat="server" Text="Rubro:"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbRubro" Runat="server" Filter="Contains" Width="400px">
                    </telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
                <td rowspan="8">
                    <div style="vertical-align: top; text-align: center">
                    </div>
                    <div class="col-sm-10">
                        <asp:Image ID="imgEmpleado" runat="server" Height="190px" Width="210px" />
                        <p>Foto del Empleado.</p>
                        
                        <telerik:RadTextBox ID="txtEstado" runat="server" Width="300px" ReadOnly="True" ForeColor="#CC0000" Font-Size="Medium">
                            </telerik:RadTextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblCodEmpleado" runat="server" Text="Empleado:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtCodEmpleado" runat="server" Width="100px">
                    </telerik:RadTextBox>
                    &nbsp;<telerik:RadButton ID="cmdBuscarEmpleado" runat="server" Text="..." OnClick="cmdBuscarEmpleado_Click">
                    </telerik:RadButton>
                    &nbsp;<telerik:RadTextBox ID="txtEmpleado" runat="server" Width="300px" ReadOnly="True">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:Label ID="lblLimiteCredito" runat="server" Text="Limite de credito:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtLimiteCredito" runat="server" ReadOnly="True" Width="100px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:Label ID="lblCodTipoNomina" runat="server" Text="Cod. tipo nomina:" CssClass="hidden-field"></asp:Label>
                </td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtCodTipoNomina" runat="server" ReadOnly="True" Width="100px" CssClass="hidden-field">
                    </telerik:RadTextBox>
                    <asp:HiddenField ID="hdfEstado" runat="server" />
                    <!-- Campo oculto para el límite de crédito -->
                    <%--<telerik:RadTextBox ID="txtLimiteCredito" runat="server" ReadOnly="True" Width="100px" CssClass="hidden-field">
                    </telerik:RadTextBox>--%>
                </td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style1">
                    <asp:Label ID="lblFechaIngreso" runat="server" Text="Fecha ingreso:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <telerik:RadDatePicker ID="dtpFechaIngreso" runat="server" Enabled="false">
                    </telerik:RadDatePicker>
                </td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:Label ID="lblNumeroDocumento" runat="server" Text="Numero documento:"></asp:Label>
                </td>
                <td class="auto-style1">
                    <telerik:RadTextBox ID="txtNumDocumento" runat="server" Width="100px">
                    </telerik:RadTextBox>
                </td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpFecha" runat="server">
                    </telerik:RadDatePicker>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMonto" runat="server" Text="Monto:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtMonto" runat="server" Width="100px">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <telerik:RadButton ID="cmdGuardar" runat="server" Text="Guardar" Width="300px" OnClick="cmdGuardar_Click">
                    </telerik:RadButton>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </div>
        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
        ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true">
    </telerik:RadNotification>
</asp:Content>
