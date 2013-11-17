

using System;

namespace TheLoop.PortableLibrary.Contract
{
	/// <summary>
	/// Represents a Task.
	/// </summary>
	public class InOutPost : BaseContract
	{
		public InOutPost ()
		{
		}
		public DateTime InDateTime { get; set; }
		public DateTime OuDateTime { get; set; }
        public String Details { get; set; }
		public bool SendMail { get; set; }
	}
}

