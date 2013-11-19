using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using TheLoop.PortableEntities.Contract;

namespace TheLoop.Web.Core.DataAccess
{
	public abstract class BaseDataAccess
	{
        protected static DataOperationResult<T> ExecuteListProc<T> (string connection, string procId, List<DBManagerParam> parameters)
        {
            var dor = ExecuteDataSetProc<T>(connection, procId, parameters);
            if (!dor.IsValid) return dor; 
            var list = new List<T>();  
            try
            {
                var dt = dor.GetDataTable(0);
                var columnNames = dor.DataSetResult.Tables[0].Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();

                var properties = typeof(T).GetProperties();

                list = dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();

                    foreach (var pro in properties)
                    {
                        if (!columnNames.Contains(pro.Name)) continue;
                        var value = row[pro.Name];
                        pro.SetValue(objT, row.IsNull(pro.Name) ? null : value, null);
                    }

                    return objT;
                }).ToList();

            }
            catch (Exception ex)
            {
                dor.AddException(ex);
            }
            dor.List = list;
            return dor;
        }
        
        protected static DataOperationResult<T> ExecuteDataSetProc<T>(string connection, string procId, List<DBManagerParam> parameters)
		{
			// Will contain error message
			var dor = new DataOperationResult<T>(connection);

			try
			{
				dor.Sql = procId;

				DBManager db = null;

				try
				{
					db = new DBManager(DataProvider.SqlServer, connection);

					if (parameters != null && parameters.Count > 0)
					{
						db.CreateParameters(parameters.Count);
						var index = 0;

						foreach (var param in parameters)
						{
							db.AddParameters(index++, param.ParamName, param.Value, param.Direction, param.IsCursor);
							if (param.Direction == ParameterDirection.Input)
							{
								dor.AddInputParam(param.ParamName, param.Value);
							}
						}
					}
					db.Open();

					var ds = db.ExecuteDataSet(CommandType.StoredProcedure, procId);
					dor.DataSetResult = ds;
				}
				finally
				{
					if (db != null)
					{
						db.Close();
					}
				}
			}
			catch (Exception ex)
			{
				dor.AddException(ex);

				// Create a LogWriter object.
				//var writer = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

				// Use the LogWriter object.
				//writer.Write((string.Format("{0}\n\n{1}",ex.Message,ex.StackTrace)), "DataAccessError", 1, 1, TraceEventType.Error);
			}
			return dor;
		}
        protected static DataOperationResult<T> ExecuteAdHocSql<T>(string dbConn, string sql, bool useAppSettings)
        {
            			if (dbConn == null) throw new ArgumentNullException("dbConn");
			// Will contain error message
			var dor = new DataOperationResult<T>(dbConn);


            try
            {
                var procedure = (useAppSettings) ? ConfigurationManager.AppSettings[sql] : sql;
                dor.Sql = procedure;

                DBManager db = null;

                try
                {
                    db = new DBManager(DataProvider.SqlServer, dbConn);
                					
					db.Open();

                    var ds = db.ExecuteDataSet(CommandType.Text, sql);
                    dor.DataSetResult = ds;
				}
        
				finally
				{
					if (db != null)
					{
						db.Close();
					}
				}
			}
			catch (Exception ex)
			{
				dor.AddException(ex);

				// Create a LogWriter object.
				//var writer = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

				// Use the LogWriter object.
				//writer.Write((string.Format("{0}\n\n{1}", ex.Message, ex.StackTrace)), "DataAccessError", 1, 1, TraceEventType.Error);
			}
			return dor;
            
        }
		protected static DataOperationResult<IContract> ExecuteNonQueryProc(string dbConn, string procId, List<DBManagerParam> parameters,
		                                                         bool useAppSettings)
		{
			if (dbConn == null) throw new ArgumentNullException("dbConn");
			// Will contain error message
			var dor = new DataOperationResult<IContract>(dbConn);


			try
			{
				string procedure = (useAppSettings) ? ConfigurationManager.AppSettings[procId] : procId;
				dor.Sql = procedure;

				DBManager db = null;

				try
				{
					db = new DBManager(DataProvider.SqlServer, dbConn);

					if (parameters != null && parameters.Count > 0)
					{
						db.CreateParameters(parameters.Count);
						var index = 0;

						foreach (var param in parameters)
						{
							db.AddParameters(index++, param.ParamName, param.Value, param.Direction, param.IsCursor);
							if (param.Direction == ParameterDirection.Input)
							{
								dor.AddInputParam(param.ParamName, param.Value);
							}
						}
					}
					db.Open();

					db.ExecuteNonQuery(CommandType.StoredProcedure, procedure);
				}
				finally
				{
					if (db != null)
					{
						db.Close();
					}
				}
			}
			catch (Exception ex)
			{
				dor.AddException(ex);

				// Create a LogWriter object.
				//var writer = EnterpriseLibraryContainer.Current.GetInstance<LogWriter>();

				// Use the LogWriter object.
				//writer.Write((string.Format("{0}\n\n{1}", ex.Message, ex.StackTrace)), "DataAccessError", 1, 1, TraceEventType.Error);
			}
			return dor;
		}
	}
}