using BaseEngine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace BaseEngine.Models
{
    public class Background : DrawableObjectBase, IDrawableObject
    {

        public void Load(ContentManager content)
        {            
            Random random = new Random();

            int indexBackground = random.Next(1, Constants.COUNT_VARIANTS_BACKGROUND + 1);

            LoadTexture($"{Constants.PATH_RESOURCES_IMAGES}background{indexBackground}", content);
        }


        public void Start()
        {
            Debug.WriteLine("background loaded");
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle = new Rectangle(0, 0, Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT);

            spriteBatch.Draw(Texture, Vector2.Zero, rectangle, Color.White);

        }

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }
}
