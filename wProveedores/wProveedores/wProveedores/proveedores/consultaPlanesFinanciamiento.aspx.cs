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
    public partial class consultaPlanesFinanciamiento : System.Web.UI.Page
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
                    dtpFechaInicio.SelectedDate = DateTime.Now;
                    dtpFechaFinal.SelectedDate = DateTime.Now;
                    cargaRubro();
                    cargaGrid();
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

        private void cargaGrid()
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string fechaInicio = "";
            string fechaFinal = "";
            try
            {
                fechaInicio = this.dtpFechaInicio.SelectedDate.Value.Day.ToString() + "/" + this.dtpFechaInicio.SelectedDate.Value.Month.ToString() + "/" + this.dtpFechaInicio.SelectedDate.Value.Year.ToString();
                fechaFinal = this.dtpFechaFinal.SelectedDate.Value.Day.ToString() + "/" + this.dtpFechaFinal.SelectedDate.Value.Month.ToString() + "/" + this.dtpFechaFinal.SelectedDate.Value.Year.ToString();

                qry = " SELECT COD_PLAN_PAGO, COD_EMPLEADO, NOM_COMPLETO, COD_ING_DES, DESC_PLAN_PAGO, NO_CUOTAS, FECHA, MONTO, SALDO ";
                qry += "  FROM OPENQUERY(ORACLE, 'SELECT A.COD_PLAN_PAGO, A.COD_EMPLEADO, B.NOM_COMPLETO, A.COD_ING_DES, A.DESC_PLAN_PAGO, A.NO_CUOTAS, A.FECHA, MONTO, A.SALDO ";
                qry += " 							 FROM RRHH.PLANES_PAGO A, ";
                qry += " 								  RRHH.EMPLEADOS B ";
                qry += " 							WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                qry += " 								AND A.SALDO > 0       ";
                qry += " 								AND A.COD_ING_DES = " + this.cmbRubro.SelectedItem.Value;
                if (txtCodEmpleado.Text != "")
                {
                    qry += " 					        AND A.COD_EMPLEADO = " + txtCodEmpleado.Text;
                }
                qry += " 								AND FECHA >= TO_DATE(''" + fechaInicio + "'', ''DD/MM/RRRR'') ";
                qry += " 								AND FECHA <= TO_DATE(''" + fechaFinal + "'', ''DD/MM/RRRR'')') ";

                cnn.Open();
                cmd.Connection = cnn;
                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                gPlanesPago.DataSource = dt;
                gPlanesPago.DataBind();
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

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Index)
            {
                case 0:
                    cargaGrid();
                    break;
                case 2:
                    gPlanesPago.ExportSettings.IgnorePaging = true;
                    gPlanesPago.ExportSettings.ExportOnlyData = true;
                    gPlanesPago.ExportSettings.OpenInNewWindow = true;
                    gPlanesPago.MasterTableView.ExportToExcel();
                    break;
                case 3:
                    cargaGrid();
                    this.gPlanesPago.ExportSettings.Word.Format = GridWordExportFormat.Html;
                    gPlanesPago.ExportSettings.ExportOnlyData = true;
                    gPlanesPago.ExportSettings.IgnorePaging = true;
                    gPlanesPago.ExportSettings.OpenInNewWindow = true;
                    gPlanesPago.ExportSettings.UseItemStyles = true;
                    gPlanesPago.MasterTableView.ExportToWord();
                    break;
                case 4:
                    gPlanesPago.MasterTableView.ExportToPdf();
                    break;
            }

        }

    }
}