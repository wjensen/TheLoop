using System;
using System.Collections.Generic;
using System.Data;

namespace TheLoop.Web.Core.DataAccess
{
	public enum DataProvider
	{
		Oracle,
		SqlServer,
		OleDb,
		Odbc
	}

	public sealed class DBManager : IDBManager, IDisposable
	{
		private IDbCommand _idbCommand;
		private IDbConnection _idbConnection;
		private IDbDataParameter[] _idbParameters;
		private IDbTransaction _idbTransaction;
		private DataProvider _providerType;

		public DBManager()
		{
		}

		public DBManager(DataProvider providerType)
		{
			this._providerType = providerType;
		}

		public DBManager(DataProvider providerType, string
			                                            connectionString)
		{
			this._providerType = providerType;
			ConnectionString = connectionString;
		}

		public IDbConnection Connection
		{
			get { return _idbConnection; }
		}

		public IDataReader DataReader { get; set; }

		public DataProvider ProviderType
		{
			get { return _providerType; }
			set { _providerType = value; }
		}

		public string ConnectionString { get; set; }

		public IDbCommand Command
		{
			get { return _idbCommand; }
		}

		public IDbTransaction Transaction
		{
			get { return _idbTransaction; }
		}

		public IDbDataParameter[] Parameters
		{
			get { return _idbParameters; }
		}

		public void Open()
		{
			_idbConnection =
				DBManagerFactory.GetConnection(_providerType);
			_idbConnection.ConnectionString = ConnectionString;
			if (_idbConnection.State != ConnectionState.Open)
				_idbConnection.Open();
			_idbCommand = DBManagerFactory.GetCommand(ProviderType);
		}

		public void Close()
		{
			if (_idbConnection.State != ConnectionState.Closed)
				_idbConnection.Close();
		}

		public void Dispose()
		{
// ReSharper disable GCSuppressFinalizeForTypeWithoutDestructor
			GC.SuppressFinalize(this);
// ReSharper restore GCSuppressFinalizeForTypeWithoutDestructor
			Close();
			_idbCommand = null;
			_idbTransaction = null;
			_idbConnection = null;
		}

		public void CreateParameters(int paramsCount)
		{
			_idbParameters = new IDbDataParameter[paramsCount];
			_idbParameters = DBManagerFactory.GetParameters(ProviderType, paramsCount);
		}

		public void AddParameters(int index, string paramName, object objValue)
		{
			if (index < _idbParameters.Length)
			{
				_idbParameters[index].ParameterName = paramName;
				_idbParameters[index].Value = objValue;
			}
		}

		//dop.AddParameter("vUser", OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output);

		public void BeginTransaction()
		{
			if (_idbTransaction == null)
				_idbTransaction =
					DBManagerFactory.GetTransaction(ProviderType);
			_idbCommand.Transaction = _idbTransaction;
		}

		public void CommitTransaction()
		{
			if (_idbTransaction != null)
				_idbTransaction.Commit();
			_idbTransaction = null;
		}

		public IDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			_idbCommand = DBManagerFactory.GetCommand(ProviderType);
			_idbCommand.Connection = Connection;
			PrepareCommand(_idbCommand, Connection, Transaction,
			               commandType,
			               commandText, Parameters);
			DataReader = _idbCommand.ExecuteReader();
			_idbCommand.Parameters.Clear();
			return DataReader;
		}

		public void CloseReader()
		{
			if (DataReader != null)
				DataReader.Close();
		}

		public int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			_idbCommand = DBManagerFactory.GetCommand(ProviderType);
			PrepareCommand(_idbCommand, Connection, Transaction,
			               commandType, commandText, Parameters);
			int returnValue = _idbCommand.ExecuteNonQuery();
			_idbCommand.Parameters.Clear();
			return returnValue;
		}

		public object ExecuteScalar(CommandType commandType, string commandText)
		{
			_idbCommand = DBManagerFactory.GetCommand(ProviderType);
			PrepareCommand(_idbCommand, Connection, Transaction,
			               commandType,
			               commandText, Parameters);
			object returnValue = _idbCommand.ExecuteScalar();
			_idbCommand.Parameters.Clear();
			return returnValue;
		}

		public DataSet ExecuteDataSet(CommandType commandType, string commandText)
		{
			_idbCommand = DBManagerFactory.GetCommand(ProviderType);
			PrepareCommand(_idbCommand, Connection, Transaction,
			               commandType,
			               commandText, Parameters);
			IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter
				(ProviderType);
			dataAdapter.SelectCommand = _idbCommand;
			var dataSet = new DataSet();
			dataAdapter.Fill(dataSet);

			_idbCommand.Parameters.Clear();
			return dataSet;
		}

		public void AddParameters(int index, string paramName, object objValue, ParameterDirection direction, bool isCursor)
		{
			if (index < _idbParameters.Length)
			{
				_idbParameters[index].ParameterName = paramName;
				_idbParameters[index].Value = objValue;
				_idbParameters[index].Direction = direction;

			}
		}

		private void AttachParameters(IDbCommand command, IEnumerable<IDbDataParameter> commandParameters)
		{
			foreach (var idbParameter in commandParameters)
			{
				if ((idbParameter.Direction == ParameterDirection.InputOutput)
				    &&
				    (idbParameter.Value == null))
				{
					idbParameter.Value = DBNull.Value;
				}
				command.Parameters.Add(idbParameter);
			}
		}

		private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction,
		                            CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters)
		{
			command.Connection = connection;
			command.CommandText = commandText;
			command.CommandType = commandType;

			if (transaction != null)
			{
				command.Transaction = transaction;
			}

			if (commandParameters != null)
			{
				AttachParameters(command, commandParameters);
			}
		}
	}
}