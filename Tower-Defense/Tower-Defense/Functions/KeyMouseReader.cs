using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Input;

namespace Tower_Defense
{
    static class KeyMouseReader
    {
        private static KeyboardState
            myCurrentKeyState,
            myPreviousKeyState = Keyboard.GetState();
        private static MouseState
            myCurrentMouseState,
            myPreviousMouseState = Mouse.GetState();

        public static bool KeyPressed(Keys aKey)
        {
            return myCurrentKeyState.IsKeyDown(aKey) && myPreviousKeyState.IsKeyUp(aKey);
        }
        public static bool KeyHold(Keys aKey)
        {
            return myCurrentKeyState.IsKeyDown(aKey);
        }
        public static KeyboardState CurrentKeyState
        {
            get => myCurrentKeyState;
        }
        public static KeyboardState PreviousKeyState
        {
            get => myPreviousKeyState;
        }

        public static bool MiddleMouseClick()
        {
            return myCurrentMouseState.MiddleButton == ButtonState.Pressed && myPreviousMouseState.MiddleButton == ButtonState.Released;
        }
        public static bool LeftClick()
        {
            return myCurrentMouseState.LeftButton == ButtonState.Pressed && myPreviousMouseState.LeftButton == ButtonState.Released;
        }
        public static bool RightClick()
        {
            return myCurrentMouseState.RightButton == ButtonState.Pressed && myPreviousMouseState.RightButton == ButtonState.Released;
        }

        public static bool MiddleMouseHold()
        {
            return myCurrentMouseState.MiddleButton == ButtonState.Pressed;
        }
        public static bool LeftHold()
        {
            return myCurrentMouseState.LeftButton == ButtonState.Pressed;
        }
        public static bool RightHold()
        {
            return myCurrentMouseState.RightButton == ButtonState.Pressed;
        }

        public static bool ScrollUp()
        {
            return myCurrentMouseState.ScrollWheelValue > myPreviousMouseState.ScrollWheelValue;
        }
        public static bool ScrollDown()
        {
            return myCurrentMouseState.ScrollWheelValue < myPreviousMouseState.ScrollWheelValue;
        }

        public static MouseState CurrentMouseState
        {
            get => myCurrentMouseState;
        }
        public static MouseState PreviousMouseState
        {
            get => myPreviousMouseState;
        }

        //Should be called at beginning of Update in Game
        public static void Update()
        {
            myPreviousKeyState = myCurrentKeyState;
            myCurrentKeyState = Keyboard.GetState();

            myPreviousMouseState = myCurrentMouseState;
            myCurrentMouseState = Mouse.GetState();
        }
    }
}