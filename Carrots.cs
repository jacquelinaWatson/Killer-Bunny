using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Killer_Bunny
{
    class Carrots
    {
        Texture2D carrotTexture;
        Vector2 carrotPosition;
        public bool Active;
        Animation carrotAnimation;

        public void Initialize(Texture2D texture, Vector2 position)
        {
            this.carrotTexture = texture;
            this.carrotPosition = position;
            Active = true;

            carrotAnimation = new Animation();
            carrotAnimation.Initialize(carrotTexture, carrotPosition, 64, 64, 1, 0, Color.White, true);
        }

        public void Collisions(Player player)
        {
            Rectangle PlayRec, CarrotRec;

            PlayRec = new Rectangle(
                (int)player.Position.X,
                (int)player.Position.Y,
                player.Width, player.Height);

            CarrotRec = new Rectangle(
                (int)carrotPosition.X,
                (int)carrotPosition.Y,
                carrotAnimation.frameWidth, carrotAnimation.frameWidth);

            if (PlayRec.Intersects(CarrotRec))
            {
                Active = false;
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
            Collisions(player);
            carrotAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            carrotAnimation.Draw(spriteBatch);
        }
    }
}


