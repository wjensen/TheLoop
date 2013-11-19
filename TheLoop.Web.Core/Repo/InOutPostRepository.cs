using System.Collections.Generic;
using TheLoop.PortableEntities.Contract;
using TheLoop.Web.Core.Data;

namespace TheLoop.Web.Core.Repo {
	public static class InOutPostRepository {
		static TheLoopDatabase db  = new TheLoopDatabase();

        public static InOutPost GetPost(int id)
		{
            return db.GetItem<InOutPost>(id, "dbo.GetInOutPost");
		}

        public static IEnumerable<InOutPost> GetPosts()
		{
            return db.GetItems<InOutPost>("dbo.GetInOutPosts");
		}

        public static int SavePost(InOutPost item)
		{
            return db.SaveItem<InOutPost>(item, "dbo.UpdateInsertInOutPost");
		}

        public static int DeletePost(int id)
		{
            return db.DeleteItem<InOutPost>(id);
		}
	}
}

