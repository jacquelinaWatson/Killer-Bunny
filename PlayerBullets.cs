using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Killer_Bunny
{
    class PlayerBullets
    {
        #region Player Bullet Declerations

        public Animation pistolAnimationRight, pistolAnimationLeft, pistolAnimationUp, pistolAnimationDown; //Represents the bullet animation

        const float bulletMoveSpeed=25f; //speed at which the bullet travels
        public Vector2 Position; //the position of the bullet

        int Direction; //The direction of the bullet
        int Damage = 10; //the damage the bullet deals
        public bool Active; //set the bullet to active
        int PistolRange; //the range of the bullet

        public int Width //the width of the bullet image
        {
            get { return pistolAnimationRight.frameWidth; }
        }

        public int Height
        {
            get { return pistolAnimationRight.frameHeight; }
        }

        #endregion

        public void Initialize(Animation animationRight, Animation animationLeft, Animation animationUp, Animation animationDown, Vector2 position, int direction)
        {
            pistolAnimationRight = animationRight;
            pistolAnimationLeft = animationLeft;
            pistolAnimationUp = animationUp;
            pistolAnimationDown = animationDown;

            Direction = direction;

            Position = position;

            Active = true;
        }



        public void Update(GameTime gameTime)
        {
            //Update bullets based on character position
            if (Direction == 1) //If player is facing left
            {
                Position.X -= bulletMoveSpeed;
                pistolAnimationLeft.Position = Position;
                pistolAnimationLeft.Update(gameTime);

            }
            else if (Direction == 2) //If player is facing right
            {

                Position.X += bulletMoveSpeed;
                pistolAnimationRight.Position = Position;
                pistolAnimationRight.Update(gameTime);

            }
            else if (Direction == 3) //If player is facing up
            {
                Position.Y += bulletMoveSpeed;
                pistolAnimationDown.Position = Position;
                pistolAnimationDown.Update(gameTime);

            }
            else if (Direction == 4) //If player is facing down
            {
                Position.Y -= bulletMoveSpeed;
                pistolAnimationUp.Position = Position;
                pistolAnimationUp.Update(gameTime);

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pistolAnimationRight.Draw(spriteBatch);
            pistolAnimationLeft.Draw(spriteBatch);
            pistolAnimationUp.Draw(spriteBatch);
            pistolAnimationDown.Draw(spriteBatch);
        }
    }
}
