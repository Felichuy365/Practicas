<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="permisos.aspx.cs" Inherits="wProveedores.seguridad.permisos" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div style="width:712px; margin:0 auto;">
        <div style="width:100%;">
        
        <table style="font-family: Tahoma; font-size: small; width:100%" >
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Label ID="lblModulo" runat="server" Text="Modulo:"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbModulo" Runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cmbRol_SelectedIndexChanged">
                    </telerik:RadComboBox>
                    </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                        &nbsp;</td>
                <td>
                        &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Label ID="lblRol" runat="server" Text="Rol de usuario:"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbRol" Runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="cmbRol_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
                <td>
                        &nbsp;</td>
                <td>
                        &nbsp;</td>
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
                        &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                        &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                        <telerik:RadButton ID="cmdConsultar" runat="server" Text="Consultar" OnClick="cmdConsultar_Click" style="top: 0px; left: 1px">
                        </telerik:RadButton>                    
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                        &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            </table>
        
    </div>
    </div>
    <div style="width:712px; margin:0 auto;">
    <div id="dvDetalle" style="width:420px; float:left;" >
        <h3><asp:Label ID="lblOpcion" runat="server" Text="Opcion"></asp:Label>
                </h3>
        <telerik:RadTreeView ID="rtvOpciones" Runat="server" Width="100%"  OnNodeClick="rtvOpciones_NodeClick" Height="600px" Font-Names="Tahoma" Font-Size="Small">
        </telerik:RadTreeView>        

    </div>
    <div id="dvPermisos" style="width:275px; float:left;" >
        <h3><asp:Label ID="lblPermisos" runat="server" Text="Permisos"></asp:Label></h3>
        <div style="text-align: justify;">
            <table style="width:100%; font-family: tahoma; font-size: small;">
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkAcceso" runat="server" Text="Acceso" AutoPostBack="True" OnCheckedChanged="chkAcceso_CheckedChanged"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkAgregar" runat="server"  Text="Agregar" AutoPostBack="True" OnCheckedChanged="chkAgregar_CheckedChanged"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkEditar" runat="server"  Text="Editar" AutoPostBack="True" OnCheckedChanged="chkEditar_CheckedChanged"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkEliminar" runat="server"  Text="Eliminar" AutoPostBack="True" OnCheckedChanged="chkEliminar_CheckedChanged"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkAnular" runat="server"  Text="Anular" AutoPostBack="True" OnCheckedChanged="chkAnular_CheckedChanged"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chkReporte" runat="server"  Text="Reporte" AutoPostBack="True" OnCheckedChanged="chkReporte_CheckedChanged"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
    </div>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
                            ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true" >
    </telerik:RadNotification>
</asp:Content>

