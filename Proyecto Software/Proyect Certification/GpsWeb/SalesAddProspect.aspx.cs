using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesLayout.Ent;
using LogicLayout;

namespace GpsWeb
{
    public partial class SalesAddProspect : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            CargarControles();
        }

        protected void CargarControles()
        {
            var lstClientName = ClienteController.GetAllNameClient();
            var lstClientRazon = ClienteController.GetAllRazon();
            var lstClientRfc = ClienteController.GetAllRfc();
            var lstContactMail = ContactController.GetAllContactMail();

            var autoClientName = (Control.Autocomplete)LoadControl("~/Control/Autocomplete.ascx");
            autoClientName.IdjQuery = "ContenedorPrincipal_ctl02_txtAutoComplete"; 
            autoClientName.SourceAutocomplete = lstClientName;

            pnlClientName.Controls.Add(autoClientName);

            var autoClientRazon = (Control.AutoCompleteWithValidate)LoadControl("~/Control/AutoCompleteWithValidate.ascx");
            autoClientRazon.SourceAutocomplete = lstClientRazon;

            divClientRazon.Controls.Add(autoClientRazon);

            var autoClientRfc = (Control.Autocomplete)LoadControl("~/Control/Autocomplete.ascx");
            autoClientRfc.IdjQuery = "ContenedorPrincipal_ctl04_txtAutoComplete"; 
            autoClientRfc.SourceAutocomplete = lstClientRfc;

            divClientRfc.Controls.Add(autoClientRfc);

            var autoContactMail = (Control.AutoCompleteWithValidate)LoadControl("~/Control/AutoCompleteWithValidate.ascx");
            autoContactMail.SourceAutocomplete = lstContactMail;

            divContactMail.Controls.Add(autoContactMail);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillDdl();
            }
        }

        private void fillDdl()
        {
            ddlPais.DataSource = CountryController.GetAllConuntry();
            ddlPais.DataTextField = "Name";
            ddlPais.DataValueField = "CountryKey";
            ddlPais.DataBind();

            ddlPais.SelectedValue = "3177E8C2-FBAA-424B-A60F-0BAF24C34F18";

            ddlEstado.DataSource = StateController.GetAllStates(ddlPais.SelectedValue);
            ddlEstado.DataTextField = "Name";
            ddlEstado.DataValueField = "StateKey";
            ddlEstado.DataBind();

            ddlFuente.DataSource = SourceClientController.GetAllSourceClient();
            ddlFuente.DataTextField = "Name";
            ddlFuente.DataValueField = "SourceClientKey";
            ddlFuente.DataBind();

            ddlFuente.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));


            ddlInteres.DataSource = InterestController.GetAllInterest();
            ddlInteres.DataTextField = "Name";
            ddlInteres.DataValueField = "InterestTypeKey";
            ddlInteres.DataBind();

            ddlInteres.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));
            ClearDdlInterest();

            ddlTypeContact.DataSource = ContactTypeController.GetAllTypeContact();
            ddlTypeContact.DataTextField = "Name";
            ddlTypeContact.DataValueField = "ContactTypeKey";
            ddlTypeContact.DataBind();

            ddlTypeContact.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));

            ddlStatusCliente.DataSource = StatusClienteController.GetAll();
            ddlStatusCliente.DataTextField = "Name";
            ddlStatusCliente.DataValueField = "StatusClienteKey";
            ddlStatusCliente.DataBind();

            ddlStatusCliente.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));
           
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEstado.DataSource = StateController.GetAllStates(ddlPais.SelectedValue);
            ddlEstado.DataTextField = "Name";
            ddlEstado.DataValueField = "StateKey";
            ddlEstado.DataBind();

            updatePanelStates.Update();
        }

        protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillddlStandard();
        }

        private void ClearDdlInterest()
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));
            ddlStandard.Items.Clear();
            ddlStandard.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));
        }

        protected void ddlInteres_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlInteres.SelectedValue)
            {
                //Auditorias de 2da parte
                case "347ac229-609c-4d9d-b865-7b53a8ca37e4":
                    ddlCourse.Enabled = false;
                    ddlStandard.Enabled = false;
                    ClearDdlInterest();
                    break;
                //Cursos 
                case "e9eb421b-c84e-4ed0-9e62-70d8488cb379": 
                    ddlCourse.Enabled = true;
                    ddlStandard.Enabled = true;
                    FillddlStandardForCourse();
                    break;
                //Auditorias / Certificación & Capacitación
                case "1b74f20a-975d-430f-a105-2ae0b428edf5": case "d9d50ca4-cbaa-43e0-a12f-fb4b8b92e11e":
                    ddlCourse.Enabled = false;
                    ddlStandard.Enabled = true;
                    fillddlStandardComplete();
                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));
                    break;
                default:
                    ClearDdlInterest();
                    break;
            }
        }

        private void FillddlStandard()
        {
            if (!ddlInteres.SelectedValue.Equals("1b74f20a-975d-430f-a105-2ae0b428edf5") && !ddlInteres.SelectedValue.Equals("d9d50ca4-cbaa-43e0-a12f-fb4b8b92e11e"))
            {
                ddlCourse.Items.Clear();
                ddlCourse.DataSource = CourseController.GetAllCourseByStandardKey(ddlStandard.SelectedValue);
                ddlCourse.DataTextField = "Name";
                ddlCourse.DataValueField = "StandardCourseKey";
                ddlCourse.DataBind();
            }
        }

        private void FillddlStandardForCourse()
        {
            ddlStandard.Items.Clear();
            ddlStandard.DataSource = StandardController.GetAllStandardWhitCourse();
            ddlStandard.DataTextField = "Name";
            ddlStandard.DataValueField = "StandardKey";
            ddlStandard.DataBind();

            ddlStandard.SelectedValue = "26c2d21c-68e2-427a-8e0c-5df9ba8eae95";

            ddlCourse.Items.Clear();
            ddlCourse.DataSource = CourseController.GetAllCourseByStandardKey(ddlStandard.SelectedValue);
            ddlCourse.DataTextField = "Name";
            ddlCourse.DataValueField = "StandardCourseKey";
            ddlCourse.DataBind();
        }

        private void fillddlStandardComplete()
        {
            ddlStandard.DataSource = StandardController.GetAll();
            ddlStandard.DataTextField = "Name";
            ddlStandard.DataValueField = "StandardKey";
            ddlStandard.DataBind();

            ddlStandard.Items.RemoveAt(4);
        }

        protected void btnEnviarMail_Click(object sender, EventArgs e)
        {
            //mcapiController.AccedeApi();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            AddProspect();
        }

        private void AddProspect()
        {
            var txtName = (TextBox)pnlClientName.Controls[3].Controls[0];
            var txtRazon = (TextBox)divClientRazon.Controls[1].Controls[1]; 
            var txtRfc = (TextBox)divClientRfc.Controls[1].Controls[0];
            var txxContactMail = (TextBox)divContactMail.Controls[1].Controls[1];

            var objProspecto = new Cliente();
            objProspecto.NombreCliente = txtName.Text;
            objProspecto.RazonSocial = txtRazon.Text;
            objProspecto.IsCliente = false;
            objProspecto.RFC = txtRfc.Text;

            string sIdCreated;
            ClienteController.AddOrUpdateCliente(objProspecto, out sIdCreated); 




        }


    }

}

