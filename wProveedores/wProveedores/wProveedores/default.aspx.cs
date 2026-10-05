using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Telerik.Web.UI;


namespace wProveedores
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            if (Page.IsPostBack != true)
            {
                cargaEmpresas();
            }
        }

        private void cargaEmpresas()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " select GLB_EMP_Id, nombre ";
                qry += " from GLB_Empresas ";
                qry += " where glb_est_id = 1 ";
                qry += " order by GLB_EMP_Id ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                cmbEmpresas.Items.Clear();

                RadComboBoxItem itm1 = new RadComboBoxItem();
                itm1.Text = "Seleccione opcion";
                itm1.Value = "-1";
                cmbEmpresas.Items.Add(itm1);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RadComboBoxItem itm = new RadComboBoxItem();
                        itm.Text = dt.Rows[i][1].ToString().Trim();
                        itm.Value = dt.Rows[i][0].ToString().Trim();
                        cmbEmpresas.Items.Add(itm);
                    }
                }
            }
            catch (Exception ex)
            {
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

        private void cargaModulos()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " select SEG_MOD_Id, Modulo ";
                qry += " from SEG_Modulos ";
                qry += " where GLB_EMP_Id = " + cmbEmpresas.SelectedItem.Value.ToString();
                qry += " and GLB_EST_Id = 1 ";
                qry += " and SEG_MOD_ID = 2 ";
                qry += " order by SEG_MOD_Id ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                this.cmbModulos.Items.Clear();

                //RadComboBoxItem itm1 = new RadComboBoxItem();
                //itm1.Text = "Seleccione opcion";
                //itm1.Value = "-1";
                //cmbModulos.Items.Add(itm1);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RadComboBoxItem itm = new RadComboBoxItem();
                        itm.Text = dt.Rows[i][1].ToString().Trim();
                        itm.Value = dt.Rows[i][0].ToString().Trim();
                        cmbModulos.Items.Add(itm);
                    }
                }
            }
            catch (Exception ex)
            {
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

        protected void cmdAceptar_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {

                //RadTextBox txtUsuario = (RadTextBox)RadPanelBar1.FindItemByValue("inicioSesion").FindControl("txtUsuario");
                //RadTextBox txtPassword = (RadTextBox)RadPanelBar1.FindItemByValue("inicioSesion").FindControl("txtPassword");
                if (cmbEmpresas.SelectedItem.Value == "-1")
                {
                    lblError.Text = "Debe de seleccionar la empresa para iniciar sesion.";
                    return;
                }
                if (txtUsuario.Text == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Debe de ingresar el usuario para iniciar sesion.')</script>");
                    lblError.Text = "Debe de ingresar el usuario para iniciar sesion.";
                    return;
                }
                if (txtPassword.Text == "")
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Debe de ingresar la contraseña para iniciar sesion.')</script>");
                    lblError.Text = "Debe de ingresar la contraseña para iniciar sesion.";
                    return;
                }

                qry = " SELECT A.NOMBRE, A.NOMBRE, A.GLB_EMP_ID, B.NOMBRE, A.SEG_ROL_ID ";
                qry += " FROM SEG_USUARIOS A  ";
                qry += " INNER JOIN GLB_EMPRESAS B ON A.GLB_EMP_ID = B.GLB_EMP_ID ";
                qry += " WHERE A.CORREOELECTRONICO ='" + txtUsuario.Text.Trim() + "'";
                qry += " AND A.PASS = '" + txtPassword.Text.Trim() + "'  ";
                //qry += " AND A.SEG_MOD_ID = " + cmbModulos.SelectedItem.Value.ToString();
                qry += " AND A.GLB_EMP_Id = " + this.cmbEmpresas.SelectedItem.Value.ToString();
                qry += " AND A.GLB_EST_ID = 1  ";
                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Session.Add("user", dt.Rows[0][0].ToString());
                    Session.Add("username", dt.Rows[0][1].ToString());
                    Session.Add("id_empresa", dt.Rows[0][2].ToString());
                    Session.Add("nombre_empresa", dt.Rows[0][3].ToString());
                    Session.Add("id_rol", dt.Rows[0][4].ToString());
                    Session.Add("id_modulo", cmbModulos.SelectedItem.Value.ToString());
                    Session.Add("modulo", cmbModulos.SelectedItem.Text.ToString());
                    Session.Add("idioma", "SPA");
                    Session.Add("sessionUser", txtUsuario.Text);
                    if (cmbModulos.SelectedItem.Value.ToString() == "5")
                    {
                        Response.Redirect("~/mainCG.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/main.aspx");
                    }
                }
                else
                {
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('El usuario o contraseña ingresada es incorrecta, intente nuevamente por favor.')</script>");
                    lblError.Text = "El usuario o contraseña ingresada es incorrecta, intente nuevamente por favor.";
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

        protected void cmbEmpresas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cargaModulos();
        }
    }
}