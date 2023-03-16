using Honeymoon.Source.World.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.SavedData
{
	public class WorldSave
	{
		public int id, day;
		public string name;
		public MapObject[] MapObjectsData;

		public WorldSave(int id, string name, MapObject[] MapObjectsData, int day)
		{
			this.id = id;
			this.name = name;
			this.MapObjectsData = MapObjectsData;
			this.day = day;
		}
	}
}
