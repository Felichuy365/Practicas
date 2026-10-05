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
    public partial class consultaEmpleado : System.Web.UI.Page
    {
        string user = "";
        string id_empresa = "";
        string nombre_empresa = "";
        string id_modulo = "0";
        string maquina = "";
        string idioma = "";
        string sessionUser = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["username"].ToString();
            id_empresa = Session["id_empresa"].ToString();
            nombre_empresa = Session["nombre_empresa"].ToString();
            id_modulo = Session["id_modulo"].ToString();
            idioma = Session["idioma"].ToString();
            maquina = System.Environment.MachineName;
            sessionUser = Session["sessionUser"].ToString();

            if (Page.IsPostBack != true)
            {
                if (user == "")
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                                        
                }
            }
        }

        protected void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (txtCodigoEmpleado.Text != "")
            {
                fnCalculaLimiteCredito(txtCodigoEmpleado.Text);
                CalculaVales(txtCodigoEmpleado.Text);
                CalculaSaldoPlanes(txtCodigoEmpleado.Text);
            }
        }

        private void fnCalculaLimiteCredito(string cod_empleado)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                string codEmpleado = cod_empleado.Trim();
                if (cod_empleado.Length > 6)
                {
                // Tomar solo los últimos 6 dígitos
                cod_empleado = cod_empleado.Substring(cod_empleado.Length - 6);
                codEmpleado = cod_empleado; // Actualizar el texto en el campo
                }

                qry = " SELECT NOM_COMPLETO, CREDITO, ESTADO, FECHA_INGRESO, COD_TIPO_NOMINA, FRECUENCIA ";
                    qry += " FROM OPENQUERY(ORACLE, 'SELECT A.COD_EMPLEADO, ";
                    qry += " 							   A.NOM_COMPLETO, ";
                    qry += " 							   NEW_PLANILLA.CALCULO_CREDITO_DISPONIBLE(" + codEmpleado + ") CREDITO, ";
                    qry += " 							   CASE B.ESTADO ";
                    qry += " 							    WHEN ''A'' THEN ''ALTA'' ";
                    qry += " 							    ELSE ''SIN CREDITO''  ";
                    qry += " 							   END ESTADO, ";
                    qry += " 							   B.FECHA_INGRESO, ";
                    qry += " 							   B.COD_TIPO_NOMINA, ";
                    qry += " 							  CASE COD_FREC_NOMINA ";
                    qry += " 							    WHEN 1 THEN ''QUINCENAL'' ";
                    qry += " 							    ELSE ''MENSUAL'' ";
                    qry += " 							  END FRECUENCIA";
                    qry += " 						 FROM RRHH.EMPLEADOS A, ";
                    qry += " 							  RRHH.CONTRATOS B ";
                    qry += " 						WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                    qry += " 						 AND A.COD_EMPLEADO = " + codEmpleado + "') ";

                    cnn.Open();
                    cmd.Connection = cnn;
                    cmd.CommandText = qry;
                    adp.SelectCommand = cmd;
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        txtEmpleado.Text = dt.Rows[0][0].ToString();
                        this.txtLimiteCredito.Text = dt.Rows[0][1].ToString();
                        txtEstado.Text = dt.Rows[0][2].ToString();
                        txtFechaIngreso.Text = dt.Rows[0][3].ToString();
                        this.txtCodTipoNonina.Text = dt.Rows[0][4].ToString();
                        this.txtFrecuencia.Text = dt.Rows[0][5].ToString();
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


        private void CalculaVales(string cod_empleado)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";            
            try
            {

                qry = " SELECT MONTO ";
                qry += "  FROM OPENQUERY(ORACLE, 'SELECT NVL(SUM(MONTO),0) MONTO ";
                qry += " 						 FROM NEW_PLANILLA.OTROS_ING_DES ";
                qry += " 						 WHERE COD_EMPLEADO = " + txtCodigoEmpleado.Text ;
                qry += " 						  AND NUM_NOMINA IN(SELECT NUM_NOMINA ";
                qry += " 											 FROM MANO_OBRA.CODIGOS_NOMINA  ";
                qry += " 											 WHERE TRUNC(SYSDATE) >= TRUNC(FECHA_INI_CALC) ";
                qry += " 											  AND TRUNC(SYSDATE) <= TRUNC(FECHA_FIN_CALC)  ";
                qry += " 											  AND COD_TIPO_NOMINA = " + txtCodTipoNonina.Text;
                qry += " 											  AND SUBSTR(NUM_NOMINA, 7, 2) IN (11,12,21))') ";


                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    this.txtVales.Text = dt.Rows[0][0].ToString();                    
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


        private void CalculaSaldoPlanes(string cod_empleado)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {

                qry = " SELECT SALDO ";
                qry += "  FROM OPENQUERY(ORACLE, 'SELECT NVL(SUM(SALDO),0) SALDO ";
                qry += " 							 FROM RRHH.PLANES_PAGO ";
                qry += " 							WHERE SALDO > 0 ";
                qry += " 							 AND ESTADO = ''A'' ";
                qry += " 							 AND COD_EMPLEADO = " + txtCodigoEmpleado.Text + "') ";


                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    this.txtPlanesPago.Text = dt.Rows[0][0].ToString();
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

        protected void cmbLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/consultaEmpleado.aspx");
        }
    }
}