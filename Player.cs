using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Killer_Bunny
{
    class Player //JACKIES CLASS DO NOT TOUCH PLS THIS IS VERY IMPORTANT STUFF FOR ME SO DONT TOUCH PLS THANK YOU LOL BYES
    {
        #region Player Declerations

        //Player
        Rectangle bulletRectangle; //Rectangle to store the bullet
        Rectangle PlayerRec; //Rectangle to store the player
        Texture2D walkRight, walkLeft, walkUp, walkDown, death; //Player textures
        public Vector2 Position; //Position of the player relative to the upper left side of the screen
        public bool Active; //State of the player
        public int Health = 100; //Amount of hitpoints the player has
        float playerMoveSpeed; //Movement speed for the player
        public Animation playerAnimation, right, left, up, down, dying; //Player animations
        public static int playerState;//What direction the player is facing
        int worldHeight = 1500; //Total height of the game world
        int worldWidth = 1500; //Total width of the game world
        SoundEffect hurt, RIP;

        //Width and Height of the current frame
        public int Width
        {
            get { return playerAnimation.frameWidth; } //returns the framewidth of the current animation
        }

        public int Height
        {
            get { return playerAnimation.frameHeight; } //returns the height of the current animation
        }

        //Graphics
        Vector2 graphicsInfo; //Holds the viewport

        //Keyboard states to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        //Gamepad states to determine button presses
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        //Mouse states to track Mouse button press
        MouseState currentMouseState;
        MouseState previousMouseState;

        GUI GUI;
        #endregion


        //Player Constructor (currentanimation, textures, position, graphical infomation) 
        public void Initialize(Animation playerAnimation, Texture2D death, Texture2D walkRight, Texture2D walkLeft, Texture2D walkUp, Texture2D walkDown, Vector2 position, Vector2 grInfo, int playerState, GUI gui, SoundEffect hurt, SoundEffect RIP)
        {
            this.walkRight = walkRight; //Spritestrip for walking right
            this.walkLeft = walkLeft; //Spritestrip walking left
            this.walkUp = walkUp; //Spritestrip for walking up
            this.walkDown = walkDown; //Spritestrip for walking down
            this.death = death; 
            Position = position; //Starting position of the player
            Active = true; //The player is active
            Health = 100; //The initial player health is 100
            graphicsInfo = grInfo; //Sets the player viewport
            playerMoveSpeed = 3.0f; //Constant playermovespeed
            playerState = 2;
            GUI = gui;
            this.playerAnimation = playerAnimation; //Player animation
            this.hurt = hurt; //hurt sound
            this.RIP = RIP; //death sound
            

            playerAnimation.Initialize(walkRight, Vector2.Zero, 64, 64, 3, 150, Color.White, true);
            //Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, bool looping
            left = new Animation(); //Animation becomes left animation
            left.Initialize(walkLeft, Vector2.Zero, 64, 64, 3, 150, Color.White, true);
            right = new Animation(); //PAnimation becomes right animation
            right.Initialize(walkRight, Vector2.Zero, 64, 64, 3, 150, Color.White, true);
            up = new Animation(); //Animation becomes up animation
            up.Initialize(walkUp, Vector2.Zero, 64, 64, 3, 150, Color.White, true);
            down = new Animation(); //Animation becomes down animation
            down.Initialize(walkDown, Vector2.Zero, 64, 64, 3, 150, Color.White, true);
            dying = new Animation(); //Animation becomes death animation
            dying.Initialize(death, Vector2.Zero, 64, 64, 3, 70, Color.White, true);

        }
        public Vector2 GetPosition
        {
            get { return Position; }
            set { Position = value; }
        }
        public void Update(GameTime gameTime)
        {
            //Save the previous state of the keyboard and game pad so we can determine single key/button presses
            previousGamePadState = currentGamePadState; //Previous state becomes current state (GamePad)
            previousKeyboardState = currentKeyboardState; //Previous state becomes current state (Keyboard)

            //Read the current state of the keyboard and gamepad and store it
            currentGamePadState = GamePad.GetState(PlayerIndex.One); //Get state of player ones gamepad
            currentKeyboardState = Keyboard.GetState(); //Get the state of the keyboard and store

            // Read the current state of the Mouse  and store it
            previousMouseState = currentMouseState; //Previous state becomes current state(Mouse)
            currentMouseState = Mouse.GetState(); //Get state of mouse and store
            //Get Mouse State then Capture the Button type and Respond Button Press
            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            //Mouse moves to coordinates of point clicked onscreen


            //Get Thumbsticks Controls
            Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed; //Moves up and down at playerspeed
            Position.Y += currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed; //Moves left and right at playerspeed

            //Use the Keyboard/DPad
            //Checks if the left key or left button is being pressed
            if (currentKeyboardState.IsKeyDown(Keys.A) || currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                if (Position.X < 0) //If the x position goes beyond zero
                {
                    Position.X = 0; //Then reset the x position to zero
                }
                else //If the position is not beyond 0
                {
                    playerAnimation = left;
                    Position.X -= playerMoveSpeed; //Move the player left at a speed
                   playerAnimation.frameTime = 150;
                    playerState = 1;

                }
            }

            //Checks if the right key or right button is being pressed
            else if (currentKeyboardState.IsKeyDown(Keys.D) || currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                if (Position.X > worldWidth) //If the x position goes beyond 750
                {
                    Position.X = worldWidth; //Reset the x position to 750

                }
                else //If the position is not beyond 750
                {
                    playerAnimation = right;
                    Position.X += playerMoveSpeed; //Move the player right at a speed 
                    playerAnimation.frameTime = 150;
                    playerState = 2;
                }

            }
            //Checks if the up key or up button is being pressed
            else if (currentKeyboardState.IsKeyDown(Keys.W) || currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                if (Position.Y < 0) //Checks if the y position goes beyond zero
                {
                    Position.Y = 0; //Reset the position to zero
                }
                else //If the y position is not beyond zero
                {
                    playerAnimation = up;
                    Position.Y -= playerMoveSpeed; //Move the player up at a speed
                    playerAnimation.frameTime = 150;
                    playerState = 4;

                }
            }
            //Checks if the down key or button is being pressed
            else if (currentKeyboardState.IsKeyDown(Keys.S) || currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                if (Position.Y > worldHeight) //If the y position goes beyond 450
                {
                    Position.Y = worldHeight; //Reset the y position to 450
                }
                else //If the y position is not beyond 450
                {
                    playerAnimation = down;
                    Position.Y += playerMoveSpeed; //Move the player down at a speed
                    playerAnimation.frameTime = 150;
                    playerState = 3;
                }
            }

            else if (currentKeyboardState == previousKeyboardState)
            {
                playerAnimation.sourceRectangle = new Rectangle(128, 0, 64, 64);
                playerAnimation.currentFrame = 2;
                playerAnimation.frameTime = 0;

            }

            //Checks if the left mouse button is being pressed
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 posDelta = mousePosition - Position;
                posDelta.Normalize();
                posDelta = posDelta * playerMoveSpeed;
                Position = Position + posDelta;
            }
            //--------------------------------Player DEATH
            foreach (EnemyBullets L in EnemyBulletManager.bullets)
            {
                bulletRectangle = new Rectangle((int)L.Position.X, (int)L.Position.Y, L.Width, L.Height);
                PlayerRec = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
                if (bulletRectangle.Intersects(PlayerRec))
                {
                    //dieing sound
                    //score
                    //enemy health
                    Health -= 5;
                    hurt.Play();

                    if (Health <= 0) //If the player health reaches zero
                    {
                        RIP.Play();
                        playerAnimation = dying; //The death animation is played
                        Active = false; //The player becomes inactive
                       
                    }

                    GUI.PlayerHealth = Health;

                    L.Active = false;

                    
                   
                }
            }
            //--------------------------------

            playerAnimation.Position = Position; //Updates the position via the player animation
            playerAnimation.Update(gameTime); //Updates the playeranimation via gametime

        } 
        
        public void Draw(SpriteBatch spriteBatch)
        {
            playerAnimation.Draw(spriteBatch); //Draws the playerAnimation
        }
    }
}



