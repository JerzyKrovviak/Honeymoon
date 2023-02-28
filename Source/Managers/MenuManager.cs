using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Menus;
using Honeymoon.Source.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.Managers
{
	public class MenuManager
	{
		public MenuManager()
		{
			Globals.mainMenu = new MainMenu();
			Globals.settingsMenu = new SettingsMenu();
			Globals.generalSettings = new GeneralSettings();
			Globals.volumeSettings = new VolumeSettings();
			Globals.videoSettings = new VideoSettings();
		}

		public virtual void Update()
		{
			if (Globals.gameState == 0)
			{
				Globals.mainMenu.Update();
			}
			else if (Globals.gameState == 2)
			{
				Globals.settingsMenu.Update();
			}
			else if (Globals.gameState == 3)
			{
				Globals.generalSettings.Update();
			}
			else if (Globals.gameState == 4)
			{
				Globals.videoSettings.Update();
			}
			else if (Globals.gameState == 5)
			{
				Globals.volumeSettings.Update();
			}
		}

		public virtual void Draw()
		{
			if (Globals.gameState == 0)
			{
				Globals.mainMenu.Draw();
			}
			else if (Globals.gameState == 2)
			{
				Globals.settingsMenu.Draw();
			}
			else if (Globals.gameState == 3)
			{
				Globals.generalSettings.Draw();
			}
			else if (Globals.gameState == 4)
			{
				Globals.videoSettings.Draw();
			}
			else if (Globals.gameState == 5)
			{
				Globals.volumeSettings.Draw();
			}
		}
	}
}
