using System;
using System.Data;
using System.Data.SqlTypes;

namespace DataLayout.DAL.Utilerias
{
    public class DTUtil
    {




        public static bool isValidSqlDate(DateTime? date)
        {
            if (date == null)
            {
                return false;
            }

            return ((date >= (DateTime)SqlDateTime.MinValue) && (date <= (DateTime)SqlDateTime.MaxValue));
        }

        public static int parseInt(DataRow dr, string id)
        {

            return dr[id].ToString() == String.Empty ? 0 : Convert.ToInt32(dr[id].ToString());

        }

        public static string parseString(DataRow dr, string id)
        {
            return dr[id] != DBNull.Value ? dr[id].ToString() : string.Empty;
        }

        public static decimal parseDouble(DataRow dr, string id)
        {
            return Convert.ToDecimal(dr[id].ToString());

        }

        public static bool parseBool(DataRow dr, string id)
        {
            return Convert.ToBoolean(dr[id].ToString());
        }

        public static bool? parseBoolNullable(DataRow dr, string id)
        {
            Object obj = dr[id];
            if (obj.ToString() != null && obj.ToString() != String.Empty)
            {
                return Convert.ToBoolean(obj.ToString());
            }
            else
            {
                return null;
            }
        }

        public static byte[] parseByteArray(DataRow dr, string id)
        {
            Byte[] valor = null;
            if (dr[id] == System.DBNull.Value)
            {
                return valor;
            }
            else
            {
                return (byte[])dr[id];
            }

        }

        public static DateTime parseDate(DataRow dr, string id)
        {
            return Convert.ToDateTime(dr[id].ToString());

        }

        public static DateTime? parseDateNullable(DataRow dr, string id)
        {
            if (dr[id] == DBNull.Value)
            {
                return null;
            }
            return Convert.ToDateTime(dr[id].ToString());
        }

        public static decimal parseDecimal(DataRow dr, string id)
        {
            return dr[id].ToString() == String.Empty ? 0 : decimal.Parse((dr[id].ToString()));
        }



        public static Guid parseGuid(DataRow dr, string id)
        {
            if (dr[id] == DBNull.Value)
            {
                return Guid.Empty;
            }

            return new Guid(dr[id].ToString());
        }

        public static Guid? parseGuidNullable(DataRow dr, string id)
        {
            if (dr[id] == DBNull.Value)
            {
                return null;
            }
            return new Guid(dr[id].ToString());
        }


        //todo:mover este metodo a utils
        public static string ByteArrayToStr(byte[] byteArray)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetString(byteArray);
        }



        #region Funciones de Formato de Datos
        public static string formatoString(object dato)
        {
            string strValor = string.Empty;

            if (!Convert.IsDBNull(dato))
                strValor = Convert.ToString(dato);

            return (strValor);
        }

        public static int formatoInt(object dato)
        {
            int valor = 0;

            if (!Convert.IsDBNull(dato))
                valor = Convert.ToInt32(dato);

            return (valor);
        }

        public static Guid formatoGuid(object dato)
        {
            Guid objGuid = new Guid();

            if (!Convert.IsDBNull(dato))
                objGuid = new Guid(Convert.ToString(dato));

            return (objGuid);
        }

        public static bool formatoBool(object dato)
        {
            bool bResp = false;

            if (!Convert.IsDBNull(dato))
                bResp = (bool)dato;

            return (bResp);
        }

        public static DateTime formatoFecha(object dato)
        {
            DateTime fechaAct = new DateTime();

            if (!Convert.IsDBNull(dato))
                fechaAct = Convert.ToDateTime(dato.ToString());

            return (fechaAct);
        }

        public static decimal formatoDecimal(object dato)
        {
            decimal objdecimal = 0;

            if (!Convert.IsDBNull(dato))
            {
                if (!decimal.TryParse(dato.ToString(), out objdecimal))
                    objdecimal = decimal.MaxValue;
            }
            return (objdecimal);
        }

        public static double formatoDouble(object dato)
        {
            double objdouble = 0;

            if (!Convert.IsDBNull(dato))
                objdouble = Convert.ToDouble(dato.ToString());

            return (objdouble);
        }


        #endregion

    }
}
