using System;
using System.Collections.Generic;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class InterestController
    {
        public static List<InterestType> GetAllInterest()
        {
            var query = new StringBuilder();

            query.Append(" SELECT *FROM dbo.InterestType");
            query.Append(" ORDER BY Name");

            var lstObj = new List<InterestType>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<InterestType>(query.ToString());
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
