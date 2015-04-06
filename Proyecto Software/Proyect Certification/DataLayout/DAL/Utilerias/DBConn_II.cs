using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DataLayout.DAL.Utilerias
{
    public class DBConn_II : IDisposable
    {
        #region Constantes
        const int timeOut = 1000 * 60;

        const string nombreBDAppSettings = "Data Source=S_ALVAREZ\\DEVELOPER;Initial Catalog=DbGlobalCertification;Integrated Security=True";
        
        #endregion

        #region Atributos
        SqlConnection objConn;
        SqlTransaction objTran;
        bool doCommit = true;        
        #endregion

        #region Constructor
        public DBConn_II()
        {
            objConn = fnObtenerConexion();
            objConn.Open();
        }

        public DBConn_II(bool transac)
        {

            objConn = fnObtenerConexion();
            objConn.Open();

            if (transac)
            {                
                objTran = objConn.BeginTransaction();
            }
        }

        public DBConn_II(IDbTransaction transac)
        {
            objTran = (SqlTransaction)transac;
            objConn = objTran.Connection;
            doCommit = false;
        }
        #endregion

        public SqlTransaction Transaccion
        {
            get
            {
                return objTran;
            }
        }
        /******************************* Métodos para la ejecución de los querys en ADO .NET ******************/

        #region fnObtenerConexion
        public string getCnn()
        {
            return nombreBDAppSettings;
        }        

        private SqlConnection fnObtenerConexion()
        {
            return new SqlConnection(nombreBDAppSettings);
        }
        #endregion

        #region fnEjecutarComando
        public int fnEjecutarComando(string consulta)
        {
            try
            {

                SqlCommand command = objConn.CreateCommand();
                command.CommandText = consulta;
                command.CommandType = CommandType.Text;
                command.Connection = objConn;
                if (objTran != null) { command.Transaction = objTran; }
                

                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region fnEjecutarComandoEscalar
        public object fnEjecutarComandoEscalar(string consulta)
        {
            try
            {
                SqlCommand command = objConn.CreateCommand();

                command.CommandText = consulta;
                command.CommandType = CommandType.Text;

                command.Connection = objConn;
                if (objTran != null) { command.Transaction = objTran; }

                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private object fnEjecutarComandoEscalar(SqlCommand command)
        {
            var conn = new SqlConnection(nombreBDAppSettings);
            conn.Open();
            try
            {
                command.Connection = conn;
                command.CommandTimeout = timeOut;
                if (objTran != null) { command.Transaction = objTran; }
                return command.ExecuteScalar();
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region fnObtenerDataTable
        //Obtener datatable con query
        public DataTable fnObtenerDataTable(string consulta)
        {
            var dt = new DataTable();

            try
            {
                SqlCommand command = objConn.CreateCommand();
                command.CommandText = consulta;
                command.CommandType = CommandType.Text;
                if (objTran != null) { command.Transaction = objTran; }

                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //Obtener datatable con procedimiento almacenado con parametros
        public DataTable fnObtenerDataTableSP(string storeProcedure, params object[] parametros)
        {
            var command = new SqlCommand
                              {
                                  CommandType = CommandType.Text,
                                  CommandText = "EXEC " + storeProcedure + " " + fnParametrosSQL(parametros)
                              };
            return fnEjecutarComandoDataTable(command);
        }

        //Obtener datatable con procedimiento almacenado sin parametros
        public DataTable fnObtenerDataTableSP(string storeProcedure)
        {
            var command = new SqlCommand(storeProcedure) {CommandType = CommandType.StoredProcedure};
            return fnEjecutarComandoDataTable(command);
        }

        //Obtener objeto con procedimiento almacenado con parametros
        public object fnEjecutaQueryEscalarSP(string storeProcedure, params object[] parametros)
        {
            var command = new SqlCommand
                              {
                                  CommandType = CommandType.Text,
                                  CommandText = "EXEC " + storeProcedure + " " + fnParametrosSQL(parametros)
                              };
            return fnEjecutarComandoEscalar(command);
        }

        private DataTable fnEjecutarComandoDataTable(SqlCommand command)
        {
            var dt = new DataTable();
            try
            {
                var conn = new SqlConnection(nombreBDAppSettings);
                command.Connection = conn;
                command.CommandTimeout = timeOut;
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception("Query : " + command.CommandText, ex.InnerException);
            }
        }

        #endregion

        /******************************* Métodos para la generación de los querys de T-SQL ********************/

        #region Reflection

        private Dictionary<string, object> getNombreValorAtributosType(Type type, object objEntidad)
        {
            var listaAtrib = new Dictionary<string, object>();

            //Obtener el nombre de la entidad
            string strNombreEntidad = type.Name;

            //Obtener el listado de los campos de la tabla, para hacer match con lso atributos
            DataTable dt = getListaCamposTablaNoIdent(strNombreEntidad);

            //Obtener el listado de propiedades de la entidad
            foreach (DataRow objR in dt.Rows)
            {
                //Obtiene la propiedad asociada al campo actual
                System.Reflection.PropertyInfo memberAct = type.GetProperty(objR[0].ToString());
                //Obtiene el nombre/valor de la propiedad asociada
                string strNombreAtributo = memberAct.Name;
                object objValorAtributo = memberAct.GetValue(objEntidad, null);
                //Agregar el par a la lista de nombre/valor de cada propiedad/atributo
                if (objValorAtributo != null)
                    listaAtrib.Add(strNombreAtributo, objValorAtributo);
            }

            return (listaAtrib);
        }


        private Dictionary<string, object> getNombreValorAtributos(object objEntidad)
        {
            var listaAtrib = new Dictionary<string, object>();

            //Obtener el tipo de dato del objeto reflexionado
            Type type = objEntidad.GetType();
            //Obtener el nombre de la entidad
            string strNombreEntidad;
            switch (type.Name)
            {
                case "ValeSalidaConsolidado":
                    strNombreEntidad = "ValeSalida";
                    break;
                default:
                    strNombreEntidad = type.Name;
                    break;
            }

            //Obtener el listado de los campos de la tabla, para hacer match con lso atributos
            DataTable dt = getListaCamposTablaNoIdent(strNombreEntidad);

            //Obtener el listado de propiedades de la entidad
            foreach (DataRow objR in dt.Rows)
            {
                //Obtiene la propiedad asociada al campo actual
                System.Reflection.PropertyInfo memberAct = type.GetProperty(objR[0].ToString());
                if (memberAct == null) continue;
                //Obtiene el nombre/valor de la propiedad asociada
                string strNombreAtributo = memberAct.Name;
                object objValorAtributo = memberAct.GetValue(objEntidad, null);
                //Agregar el par a la lista de nombre/valor de cada propiedad/atributo
                listaAtrib.Add(strNombreAtributo, objValorAtributo);
            }

            return (listaAtrib);
        }

        //Utilizada en insertar y actualizar
        public string[] getNombresAtributos(object objEntidad)
        {
            var listaAtrib = new List<string>();

            //Obtener el tipo de dato del objeto reflexionado
            Type type = objEntidad.GetType();
            //Obtener el nombre de la entidad
            string strNombreEntidad = type.Name;

            //Obtener el listado de los campos de la tabla, para hacer match con lso atributos
            DataTable dt = getListaCamposTablaNoIdent(strNombreEntidad);

            //Obtener el listado de propiedades de la entidad
            foreach (DataRow objR in dt.Rows)
            {
                listaAtrib.Add(objR[0].ToString());
            }

            return (listaAtrib.ToArray());
        }

        //Utilizada Consulta
        public Dictionary<string, object> getNombresAtributosTipo(object objEntidad)
        {
            var dict = new Dictionary<string, object>();

            //Obtener el tipo de dato del objeto reflexionado
            Type type = objEntidad.GetType();
            //Obtener el nombre de la entidad
            string strNombreEntidad = type.Name;

            //Obtener el listado de los campos de la tabla, para hacer match con lso atributos
            DataTable dt = getListaCamposTablaNoIdentTipo(strNombreEntidad);

            //Obtener el listado de propiedades de la entidad
            foreach (DataRow objR in dt.Rows)
            {
                dict.Add(objR[0].ToString(), objR[1]);
            }

            return (dict);
        }

        public object[] getValoresAtributos(object objEntidad, string[] listaNombres)
        {
            var listaAtrib = new List<object>();

            //Obtener el tipo de dato del objeto reflexionado
            Type type = objEntidad.GetType();
            //Obtener el nombre de la entidad
            string strNombreEntidad = type.Name;

            //Obtener el listado de propiedades de la entidad
            foreach (string nameAtributo in listaNombres)
            {
                //Obtiene la propiedad asociada al campo actual
                System.Reflection.PropertyInfo memberAct = type.GetProperty(nameAtributo);

                if (memberAct != null)
                {
                    //Obtiene el valor de la propiedad asociada                
                    object objValorAtributo = memberAct.GetValue(objEntidad, null);
                    //Agregar el par a la lista de valores 
                    listaAtrib.Add(objValorAtributo);
                }
                else
                {
                    int i = 0;
                }
            }

            return (listaAtrib.ToArray());
        }

        public DataTable getListaCamposTabla(string strNombreTabla)
        {
            var strSQL = new StringBuilder();

            //Crear la consulta para obtener el listado de campos por tabla
            strSQL.AppendLine("SELECT sc.name FROM syscolumns sc ");
            strSQL.AppendLine("INNER JOIN systypes st ");
            strSQL.AppendLine("ON st.xusertype = sc.xusertype ");
            strSQL.AppendLine(string.Format("WHERE Id=OBJECT_ID('{0}') ", strNombreTabla));

            return (fnObtenerDataTable(strSQL.ToString()));
        }


        private DataTable getListaCamposTablaNoIdent(string strNombreTabla)
        {
            var strSQL = new StringBuilder();
            strSQL.AppendLine(" SELECT sc.name ");
            strSQL.AppendLine(" FROM syscolumns sc  ");
            strSQL.AppendLine(" INNER JOIN systypes st ON st.xusertype = sc.xusertype ");
            strSQL.AppendFormat(" WHERE Id =OBJECT_ID('{0}')  ", strNombreTabla);
            strSQL.AppendLine(" AND sc.Name NOT IN( ");
            strSQL.AppendLine(" SELECT name  ");
            strSQL.AppendLine(" FROM sys.identity_columns  ");
            strSQL.AppendFormat(" WHERE [object_id] = OBJECT_ID('{0}') ) ", strNombreTabla);
            return (fnObtenerDataTable(strSQL.ToString()));
        }

        private DataTable getListaCamposTablaNoIdentTipo(string strNombreTabla)
        {
            var strSQL = new StringBuilder();
            strSQL.AppendLine(" SELECT sc.name, st.name ");
            strSQL.AppendLine(" FROM syscolumns sc  ");
            strSQL.AppendLine(" INNER JOIN systypes st ON st.xusertype = sc.xusertype ");
            strSQL.AppendFormat(" WHERE Id =OBJECT_ID('{0}')  ", strNombreTabla);
            strSQL.AppendLine(" AND sc.Name NOT IN( ");
            strSQL.AppendLine(" SELECT name  ");
            strSQL.AppendLine(" FROM sys.identity_columns  ");
            strSQL.AppendFormat(" WHERE [object_id] = OBJECT_ID('{0}') ) ", strNombreTabla);
            return (fnObtenerDataTable(strSQL.ToString()));
        }

        #endregion

        #region aplicaFormato
        public string aplicaFormato(object valor)
        {
            string strValor = string.Empty;

            if ((valor is string || valor is Guid) && (!valor.Equals("ISNOTNULL")))
                strValor = string.Format("'{0}'", fnValidaSQLInjection(valor.ToString()));
            else if (valor is DateTime)
                strValor = string.Format("'{0}'", ((DateTime)valor).ToString("yyyyMMdd HH:mm:ss"));
            else if (valor is bool || valor is Boolean)
                strValor = (Convert.ToBoolean(valor) ? "1" : "0");
            else if (fnIsNumber(valor))
                strValor = valor.ToString();
            else if (valor is byte[] || valor is Byte[])
                strValor = "(CONVERT(varbinary(max), '" + BitConverter.ToString((byte[])valor) + "', 1))";
            else if (valor == null)
                strValor = "null";
            else if (valor is string && valor.Equals("ISNOTNULL"))
                strValor = "null";


            return (strValor);
        }
        #endregion

        #region operador
        public string operador(object campo)
        {
            if (campo == null)
                return "is";
            if (campo is string)
                if (campo.Equals("ISNOTNULL"))
                    return ("IS NOT");
            if (campo is string)
                return ("LIKE");

            return ("=");
        }
        #endregion

        #region fnIsNumber
        private bool fnIsNumber(object obj)
        {
            return (obj != null && (
                obj is int || obj is bool || obj is decimal || obj is double || obj is long || obj is float || obj is byte || obj is short || obj is sbyte || obj is ushort || obj is uint || obj is ulong
            ));
        }
        #endregion

        #region fnParametrosSQL
        public string fnParametrosSQL(params object[] values)
        {
            StringBuilder result;

            if (values == null)
            {
                return "";
            }
            else
            {
                result = new StringBuilder();

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] is string || values[i] is Guid)
                    {
                        if (values[i].ToString().Contains("@param:"))
                        {
                            result.Append(string.Format("{0}", fnValidaSQLInjection(values[i].ToString().Replace("@param:", "@"))));
                        }
                        else
                        {
                            result.Append(string.Format("'{0}'", fnValidaSQLInjection(values[i].ToString())));
                        }
                    }
                    else if (values[i] is DateTime)
                    {
                        result.Append(string.Format("'{0}'", ((DateTime)values[i]).ToString("yyyyMMdd HH:mm:ss")));
                    }
                    else if (values[i] is bool || values[i] is Boolean)
                    {
                        result.Append((Convert.ToBoolean(values[i]) ? "1" : "0"));
                    }
                    else if (fnIsNumber(values[i]))
                    {
                        result.Append(values[i].ToString());
                    }
                    else if (values[i] is byte[] || values[i] is Byte[])
                    {

                        //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                        //string other = enc.GetString((byte[])values[i]);
                        result.Append("0x" + BitConverter.ToString((byte[])values[i]).Replace("-", ""));
                    }
                    else if (values[i] == null)
                    {
                        result.Append("null");
                    }
                    if (i != values.Length - 1) { result.Append(","); }
                }
            }
            return result.ToString();
        }

        #endregion

        #region fnValidaSQLInjection
        public static string fnValidaSQLInjection(string parametro)
        {
            string[] eliminar = new string[] { "'", "delete", "from", "update", "select", "drop"/*, "\"" */};
            foreach (string s in eliminar)
            {
                if (parametro.Contains("'"))
                {
                    parametro = Regex.Replace(parametro, s, "''", RegexOptions.IgnoreCase);
                }
                else
                {
                    parametro = Regex.Replace(parametro, s, string.Empty, RegexOptions.IgnoreCase);
                }

            }
            return parametro;
        }
        #endregion

        #region fnObtenerArregloSeparadoComa
        private string fnObtenerArregloSeparadoComa(string[] campos)
        {
            var strCampos = new StringBuilder();
            for (int i = 0; i < campos.Length; i++)
            {
                strCampos.Append(campos[i]);
                if (i != (campos.Length - 1)) strCampos.Append(",");
            }
            return strCampos.ToString();
        }
        #endregion

        #region fnObtenerCadenaActualizar
        private string fnObtenerCadenaActualizar(string tabla, string[] camposTabla, object[] valoresArr, string condicion)
        {
            var str = new StringBuilder();
            str.AppendFormat(" UPDATE {0} SET ", tabla);
            for (int i = 0; i < camposTabla.Length; i++)
            {
                str.AppendFormat("{0} = {1}", camposTabla[i], fnParametrosSQL(valoresArr[i]));
                if (i != (camposTabla.Length - 1))
                {
                    str.Append(",");
                }
            }
            str.Append(" WHERE ");
            str.Append(condicion);
            return str.ToString();
        }
        #endregion

        #region fnObtenerCadenaINSERT
        public string fnObtenerCadenaINSERT(object objEntidad)
        {
            Dictionary<string, object> listaValores = getNombreValorAtributos(objEntidad);

            //Obtiene los nombres de los atributos con su respectivo valor
            string[] arrCampos = listaValores.Keys.ToArray<string>();
            object[] arrValores = listaValores.Values.ToArray<object>();
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            //Dar formato al listado de NombreCampo/Valor
            string campos = fnObtenerArregloSeparadoComa(arrCampos);
            string valores = fnParametrosSQL(arrValores);
            //Armar el script
            string query = string.Format("INSERT INTO {0}({1}) VALUES({2})  SELECT scope_identity() ", tabla, campos, valores);

            return query;
        }


        public string fnObtenerCadenaINSERT(object objEntidad, Type type)
        {
            Dictionary<string, object> listaValores = getNombreValorAtributosType(type, objEntidad);

            //Obtiene los nombres de los atributos con su respectivo valor
            string[] arrCampos = listaValores.Keys.ToArray<string>();
            object[] arrValores = listaValores.Values.ToArray<object>();
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = type.Name;
            //Dar formato al listado de NombreCampo/Valor
            string campos = fnObtenerArregloSeparadoComa(arrCampos);
            string valores = fnParametrosSQL(arrValores);
            //Armar el script
            string query = string.Format("INSERT INTO {0}({1}) VALUES({2})  SELECT scope_identity() ", tabla, campos, valores);

            return query;
        }


        public string fnObtenerCadenaINSERT(string tabla, string[] arrCampos, object[] arrValores)
        {
            //Dar formato al listado de NombreCampo/Valor
            string campos = fnObtenerArregloSeparadoComa(arrCampos);
            string valores = fnParametrosSQL(arrValores);
            //Armar el script
            string query = string.Format("INSERT INTO {0} with (UPDLOCK , ROWLOCK) ({1}) VALUES({2})  SELECT scope_identity() ", tabla, campos, valores);

            return query;
        }
        #endregion

        #region fnObtenerCadenaSELECT

        public string fnObtenerCadenaSELECT(object objEntidad)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            //Armar el script
            string query = string.Format("SELECT * FROM {0} ", tabla);

            return query;
        }

        public string fnObtenerCadenaSELECTTOP(string tabla, string campoOrdenar, int top, IDictionary<string, object> filtros, string orden)
        {
            //Armar el script
            string query = string.Empty;
            string condicion = string.Empty;
            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            query = string.Format("SELECT TOP {0} * FROM {1} WHERE 1=1 {2} {3}", top, tabla, condicion, orden);

            return query;
        }

        public string fnObtenerCadenaSELECTPorKey(object objEntidad, string nameField, object valueField)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            //Obtiene el nombre del campo de selección
            string campo = nameField;
            //Obtiene el valor del campo de selección
            object valor = valueField;
            //Armar el script
            string query = string.Format("SELECT * FROM {0} WHERE {1} {2} {3} ", tabla, campo, operador(valor), aplicaFormato(valor));

            return query;
        }

        public string fnObtenerCadenaSELECTPorKey(object objEntidad, IDictionary<string, object> filtros)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = "";

            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            //Armar el script
            string query = string.Format("SELECT * FROM {0} WHERE 1=1 {1} ", tabla, condicion);


            return query;
        }

        public string fnObtenerCadenaSELECTPorKey2(object objEntidad, IDictionary<string, object> filtros)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = "";

            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                if (filtro.Value.ToString().Contains(" IN "))
                    condicion += string.Format(" AND {0} {1} {2}", filtro.Key, "IN", filtro.Value.ToString().Replace("IN", ""));
                else
                    condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            //Armar el script
            string query = string.Format("SELECT * FROM {0} WHERE 1=1 {1} ", tabla, condicion);


            return query;
        }

        public string fnObtenerCadenaSELECTPorKey(object objEntidad, string strOrdenamiento, IDictionary<string, object> filtros)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = "";

            foreach (KeyValuePair<string, object> filtro in filtros)
            {


                condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            //Armar el script
            string query = string.Format("SELECT * FROM {0} WHERE 1=1 {1} ", tabla, condicion);

            if (!string.IsNullOrEmpty(strOrdenamiento))
                query += string.Format("ORDER BY {0} ", strOrdenamiento);

            return query;
        }

        public string fnObtenerCadenaSELECTPorKey(object objEntidad, string strOrdenamiento, List<string> filtros, IDictionary<string, object> filtrosD)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = "";

            foreach (string f in filtros)
            {
                condicion += string.Format(" AND {0} ", f);
            }
            foreach (KeyValuePair<string, object> f in filtrosD)
            {
                condicion += string.Format(" AND {0} {1} {2}", f.Key, operador(f.Value), aplicaFormato(f.Value));
            }

            //Armar el script
            string query = string.Format("SELECT * FROM {0} WHERE 1=1 {1} ", tabla, condicion);

            if (!string.IsNullOrEmpty(strOrdenamiento))
                query += string.Format("ORDER BY {0} ", strOrdenamiento);

            return query;
        }

        public string fnObtenerCadenaSELECTPaginado(int pag, int cantReg, string Ordenamiento, string consulta)
        {
            //Obtiene el nombre de la entidad (Tabla)
            // string tabla = objEntidad.GetType().Name;           

            //Armar el script
            StringBuilder query = new StringBuilder();
            int pIni = (pag - 1) * cantReg + 1;
            int pFin = pag * cantReg;

            query.Append(String.Format("SELECT * FROM (select ROW_NUMBER() OVER(ORDER BY {0}) AS 'RN',T.* from ({1}) as T) as T2 ", Ordenamiento, consulta));
            query.Append(String.Format("WHERE 1=1 AND RN>= {0} AND RN<= {1} ", pIni, pFin));

            return query.ToString();
        }

        public string fnObtenerCadenaSELECTPaginado(int pag, int cantReg, string Ordenamiento, string consulta, IDictionary<string, object> filtros)
        {
            //Obtiene el nombre de la entidad (Tabla)
            // string tabla = objEntidad.GetType().Name;           
            string condicion = string.Empty;
            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            //Armar el script
            StringBuilder query = new StringBuilder();
            int pIni = (pag - 1) * cantReg + 1;
            int pFin = pag * cantReg;

            query.Append(String.Format("SELECT * FROM (select ROW_NUMBER() OVER(ORDER BY {0}) AS 'RN',T.* FROM ({1} WHERE 1= 1 {2}) as T) as T2 ", Ordenamiento, consulta, condicion));
            query.Append(String.Format("WHERE 1=1 AND RN>= {0} AND RN<= {1} ", pIni, pFin));

            return query.ToString();
        }

        public string fnObtenerCadenaSELECTPaginado(int pag, int cantReg, string Ordenamiento, object objEntidad, IDictionary<string, object> filtros)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = "";

            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            //Armar el script
            StringBuilder query = new StringBuilder();
            int pIni = (pag - 1) * cantReg + 1;
            int pFin = pag * cantReg;

            query.Append("SELECT * FROM (");
            query.Append(String.Format("SELECT ROW_NUMBER() OVER(ORDER BY {0} ) AS 'RN',x.* FROM {1} x WHERE 1=1 {2}  ", Ordenamiento, tabla, condicion));
            query.Append(") as T ");
            query.Append(String.Format("WHERE 1=1 AND RN>= {0} AND RN<= {1} ", pIni, pFin));

            return query.ToString();
        }

        public string fnObtenerCadenaSELECTPaginado(int pag, int cantReg, string Ordenamiento, object objEntidad, List<string> filtros, IDictionary<string, object> filtrosD)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = string.Empty;

            foreach (string f in filtros)
            {
                if (f.Contains("OR"))
                {
                    condicion += string.Format(" {0} ", f);
                }
                else
                {
                    condicion += string.Format(" AND {0} ", f);
                }
            }
            foreach (KeyValuePair<string, object> f in filtrosD)
            {
                condicion += string.Format(" AND {0} {1} {2}", f.Key, operador(f.Value), aplicaFormato(f.Value));
            }

            //Armar el script
            StringBuilder query = new StringBuilder();
            int pIni = (pag - 1) * cantReg + 1;
            int pFin = pag * cantReg;

            query.Append("SELECT * FROM (");
            query.Append(String.Format("SELECT ROW_NUMBER() OVER(ORDER BY {0} ) AS 'RN',x.* FROM {1} x WHERE 1=1 {2}  ", Ordenamiento, tabla, condicion));
            query.Append(") as T ");
            query.Append(String.Format("WHERE 1=1 AND RN>= {0} AND RN<= {1} ", pIni, pFin));

            return query.ToString();
        }

        public string fnObtenerCadenaCOUNTPaginado(string Ordenamiento, object objEntidad, List<string> filtros)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = "";

            foreach (string filtro in filtros)
            {
                condicion += string.Format(" AND {0} ", filtro);
            }

            //Armar el script
            StringBuilder query = new StringBuilder();
            query.Append(String.Format("SELECT count(*) FROM {0} x WHERE 1=1 {1}  ", tabla, condicion));

            return query.ToString();
        }

        public string fnObtenerCadenaCOUNTPaginado(string Ordenamiento, object objEntidad, IDictionary<string, object> filtros)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = "";

            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            //Armar el script
            StringBuilder query = new StringBuilder();
            query.Append(String.Format("SELECT COUNT(*) FROM {0} x WHERE 1=1 {1}  ", tabla, condicion));

            return query.ToString();
        }

        public string fnObtenerCadenaCOUNTPaginado(string consulta, IDictionary<string, object> filtros)
        {

            string condicion = string.Empty;
            foreach (KeyValuePair<string, object> filtro in filtros)
            {
                condicion += string.Format(" AND {0} {1} {2}", filtro.Key, operador(filtro.Value), aplicaFormato(filtro.Value));
            }

            //Armar el script
            StringBuilder query = new StringBuilder();
            //consulta = consulta.Substring(consulta.IndexOf("FROM"));
            query.Append(String.Format(" SELECT COUNT(*)  FROM (  {0}  WHERE 1=1 {1} )tx  ", consulta, condicion));

            return query.ToString();
        }

        public string fnObtenerCadenaCOUNTPaginado(string Ordenamiento, string consulta)
        {
            //Obtiene el nombre de la entidad (Tabla)
            // string tabla = objEntidad.GetType().Name;           

            //Armar el script
            StringBuilder query = new StringBuilder();

            query.Append(String.Format("SELECT COUNT(*) FROM (select ROW_NUMBER() OVER(ORDER BY {0}) AS 'RN',T.* from ({1}) as T) as T2 ", Ordenamiento, consulta));

            return query.ToString();
        }

        public string fnObtenerCadenaCOUNTPorKey(Type entidad, string nameField, object valueField)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = entidad.Name;
            //Obtiene el nombre del campo de selección
            string campo = nameField;
            //Obtiene el valor del campo de selección
            object valor = valueField;
            //Armar el script
            string query = string.Format("SELECT COUNT(*) FROM {0} WHERE {1} {2} {3} ", tabla, campo, operador(valor), aplicaFormato(valor));

            return query;
        }

        public string fnObtenerCadenaCOUNTPorConsulta(Type entidad, string consulta)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = entidad.Name;
            string query = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", tabla, consulta);

            return query;
        }

        public string fnObtenerCadenaSELECTPorIN(object objEntidad, string nameField, object[] fieldValues, IDictionary<string, object> filtrosD)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            string condicion = string.Empty;
            string condicionValues = string.Empty;

            foreach (object value in fieldValues)
            {
                if (!string.IsNullOrEmpty(condicionValues))
                    condicionValues += ",";
                condicionValues += string.Format(" {0} ", aplicaFormato(value));
            }


            if (!string.IsNullOrEmpty(condicionValues))
                condicion += string.Format(" AND {0} IN ({1}) ", nameField, condicionValues);

            foreach (KeyValuePair<string, object> f in filtrosD)
            {
                condicion += string.Format(" AND {0} {1} {2}", f.Key, operador(f.Value), aplicaFormato(f.Value));
            }

            //Armar el script
            string query = string.Format("SELECT * FROM {0} WHERE 1=1 {1} ", tabla, condicion);

            return query;
        }

        #endregion

        #region fnObtenerCadenaUPDATE

        public string fnObtenerCadenaUPDATEPorKey(Type type, object objEntidad, string nameField)
        {
            Dictionary<string, object> listaValores = getNombreValorAtributosType(type, objEntidad);

            //Obtiene los nombres de los atributos con su respectivo valor
            string[] arrCampos = listaValores.Keys.ToArray<string>();
            object[] arrValores = listaValores.Values.ToArray<object>();


            string tabla = type.Name;
            //Obtiene el nombre del campo de selección
            string nombreCampoFiltro = nameField;
            //Obtiene el valor del campo de selección
            object valorCampoFiltro = objEntidad.GetType().GetProperty(nombreCampoFiltro).GetValue(objEntidad, null);
            //Crea la condición de actualización que se utilizará en el WHERE
            string condicion = string.Format(" {0} {1} {2} ", nombreCampoFiltro, operador(valorCampoFiltro), aplicaFormato(valorCampoFiltro));
            //Armar el script
            string query = fnObtenerCadenaActualizar(tabla, arrCampos, arrValores, condicion);

            return query;
        }

        public string fnObtenerCadenaUPDATEPorKey(object objEntidad, string nameField)
        {
            Dictionary<string, object> listaValores = getNombreValorAtributos(objEntidad);

            //Obtiene los nombres de los atributos con su respectivo valor
            string[] arrCampos = listaValores.Keys.ToArray<string>();
            object[] arrValores = listaValores.Values.ToArray<object>();
            //Obtiene el nombre de la entidad (Tabla)

            string tabla = string.Empty;
            switch (objEntidad.GetType().Name)
            {
                case "ValeSalidaConsolidado":
                    tabla = "ValeSalida";
                    break;
                default:
                    tabla = objEntidad.GetType().Name;
                    break;
            }

            //Obtiene el nombre del campo de selección
            string nombreCampoFiltro = nameField;
            //Obtiene el valor del campo de selección
            object valorCampoFiltro = objEntidad.GetType().GetProperty(nombreCampoFiltro).GetValue(objEntidad, null);
            //Crea la condición de actualización que se utilizará en el WHERE
            string condicion = string.Format(" {0} {1} {2} ", nombreCampoFiltro, operador(valorCampoFiltro), aplicaFormato(valorCampoFiltro));
            //Armar el script
            string query = fnObtenerCadenaActualizar(tabla, arrCampos, arrValores, condicion);

            return query;
        }

        public string fnObtenerCadenaUPDATE(string tabla, string[] arrCampos, object[] arrValores, string condicion)
        {
            //Armar el script
            string query = fnObtenerCadenaActualizar(tabla, arrCampos, arrValores, condicion);

            return query;
        }

        public List<string> ObtenerCamposDBConn(string sObjEntidad)
        {
            List<string> lstRet = new List<string>();

            DataTable dt = getListaCamposTabla(sObjEntidad);

            //Obtener el listado de propiedades de la entidad
            foreach (DataRow objR in dt.Rows)
            {
                lstRet.Add(string.Format("{0}.{1}", sObjEntidad, objR[0].ToString()));
            }

            return lstRet;
        }
        #endregion

        #region fnObtenerCadenaDELETE
        public string fnObtenerCadenaDELETEPorTipo(Type type, object objEntidad, string nameField)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = type.Name;
            //Obtiene el nombre del campo de selección
            string nombreCampoFiltro = nameField;
            //Obtiene el valor del campo de selección
            object valorCampoFiltro = objEntidad.GetType().GetProperty(nombreCampoFiltro).GetValue(objEntidad, null);
            //Crea la condición de actualización que se utilizará en el WHERE
            string condicion = string.Format(" {0} {1} {2} ", nombreCampoFiltro, operador(valorCampoFiltro), aplicaFormato(valorCampoFiltro));
            //Armar el script
            string query = string.Format("DELETE FROM {0} WHERE {1} ", tabla, condicion);

            return query;
        }



        public string fnObtenerCadenaDELETEPorKey(object objEntidad, string nameField)
        {
            //Obtiene el nombre de la entidad (Tabla)
            string tabla = objEntidad.GetType().Name;
            //Obtiene el nombre del campo de selección
            string nombreCampoFiltro = nameField;
            //Obtiene el valor del campo de selección
            object valorCampoFiltro = objEntidad.GetType().GetProperty(nombreCampoFiltro).GetValue(objEntidad, null);
            //Crea la condición de actualización que se utilizará en el WHERE
            string condicion = string.Format(" {0} {1} {2} ", nombreCampoFiltro, operador(valorCampoFiltro), aplicaFormato(valorCampoFiltro));
            //Armar el script
            string query = string.Format("DELETE FROM {0} WHERE {1} ", tabla, condicion);

            return query;
        }


        public string fnObtenerCadenaDELETEPorTipo(Type type, string nameField, object valueField)
        {
            //Armar el script
            string tabla = type.Name;

            string query = string.Format("DELETE FROM {0} ", tabla);


            query += string.Format(" WHERE {0}  = {1} ", nameField, aplicaFormato(valueField));

            return query;
        }

        public string fnObtenerCadenaDELETEPorTipo(Type type, List<string> filtro)
        {
            //Armar el script
            string tabla = type.Name;

            string query = string.Format("DELETE FROM {0} ", tabla);
            query += string.Format(" WHERE 1=1 ");

            foreach (string f in filtro)
            {
                query += string.Format(" AND {0} ", f);
            }

            return query;
        }



        public string fnObtenerCadenaDELETE(string tabla, string condicion)
        {
            //Armar el script
            string query = string.Format("DELETE FROM {0} ", tabla);

            if (condicion.Length > 0)
                query += string.Format(" WHERE {0} ", condicion);

            return query;
        }
        #endregion

        #region materializaEntidad

        public object materializaEntidad(object objEntidad, DataRow objR)
        {
            return materializaEntidad(objEntidad, objR, false);
        }

        public object materializaEntidad(object objEntidad, DataRow objR, bool incluirPropiedadesPorHerencia)
        {
            System.Reflection.PropertyInfo objProp = null;
            object valor = null;

            try
            {
                Type type = objEntidad.GetType();
                foreach (DataColumn dc in objR.Table.Columns)
                {
                    if (incluirPropiedadesPorHerencia)
                    {
                        objProp = type.GetProperty(dc.ColumnName);
                    }
                    else
                    {
                        objProp = type.GetProperty(dc.ColumnName);
                    }

                    if (objProp != null)
                    {
                        valor = parseObject(objR[dc.ColumnName], objProp.PropertyType.FullName);
                        objProp.SetValue(objEntidad, valor, null);
                    }
                }
            }
            catch (Exception ex)
            {
                string strMensaje = string.Format("Entidad: {0}, Propiedad: {1}, Valor: {2} \n", objEntidad.GetType().Name, objProp.Name, valor);

                throw new Exception(strMensaje + ex.Message, ex);
            }

            return (objEntidad);
        }

        public List<T> materializaEntidad<T>(DataTable dt) where T : class, new()
        {
            System.Reflection.PropertyInfo objProp = null;
            List<T> listaItems = new List<T>();
            object valor = null;

            try
            {
                foreach (DataRow objR in dt.Rows)
                {
                    T objEntidad = new T();
                    Type type = objEntidad.GetType();

                    foreach (DataColumn dc in objR.Table.Columns)
                    {
                        objProp = type.GetProperty(dc.ColumnName);

                        valor = parseObject(objR[dc.ColumnName], objProp.PropertyType.FullName);
                        objProp.SetValue(objEntidad, valor, null);
                    }
                    //Agregar la entidad
                    listaItems.Add(objEntidad);
                }
            }
            catch (Exception ex)
            {
                T objEntidad = new T();
                string strMensaje = string.Format("Entidad: {0}, Propiedad: {1}, Valor: {2} \n", objEntidad.GetType().Name, objProp.Name, valor);

                throw new Exception(strMensaje + ex.Message, ex);
            }

            return (listaItems);
        }

        private object parseObject(object valor, string strName)
        {
            if (valor == Convert.DBNull)
                valor = null;
            else
            {
                if (strName.ToUpper().Contains("GUID"))
                    valor = DTUtil.formatoGuid(valor);
                else if (strName.ToUpper().Contains("INT32"))
                    valor = DTUtil.formatoInt(valor);
                else if (strName.ToUpper().Contains("DOUBLE"))
                    valor = DTUtil.formatoDouble(valor);
                else if (strName.ToUpper().Contains("STRING"))
                    valor = DTUtil.formatoString(valor);
                else if (strName.ToUpper().Contains("BOOL"))
                    valor = DTUtil.formatoBool(valor);
                if (strName.ToUpper().Contains("DATETIME"))
                    valor = DTUtil.formatoFecha(valor);
                else if (strName.ToUpper().Contains("DECIMAL"))
                    valor = DTUtil.formatoDecimal(valor);

            }
            return (valor);
        }
        #endregion

        #region "Metodos Nuevos"
        public List<T> GetEventsOfQuery<T>(string sQuery) where T : class, new()
        {
            return ConvertTable<T>(fnObtenerDataTable(sQuery));
        }

        private List<T> ConvertTable<T>(DataTable datos) where T : class, new()
        {
            var lstObjets = new List<T>();

            if (datos != null)
            {
                foreach (DataRow row in datos.Rows)
                {
                    var obj = new T();
                    lstObjets.Add((T)materializaEntidad(obj, row));
                }
            }
            return lstObjets;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.objConn != null && this.objConn.State == ConnectionState.Open)
            {

                if (this.objTran != null && doCommit == true)
                {
                    objTran.Commit();
                }
                else if (this.objTran != null)
                {
                    objTran.Rollback();
                }

                this.objConn.Close();
            }
        }

        #endregion
    }
}
