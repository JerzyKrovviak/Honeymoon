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
			Camera.CalculateTranslation();
		}
		public virtual void Update()
		{
			if (InputManager.IsKeyNewlyPressed(Keys.Escape))
			{
				Globals.ingMenu = !Globals.ingMenu;
				AudioManager.soundBank.PlayCue("selectorAdd");
			}

			if (!Globals.ingMenu)
			{
				Globals.tilesetManager.Update();
				Globals.map.Update();
				Globals.player.Update();
			}
			else
			{
				Globals.inGameMenu.Update();
			}
		}
		public virtual void Draw()
		{
			Globals.map.Draw(0);
			//Globals.map.DrawDebugMode();
			Globals.input.DrawPlayerAnim();
			Globals.objectLayer.Draw();
			if (Globals.ingMenu)
			{
				Globals.inGameMenu.Draw();
			}
		}
	}
}
