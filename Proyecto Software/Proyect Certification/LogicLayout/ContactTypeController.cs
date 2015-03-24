using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class ContactTypeController
    {
        public static List<ContactType> GetAllTypeContact()
        {
            var lstObj = new List<ContactType>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<ContactType>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }

            return lstObj;
        }
    }
}
