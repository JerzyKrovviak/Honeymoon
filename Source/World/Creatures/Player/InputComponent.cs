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
using static Honeymoon.Source.World.PhysicalComponent;

namespace Honeymoon.Source.World.Creatures
{
	public class InputComponent
	{
		private float stepDelay;
		public void BasicMovement(float elapsed)
		{
			if (InputManager.IsKeyDown(Keys.A))
			{
				Globals.player.velocity.X = -Globals.player.speed * elapsed;
				Globals.player.direction = "left";
				Globals.animationManager.PlayAnimationSet(Globals.animationManager.walkRight);
			}
			else if (InputManager.IsKeyDown(Keys.D))
			{
				Globals.player.velocity.X = Globals.player.speed * elapsed;
				Globals.player.direction = "right";
				Globals.animationManager.PlayAnimationSet(Globals.animationManager.walkRight);
			}
			if (InputManager.IsKeyDown(Keys.W))
			{
				Globals.player.velocity.Y = -Globals.player.speed * elapsed;
				Globals.player.direction = "up";
				Globals.animationManager.PlayAnimationSet(Globals.animationManager.walkUp);
			}
			else if (InputManager.IsKeyDown(Keys.S))
			{
				Globals.player.velocity.Y = Globals.player.speed * elapsed;
				Globals.player.direction = "down";
				Globals.animationManager.PlayAnimationSet(Globals.animationManager.walkDown);
			}
			Globals.player.HandleCollisions();
			Globals.player.position += Globals.player.velocity;
			Globals.player.velocity = Vector2.Zero;

			if (InputManager.IsKeyDown(Keys.W) && InputManager.IsKeyDown(Keys.D))
			{
				Globals.player.direction = "TopRight";
			}
			else if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.D))
			{
				Globals.player.direction = "DownRight";
			}
			else if (InputManager.IsKeyDown(Keys.S) && InputManager.IsKeyDown(Keys.A))
			{
				Globals.player.direction = "DownLeft";
			}
			else if (InputManager.IsKeyDown(Keys.A) && InputManager.IsKeyDown(Keys.W))
			{
				Globals.player.direction = "TopLeft";
			}

			if (InputManager.AreKeysDown(new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D }))
			{
				float delay = 0;
				if (Globals.player.direction == "left" || Globals.player.direction == "right")
				{
					delay = 350;
				}
				else if (Globals.player.direction == "up" || Globals.player.direction == "down")
				{
					delay = 300;
				}
				else
				{
					delay = 350;
				}

				stepDelay += elapsed;
				if (stepDelay >= delay)
				{
					AudioManager.soundBank.PlayCue(Globals.player.GetTileStepSound((int)Globals.player.GetEntityHitboxTile().X, (int)Globals.player.GetEntityHitboxTile().Y));
					stepDelay = 0;
				}
			}
		}
		public void Update(float elapsed)
		{
			Globals.player.oldPosition = Globals.player.position;
			BasicMovement(elapsed);
			if (InputManager.IsKeyDown(Keys.F1))
			{
				Globals.player.position = new Vector2(941, 5154);
			}
			else if (InputManager.IsKeyDown(Keys.F2))
			{
				Globals.player.position = new Vector2(2345, 2393);
			}

			if (!InputManager.AreKeysDown(new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D }))
			{
				if (Globals.player.direction == "up")
				{
					Globals.animationManager.StopAnimationSet(Globals.animationManager.walkUp);
				}
				else if (Globals.player.direction == "down")
				{
					Globals.animationManager.StopAnimationSet(Globals.animationManager.walkDown);
				}
				else if (Globals.player.direction == "right" || Globals.player.direction == "left")
				{
					Globals.animationManager.StopAnimationSet(Globals.animationManager.walkRight);
				}
			}
		}

		public virtual void DrawPlayerAnim()
		{
			if (InputManager.IsKeyDown(Keys.A))
			{
				Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkRight, true);
			}
			else if (InputManager.IsKeyDown(Keys.D))
			{
				Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkRight, false);
			}
			else if (InputManager.IsKeyDown(Keys.W))
			{
				Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkUp, false);
			}
			else if (InputManager.IsKeyDown(Keys.S))
			{
				Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkDown, false);
			}

			if (!InputManager.AreKeysDown(new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D }))
			{
				if (Globals.player.direction == "up")
				{
					Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkUp, false);
				}
				else if (Globals.player.direction == "down")
				{
					Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkDown, false);
				}
				else if (Globals.player.direction == "right")
				{
					Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkRight, false);
				}
				else if (Globals.player.direction == "left")
				{
					Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkRight, true);
				}
			}
		}
	}
}
