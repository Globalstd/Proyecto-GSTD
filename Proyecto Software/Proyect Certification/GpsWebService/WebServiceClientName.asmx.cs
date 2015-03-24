using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LogicLayout;

namespace GpsWebService
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://webservice.globalstd.com/service", Name = "Servicio App", Description = "Autor: Sergio Alvarez Castellanos")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    //[System.Web.Script.Services.ScriptService]
    public class WebServiceClientName : WebService
    {
        [WebMethod]
        public static List<string> AutoCompleteAjaxRequest(string prefixText, int count)
        {
            var lstOffice = OfficeController.GetAllOffice();

            return lstOffice.Select(office => office.Name).ToList();
        }
    }
}
