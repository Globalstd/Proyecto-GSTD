using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class StateController
    {
        public static List<States> GetAllStates(string countryKey)
        {
            
            var query = new StringBuilder();

            query.Append(" SELECT *FROM dbo.States");
            query.Append(String.Format(" WHERE CountryFk = '{0}'", countryKey));
            query.Append(" ORDER BY Name");

            var lstObj = new List<States>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<States>(query.ToString());
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
