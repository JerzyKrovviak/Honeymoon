using Honeymoon.Managers;
using Honeymoon.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honeymoon.Source.Menus
{
	public class TextInput
	{
		public string text;
		public Vector2 position;
		public Rectangle inGameData, sourceData;
		public bool isFocused, capslockOn;
		public float scale;
		public Color color = Color.Black;
		public int maxChars;
		public Texture2D label;

		public TextInput(string text, Vector2 position)
		{
			this.text = text;
			this.position = position;
			label = Globals.content.Load<Texture2D>("MiscSprites/hm_uiElements");
			sourceData = new Rectangle(0,64,47,12);
			inGameData.Width = sourceData.Width * 5;
			inGameData.Height = sourceData.Height * 5;
			scale = 1.4f;
			maxChars = 10;
		}

		public string GetLastKeysToString()
		{
			var keys = Keyboard.GetState().GetPressedKeys();
			string lastKey = "";

			if (keys.Count() > 0)
			{
				if (InputManager.IsKeyNewlyPressed(keys[keys.Length - 1]))
				{
					AudioManager.soundBank.PlayCue("optionHover");
				}

				if (InputManager.IsKeyNewlyPressed(Keys.D0))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += ')' : lastKey += '0';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D1))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '!' : lastKey += "1";
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D2))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '@' : lastKey += '2';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D3))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '#' : lastKey += '3';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D4))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '$' : lastKey += '4';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D5))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '%' : lastKey += '5';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D6))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '^' : lastKey += '6';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D7))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '&' : lastKey += '7';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D8))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '*' : lastKey += '8';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D9))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += '(' : lastKey += '9';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.A))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'A' : capslockOn ? lastKey += 'A' : lastKey += 'a';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.B))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'B' : capslockOn ? lastKey += 'B' : lastKey += 'b';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.C))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'C' : capslockOn ? lastKey += 'C' : lastKey += 'c';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.D))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'D' : capslockOn ? lastKey += 'D' : lastKey += 'd';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.E))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'E' : capslockOn ? lastKey += 'E' : lastKey += 'e';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.F))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'F' : capslockOn ? lastKey += 'F' : lastKey += 'f';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.G))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'G' : capslockOn ? lastKey += 'G' : lastKey += 'g';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.H))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'H' : capslockOn ? lastKey += 'H' : lastKey += 'h';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.I))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'I' : capslockOn ? lastKey += 'I' : lastKey += 'i';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.J))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'J' : capslockOn ? lastKey += 'J' : lastKey += 'j';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.K))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'K' : capslockOn ? lastKey += 'K' : lastKey += 'k';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.L))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'L' : capslockOn ? lastKey += 'L' : lastKey += 'l';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.M))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'M' : capslockOn ? lastKey += 'M' : lastKey += 'm';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.N))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'N' : capslockOn ? lastKey += 'N' : lastKey += 'n';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.O))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'O' : capslockOn ? lastKey += 'O' : lastKey += 'o';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.P))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'P' : capslockOn ? lastKey += 'P' : lastKey += 'p';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.Q))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'Q' : capslockOn ? lastKey += 'Q' : lastKey += 'q';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.R))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'R' : capslockOn ? lastKey += 'R' : lastKey += 'r';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.S))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'S' : capslockOn ? lastKey += 'S' : lastKey += 's';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.T))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'T' : capslockOn ? lastKey += 'T' : lastKey += 't';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.U))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'U' : capslockOn ? lastKey += 'U' : lastKey += 'u';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.V))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'V' : capslockOn ? lastKey += 'V' : lastKey += 'v';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.W))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'W' : capslockOn ? lastKey += 'W' : lastKey += 'w';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.X))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'X' : capslockOn ? lastKey += 'X' : lastKey += 'x';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.Y))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'Y' : capslockOn ? lastKey += 'Y' : lastKey += 'y';
				}
				if (InputManager.IsKeyNewlyPressed(Keys.Z))
				{
					return InputManager.IsKeyDown(Keys.RightShift) ? lastKey += 'Z' : capslockOn ? lastKey += 'Z' : lastKey += 'z';
				}
			}
			return lastKey;
		}

		public virtual void UpdateTextInput()
		{
			if (text.Length > 0)
			{
				int count = 0;
				foreach (char c in text)
				{
					if (Char.IsUpper(c))
						count++;
				}

				if (count >= 3)
				{
					maxChars = 7;
				}
				else
					maxChars = 12;
			}

			if (inGameData.Contains(Globals.mousePosition))
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					AudioManager.soundBank.PlayCue("optionHover");
					isFocused = true;
				}
			}
			else
			{
				if (InputManager.IsLeftButtonNewlyPressed())
				{
					isFocused = false;
				}
			}

			if (isFocused)
			{
				if (InputManager.IsKeyNewlyPressed(Keys.Back))
				{
					if (text.Length > 0)
					{
						text = text.Remove(text.Length - 1);
						AudioManager.soundBank.PlayCue("optionHover");
					}
				}
				else if (text.Length <= maxChars)
				{
					text += GetLastKeysToString();
				}
			}

			if (InputManager.IsKeyNewlyPressed(Keys.CapsLock))
			{
				capslockOn = !capslockOn;
			}
			inGameData.X = (int)position.X;
			inGameData.Y = (int)position.Y;
		}

		public virtual void DrawTextInput()
		{
			Globals.spriteBatch.Draw(label, inGameData, sourceData, Color.White);
			Globals.spriteBatch.DrawString(FontManager.hm_f_default, text, new Vector2(position.X + 10, position.Y + 13), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
			if (capslockOn && isFocused)
			{
				Globals.spriteBatch.Draw(label, new Rectangle(inGameData.X - 30, inGameData.Y + 8, 28, 48), new Rectangle(47, 64, 7, 12), Color.White);
			}
		}
	}
}
