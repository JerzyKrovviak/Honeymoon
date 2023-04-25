using Honeymoon.Managers;
using Honeymoon.Source.Menus;
using Honeymoon.Source.World.Creatures.Player;
using Honeymoon.Source.World.Map;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.Managers
{
	public class GameManager
	{
		public GameManager()
		{
			Globals.tilesetManager = new TilesetManager();
			Globals.map = new Map();
			Globals.input = new Source.World.Creatures.InputComponent();
			Globals.inGameMenu = new InGameMenu();
		}
		public virtual void ResolutionReload()
		{
			Globals.inGameMenu.ResolutionReload();
			Globals.mainInventory.ResolutionReload();
			Camera.CalculateTranslation();
		}
		public virtual void Update()
		{
			if (InputManager.IsKeyNewlyPressed(Keys.Escape) && !Globals.openInventory)
			{
				Globals.openIngMenu = !Globals.openIngMenu;
				Globals.inGameMenu.confirmExit.draw = false;
				AudioManager.soundBank.PlayCue("selectorAdd");
			}
			else if (InputManager.IsKeyNewlyPressed(Keys.Tab) && !Globals.openIngMenu)
			{
				Globals.openInventory = !Globals.openInventory;
				AudioManager.soundBank.PlayCue("selectorAdd");
			}

			if (!Globals.openInventory && !Globals.openIngMenu)
			{
				Globals.isAnyMenuOpen = false;
			}
			else
			{
				Globals.isAnyMenuOpen = true;
			}

			if (!Globals.isAnyMenuOpen)
			{
				Globals.tilesetManager.Update();
				Globals.map.Update();
				Globals.player.Update();
				Globals.objectLayer.Update();
			}

			if (Globals.openIngMenu)
			{
				Globals.inGameMenu.Update();
			}
			if (Globals.openInventory)
			{
				Globals.mainInventory.Update();
			}
		}
		public virtual void Draw()
		{
			Globals.map.Draw(Globals.player.mapId);
			//Globals.map.DrawDebugMode();
			Globals.objectLayer.DrawUnder();
			Globals.input.DrawPlayerAnimations();
			Globals.objectLayer.DrawAbove();
			if (Globals.openIngMenu)
			{
				Globals.inGameMenu.Draw();
			}
			if (Globals.openInventory)
			{
				Globals.mainInventory.Draw();
			}
		}
	}
}
