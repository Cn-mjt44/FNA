#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System;
using System.Drawing;

#endregion

namespace Microsoft.Xna.Framework.Input
{
	public static class TextInputEXT
	{
		private static Rectangle _InputRectangle;
		#region Event

		/// <summary>
		/// Use this event to retrieve text for objects like textboxes.
		/// This event is not raised by noncharacter keys.
		/// This event also supports key repeat.
		/// For more information this event is based off:
		/// http://msdn.microsoft.com/en-AU/library/system.windows.forms.control.keypress.aspx
		/// </summary>
		public static event Action<int> TextInput;

		/// <summary>
		/// This event notifies you of in-progress text composition happening in an IME or other tool
		///  and allows you to display the draft text appropriately before it has become input.
		/// For more information, see SDL's tutorial: https://wiki.libsdl.org/Tutorials-TextInput
		/// </summary>
		public static event Action<string, int, int> TextEditing;

		#endregion

		#region Public Properties

		public static IntPtr WindowHandle
		{
			get;
			set;
		}

		public static bool IsTextInputActive
		{
			get => FNAPlatform.IsTextInputActive(WindowHandle);
			set
			{
				if (value != IsTextInputActive)
				{
					if(value)
					{
						FNAPlatform.StartTextInput(WindowHandle);
					}
					else
					{
						FNAPlatform.StopTextInput(WindowHandle);
					}
				}
			}
		}

		public static bool IsScreenKeyboardShown => FNAPlatform.IsScreenKeyboardShown(WindowHandle);


		/// <summary>
		/// Sets the location within the game window where the text input is located.
		/// This is used to set the location of the IME suggestions
		/// </summary>
		/// <param name="rectangle">Text input location relative to GameWindow.ClientBounds</param>
		public static Rectangle InputRectangle
		{
			get => _InputRectangle;
			set
			{
				if (value != _InputRectangle)
				{
					FNAPlatform.SetTextInputRectangle(WindowHandle, value);
				}
			}
		}

		public static string ClipboardText
		{
			get
			{
				if (FNAPlatform.HasClipboardText())
					return FNAPlatform.GetClipboardText();
				else
					return string.Empty;
			}
			set => FNAPlatform.SetClipboardText(value);
		}

		public static string PrimarySelectionText
		{
			get
			{
				if (FNAPlatform.HasPrimarySelectionText())
					return FNAPlatform.GetPrimarySelectionText();
				else
					return string.Empty;
			}
			set => FNAPlatform.SetPrimarySelectionText(value);
		}

		#endregion

		#region Internal Event Access Method

		internal static void OnTextInput(int codepoint)
		{
			if (TextInput != null)
			{
				TextInput(codepoint);
			}
		}

		internal static void OnTextEditing(string text, int start, int length)
		{
			if (TextEditing != null)
			{
				TextEditing(text, start, length);
			}
		}

		#endregion
	}
}
