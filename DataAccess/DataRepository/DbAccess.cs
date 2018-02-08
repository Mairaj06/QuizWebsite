using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataRepository
{
    public class DbAccess : IDisposable
    {
        SqlConnection conn = null;
        SqlCommand command = null;
        SqlDataAdapter adapter = null;
        string connectionString = ConfigurationManager.ConnectionStrings["ConnQuizDB"].ConnectionString;
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Clear all property values that maybe have been set
                    // when the class was instantiated

                }

                // Indicate that the instance has been disposed.
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DbAccess()
        {

        }
        public DbAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private void CreateConnection()
        {
            try
            {
                conn = new SqlConnection(connectionString);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// This method is used to return SqlDataReader.When finished working with DataReader call method CloseConnection of this class.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, List<SqlParameter> parameters)
        {
            SqlDataReader reader = null;
            try
            {
                CreateConnection();
                command = new SqlCommand();
                BuildCommand(command, commandType, commandText, conn);
                AddParametersToCommand(parameters, command);
                reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {

            }
            finally
            {


            }
            return reader;
        }
        /// <summary>
        /// This method is used to get records from database and return list of the Type which is set in name of this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        
        /// <summary>
        /// This method is used to fill DataTable from database.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="dataSet"></param>
        /// <param name="dataTbl"></param>
        public void FillDataTable(CommandType commandType, string commandText, List<SqlParameter> parameters, DataTable dataTbl)
        {
            try
            {
                CreateConnection();
                command = new SqlCommand();
                BuildCommand(command, commandType, commandText, conn);
                AddParametersToCommand(parameters, command);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTbl);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                adapter.Dispose();
                CloseConnection();
            }

        }
        /// <summary>
        /// This method is used to fill a dataset from database. If you want to return more than one table call this method.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="dataSet"></param>
        public void FillDataSet(CommandType commandType, string commandText, List<SqlParameter> parameters, DataSet dataSet)
        {
            try
            {
                CreateConnection();
                command = new SqlCommand();
                BuildCommand(command, commandType, commandText, conn);
                AddParametersToCommand(parameters, command);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataSet);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                adapter.Dispose();
                CloseConnection();
            }

        }
        /// <summary>
        /// This method is used to fill DataTable from database.Datable will be filled on basis of the name passed as argument.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="dataSet"></param>
        /// <param name="tblName"></param>
        public void FillDataTableByName(CommandType commandType, string commandText, List<SqlParameter> parameters, DataSet dataSet, string tblName)
        {
            try
            {
                CreateConnection();
                command = new SqlCommand();
                BuildCommand(command, commandType, commandText, conn);
                AddParametersToCommand(parameters, command);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataSet, tblName);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                adapter.Dispose();
                CloseConnection();
            }

        }
        /// <summary>
        /// This method is used to insert/update/delete records from databse.It returns number of rows effected as int.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteQuery(CommandType commandType, string commandText, List<SqlParameter> parameters)
        {
            try
            {

                CreateConnection();
                command = new SqlCommand();
                BuildCommand(command, commandType, commandText, conn);
                AddParametersToCommand(parameters, command);
                return command.ExecuteNonQuery();
            }
            catch (Exception exp)
            {

                throw exp;
            }
            finally
            {

                CloseConnection();
            }

        }
        /// <summary>
        /// This method is used to return only a single column value.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string GetColumnValue(CommandType commandType, string commandText, List<SqlParameter> parameters)
        {
            try
            {
                CreateConnection();
                command = new SqlCommand();
                BuildCommand(command, commandType, commandText, conn);
                AddParametersToCommand(parameters, command);
                object objResult = command.ExecuteScalar();
                if (objResult == null)
                {
                    return "";
                }
                if (objResult == System.DBNull.Value)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(objResult);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        private void BuildCommand(SqlCommand command, CommandType commandType, string commandText, SqlConnection conn)
        {
            command.Connection = conn;
            command.CommandText = commandText;
            command.CommandType = commandType;
        }
        private void AddParametersToCommand(List<SqlParameter> parameters, SqlCommand command)
        {
            if (parameters.Count > 0)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    command.Parameters.Add(parameters[i]);
                }
            }
        }
        /// <summary>
        /// This method is used to close connection to database. Call this method if you used SqlDataReader after finishing operation on DataReader. 
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                conn.Dispose();
                command.Parameters.Clear();
                command.Dispose();
            }
            catch (Exception ex)
            {
                conn.Close();
            }


        }
    }
}
