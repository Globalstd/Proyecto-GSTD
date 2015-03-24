using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class SourceClientController
    {
        public static List<SourceClient> GetAllSourceClient()
        {

            var query = new StringBuilder();

            query.Append(" SELECT *FROM dbo.SourceClient");
            query.Append(" ORDER BY Name");

            var lstObj = new List<SourceClient>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<SourceClient>(query.ToString());
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
