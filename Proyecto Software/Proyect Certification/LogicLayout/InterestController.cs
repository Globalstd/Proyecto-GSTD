using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class InterestController
    {
        public static List<Interest> GetAllInterest()
        {

            var query = new StringBuilder();

            query.Append(" SELECT *FROM dbo.Interest");
            query.Append(" ORDER BY Name");

            var lstObj = new List<Interest>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<Interest>(query.ToString());
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
