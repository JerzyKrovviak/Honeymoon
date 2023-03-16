using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
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
			Globals.generalSettingsMenu = new GeneralSettingsMenu();
			Globals.volumeSettingsMenu = new VolumeSettingsMenu();
			Globals.videoSettingsMenu = new VideoSettingsMenu();
			Globals.playerSelectionMenu = new PlayerSelectionMenu();
			Globals.worldSelectionMenu = new WorldSelectionMenu();
			Globals.playerCreationMenu = new PlayerCreationMenu();
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
				Globals.generalSettingsMenu.Update();
			}
			else if (Globals.gameState == 4)
			{
				Globals.videoSettingsMenu.Update();
			}
			else if (Globals.gameState == 5)
			{
				Globals.volumeSettingsMenu.Update();
			}
			else if (Globals.gameState == 6)
			{
				Globals.playerSelectionMenu.Update();
			}
			else if (Globals.gameState == 7)
			{
				Globals.playerCreationMenu.Update();
			}

			if (InputManager.IsKeyNewlyPressed(Keys.Escape))
			{
				Globals.gameState = 0;
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
				Globals.generalSettingsMenu.Draw();
			}
			else if (Globals.gameState == 4)
			{
				Globals.videoSettingsMenu.Draw();
			}
			else if (Globals.gameState == 5)
			{
				Globals.volumeSettingsMenu.Draw();
			}
			else if (Globals.gameState == 6)
			{
				Globals.playerSelectionMenu.Draw();
			}
			else if (Globals.gameState == 7)
			{
				Globals.playerCreationMenu.Draw();
			}
		}
	}
}
