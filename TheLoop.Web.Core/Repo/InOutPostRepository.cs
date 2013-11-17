using System.Collections.Generic;
using TheLoop.PortableEntities.Contract;
using TheLoop.PortableLibrary.Data;

namespace TheLoop.Web.Core.Repo {
	public static class InOutPostRepository {
		static InOutPostDatabase db  = new InOutPostDatabase();


        public static InOutPost GetPost(int id)
		{
            return db.GetItem<InOutPost>(id);
		}

        public static IEnumerable<InOutPost> GetPosts()
		{
			return db.GetItems<InOutPost>();
		}

        public static int SavePost(InOutPost item)
		{
            return db.SaveItem<InOutPost>(item);
		}

        public static int DeletePost(int id)
		{
            return db.DeleteItem<InOutPost>(id);
		}
	}
}

