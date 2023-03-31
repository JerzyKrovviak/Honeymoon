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
		public Color pantsColor { get; set; }
		public Color shirtColor { get; set; }
		public Vector2 Position { get; set; }
	}
}
