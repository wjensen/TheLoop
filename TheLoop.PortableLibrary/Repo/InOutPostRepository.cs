using System.Collections.Generic;
using TheLoop.DL.SQLiteBase;
using TheLoop.PortableEntities.Contract;
using TheLoop.PortableLibrary.Data;

namespace TheLoop.PortableLibrary.Repo {
	public class InOutPostRepository {
		InOutPostDatabase db = null;
		protected static string dbLocation;		
		//protected static TaskRepository me;

        public InOutPostRepository(SQLiteConnection conn, string dbLocation)
		{
			db = new InOutPostDatabase(conn, dbLocation);
		}

		public InOutPost GetPost(int id)
		{
            return db.GetItem<InOutPost>(id);
		}

        public IEnumerable<InOutPost> GetPosts()
		{
			return db.GetItems<InOutPost>();
		}

        public int SavePost(InOutPost item)
		{
            return db.SaveItem<InOutPost>(item);
		}

		public int DeletePost(int id)
		{
            return db.DeleteItem<InOutPost>(id);
		}
	}
}

