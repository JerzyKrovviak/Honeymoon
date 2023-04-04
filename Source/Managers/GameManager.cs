using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.Menus;
using Honeymoon.Source;
using Honeymoon.Source.World.Map;
using Honeymoon.Source.World.Creatures.Player;

namespace Honeymoon.Source.Managers
{
	public class GameManager
	{
		public GameManager()
		{
			Globals.tilesetManager = new TilesetManager();
			Globals.map = new Map();
			Globals.input = new Source.World.Creatures.InputComponent();
		}

		public virtual void Update()
		{
			Globals.tilesetManager.Update();
			Globals.map.Update();
			Globals.player.Update();
		}

		public virtual void Draw()
		{
			Globals.map.Draw(0);
			//Globals.map.DrawDebugMode();
			Globals.input.DrawPlayerAnim();
		}
	}
}
