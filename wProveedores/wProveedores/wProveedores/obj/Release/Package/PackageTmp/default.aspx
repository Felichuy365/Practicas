<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="wProveedores._default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modulos DMX</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- Main CSS --> 
    <link rel="stylesheet" href="css/style.css">
    <style type="text/css">
        .auto-style1 {
            width: 485px;
        }
    </style>
</head>
<body>
        
        <!-- Header -->
        <div class="header-wrap d-none d-md-block">
            <div class="container">
                <div class="row">
                    
                    <!-- Left header box -->
                    <header class="col-6 text-left">
                        <h1><span>Mano</span>DeObra</h1>
                    </header>
                    
                    <!-- Right header box -->
                    <%--<div class="col-6 text-right">               
                        <p class="header-social-icons social-icons">
                            <a href="#"><i class="fa fa-facebook fa-2x"></i></a>
                            <a href="#"><i class="fa fa-twitter fa-2x"></i></a>
                            <a href="#"><i class="fa fa-youtube fa-2x"></i></a>
                            <a href="#"><i class="fa fa-instagram fa-2x"></i></a>
                        </p>
                    </div>--%>
                </div>
            </div>
        </div>

        
        <!-- Main navigation -->
<%--        <nav class="navbar navbar-expand-md navbar-dark bg-primary">
            <div class="container">
                
                <!-- Company name shown on mobile -->
                <a class="navbar-brand d-md-none d-lg-none d-xl-none" href="#"><span>Marcaje</span>Acceso</a>

                <!-- Mobile menu toggle -->
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#mainNavbar" aria-controls="mainNavbar" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <!-- Main navigation items -->
                <div class="collapse navbar-collapse" id="mainNavbar">
                    <ul class="navbar-nav mr-auto">
                        <li class="nav-item">
                                <a class="nav-link" href="index.html">Home <span class="sr-only">(current)</span></a>
                        </li>

                        <li class="nav-item dropdown active">
                                    <a class="nav-link dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false">Examples &amp; Pages</a>
                                    <div class="dropdown-menu navbar-dark bg-primary">
                                          <a class="dropdown-item" href="examples.html">Style Examples</a>
                                          <a class="dropdown-item" href="three-column.html">Three Column</a>
                                          <a class="dropdown-item active" href="one-column.html">One column / no sidebar</a>
                                          <a class="dropdown-item"  href="text.html">Text / left sidebar</a>
                                    </div>
                        </li>


                        <li class="nav-item">
                                <a class="nav-link" href="#">Services</a>
                        </li>

                        <li class="nav-item">
                                <a class="nav-link" href="#">Products</a>
                        </li>

                        <li class="nav-item">
                                <a class="nav-link" href="#">Contact</a>
                        </li>
                    </ul>                    
                    
                </div>
            </div>
        </nav>--%>


        <!-- Jumbtron / Slider -->
        <%--<div class="jumbotron-wrap"><div class="container">
                <div class="jumbotron">
                    <p class="lead text-center">&nbsp;</p>
                </div>
            </div>            
        </div>--%>

        <!-- Main content area -->
        <form id="form1" runat="server">    
             <asp:Label ID="lblError" runat="server" ForeColor="#CC3300"></asp:Label>   
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                    </asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                    </asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                    </asp:ScriptReference>
                </Scripts>
            </telerik:RadScriptManager>
            <main class="container">
                <table style="width: 100%; height: 600px;">
                                <tr>
                                    <td class="auto-style1">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">&nbsp;</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>Empresa:</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbEmpresas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbEmpresas_SelectedIndexChanged"  Width="200px">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Usuario:</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtUsuario" runat="server"  Width="200px" AutoPostBack="True" LabelWidth="80px" Resize="None">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Contraseña:</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtPassword" runat="server" TextMode="Password"  Width="200px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Modulo:</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <telerik:RadComboBox ID="cmbModulos" runat="server" AutoPostBack="True"  Width="200px" DataSourceID="sqlModulos" DataTextField="modulo" DataValueField="SEG_MOD_Id" >
                                                    </telerik:RadComboBox>
                                                    <asp:SqlDataSource ID="sqlModulos" runat="server" ConnectionString="<%$ ConnectionStrings:RRHHDMXConnectionString %>" SelectCommand="select b.SEG_MOD_Id, b.modulo
 from SEG_ModuloUsuario a
  inner join SEG_Modulos b on a.SEG_MOD_Id = b.SEG_MOD_Id
  inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id
where a.GLB_EMP_Id = @GLB_EMP_Id
 and c.correoElectronico = @usuario
 and b.SEG_MOD_Id = 2
order by b.modulo">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="cmbEmpresas" Name="GLB_EMP_Id" PropertyName="SelectedValue" />
                                                            <asp:ControlParameter ControlID="txtUsuario" Name="usuario" PropertyName="Text" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <div style="text-align: center;">
                                                        <telerik:RadButton ID="cmdAceptar" runat="server" Text="Aceptar" Width="200px" OnClick="cmdAceptar_Click" style="position: relative;">
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
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style1">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
            </main>
            <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" Width="350px" Height="150px" Title="Informacion" TitleIcon="warning"
                            ContentIcon="info" Position="Center" AutoCloseDelay="5000" EnableRoundedCorners="true" EnableShadow="true" >
    </telerik:RadNotification>
        </form>
<%--        <footer class="footer">
            
            
            <div class="footer-bottom">
                    <p class="text-center">Sistema de marcaje</p>                    
            </div>
            
        </footer>--%>



        <!-- Bootcamp JavaScript -->
        <!-- jQuery first, then Popper.js, then Bootstrap JS -->
        <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>

    </body>
</html>
