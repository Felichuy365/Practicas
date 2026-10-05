<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="consultaValesCafeteria.aspx.cs" Inherits="wProveedores.cafeteria.consultaValesCafeteria" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
                        <Items>
                            <telerik:RadToolBarButton runat="server" Text="PDF" ImageUrl="~/Images/16/pdf.png">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="Excel" ImageUrl="~/Images/16/excel.png">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="Word" ImageUrl="~/Images/16/word.png">
                            </telerik:RadToolBarButton>                            
                        </Items>
                    </telerik:RadToolBar>
    </div>
    <div style="width:712px; margin:0 auto;">
        <table >
            <tr>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRubro" runat="server" Text="Rubro:"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbRubro" Runat="server" Filter="Contains" Width="400px">
                    </telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha inicio:"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpFecha" Runat="server">
                    </telerik:RadDatePicker>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFechaFinal" runat="server" Text="Fecha final:"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpFechaFinal" Runat="server">
                    </telerik:RadDatePicker>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="text-align:center;">
                    <telerik:RadButton ID="cmdGenerar" runat="server" Text="Generar" Width="300px" OnClick="cmdGenerar_Click">
                    </telerik:RadButton>
                        </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div >
        <telerik:RadGrid ID="gVales" runat="server" GroupPanelPosition="Top" CellSpacing="-1" DataSourceID="sqlData" GridLines="Both">
            <MasterTableView DataSourceID="sqlData">
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
        ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true">
    </telerik:RadNotification>
    <asp:SqlDataSource ID="sqlData" runat="server"></asp:SqlDataSource>
    </asp:Content>
