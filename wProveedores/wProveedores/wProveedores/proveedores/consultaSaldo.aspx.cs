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
    public partial class consultaSaldo : System.Web.UI.Page
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
            maquina = System.Environment.MachineName;

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
                }
            }
        }

        private void cargaGrid()
        {
            if (txtCodCliente.Text != "")
            { 
                WSDBOracle.DBOracleSoapClient WS = new WSDBOracle.DBOracleSoapClient();
                DataSet ds = WS.GetData(txtCodCliente.Text);
                this.gEmpleados.DataSource = ds.Tables[0];
                gEmpleados.DataBind();
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (e.Item.Index)
            { 
                case 0:
                    cargaGrid();
                    break;
            }
                
        }

    }
}