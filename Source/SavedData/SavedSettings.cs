using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Text.Json;


namespace Honeymoon.Source
{
    public class SavedSettings
    {
        public static string appdataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon", "persistentsettings.json");
        public int resolution { get; set; }

        public SavedSettings()
        {
            resolution = Globals.selectedResolution;
            CreateFolderIfNotExists();
        }

        public void CreateFolderIfNotExists()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Honeymoon"));
            }
        }

        public static void SaveSettings(SavedSettings settings)
        {
            string serializedText = JsonSerializer.Serialize<SavedSettings>(settings);
            File.WriteAllText(appdataPath, serializedText);
        }

        public static SavedSettings LoadSettings()
        {
            if (CheckIfFileExists())
            {
                var fileContent = File.ReadAllText(appdataPath);
                return JsonSerializer.Deserialize<SavedSettings>(fileContent);
            }
            return null;
        }

        public static bool CheckIfFileExists()
        {
            return File.Exists(appdataPath);
        }
    }
}
