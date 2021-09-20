using Microsoft.Xna.Framework;
using System;

namespace BaseEngine.Models
{
    public  class MouseInput
    {
        public event Action<int, Vector2> OnMouseDown;

        public MouseInput(BaseEngine baseEngine) => baseEngine.OnMouseDown += CallEventMouseDown;

        private void CallEventMouseDown(int indexMouse, Vector2 mousePosition) => OnMouseDown?.Invoke(indexMouse, mousePosition);
    }
}
