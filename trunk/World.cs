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

namespace Galaxy
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
        public CStageDefinition StageDefinition { get; set; }
        public string StageName { get; set; }
        public CStage Stage { get; set; }
        public CCamera GameCamera { get; set; }

        public CWorld(CGalaxy game)
        {
            Game = game;
            Random = new Random();
            Entities = new List<CEntity>();
            EntitiesToAdd = new List<CEntity>();
            EntitiesToDelete = new List<CEntity>();
            GameCamera = new CCamera(game);
        }

        // TODO: stage definition param
        public void Start()
        {
            // TODO: use this.Content to load your game content here
            Texture2D star_texture = CContent.LoadTexture2D(Game, "Textures/Background/Star");
            StarsLower = new CStars(this, star_texture, 1.2f, 6.0f);
            StarsUpper = new CStars(this, star_texture, 0.8f, 9.0f);

            Entities = new List<CEntity>();
            // TODO: load ship from profile
            SProfile profile = CSaveData.GetCurrentProfile();
            CShip ship = new CShip(this, profile, new Vector2(300.0f, 400.0f));
            Entities.Add(ship);


            Game.Music.Play("Music/Stage1");

            StageName = "EditorStage";
            Type stage_type = Type.GetType(String.Format("Galaxy.Stages.{0}", StageName));
            MethodInfo generate_method = stage_type.GetMethod("GenerateDefinition");
            StageDefinition = generate_method.Invoke(null, null) as CStageDefinition;
            Stage = new CStage(this, StageDefinition);
        }

        public void Stop()
        {
            Entities.Clear();
            EntitiesToAdd.Clear();
            EntitiesToDelete.Clear();
        }

        public void Update()
        {
            Stage.Update();
            StarsLower.Update();
            StarsUpper.Update();

            GameCamera.Update();
            UpdateEntities();
        }

        public void UpdateEntities()
        {
            ProcessEntityAdd();
            ProcessEntityUpdates();
            ProcessEntityCollisions();
            ProcessEntityDelete();
        }

        public void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);

            DrawBackground(GameCamera);
            DrawEntities(GameCamera);
            DrawHUD(GameCamera);
        }

        public void DrawBackground(CCamera camera)
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None, camera.WorldMatrix);

            // TODO: split to scenery/bg system
            StarsLower.Draw(Game.DefaultSpriteBatch);
            StarsUpper.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

        public void DrawEntities(CCamera camera)
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, camera.WorldMatrix);

            Entities.ForEach(entity => entity.Draw(Game.DefaultSpriteBatch));

            Game.DefaultSpriteBatch.End();
        }

        public void DrawHUD(CCamera camera)
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None, Matrix.Identity);

            // TODO: split to HUD system
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Score: " + Score.ToString(), new Vector2(10, 10), Color.White);
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Lives: No", new Vector2(10, 30), Color.White);

            // TODO: HUD system
            CShip ship = GetNearestShip(Vector2.Zero);
            if (ship != null)
            {
                Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, String.Format("Shield: {0:0.0}", ship.Shield), new Vector2(10, 50), Color.White);
                Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, String.Format("Armor: {0:0.0}", ship.Armor), new Vector2(10, 70), Color.White);
            }

            Game.DefaultSpriteBatch.End();
        }

        public void EntityAdd(CEntity entity)
        {
            EntitiesToAdd.Add(entity); 
        }

        public void EntityDelete(CEntity entity)
        {
            EntitiesToDelete.Add(entity);
        }

        public IEnumerable<CEntity> GetEntities()
        {
            return Entities;
        }

        public IEnumerable<CEntity> GetEntitiesOfType(Type type)
        {
            return Entities.Where(entity => entity.GetType() == type);
        }

        public CEntity GetEntityAtPosition(Vector2 position)
        {
            Collision find = new CollisionCircle(position, 1.0f);

            foreach (CEntity entity in Entities)
            {
                if (entity.Physics == null)
                    continue;

                Collision collision = entity.Collision ?? new CollisionCircle(entity.Physics.PositionPhysics.Position, entity.GetRadius());
                if (find.Intersects(collision))
                    return entity;
            }

            return null;
        }

        public CShip GetNearestShip(Vector2 position)
        {
            CShip result = null;
            float nearest = float.MaxValue;
            foreach (CEntity entity in GetEntitiesOfType(typeof(CShip)))
            {
                CShip ship = entity as CShip;
                Vector2 ship_position = ship.Physics.PositionPhysics.Position;
                Vector2 offset = ship_position - position;
                float length = offset.Length();

                if (length < nearest)
                {
                    result = ship;
                    nearest = length;
                }
            }

            return result;
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

                    if (inner.Collision == null || outer.Collision == null)
                    {
                        continue;
                    }

                    if (inner.Collision.Enabled == false || outer.Collision.Enabled == false)
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
                        // TODO: we can't use method overloads here because we can't override with a different param
                        // TODO: make something that overrides for all types? and allow  
                        //outer.OnCollision(inner);

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

                            try
                            {
                                method.Invoke(outer, new object[] { inner });
                            }
                            catch (Exception exception)
                            {
                                throw exception.InnerException;
                            }
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
    }
}
