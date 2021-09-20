using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseEngine.Models.DuckModel.States
{
   public class DuckStateDeath : DuckStateBase
    {
        public override void Enter()
        {
            LogOfState("enter on ste death");

            Owner.SetTextureDeath();
        }

        public override void Exit()
        {
            LogOfState("exit on state death");
        }

        public override void Update()
        {
            Owner.CurrentPosition = new Vector2(Owner.CurrentPosition.X, Owner.CurrentPosition.Y + 1 * 10);

            if (Owner.CurrentPosition.Y > Constants.SCREEN_HEIGHT)
            {
                Debug.WriteLine("duck remove");

                Owner.CallEventRemove();
            }
        }
    }
}
