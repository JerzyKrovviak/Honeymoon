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
			if (InputManager.IsKeyDown(Keys.A))
			{
				Globals.player.direction = 7;
				Globals.player.position.X -= (Globals.player.velocity.X) * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
				for (int i = 5; i < 10; i++)
				{
					Globals.player.animations[i].PlayAnimation();
					Globals.player.animations[i].flipHorizontal = true;
				}
			}
			else if (InputManager.IsKeyDown(Keys.D))
			{
				Globals.player.direction = 3;
				Globals.player.position.X += (Globals.player.velocity.X) * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
				for (int i = 5; i < 10; i++)
				{
					Globals.player.animations[i].PlayAnimation();
					Globals.player.animations[i].flipHorizontal = false;
				}
			}

			if (InputManager.IsKeyDown(Keys.W))
			{
				Globals.player.direction = 1;
				Globals.player.position.Y -= (Globals.player.velocity.Y) * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
				for (int i = 10; i < 15; i++)
				{
					Globals.player.animations[i].PlayAnimation();
				}
			}
			else if (InputManager.IsKeyDown(Keys.S))
			{
				Globals.player.direction = 5;
				Globals.player.position.Y += (Globals.player.velocity.Y) * (float)Globals.gameTime.ElapsedGameTime.TotalMilliseconds;
				for (int i = 0; i < 5; i++)
				{
					Globals.player.animations[i].PlayAnimation();
				}
			}

			//if (InputManager.IsKeyDown(Keys.W) && InputManager.IsKeyDown(Keys.D))
			//{
			//	Globals.player.direction = 2;
			//}
			//else if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.D))
			//{
			//	Globals.player.direction = 4;
			//}
			//else if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.A))
			//{
			//	Globals.player.direction = 6;
			//}
			//else if (InputManager.IsKeyDown(Keys.A) && InputManager.IsKeyDown(Keys.W))
			//{
			//	Globals.player.direction = 8;
			//}

			if (!InputManager.AreKeysDown(new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D }))
			{
				foreach (AnimatedComponent animation in Globals.player.animations)
				{
					animation.StopAnimation();
				}
			}

			if (InputManager.IsKeyNewlyPressed(Keys.Escape))
			{
				Globals.gameState = 0;
			}
		}

		public virtual void DrawPlayerAnim()
		{
			if (InputManager.IsKeyDown(Keys.A))
			{
				for (int i = 5; i < 10; i++)
				{
					Globals.player.animations[i].Draw(Globals.player.position);
				}
			}
			else if (InputManager.IsKeyDown(Keys.D))
			{
				for (int i = 5; i < 10; i++)
				{
					Globals.player.animations[i].Draw(Globals.player.position);
				}
			}
			else if (InputManager.IsKeyDown(Keys.W))
			{
				for (int i = 10; i < 15; i++)
				{
					Globals.player.animations[i].Draw(Globals.player.position);
				}
			}
			else if (InputManager.IsKeyDown(Keys.S))
			{
				for (int i = 0; i < 5; i++)
				{
					Globals.player.animations[i].Draw(Globals.player.position);
				}
			}

			if (!InputManager.AreKeysDown(new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D }))
			{
				if (Globals.player.direction == 1)
				{
					for (int i = 10; i < 15; i++)
					{
						Globals.player.animations[i].Draw(Globals.player.position);
					}
				}
				else if (Globals.player.direction == 5)
				{
					for (int i = 0; i < 5; i++)
					{
						Globals.player.animations[i].Draw(Globals.player.position);
					}
				}
				else if (Globals.player.direction == 3 || Globals.player.direction == 7)
				{
					for (int i = 5; i < 10; i++)
					{
						Globals.player.animations[i].Draw(Globals.player.position);
					}
				}
			}
		}
	}
}
