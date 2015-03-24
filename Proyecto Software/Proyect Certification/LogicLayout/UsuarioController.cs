using System;
using System.Collections.Generic;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class UsuarioController
    {
        #region *** Métodos Estaticos públicos ***

        public static List<Usuario> GetAllUsers()
        {
            var lstObj = new List<Usuario>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<Usuario>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  "+ ex.Message);
            }
            return lstObj;
        }

        public static Usuario GetUsuarioByKey(string key)
        {
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    return objDal.consultarPorKey<Usuario>("UsuarioKey", key); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }
        }

        private static Usuario GetUsuarioByUserName(string username)
        {
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    return objDal.consultarPorKey<Usuario>("Nickname", username);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }
        }

        public static Usuario GetUsuarioByUsernameAndPass(string username, string password)
        {
            var pass = EncriptaController.ObtieneEncriptacion(password);
            var objUser = GetUsuarioByUserName(username);

            if (objUser!=null)
            {
                if (objUser.password.ToLower().Equals(pass.ToLower()) && (objUser.Baja == false))
                {
                    return objUser;
                }
                return null;
            }
            return null;
        }

        public static bool AddOrUpdateUser(Usuario objUser, out string sIdCreated)
        {
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    if (objUser.UsuarioKey == null || objUser.UsuarioKey.ToString().Trim().Equals("00000000-0000-0000-0000-000000000000"))
                    {
                        objUser.UsuarioKey = Guid.NewGuid();
                        sIdCreated = objUser.UsuarioKey.ToString();
                        objDal.guardar(objUser);
                    }
                    else
                    {
                        objDal.actualizar(objUser, "UsuarioKey");
                    }
                }
            }
            catch (Exception ex)
            {
                sIdCreated = null;
                return  false;
            }

            sIdCreated = null;
            return true; 
        }

        #endregion
    }
}
