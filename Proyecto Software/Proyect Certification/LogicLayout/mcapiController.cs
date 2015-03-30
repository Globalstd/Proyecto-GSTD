using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailChimp;
using MailChimp.Types;

namespace LogicLayout
{
    public class mcapiController
    {
        public static void AccedeApi(string nombre, string apPaterno, string correo)
        {
            const string apiKey = "eef4bc1722806a84538bc67d058b44c0-us7";   // Replace it before
            string listId = "c9ad222383";                      // Replace it before
            string listId1 = "e321ec7f9d";

            var options = new List.SubscribeOptions
                              {DoubleOptIn = true, EmailType = List.EmailType.Html, SendWelcome = false};

            //var entry = new Dictionary<string, object>();
            //entry.Add("FNAME", "Sergio");
            //entry.Add("LNAME", "Alvarez");
            
            //var lsi =  new listSubscribe(apiKey, listId, "xyz@abc.com", entry);


            var mergeText = new List.Merges()
                    {
                        {"FNAME", nombre},
                        {"LNAME", apPaterno}
                    };
            //var merges = new List<List.Merges> { mergeText };

            var mcApi = new MCApi(apiKey, false);
            

            mcApi.ListSubscribe(listId,correo, mergeText, options);
            //mcApi.ListMergeVarAdd(listId, "Test", "Testing");


            //var lstMailChimp = mcApi.ListClients("e321ec7f9d");
            //mcApi.ListMembers("e321ec7f9d", List.MemberStatus.Subscribed).Data[1];
            //mcApi.ListSubscribe(listId, "evazquez@globalstd.com");

            //var listaClientes = mcApi.ListMembers(listId, List.MemberStatus.Subscribed).Data;
            //var lstMember = mcApi.ListMergeVars(listId); 

            //foreach (var item in listaClientes)
            //{
            //    var email = item.Email;
            //    var description = item.Description;
            //    var reason = item.Reason;
            //    var timestamp = item.Timestamp;
            //}

            //foreach (var mergeVar in lstMember)
            //{
            //    var name = mergeVar.Name;
            //    var ejemplo = mergeVar.HelpText[0]; 
            //}


        }
    }
}
