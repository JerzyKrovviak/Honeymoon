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
		private static KeyboardState lastKeyboardState;
		private static KeyboardState currentKeyboardState;
		private static bool[] lastMouseButtonStates = { false, false };
		private static bool[] mouseButtonStates = { false, false }; // left | right
		public static Keys[] specialChars = new Keys[] { Keys.Tab, Keys.CapsLock, Keys.LeftShift, Keys.LeftControl, Keys.LeftWindows, Keys.LeftAlt, Keys.RightAlt, Keys.RightAlt, Keys.RightShift, Keys.RightControl, Keys.Enter, Keys.Insert, Keys.Delete, Keys.End, Keys.Home, Keys.PageDown, Keys.PageUp, Keys.NumLock, Keys.PrintScreen, Keys.Pause, Keys.Back };
		public static Keys[] normalChars = new Keys[] { Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z, };

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
