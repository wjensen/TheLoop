using System;
using System.Data;

namespace TheLoop.Web.Core.DataAccess
{
	public class DBManagerParam
	{
		private readonly ParameterDirection direction;
		private readonly bool isCursor;
		private readonly string paramName;
		private readonly object value;

		public DBManagerParam(string paramName, ParameterDirection direction, object value, bool isCursor)
		{
			this.paramName = paramName;
			this.direction = direction;
			this.value = value ?? DBNull.Value;
			this.isCursor = isCursor;
		}

		public string ParamName
		{
			get { return paramName; }
		}

		public ParameterDirection Direction
		{
			get { return direction; }
		}

		public object Value
		{
			get { return value; }
		}

		public bool IsCursor
		{
			get { return isCursor; }
		}
	}
}