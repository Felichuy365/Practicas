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
    public partial class gruposUsuarios : System.Web.UI.Page
    {
        string user = "";
        string id_empresa = "";
        string nombre_empresa = "";
        string id_modulo = "0";
        string maquina = "";
        string idioma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["username"].ToString();
            id_empresa = Session["id_empresa"].ToString();
            nombre_empresa = Session["nombre_empresa"].ToString();
            id_modulo = Session["id_modulo"].ToString();
            idioma = Session["idioma"].ToString();
            cambiaIdioma(idioma);
            maquina = System.Environment.MachineName;

            if (idioma == "SPA")
            {
                cambiaIdiomaFiltroGrid();
            }

            cargaGrid();

            if (Page.IsPostBack != true)
            {
                if (user == "")
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    verificaPermisos(path);
                    control_enabled(true);
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
                        this.gRoles.MasterTableView.Columns[0].Visible = true;
                    }
                    else
                    {
                        this.gRoles.MasterTableView.Columns[0].Visible = false;
                    }
                    // anular
                    if (Convert.ToBoolean(dt.Rows[0][2].ToString()) == true)
                    {
                        this.gRoles.MasterTableView.Columns[this.gRoles.Columns.Count - 1].Visible = true;
                    }
                    else
                    {
                        this.gRoles.MasterTableView.Columns[this.gRoles.Columns.Count - 1].Visible = false;
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
                    this.RadTabStrip1.Tabs[0].Text = "Grupos de usuarios";
                    RadToolBar1.Items[0].Text = "Nuevo";
                    RadToolBar2.Items[0].Text = "Todos";
                    RadToolBar2.Items[1].Text = "Solo activos";
                    RadToolBar2.Items[2].Text = "Ayuda";

                    this.RadTabStrip2.Tabs[0].Text = "Grupos de usuarios";
                    rtbMenu.Items[0].Text = "Guardar";
                    rtbMenu.Items[1].Text = "Cancelar";

                    lblCodigo.Text = "Codigo";
                    lblGrupoUsuarios.Text = "Grupo de usuarios";
                    lblEstado.Text = "Estado";

                    this.gRoles.Columns[1].HeaderText = "Codigo";
                    this.gRoles.Columns[2].HeaderText = "Descripcion";
                    this.gRoles.Columns[3].HeaderText = "Estado";

                    break;
                case "ENG":
                    this.RadTabStrip1.Tabs[0].Text = "Users group";
                    RadToolBar1.Items[0].Text = "New";
                    RadToolBar2.Items[0].Text = "All";
                    RadToolBar2.Items[1].Text = "Only active";
                    RadToolBar2.Items[2].Text = "Help";

                    this.RadTabStrip2.Tabs[0].Text = "Users group";
                    rtbMenu.Items[0].Text = "Save";
                    rtbMenu.Items[1].Text = "Cancel";

                    lblCodigo.Text = "Code";
                    lblGrupoUsuarios.Text = "User group:";
                    lblEstado.Text = "State:";

                    this.gRoles.Columns[1].HeaderText = "Code";
                    this.gRoles.Columns[2].HeaderText = "Description";
                    this.gRoles.Columns[3].HeaderText = "State";

                    break;
            }
        }

        private void cargaGrid()
        {
            string qry = "";
            if (idioma == "SPA")
            {
                qry = " SELECT A.SEG_ROL_ID CODIGO, A.ROL DESCRIPCION, B.ESTADO ";
                qry += " FROM SEG_ROLES A  ";
                qry += " INNER JOIN GLB_ESTADOS B ON A.GLB_EST_Id = B.GLB_EST_Id  ";
                qry += " WHERE A.GLB_EMP_Id = " + id_empresa;
            }
            else
            {
                qry = " SELECT A.SEG_ROL_ID CODIGO, A.ROL DESCRIPCION, B.ESTADO_INGLES ";
                qry += " FROM SEG_ROLES A  ";
                qry += " INNER JOIN GLB_ESTADOS B ON A.GLB_EST_Id = B.GLB_EST_Id  ";
                qry += " WHERE A.GLB_EMP_Id = " + id_empresa;
            }
            this.sqlData.SelectCommand = qry;
            sqlData.DataBind();
        }

        private void cambiaIdiomaFiltroGrid()
        {
            GridFilterMenu menu = this.gRoles.FilterMenu;
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

        private void carga_roles(string id_rol)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " SELECT A.SEG_ROL_ID, A.ROL, B.ESTADO ";
                qry += " FROM SEG_ROLES A  ";
                qry += " INNER JOIN GLB_ESTADOS B ON A.GLB_EST_Id = B.GLB_EST_Id  ";
                qry += " WHERE A.GLB_EMP_Id = " + id_empresa;
                qry += " AND A.SEG_ROL_Id = " + id_rol;
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtCodigo.Text = dt.Rows[0][0].ToString();
                    this.txtDescripcion.Text = dt.Rows[0][1].ToString();
                    this.txtEstado.Text = dt.Rows[0][2].ToString();
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
                //txtDescripcion.ReadOnly = valor;
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
        protected void gRoles_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            cargaGrid();
        }
        protected void gRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            carga_roles(gRoles.SelectedItems[0].OwnerTableView.DataKeyValues[gRoles.SelectedItems[0].ItemIndex]["CODIGO"].ToString());
            pConsulta.Visible = false;
            pMantenimiento.Visible = true;
        }
        protected void gRoles_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //carga_roles(gRoles.SelectedItems[0].OwnerTableView.DataKeyValues[gRoles.SelectedItems[0].ItemIndex]["CODIGO"].ToString());
            //pConsulta.Visible = false;
            //pMantenimiento.Visible = true;
        }
        private void limpia()
        {
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
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
                    pConsulta.Visible = true;
                    pMantenimiento.Visible = false;
                    cargaGrid();
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
            string strRecordkey = "";
            string id_rol = "";
            try
            {
                if (txtCodigo.Text == "")
                {
                    qry = " INSERT INTO SEG_ROLES (GLB_EMP_ID, ROL, GLB_EST_ID, UsuarioIngreso, FechaIngreso, UsuarioModificacion, fechaModificacion, origen) ";
                    qry += " VALUES (" + id_empresa + ", '" + txtDescripcion.Text + "', '1', '" + user + "', getDate(), '" + user + "', getDate(), '" + maquina + "') ";
                }
                else
                {
                    qry = " UPDATE SEG_ROLES SET ROL = '" + txtDescripcion.Text + "', UsuarioModificacion = '" + user + "', fechaModificacion = getDate(), origen = '" + maquina + "'";
                    qry += " WHERE SEG_ROL_ID = " + txtCodigo.Text + " AND GLB_EMP_ID = " + id_empresa;
                }
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();
                cargaGrid();
                this.gRoles.DataBind();
                //asigna listado de opciones
                if (txtCodigo.Text == "")
                {
                    id_rol = buscaIDRol(txtDescripcion.Text);
                    insertaPermisos(id_rol);
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


        private string buscaIDRol(string rol)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string result = "";
            try
            {
                qry = " select SEG_ROL_ID ";
                qry += " from SEG_Roles ";
                qry += " where GLB_EMP_ID = " + id_empresa;
                qry += " and rol = '" + rol + "' ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0][0].ToString();
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

        private void insertaPermisos(string id_rol)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            string qry = "";

            try
            {
                qry = " insert into SEG_Permisos ";
                qry += " (GLB_EMP_ID, SEG_OPC_ID, SEG_MOD_ID, SEG_ROL_ID, acceso, agregar, editar, eliminar, anular, reporte, GLB_EST_Id, ";
                qry += " usuarioIngreso, fechaIngreso, usuarioModificacion, ORIGEN) ";
                qry += " select GLB_EMP_ID, SEG_OPC_ID, '" + id_modulo + "', " + id_rol + ", '1', '1', '1', '1', '1', '1', ";
                qry += " 1, '" + user + "', getDate(), '" + user + "', '" + maquina + "'";
                qry += " from SEG_OPCIONES ";
                qry += " where GLB_EMP_ID = " + id_empresa;

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();
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

        protected void gRoles_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            
            string strRecordkey = "";
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                txtCodigo.Text = item.OwnerTableView.DataKeyValues[item.ItemIndex]["CODIGO"].ToString();

                if (txtCodigo.Text == "")
                {
                    Response.Write("<script LANGUAGE='JavaScript' >alert('Debe de seleccionar un registro, para poderlo anular.')</script>");
                    return;
                }
                if (txtCodigo.Text != "")
                {
                    qry = " UPDATE SEG_Roles SET GLB_EST_ID = '2', USUARIOMODIFICACION = '" + user + "', FECHAMODIFICACION = GETDATE(), ";
                    qry += " ORIGEN ='" + maquina + "'";
                    qry += " WHERE SEG_ROL_ID = " + txtCodigo.Text;
                    qry += " and GLB_EMP_ID = " + id_empresa;
                }
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();

                // pone como anulados todos los permisos
                if (txtCodigo.Text != "")
                {

                    qry = " UPDATE SEG_PERMISOS SET GLB_EST_ID = '2', USUARIOMODIFICACION = '" + user + "', FECHAMODIFICACION = GETDATE(), ";
                    qry += " ORIGEN ='" + maquina + "'";
                    qry += " WHERE SEG_ROL_ID = " + txtCodigo.Text;
                    qry += " and GLB_EMP_ID = " + id_empresa;
                    cmd.CommandText = qry;
                    cmd.ExecuteNonQuery();
                }

                this.cargaGrid();
                this.gRoles.DataBind();
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

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Index)
            {
                case 0: // nuevo
                    pConsulta.Visible = false;
                    pMantenimiento.Visible = true;
                    limpia();
                    break;
                case 2: //excel
                    cargaGrid();
                    gRoles.ExportSettings.IgnorePaging = true;
                    gRoles.ExportSettings.ExportOnlyData = true;
                    gRoles.ExportSettings.OpenInNewWindow = true;
                    gRoles.MasterTableView.ExportToExcel();
                    break;
                case 4: //pdf
                    cargaGrid();
                    gRoles.MasterTableView.ExportToPdf();
                    break;
                case 3: // word
                    cargaGrid();
                    this.gRoles.ExportSettings.Word.Format = GridWordExportFormat.Html;
                    gRoles.ExportSettings.ExportOnlyData = true;
                    gRoles.ExportSettings.IgnorePaging = true;
                    gRoles.ExportSettings.OpenInNewWindow = true;
                    gRoles.ExportSettings.UseItemStyles = true;
                    gRoles.MasterTableView.ExportToWord();
                    break;
            }
        }

        protected void RadToolBar2_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string qry = "";

            switch (e.Item.Index)
            {
                case 0: //todos
                    qry = " SELECT A.SEG_ROL_ID CODIGO, A.ROL DESCRIPCION, B.ESTADO ";
                    qry += " FROM SEG_ROLES A ";
                    qry += " INNER JOIN GLB_ESTADOS B ON A.GLB_EST_ID = B.GLB_EST_ID ";
                    this.sqlData.SelectCommand = qry;
                    sqlData.DataBind();
                    this.gRoles.DataBind();
                    break;
                case 1: //activos
                    qry = " SELECT A.SEG_ROL_ID CODIGO, A.ROL DESCRIPCION, B.ESTADO ";
                    qry += " FROM SEG_ROLES A ";
                    qry += " INNER JOIN GLB_ESTADOS B ON A.GLB_EST_ID = B.GLB_EST_ID ";
                    qry += " where a.GLB_EST_ID = 1 ";
                    this.sqlData.SelectCommand = qry;
                    sqlData.DataBind();
                    this.gRoles.DataBind();
                    break;
            }
        }
    }
}