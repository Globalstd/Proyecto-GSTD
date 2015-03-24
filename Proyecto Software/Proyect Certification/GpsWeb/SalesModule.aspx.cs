using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayout;

namespace GpsWeb
{
    public partial class SalesModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillGrid(-1, 10);
            }
        }

        private void fillGrid(int pag, int cantReg)
        {
            pag = pag == -1 ? 1 : pag;
            gdvCliente.DataSource = ClienteController.GetAllCliente(pag, cantReg);
            gdvCliente.DataBind();
        }

        protected void gdvCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillGrid(e.NewPageIndex, 10);
        }

        protected void btnAddProspect_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalesAddProspect.aspx");
        }
    }
}