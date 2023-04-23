using Honeymoon.Source.World.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.SavedData
{
	[DataContract(Namespace = "")]
	public class WorldSave
	{
		[DataMember(Name = "worldname", IsRequired = true, Order = 1, EmitDefaultValue = false)]
		public string name { get; set; }
		[DataMember(Name = "objectsdata", IsRequired = true, Order = 2, EmitDefaultValue = false)]
		public List<MapObject> mapObjectsData { get; set; }
	}
}
