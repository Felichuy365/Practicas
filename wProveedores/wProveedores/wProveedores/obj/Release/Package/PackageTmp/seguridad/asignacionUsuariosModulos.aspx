<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="asignacionUsuariosModulos.aspx.cs" Inherits="wProveedores.seguridad.asignacionUsuariosModulos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="style.css" rel="stylesheet" type="text/css" />
    <style>
        .tablaMantenimiento
        {
            border-style: none;
	        border-color: inherit;
	        border-width: 0;
	        background-image :url('../images/16/cut_26.jpg');
	        width: 712px;
	        height: 125px;
	        text-align:justify;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="#CC0000" Font-Names="Tahoma" Font-Size="Small"></asp:Label>
    <asp:Panel ID="pnConsulta" runat="server">
        <telerik:RadTabStrip ID="tabConsulta" runat="server" SelectedIndex="0"  MultiPageID="RadMultiPage2" >
            <Tabs>
                <telerik:RadTab runat="server" Selected="True" Text="Usuarios por modulo" PageViewID="pConsulta">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage2" runat="server" Width="100%">
            <telerik:RadPageView ID="pConsulta" runat="server" Selected="True" Width="100%">
                <div style="float: left; width: 70%;">
                    <telerik:RadToolBar ID="RadToolBar1" runat="server"  Width="100%" OnButtonClick="RadToolBar1_ButtonClick" >
                        <Items>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/new.png" Text="Nuevo">
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
                <div style="float: left; width: 30%;">
                    <telerik:RadToolBar ID="RadToolBar2" runat="server"  Width="100%" OnButtonClick="RadToolBar2_ButtonClick" >
                        <Items>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/show.png" Text="Todo" Checked="True" CheckOnClick="True">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/active.png" Text="Solo Activos" CheckOnClick="True">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="Ayuda" ImageUrl="~/Images/16/help.png">
                            </telerik:RadToolBarButton>
                        </Items>
                    </telerik:RadToolBar>
                </div>
                <div>
                    <telerik:RadGrid ID="gModuloUsuario" runat="server" DataSourceID="sqlData" ShowFooter="True" OnNeedDataSource="gModuloUsuario_NeedDataSource" OnSelectedIndexChanged="gModuloUsuario_SelectedIndexChanged" AllowFilteringByColumn="True" AllowPaging="True" GroupPanelPosition="Top" OnDeleteCommand="gModuloUsuario_DeleteCommand"  Font-Names="Tahoma" Skin="Default">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False" DataSourceID="sqlData" ShowFooter="False" DataKeyNames="SEG_MUS_Id">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" ShowExportToExcelButton="True" ShowExportToPdfButton="True" ShowExportToWordButton="True"></CommandItemSettings>

                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>

                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldAlias="Usuario" FieldName="nombre" />
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="nombre" SortOrder="Descending" />
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Select" UniqueName="column" ImageUrl="~/images/Edit.gif">
                                    <HeaderStyle Width="25px" />
                                    <ItemStyle Width="25px" />
                                </telerik:GridButtonColumn>
                                <telerik:GridBoundColumn DataField="SEG_MUS_Id" DataType="System.Int32" HeaderText="SEG_MUS_Id" ReadOnly="True" SortExpression="SEG_MUS_Id" UniqueName="SEG_MUS_Id" Visible="false">
                                    <ItemStyle Width="50px" />
                                    <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="modulo" HeaderText="modulo" SortExpression="modulo" UniqueName="modulo">
                                    <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="nombre" HeaderText="usuario" SortExpression="nombre" UniqueName="nombre">
                                    <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Estado" FilterControlAltText="Filter estado column" HeaderText="Estado" SortExpression="Estado" UniqueName="estado">
                                    <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmText="¿Seguro que desea borrar el registro?" FilterControlAltText="Filter DeleteColumn column" ImageUrl="~/images/Delete.gif" Resizable="False" UniqueName="DeleteColumn">
                                    <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                                    <ItemStyle Width="25px" />
                                </telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>

                        <PagerStyle Mode="Slider" />

                        <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="sqlData" runat="server" ConnectionString="<%$ ConnectionStrings:WBSOFTWAREConnectionString %>"></asp:SqlDataSource>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>

    </asp:Panel>
    <asp:Panel ID="pnMantenimiento" runat="server">

        <div style="width:712px; margin:0 auto;">
        <telerik:RadTabStrip ID="tabMantenimiento" runat="server" SelectedIndex="0" MultiPageID="RadMultiPage3"  Width="100%" >

            <Tabs>
                <telerik:RadTab runat="server" PageViewID="pMantenimiento" Selected="True" Text="Usuarios por modulo">
                </telerik:RadTab>
            </Tabs>

        </telerik:RadTabStrip>

        <div>
            <telerik:RadToolBar ID="rtbMenu" runat="server"  Width="100%" OnButtonClick="rtbMenu_ButtonClick" >
                <Items>
                    <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/Save.png" Text="Guardar">
                    </telerik:RadToolBarButton>
                    <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/Cancel.png" Text="Cancelar">
                    </telerik:RadToolBarButton>
                </Items>
            </telerik:RadToolBar>
        </div>
        <table style="font-family: Tahoma; font-size: small; " >
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblCodigo" runat="server" Text="Codigo:"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtCodigo" runat="server"  ReadOnly="True">
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblModulo" runat="server" Text="(*)Modulo:"></asp:Label></td>
                <td>
                    <telerik:RadComboBox ID="cmbModulo" Runat="server" AutoPostBack="True" Height="200px" Width="300px" DataSourceID="sqlModulo" DataTextField="modulo" DataValueField="SEG_MOD_Id" Filter="Contains">
                    </telerik:RadComboBox>
                    <asp:SqlDataSource ID="sqlModulo" runat="server" ConnectionString="<%$ ConnectionStrings:WBSOFTWAREConnectionString %>" SelectCommand="select SEG_MOD_Id, modulo
 from SEG_Modulos
where GLB_EST_Id = 1
 and GLB_EMP_Id = @GLB_EMP_Id
order by modulo">
                        <SelectParameters>
                            <asp:SessionParameter Name="GLB_EMP_Id" SessionField="id_empresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblUsuario" runat="server" Text="(*)Usuario:"></asp:Label></td>
                <td>
                    <telerik:RadComboBox ID="cmbUsuario" Runat="server" AutoPostBack="True" DataSourceID="sqlUsuario" DataTextField="nombre" DataValueField="SEG_USE_Id" Filter="Contains" Height="200px" Width="300px">
                    </telerik:RadComboBox>
                    <asp:SqlDataSource ID="sqlUsuario" runat="server" ConnectionString="<%$ ConnectionStrings:WBSOFTWAREConnectionString %>" SelectCommand="select SEG_USE_Id, nombre
from SEG_Usuarios
where GLB_EST_Id = 1
 and GLB_EMP_Id = @id_empresa
order by nombre ">
                        <SelectParameters>
                            <asp:SessionParameter Name="id_empresa" SessionField="id_empresa" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label></td>
                <td>
                    <telerik:RadTextBox ID="txtEstado" runat="server" ReadOnly="True" >
                    </telerik:RadTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
        </table>
        <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="(*) Los campos que tengan este simbolo son obligatorios."></asp:Label>
        </div>

    </asp:Panel>

    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
                            ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true" >
    </telerik:RadNotification>
</asp:Content>


