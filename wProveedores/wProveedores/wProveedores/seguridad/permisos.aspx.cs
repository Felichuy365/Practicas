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
    public partial class permisos : System.Web.UI.Page
    {
        string user = "";
        string id_empresa = "";
        string nombre_empresa = "";
        string id_modulo = "0";
        string idioma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["username"].ToString();
            id_empresa = Session["id_empresa"].ToString();
            nombre_empresa = Session["nombre_empresa"].ToString();
            id_modulo = Session["id_modulo"].ToString();
            idioma = Session["idioma"].ToString();
            cambiaIdioma(idioma);
            if (Page.IsPostBack != true)
            {
                if (user == "")
                {
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    carga_roles();
                    carga_modulos();
                    //carga_opciones();
                }
            }
        }

        private void cambiaIdioma(string idioma)
        {
            switch (idioma)
            {
                case "SPA":
                    lblRol.Text = "Grupo de usuario:";
                    cmdConsultar.Text = "Consultar";
                    lblOpcion.Text = "Opcion";
                    lblPermisos.Text = "Permisos";

                    chkAcceso.Text = "Acceso";
                    chkEditar.Text = "Editar";
                    chkEliminar.Text = "Eliminar";
                    chkAnular.Text = "Anular";
                    chkReporte.Text = "Reporte";

                    break;
                case "ENG":
                    lblRol.Text = "User group:";
                    cmdConsultar.Text = "Consult";
                    lblOpcion.Text = "Opcions";
                    lblPermisos.Text = "Permits";

                    chkAcceso.Text = "Access";
                    chkEditar.Text = "Edit";
                    chkEliminar.Text = "Delete";
                    chkAnular.Text = "Cancel";
                    chkReporte.Text = "Report";
                    break;
            }
        }

        private void carga_modulos()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " SELECT SEG_MOD_ID, MODULO ";
                qry += " FROM SEG_MODULOS ";
                qry += " WHERE GLB_EST_Id = 1";
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cmbModulo.Items.Clear();
                    RadComboBoxItem itm1 = new RadComboBoxItem();
                    itm1.Text = "*** SELECCIONE OPCION ***";
                    itm1.Value = "-1";
                    cmbModulo.Items.Add(itm1);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RadComboBoxItem itm = new RadComboBoxItem();
                        itm.Text = dt.Rows[i][1].ToString();
                        itm.Value = dt.Rows[i][0].ToString();
                        cmbModulo.Items.Add(itm);
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


        private void carga_roles()
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
                    itm1.Value = "-1";
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


        private void carga_opciones()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";

            SqlConnection cnn1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd1 = new SqlCommand();
            SqlDataAdapter adp1 = new SqlDataAdapter();
            DataTable dt1 = new DataTable();

            SqlConnection cnn2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd2 = new SqlCommand();
            SqlDataAdapter adp2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();

            SqlConnection cnn3 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd3 = new SqlCommand();
            SqlDataAdapter adp3 = new SqlDataAdapter();
            DataTable dt3 = new DataTable();

            try
            {
                cnn.Open();
                cmd.Connection = cnn;

                cnn1.Open();
                cmd1.Connection = cnn1;

                cnn2.Open();
                cmd2.Connection = cnn2;

                cnn3.Open();
                cmd3.Connection = cnn2;

                rtvOpciones.Nodes.Clear();
                dt3.Clear();
                if (idioma == "SPA")
                {
                    qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                    qry += " FROM SEG_PERMISOS A ";
                    qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                    qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                    qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                    qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                    qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                    qry += "    AND LEN(A.SEG_OPC_ID) = 2 ";
                    qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                }
                else
                {
                    qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                    qry += " FROM SEG_PERMISOS A ";
                    qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                    qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                    qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                    qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                    qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                    qry += "    AND LEN(A.SEG_OPC_ID) = 2 ";
                    qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                }
                cmd3.CommandText = qry;
                adp3.SelectCommand = cmd3;
                adp3.Fill(dt3);

                if (dt3.Rows.Count > 0)
                {

                    for (int contModulo = 0; contModulo <= dt3.Rows.Count - 1; contModulo++)
                    {

                        RadTreeNode nodo_modulo = new RadTreeNode();
                        nodo_modulo.Text = dt3.Rows[contModulo][3].ToString();
                        nodo_modulo.Value = dt3.Rows[contModulo][1].ToString();
                        rtvOpciones.Nodes.Add(nodo_modulo);

                        if (idioma == "SPA")
                        {
                            qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                            qry += " FROM SEG_PERMISOS A ";
                            qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                            qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                            qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                            qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                            qry += "    AND C.SEG_OPC_PADRE = '" + dt3.Rows[contModulo][1].ToString() + "'";
                            qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                            qry += "    AND LEN(A.SEG_OPC_ID)=5 ";
                            qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                        }
                        else
                        {
                            qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                            qry += " FROM SEG_PERMISOS A ";
                            qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                            qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                            qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                            qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                            qry += "    AND C.SEG_OPC_PADRE = '" + dt3.Rows[contModulo][1].ToString() + "'";
                            qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                            qry += "    AND LEN(A.SEG_OPC_ID)=5 ";
                            qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                        }
                        cmd.CommandText = qry;
                        adp.SelectCommand = cmd;
                        dt.Clear();
                        adp.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                RadTreeNode nodo_padre = new RadTreeNode();
                                nodo_padre.Text = dt.Rows[i][3].ToString();
                                nodo_padre.Value = dt.Rows[i][1].ToString();
                                nodo_modulo.Nodes.Add(nodo_padre);

                                if (idioma == "SPA")
                                {
                                    qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                    qry += " FROM SEG_PERMISOS A ";
                                    qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                                    qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                                    qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                                    qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                                    qry += "    AND C.SEG_OPC_PADRE = '" + dt.Rows[i][1].ToString() + "' ";
                                    qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                                    qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                }
                                else
                                {
                                    qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                    qry += " FROM SEG_PERMISOS A ";
                                    qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                                    qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                                    qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                                    qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                                    qry += "    AND C.SEG_OPC_PADRE = '" + dt.Rows[i][1].ToString() + "' ";
                                    qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                                    qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                }
                                dt1.Clear();
                                cmd1.CommandText = qry;
                                adp1.SelectCommand = cmd1;
                                adp1.Fill(dt1);

                                if (dt1.Rows.Count > 0)
                                {
                                    for (int x = 0; x <= dt1.Rows.Count - 1; x++)
                                    {
                                        RadTreeNode nodo_hijo = new RadTreeNode();
                                        nodo_hijo.Text = dt1.Rows[x][3].ToString();
                                        nodo_hijo.Value = dt1.Rows[x][1].ToString();
                                        nodo_padre.Nodes.Add(nodo_hijo);

                                        if (idioma == "SPA")
                                        {
                                            qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                            qry += " FROM SEG_PERMISOS A ";
                                            qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                                            qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                                            qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                                            qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                                            qry += "    AND C.SEG_OPC_PADRE = '" + nodo_hijo.Value.ToString() + "' ";
                                            qry += "    AND LEN(C.SEG_OPC_PADRE) >= 10  ";
                                            qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                                            qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                        }
                                        else
                                        {
                                            qry = " SELECT A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                            qry += " FROM SEG_PERMISOS A ";
                                            qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID and a.glb_emp_id = b.GLB_EMP_Id";
                                            qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID and a.SEG_MOD_Id = c.SEG_MOD_Id and a.GLB_EMP_Id = c.GLB_EMP_Id ";
                                            qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                                            qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                                            qry += "    AND C.SEG_OPC_PADRE = '" + nodo_hijo.Value.ToString() + "' ";
                                            qry += "    AND LEN(C.SEG_OPC_PADRE) >= 10  ";
                                            qry += "    AND A.SEG_MOD_Id = " + this.cmbModulo.SelectedItem.Value.ToString();
                                            qry += " GROUP BY A.SEG_PER_ID, A.SEG_OPC_ID, C.SEG_OPC_PADRE, C.OPCION_INGLES, A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                                        }
                                        dt2.Clear();
                                        cmd2.CommandText = qry;
                                        adp2.SelectCommand = cmd2;
                                        adp2.Fill(dt2);

                                        if (dt2.Rows.Count > 0)
                                        {
                                            for (int z = 0; z <= dt2.Rows.Count - 1; z++)
                                            {
                                                RadTreeNode nodo_hijo1 = new RadTreeNode();
                                                nodo_hijo1.Text = dt2.Rows[z][3].ToString();
                                                nodo_hijo1.Value = dt2.Rows[z][1].ToString();
                                                nodo_hijo.Nodes.Add(nodo_hijo1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                rtvOpciones.ExpandAllNodes();
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
                if (cnn1.State == ConnectionState.Open)
                {
                    cnn1.Close();
                }
                if (cnn2.State == ConnectionState.Open)
                {
                    cnn2.Close();
                }
                if (cnn3.State == ConnectionState.Open)
                {
                    cnn3.Close();
                }
            }
        }
        protected void cmbRol_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void rtvOpciones_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " SELECT A.ACCESO, A.AGREGAR, A.EDITAR, A.ELIMINAR, A.ANULAR, A.REPORTE ";
                qry += " FROM SEG_PERMISOS A ";
                qry += " INNER JOIN SEG_ROLES B ON A.SEG_ROL_ID = B.SEG_ROL_ID ";
                qry += " INNER JOIN SEG_OPCIONES C ON A.SEG_OPC_ID = C.SEG_OPC_ID ";
                qry += " WHERE A.GLB_EMP_ID = " + id_empresa;
                qry += "    AND A.SEG_ROL_ID = " + cmbRol.SelectedItem.Value.ToString();
                qry += "    AND C.SEG_OPC_ID = '" + e.Node.Value.ToString() + "' ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    chkAcceso.Checked = (Boolean)dt.Rows[0][0];
                    chkAgregar.Checked = (Boolean)dt.Rows[0][1];
                    chkEditar.Checked = (Boolean)dt.Rows[0][2];
                    chkEliminar.Checked = (Boolean)dt.Rows[0][3];
                    chkAnular.Checked = (Boolean)dt.Rows[0][4];
                    chkReporte.Checked = (Boolean)dt.Rows[0][5];
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
        protected void chkAcceso_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string valor = "0";
            try
            {
                if (chkAcceso.Checked == true)
                {
                    valor = "1";
                }
                else
                {
                    valor = "0";
                }
                qry = " UPDATE SEG_PERMISOS ";
                qry += " SET ACCESO= '" + valor + "'";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND SEG_ROL_ID = " + cmbRol.SelectedItem.Value;
                qry += " AND SEG_OPC_ID =  '" + rtvOpciones.SelectedNode.Value + "'";
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
        protected void cmdConsultar_Click(object sender, EventArgs e)
        {
            carga_opciones();
        }
        protected void chkAgregar_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string valor = "0";
            try
            {
                if (this.chkAgregar.Checked == true)
                {
                    valor = "1";
                }
                else
                {
                    valor = "0";
                }
                qry = " UPDATE SEG_PERMISOS ";
                qry += " SET AGREGAR= '" + valor + "'";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND SEG_ROL_ID = " + cmbRol.SelectedItem.Value;
                qry += " AND SEG_OPC_ID =  '" + rtvOpciones.SelectedNode.Value + "'";
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
        protected void chkEditar_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string valor = "0";
            try
            {
                if (chkEditar.Checked == true)
                {
                    valor = "1";
                }
                else
                {
                    valor = "0";
                }
                qry = " UPDATE SEG_PERMISOS ";
                qry += " SET EDITAR= '" + valor + "'";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND SEG_ROL_ID = " + cmbRol.SelectedItem.Value;
                qry += " AND SEG_OPC_ID =  '" + rtvOpciones.SelectedNode.Value + "'";
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
        protected void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string valor = "0";
            try
            {
                if (chkEliminar.Checked == true)
                {
                    valor = "1";
                }
                else
                {
                    valor = "0";
                }
                qry = " UPDATE SEG_PERMISOS ";
                qry += " SET ELIMINAR= '" + valor + "'";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND SEG_ROL_ID = " + cmbRol.SelectedItem.Value;
                qry += " AND SEG_OPC_ID =  '" + rtvOpciones.SelectedNode.Value + "'";
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
        protected void chkAnular_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string valor = "0";
            try
            {
                if (chkAnular.Checked == true)
                {
                    valor = "1";
                }
                else
                {
                    valor = "0";
                }
                qry = " UPDATE SEG_PERMISOS ";
                qry += " SET ANULAR= '" + valor + "'";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND SEG_ROL_ID = " + cmbRol.SelectedItem.Value;
                qry += " AND SEG_OPC_ID =  '" + rtvOpciones.SelectedNode.Value + "'";
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
        protected void chkReporte_CheckedChanged(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string valor = "0";
            try
            {
                if (chkReporte.Checked == true)
                {
                    valor = "1";
                }
                else
                {
                    valor = "0";
                }
                qry = " UPDATE SEG_PERMISOS ";
                qry += " SET REPORTE= '" + valor + "'";
                qry += " WHERE GLB_EMP_ID = " + id_empresa;
                qry += " AND SEG_ROL_ID = " + cmbRol.SelectedItem.Value;
                qry += " AND SEG_OPC_ID =  '" + rtvOpciones.SelectedNode.Value + "'";
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
    }
}