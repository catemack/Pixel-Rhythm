using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PxlRhthm
{
    class Pixel
    {
        public Texture2D PixelTexture;

        public Vector2 Position;

        public bool Active;

        public int Value;

        public int Width
        {
            get { return PixelTexture.Width; }
        }

        public int Height
        {
            get { return PixelTexture.Height; }
        }

        public int pixelMoveSpeed;

        public void Initialize(Texture2D texture, Vector2 position)
        {
            PixelTexture = texture;

            Position = position;

            Active = true;

            pixelMoveSpeed = 1;

            Value = 1;
        }

        public void Update(GameTime gameTime)
        {
            Position.Y += pixelMoveSpeed;

            if (Position.Y > 320)
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PixelTexture, Position, null, Color.White,
                0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
