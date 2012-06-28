using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BankPanic.Engine;

namespace BankPanic.GameObjects{

    public class Door : Entity {

        SpriteBatch spriteBatch;
        Texture2D sprEnemyBad, sprEnemyGood;
        Boolean boolOpenDoor = false, timOn = false;
        MouseState mouseState;
        Timer openTimer, closeTimer;

        int m_pos;
        Enemy enemy;
        Texture2D m_tex1; 
        Texture2D m_tex2;
        Rectangle m_sprPosition;

        public Door(Game game) : base(game){
            //enemy = new Enemy(game);
        }

        public Door(Game game, int pos, Texture2D tex1, Texture2D tex2, Rectangle sprPosition)
            : base(game, pos)
        {
            m_pos  = pos;
            m_tex1 = tex1;
            m_tex2 = tex2;
            m_sprPosition = sprPosition;            
            enemy = new Enemy(game, sprPosition);
        }

        public override void Initialize(){
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            
           // enemy.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //sprEnemyBad = Game.Content.Load<Texture2D>("bad");
            //sprEnemyGood = Game.Content.Load<Texture2D>("good");
        }

        public override void Update(GameTime gameTime){
            MouseState lastMouseState = mouseState;

            mouseState = Mouse.GetState();

            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if (!timOn){
                openTimer = new Timer();
                openTimer.AutoReset = false;
                openTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEventOpen);
                openTimer.Interval = 5000;
                openTimer.Start();
                enemy.Update(gameTime);
            }

            if (m_sprPosition.Contains(mousePosition) &&
               (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released) && !boolOpenDoor)
            {
                Console.WriteLine("Inside");
                boolOpenDoor = true;

            }
            else if (m_sprPosition.Contains(mousePosition) &&
                      (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released) && boolOpenDoor)
            {
                Console.WriteLine("Outside");
                boolOpenDoor = false;
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);
            // spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);

            if (boolOpenDoor)
            {
                spriteBatch.Draw(m_tex2, new Rectangle(m_pos, 100, 256, 256), Color.White);
                this.LoadContent();
                enemy.Draw(gameTime, spriteBatch);
            }
            else
            {
                spriteBatch.Draw(m_tex1, new Rectangle(m_pos, 100, 256, 256), Color.White);
            }

          //  base.Draw(gameTime, spriteBatch);
        }
        
        public void DisplayTimeEventOpen(object source, ElapsedEventArgs e){
            boolOpenDoor = true;
            timOn = true;
            closeTimer = new Timer();
            closeTimer.AutoReset = false;
            closeTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEventClose);
            closeTimer.Interval = 5000;
            closeTimer.Start();
        }

        public void DisplayTimeEventClose(object source, ElapsedEventArgs e){
            boolOpenDoor = false;
            timOn = false;
        }

    }
}
