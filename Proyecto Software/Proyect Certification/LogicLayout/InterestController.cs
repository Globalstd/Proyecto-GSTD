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

        public static bool AddOrUpdateInterest(Interest objInterest, out string sIdCreated)
        {
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    if (objInterest.InterestKey == null || objInterest.InterestKey.ToString().Trim().Equals("00000000-0000-0000-0000-000000000000"))
                    {
                        objInterest.InterestKey = Guid.NewGuid();
                        objDal.guardar(objInterest);
                    }
                    else
                    {
                        objDal.actualizar(objInterest, "InterestKey");
                    }
                }
            }
            catch (Exception ex)
            {
                sIdCreated = null;
                return false;
            }

            sIdCreated = objInterest.InterestKey.ToString();
            return true;
        }

    }
}
