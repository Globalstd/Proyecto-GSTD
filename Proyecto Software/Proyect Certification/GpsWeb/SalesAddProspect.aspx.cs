using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicLayout;

namespace GpsWeb
{
    public partial class SalesAddProspect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAutoComplete();
                fillDdl();
            }
        }

        private void LoadAutoComplete()
        {
            var lstClientName = ClienteController.GetAllNameClient();
            var lstClientRazon = ClienteController.GetAllRazon();
            var lstClientRfc = ClienteController.GetAllRfc();
            var lstContactMail = ContactController.GetAllContactMail();

            var autoClientName = (Control.Autocomplete)LoadControl("~/Control/Autocomplete.ascx");
            autoClientName.SourceAutocomplete = lstClientName;

            divClientName.Controls.Add(autoClientName);

            var autoClientRazon = (Control.Autocomplete)LoadControl("~/Control/Autocomplete.ascx");
            autoClientRazon.SourceAutocomplete = lstClientRazon;

            divClientRazon.Controls.Add(autoClientRazon);

            var autoClientRfc = (Control.Autocomplete)LoadControl("~/Control/Autocomplete.ascx");
            autoClientRfc.SourceAutocomplete = lstClientRfc;

            divClientRfc.Controls.Add(autoClientRfc);

            var autoContactMail = (Control.Autocomplete)LoadControl("~/Control/Autocomplete.ascx");
            autoContactMail.SourceAutocomplete = lstContactMail;

            divContactMail.Controls.Add(autoContactMail);
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
            ddlInteres.DataValueField = "InterestKey";
            ddlInteres.DataBind();

            ddlInteres.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));

            ddlTypeContact.DataSource = ContactTypeController.GetAllTypeContact();
            ddlTypeContact.DataTextField = "Name";
            ddlTypeContact.DataValueField = "ContactTypeKey";
            ddlTypeContact.DataBind();

            ddlTypeContact.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));

            ddlStandard.DataSource = StandardController.GetAll();
            ddlStandard.DataTextField = "Name";
            ddlStandard.DataValueField = "StandardKey";
            ddlStandard.DataBind();

            ddlStandard.Items.Insert(0, new ListItem("-Ninguno-", Guid.Empty.ToString()));

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

    }

}

