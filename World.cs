﻿//
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
        public int Score { get; set; }
        public string StageName { get; set; }
        public CStage Stage { get; set; }
        public CScenery Scenery { get; set; }
        public CCamera GameCamera { get; set; }
        public CHud Hud { get; set; }
        public CSound Sound { get; set; }

        public CWorld(CGalaxy game)
        {
            Game = game;
            Random = new Random();
            Entities = new List<CEntity>();
            EntitiesToAdd = new List<CEntity>();
            EntitiesToDelete = new List<CEntity>();
            GameCamera = new CCamera(game);
            GameCamera.Position = Game.PlayerSpawnPosition.ToVector3();
            Sound = new CSound(this);
        }

        // TODO: stage definition param
        public void Start()
        {
            Hud = new CHud(this);

            Entities = new List<CEntity>();

            // TODO: load ship from profile
            SProfile profile = CSaveData.GetCurrentProfile();
            CShip ship = new CShip(this, profile, Game.PlayerSpawnPosition);
            Entities.Add(ship);

            Stage = new CStage(this, Game.StageDefinition);
            Stage.Start();

            // TODO: should this be in the stage?
            Game.Music.Play(Stage.Definition.MusicName);
            MethodInfo method = typeof(SceneryPresets).GetMethod(Stage.Definition.SceneryName);
            Scenery = method.Invoke(null, new object[] { this }) as CScenery;
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
            Scenery.Update();

            GameCamera.Position += Vector3.UnitY * -Stage.Definition.ScrollSpeed;
            GameCamera.Update();
            UpdateEntities();
            UpdateHud();

            Sound.Update();
        }

        public void UpdateEntities()
        {
            ProcessEntityAdd();
            ProcessEntityUpdates();
            ProcessEntityCollisions();
            ProcessEntityDelete();
        }

        public void UpdateHud()
        {
            CShip ship = GetNearestShip(Vector2.Zero);
            Hud.Update(ship);
        }

        public void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);

            DrawBackground(GameCamera);
            DrawEntities(GameCamera);
            DrawHud(GameCamera);
        }

        public void DrawBackground(CCamera camera)
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, camera.WorldMatrix);
            Scenery.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        public void DrawEntities(CCamera camera)
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, camera.WorldMatrix);

            Entities.ForEach(entity => entity.Draw(Game.DefaultSpriteBatch));

            Game.DefaultSpriteBatch.End();
        }

        public void DrawHud(CCamera camera)
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);

            Hud.Draw(Game.DefaultSpriteBatch);

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

        // TODO: generic implementation
        public CEnemy GetNearestEnemy(Vector2 position)
        {
            CEnemy result = null;
            float nearest = float.MaxValue;
            foreach (CEntity entity in Entities)
            {
                if (entity.GetType().IsSubclassOf(typeof(CEnemy)) == false)
                    continue;

                CEnemy enemy = entity as CEnemy;
                Vector2 enemy_position = enemy.Physics.PositionPhysics.Position;
                Vector2 offset = enemy_position - position;
                float length = offset.Length();

                if (length < nearest)
                {
                    result = enemy;
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

        public void ProcessEntityAdd()
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
