using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PxlRhthm
{
    class Number
    {
        public Texture2D NumberStrip;

        public Vector2 Position;

        int FrameCount;

        int CurrentFrame;

        Color color;

        Rectangle sourceRect = new Rectangle();

        Rectangle destinationRect = new Rectangle();

        public int FrameWidth;

        public int FrameHeight;

        public void Initialize(Texture2D texture, Vector2 position,
            int frameWidth, int frameHeight, int frameCount,
            Color color)
        {
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.FrameCount = frameCount;
            Position = position;
            NumberStrip = texture;
            CurrentFrame = 0;
        }

        public void Update(GameTime gameTime, int scoreIncrease)
        {
            CurrentFrame += scoreIncrease;

            if (CurrentFrame == FrameCount)
                CurrentFrame = 0;

            sourceRect = new Rectangle(CurrentFrame * FrameWidth,
                0, FrameWidth, FrameHeight);

            destinationRect = new Rectangle((int)Position.X, (int)Position.Y,
                FrameWidth, FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(NumberStrip, destinationRect, sourceRect, color);
        }
    }
}
