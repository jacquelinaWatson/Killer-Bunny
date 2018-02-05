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

    class PlayerBulletManager
    {

        #region Bullet Manager Declerations

        static Texture2D pistolRight, pistolLeft, pistolUp, pistolDown; //Bullet textures
        static Rectangle bulletRectangle;
        static public List<PlayerBullets> bullets; //Creates a new list to store all of the player bullets currently active
        const float SECONDS_IN_MINUTE = 60f; //Seconds in minute
        const float RATE_OF_FIRE = 200F; //Rate of fire
        int worldWidth = 1500;
        int worldHeight = 1500;

        static TimeSpan bulletSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE); //Bulletspawn time
        static TimeSpan previousBulletSpawnTime; //Previous spawn time

        GraphicsDeviceManager graphics;
        static Vector2 graphicsInfo;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        GUI gui;
        #endregion

        public void Initialize(Texture2D textureRight, Texture2D textureLeft, Texture2D textureUp, Texture2D textureDown, GraphicsDevice Graphics, GUI GUI)
        {
            bullets = new List<PlayerBullets>(); //New list of playerbullets
            previousBulletSpawnTime = TimeSpan.Zero; //bullet spawn time is zero
            pistolRight = textureRight; //right texture
            pistolLeft = textureLeft; //left texture
            pistolUp = textureUp; //up texture
            pistolDown = textureDown; //down texture
            graphicsInfo.X = Graphics.Viewport.Width; //screenwidth
            graphicsInfo.Y = Graphics.Viewport.Height; //screenheight

            gui = GUI;
        }

        private static void FireBullet(GameTime gameTime, Player mainCharacter) //Method to fire bullet using gametime and the player
        {
            if (gameTime.TotalGameTime - previousBulletSpawnTime > bulletSpawnTime) //If gametime minus the previous spawn is bigger than the spawn time
            {
                if (Player.playerState == 1) //left
                {
                    var direction = 1;
                    previousBulletSpawnTime = gameTime.TotalGameTime; //previous spawn = total gametime
                    AddBullet(mainCharacter, direction); //Add bullet based on the main character
                }

                else if (Player.playerState == 2) //right
                {
                    var direction = 2;
                    previousBulletSpawnTime = gameTime.TotalGameTime; //previous spawn = total gametime
                    AddBullet(mainCharacter, direction); //Add bullet based on the main character
                }

                else if (Player.playerState == 3) //up
                {
                    var direction = 3;
                    previousBulletSpawnTime = gameTime.TotalGameTime; //previous spawn = total gametime
                    AddBullet(mainCharacter, direction); //Add bullet based on the main character
                }
                else if (Player.playerState == 4) //down
                {
                    var direction = 4;
                    previousBulletSpawnTime = gameTime.TotalGameTime; //previous spawn = total gametime
                    AddBullet(mainCharacter, direction); //Add bullet based on the main character
                }
                else
                {
                    var direction = 2;
                    previousBulletSpawnTime = gameTime.TotalGameTime; //previous spawn = total gametime
                    AddBullet(mainCharacter, direction); //Add bullet based on the main character
                }
            }
        }

        private static void AddBullet(Player mainCharacter, int direction) //Method to add bullets
        {
            Animation pistolAnimationRight = new Animation(); //Creates a new instance of animation for the player bullets
            pistolAnimationRight.Initialize(pistolRight, mainCharacter.Position, 13, 6, 1, 30, Color.White, true); //Initializes the playerbullet right animation
            Animation pistolAnimationLeft = new Animation();
            pistolAnimationLeft.Initialize(pistolLeft, mainCharacter.Position, 13, 6, 1, 30, Color.White, true); //Initializes the playerbullet left animation
            Animation pistolAnimationUp = new Animation();
            pistolAnimationUp.Initialize(pistolUp, mainCharacter.Position, 6, 13, 1, 30, Color.White, true); //Initializes the playerbullet up animation
            Animation pistolAnimationDown = new Animation();
            pistolAnimationDown.Initialize(pistolDown, mainCharacter.Position, 6, 13, 1, 30, Color.White, true); //Initializes the playerbullet down animation

            PlayerBullets bullet = new PlayerBullets(); //Creates a new instance of the player bullets class
            var bulletPosition = mainCharacter.Position; //Creates a new bullet position equal to the main character position

            if (direction == 1) //left
            {
                bulletPosition.Y += 25; //The y co-ordinate of where the bullet will be created (Original)
                bulletPosition.X += 15; //The x co-ordinate of where the bullet will be created (Original)
            }
            if (direction == 2) //right
            {
                bulletPosition.Y += 25;
                bulletPosition.X += 35;
            }
            if (direction == 3) //up
            {
                bulletPosition.Y += 25;
                bulletPosition.X += 15;

            }
            if (direction == 4) //down
            {
                bulletPosition.Y += 25;
                bulletPosition.X += 15;

            }

            bullet.Initialize(pistolAnimationRight, pistolAnimationLeft, pistolAnimationUp, pistolAnimationDown, bulletPosition, direction); //Initializes a bullet with animation and position
            bullets.Add(bullet); //Adds a bullet to the list

        }

        public void UpdatePlayerBulletManager(GameTime gameTime, Player mainCharacter) //Method to update the player bullet manager
        {
            previousGamePadState = currentGamePadState; //save the current gampad state
            previousKeyboardState = currentKeyboardState; //save the current keyboard state

            currentGamePadState = GamePad.GetState(PlayerIndex.One); //The current gamepad state is the state of the player one controller
            currentKeyboardState = Keyboard.GetState(); //Current keyboard state is the state of the keyboard

            //player fires with the pistol
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space) && gui.ActiveGun1 == true) //If space bar/x button is pressed
            {
                FireBullet(gameTime, mainCharacter); //Fire a bullet using game time and player
                gui.Gun1Ammo = gui.Gun1Ammo - 1;
            }
            //player fires with the AK
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space) && gui.ActiveGun2 == true && gui.Gun2Ammo > 0) //If space bar/x button is pressed
            {
                FireBullet(gameTime, mainCharacter); //Fire a bullet using game time and player
                gui.Gun2Ammo = gui.Gun2Ammo - 1;
            }
            //player fires with the P90
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space) && gui.ActiveGun3 == true && gui.Gun3Ammo > 0) //If space bar/x button is pressed
            {
                FireBullet(gameTime, mainCharacter); //Fire a bullet using game time and player
                gui.Gun3Ammo = gui.Gun3Ammo - 1;
            }
            for (var i = 0; i < bullets.Count; i++) //Update the bullets
            {
                bullets[i].Update(gameTime); //Update the particular bullet index based on gametime

                if (!bullets[i].Active || bullets[i].Position.X > worldWidth) //If the bullet is not active or goes beyond right side of screen
                {
                    bullets.Remove(bullets[i]); //Remove bullet when deactivated or reaches the end of the screen
                }
                else if (!bullets[i].Active || bullets[i].Position.Y > worldHeight) //If the bullet is not active or goes beyond the screen
                {
                    bullets.Remove(bullets[i]); //Removes bullet from the list
                }
                else if (!bullets[i].Active || bullets[i].Position.X < 0) //If the bullet is not active and the bulletposition X is below 0
                {
                    bullets.Remove(bullets[i]); //Removes bullet from the list
                }
                else if (!bullets[i].Active || bullets[i].Position.Y < 0) //If the bullet is not active and the bulletposition Y is below 0
                {
                    bullets.Remove(bullets[i]); //Removes bullet from the list
                }
            }

            /*
             
                //Detect collisions between the player and all enemies

            foreach (Enemy e in EnemyManager.enemiesType1)
            {
                //Create a rectangle for the enemy
                Rectangle enemyRectangle = new Rectangle(
                    (int)e.Position.X,
                    (int)e.Position.Y,
                    e.Width,
                    e.Height);
            }
                //Now see if this enemy collide with any laser shots
                foreach(PlayerBullets B in PlayerBulletManager.playerBullets)
                {
                    //Create a rectangle for this bullet
                    bulletRectangle = new Rectangle)
                        (int)B.Position.X,
                        (int)B.Position.Y,
                        B.Width,
                        B.Height);

                        //Test the bounds of the bullet and the enemy
                        if (bulletRectangle.Intersects(enemyRectangle)
                        {
                            //Play the sound of the explosion
                            //TBA

                            //Kill the enemy
                            e.Health = 0;

                            //Record the kill
                            //TBA
                                
                            //Kill the bullet
                            B.Active = false;

                            //Record your score
                            //TBA

                        }

                }



            */

        }

        public void DrawBullets(SpriteBatch spriteBatch) //Method to draw the bullets
        {
            foreach (var i in bullets) //Draw the bullet from the list
            {
                i.Draw(spriteBatch);
            }
        }


    }
}
