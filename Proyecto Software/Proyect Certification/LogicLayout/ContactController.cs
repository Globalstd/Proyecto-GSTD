using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class ContactController
    {
        public static List<string> GetAllContactMail()
        {
            var lstObj = new List<ContactSite>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<ContactSite>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }

            var lstContactMail = new List<string>();

            foreach (var item in lstObj)
            {
                lstContactMail.Add(item.Email);
            }

            return lstContactMail;
        }

        public static bool AddOrUpdateContact(ContactSite objContact, out string sIdCreated)
        {
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    if (objContact.ContactSiteKey == null || objContact.ContactSiteKey.ToString().Trim().Equals("00000000-0000-0000-0000-000000000000"))
                    {
                        objContact.ContactSiteKey = Guid.NewGuid();
                        objDal.guardar(objContact);
                    }
                    else
                    {
                        objDal.actualizar(objContact, "ContactSiteKey");
                    }
                }
            }
            catch (Exception ex)
            {
                sIdCreated = null;
                return false;
            }

            sIdCreated = objContact.ContactSiteKey.ToString();
            return true;
        }
    }
}
