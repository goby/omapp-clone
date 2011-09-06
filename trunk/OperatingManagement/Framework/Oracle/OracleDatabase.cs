using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace OperatingManagement.Framework
{
    /// <summary>
    /// Provides implementation of Oracle database.
    /// </summary>
    public class OracleDatabase
    {
        private string _connectionString;

        private OracleDatabase()
        { }
        /// <summary>
        /// Initializes a new instance of <see cref="OperatingManagement.Framework.OracleDatabase"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string of Oracle database.</param>
        /// <returns></returns>
        public static OracleDatabase FromConnectionString(string connectionString)
        {
            OracleDatabase database = new OracleDatabase();
            database._connectionString = connectionString;
            return database;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OperatingManagement.Framework.OracleDatabase"/> class.
        /// </summary>
        /// <param name="nodeName">The node name of 'connectionStrings' in Web.Config file.</param>
        /// <returns></returns>
        public static OracleDatabase FromConfiguringNode(string nodeName)
        {
            OracleDatabase database = new OracleDatabase();
            database._connectionString = ConfigurationManager.ConnectionStrings[nodeName].ConnectionString;
            if (string.IsNullOrEmpty(database._connectionString))
            {
                throw new ConfigurationErrorsException("cant found configured node. node name: " + nodeName);
            }

            return database;
        }
        /// <summary>
        /// Create and prepare an OracleCommand, and call ExecuteReader with the appropriate procedure.
        /// </summary>
        /// <remarks>
        /// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        /// 
        /// If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="spName">the stored procedure name.</param>
        /// <param name="parameters">an array of OracleParameter to be associated with the command or 'null' if no parameters are required</param>
        /// <returns></returns>
        public OracleDataReader SpExecuteReader(string spName, params OracleParameter[] parameters)
        {
            OracleConnection connection = new OracleConnection(_connectionString);

            try
            {
                OracleCommand command = SpPrepareCommand(connection, spName, parameters);

                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch(Exception ex)
            {
                connection.Close();
                throw ex;
            }
        }
        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataTable dt = SpExecuteTable(spName, "GetOrders");
        /// </remarks>
        /// <param name="spName">the stored procedure name.</param>
        /// <param name="parameters">an array of OracleParameter to be associated with the command or 'null' if no parameters are required</param>
        /// <returns></returns>
        public DataTable SpExecuteTable(string spName, params OracleParameter[] parameters)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                OracleCommand command = SpPrepareCommand(connection, spName, parameters);

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                return table;
            }
        }

        /// <summary>
        /// Execute an OracleCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = SpExecuteDataSet(spName, "GetOrders");
        /// </remarks>
        /// <param name="spName">the stored procedure name.</param>
        /// <param name="parameters">an array of OracleParameter to be associated with the command or 'null' if no parameters are required</param>
        /// <returns></returns>
        public DataSet SpExecuteDataSet(string spName, params OracleParameter[] parameters)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                OracleCommand command = SpPrepareCommand(connection, spName, parameters);

                OracleDataAdapter adapter = new OracleDataAdapter(command);
                DataSet set = new DataSet();
                adapter.Fill(set);

                return set;
            }
        }
        /// <summary>
        /// Execute an OracleCommand (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = SpExecuteNonQuery(spName, "PublishOrders");
        /// </remarks>
        /// <param name="spName">the stored procedure name.</param>
        /// <param name="parameters">an array of OracleParameter to be associated with the command or 'null' if no parameters are required</param>
        /// <returns></returns>
        public int SpExecuteNonQuery(string spName, params OracleParameter[] parameters)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                OracleCommand command = SpPrepareCommand(connection, spName, parameters);

                return command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Execute an OracleCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = SpExecuteScalar<![CDATA[<int>]]>(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <typeparam name="T">The type of return value.</typeparam>
        /// <param name="spName">the stored procedure name.</param>
        /// <param name="parameters">an array of OracleParameter to be associated with the command or 'null' if no parameters are required</param>
        /// <returns></returns>
        public T SpExecuteScalar<T>(string spName, params OracleParameter[] parameters)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                OracleCommand command = SpPrepareCommand(connection, spName, parameters);

                return (T)command.ExecuteScalar();
            }
            
        }

        private OracleCommand SpPrepareCommand(OracleConnection connection, string spName, OracleParameter[] parameters)
        {
            if(connection.State!= ConnectionState.Open)
                connection.Open();
            OracleCommand command = new OracleCommand(spName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }
    }
}
