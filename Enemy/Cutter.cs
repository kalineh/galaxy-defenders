//
// Cutter.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public class CCutter
        : CEnemy
    {
        private CCutterBall Left { get; set; }
        private CCutterBall Right { get; set; }
        private CVisual BeamVisual { get; set; }
        private float BeamRotation { get; set; }
        private float BeamRotationSpeed { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(64.0f, 64.0f));
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Cutter");

            BeamVisual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/CutterBeam");
            BeamVisual.Depth = CLayers.Enemy + CLayers.SubLayerIncrement * -1.0f;
            //BeamVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);

            //Physics.AngularVelocity = 0.03f * World.Random.NextSign();
            BeamRotation = 0.0f;
            BeamRotationSpeed = 0.03f * World.Random.NextSign();

            HealthMax = 14.0f;
        }

        public override void Update()
        {
            if (Left == null)
            {
                Left = new CCutterBall();
                Left.Initialize(World);
                World.EntityAdd(Left);

                Right = new CCutterBall();
                Right.Initialize(World);
                World.EntityAdd(Right);
            }

            base.Update();
            BeamVisual.Update();
            BeamRotation += BeamRotationSpeed;
        }

        public override void UpdateAI()
        {
            Vector2 pos = Physics.Position;
            Vector2 dir = Vector2.UnitX.Rotate(BeamRotation);
            Vector2 ofs = dir * 116.0f;
            Left.Physics.Position = pos + ofs;
            Left.Physics.Rotation = BeamRotation * 3.0f;
            Right.Physics.Position = pos - ofs;
            Right.Physics.Rotation = BeamRotation * -3.0f;
        }

        public override void UpdateCollision()
        {
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Position = Physics.Position - new Vector2(32.0f, 32.0f);
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
            BeamVisual.Draw(sprite_batch, Physics.Position, BeamRotation);
        }

        protected override void OnDie()
        {
            Left.Die();
            Right.Die();
            base.OnDie();
        }
    }
}

