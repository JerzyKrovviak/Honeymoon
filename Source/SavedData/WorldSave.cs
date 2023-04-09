using Honeymoon.Source.World.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.SavedData
{
	[DataContract]
	public class WorldSave
	{
		[DataMember]
		public string name;
		//public MapObject[] MapObjectsData;
		[DataMember]
		public List<MapObject[]> mapObjectsData;
	}
}
