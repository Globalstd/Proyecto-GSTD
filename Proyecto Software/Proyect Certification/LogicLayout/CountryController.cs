using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class CountryController
    {
        public static List<Country> GetAllConuntry()
        {
            var lstObj = new List<Country>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<Country>();
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
