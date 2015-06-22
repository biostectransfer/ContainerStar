using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace MetadataLoader.MSSQL.Utils
{
    public sealed class ScriptEngine
    {
        #region Static
        public static SqlCommand GetCommand(string script, SqlConnection conn)
        {
            return new SqlCommand(script, conn) {CommandTimeout = ExecutionTimeout};
        }

        private static int ExecutionTimeout
        {
            get { return 5000; }
        }

        private static SqlCommand GetCommandWithParameters(string name,
                SqlConnection connection,
                CommandType type,
                params SqlParameter[] parameters)
        {
            var cmd = GetCommand(name, connection);
            cmd.CommandType = type;
            foreach (var parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            return cmd;
        }
        #endregion
        #region Private fields
        private readonly SqlConnectionStringBuilder _connBuilder;
        private SqlConnection _sqlConn;
        #endregion
        #region Constructors
        /// <summary>
        ///     Instance constructor
        /// </summary>
        /// <param name="connection"> </param>
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        internal ScriptEngine(SqlConnection connection)
                : this(connection.ConnectionString)
        {
            #region Check
            if (ReferenceEquals(connection, null))
            {
                throw new ArgumentNullException("connection");
            }
            #endregion
            _sqlConn = connection;
        }
        /// <summary>
        ///     Instance constructor
        /// </summary>
        /// <param name="connString"> </param>
        /// <exception cref="ArgumentNullException">
        ///     <c>conn</c>
        ///     is null.
        /// </exception>
        public ScriptEngine(string connString)
        {
            #region Check
            if (string.IsNullOrEmpty(connString))
            {
                throw new ArgumentNullException("connString");
            }
            #endregion
            _connBuilder = new SqlConnectionStringBuilder(connString);
        }
        #endregion
        #region Public properties
        public bool IsConnectionOpen
        {
            [DebuggerStepThrough]
            get
            {
                return !ReferenceEquals(Connection, null) && Connection.State == ConnectionState.Open;
            }
        }

        public void ResetSchema()
        {
            ChangeSchema(null);
        }

        public void ChangeSchema(string schema)
        {
            _connBuilder.InitialCatalog = schema;
            if (IsConnectionOpen)
            {
                try
                {
                    _sqlConn.ChangeDatabase(string.IsNullOrEmpty(schema) ? "master" : schema);
                    return;
                }
                catch
                {
                    //NOTE: Reopen connection if ChangeDatabase failed
                }
                CloseConnection();
                OpenConnection();
            }
        }
        #endregion
        #region Public methods
        public DataSet ExecuteDataAdapter(string script)
        {
            var ds = new DataSet();
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();

                var cmd = GetCommand(script, conn);
                var adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            finally
            {
                CloseConnection(conn);
            }
            return ds;
        }

        public void ExecuteProcedure(string name, params SqlParameter[] parameters)
        {
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                var cmd = GetCommandWithParameters(name, conn, CommandType.StoredProcedure, parameters);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public object ExecuteProcedureScalar(string name, params SqlParameter[] parameters)
        {
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                var cmd = GetCommandWithParameters(name, conn, CommandType.StoredProcedure, parameters);
                var result = cmd.ExecuteScalar();
                return result;
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public DataTable GetProcedureResult(string name, params SqlParameter[] parameters)
        {
            var res = new DataTable();
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                var cmd = GetCommandWithParameters(name, conn, CommandType.StoredProcedure, parameters);
                using (var reader = cmd.ExecuteReader())
                {
                    for (var i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        var dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                        res.Columns.Add(dc);
                    }

                    while (reader.Read())
                    {
                        var row = new object[reader.FieldCount];
                        for (var i = 0; i <= reader.FieldCount - 1; i++)
                        {
                            row[i] = reader[i];
                        }
                        res.Rows.Add(row);
                    }
                }
            }
            finally
            {
                CloseConnection(conn);
            }
            return res;
        }

        public void ExecuteNonQuery(string script)
        {
            SqlConnection conn = null;
            var retries = 0;
            RETRY_CONNECT:
            try
            {
                conn = GetConnection();
                var cmd = GetCommand(script, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                //SMELL: Little hack for SQL Azure
                if (retries < 2 &&
                    ex.Message ==
                    @"A transport-level error has occurred when receiving results from the server. (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.)")
                {
                    retries++;
                    goto RETRY_CONNECT;
                }
                throw;
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public void ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();

                var cmd = GetCommand(sql, conn);
                foreach (var par in parameters)
                {
                    cmd.Parameters.Add(par);
                }
                cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection(conn);
            }
        }

        public DataTable ExecuteQuery(string script)
        {
            return ExecuteQuery(script, new SqlParameter[] {});
        }
        public DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            SqlConnection conn = null;
            var res = new DataTable();
            try
            {
                conn = GetConnection();

                var cmd = GetCommand(sql, conn);
                foreach (var par in parameters)
                {
                    cmd.Parameters.Add(par);
                }
                using (var reader = cmd.ExecuteReader())
                {
                    for (var i = 0; i <= reader.FieldCount - 1; i++)
                    {
                        var dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                        res.Columns.Add(dc);
                    }
                    while (reader.Read())
                    {
                        var row = new object[reader.FieldCount];
                        for (var i = 0; i <= reader.FieldCount - 1; i++)
                        {
                            row[i] = reader[i];
                        }
                        res.Rows.Add(row);
                    }
                }
            }
            finally
            {
                CloseConnection(conn);
            }
            return res;
        }

        /// <summary>
        ///     Execute command
        /// </summary>
        /// <param name="script"> Query text </param>
        public string ExecuteNonQueryWithPrintMessage(string script)
        {
            SqlConnection conn = null;
            var retMessage = new StringBuilder();
            SqlInfoMessageEventHandler infoHandler = delegate(object sender, SqlInfoMessageEventArgs e) { retMessage.Append(e.Message); };
            try
            {
                conn = GetConnection();
                conn.InfoMessage += infoHandler;
                var cmd = GetCommand(script, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                {
                    conn.InfoMessage -= infoHandler;
                }
                CloseConnection(conn);
            }
            return retMessage.ToString();
        }
        /// <summary>
        ///     Open <see cref="SqlDataReader" /> reader for script. ATTENTION:This command not close connection!
        /// </summary>
        /// <param name="script"> </param>
        /// <returns> </returns>
        public DbDataReader ExecuteReader(string script)
        {
            var conn = GetConnection();
            var cmd = GetCommand(script, conn);
            var reader = cmd.ExecuteReader();
            return reader;
        }

        /// <summary>
        ///     Open connection for run multiple statement
        /// </summary>
        public void OpenConnection()
        {
            Connection = new SqlConnection(_connBuilder.ToString());
        }
        /// <summary>
        ///     Close connection after run multiple statement
        /// </summary>
        public void CloseConnection()
        {
            CloseConnection(Connection);
            Connection = null;
        }

        public bool IsDBClientException(Exception exception)
        {
            return exception is SqlException;
        }

        public override string ToString()
        {
            return string.Format("{0}; IsOpened: {1}", _connBuilder, IsConnectionOpen);
        }
        #endregion
        #region Private methods
        private SqlConnection Connection
        {
            [DebuggerStepThrough]
            get
            {
                return _sqlConn;
            }
            [DebuggerStepThrough]
            set
            {
                _sqlConn = value;
            }
        }

        private SqlConnection GetConnection()
        {
            if (IsConnectionOpen)
            {
                return Connection;
            }
            var conn = new SqlConnection(_connBuilder.ToString());
            conn.Open();
            return conn;
        }

        private void CloseConnection(SqlConnection connection)
        {
            if (connection != null &&
                connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        #endregion
    }
}