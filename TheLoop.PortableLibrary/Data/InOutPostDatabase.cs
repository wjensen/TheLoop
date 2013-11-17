using System.Collections.Generic;
using System.Linq;
using TheLoop.DL.SQLiteBase;
using TheLoop.PortableEntities.Contract;

namespace TheLoop.PortableLibrary.Data
{
	/// <summary>
    /// InOutPostDatabase builds on SQLite.Net and represents a specific database, in our case, the Task DB.
	/// It contains methods for retrieval and persistance as well as db creation, all based on the 
	/// underlying ORM.
	/// </summary>
	public class InOutPostDatabase 
	{
		static object locker = new object ();

        SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="TheLoop.Data.InOutPostDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
        public InOutPostDatabase(SQLiteConnection conn, string path)
		{
            database = conn;
			// create the tables
            database.CreateTable<InOutPost>();
		}
		
		public IEnumerable<T> GetItems<T> () where T : IContract, new ()
		{
            lock (locker) {
                return (from i in database.Table<T>() select i).ToList();
            }
		}

        public T GetItem<T>(int id) where T : IContract, new()
		{
            lock (locker) {
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
            }
		}

		public int SaveItem<T> (T item) where T : IContract
		{
            lock (locker) {
                if (item.Id != 0) {
                    database.Update(item);
                    return item.Id;
                } else {
                    return database.Insert(item);
                }
            }
		}

        public int DeleteItem<T>(int id) where T : IContract, new()
		{
            lock (locker) {
                return database.Delete<T>(new T() { Id = id });
            }
		}
	}
}