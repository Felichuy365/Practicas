using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;


namespace wProveedores.seguridad
{
    public partial class usuarios : System.Web.UI.Page
    {
        string user = "";
        string id_empresa = "";
        string nombre_empresa = "";
        string id_modulo = "0";
        string idioma = "";
        string maquina = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["username"].ToString();
            id_empresa = Session["id_empresa"].ToString();
            nombre_empresa = Session["nombre_empresa"].ToString();
            id_modulo = Session["id_modulo"].ToString();
            idioma = Session["idioma"].ToString();
            maquina = System.Environment.MachineName;
            cambiaIdioma(idioma);

            if (idioma == "SPA")
            {
                cambiaIdiomaFiltroGrid();
            }
            cargaGrid();
            if (Page.IsPostBack != true)
            {
                if (user == "")
                {
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    verificaPermisos(path);
                    control_enabled(true);
                    carga_roles(id_empresa);
                }
            }
        }

        private void verificaPermisos(string path)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";

            try
            {

                qry = " select agregar, editar, anular, reporte ";
                qry += " from SEG_Permisos_VW ";
                qry += " where replace(navigate_url,'~','') = '" + path + "' ";
                qry += "  and glb_emp_id = " + id_empresa;

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // agregar
                    if (Convert.ToBoolean(dt.Rows[0][0].ToString()) == true)
                    {
                        RadToolBar1.Items[0].Visible = true;
                    }
                    else
                    {
                        RadToolBar1.Items[0].Visible = false;
                    }

                    // editar
                    if (Convert.ToBoolean(dt.Rows[0][1].ToString()) == true)
                    {
                        this.gUsuarios.MasterTableView.Columns[0].Visible = true;
                    }
                    else
                    {
                        this.gUsuarios.MasterTableView.Columns[0].Visible = false;
                    }
                    // anular
                    if (Convert.ToBoolean(dt.Rows[0][2].ToString()) == true)
                    {
                        this.gUsuarios.MasterTableView.Columns[this.gUsuarios.Columns.Count - 1].Visible = true;
                    }
                    else
                    {
                        this.gUsuarios.MasterTableView.Columns[this.gUsuarios.Columns.Count - 1].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
                RadNotification1.Show(ex.Message);
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }

        private void cambiaIdioma(string idioma)
        {
            switch (idioma)
            {
                case "SPA":
                    this.tabConsulta.Tabs[0].Text = "Catalogo de usuarios";
                    RadToolBar1.Items[0].Text = "Nuevo";
                    RadToolBar2.Items[0].Text = "Todos";
                    RadToolBar2.Items[1].Text = "Solo activos";
                    RadToolBar2.Items[2].Text = "Ayuda";

                    this.tabMantenimiento.Tabs[0].Text = "Catalogo de usuarios";
                    rtbMenu.Items[0].Text = "Guardar";
                    rtbMenu.Items[1].Text = "Cancelar";

                    lblCodigo.Text = "Codigo:";
                    this.lblCorreo.Text = "Correo electronico:";
                    this.lblPassowrd.Text = "Contraseña:";
                    this.lblConfirma.Text = "Confirma contraseña:";
                    this.lblGrupoUsuario.Text = "Grupo de usuario:";
                    lblEstado.Text = "Estado";

                    this.gUsuarios.Columns[1].HeaderText = "Codigo";
                    this.gUsuarios.Columns[2].HeaderText = "Usuario";
                    this.gUsuarios.Columns[4].HeaderText = "Nombre";
                    this.gUsuarios.Columns[5].HeaderText = "Grupo de usuario";
                    this.gUsuarios.Columns[6].HeaderText = "Estado";

                    break;
                case "ENG":
                    this.tabConsulta.Tabs[0].Text = "Users";
                    RadToolBar1.Items[0].Text = "New";
                    RadToolBar2.Items[0].Text = "All";
                    RadToolBar2.Items[1].Text = "Active only";
                    RadToolBar2.Items[2].Text = "Help";

                    this.tabMantenimiento.Tabs[0].Text = "Users";
                    rtbMenu.Items[0].Text = "Save";
                    rtbMenu.Items[1].Text = "Cancel";

                    lblCodigo.Text = "Code:";
                    this.lblCorreo.Text = "E-Mail:";
                    this.lblPassowrd.Text = "Password:";
                    this.lblConfirma.Text = "Password confirm:";
                    this.lblGrupoUsuario.Text = "User group:";
                    lblEstado.Text = "State";

                    this.gUsuarios.Columns[1].HeaderText = "Code";
                    this.gUsuarios.Columns[2].HeaderText = "User";
                    this.gUsuarios.Columns[4].HeaderText = "Name";
                    this.gUsuarios.Columns[5].HeaderText = "User group";
                    this.gUsuarios.Columns[6].HeaderText = "State";

                    break;
            }
        }

        private void cargaGrid()
        {
            string qry = "";
            if (idioma == "SPA")
            {
                qry = " SELECT A.SEG_USE_ID CODIGO, A.correoElectronico USUARIO, A.PASS, A.NOMBRE, B.ROL, C.ESTADO ";
                qry += " FROM SEG_USUARIOS A ";
                qry += " INNER JOIN SEG_ROLES B ON A.GLB_EMP_ID = B.GLB_EMP_ID AND A.SEG_ROL_ID = B.SEG_ROL_ID ";
                qry += " INNER JOIN GLB_ESTADOS C ON A.GLB_EST_ID = C.GLB_EST_ID ";
                qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
            }
            else
            {
                qry = " SELECT A.SEG_USE_ID CODIGO, A.correoElectronico USUARIO, A.PASS, A.NOMBRE, B.ROL, C.ESTADO_INGLES ESTADO ";
                qry += " FROM SEG_USUARIOS A ";
                qry += " INNER JOIN SEG_ROLES B ON A.GLB_EMP_ID = B.GLB_EMP_ID AND A.SEG_ROL_ID = B.SEG_ROL_ID ";
                qry += " INNER JOIN GLB_ESTADOS C ON A.GLB_EST_ID = C.GLB_EST_ID ";
                qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
            }
            this.sqlData.SelectCommand = qry;
            sqlData.DataBind();
        }

        private void cambiaIdiomaFiltroGrid()
        {
            GridFilterMenu menu = this.gUsuarios.FilterMenu;
            foreach (RadMenuItem item in menu.Items)
            {
                switch (item.Text)
                {
                    case "NoFilter":
                        item.Text = "Sin Filtro";
                        break;
                    case "EqualTo":
                        item.Text = "Igual A";
                        break;
                    case "NotEqualTo":
                        item.Text = "No Igual A";
                        break;
                    case "GreaterThan":
                        item.Text = "Mayor A";
                        break;
                    case "LessThan":
                        item.Text = "Menor A";
                        break;
                    case "GreaterThanOrEqualTo":
                        item.Text = "Mayor o Igual a";
                        break;
                    case "LessThanOrEqualTo":
                        item.Text = "Menor o Igual a";
                        break;
                    case "Between":
                        item.Text = "Entre";
                        break;
                    case "NotBetween":
                        item.Text = "No esta entre";
                        break;
                    case "IsNull":
                        item.Text = "Nulo";
                        break;
                    case "NotIsNull":
                        item.Text = "No Nulo";
                        break;
                    case "Contains":
                        item.Text = "Contenga";
                        break;
                    case "DoesNotContain":
                        item.Text = "No Contenga";
                        break;
                    case "StartsWith":
                        item.Text = "Inicie Con";
                        break;
                    case "EndsWith":
                        item.Text = "Termine Con";
                        break;
                    case "IsEmpty":
                        item.Text = "Es Vacio";
                        break;
                    case "NotIsEmpty":
                        item.Text = "No Es Vacio";
                        break;
                }
            }
        }
        private void carga_roles(string id_empresa)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " SELECT SEG_ROL_ID, ROL ";
                qry += " FROM SEG_ROLES  ";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND GLB_EST_ID = 1 ";
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cmbRol.Items.Clear();
                    RadComboBoxItem itm1 = new RadComboBoxItem();
                    itm1.Text = "*** SELECCIONE OPCION ***";
                    itm1.Value = "0";
                    cmbRol.Items.Add(itm1);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RadComboBoxItem itm = new RadComboBoxItem();
                        itm.Text = dt.Rows[i][1].ToString();
                        itm.Value = dt.Rows[i][0].ToString();
                        cmbRol.Items.Add(itm);
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }

