using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PxlRhthm
{
    class Barrier
    {
        public Texture2D BarrierTexture;

        public Vector2 Position;

        public int Width
        {
            get { return BarrierTexture.Width; }
        }

        public int Height
        {
            get { return BarrierTexture.Height; }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            BarrierTexture = texture;

            Position = position;
        }
        
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BarrierTexture, Position, null, Color.White,
                0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
