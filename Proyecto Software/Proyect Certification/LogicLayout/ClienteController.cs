using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayout.DAL;
using EntidadesLayout.Ent;

namespace LogicLayout
{
    public class ClienteController
    {
        public static List<Cliente> GetAllCliente()
        {
            var lstObj = new List<Cliente>();
            var query = new StringBuilder();
            query.Append("SELECT *FROM dbo.Cliente");
            query.Append(" ORDER BY RazonSocial ASC"); 

            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<Cliente>(query.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }
            return lstObj;
        }

        public static List<Cliente> GetAllCliente(int pag, int cantReg )
        {
            var lstObj = new List<Cliente>();
            var query = new StringBuilder();
            var orden = "NombreCliente";

            query.Append(" SELECT cliente.ClaveCliente, cliente.NombreCliente, oficina.Name as 'OfficeName', cliente.Baja 'Status'");
            query.Append(" FROM dbo.Cliente cliente ");
            query.Append(" LEFT JOIN dbo.Office oficina ON cliente.OfficeFk = oficina.OfficeKey ");
            query.Append(" WHERE cliente.Baja = 0");

            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultar<Cliente>(pag, cantReg, orden, query.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }
            return lstObj;
        }

        public static List<string> GetAllNameClient()
        {
            var lstObj = new List<Cliente>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<Cliente>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }

            var lstNameClient = new List<string>();

            foreach(var item in lstObj)
            {
                lstNameClient.Add(item.NombreCliente);
            }

            return lstNameClient;
        }

        public static List<string> GetAllRazon()
        {
            var lstObj = new List<Cliente>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<Cliente>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }

            var lstNameClient = new List<string>();

            foreach (var item in lstObj)
            {
                lstNameClient.Add(item.RazonSocial);
            }

            return lstNameClient;
        }

        public static List<string> GetAllRfc()
        {
            var lstObj = new List<Cliente>();
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    lstObj = objDal.consultarTodos<Cliente>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error:  " + ex.Message);
            }

            var lstNameClient = new List<string>();

            foreach (var item in lstObj)
            {
                lstNameClient.Add(item.RFC);
            }

            return lstNameClient;
        }

        public static bool AddOrUpdateCliente(Cliente objClient, out string sIdCreated)
        {
            try
            {
                using (var objDal = new BaseDAL_II())
                {
                    if (objClient.Clientekey == null || objClient.Clientekey.ToString().Trim().Equals("00000000-0000-0000-0000-000000000000"))
                    {
                        objClient.Clientekey = Guid.NewGuid();
                        sIdCreated = objClient.Clientekey.ToString();
                        objDal.guardar(objClient);
                    }
                    else
                    {
                        objDal.actualizar(objClient, "ClienteKey");
                    }
                }
            }
            catch (Exception ex)
            {
                sIdCreated = null;
                return false;
            }

            sIdCreated = null;
            return true; 
        }

    }
}
