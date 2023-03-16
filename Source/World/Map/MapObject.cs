using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Map
{
	public class MapObject : PhysicalComponent
	{
		public int id, mapid;
		public string name;
		public bool canHit;
	}
}
