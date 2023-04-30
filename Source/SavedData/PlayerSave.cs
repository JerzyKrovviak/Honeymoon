using Honeymoon.Source.World.HudElements;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace Honeymoon.Source.SavedData
{
	public class PlayerSave
	{
		public string Name { get; set; }
		public int mapId { get; set; }
		public Vector2 Position { get; set; }
		public Color pantsColor { get; set; }
		public Color shirtColor { get; set; }
		public Color skinColor { get; set; }
		public Color eyesColor { get; set; }
		public Color bootsColor { get; set; }
		public float walkSpeed { get; set; }
		//public Item[] items { get; set; }
	}
}
