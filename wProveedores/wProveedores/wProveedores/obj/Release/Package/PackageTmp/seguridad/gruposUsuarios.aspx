<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="gruposUsuarios.aspx.cs" Inherits="wProveedores.seguridad.gruposUsuarios" %>
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
<asp:Panel ID="pConsulta" runat="server">
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1">
            <Tabs>
                <telerik:RadTab runat="server" Selected="True" Text="Grupos de Usuarios" PageViewID="pConsultaGruposUsuarios">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>

        <%--<div class="textarea">
        <h2>Grupo de Usuarios</h2>        
    </div>
    <div class="clr"></div>    
    <div class="bg"></div>--%>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server"  SelectedIndex="0" BorderColor="#F0F0F0" BorderStyle="Solid" BorderWidth="1px">
            <telerik:RadPageView ID="pConsultaGruposUsuarios" runat="server" Selected="True">
                <div style="float: left; width: 75%;">
                    <telerik:RadToolBar ID="RadToolBar1" runat="server" Width="100%" OnButtonClick="RadToolBar1_ButtonClick">
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
                <div style="float: left; width: 25%; margin-right: 0px;">
                    <telerik:RadToolBar ID="RadToolBar2" runat="server" Width="100%" OnButtonClick="RadToolBar2_ButtonClick">
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
                <br />
                <div>
                    <telerik:RadGrid ID="gRoles" runat="server" Width="100%" DataSourceID="sqlData" OnItemCommand="gRoles_ItemCommand" OnNeedDataSource="gRoles_NeedDataSource" OnSelectedIndexChanged="gRoles_SelectedIndexChanged" AllowPaging="True" AllowSorting="True" GroupPanelPosition="Top" OnDeleteCommand="gRoles_DeleteCommand">
                        <MasterTableView AllowFilteringByColumn="True" AutoGenerateColumns="False" DataKeyNames="CODIGO" DataSourceID="sqlData" ShowFooter="True">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>

                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Select" ImageUrl="~/images/Edit.gif" UniqueName="column">
                                    <HeaderStyle Width="25px" />
                                    <ItemStyle Width="25px" />
                                </telerik:GridButtonColumn>
                                <telerik:GridBoundColumn DataField="CODIGO" DataType="System.Int32" HeaderText="Codigo" ReadOnly="True" SortExpression="CODIGO" UniqueName="CODIGO" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIPCION" HeaderText="Grupo de Usuarios" SortExpression="DESCRIPCION" UniqueName="DESCRIPCION">
                                    <HeaderStyle Font-Names="Arial" Font-Size="Small" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" UniqueName="ESTADO">
                                    <HeaderStyle Font-Names="Arial" Font-Size="Small" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmText="¿Seguro que desea borrar el registro?" FilterControlAltText="Filter DeleteColumn column" ImageUrl="~/images/Delete.gif" Resizable="False" UniqueName="DeleteColumn">
                                    <ItemStyle Width="25px" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <PagerStyle Mode="Slider" />
                        </MasterTableView>

                        <PagerStyle Mode="Slider" />

                        <HeaderContextMenu EnableImageSprites="True" CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
                    </telerik:RadGrid>
                    <asp:SqlDataSource ID="sqlData" runat="server" ConnectionString="<%$ ConnectionStrings:RRHHDMXConnectionString %>"></asp:SqlDataSource>
                </div>
                <div class="cleaner h20"></div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </asp:Panel>



    <asp:Panel ID="pMantenimiento" runat="server" Visible="false">
        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="RadMultiPage2" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab runat="server" Text="Grupos de Usuarios" PageViewID="pMantenimientoGruposUsuario" Selected="True">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage2" runat="server" >
            <telerik:RadPageView ID="pMantenimientoGruposUsuario" runat="server"  Selected="True">
                <telerik:RadToolBar ID="rtbMenu" runat="server" Width="100%" OnButtonClick="rtbMenu_ButtonClick">
                    <Items>
                        <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/Save.png" Text="Guardar">
                        </telerik:RadToolBarButton>
                        <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/Cancel.png" Text="Cancelar">
                        </telerik:RadToolBarButton>
                    </Items>
                </telerik:RadToolBar>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" Font-Names="Arial" />
                <div style="width:712px; margin:0 auto;">
                    <table style="font-family: Tahoma; font-size: small; width: 100%;" class="tablaMantenimiento"; cellpadding="0"; cellspacing="0"; >
                        <tr>
                            <td colspan="4"><asp:Image ID="Image8" runat="server" ImageUrl="~/images/16/cut_24.jpg" /></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="auto-style1">&nbsp;</td>
                            <td class="auto-style2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:Label ID="lblCodigo" runat="server" Text="Codigo:"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <telerik:RadTextBox ID="txtCodigo" runat="server"  ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                            <td class="auto-style2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:Label ID="lblGrupoUsuarios" runat="server" Text="Grupo de usuario:"></asp:Label></td>
                            <td class="auto-style1">
                                <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="300px" >
                                </telerik:RadTextBox>
                            </td>
                            <td class="auto-style2">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Debe de ingresar el nombre del grupo de usuarios." ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label></td>
                            <td class="auto-style1">
                                <telerik:RadTextBox ID="txtEstado" runat="server"  ReadOnly="True">
                                </telerik:RadTextBox>
                            </td>
                            <td class="auto-style2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4"><asp:Image ID="Image9" runat="server" ImageUrl="~/images/16/cut_37.jpg" /></td>
                        </tr>
                    </table>
                </div>

            </telerik:RadPageView>
        </telerik:RadMultiPage>

    </asp:Panel>
    <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
                            ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true" >
    </telerik:RadNotification>
</asp:Content>

