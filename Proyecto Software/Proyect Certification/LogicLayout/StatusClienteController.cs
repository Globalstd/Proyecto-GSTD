using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class StatusClienteController
    {
        public static List<StatusCliente> GetAll()
        {
            var query = new StringBuilder();
            query.Append(" SELECT *FROM dbo.StatusCliente");
            query.Append(" Order by Orden ASC");

            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    return objDal.consultar<StatusCliente>(query.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }
        }
    }
}
