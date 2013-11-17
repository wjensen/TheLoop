using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TheLoop.PortableEntities.Contract;
using TheLoop.Web.Core.Repo;

namespace TheLoop.Web.Api
{
    public class InOutPostController : ApiController
    {
        public IEnumerable<InOutPost> Get()
        {
            return InOutPostRepository.GetPosts();
        }
    }
}