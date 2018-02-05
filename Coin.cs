using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Killer_Bunny
{
    class Coin
    {
        //Values related to the basic creation of a coin
        public Animation CoinAnimation; //Animation representing coin
        public Vector2 CoinPosition; //Coins position in the game
        public bool Active; //Is the coin active in the game?
        public int CoinValue; //How much is the coin worth?
        

        public int Width //Width of the coins animation
        { get { return CoinAnimation.frameWidth; } }

        public int Height //Height of the coins animation
        { get { return CoinAnimation.frameWidth; } }

        //Initialize the coin with an animation, position and value
        public void Initialize(Animation animation, Vector2 position, int value)
        {
            CoinAnimation = animation; //animation becomes coin animation
            CoinPosition = position; // position becomes coin position
            Active = true; //Starts off active
            this.CoinValue = value; //The value initialized with becomes the coinValue
        }

        public void Update(GameTime gameTime)
        {
            CoinAnimation.Update(gameTime); //Update the animation so it is active
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CoinAnimation.Draw(spriteBatch); //Draw the coin animation so it is visible
        }
    }
}