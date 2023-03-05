﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Honeymoon.Managers;
using Honeymoon.Menus;
using Honeymoon.Source.Menus;
using Honeymoon.Source;
using Honeymoon.Source.World.Map;
using Honeymoon.Source.World.Creatures.Player;
using Honeymoon.Source.Managers;

namespace Honeymoon
{
	public class Main : Game
	{
		public static Main self;

		public Main()
		{
			Globals._graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			self = this;
		}

		protected override void Initialize()
		{
			base.Initialize();
			Globals.windowSize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			if (SavedSettings.CheckIfFileExists())
			{
				Globals.persistentSettings = SavedSettings.LoadSettings();
				Globals.ChangeGameResolution(Globals.persistentSettings.resolution);
			}
			else
			{
				Globals.ChangeGameResolution(2);
			}
			Globals._graphics.ApplyChanges();
		}

		public static bool IsOutOfFocus()
		{
			return !self.IsActive;
		}

		protected override void LoadContent()
		{
			Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
			Globals.content = this.Content;
			AudioManager.LoadAudioContent();
			FontManager.LoadContent(Content);
			Globals.gameManager = new GameManager();
			Globals.menuManager = new MenuManager();
		}

		protected override void Update(GameTime gameTime)
		{
			Globals.windowSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			if (!IsOutOfFocus())
			{
				Globals.gameTime = gameTime;
				MouseState mouseState = Mouse.GetState();
				Globals.mousePosition = new Vector2(mouseState.X, mouseState.Y);
				InputManager.SetCurrentStates(new ButtonState[] { mouseState.LeftButton, mouseState.RightButton });
				InputManager.SetCurrentStates(Keyboard.GetState());

				if (Globals.gameState == 1)
				{
					Globals.gameManager.Update();
				}
				else
				{
					Globals.menuManager.Update();
				}
			}
			AudioManager.audioEngine.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			if (Globals.gameState == 1)
			{
				Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, transformMatrix: Camera._translation);
				Globals._graphics.GraphicsDevice.Clear(new Color(255, 235, 177));
				Globals.gameManager.Draw();
				Globals.spriteBatch.End();
			}
			else
			{
				Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, null);
				Globals._graphics.GraphicsDevice.Clear(new Color(255, 235, 177));
				Globals.menuManager.Draw();
				Globals.spriteBatch.End();
			}
		}
	}
}