        private void control_enabled(Boolean valor)
        {
            try
            {
                //txtCodigo.ReadOnly = true;
                //txtUsuario.ReadOnly = valor;
                //txtPassword.ReadOnly = valor;
                //txtNombre.ReadOnly = valor;
                //this.cmbRol.Enabled = !valor;
                //txtEstado.ReadOnly = true;

                //rtbMenu.Items[0].Visible = valor; //nuevo
                //rtbMenu.Items[1].Visible = valor; //editar
                //rtbMenu.Items[2].Visible = valor; //anular

                //rtbMenu.Items[3].Visible = !valor; //anular
                //rtbMenu.Items[4].Visible = !valor; //anular
            }
            catch (Exception ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
            }
        }

        private void limpia()
        {
            txtCodigo.Text = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
            txtNombre.Text = "";
            txtEstado.Text = "";
        }
        protected void rtbMenu_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Index)
            {
                case 0: //guardar
                    if (Page.IsValid)
                    {
                        guardar();
                        limpia();
                        control_enabled(true);
                        pConsulta.Visible = true;
                        pMantenimiento.Visible = false;
                    }
                    break;
                case 1: //cancelar
                    control_enabled(true);
                    limpia();
                    cargaGrid();
                    pConsulta.Visible = true;
                    pMantenimiento.Visible = false;
                    break;
            }
        }
        private void guardar()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                if (txtPassword.Text.Trim() != txtConfirma.Text.Trim())
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('La contraseña ingresada no coincide con la confirmacion de contraseña, por favor verifique.')</script>");
                    return;
                }

                if (txtCodigo.Text == "")
                {
                    qry = " INSERT INTO SEG_USUARIOS ";
                    qry += " (GLB_EMP_ID, SEG_ROL_ID, CORREOELECTRONICO, PASS, NOMBRE, GLB_EST_ID) ";
                    qry += " VALUES ";
                    qry += " (" + id_empresa + ", " + cmbRol.SelectedItem.Value.ToString() + ", '" + txtUsuario.Text + "', '" + txtPassword.Text + "', '" + txtNombre.Text + "', 1) ";
                }
                else
                {
                    if (txtPassword.Text != "")
                    {
                        qry = " UPDATE SEG_USUARIOS SET SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString() + ", CORREOELECTRONICO = '" + txtUsuario.Text + "', PASS = '" + txtPassword.Text + "', NOMBRE = '" + txtNombre.Text + "' ";
                        qry += " WHERE GLB_EMP_ID = " + id_empresa;
                        qry += " AND SEG_USE_ID = " + txtCodigo.Text;
                    }
                    else
                    {
                        qry = " UPDATE SEG_USUARIOS SET SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString() + ", CORREOELECTRONICO = '" + txtUsuario.Text + "', NOMBRE = '" + txtNombre.Text + "' ";
                        qry += " WHERE GLB_EMP_ID = " + id_empresa;
                        qry += " AND SEG_USE_ID = " + txtCodigo.Text;
                    }
                }
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();
                cargaGrid();
                this.gUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }

        protected void gUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            carga_usuarios(txtCodigo.Text = gUsuarios.SelectedItems[0].OwnerTableView.DataKeyValues[gUsuarios.SelectedItems[0].ItemIndex]["CODIGO"].ToString(), id_empresa);
            pConsulta.Visible = false;
            pMantenimiento.Visible = true;
        }

        private void carga_usuarios(string id_usuario, string id_empresa)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            int index;
            try
            {
                qry = " SELECT A.SEG_USE_ID CODIGO, A.CORREOELECTRONICO USUARIO, A.PASS, A.NOMBRE, A.SEG_ROL_ID, B.ROL, C.ESTADO ";
                qry += " FROM SEG_USUARIOS A ";
                qry += " INNER JOIN SEG_ROLES B ON A.GLB_EMP_ID = B.GLB_EMP_ID AND A.SEG_ROL_ID = B.SEG_ROL_ID ";
                qry += " INNER JOIN GLB_ESTADOS C ON A.GLB_EST_ID = C.GLB_EST_ID  ";
                qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                qry += " AND A.SEG_USE_ID = " + id_usuario;

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtCodigo.Text = dt.Rows[0][0].ToString();
                    this.txtUsuario.Text = dt.Rows[0][1].ToString();
                    this.txtPassword.Text = dt.Rows[0][2].ToString();
                    this.txtNombre.Text = dt.Rows[0][3].ToString();
                    //this.cmbRol.SelectedValue = dt.Rows[0][4].ToString();

                    carga_roles(id_empresa);
                    index = this.cmbRol.FindItemIndexByValue(dt.Rows[0][4].ToString());
                    cmbRol.SelectedIndex = index;

                    this.txtEstado.Text = dt.Rows[0][6].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }
        private string busca_rol(string id_rol, string retorna)
        {
            string result = "";
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " SELECT SEG_ROL_ID, ROL ";
                qry += " FROM SEG_ROLES  ";
                qry += " WHERE SEG_ROL_ID = " + id_rol;
                qry += " AND GLB_EMP_Id = " + id_empresa;
                qry += " AND GLB_EST_Id = 1 ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    switch (retorna)
                    {
                        case "1":
                            result = dt.Rows[0][0].ToString();
                            break;
                        case "2":
                            result = dt.Rows[0][1].ToString();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
        protected void cmbRol_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void cmdNuevo_Click(object sender, ImageClickEventArgs e)
        {
            pConsulta.Visible = false;
            pMantenimiento.Visible = true;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Index)
            {
                case 0: //NUEVO
                    pConsulta.Visible = false;
                    pMantenimiento.Visible = true;
                    limpia();
                    break;
                case 2: //excel
                    gUsuarios.ExportSettings.IgnorePaging = true;
                    gUsuarios.ExportSettings.ExportOnlyData = true;
                    gUsuarios.ExportSettings.OpenInNewWindow = true;
                    gUsuarios.MasterTableView.ExportToExcel();
                    break;
                case 4://pdf
                    gUsuarios.MasterTableView.ExportToPdf();
                    break;
                case 3://word
                    this.gUsuarios.ExportSettings.Word.Format = GridWordExportFormat.Html;
                    gUsuarios.ExportSettings.ExportOnlyData = true;
                    gUsuarios.ExportSettings.IgnorePaging = true;
                    gUsuarios.ExportSettings.OpenInNewWindow = true;
                    gUsuarios.ExportSettings.UseItemStyles = true;
                    gUsuarios.MasterTableView.ExportToWord();
                    break;
            }
        }

        protected void RadToolBar2_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string qry = "";

            switch (e.Item.Index)
            {
                case 0: //todos                    
                    if (idioma == "SPA")
                    {
                        qry = " SELECT A.SEG_USE_ID CODIGO, A.CORREOELECTRONICO 'USUARIO', A.PASS, A.NOMBRE, B.ROL, C.ESTADO ";
                        qry += " FROM SEG_USUARIOS A ";
                        qry += " INNER JOIN SEG_ROLES B ON A.GLB_EMP_ID = B.GLB_EMP_ID AND A.SEG_ROL_ID = B.SEG_ROL_ID ";
                        qry += " INNER JOIN GLB_ESTADOS C ON A.GLB_EST_ID = C.GLB_EST_ID  ";
                        qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                    }
                    else
                    {
                        qry = " SELECT A.SEG_USE_ID CODIGO, A.CORREOELECTRONICO 'USUARIO', A.PASS, A.NOMBRE, B.ROL, C.ESTADO_INGLES ESTADO ";
                        qry += " FROM SEG_USUARIOS A ";
                        qry += " INNER JOIN SEG_ROLES B ON A.GLB_EMP_ID = B.GLB_EMP_ID AND A.SEG_ROL_ID = B.SEG_ROL_ID ";
                        qry += " INNER JOIN GLB_ESTADOS C ON A.GLB_EST_ID = C.GLB_EST_ID  ";
                        qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                    }
                    this.sqlData.SelectCommand = qry;
                    sqlData.DataBind();
                    gUsuarios.DataBind();
                    break;
                case 1: //activos
                    if (idioma == "SPA")
                    {
                        qry = " SELECT A.SEG_USE_ID CODIGO, A.CORREOELECTRONICO 'USUARIO', A.PASS, A.NOMBRE, B.ROL, C.ESTADO ";
                        qry += " FROM SEG_USUARIOS A ";
                        qry += " INNER JOIN SEG_ROLES B ON A.GLB_EMP_ID = B.GLB_EMP_ID AND A.SEG_ROL_ID = B.SEG_ROL_ID ";
                        qry += " INNER JOIN GLB_ESTADOS C ON A.GLB_EST_ID = C.GLB_EST_ID  ";
                        qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                        qry += " and a.GLB_EST_ID = 1 ";
                    }
                    else
                    {
                        qry = " SELECT A.SEG_USE_ID CODIGO, A.CORREOELECTRONICO 'USUARIO', A.PASS, A.NOMBRE, B.ROL, C.ESTADO_INGLES ESTADO ";
                        qry += " FROM SEG_USUARIOS A ";
                        qry += " INNER JOIN SEG_ROLES B ON A.GLB_EMP_ID = B.GLB_EMP_ID AND A.SEG_ROL_ID = B.SEG_ROL_ID ";
                        qry += " INNER JOIN GLB_ESTADOS C ON A.GLB_EST_ID = C.GLB_EST_ID  ";
                        qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                        qry += " and a.GLB_EST_ID = 1 ";
                    }
                    this.sqlData.SelectCommand = qry;
                    sqlData.DataBind();
                    gUsuarios.DataBind();
                    break;
            }
        }

        protected void gUsuarios_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string codigo = "";
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                codigo = item.OwnerTableView.DataKeyValues[item.ItemIndex]["CODIGO"].ToString();


                qry = " UPDATE SEG_USUARIOS SET GLB_EST_ID = '2', USUARIOMODIFICACION = '" + user + "', FECHAMODIFICACION = GETDATE(), ORIGEN = '" + maquina + "'";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND SEG_USE_ID = " + codigo;
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();
                cargaGrid();
                this.gUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }
    }
}