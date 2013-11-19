
using System.Runtime.Serialization;

using System.IO;

namespace TheLoop.PortableEntities.Contract {
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
        public string ToXml()
        {
            
            string xml;

            using (var memStm = new MemoryStream())
            {
                var serializer = new DataContractSerializer(GetType());
                serializer.WriteObject(memStm, this);

                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm))
                {
                    xml = streamReader.ReadToEnd();
                    
                }
            }

            //xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            //xml = xml.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
            //xml = xml.Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            //xml = xml.Replace("xsi:nil=\"true\"", "");

            return xml;
        }

    }
}

