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

           // Owner.SetTextureDeath();
        }

        public override void Exit()
        {
            LogOfState("exit on state death");
        }

        public override void Update()
        {
            Owner.CurrentPosition = new Vector2(Owner.CurrentPosition.X, Owner.CurrentPosition.Y + 1 * 10);

            if (Owner.CurrentColor.A > 0)
            {
                byte newAlpha = (byte)MathHelper.Clamp(Owner.CurrentColor.A - 1 * 4, 0, 255);

                Owner.SetAlphaColorIntensity(newAlpha);
            }

            else
            {

                Debug.WriteLine("duck remove");

                Owner.CallEventRemove();
            }

            float angle = MathHelper.Clamp(Owner.Angle + 0.1f * 1.6f, 0, 360);

            Owner.SetAngleRotation(angle);
        }
    }
}
