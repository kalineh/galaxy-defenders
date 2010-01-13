//
// World.cs
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace galaxy
{
    public class CWorld
    {
        public CGalaxy Game { get; set; }
        public Random Random { get; private set; }
        private List<CEntity> Entities { get; set; }
        private List<CEntity> EntitiesToAdd { get; set; }
        private List<CEntity> EntitiesToDelete { get; set; }
        private CStars StarsLower { get; set; }
        private CStars StarsUpper { get; set; }
        public int Score { get; set; }

        public CWorld(CGalaxy game)
        {
            Game = game;
            Random = new Random();
            Entities = new List<CEntity>();
            EntitiesToAdd = new List<CEntity>();
            EntitiesToDelete = new List<CEntity>();
        }

        // TODO: stage definition param
        public void Start()
        {
            // TODO: use this.Content to load your game content here
            Texture2D star_texture = Game.Content.Load<Texture2D>("Star");
            StarsLower = new CStars(this, star_texture, 1.2f, 6.0f);
            StarsUpper = new CStars(this, star_texture, 0.8f, 9.0f);

            Texture2D ship_texture = Game.Content.Load<Texture2D>("Ship");

            Entities = new List<CEntity>();
            CShip ship = new CShip(this, "Ship", ship_texture);
            ship.Physics.PositionPhysics.Position = new Vector2(300.0f, 400.0f);
            Entities.Add(ship);

            Game.Music.Play("Stage1");
        }

        public void Update()
        {
            UpdateAsteroidSpawn();

            StarsLower.Update();
            StarsUpper.Update();

            ProcessEntityAdd();
            ProcessEntityUpdates();
            ProcessEntityCollisions();
            ProcessEntityDelete();
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            sprite_batch.Begin();

            StarsLower.Draw(sprite_batch);
            StarsUpper.Draw(sprite_batch);

            foreach (CEntity entity in Entities)
            {
                entity.Draw(sprite_batch);
            }

            sprite_batch.DrawString(Game.DefaultFont, "Score: " + Score.ToString(), new Vector2(10, 10), Color.White);

            sprite_batch.End();
        }

        public void EntityAdd(CEntity entity)
        {
            EntitiesToAdd.Add(entity); 
        }

        public void EntityDelete(CEntity entity)
        {
            EntitiesToDelete.Add(entity);
        }

        private void ProcessEntityUpdates()
        {
            foreach (CEntity entity in Entities)
            {
                entity.Update();
            }
        }

        private void ProcessEntityCollisions()
        {
            foreach (CEntity outer in Entities)
            {
                foreach (CEntity inner in Entities)
                {
                    if (inner == outer)
                    {
                        continue;
                    }

                    if (inner.Collision.Enabled == false ||
                        outer.Collision.Enabled == false)
                    {
                        continue;
                    }

                    if (outer.GetType() == inner.GetType())
                    {
                        if (outer.Collision.IgnoreSelfType && inner.Collision.IgnoreSelfType)
                        {
                            continue;
                        }
                    }

                    if (outer.Collision.Intersects(inner.Collision))
                    {
                        // TODO: something proper
                        outer.Collision.Intersects(inner.Collision);
                        System.Type inner_type = inner.GetType();
                        System.Type outer_type = outer.GetType();
                        MethodInfo[] methods = outer_type.GetMethods();
                        foreach (MethodInfo method in methods)
                        {
                            if (method.Name != "OnCollide")
                                continue;

                            ParameterInfo param_info = method.GetParameters()[0];
                            System.Type param = param_info.ParameterType;
                            if (param != inner_type)
                                continue;

                            method.Invoke(outer, new object[] { inner });
                        }

                        //var method = from m in methods where m.Name == "OnCollide" && m.GetParameters()[0].GetType() == inner_type select m;

                        //if (method.Count() < 1)
                        //{
                            //Console.WriteLine("{0}: no collide function for: {1}", outer.GetType().ToString(), inner.GetType().ToString());
                            //continue;
                        //}

                        //method.Single().Invoke(outer, new object[] { inner });
                    }
                }
            }
        }

        private void ProcessEntityAdd()
        {
            foreach (CEntity entity in EntitiesToAdd)
            {
                Entities.Add(entity);
            }
            EntitiesToAdd.Clear();
        }

        private void ProcessEntityDelete()
        {
            foreach (CEntity entity in EntitiesToDelete)
            {
                Entities.Remove(entity);
            }
            EntitiesToDelete.Clear();
        }

        private void UpdateAsteroidSpawn()
        {
            if (Random.NextFloat() < 0.05f)
            {
                CAsteroid.Spawn(this);
            }
        }
    }
}
