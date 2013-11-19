using System.Collections.Generic;
using System.Configuration;
using TheLoop.Web.Core.DataAccess;

namespace TheLoop.Web.Core.Data
{
	public class BaseDatabase : BaseDataAccess
	{
		protected static string GetDBConnection()
		{
			return ConfigurationManager.AppSettings["DBConnection"];
		}

		
	}
}