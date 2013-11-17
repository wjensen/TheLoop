using System;
using TheLoop.BL.Contracts;
using TheLoop.DL.SQLite;

namespace TheLoop.BL
{
	/// <summary>
	/// Represents a Task.
	/// </summary>
	public class Task : IBusinessEntity
	{
		public Task ()
		{
		}

		[PrimaryKey, AutoIncrement]
        public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		// new property
		public bool Done { get; set; }
	}
}

