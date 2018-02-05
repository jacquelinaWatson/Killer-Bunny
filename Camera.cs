using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Killer_Bunny
{
    public class Camera
    { 
        private Viewport viewport;
        public int worldWidth = 3200;
        public int worldHeight = 3200;
        public Vector2 Position;
        public const int ViewportWidth = 800;
        public const int ViewportHeight = 600;
        GUI gui;
        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            viewport.Height = ViewportHeight;
            viewport.Width = ViewportWidth;
            Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }
        public Vector2 Origin { get; set; }

        public Matrix GetViewMatrix() //Gets the viewport and applies relevant translation 
        {
            return
            Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
            Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
            Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void NoEscape(Vector2 PlayerPos, Rectangle PlayerRec) //Camera does not go off the edge of the map;
        {
            Follow(PlayerRec, PlayerPos);
           if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y);
            }
              else if (Position.X + viewport.Width > worldWidth)
              {
                  Position = new Vector2(Position.Y, worldWidth - viewport.Width);
                   
              }
          
            if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, 0);
            }
            else if (Position.Y + viewport.Height > worldHeight)
            {
                Position = new Vector2(Position.X, worldHeight - viewport.Height);
            }
        }

        public void Follow(Rectangle PlayerRec, Vector2 PlayerPos) //Camera follows the position of an object
        {
         
            if (PlayerRec.X - viewport.Width < 0)
            {
                Position.X = PlayerRec.X - (int)Origin.X;
                
            }
            if (PlayerRec.Y - viewport.Height < 0)
            {
                Position.Y = PlayerRec.Y - (int)Origin.Y;
            }
            if (PlayerPos.X - PlayerRec.Width > worldWidth)
            {
                Position.X = worldWidth - viewport.Width;
            }
            if (PlayerPos.Y - PlayerRec.Height > worldHeight)
            {
                Position.Y = worldHeight - viewport.Height;
            }

        }
        public  Vector2 Transform(Vector2 point)
        {
            return point - Position;
        }
        public  Rectangle Transform(Rectangle rectangle)
        {
            return new Rectangle(rectangle.Left - (int)Position.X, rectangle.Top - (int)Position.Y, rectangle.Width, rectangle.Height);
        }



    }
}





