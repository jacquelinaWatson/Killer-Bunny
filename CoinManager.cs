using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Killer_Bunny
{
    class CoinManager
    {
        Texture2D coinTexture; //Texture representing the coin
        static public List<Coin> coins = new List<Coin>(); //List to store all coins in game
        Random random = new Random(); //variable to store a random number 
        public bool Active;
        SoundEffect sound;
        public int coinQUantity = 0; 

        //Initialize the coin manager with a texture
        public void Initialize(Texture2D texture, SoundEffect sound)
        {
            coinTexture = texture; //texture becomes the coin texture

            for (int i = 0; i < 30; i++) //For 30 coins
            {
                if (TileMap.GetTileAtPixel(random.Next(0, 1500), random.Next(0, 1500)) == 0) //If function returns 0
                {
                    AddCoin(); //Call the add coin method
                }
            }

            Active = true;

            this.sound = sound;

        }

        //Method to add a coin to the game world
        private void AddCoin()
        {

            Vector2 CoinPosition = new Vector2(random.Next(0, 1500), random.Next(0, 1500));
            Animation CoinAnimation = new Animation(); //Create a new animation for the coin
            CoinAnimation.Initialize(coinTexture, CoinPosition, 64, 64, 8, 150, Color.White, true); //Initialize the coins animation
            Coin coin = new Coin(); //Create a new instance of the coin class
            coin.Initialize(CoinAnimation, CoinPosition, 10); //Initialize a coin with an animation, position and value
            coins.Add(coin); //Add the newly initialized coin to the list
        }

        //Method to handle the collision between coin and player
        public void CoinCollision(Player player)
        {
            //Rectangles for each object to see if they are overlapping
            Rectangle PlayRec, CoinRec;

            //Create the player rectangle
            PlayRec = new Rectangle(
                (int)player.Position.X,
                (int)player.Position.Y,
                player.Width, player.Height);
            //Create the coin rectangle

            for (int i = 0; i < coins.Count; i++)
            {
                CoinRec = new Rectangle(
                    (int)coins[i].CoinPosition.X,
                    (int)coins[i].CoinPosition.Y,
                    coins[i].Width, coins[i].Height);

                //Check for the collision
                if (PlayRec.Intersects(CoinRec))
                {
                    sound.Play();
                    coins[i].Active = false;
                    coinQUantity++;
                }

            }
        }

        //Method to update all of the coins in the game world
        public void Update(GameTime gameTime, Player player)
        {
            CoinCollision(player);

            for (int i = 0; i < coins.Count; i++)
            {
                coins[i].Update(gameTime);

                if (coins[i].Active == false) //If the coin is no longer active
                {
                    coins.RemoveAt(i); //Remove the coin from the list
                }

            }

        }

        //Method to draw all of the coins in the game world
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < coins.Count; i++) //For all the coins in the list
            {
                coins[i].Draw(spriteBatch); //Draw all of the coins in the list

            }
        }
    }
}