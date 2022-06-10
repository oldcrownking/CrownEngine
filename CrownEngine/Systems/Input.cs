using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using Microsoft.Xna.Framework.Input;

namespace CrownEngine.Systems
{
    public class Input : GameSystem
    {
        public static Input Instance { get; private set; }

        public KeyboardState oldKeyboardState;
        public KeyboardState keyboardState;

        public MouseState oldMouseState;
        public MouseState mouseState;

        public Vector2 oldMousePos;
        public Vector2 mousePos;

        public Input() : base()
        {

        }

        public override void Update()
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            mousePos = (mouseState.Position.ToVector2() / EngineGame.instance.windowScale);
            oldMousePos = (oldMouseState.Position.ToVector2() / EngineGame.instance.windowScale);
        }

        public bool KeyJustPressed(Keys key)
        {
            if (oldKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyJustReleased(Keys key)
        {
            if (oldKeyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool IsKeyDown(Keys key)
        {
            if (keyboardState.IsKeyDown(key))
                return true;
            return false;
        }
    }
}
