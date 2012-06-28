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
using BankPanic.GameObjects;

namespace BankPanic
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Door door;
        //Enemy enemy;

        public List<Door> listDoor = new List<Door>();

        public void AddEntity(Door NewDoor)
        {
            listDoor.Add(NewDoor);
        }


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            door = new Door(this);
            //enemy = new Enemy(this);

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            door.Initialize();
           // enemy.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            listDoor.Add(new Door(this, 40, Content.Load<Texture2D>("CastleDoor1"), Content.Load<Texture2D>("CastleDoor2"), new Rectangle(40, 100, 256, 256)));
            listDoor.Add(new Door(this, 240, Content.Load<Texture2D>("CastleDoor1"), Content.Load<Texture2D>("CastleDoor2"), new Rectangle(240, 100, 256, 256)));
            listDoor.Add(new Door(this, 440, Content.Load<Texture2D>("CastleDoor1"), Content.Load<Texture2D>("CastleDoor2"), new Rectangle(440, 100, 256, 256)));
            //enemy.LoadContent();
        }

        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            foreach (Door Current in listDoor)
                Current.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone);

            foreach (Door Current in listDoor)
                Current.Draw(gameTime, spriteBatch);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
