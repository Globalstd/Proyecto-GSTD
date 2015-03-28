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

        public static List<Standards> GetAllStandardWhitCourse()
        {
            var query = new StringBuilder();
            query.Append(" SELECT DISTINCT dbo.StandardCourse.StandardFk as StandardKey, dbo.Standards.Name");
            query.Append(" FROM dbo.StandardCourse");
            query.Append(" LEFT JOIN dbo.Standards ON dbo.StandardCourse.StandardFk = dbo.Standards.StandardKey");
            try
            {
                using (var objDal =new BaseDAL_II())
                {
                    return objDal.consultar<Standards>(query.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }
        }
    }
}
