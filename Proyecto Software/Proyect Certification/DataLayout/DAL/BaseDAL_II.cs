using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataLayout.DAL.Utilerias;

namespace DataLayout.DAL
{
    public class BaseDAL_II : IDisposable
    {
        #region Atributos
        public DBConn_II objDBConn;       
        IDictionary<string, object> filtroGeneral = new Dictionary<string, object>();
        public string sCnn;    

        public IDictionary<string, object> GetFiltroGeneral<T>(IDictionary<string, object> filtros) where T : class, new()
        {

            System.Reflection.PropertyInfo objProp = null;
            T objEntidad = new T();            
            Type type = objEntidad.GetType();              

            foreach (KeyValuePair<string, object> filtro in filtroGeneral)
            {
                objProp = type.GetProperty(filtro.Key);
                if (objProp != null && !filtros.ContainsKey(filtro.Key) && objProp.DeclaringType == objProp.ReflectedType)
                    filtros.Add(filtro);
            }
            return filtros;                   
        }

        public IDictionary<string, object> GetFiltroGeneral<T>(string s, object o) where T : class, new()
        {
            IDictionary<string, object> filtros = new Dictionary<string, object>();
            filtros.Add(s, o);

            System.Reflection.PropertyInfo objProp = null;
            T objEntidad = new T();
            Type type = objEntidad.GetType();

            foreach (KeyValuePair<string, object> filtro in filtroGeneral)
            {
                objProp = type.GetProperty(filtro.Key);
                if (objProp != null)
                    filtros.Add(filtro);
            }
            return filtros;
        }

        public void SetFiltroGeneral(IDictionary<string, object> filtros)
        {
            filtroGeneral = filtros;
        }

        private SqlTransaction objTran;
        public SqlTransaction Transaccion
        {
            get
            {
                return objTran;
            }
            set {
                objTran = value;
            }
        }
        #endregion

        #region Constructor
        public BaseDAL_II()
        {
            this.objDBConn = new DBConn_II();
            sCnn = this.objDBConn.getCnn();
        }
        public string getCnn()
        {
            return sCnn;
        }

        public BaseDAL_II(bool crearTransaccion)
        {
            this.objDBConn = new DBConn_II(crearTransaccion);
        }
        public BaseDAL_II(IDbTransaction transac)
        {
            this.objDBConn = new DBConn_II(transac); 
            objTran = (SqlTransaction)transac;
        }
        #endregion

        #region Destructor
        ~BaseDAL_II()
        {
            //this.objDBConn = null;
        }
        #endregion

        #region guardar

        public void guardarPorTipo<T>(T objEntidad) where T : class, new()
        {
            try
            {
                //Obtener el script de inserción, y agregarlo al list completo de la transacción
                string strSQL = this.objDBConn.fnObtenerCadenaINSERT(objEntidad, new T().GetType());
                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int guardar<T>(T objEntidad) where T: class,new()
        {   
            try
            {
                string strSQL = this.objDBConn.fnObtenerCadenaINSERT(objEntidad);
                object ob = this.objDBConn.fnEjecutarComandoEscalar(strSQL);
                if (ob == DBNull.Value) return 0;
                return Convert.ToInt32(ob);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message, ex);
                
            }
            
        }

        public void guardar<T>(List<T> listaEntidades) where T: class
        {
            try
            {
                if (listaEntidades.Count > 0) { 
                    //Obtener el nombre de la entidad
                    string tabla = listaEntidades[0].GetType().Name;
                    //Obtener el listado de nombres de propiedades/atributos
                    string[] arrCampos = this.objDBConn.getNombresAtributos(listaEntidades[0]);

                    string strSQL = string.Empty;

                    foreach (object objEntidad in listaEntidades)
                    {
                        try
                        {
                            //Obtener listado de valores
                            object[] arrValores = this.objDBConn.getValoresAtributos(objEntidad, arrCampos);
                            //Obtener el script de inserción
                            strSQL = this.objDBConn.fnObtenerCadenaINSERT(tabla, arrCampos, arrValores);

                            this.objDBConn.fnEjecutarComando(strSQL);
                        }
                        catch {
                            int i = 0;
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region consultar

        //public abstract void filtroGeneral(IDictionary<string, object> fg); 

        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, IDictionary<string, object> filtros) where T: class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag,cantReg,Ordenamiento,objEntidad,filtros);
            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);                                              
        }

        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, List<string> filtros, ref int top) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            IDictionary<string, object> filtrosD = new Dictionary<string, object>();
            filtrosD = GetFiltroGeneral<T>(filtrosD);

            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, objEntidad, filtros, filtrosD);
            string strMaxSQL = this.objDBConn.fnObtenerCadenaCOUNTPaginado(Ordenamiento, objEntidad, filtros);

