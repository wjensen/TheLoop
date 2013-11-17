using System.Collections.Generic;
using System.Linq;
using TheLoop.PortableEntities.Contract;

namespace TheLoop.PortableLibrary.Data
{

	public class InOutPostDatabase 
	{
		static object locker = new object ();


        public InOutPostDatabase()
		{

		}
		
		public IEnumerable<T> GetItems<T> () where T : IContract, new ()
		{
            //lock (locker) {
               // return (from i in database.Table<T>() select i).ToList();
            //}
            return new List<T>();
		}

        public T GetItem<T>(int id) where T : IContract, new()
		{
            //lock (locker) {
            //    return database.Table<T>().FirstOrDefault(x => x.Id == id);
            //}
            return new T();
		}

		public int SaveItem<T> (T item) where T : IContract
		{
            //lock (locker) {
            //    if (item.Id != 0) {
            //        database.Update(item);
            //        return item.Id;
            //    } else {
            //        return database.Insert(item);
             //   }
            //}

		    return 0;
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