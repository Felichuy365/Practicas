<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="wProveedores.seguridad.usuarios" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
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
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="#CC0000" Font-Names="Tahoma" Font-Size="Small"></asp:Label>
    <asp:Panel ID="pConsulta" runat="server">
        <telerik:RadTabStrip ID="tabConsulta" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Default" >
            <Tabs>
                <telerik:RadTab runat="server" PageViewID="pConsultaUsuarios" Selected="True" Text="Catálogo de Usuarios">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server">
            <telerik:RadPageView ID="pConsultaUsuarios" runat="server" Selected="True"  Font-Names="Tahoma">
            

            <div style="float:left; width:75%;">
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
        <div style="float:left; width:25%;">
            <telerik:RadToolBar ID="RadToolBar2" runat="server"  Width="100%" OnButtonClick="RadToolBar2_ButtonClick" Font-Names="Tahoma" >
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

        <telerik:RadGrid ID="gUsuarios" runat="server"  DataSourceID="sqlData" OnSelectedIndexChanged="gUsuarios_SelectedIndexChanged" AllowPaging="True" GroupPanelPosition="Top"  OnDeleteCommand="gUsuarios_DeleteCommand" >
            <MasterTableView AllowFilteringByColumn="True" AutoGenerateColumns="False" DataSourceID="sqlData" ShowFooter="True" DataKeyNames="CODIGO">
            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

            <RowIndicatorColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>

            <ExpandCollapseColumn>
            <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Select" UniqueName="column" ImageUrl="~/images/Edit.gif">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="CODIGO" DataType="System.Int32" HeaderText="Codigo" ReadOnly="True" SortExpression="CODIGO" UniqueName="CODIGO" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="USUARIO" HeaderText="Usuario" SortExpression="USUARIO" UniqueName="USUARIO">
                        <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PASS" HeaderText="Contraseña" SortExpression="PASS" UniqueName="PASS" Visible="False">
                        <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" UniqueName="NOMBRE">
                        <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ROL" HeaderText="Grupo de usuario" SortExpression="ROL" UniqueName="ROL">
                        <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" UniqueName="ESTADO">
                        <HeaderStyle Font-Names="Tahoma" Font-Size="Small" />
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
                <asp:SqlDataSource ID="sqlData" runat="server" ConnectionString="<%$ ConnectionStrings:RRHHDMXConnectionString %>">
                    <SelectParameters>
                        <asp:SessionParameter Name="ID_EMPRESA" SessionField="ID_EMPRESA" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </asp:Panel>


    
    <asp:Panel ID="pMantenimiento" runat="server" Visible="false">
            <telerik:RadTabStrip ID="tabMantenimiento" runat="server" MultiPageID="RadMultiPage2" SelectedIndex="0" >
                <Tabs>
                    <telerik:RadTab runat="server" Selected="True" Text="Catálogo de Usuarios" PageViewID="pMantenimientoUsuarios">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage2" runat="server">
                <telerik:RadPageView ID="pMantenimientoUsuarios" runat="server" Selected="True" >
                
                        <telerik:RadToolBar ID="rtbMenu" runat="server"   OnButtonClick="rtbMenu_ButtonClick" Width="100%" Font-Names="Tahoma">
                            <Items>
                                <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/Save.png" Text="Guardar">
                                </telerik:RadToolBarButton>
                                <telerik:RadToolBarButton runat="server" ImageUrl="~/Images/16/Cancel.png" Text="Cancelar">
                                </telerik:RadToolBarButton>
                            </Items>
                        </telerik:RadToolBar>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" Font-Names="Tahoma" />
                        <div style="width:712px; margin:0 auto;">
                            <table style="font-family: Tahoma; font-size: small; width: 100%;" >
                                <tr>
                                    <td colspan="4"><asp:Image ID="Image8" runat="server" ImageUrl="~/images/16/cut_24.jpg" /></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblCodigo" runat="server" Text="Codigo de Usuario:"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtCodigo" Runat="server" Skin="Default" ReadOnly="True">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblCorreo" runat="server" Text="Correo electronico:"></asp:Label>
&nbsp;</td>
                                    <td>
                                        <telerik:RadTextBox ID="txtUsuario" Runat="server" Skin="Default">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Debe de ingresar el usuarios de acceso." ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblPassowrd" runat="server" Text="Contraseña:"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtPassword" Runat="server" MaxLength="20" Skin="Default" TextMode="Password">
                                        </telerik:RadTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsuario" ErrorMessage="Debe de ingresar la contraseña relacionada al usuario de acceso." ForeColor="#CC0000">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblConfirma" runat="server" Text="Confirmar contraseña:"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtConfirma" Runat="server" Skin="Default" MaxLength="20" TextMode="Password">
                                        </telerik:RadTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConfirma" ErrorMessage="Debe de ingresar la confirmacion de contraseña relacionada al usuario de acceso." ForeColor="#CC0000">*</asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtNombre" Runat="server" Skin="Default" Width="300px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNombre" ErrorMessage="Debe de ingresar el nombre del usuario de acceso." ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblGrupoUsuario" runat="server" Text="Grupo de usuario:"></asp:Label></td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbRol" Runat="server" OnSelectedIndexChanged="cmbRol_SelectedIndexChanged" Skin="Default">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtEstado" Runat="server" Skin="Default" ReadOnly="True">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>&nbsp;</td>
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

