using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace wProveedores.proveedores
{
    public partial class planesFinanciamiento : System.Web.UI.Page
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
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    dtpFecha.SelectedDate = DateTime.Now;
                    cargaRubro();
                }
            }
        }

        private void cargaRubro()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                qry = " SELECT a.PLA_RUB_ID, b.Rubro ";
                qry += "  FROM PLA_RubrosUsuarios a ";
                qry += "   inner join PLA_Rubros b on a.PLA_RUB_Id = b.PLA_RUB_Id ";
                qry += "   inner join SEG_Usuarios c on a.SEG_USE_Id = c.SEG_USE_Id ";
                qry += " where c.correoElectronico = '" + sessionUser + "'";
                qry += " and c.GLB_EMP_Id = '" + id_empresa + "'";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cmbRubro.Items.Clear();
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        RadComboBoxItem itm = new RadComboBoxItem();
                        itm.Text = dt.Rows[i][1].ToString();
                        itm.Value = dt.Rows[i][0].ToString();
                        cmbRubro.Items.Add(itm);
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

        private void guardar()
        {
            
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            
        }

        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodEmpleado.Text == "")
            {
                RadNotification1.Show("Debe de ingresar el codigo del empleado.");
                return;
            }
            if (txtDescripcion.Text == "")
            {
                RadNotification1.Show("Debe de ingresar la descripcion del plan de financiamiento.");
                return;
            }
            if (txtMonto.Text == "")
            {
                RadNotification1.Show("Debe de ingresar el monto del plan de financiamiento.");
                return;
            }
            if (txtNoCuotas.Text == "")
            {
                RadNotification1.Show("Debe de ingresar el numero de cuotas, para el plan de plago.");
                return;
            }

            //string fecha = "";
            //fecha = this.dtpFecha.SelectedDate.Value.Day.ToString() + "/" + this.dtpFecha.SelectedDate.Value.Month.ToString() + "/" + this.dtpFecha.SelectedDate.Value.Year.ToString();
            //WSDBOracle.DBOracleSoapClient WS = new WSDBOracle.DBOracleSoapClient();
            //Boolean result = WS.SendDataPlanPago(txtCodEmpleado.Text, txtDescripcion.Text, fecha, txtMonto.Text, "", user, txtNoCuotas.Text);
            Boolean result = insertaPlanPago();
            if (result == true)
            {
                lblMensaje.Text = "Registro guardado con exito.";
                RadNotification1.Show("Registro guardado con exito.");
            }            
        }

        private Boolean insertaPlanPago()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            Boolean result = false;
            string cod_plan_pago = "";
            try
            {
                cod_plan_pago = buscaCodPlanPago();
                string fecha = "";
                fecha = this.dtpFecha.SelectedDate.Value.Day.ToString() + "/" + this.dtpFecha.SelectedDate.Value.Month.ToString() + "/" + this.dtpFecha.SelectedDate.Value.Year.ToString();

                //qry = " SELECT COD_PLAN_PAGO, COD_EMPLEADO, COD_ING_DES, DESC_PLAN_PAGO, FECHA, MONTO, SALDO, SYS_USER, SYS_DATE, INTERES, SALDO_INTERES, PORCENT_INTERES, NO_CUOTAS, ESTADO, AFECTA ";
                qry = "  SET DATEFORMAT DMY INSERT INTO OPENQUERY(ORACLE, 'SELECT  ";
                qry += "   COD_PLAN_PAGO, COD_EMPLEADO, COD_ING_DES, DESC_PLAN_PAGO, FECHA, MONTO, SALDO, SYS_USER, SYS_DATE,  ";
                qry += "   INTERES, SALDO_INTERES, PORCENT_INTERES, NO_CUOTAS, ESTADO, AFECTA FROM RRHH.PLANES_PAGO WHERE COD_PLAN_PAGO = 1') ";
                qry += "  VALUES (" + cod_plan_pago + ", " + txtCodEmpleado.Text + ", " + cmbRubro.SelectedItem.Value + ", '" + txtDescripcion.Text + "', CONVERT(DATETIME, '" + fecha + "'), " + txtMonto.Text + ", " + txtMonto.Text + ", 'WEB', GETDATE(),  ";
                qry += "  0, 0, 0, "  + txtNoCuotas.Text + ", 'A', 'N') ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                cmd.ExecuteNonQuery();

                result = true;

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
            return result;
        }

        private string buscaCodPlanPago()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string result = "0";
            try
            {

                qry = " SELECT COD_PLAN_PAGO ";
                qry += " FROM OPENQUERY(ORACLE, 'SELECT NVL(MAX(COD_PLAN_PAGO), 0)+1 COD_PLAN_PAGO FROM RRHH.PLANES_PAGO  ') ";

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
                RadNotification1.Show(ex.Message);
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

        private void fnCalculaLimiteCredito(string cod_empleado)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                // Validar y ajustar el código del empleado
                string codEmpleado = txtCodEmpleado.Text.Trim();

                if (codEmpleado.Length > 6)
                {
                    // Tomar solo los últimos 6 dígitos
                    codEmpleado = codEmpleado.Substring(codEmpleado.Length - 6);
                    txtCodEmpleado.Text = codEmpleado; // Actualizar el texto en el campo
                }

                qry = " SELECT NOM_COMPLETO, CREDITO, ESTADO, FECHA_INGRESO, COD_TIPO_NOMINA, FRECUENCIA ";
                qry += " FROM OPENQUERY(ORACLE, 'SELECT A.COD_EMPLEADO, ";
                qry += " 							   A.NOM_COMPLETO, ";
                qry += " 							   NEW_PLANILLA.CALCULO_CREDITO_DISPONIBLE(" + cod_empleado + ") CREDITO, ";
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
                qry += " 						 AND A.COD_EMPLEADO = " + cod_empleado + "') ";

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
                    //txtFechaIngreso.Text = dt.Rows[0][3].ToString();
                    //this.txtCodTipoNonina.Text = dt.Rows[0][4].ToString();
                    txtFrecuencia.Text = dt.Rows[0][5].ToString();
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

        protected void cmdBuscar_Click(object sender, EventArgs e)
        {
            if (txtCodEmpleado.Text != "")
            {
                fnCalculaLimiteCredito(txtCodEmpleado.Text);
            }            
        }
    }
}