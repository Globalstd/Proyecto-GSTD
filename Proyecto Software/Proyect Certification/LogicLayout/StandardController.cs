using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class StandardController
    {
        public static List<Standards> GetAll()
        {
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    return objDal.consultarTodos<Standards>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }
        }
    }
}
