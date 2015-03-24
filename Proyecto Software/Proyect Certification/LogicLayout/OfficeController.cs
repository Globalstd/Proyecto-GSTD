using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class OfficeController
    {
        public static List<Office> GetAllOffice()
        {
            var lstObj = new List<Office>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<Office>(1, 3, "Orden ASC", "SELECT *FROM dbo.Office");    //consultarTodos<Office>();
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
