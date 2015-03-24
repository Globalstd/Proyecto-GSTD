using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayout;

namespace GpsWeb
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VerificarSesion();
                LoadMenuPrincipal(Session["Certifi"].ToString());
            }
        }

        public bool VerificarSesion()
        {
            var vSession = Session["Usuario"] != null;
            
            PnlDefault.Visible = vSession;
            pnlLogin.Visible = !vSession;

            if (Session["Oficina"] == null)
            {
                ddlOffice.SelectedValue = "c3ab8e8f-cae4-4912-ac9b-c67f935bc023";
                Session["Oficina"] = "c3ab8e8f-cae4-4912-ac9b-c67f935bc023";
            }
            else
            {
                ddlOffice.SelectedValue = Session["Oficina"].ToString();
            }

            if (Session["Certifi"] == null)
            {
                ddlCertificacionCurso.SelectedValue = "certification";
                Session["Certifi"] = "certification";
            }
            else
            {
                ddlCertificacionCurso.SelectedValue = Session["Certifi"].ToString();
            }


            if (Session["Idioma"] == null)
            {
                ddlLenguage.SelectedValue = "es-MX";
                Session["Idioma"] = "es-MX";
            }
            else
            {
                ddlLenguage.SelectedValue = Session["Idioma"].ToString();
            }

            if(vSession)
            {
                fillGrid();
            }
            else
            {
                var pagina = HttpContext.Current.Request.Url.LocalPath;
                if (!pagina.Equals("/Default.aspx"))
                {
                    Response.Redirect("/Default.aspx");
                }
            }

            //Revisar modo dios (ver si tiene permiso)
            //lblModoDios

            return vSession;
        }

        protected void btnAcceder_Click(object sender, EventArgs e)
        {
            var objUsuario = UsuarioController.GetUsuarioByUsernameAndPass(txtUsuario.Text, txtPassword.Text);

            if (objUsuario != null)
            {
                Session["Usuario"] = objUsuario;
                VerificarSesion();
                Response.Redirect("~/Default.aspx");
                
            }
            else
            {
                PnlError.Style.Add("display", "block");
            }
        }
        protected void lnkSingOut_click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        private void fillGrid()
        {
            ddlOffice.DataSource = OfficeController.GetAllOffice();
            ddlOffice.DataTextField = "Name";
            ddlOffice.DataValueField = "OfficeKey";
            ddlOffice.DataBind();
        }

        protected void ddlLenguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Idioma"] = ddlLenguage.SelectedValue;
            var pagina = HttpContext.Current.Request.Url.LocalPath;
            Response.Redirect(pagina);
        }

        protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Oficina"] = ddlOffice.SelectedValue;
            var pagina = HttpContext.Current.Request.Url.LocalPath;
            Response.Redirect(pagina);
        }
        protected void ddlCertificacionCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Certifi"] = ddlCertificacionCurso.SelectedValue;
            var pagina = HttpContext.Current.Request.Url.LocalPath;
            Response.Redirect(pagina);
        }

        public void LoadMenuPrincipal(string SessionCertificacion)
        {
            var certificacion = SessionCertificacion.Equals("certification");
            if (certificacion)
            {
                lnkVentas.PostBackUrl = "SalesModule.aspx"; 
            }
            lnkVentas.Visible = true;
            lnkPlaneacion.Visible = true;
            lnkServicio.Visible = true;
            lnkAuditores.Visible = certificacion;
            lnkInstructor.Visible = !certificacion;
            lnkRecursos.Visible = true;
            lnkFinanzas.Visible = true;
            lnkCompetencias.Visible = true;
        }


    }
}