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

		public void CreateFolderIfNotExists()
		{
			if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles")))
			{
				Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles"));
			}
		}

		public bool CheckIfProfileExists()
		{
			if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles\\" + Name + ".yaml")))
			{
				return false;
			}
			return true;
		}

		public void CreatePlayerProfile(object data)
		{
			if (!CheckIfProfileExists())
			{
				string profilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon\\PlayerProfiles\\" + Name + ".yaml");
				//System.Diagnostics.Debug.WriteLine("***Dumping Object Using Yaml Serializer***");
				var stringBuilder = new StringBuilder();
				var serializer = new Serializer();
				stringBuilder.AppendLine(serializer.Serialize(data));
				//System.Diagnostics.Debug.WriteLine(stringBuilder);
				//System.Diagnostics.Debug.WriteLine("");

				using (StreamWriter writer = new StreamWriter(profilePath))
				{
					serializer.Serialize(writer, data);
				}
			}
		}
	}
}
