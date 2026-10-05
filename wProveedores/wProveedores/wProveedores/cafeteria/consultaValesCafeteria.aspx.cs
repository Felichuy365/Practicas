using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;



namespace wProveedores.cafeteria
{
    public partial class consultaValesCafeteria : System.Web.UI.Page
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
                    cargaRubro();
                    dtpFecha.SelectedDate = DateTime.Now;
                    dtpFechaFinal.SelectedDate = DateTime.Now;
                    imprimeReporte();
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

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            imprimeReporte();
            switch (e.Item.Index)
            {
                case 1: //excel
                    this.gVales.ExportSettings.IgnorePaging = true;
                    gVales.ExportSettings.ExportOnlyData = true;
                    gVales.ExportSettings.OpenInNewWindow = true;
                    gVales.MasterTableView.ExportToExcel();
                    break;
                case 0://pdf
                    gVales.MasterTableView.ExportToPdf();
                    break;
                case 2://word
                    this.gVales.ExportSettings.Word.Format = GridWordExportFormat.Html;
                    gVales.ExportSettings.ExportOnlyData = true;
                    gVales.ExportSettings.IgnorePaging = true;
                    gVales.ExportSettings.OpenInNewWindow = true;
                    gVales.ExportSettings.UseItemStyles = true;
                    gVales.MasterTableView.ExportToWord();
                    break;
            }
        }

        private void imprimeReporte()
        {
            string qry = "";            
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string fecha = "";
            string fechaFinal = "";

            try
            {
                dt.Columns.Clear();
                dt.Columns.Add("COD_PLANTA", typeof(Int32));
                dt.Columns.Add("DESC_PLANTA", typeof(String));
                dt.Columns.Add("COD_DEPTO_PLANTA", typeof(Int32));
                dt.Columns.Add("DESC_DEPTO_PLANTA", typeof(String));
                dt.Columns.Add("COD_GRUPO_TRAB", typeof(Int32));
                dt.Columns.Add("DESC_GRUPO_TRAB", typeof(String));
                dt.Columns.Add("COD_EMPLEADO", typeof(Int32));
                dt.Columns.Add("NOM_COMPLETO", typeof(String));
                dt.Columns.Add("FECHA", typeof(DateTime));
                dt.Columns.Add("MONTO", typeof(decimal));
                dt.Columns.Add("SYS_USER", typeof(String));
                dt.Columns.Add("SYS_DATE", typeof(DateTime));  

                fecha = this.dtpFecha.SelectedDate.Value.Day.ToString() + "/" + this.dtpFecha.SelectedDate.Value.Month.ToString() + "/" + this.dtpFecha.SelectedDate.Value.Year.ToString();
                fechaFinal = this.dtpFechaFinal.SelectedDate.Value.Day.ToString() + "/" + this.dtpFechaFinal.SelectedDate.Value.Month.ToString() + "/" + this.dtpFechaFinal.SelectedDate.Value.Year.ToString();

                qry = " SET DATEFORMAT DMY ";
                qry += " SELECT NUM_NOMINA, NUM_DOCUMENTO, CONVERT(INT, COD_PLANTA) COD_PLANTA, DESC_PLANTA, CONVERT(INT, COD_DEPTO_PLANTA) COD_DEPTO_PLANTA, DESC_DEPTO_PLANTA, CONVERT(INT, COD_GRUPO_TRAB) COD_GRUPO_TRAB, DESC_GRUPO_TRAB, CONVERT(INT, COD_EMPLEADO) COD_EMPLEADO, NOM_COMPLETO, FECHA, MONTO, SYS_USER, SYS_DATE ";
                qry += "  FROM OPENQUERY (ORACLE, 'SELECT  A.NUM_NOMINA, A.NUM_DOCUMENTO, ";
                qry += " 								  C.COD_PLANTA, ";
                qry += " 								  (SELECT DESC_PLANTA ";
                qry += " 									FROM GENERAL.PLANTAS ";
                qry += " 									WHERE COD_PLANTA = C.COD_PLANTA) DESC_PLANTA, ";
                qry += " 								  C.COD_DEPTO_PLANTA, ";
                qry += " 								  (SELECT DESC_DEPTO_PLANTA ";
                qry += " 									FROM GENERAL.DEPTOS_PLANTA ";
                qry += " 									WHERE COD_PLANTA = C.COD_PLANTA ";
                qry += " 									 AND COD_DEPTO_PLANTA = C.COD_DEPTO_PLANTA) DESC_DEPTO_PLANTA, ";
                qry += " 								  C.COD_GRUPO_TRAB, ";
                qry += " 								  (SELECT DESC_GRUPO_TRAB ";
                qry += " 									FROM GENERAL.GRUPOS_TRABAJO ";
                qry += " 									WHERE COD_PLANTA = C.COD_PLANTA ";
                qry += " 									AND COD_DEPTO_PLANTA = C.COD_DEPTO_PLANTA ";
                qry += " 									AND COD_GRUPO_TRAB = C.COD_GRUPO_TRAB) DESC_GRUPO_TRAB, ";
                qry += " 								  A.COD_EMPLEADO, ";
                qry += " 								  B.NOM_COMPLETO, ";
                qry += " 								  A.FECHA, ";
                qry += " 								  A.MONTO, ";
                qry += " 								  A.SYS_USER, ";
                qry += " 								  A.SYS_DATE     ";   
                qry += " 							 FROM NEW_PLANILLA.OTROS_ING_DES A, ";
                qry += " 								  RRHH.EMPLEADOS B, ";
                qry += " 								  RRHH.CONTRATOS C ";
                qry += " 							 WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                qry += " 							  AND B.COD_EMPLEADO = C.COD_EMPLEADO ";
                qry += " 							  AND SUBSTR(A.COD_ING_DES_FRTI, 4, 2) = ''" + cmbRubro.SelectedItem.Value + "'' ";
                qry += " 							  AND A.FECHA >= TO_DATE(''" + fecha + "'', ''DD/MM/RRRR'') ";
                qry += " 							  AND A.FECHA <= TO_DATE(''" + fechaFinal + "'', ''DD/MM/RRRR'')') ";

                
                sqlData.ConnectionString = cnn.ConnectionString;
                sqlData.SelectCommand = qry;
                sqlData.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (cnn.State == ConnectionState.Open)
                //{
                //    cnn.Close();
                //}
            }
        }

        protected void cmdGenerar_Click(object sender, EventArgs e)
        {
            imprimeReporte();
        }

    }
}