using System;

namespace TheLoop.PortableEntities.Contract
{
	/// <summary>
	/// Represents a Task.
	/// </summary>
	public class InOutPost : BaseContract
	{
	    public DateTime InDateTime { get; set; }
		public DateTime OutDateTime { get; set; }
        public String EmployeeName { get; set; }
        public String Details { get; set; }
		public bool SendMail { get; set; }
	}
}

