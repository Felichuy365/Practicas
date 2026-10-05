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
    public partial class asignacionUsuariosModulos : System.Web.UI.Page
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
            maquina = System.Environment.MachineName;
            idioma = Session["idioma"].ToString();
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if (idioma == "SPA")
            {
                cambiaIdiomaFiltroGrid();
            }
            cambiaIdioma(idioma);
            cargaGrid();
            id_modulo = Session["id_modulo"].ToString();

            if (Page.IsPostBack != true)
            {
                if (user == "")
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    verificaPermisos(path);
                    pnConsulta.Visible = true;
                    pnMantenimiento.Visible = false;
                    control_enabled(true);
                }
                lblError.Text = "";
            }
        }

        private void verificaPermisos(string path)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WBSOFTWAREConnectionString"].ConnectionString);
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
                        this.gModuloUsuario.MasterTableView.Columns[0].Visible = true;
                    }
                    else
                    {
                        this.gModuloUsuario.MasterTableView.Columns[0].Visible = false;
                    }
                    // anular
                    if (Convert.ToBoolean(dt.Rows[0][2].ToString()) == true)
                    {
                        this.gModuloUsuario.MasterTableView.Columns[this.gModuloUsuario.Columns.Count - 1].Visible = true;
                    }
                    else
                    {
                        this.gModuloUsuario.MasterTableView.Columns[this.gModuloUsuario.Columns.Count - 1].Visible = false;
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
                    this.tabConsulta.Tabs[0].Text = "Usuarios por modulo";
                    RadToolBar1.Items[0].Text = "Nuevo";
                    RadToolBar2.Items[0].Text = "Todos";
                    RadToolBar2.Items[1].Text = "Solo activos";
                    RadToolBar2.Items[2].Text = "Ayuda";

                    this.tabMantenimiento.Tabs[0].Text = "Usuario por modulo";
                    rtbMenu.Items[0].Text = "Guardar";
                    rtbMenu.Items[1].Text = "Cancelar";

                    lblCodigo.Text = "Codigo:";
                    this.lblModulo.Text = "(*)Modulo:";
                    this.lblUsuario.Text = "(*)usuario:";
                    lblEstado.Text = "Estado";

                    this.gModuloUsuario.Columns[1].HeaderText = "Codigo";
                    this.gModuloUsuario.Columns[2].HeaderText = "Modulo";
                    this.gModuloUsuario.Columns[3].HeaderText = "Usuario";
                    this.gModuloUsuario.Columns[4].HeaderText = "Estado";

                    break;
                case "ENG":
                    this.tabConsulta.Tabs[0].Text = "Modules users";
                    RadToolBar1.Items[0].Text = "New";
                    RadToolBar2.Items[0].Text = "All";
                    RadToolBar2.Items[1].Text = "Only actives";
                    RadToolBar2.Items[2].Text = "Help";

                    this.tabMantenimiento.Tabs[0].Text = "Module users";
                    rtbMenu.Items[0].Text = "Save";
                    rtbMenu.Items[1].Text = "Cancel";

                    lblCodigo.Text = "Code:";
                    this.lblModulo.Text = "(*)Module:";
                    this.lblUsuario.Text = "(*)User:";
                    lblEstado.Text = "State";

                    this.gModuloUsuario.Columns[1].HeaderText = "Code";
                    this.gModuloUsuario.Columns[2].HeaderText = "Module";
                    this.gModuloUsuario.Columns[3].HeaderText = "User";
                    this.gModuloUsuario.Columns[4].HeaderText = "State";

                    break;
            }
        }

        private void cargaGrid()
        {
            string qry = "";
            if (idioma == "SPA")
            {
                qry = " select a.SEG_MUS_Id, b.modulo, c.nombre, d.estado ";
                qry += "  from seg_moduloUsuario a ";
                qry += "   inner join SEG_Modulos b on a.SEG_MOD_Id = b.SEG_MOD_Id ";
                qry += "   inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id ";
                qry += "   inner join GLB_Estados d on a.GLB_EST_Id = d.GLB_EST_Id ";
                qry += " where a.GLB_EMP_Id = " + id_empresa;
                qry += " order by c.nombre, b.modulo";
            }
            else
            {
                qry = " select a.SEG_MUS_Id, b.modulo, c.nombre, d.estado_ingles ";
                qry += "  from seg_moduloUsuario a ";
                qry += "   inner join SEG_Modulos b on a.SEG_MOD_Id = b.SEG_MOD_Id ";
                qry += "   inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id ";
                qry += "   inner join GLB_Estados d on a.GLB_EST_Id = d.GLB_EST_Id ";
                qry += " where a.GLB_EMP_Id = " + id_empresa;
                qry += " order by c.nombre, b.modulo";
            }
            this.sqlData.SelectCommand = qry;
            sqlData.DataBind();
        }

        private void control_enabled(Boolean valor)
        {
            lblError.Text = "";
            try
            {
                //txtCodigo.ReadOnly = true;
                //txtPais.ReadOnly = valor;
                //txtEstado.ReadOnly = true;

                //rtbMenu.Items[0].Visible = valor; //nuevo
                //rtbMenu.Items[1].Visible = valor; //editar
                //rtbMenu.Items[2].Visible = valor; //anular

                //rtbMenu.Items[3].Visible = !valor; //anular
                //rtbMenu.Items[4].Visible = !valor; //anular
            }
            catch (Exception ex)
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
                RadNotification1.Show(ex.Message);
            }
        }

        private Boolean valida()
        {
            Boolean result = true;
            if (this.cmbModulo.SelectedItem.Value == "-1")
            {
                if (idioma == "SPA")
                {
                    RadNotification1.Show("Debe de seleccionar el modulo.");
                }
                else
                {
                    RadNotification1.Show("The module is required field.");
                }
                result = false;
            }
            if (this.cmbUsuario.SelectedItem.Value == "-1")
            {
                if (idioma == "SPA")
                {
                    RadNotification1.Show("Debe de seleccionar el usuario.");
                }
                else
                {
                    RadNotification1.Show("The user is required field.");
                }
                result = false;
            }
            return result;
        }

        protected void rtbMenu_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Index)
            {
                case 0:
                    if (valida() == true)
                    {
                        guardar();
                        limpia();
                        control_enabled(true);
                        pnConsulta.Visible = true;
                        pnMantenimiento.Visible = false;
                    }
                    break;
                case 1:
                    pnConsulta.Visible = true;
                    pnMantenimiento.Visible = false;
                    control_enabled(true);
                    limpia();
                    cargaGrid();
                    break;
            }
        }

        private void limpia()
        {
            txtCodigo.Text = "";
            txtEstado.Text = "";
            cmbUsuario.DataBind();
            cmbModulo.DataBind();
        }

        private void guardar()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WBSOFTWAREConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {

                lblError.Text = "";
                if (txtCodigo.Text == "")
                {
                    qry = " insert into SEG_ModuloUsuario (GLB_EMP_Id, SEG_MOD_Id, SEG_USE_Id, GLB_EST_Id, ";
                    qry += " usuarioRegistro, fechaRegistro, usuarioModificacion, fechaModificacion, origen) ";
                    qry += " values (" + id_empresa + ", " + cmbModulo.SelectedItem.Value + ", " + cmbUsuario.SelectedItem.Value + ", 1, ";
                    qry += " '" + user + "', getDate(), '" + user + "', getDate(), '" + maquina + "') ";
                }
                else
                {
                    //strRecordkey = encriptar.recordkey(System.Configuration.ConfigurationManager.ConnectionStrings["Glb_EstadoCivil"].ConnectionString, "GLB_ESC_Id", 2, "GLB_ESC_Id", txtCodigo.Text);

                    qry = " UPDATE SEG_ModuloUsuario SET SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value + ", SEG_USE_ID = " + cmbUsuario.SelectedItem.Value + ", ";
                    qry += " usuarioModificacion = '" + user + "', fechaModificacion = getdate(), origen = '" + maquina + "'";
                    qry += " WHERE SEG_MUS_Id = " + txtCodigo.Text;
                    qry += " and GLB_EMP_Id = " + id_empresa;
                }
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();
                cargaGrid();
                this.gModuloUsuario.DataBind();
            }
            catch (Exception ex)
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
                //RadNotification1.Show(ex.Message);
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

        private void anular()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WBSOFTWAREConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                lblError.Text = "";
                if (txtCodigo.Text == "")
                {
                    if (idioma == "SPA")
                    {
                        Response.Write("<script LANGUAGE='JavaScript' >alert('Debe de seleccionar un registro, para poderlo anular.')</script>");
                    }
                    else
                    {
                        Response.Write("<script LANGUAGE='JavaScript' >alert('You must select a record, in order to be able to cancel it.')</script>");
                    }
                    return;
                }
                if (txtCodigo.Text != "")
                {
                    qry = " UPDATE SEG_ModuloUsuario SET GLB_EST_Id = '2' ";
                    qry += " WHERE SEG_MUS_Id = " + txtCodigo.Text;
                }
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();
                cargaGrid();
            }
            catch (Exception ex)
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
                //RadNotification1.Show(ex.Message);
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
        protected void gModuloUsuario_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }
        protected void gModuloUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaModuloUsuario(this.gModuloUsuario.SelectedItems[0].OwnerTableView.DataKeyValues[gModuloUsuario.SelectedItems[0].ItemIndex]["SEG_MUS_Id"].ToString(), id_empresa);
            pnConsulta.Visible = false;
            pnMantenimiento.Visible = true;
        }

        private void cargaModuloUsuario(string SEG_MUS_Id, string id_empresa)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WBSOFTWAREConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                lblError.Text = "";
                if (idioma == "SPA")
                {
                    qry = " SELECT a.SEG_MUS_Id, a.SEG_MOD_Id, a.SEG_USE_Id, b.estado  ";
                    qry += " FROM SEG_ModuloUsuario a ";
                    qry += " inner join GLB_Estados b on a.GLB_EST_Id = b.GLB_EST_Id ";
                    qry += " where a.SEG_MUS_Id = " + SEG_MUS_Id;
                    qry += " and a.GLB_EMP_Id = " + id_empresa;
                }
                else
                {
                    qry = " SELECT a.SEG_MUS_Id, a.SEG_MOD_Id, a.SEG_USE_Id, b.estado_ingles ";
                    qry += " FROM SEG_ModuloUsuario a ";
                    qry += " inner join GLB_Estados b on a.GLB_EST_Id = b.GLB_EST_Id ";
                    qry += " where a.SEG_MUS_Id = " + SEG_MUS_Id;
                    qry += " and a.GLB_EMP_Id = " + id_empresa;
                }
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtCodigo.Text = dt.Rows[0][0].ToString();
                    this.cmbModulo.SelectedValue = dt.Rows[0][1].ToString();
                    this.cmbUsuario.SelectedValue = dt.Rows[0][2].ToString();
                    this.txtEstado.Text = dt.Rows[0][3].ToString();
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<script LANGUAGE='JavaScript' >alert('" + ex.Message + "')</script>");
                //RadNotification1.Show(ex.Message);
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

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Index)
            {
                case 0:
                    pnConsulta.Visible = false;
                    pnMantenimiento.Visible = true;
                    limpia();
                    break;
                case 2:
                    cargaGrid();
                    gModuloUsuario.ExportSettings.IgnorePaging = true;
                    gModuloUsuario.ExportSettings.ExportOnlyData = true;
                    gModuloUsuario.ExportSettings.OpenInNewWindow = true;
                    gModuloUsuario.MasterTableView.ExportToExcel();
                    break;
                case 4:
                    cargaGrid();
                    gModuloUsuario.MasterTableView.ExportToPdf();
                    break;
                case 3:
                    cargaGrid();
                    this.gModuloUsuario.ExportSettings.Word.Format = GridWordExportFormat.Html;
                    gModuloUsuario.ExportSettings.ExportOnlyData = true;
                    gModuloUsuario.ExportSettings.IgnorePaging = true;
                    gModuloUsuario.ExportSettings.OpenInNewWindow = true;
                    gModuloUsuario.ExportSettings.UseItemStyles = true;
                    gModuloUsuario.MasterTableView.ExportToWord();
                    break;
            }
        }

        protected void RadToolBar2_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            string qry = "";

            switch (e.Item.Index)
            {
                case 0:
                    if (idioma == "SPA")
                    {
                        qry = " select a.SEG_MUS_Id, b.modulo, c.nombre, d.estado ";
                        qry += "  from seg_moduloUsuario a ";
                        qry += "   inner join SEG_Modulos b on a.SEG_MOD_Id = b.SEG_MOD_Id ";
                        qry += "   inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id ";
                        qry += "   inner join GLB_Estados d on a.GLB_EST_Id = d.GLB_EST_Id ";
                        qry += " where a.GLB_EMP_Id = " + id_empresa;
                        qry += " order by c.nombre, b.modulo";
                    }
                    else
                    {
                        qry = " select a.SEG_MUS_Id, b.modulo, c.nombre, d.estado_ingles ";
                        qry += "  from seg_moduloUsuario a ";
                        qry += "   inner join SEG_Modulos b on a.SEG_MOD_Id = b.SEG_MOD_Id ";
                        qry += "   inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id ";
                        qry += "   inner join GLB_Estados d on a.GLB_EST_Id = d.GLB_EST_Id ";
                        qry += " where a.GLB_EMP_Id = " + id_empresa;
                        qry += " order by c.nombre, b.modulo";
                    }
                    this.sqlData.SelectCommand = qry;
                    sqlData.DataBind();
                    this.gModuloUsuario.DataBind();
                    break;
                case 1:
                    if (idioma == "SPA")
                    {
                        qry = " select a.SEG_MUS_Id, b.modulo, c.nombre, d.estado ";
                        qry += "  from seg_moduloUsuario a ";
                        qry += "   inner join SEG_Modulos b on a.SEG_MOD_Id = b.SEG_MOD_Id ";
                        qry += "   inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id ";
                        qry += "   inner join GLB_Estados d on a.GLB_EST_Id = d.GLB_EST_Id ";
                        qry += " where a.GLB_EMP_Id = " + id_empresa;
                        qry += " and a.GLB_EST_Id = 1 ";
                        qry += " order by c.nombre, b.modulo";
                    }
                    else
                    {
                        qry = " select a.SEG_MUS_Id, b.modulo, c.nombre, d.estado_ingles ";
                        qry += "  from seg_moduloUsuario a ";
                        qry += "   inner join SEG_Modulos b on a.SEG_MOD_Id = b.SEG_MOD_Id ";
                        qry += "   inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id ";
                        qry += "   inner join GLB_Estados d on a.GLB_EST_Id = d.GLB_EST_Id ";
                        qry += " where a.GLB_EMP_Id = " + id_empresa;
                        qry += " and a.GLB_EST_Id = 1 ";
                        qry += " order by c.nombre, b.modulo";
                    }
                    this.sqlData.SelectCommand = qry;
                    sqlData.DataBind();
                    this.gModuloUsuario.DataBind();
                    break;
            }

        }

        protected void gModuloUsuario_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["WBSOFTWAREConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                lblError.Text = "";
                cnn.Open();
                cmd.Connection = cnn;
                GridDataItem item = (GridDataItem)e.Item;
                txtCodigo.Text = item.OwnerTableView.DataKeyValues[item.ItemIndex]["SEG_MUS_Id"].ToString();

                if (txtCodigo.Text != "")
                {

                    qry = " UPDATE seg_moduloUsuario SET GLB_EST_Id = '2', ";
                    qry += " usuarioModificacion = '" + user + "', fechaModificacion = getdate(), origen = '" + maquina + "'";
                    qry += " WHERE SEG_MUS_Id = " + txtCodigo.Text;
                    qry += " and GLB_EMP_Id = " + id_empresa;

                    cmd.CommandText = qry;
                    cmd.ExecuteNonQuery();
                    this.gModuloUsuario.Rebind();
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

        private void cambiaIdiomaFiltroGrid()
        {
            GridFilterMenu menu = this.gModuloUsuario.FilterMenu;
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
    }
}