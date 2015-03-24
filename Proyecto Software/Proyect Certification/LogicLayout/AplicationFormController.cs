using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class AplicationFormController
    {
        #region "Metodos Publicos Staticos"

            public static AplicationForm GetAplicationByKey(string key)
            {
                try
                {
                    using (var objDal = new BaseDAL_II())
                    {
                        return objDal.consultarPorKey<AplicationForm>("AplicationFormkey", key);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error:  " + ex.Message);
                }
            }
            
            public static List<AplicationForm> GetAll()
            {
                try
                {
                    using (var objDal = new BaseDAL_II())
                    {
                        return objDal.consultarTodos<AplicationForm>();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error:  " + ex.Message);
                }
            }

            //public static bool ChangeToContract(string key)
            //{

            //}

        #endregion
    }
}
