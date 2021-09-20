using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BaseEngine.Models
{
    public abstract class DrawableObjectBase
    {
        protected Texture2D Texture { get; set; }

        protected Rectangle Rectangle { get; set; }

        protected void LoadTexture(string path, ContentManager content) => Texture = content.Load<Texture2D>(path);
    }
}
