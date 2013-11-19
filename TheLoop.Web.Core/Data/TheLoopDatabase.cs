using System.Collections.Generic;
using System.Data;
using System.Linq;
using TheLoop.PortableEntities.Contract;
using TheLoop.Web.Core.DataAccess;

namespace TheLoop.Web.Core.Data
{

	public class TheLoopDatabase : BaseDatabase
	{
		
		public IEnumerable<T> GetItems<T> (string procid) where T : IContract, new ()
		{
		    var dor = ExecuteListProc<T>(GetDBConnection(), procid, null);
            if (!dor.IsValid)
            {
                throw dor.GetException;
            }
            return dor.List;
		}

        public T GetItem<T>(int id, string procid) where T : IContract, new()
        {
            var dor = ExecuteListProc<T>(GetDBConnection(), procid, null);
            if (!dor.IsValid)
            {
                throw dor.GetException;
            }
            return dor.List.FirstOrDefault();
		}

		public int SaveItem<T> (T item, string procid) where T : IContract
		{
		    var dor = ExecuteNonQueryProc(GetDBConnection(), procid,
		        new List<DBManagerParam> {new DBManagerParam( "@xml", ParameterDirection.Input, item.ToXml(), false)},false);
            if (!dor.IsValid)
            {
                throw dor.GetException;
            }
		    return item.Id;
		}

        public int DeleteItem<T>(int id) where T : IContract, new()
		{
            //lock (locker) {
            //    return database.Delete<T>(new T() { Id = id });
            //}
            return 0;
		}
	}
}