namespace TheLoop.Web.Core.Framework
{
    public class DefaultDBContract : BaseBusinessObject
    {
        public DMIUser User = SessionHelper.User();
    }
   
}