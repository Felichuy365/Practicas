using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

namespace wProveedores.cafeteria
{
    public partial class valesCafeteria : System.Web.UI.Page
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
            //sessionUser
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
                    limpia();
                }
            }
        }

        private void limpia()
        {
            txtCodEmpleado.Text = "";
            txtEmpleado.Text = "";
            txtLimiteCredito.Text = "";
            txtNumDocumento.Text = "";
            txtMonto.Text = "";
            dtpFecha.SelectedDate = DateTime.Now;
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

        protected void cmdBuscarEmpleado_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            try
            {
                string codEmpleado = txtCodEmpleado.Text.Trim();

                if (codEmpleado.Length > 6)
                {
                    codEmpleado = codEmpleado.Substring(codEmpleado.Length - 6);
                    txtCodEmpleado.Text = codEmpleado;
                }

                if (!string.IsNullOrEmpty(codEmpleado))
                {
                    qry = @"
                SELECT COD_EMPLEADO, NOM_COMPLETO, FECHA_INGRESO, LIMITE_CREDITO, ESTADO, COD_TIPO_NOMINA, TIPO_COSTO
                FROM OPENQUERY(ORACLE, 
                    'SELECT  
                        A.COD_EMPLEADO, 
                        A.NOM_COMPLETO, 
                        A.FECHA_INGRESO, 
                        CASE 
                            WHEN A.TIPO_COSTO LIKE ''%ADMIN%'' THEN NULL
                            ELSE NEW_PLANILLA.CALCULO_CREDITO_DISPONIBLE(A.COD_EMPLEADO)
                        END AS LIMITE_CREDITO, 
                        CASE A.ESTADO 
                            WHEN ''A'' THEN ''ALTA'' 
                            WHEN ''S'' THEN ''SUSPENDIDO'' 
                            WHEN ''V'' THEN ''VACACIONES'' 
                            ELSE ''VALIDE SU ESTADO CON RRHH'' 
                        END ESTADO, 
                        B.COD_TIPO_NOMINA,
                        A.TIPO_COSTO
                    FROM SYSTEM.VW_INFO_EMPLEADO A 
                    INNER JOIN RRHH.CONTRATOS B ON A.COD_EMPLEADO = B.COD_EMPLEADO 
                    WHERE A.COD_EMPLEADO = " + codEmpleado + "')";

                    cnn.Open();
                    cmd.Connection = cnn;
                    cmd.CommandText = qry;
                    adp.SelectCommand = cmd;
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        // Asignar valores básicos
                        txtEmpleado.Text = row["NOM_COMPLETO"].ToString();
                        dtpFechaIngreso.SelectedDate = Convert.ToDateTime(row["FECHA_INGRESO"]);
                        txtEstado.Text = row["ESTADO"].ToString();
                        txtCodTipoNomina.Text = row["COD_TIPO_NOMINA"].ToString();

                        // Manejar el límite de crédito según el tipo de costo
                        string tipoCosto = row["TIPO_COSTO"].ToString().ToUpper();
                        if (tipoCosto.Contains("ADMIN"))
                        {
                            txtLimiteCredito.Text = "N/A";
                            txtLimiteCredito.Enabled = false;
                        }
                        else
                        {
                            txtLimiteCredito.Text = row["LIMITE_CREDITO"] != DBNull.Value ?
                                                  row["LIMITE_CREDITO"].ToString() : "0";
                        }

                        // Cargar foto
                        var foto = getFoto(codEmpleado);
                        imgEmpleado.ImageUrl = $"data:image/jpeg;base64, {foto}";
                    }
                    else
                    {
                        RadNotification1.Show("No se encontró el empleado con el código proporcionado.");
                        limpia();
                    }
                }
                else
                {
                    RadNotification1.Show("Debe ingresar un código de empleado válido.");
                }
            }
            catch (Exception ex)
            {
                RadNotification1.Show("Error al buscar empleado: " + ex.Message);
                limpia();
            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }
        private Boolean validaCampos()
        {
            Boolean result = false;
            lblError.Text = "";

            // Validación código empleado
            if (string.IsNullOrEmpty(txtCodEmpleado.Text))
            {
                result = true;
                RadNotification1.Show("Debe ingresar el código del empleado.");
                lblError.Text = "Debe ingresar el código del empleado.";
                return result; // Retornar inmediatamente para evitar validaciones innecesarias
            }

            // Validación monto
            if (string.IsNullOrEmpty(txtMonto.Text))
            {
                result = true;
                RadNotification1.Show("Debe ingresar un monto.");
                lblError.Text = "Debe ingresar un monto.";
                return result;
            }

            // Validación formato numérico del monto
            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto))
            {
                result = true;
                RadNotification1.Show("El monto debe ser un valor numérico válido.");
                lblError.Text = "El monto debe ser un valor numérico válido.";
                return result;
            }

            // Validación monto positivo
            if (monto <= 0)
            {
                result = true;
                RadNotification1.Show("Debe ingresar un monto mayor a cero (0).");
                lblError.Text = "Debe ingresar un monto mayor a cero (0).";
                return result;
            }

            // Validación monto máximo (excepto para rubro 89)
            if (monto > 500 && cmbRubro.SelectedItem?.Value != "89")
            {
                result = true;
                RadNotification1.Show("Debe ingresar un monto menor a Q500.");
                lblError.Text = "Debe ingresar un monto menor a Q500.";
                return result;
            }

            // Validación límite de crédito (solo si no es "N/A")
            if (txtLimiteCredito.Text != "N/A")
            {
                decimal limiteCredito;
                if (!decimal.TryParse(txtLimiteCredito.Text, out limiteCredito) || limiteCredito <= 0)
                {
                    result = true;
                    RadNotification1.Show("El colaborador no posee crédito disponible.");
                    lblError.Text = "El colaborador no posee crédito disponible.";
                    return result;
                }
            }

            // Validación estado del empleado
            if (txtEstado.Text == "VALIDE SU ESTADO CON RRHH")
            {
                result = true;
                RadNotification1.Show("El estado del colaborador no es válido para esta operación.");
                lblError.Text = "El estado del colaborador no es válido para esta operación.";
                return result;
            }

            return result;
        }

        private Boolean verificaExisteRegistro(string cod_empleado, string documento, string num_nomina, string rubro, string fecha)
        {
            Boolean result = false;
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";

            qry = " select total ";
            qry += " from openquery(oracle,' ";
            qry += "   SELECT COUNT(*) total ";
            qry += "   FROM NEW_PLANILLA.OTROS_ING_DES ";
            qry += "   WHERE COD_EMPLEADO = " + cod_empleado + " ";
            qry += "   AND NUM_NOMINA = " + num_nomina + " ";
            qry += "   AND SUBSTR(COD_ING_DES_FRTI, 4, 2) = " + rubro;
            qry += "    AND FECHA = TO_DATE(''" + fecha + "'', ''DD/MM/RRRR'')";
            qry += "   AND NUM_DOCUMENTO = " + documento + "')";

            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = qry;
            adp.SelectCommand = cmd;
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
                {
                    result = true;
                }
            }

            cnn.Close();

            return result;
        }

        private string devuelveNumNomina(string cod_tipo_nomina, string fecha)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string num_nomina = "";

            if (cod_tipo_nomina != "")
            {
                cnn.Open();
                cmd.Connection = cnn;

                qry = "  declare @fecha		NVARCHAR(50) ";
                qry += " declare @qry		nvarchar(1000) ";
                qry += " set @fecha =  '" + fecha + "' ";
                qry += " set @qry ='select NUM_NOMINA  ";
                qry += " 			 from OPENQUERY(ORACLE,' + '''SELECT NUM_NOMINA ";
                qry += " 								 FROM MANO_OBRA.CODIGOS_NOMINA  ";
                qry += " 								 WHERE COD_TIPO_NOMINA = " + cod_tipo_nomina;
                qry += " 								 AND SUBSTR(NUM_NOMINA, 7, 2) IN (''''11'''', ''''12'''', ''''21'''') ";
                qry += " 								 AND TO_DATE(''''' + @fecha + ''''', ''''DD/MM/RRRR'''') BETWEEN FECHA_INI_CALC AND FECHA_FIN_CALC ''' + ')' ";


                qry += " EXEC sp_executesql @qry ";

                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    num_nomina = dt.Rows[0][0].ToString();
                }

                cnn.Close();
            }

            return num_nomina;
        }

        private string devuelveCodTipoNomina(string cod_empleado)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string cod_tipo_nomina = "";


            if (cod_empleado != "")
            {
                cnn.Open();
                cmd.Connection = cnn;

                qry = "  declare @qry			nvarchar(700) ";
                qry += " declare @cod_empleado	nvarchar(10) ";

                qry += " set @cod_empleado = '" + cod_empleado + "' ";
                qry += " set @qry = ' select COD_TIPO_NOMINA ";
                qry += "                from OPENQUERY(ORACLE,' + '''SELECT  ";
                qry += "                                                EMPL.COD_TIPO_NOMINA ";
                qry += "                                            FROM SYSTEM.VW_INFO_EMPLEADO EMPL ";
                qry += "                                            WHERE EMPL.COD_EMPLEADO = ' + @cod_empleado + '''' + ')' ";

                qry += " EXEC sp_executesql @qry ";

                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cod_tipo_nomina = dt.Rows[0][0].ToString();
                }

                cnn.Close();
            }
            return cod_tipo_nomina;
        }

        private string devuelveRubro(string cod_empleado, string rubro)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string result = "";

            if (cod_empleado != "")
            {
                cnn.Open();
                cmd.Connection = cnn;

                qry = "  declare @qry		nvarchar(1000) ";
                qry += " set @qry ='select RUBRO  ";
                qry += " from OPENQUERY(ORACLE,' + '''SELECT COD_FREC_NOMINA || COD_TIPO_NOMINA || ''''" + rubro + "'''' RUBRO ";
                qry += " 											 FROM RRHH.CONTRATOS ";
                qry += " 											 WHERE COD_EMPLEADO = " + cod_empleado + "''' + ')' ";
                qry += " EXEC sp_executesql @qry ";

                cmd.CommandText = qry;
                adp.SelectCommand = cmd;
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0][0].ToString();
                }

                cnn.Close();
            }

            return result;
        }

        protected void cmdGuardar_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["RRHHDMXConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string qry = "";
            string num_nomina = "";
            string cod_tipo_nomina = "";
            string fecha = "";
            string rubro = "";
            try
            {
                if (validaCampos() == false)
                {
                    cnn.Open();
                    cmd.Connection = cnn;

                    fecha = dtpFecha.SelectedDate.Value.Day.ToString() + "/" + dtpFecha.SelectedDate.Value.Month.ToString() + "/" + dtpFecha.SelectedDate.Value.Year.ToString();
                    cod_tipo_nomina = devuelveCodTipoNomina(this.txtCodEmpleado.Text).ToString();
                    num_nomina = devuelveNumNomina(cod_tipo_nomina, fecha);
                    rubro = devuelveRubro(txtCodEmpleado.Text, cmbRubro.SelectedItem.Value);

                    if (verificaExisteRegistro(txtCodEmpleado.Text, txtNumDocumento.Text, num_nomina, cmbRubro.SelectedItem.Value, fecha) == false)
                    {
                        qry = " SET DATEFORMAT DMY ";
                        qry += " INSERT INTO OPENQUERY(ORACLE, 'SELECT COD_TIPO_NOMINA, NUM_NOMINA, COD_EMPLEADO, NUM_DOCUMENTO, FECHA, COD_ING_DES_FRTI, BASE, FACTOR, CANTIDAD, MONTO, ESTADO, SYS_USER, SYS_DATE, ESTADO_HEADCOUNT ";
                        qry += "                                FROM NEW_PLANILLA.OTROS_ING_DES ";
                        qry += "                                WHERE NUM_NOMINA = " + num_nomina;
                        qry += "                                    AND COD_TIPO_NOMINA = " + cod_tipo_nomina + "') ";
                        qry += " VALUES (" + cod_tipo_nomina + ", " + num_nomina + ", " + txtCodEmpleado.Text + ", " + txtNumDocumento.Text + ", convert(datetime, '" + fecha + "'), " + rubro + ", " + txtMonto.Text + ", 1, 1, " + txtMonto.Text + ", 'I', 'WEB', GETDATE(), 1)";
                        cmd.CommandText = qry;
                        cmd.ExecuteNonQuery();
                        RadNotification1.Show("El vale fue guardado con exito.");

                        Response.Redirect("~/cafeteria/valesCafeteria.aspx");
                    }
                    else
                    {
                        RadNotification1.Show("El vale que desea ingresar ya existe.");
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
        private string getFoto(string codigo)
        {
            string path = $"//172.20.1.20/APP_Prod/Aplicaciones/Mano_Obra/Fotos/{codigo}.jpg";
            byte[] foto = new WebClient().DownloadData(path);
            string fotostr = Convert.ToBase64String(foto);

            return fotostr;
        }
    }
}