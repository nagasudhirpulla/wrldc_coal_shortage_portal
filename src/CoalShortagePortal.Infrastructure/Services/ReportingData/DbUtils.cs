using System;
using Oracle.ManagedDataAccess.Client;

namespace CoalShortagePortal.Infrastructure.Services.ReportingData
{
    internal class DbUtils
    {
        public static string SafeGetString(OracleDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        // get values by column name instead of position
        // https://stackoverflow.com/questions/28325813/sqldatareader-get-value-by-column-name-not-ordinal-number/42182943
        public static string SafeGetString(OracleDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetString(reader, colIndex);
        }

        public static int SafeGetInt(OracleDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return -1;
        }

        public static int SafeGetInt(OracleDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetInt(reader, colIndex);
        }

        public static double SafeGetDouble(OracleDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDouble(colIndex);
            return -1;
        }

        public static double SafeGetDouble(OracleDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetDouble(reader, colIndex);
        }

        public static DateTime? SafeGetDt(OracleDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            return null;
        }

        public static DateTime? SafeGetDt(OracleDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetDt(reader, colIndex);
        }
    }
}
