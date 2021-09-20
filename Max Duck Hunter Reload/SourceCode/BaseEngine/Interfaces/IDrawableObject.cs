using Microsoft.Xna.Framework.Graphics;

namespace BaseEngine.Interfaces
{
    public interface IDrawableObject : IInteractorObject
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
