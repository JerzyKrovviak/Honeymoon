using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeymoon.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Source.World.Creatures
{
	public class InputComponent
	{
		public void Update()
		{
			if (InputManager.IsKeyDown(Keys.A) && !Map.Map.CheckForCollision(Globals.player))
			{
				Globals.player.direction = 7;
				Globals.player.position.X -= (Globals.player.velocity.X) * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
				Globals.player.animation[0].PlayAnimation();
				Globals.player.flipHorizontal = true;
			}
			else if (InputManager.IsKeyDown(Keys.D) && !Map.Map.CheckForCollision(Globals.player))
			{
				Globals.player.direction = 3;
				Globals.player.position.X += (Globals.player.velocity.X) * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
				Globals.player.animation[0].PlayAnimation();
				Globals.player.flipHorizontal = false;
			}

			if (InputManager.IsKeyDown(Keys.W) && !Map.Map.CheckForCollision(Globals.player))
			{
				Globals.player.direction = 1;
				Globals.player.position.Y -= (Globals.player.velocity.Y) * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
				Globals.player.animation[2].PlayAnimation();
			}
			else if (InputManager.IsKeyDown(Keys.S) && !Map.Map.CheckForCollision(Globals.player))
			{
				Globals.player.direction = 5;
				Globals.player.position.Y += (Globals.player.velocity.Y) * (float)Globals.gameTime.ElapsedGameTime.TotalSeconds;
				Globals.player.animation[1].PlayAnimation();
			}

			if (InputManager.IsKeyDown(Keys.W) && InputManager.IsKeyDown(Keys.D))
			{
				Globals.player.direction = 2;
			}
			else if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.D))
			{
				Globals.player.direction = 4;
			}
			else if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.A))
			{
				Globals.player.direction = 6;
			}
			else if (InputManager.IsKeyDown(Keys.A) && InputManager.IsKeyDown(Keys.W))
			{
				Globals.player.direction = 8;
			}

			if (InputManager.IsKeyNewlyPressed(Keys.Escape))
			{
				Globals.gameState = 0;
			}
		}
	}
}
