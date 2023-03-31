using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Honeymoon.Managers
{
	public class InputManager
	{
		public static KeyboardState lastKeyboardState;
		public static KeyboardState currentKeyboardState;
		private static bool[] lastMouseButtonStates = { false, false };
		private static bool[] mouseButtonStates = { false, false }; // left | right

		#region keyInput
		public static void SetCurrentStates(KeyboardState states)
		{
			lastKeyboardState = currentKeyboardState;
			currentKeyboardState = states;
		}

		public static bool IsKeyNewlyPressed(Keys key)
		{
			if (lastKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key))
				return true;
			return false;
		}

		public static bool IsLastPressedKeyUp(Keys key)
		{
			if (lastKeyboardState.IsKeyUp(key))
				return true;
			return false;
		}

		public static bool IsKeyDown(Keys key)
		{
			if (currentKeyboardState.IsKeyDown(key))
				return true;
			return false;
		}

		public static bool AreKeysDown(Keys[] keys)
		{
			foreach (Keys key in keys)
				if (currentKeyboardState.IsKeyDown(key))
					return true;
			return false;
		}

		public static List<Keys> GetNewlyPressedKeys()
		{
			Keys[] keys = currentKeyboardState.GetPressedKeys();
			if (keys.Length == 0)
				return null;

			List<Keys> result = new List<Keys>();
			foreach (Keys key in keys)
			{
				if (lastKeyboardState.IsKeyUp(key))
					result.Add(key);
			}
			return result;
		}
		#endregion
		#region mouseInput
		public static void SetCurrentStates(ButtonState[] states)
		{
			for (int i = 0; i < 2; i++)
			{
				lastMouseButtonStates[i] = mouseButtonStates[i];
				mouseButtonStates[i] = states[i] == ButtonState.Pressed;
			}
		}

		public static bool IsLeftButtonReleased()
		{
			return lastMouseButtonStates[0] && !mouseButtonStates[0];
		}

		public static bool IsLeftButtonDown()
		{
			return mouseButtonStates[0];
		}

		public static bool IsRightButtonDown()
		{
			return mouseButtonStates[1];
		}

		public static bool AreTwoButtonsDown()
		{
			return mouseButtonStates[0] && mouseButtonStates[1];
		}

		public static bool IsLeftButtonNewlyPressed()
		{
			return !lastMouseButtonStates[0] && mouseButtonStates[0];
		}

		public static bool IsRightButtonNewlyPressed()
		{
			return !lastMouseButtonStates[1] && mouseButtonStates[1];
		}
		#endregion
	}
}
