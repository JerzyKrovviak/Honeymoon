using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.SavedData
{
	public class PlayerSave
	{
		public string Name { get; set; }
		public Color pantsColor { get; set; }
		public Color shirtColor { get; set; }
		public Vector2 Position { get; set; }

		public PlayerSave()
		{
			CreateFolderIfNotExists();
		}

		public void CreateFolderIfNotExists()
		{
			if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles")))
			{
				Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PlayerProfiles"));
			}
		}
	}
}
