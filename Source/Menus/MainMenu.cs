using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Honeymoon.Managers;
using Honeymoon.Source.Menus;
using Honeymoon.Source;

namespace Honeymoon.Menus
{
	public class MainMenu
	{
		private static List<MenuButton> menuButtons = new List<MenuButton>();
		private static MenuButton logo;
		public MainMenu()
		{
			logo = new MenuButton(Globals.content.Load<Texture2D>("MiscSprites/logo"), Vector2.Zero, new Rectangle(0, 0, 152, 68), 4, Color.White);
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Singleplayer", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Multiplayer", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Settings", Vector2.Zero, 3, Color.White));
			menuButtons.Add(new MenuButton(FontManager.hm_f_menu, "Exit", Vector2.Zero, 3, Color.White));
			ResolutionReload();
		}
		public virtual void ResolutionReload()
		{
			logo.position = new Vector2(logo.PerfectMidPositionTexture().X, 30);
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i);
			}
		}
		public virtual void Update()
		{
			for (int i = 0; i < menuButtons.Count; i++)
			{
				menuButtons[i].Update();
				menuButtons[i].position = new Vector2(menuButtons[i].PerfectMidPositionText().X, menuButtons[i].PerfectMidPositionText().Y + 80 * i);
			}
			if (menuButtons[0].IsHoveredAndClicked())
			{
				Globals.gameState = 6;
				PlayerSelectionMenu.LoadPlayerProfiles();
			}
			else if (menuButtons[2].IsHoveredAndClicked())
			{
				Globals.gameState = 2;
			}
			else if (menuButtons[3].IsHoveredAndClicked())
			{
				Globals.persistentSettings = new SavedSettings();
				SavedSettings.SaveSettings(Globals.persistentSettings);
				Main.self.Exit();
			}
		}
		public virtual void Draw()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.DrawString();
			}
			logo.DrawTexture();
		}
	}
}
