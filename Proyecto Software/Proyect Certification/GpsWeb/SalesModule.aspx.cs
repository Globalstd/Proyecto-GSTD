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
                fillGrid();
            }
        }

        private void fillGrid()
        {
            gdvCliente.DataSource = ClienteController.GetAllCliente();
            gdvCliente.DataBind();
        }
        protected void btnAddProspect_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalesAddProspect.aspx");
        }

        protected void gdvCliente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvCliente.PageIndex = e.NewPageIndex; 
            gdvCliente.DataBind();
        }

    }
}