            try
            {
                if (pag == 1)
                    top = (int)this.objDBConn.fnEjecutarComandoEscalar(strMaxSQL);


                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }
   
        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, List<string> filtros) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            IDictionary<string, object> filtrosD = new Dictionary<string, object>();
            filtrosD = GetFiltroGeneral<T>(filtrosD);

            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, objEntidad, filtros, filtrosD);
            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, string consulta) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();

            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, consulta);
            
            try
            {               

                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultar<T>(string consulta) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            string strSQL = consulta;
            try
            {

                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    try
                    {
                        T objNew = new T();
                        listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                    }
                    catch (Exception ex) {
                        throw new Exception(ex.Message, ex);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, IDictionary<string, object> filtros,ref int top) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, objEntidad ,filtros);
            string strMaxSQL = this.objDBConn.fnObtenerCadenaCOUNTPaginado(Ordenamiento, objEntidad, filtros);

            try
            {
                if (pag==1)
                top = (int)this.objDBConn.fnEjecutarComandoEscalar(strMaxSQL);

                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, IDictionary<string, object> filtros, string consulta, ref int top) where T : class, new() {
            return consultar<T>(pag,cantReg,Ordenamiento,filtros,consulta,ref top,false);
        }
       
        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, IDictionary<string, object> filtros,string consulta, ref int top,bool incluirPropiedadesHerencia) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, consulta, filtros);
            string strMaxSQL = this.objDBConn.fnObtenerCadenaCOUNTPaginado(consulta, filtros);

            try
            {
                if (pag == 1)
                    top = (int)this.objDBConn.fnEjecutarComandoEscalar(strMaxSQL);

                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR,incluirPropiedadesHerencia));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultarTop<T>(string campoOrdenar, EnumOrdernamiento enumOrdernamiento, int top, IDictionary<string, object> filtros) where T : class, new()
        {
            List<T> listaObj = new List<T>();

            if (null == filtros)
                filtros = new Dictionary<string, object>();
            filtros = GetFiltroGeneral<T>(filtros);

            string ordenamiento = string.Empty;
            switch (enumOrdernamiento)
            {
                case EnumOrdernamiento.OrdenarASC:
                    ordenamiento = string.Format("ORDER BY {0} ASC", campoOrdenar);
                    break;
                case EnumOrdernamiento.OrdenarDESC:
                    ordenamiento = string.Format("ORDER BY {0} DESC", campoOrdenar);
                    break;
            }

            string strSQL = this.objDBConn.fnObtenerCadenaSELECTTOP(typeof(T).Name,campoOrdenar, top, filtros, ordenamiento);
            
            try
            {               
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultar<T>(int pag, int cantReg, string Ordenamiento, string consulta, ref int top) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, consulta);
            string strMaxSQL = this.objDBConn.fnObtenerCadenaCOUNTPaginado(Ordenamiento, consulta);
            try
            {
                if (pag == 1)
                top = (int)this.objDBConn.fnEjecutarComandoEscalar(strMaxSQL);

                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }         

        public T consultarEscalar<T>(string Ordenamiento, IDictionary<string, object> filtros) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            filtros = GetFiltroGeneral<T>(filtros);            
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, Ordenamiento, filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                if (dt != null && dt.Rows.Count > 0)
                    objEntidad = (T)objDBConn.materializaEntidad(objEntidad, dt.Rows[0]);
                else
                    objEntidad = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return (objEntidad);
        }

        public T consultarEscalar<T>(string Ordenamiento, List<string> filtros) where T : class, new()
        {

            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            IDictionary<string, object> filtrosD = new Dictionary<string, object>();
            filtrosD = GetFiltroGeneral<T>(filtrosD);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, Ordenamiento, filtros, filtrosD);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                if (dt != null && dt.Rows.Count > 0)
                    objEntidad = (T)objDBConn.materializaEntidad(objEntidad, dt.Rows[0]);
                else
                    objEntidad = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return (objEntidad);
        }

        public T consultarEscalar<T>(int pag, int cantReg, string Ordenamiento, string consulta) where T : class, new()
        {
           
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();

            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, consulta);
            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                if (dt != null && dt.Rows.Count > 0)
                    objEntidad = (T)objDBConn.materializaEntidad(objEntidad, dt.Rows[0]);
                else
                    objEntidad = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (objEntidad);
        }

        public T consultarEscalar<T>(int pag, int cantReg, string Ordenamiento, IDictionary<string, object> filtros) where T : class, new()
        {
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPaginado(pag, cantReg, Ordenamiento, objEntidad, filtros);
            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                if (dt != null && dt.Rows.Count > 0)
                    objEntidad = (T)objDBConn.materializaEntidad(objEntidad, dt.Rows[0]);
                else
                    objEntidad = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (objEntidad);
        }

        public T consultarPorKey<T>(string nameField, object valueField) where T: class, new()
        {
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            IDictionary<string, object> filtros = new Dictionary<string, object>();
            filtros.Add(nameField, valueField);
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad,"", filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                if (dt != null && dt.Rows.Count > 0)
                    objEntidad = (T)objDBConn.materializaEntidad(objEntidad, dt.Rows[0]);
                else
                    objEntidad = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (objEntidad);
        }


        public THeredado consultarPorKey<T,THeredado>(string nameField, object valueField)
            where THeredado : class , new()
            where T : class , new()
        {

            T objEntidad = new T();
            THeredado obj2 = new THeredado();
            IDictionary<string, object> filtros = new Dictionary<string, object>();
            filtros.Add(nameField, valueField);
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, "", filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj2 = (THeredado)objDBConn.materializaEntidad(obj2, dt.Rows[0]);
                }
                else
                    obj2 = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (obj2);
        }

        public T consultarPorKey<T>(IDictionary<string, object> filtros) where T : class, new()
        {
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();                       
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad,"", filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                if (dt != null && dt.Rows.Count > 0)
                    objEntidad = (T)objDBConn.materializaEntidad(objEntidad, dt.Rows[0]);
                else
                    objEntidad = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (objEntidad);
        }

        public List<T> consultarPorCampo<T>(string nameField, object valueField) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            IDictionary<string, object> filtros = new Dictionary<string, object>();
            filtros.Add(nameField, valueField);
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());
                
                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultarPorCampo<T>(IDictionary<string, object> filtros) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            filtros = GetFiltroGeneral<T>(filtros);           
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultarPorCampo<T>(string Ordenamiento, List<string> filtros) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            IDictionary<string, object> filtrosD = new Dictionary<string, object>();
            filtrosD = GetFiltroGeneral<T>(filtrosD);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, Ordenamiento, filtros, filtrosD);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return (listaObj);
        }
        /// <summary>
        /// Recibe un listado de Ids para hacer la consulta
        /// Nota: Si se envia un listado vacio,devuelve todos los registros de la tabla
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameField">Llave en la base de datos</param>
        /// <param name="fieldValues">Listado de identificadores</param>
        /// <returns></returns>

        public List<T> consultarPorCampoIN<T>(string nameField, List<object> fieldValues) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            IDictionary<string, object> filtros = new Dictionary<string, object>();
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorIN(objEntidad, nameField, fieldValues.ToArray() , filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultarPorCampoIN<T>(string nameField, List<object> fieldValues, IDictionary<string, object> filtros) where T : class, new()
        {
            List<T> listaObj = new List<T>();
            //Obtener el script de consulta por campo/llave
            T objEntidad = new T();
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorIN(objEntidad, nameField, fieldValues.ToArray(), filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return (listaObj);
        }

        public List<T> consultarTodos<T>() where T: class, new()
        {
            var listaObj = new List<T>();

            //Obtener el script de consulta de todos los elementos
            T objEntidad = new T();
            IDictionary<string, object> filtros = new Dictionary<string, object>();           
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return (listaObj);
        }

        public bool existeSimple<T>(String nameField, object fieldValue) where T : class, new()
        {
            bool esta = false;
            String strSQL = objDBConn.fnObtenerCadenaCOUNTPorKey(typeof(T), nameField, fieldValue);
            try
            {
                int dt =(int) this.objDBConn.fnEjecutarComandoEscalar(strSQL.ToString());
                if (dt > 0) esta = true;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return esta;
        }

        public bool existeConsulta<T>(String consulta) where T : class, new()
        {
            bool esta = false;
            String strSQL = objDBConn.fnObtenerCadenaCOUNTPorConsulta(typeof(T), consulta);
            try
            {
                int dt = (int)this.objDBConn.fnEjecutarComandoEscalar(strSQL.ToString());
                if (dt > 0) esta = true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return esta;
        }

        public object getValorCampo<T>(String nombreCampo,  string idCampo, Guid idValor) where T : class, new()
        {
            String strSQL = String.Format("SELECT {0} FROM {1} WHERE {2} ='{3}'",nombreCampo,  typeof(T).Name, idCampo,idValor);
            return this.objDBConn.fnEjecutarComandoEscalar(strSQL.ToString());
       }

        public List<T> consultarTodos<T>(IDictionary<string, object> filtros) where T : class, new()
        {
            List<T> listaObj = new List<T>();

            //Obtener el script de consulta de todos los elementos
            T objEntidad = new T();
            //= new Dictionary<string, object>();
            filtros = GetFiltroGeneral<T>(filtros);
            string strSQL = this.objDBConn.fnObtenerCadenaSELECTPorKey(objEntidad, filtros);

            try
            {
                DataTable dt = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                foreach (DataRow objR in dt.Rows)
                {
                    T objNew = new T();
                    listaObj.Add((T)objDBConn.materializaEntidad(objNew, objR));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return (listaObj);
        }

        public String getNombreIdPorEntidad(Type t)
        {
            string strSQL = String.Format("select c.COLUMN_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk , INFORMATION_SCHEMA.KEY_COLUMN_USAGE c " +
                            "where pk.TABLE_NAME = '{0}'" +
                            "and pk.CONSTRAINT_TYPE = 'PRIMARY KEY' " +
                            "and c.TABLE_NAME = pk.TABLE_NAME " +
                            "and c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME"
                            ,t.Name);
            try
            {
                return (String)this.objDBConn.fnEjecutarComandoEscalar(strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<Campo> getCampos(Type t)
        {
            DataTable tabla = default(DataTable);
            List<Campo> campos = new List<Campo>();
            string strSQL = String.Format("select c.COLUMN_NAME from INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk , INFORMATION_SCHEMA.KEY_COLUMN_USAGE c " +
                           "where pk.TABLE_NAME = '{0}'" +
                           //"and pk.CONSTRAINT_TYPE = 'PRIMARY KEY' " +
                           "and c.TABLE_NAME = pk.TABLE_NAME " +
                           "and c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME"
                           , t.Name);
            try
            {
                tabla = this.objDBConn.fnObtenerDataTable(strSQL.ToString());

                if (null != tabla && 0 < tabla.Rows.Count)
                {
                    foreach (DataRow fila in tabla.Rows)
                    {
                        campos.Add(new Campo(fila["COLUMN_NAME"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return campos;
        }

        public String getNombreFechaPorEntidad(Type t)
        {
            string strSQL = String.Format("SELECT Column_Name FROM Information_schema.columns "+
                                          "WHERE Table_Name LIKE '{0}' AND Data_Type='datetime' "+
                                          "AND (Column_Name LIKE '%crea%' OR Column_Name LIKE '%registro%' OR Column_Name ='Fecha' OR Column_Name LIKE '%Captura%' " +
                                          " OR Column_Name = 'fechasolicitud')"
                                          , t.Name);
            try
            {
                return (String)this.objDBConn.fnEjecutarComandoEscalar(strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Guid getUltimoIdPorEntidad(Type t,String nombreId, String fecha)
        {
            string strSQL = String.Format("SELECT TOP(1){0} FROM {1} ORDER BY {2} DESC"
                                          , nombreId
                                          , t.Name
                                          , fecha  );
            try
            {
                return (Guid)this.objDBConn.fnEjecutarComandoEscalar(strSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<string> obtenerCamposDAL(string sObjEntidad)
        {
            List<string> lstResult = new List<string>();
            try
            {
                //Obtener el script de inserción, y agregarlo al list completo de la transacción
                lstResult = this.objDBConn.ObtenerCamposDBConn(sObjEntidad);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return lstResult;
        }


        #endregion

        #region actualizar
        public void actualizar<T>(T objEntidad, string nameField) where T: class
        {
            try
            {
                //Obtener el script de actualización            
                string strSQL = this.objDBConn.fnObtenerCadenaUPDATEPorKey(objEntidad, nameField);
                
                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void actualizaPorTipo<T>(T objEntidad, string nameField) where T : class,new()
        {
            try
            {
                //Obtener el script de actualización            
                string strSQL = this.objDBConn.fnObtenerCadenaUPDATEPorKey(new T().GetType(),objEntidad, nameField);

                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void actualizar<T>(List<T> listaEntidades, string nameField) where T: class
        {            
            try
            {
                if (listaEntidades.Count == 0) return;

                //Obtener el nombre de la entidad
                string tabla = listaEntidades[0].GetType().Name;
                //Obtener el listado de nombres de propiedades/atributos
                string[] arrCampos = this.objDBConn.getNombresAtributos(listaEntidades[0]);
                
                foreach (T objEntidad in listaEntidades)
                {
                    //Obtener listado de valores
                    object[] arrValores = this.objDBConn.getValoresAtributos(objEntidad, arrCampos);
                    //Crear la condición de actualización
                    object valorCampoFiltro = objEntidad.GetType().GetProperty(nameField).GetValue(objEntidad, null);
                    string condicion = string.Format(" {0} {1} {2} ", nameField, this.objDBConn.operador(valorCampoFiltro), this.objDBConn.aplicaFormato(valorCampoFiltro));
                    //Obtener el script de actualización
                    string strSQL = this.objDBConn.fnObtenerCadenaUPDATE(tabla, arrCampos, arrValores, condicion);                                        
                    
                    this.objDBConn.fnEjecutarComando(strSQL);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Actualiza una lista de objetos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listaEntidades">The lista entidades.</param>
        /// <param name="filtros">Es la condicion WHERE que se generara</param>
        public void actualizar<T>(List<T> listaEntidades, List<string> filtros) where T : class
        {
            try
            {
                if (listaEntidades.Count == 0) return;

                //Obtener el nombre de la entidad
                string tabla = listaEntidades[0].GetType().Name;
                
                //Obtener el listado de nombres de propiedades/atributos
                string[] arrCampos = this.objDBConn.getNombresAtributos(listaEntidades[0]);

                string condicion = string.Empty;
                string strSQLMultiple = string.Empty;
                foreach (T objEntidad in listaEntidades)
                {
                    //Obtener listado de valores
                    object[] arrValores = this.objDBConn.getValoresAtributos(objEntidad, arrCampos);
                    condicion = string.Empty; 
                    //Crear la condición de actualización

                    object valorCampoFiltro = default(object);
                    bool multiple = false;
                    foreach (string filtro in filtros)
                    {
                        if (true == multiple)
                            condicion += " AND ";
                        valorCampoFiltro = objEntidad.GetType().GetProperty(filtro).GetValue(objEntidad, null);
                        condicion += string.Format(" {0} {1} {2} \n", filtro, this.objDBConn.operador(valorCampoFiltro), this.objDBConn.aplicaFormato(valorCampoFiltro));
                        multiple = true;
                    }
                    strSQLMultiple += this.objDBConn.fnObtenerCadenaUPDATE(tabla, arrCampos, arrValores, condicion);
                }
                this.objDBConn.fnEjecutarComando(strSQLMultiple);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }        
        #endregion

        #region eliminar
        public void eliminarPorTipo<T>(T objEntidad, string nameField) where T : class
        {
            try
            {
                //Obtener el script de eliminación
                string strSQL = this.objDBConn.fnObtenerCadenaDELETEPorTipo(typeof(T),objEntidad, nameField);

                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void eliminar<T>(string nameField,object objValue ) where T : class
        {
            try
            {
                //Obtener el script de eliminación
                string strSQL = this.objDBConn.fnObtenerCadenaDELETEPorTipo(typeof(T), nameField,objValue);

                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void eliminar<T>(T objEntidad, string nameField) where T : class
        {
            try
            {
                //Obtener el script de eliminación
                string strSQL = this.objDBConn.fnObtenerCadenaDELETEPorKey(objEntidad, nameField);

                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void eliminarPorCampo<T>(string campo, object valor) where T : class
        {
            List<string> campos = new List<string>();
            campos.Add(string.Format("{0} = {1}",campo ,this.objDBConn.fnParametrosSQL(valor)));
                
            this.eliminar<T>(campos);
        }

        public void eliminar<T>(List<string> filtro) where T : class
        {
            try
            {
                //Obtener el script de eliminación
                string strSQL = this.objDBConn.fnObtenerCadenaDELETEPorTipo(typeof(T), filtro);

                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void eliminarPorRelacion<TRelacion, TPadre>(TPadre padre, String llave) where TRelacion : class
                                                                                       where TPadre : class
        {
            try
            {
                Type tipoRelacion = typeof(TRelacion);
                string strSQL = this.objDBConn.fnObtenerCadenaDELETEPorTipo(tipoRelacion, padre, llave);
                this.objDBConn.fnEjecutarComando(strSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void eliminar<T>(List<T> listaEntidades, string nameField) where T: class
        {
            try
            {
                foreach (T objEntidad in listaEntidades)
                {
                    //Obtener el script de eliminación, y agregarlo al list completo de la transacción
                    string strSQL = this.objDBConn.fnObtenerCadenaDELETEPorKey(objEntidad, nameField);

                    this.objDBConn.fnEjecutarComando(strSQL);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion                
    
        #region IDisposable Members

        public void Dispose()
        {
            this.objDBConn.Dispose();
        }

        #endregion

        #region "Clases internas"
        public partial class Campo
        {
            private string nombre;

            public string Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }

            public Campo(string nombreCampo)
            {
                this.Nombre = nombreCampo;
            }
        }
        #endregion

        #region "Enumeraciones"
        public enum EnumOrdernamiento : int
        {
            SinOrdenar = 0,
            OrdenarASC = 1,
            OrdenarDESC = 2
        }
        #endregion 
    }
}
