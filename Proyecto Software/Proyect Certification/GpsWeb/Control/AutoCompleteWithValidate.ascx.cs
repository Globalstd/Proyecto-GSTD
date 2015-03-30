using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GpsWeb.Control
{
    public partial class AutoCompleteWithValidate : System.Web.UI.UserControl
    {
        public string cadena;
        public string id;

        [Browsable(true), Category("Appearance"), Description("Lista de string para autocomplete.")]
        public List<string> SourceAutocomplete { get; set; }

        [Browsable(true), Category("Appearance"), Description("Lista de string para autocomplete.")]
        public string IdjQuery { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var listaCadena = new StringBuilder();
                SourceAutocomplete = SourceAutocomplete ?? new List<string>();

                listaCadena.Append("[");

                foreach (var item in SourceAutocomplete)
                {
                    listaCadena.Append("\"");
                    listaCadena.Append(item);
                    listaCadena.Append("\"");
                    listaCadena.Append(", ");
                }

                if (listaCadena.Length > 2)
                    listaCadena = listaCadena.Remove(listaCadena.Length - 2, 2);
                listaCadena.Append("];");
                cadena = listaCadena.ToString();
                id = IdjQuery;
            }
        }
        

    }
}