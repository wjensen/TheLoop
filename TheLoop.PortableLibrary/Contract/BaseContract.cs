


namespace TheLoop.PortableLibrary.Contract {
	/// <summary>
	///Contract base class. Provides the ID property.
	/// </summary>
	public abstract class BaseContract : IContract {
		public BaseContract ()
		{
		}
		
		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>


	    public int Id { get; set; }
	}
}

