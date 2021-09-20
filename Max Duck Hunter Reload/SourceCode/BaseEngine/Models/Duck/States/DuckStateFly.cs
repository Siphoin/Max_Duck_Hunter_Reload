using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace BaseEngine.Models.DuckModel.States
{
    public class DuckStateFly : DuckStateBase
    {
        public override void Enter()
        {
            LogOfState("enter on state fly");

            Owner.SubcribeMouseDownEvent();
        }

        public override void Exit()
        {
            LogOfState("exit on state fly");

            Owner.UncribeMouseDownEvent();
        }

        public override void Update()
        {
            float nextValueX = Owner.Direction == DuckDirection.Right ? 1 : -1;

            Owner.CurrentPosition = new Vector2(Owner.CurrentPosition.X + nextValueX * 2, Owner.CurrentPosition.Y);

            if (Owner.Direction == DuckDirection.Right)
            {
                if (Owner.CurrentPosition.X > Constants.SCREEN_WIDTH)
                {
                    Debug.WriteLine("duck remove");

                    Owner.CallEventExitDuckOnScreen();
                }
            }

        }
    }
}
