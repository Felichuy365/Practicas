using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace wProveedores
{
    public partial class main : System.Web.UI.MasterPage
    {
        string user = "";
        string id_empresa = "";
        string nombre_empresa = "";
        string id_modulo = "0";
        string id_rol = "0";
        string idioma = "SPA";
        string modulo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["username"].ToString();
            id_empresa = Session["id_empresa"].ToString();
            nombre_empresa = Session["nombre_empresa"].ToString();
            id_rol = Session["id_rol"].ToString();
            id_modulo = Session["id_modulo"].ToString();
            idioma = Session["idioma"].ToString();
            modulo = Session["modulo"].ToString();
            //cambaiIdioma(idioma);

            if (Page.IsPostBack != true)
            {
                if (user == "")
                {
                    Response.Redirect("~/login.aspx");
                }
                else
                {
                    lblModulo.Text = modulo;
                    carga_opciones_modulo();
                }
            }
        }

        //procedimiento que carga los modulos, si la empresa posee acceso
        private void carga_modulos()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " SELECT MODULO, ICONO ";
                qry += " FROM RRHH..GEN_MODULOS A ";
                qry += " INNER JOIN RRHH..SEG_EMPRESAS_MODULOS B ON (A.ID_MODULO = B.ID_MODULO) ";
                qry += " WHERE B.ID_EMPRESA = " + id_empresa;
                qry += " AND B.ACCESO = '1' ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    //RadToolBarButton itm1 = new RadToolBarButton("INICIO");
                    //itm1.ImageUrl = "~/Images/32/Home.png";
                    //RadToolBar1.Items.Add(itm1);

                    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    //{
                    //    RadToolBarButton itm = new RadToolBarButton(dt.Rows[i][0].ToString());
                    //    itm.ImageUrl = dt.Rows[i][1].ToString();
                    //    RadToolBar1.Items.Add(itm);
                    //}

                    //RadToolBarButton itmSoporte = new RadToolBarButton("SOPORTE");
                    //itmSoporte.ImageUrl = "~/Images/32/support-32.png";
                    //RadToolBar1.Items.Add(itmSoporte);
                    //RadToolBarButton itmCerrarSesion = new RadToolBarButton("CERRAR SESION");
                    //itmCerrarSesion.ImageUrl = "~/Images/32/door_out.png";
                    //RadToolBar1.Items.Add(itmCerrarSesion);
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

        private void carga_opciones_modulo()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();

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

            SqlConnection cnn4 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd4 = new SqlCommand();
            SqlDataAdapter adp4 = new SqlDataAdapter();
            DataTable dt4 = new DataTable();

            string qry = "";
            try
            {



                cnn1.Open();
                cmd1.Connection = cnn1;

                cnn2.Open();
                cmd2.Connection = cnn2;

                cnn3.Open();
                cmd3.Connection = cnn3;

                cnn4.Open();
                cmd4.Connection = cnn4;

                this.mnuOpciones.Items.Clear();


                if (idioma == "SPA")
                {
                    qry = " SELECT A.GLB_EMP_ID, B.SEG_OPC_ID, B.OPCION, B.NAVIGATE_URL, B.SEG_OPC_PADRE, B.ICONO  ";
                    qry += " FROM SEG_PERMISOS A  ";
                    qry += "  INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_Id  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                    qry += " WHERE A.ACCESO = '1'  ";
                    qry += "  AND LEN(B.SEG_OPC_ID) =2  ";
                    qry += "  AND A.GLB_EMP_ID = " + id_empresa;
                    qry += "  AND A.GLB_EST_ID = 1 ";
                    qry += "  AND A.SEG_ROL_ID = " + id_rol;
                    qry += "  AND A.SEG_MOD_ID = " + id_modulo;
                    qry += " ORDER BY B.SEG_OPC_ID ";
                }
                else
                {
                    qry = " SELECT A.GLB_EMP_ID, B.SEG_OPC_ID, B.OPCION_INGLES, B.NAVIGATE_URL, B.SEG_OPC_PADRE, B.ICONO  ";
                    qry += " FROM SEG_PERMISOS A  ";
                    qry += "  INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_Id  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                    qry += " WHERE A.ACCESO = '1'  ";
                    qry += "  AND LEN(B.SEG_OPC_ID) =2  ";
                    qry += "  AND A.GLB_EMP_ID = " + id_empresa;
                    qry += "  AND A.GLB_EST_ID = 1 ";
                    qry += "  AND A.SEG_ROL_ID = " + id_rol;
                    qry += "  AND A.SEG_MOD_ID = " + id_modulo;
                    qry += " ORDER BY B.SEG_OPC_ID ";
                }

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                RadMenuItem itm0 = new RadMenuItem();
                itm0.Value = "0";
                itm0.Text = "Inicio";
                itm0.NavigateUrl = "~/main.aspx";
                mnuOpciones.Items.Add(itm0);

                if (dt.Rows.Count > 0)
                {
                    for (int contModulos = 0; contModulos <= dt.Rows.Count - 1; contModulos++)
                    {
                        RadMenuItem itmModulo = new RadMenuItem();
                        itmModulo.Value = dt.Rows[contModulos][1].ToString();
                        itmModulo.Text = dt.Rows[contModulos][2].ToString();
                        itmModulo.ImageUrl = dt.Rows[contModulos][5].ToString();
                        itmModulo.NavigateUrl = dt.Rows[contModulos][3].ToString();
                        mnuOpciones.Items.Add(itmModulo);

                        if (idioma == "SPA")
                        {
                            qry = " SELECT A.GLB_EMP_ID, B.SEG_OPC_ID, B.OPCION, B.NAVIGATE_URL, B.SEG_OPC_PADRE ";
                            qry += " FROM SEG_PERMISOS A  ";
                            qry += " INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_ID  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                            qry += " WHERE A.ACCESO = '1'  ";
                            qry += " AND SUBSTRING(B.SEG_OPC_ID,1,2)='" + dt.Rows[contModulos][1].ToString() + "'";
                            qry += " AND LEN(B.SEG_OPC_ID) = 5  ";
                            qry += " AND A.GLB_EMP_ID = " + id_empresa;
                            qry += " AND A.GLB_EST_ID = 1 ";
                            qry += " AND A.SEG_ROL_ID = " + id_rol;
                            qry += " AND A.SEG_MOD_ID = " + id_modulo;
                            qry += " ORDER BY B.SEG_OPC_ID  ";
                        }
                        else
                        {
                            qry = " SELECT A.GLB_EMP_ID, B.SEG_OPC_ID, B.OPCION_INGLES, B.NAVIGATE_URL, B.SEG_OPC_PADRE ";
                            qry += " FROM SEG_PERMISOS A  ";
                            qry += " INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_ID  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                            qry += " WHERE A.ACCESO = '1'  ";
                            qry += " AND SUBSTRING(B.SEG_OPC_ID,1,2)='" + dt.Rows[contModulos][1].ToString() + "'";
                            qry += " AND LEN(B.SEG_OPC_ID) = 5  ";
                            qry += " AND A.GLB_EMP_ID = " + id_empresa;
                            qry += " AND A.GLB_EST_ID = 1 ";
                            qry += " AND A.SEG_ROL_ID = " + id_rol;
                            qry += " AND A.SEG_MOD_ID = " + id_modulo;
                            qry += " ORDER BY B.SEG_OPC_ID  ";
                        }
                        dt2.Clear();
                        cmd2.CommandText = qry;
                        adp2.SelectCommand = cmd2;
                        adp2.Fill(dt2);

                        if (dt2.Rows.Count > 0)
                        {
                            for (int contOpciones = 0; contOpciones <= dt2.Rows.Count - 1; contOpciones++)
                            {
                                RadMenuItem itmPadre = new RadMenuItem();
                                itmPadre.Value = dt2.Rows[contOpciones][1].ToString();
                                itmPadre.Text = dt2.Rows[contOpciones][2].ToString();
                                //itmPadre.ImageUrl = dt2.Rows[contOpciones][5].ToString();
                                itmPadre.NavigateUrl = dt2.Rows[contOpciones][3].ToString();
                                itmModulo.Items.Add(itmPadre);

                                if (idioma == "SPA")
                                {
                                    qry = " SELECT A.GLB_EMP_Id, B.SEG_OPC_ID, B.OPCION, B.NAVIGATE_URL, B.SEG_OPC_PADRE ";
                                    qry += " FROM SEG_PERMISOS A  ";
                                    qry += "    INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_ID  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                                    qry += " WHERE A.ACCESO = '1'  ";
                                    qry += "    AND B.SEG_OPC_PADRE = '" + dt2.Rows[contOpciones][1].ToString() + "'";
                                    qry += "    AND LEN(B.SEG_OPC_ID) > 6  ";
                                    qry += "    AND A.GLB_EMP_ID = " + id_empresa;
                                    qry += "    AND A.GLB_EST_ID = 1 ";
                                    qry += "    AND A.SEG_ROL_ID = " + id_rol;
                                    qry += "    AND A.SEG_MOD_ID = " + id_modulo;
                                    qry += " ORDER BY B.SEG_OPC_ID ";
                                }
                                else
                                {
                                    qry = " SELECT A.GLB_EMP_Id, B.SEG_OPC_ID, B.OPCION_INGLES, B.NAVIGATE_URL, B.SEG_OPC_PADRE ";
                                    qry += " FROM SEG_PERMISOS A  ";
                                    qry += "    INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_ID  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                                    qry += " WHERE A.ACCESO = '1'  ";
                                    qry += "    AND B.SEG_OPC_PADRE = '" + dt2.Rows[contOpciones][1].ToString() + "'";
                                    qry += "    AND LEN(B.SEG_OPC_ID) > 6  ";
                                    qry += "    AND A.GLB_EMP_ID = " + id_empresa;
                                    qry += "    AND A.GLB_EST_ID = 1 ";
                                    qry += "    AND A.SEG_ROL_ID = " + id_rol;
                                    qry += "    AND A.SEG_MOD_ID = " + id_modulo;
                                    qry += " ORDER BY B.SEG_OPC_ID ";
                                }

                                dt3.Clear();
                                cmd3.CommandText = qry;
                                adp3.SelectCommand = cmd3;
                                adp3.Fill(dt3);

                                if (dt3.Rows.Count > 0)
                                {
                                    for (int contOpciones1 = 0; contOpciones1 <= dt3.Rows.Count - 1; contOpciones1++)
                                    {
                                        RadMenuItem itmOpcion = new RadMenuItem();
                                        itmOpcion.Value = dt3.Rows[contOpciones1][1].ToString();
                                        itmOpcion.Text = dt3.Rows[contOpciones1][2].ToString();
                                        //itmOpcion.ImageUrl = dt3.Rows[contOpciones1][5].ToString();
                                        itmOpcion.NavigateUrl = dt3.Rows[contOpciones1][3].ToString();
                                        itmPadre.Items.Add(itmOpcion);

                                        if (idioma == "SPA")
                                        {
                                            qry = " SELECT A.GLB_EMP_Id, B.SEG_OPC_ID, B.OPCION, B.NAVIGATE_URL, B.SEG_OPC_PADRE ";
                                            qry += " FROM SEG_PERMISOS A  ";
                                            qry += "    INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_ID  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                                            qry += " WHERE A.ACCESO = '1'  ";
                                            qry += "    AND B.SEG_OPC_PADRE = '" + dt3.Rows[contOpciones1][1].ToString() + "'";
                                            qry += "    AND LEN(B.SEG_OPC_ID) > 6  ";
                                            qry += "    AND A.GLB_EMP_ID = " + id_empresa;
                                            qry += "    AND A.GLB_EST_ID = 1 ";
                                            qry += "    AND A.SEG_ROL_ID = " + id_rol;
                                            qry += "    AND A.SEG_MOD_ID = " + id_modulo;
                                            qry += " ORDER BY B.SEG_OPC_ID ";
                                        }
                                        else
                                        {
                                            qry = " SELECT A.GLB_EMP_Id, B.SEG_OPC_ID, B.OPCION_INGLES, B.NAVIGATE_URL, B.SEG_OPC_PADRE ";
                                            qry += " FROM SEG_PERMISOS A  ";
                                            qry += "    INNER JOIN SEG_OPCIONES B ON (A.SEG_OPC_ID = B.SEG_OPC_ID AND A.GLB_EMP_ID = B.GLB_EMP_ID  AND A.SEG_MOD_Id = B.SEG_MOD_Id)  ";
                                            qry += " WHERE A.ACCESO = '1'  ";
                                            qry += "    AND B.SEG_OPC_PADRE = '" + dt3.Rows[contOpciones1][1].ToString() + "'";
                                            qry += "    AND LEN(B.SEG_OPC_ID) > 6  ";
                                            qry += "    AND A.GLB_EMP_ID = " + id_empresa;
                                            qry += "    AND A.GLB_EST_ID = 1 ";
                                            qry += "    AND A.SEG_ROL_ID = " + id_rol;
                                            qry += "    AND A.SEG_MOD_ID = " + id_modulo;
                                            qry += " ORDER BY B.SEG_OPC_ID ";
                                        }

                                        dt4.Clear();
                                        cmd4.CommandText = qry;
                                        adp4.SelectCommand = cmd4;
                                        adp4.Fill(dt4);

                                        if (dt4.Rows.Count > 0)
                                        {
                                            for (int contOpciones2 = 0; contOpciones2 <= dt4.Rows.Count - 1; contOpciones2++)
                                            {
                                                RadMenuItem itmOpcion2 = new RadMenuItem();
                                                itmOpcion2.Value = dt4.Rows[contOpciones2][1].ToString();
                                                itmOpcion2.Text = dt4.Rows[contOpciones2][2].ToString();
                                                //itmOpcion2.ImageUrl = dt4.Rows[contOpciones2][5].ToString();
                                                itmOpcion2.NavigateUrl = dt4.Rows[contOpciones2][3].ToString();
                                                itmOpcion.Items.Add(itmOpcion2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    RadMenuItem itm5 = new RadMenuItem();
                    itm5.Value = "99";
                    itm5.Text = "Salir";
                    itm5.NavigateUrl = "~/default.aspx";
                    mnuOpciones.Items.Add(itm5);
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
                if (cnn4.State == ConnectionState.Open)
                {
                    cnn4.Close();
                }
            }
        }
    }
}