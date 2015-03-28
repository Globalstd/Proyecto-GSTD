using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayout;

namespace GpsWeb.Control
{
    public partial class GridViewCostum : System.Web.UI.UserControl
    {
        public string id;

        [Browsable(true), Category("Appearance"), Description("Lista de string para autocomplete.")]
        public string IdjQuery { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = IdjQuery;
                fillGrid(-1, 10);
            }
        }

        private void fillGrid(int pag, int cantReg)
        {
            pag = pag == -1 ? 1 : pag;
            gdvCliente.DataSource = ClienteController.GetAllCliente(pag, cantReg);
            gdvCliente.DataBind();


            gdvCliente.UseAccessibleHeader = true;

            if (gdvCliente.HeaderRow != null)
            {
                gdvCliente.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
    }
}