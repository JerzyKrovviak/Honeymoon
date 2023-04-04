using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.Menus;
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
			Globals.playerCreationMenu = new PlayerCreationMenu();
			Globals.worldSelectionMenu = new WorldSelectionMenu();
		}
		public virtual void ResolutionReload()
		{
			Globals.mainMenu.ResolutionReload();
			Globals.settingsMenu.ResolutionReload();
			Globals.generalSettingsMenu.ResolutionReload();
			Globals.volumeSettingsMenu.ResolutionReload();
			Globals.videoSettingsMenu.ResolutionReload();
			Globals.playerSelectionMenu.ResolutionReload();
			Globals.playerCreationMenu.ResolutionReload();
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
			else if (Globals.gameState == 8)
			{
				Globals.worldSelectionMenu.Update();
			}

			if (InputManager.IsKeyNewlyPressed(Keys.Escape))
			{
				Globals.gameState = 0;
				Globals.playerSelectionMenu.deleteConfirm.draw = false;
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
			else if (Globals.gameState == 8)
			{
				Globals.worldSelectionMenu.Draw();
			}
		}
	}
}
