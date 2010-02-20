//
// World.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
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
            GameCamera.Position = Game.PlayerSpawnPosition.ToVector3();
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
            CShip ship = new CShip(this, profile, Game.PlayerSpawnPosition);
            Entities.Add(ship);

            Game.Music.Play("Music/Stage1");
            Stage = new CStage(this, Game.StageDefinition);
            Stage.Start();
        }

        public void Stop()
        {
            Game.Music.StopImmediate();

            // TODO: clean up
            if (Stage != null)
            {
                Stage.Finish();
            }

            Entities.Clear();
            EntitiesToAdd.Clear();
            EntitiesToDelete.Clear();
        }

        public void Update()
        {
            Stage.Update();
            StarsLower.Update();
            StarsUpper.Update();

            GameCamera.Position += Vector3.UnitY * -Stage.Definition.ScrollSpeed;
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
            Game.GraphicsDevice.Clear(new Color(133, 145, 181));

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
            int money = CSaveData.GetCurrentProfile().Money + Score; ;
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, "Money: " + money.ToString(), new Vector2(10, 10), Color.White);
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

        public IEnumerable<CEntity> GetEntitiesInBox(CollisionAABB box)
        {
            List<CEntity> results = new List<CEntity>();

            foreach (CEntity entity in Entities)
            {
                // TODO: dont create so many objects
                CollisionCircle collide = new CollisionCircle(entity.Physics.PositionPhysics.Position, entity.GetRadius());
                if (collide.Intersects(box))
                    results.Add(entity);
            }

            return results;
        }

        public CEntity GetEntityAtPosition(Vector2 position)
        {
            Collision find = new CollisionCircle(position, 1.0f);

            foreach (CEntity entity in Entities)
            {
                // TODO: not this hack :|
                if (entity.GetType() == typeof(CEditorEntityPreview))
                    continue;

                if (entity.Physics == null)
                    continue;

                Collision collision = entity.Collision ?? new CollisionCircle(entity.Physics.PositionPhysics.Position, entity.GetRadius());
                if (find.Intersects(collision))
                    return entity;
            }

            return null;
        }

        public CEntity GetHighestEntityAtPosition(Vector2 position)
        {
            Collision find = new CollisionCircle(position, 1.0f);

            CEntity result = null;
            float highest = -1.0f;
            foreach (CEntity entity in Entities)
            {
                // TODO: not this hack :|
                if (entity.GetType() == typeof(CEditorEntityPreview))
                    continue;

                if (entity.Physics == null)
                    continue;

                Collision collision = entity.Collision ?? new CollisionCircle(entity.Physics.PositionPhysics.Position, entity.GetRadius());
                if (!find.Intersects(collision))
                    continue;

                if (highest <= 0.0f)
                {
                    result = entity;
                    highest = 0.0f;
                }

                if (entity.Visual == null)
                    continue;

                if (entity.Visual.Depth >= highest)
                {
                    result = entity;
                    highest = entity.Visual.Depth;
                }
            }

            return result;
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
                        continue;

                    if (inner.Collision == null || outer.Collision == null)
                        continue;

                    if (inner.Collision.Enabled == false || outer.Collision.Enabled == false)
                        continue;

                    if (outer.GetType() == inner.GetType())
                    {
                        if (outer.Collision.IgnoreSelfType && inner.Collision.IgnoreSelfType)
                            continue;
                    }

                    if (outer.Collision.Intersects(inner.Collision))
                    {
                        // TODO: something proper
                        System.Type inner_type = inner.GetType();
                        System.Type outer_type = outer.GetType();
                        MethodInfo method = outer_type.GetMethod("OnCollide", new Type[]{ inner_type });

                        if (method == null)
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
