using Honeymoon.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Honeymoon.Source.World.Creatures
{
	public class InputComponent
	{
		private float stepDelay;
		private bool specialAction;
		public void UpdateBasicMovement(float elapsed)
		{
			Globals.player.oldPosition = Globals.player.position;
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
			else
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
		public void Update(float elapsed)
		{
			if (!specialAction)
			{
				UpdateBasicMovement(elapsed);
			}
			if (InputManager.IsKeyDown(Keys.F1))
			{
				Globals.player.position = new Vector2(941, 5154);
			}
			else if (InputManager.IsKeyDown(Keys.F2))
			{
				Globals.player.position = new Vector2(2345, 500);
			}
		}
		private void DrawBasicMovement()
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
			else if (Globals.player.direction == "TopRight" || Globals.player.direction == "DownRight")
			{
				Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkRight, false);
			}
			else if (Globals.player.direction == "TopLeft" || Globals.player.direction == "DownLeft")
			{
				Globals.animationManager.DrawAnimationSet(Globals.animationManager.walkRight, true);
			}
		}
		public virtual void DrawPlayerAnimations()
		{
			if (!specialAction)
			{
				DrawBasicMovement();
			}
		}
	}
}
