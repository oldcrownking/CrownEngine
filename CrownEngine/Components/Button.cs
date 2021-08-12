using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CrownEngine
{
    public class Button : Component
    {
        public ButtonState desiredButton;

        public bool pressed;

        public Button(ButtonState mouseButton, Actor myActor) : base(myActor)
        {
            desiredButton = mouseButton;
        }

        public override void Update()
        {
            if (EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed && myActor.rect.Contains(EngineGame.instance.mouseState.Position.ToVector2() / EngineGame.instance.windowScale))
            {
                pressed = true;
            }
            else
            {
                pressed = false;
            }

            base.Update();
        }
    }
}
