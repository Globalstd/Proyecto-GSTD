using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LogicLayout;

namespace GpsWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Certifi"] == null)
            {
                Session["Certifi"] = "certification";
            }
            LoadMenuLateral(Session["Certifi"].ToString());
        }

        private void LoadMenuLateral(string sessionCertificacion)
        {
            var certificacion = sessionCertificacion.Equals("certification");
            var controlVentas = FillControl(Resources.Idioma.lblVentasLateral, "SalesModule.aspx");
            ulMenuLateral.Controls.Add(controlVentas);
            var controlPlaneacion = FillControl(Resources.Idioma.lblPlaneacionLateral, "SalesModule.aspx");
            ulMenuLateral.Controls.Add(controlPlaneacion);
            var controlServicio = FillControl(Resources.Idioma.lblServicioLateral, "SalesModule.aspx");
            ulMenuLateral.Controls.Add(controlServicio);

            if (certificacion)
            {
                var controlAuditores = FillControl(Resources.Idioma.lblAuditoresLateral, "SalesModule.aspx");
                ulMenuLateral.Controls.Add(controlAuditores);
            }
            else
            {
                var controlInstructores = FillControl(Resources.Idioma.lblInstructorLateral, "SalesModule.aspx");
                ulMenuLateral.Controls.Add(controlInstructores);
            }

            var controlRecursos = FillControl(Resources.Idioma.lblRecursosLateral, "SalesModule.aspx");
            ulMenuLateral.Controls.Add(controlRecursos);
            var controlFinanzas = FillControl(Resources.Idioma.lblFinanzasLateral, "SalesModule.aspx");
            ulMenuLateral.Controls.Add(controlFinanzas);
            var controlCompetencias = FillControl(Resources.Idioma.lblCompetenciasLateral, "SalesModule.aspx");
            ulMenuLateral.Controls.Add(controlCompetencias);
        }

        private HtmlGenericControl FillControl(string recurso, string href)
        {
            var liControl = new HtmlGenericControl("li");
            var aControl = new HtmlGenericControl("a");
            aControl.Attributes.Add("class","list-group-item");
            aControl.Attributes.Add("style","border: none; color: #111;");
            aControl.Attributes.Add("href", href);
            var spanControl = new HtmlGenericControl("span");
            spanControl.Attributes.Add("class", "glyphicon glyphicon-hand-right");
            spanControl.Attributes.Add("aria-hidden", "true");
            spanControl.Attributes.Add("style", "padding-right: 10px;");
            aControl.Controls.Add(spanControl);
            var literal = new LiteralControl { Text = recurso};
            aControl.Controls.Add(literal);
            liControl.Controls.Add(aControl);

            return liControl;
        }
    }
}