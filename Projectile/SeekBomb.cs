//
// SeekBomb.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CSeekBomb
        : CEntity
    {
        public static CSeekBomb Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage, PlayerIndex index)
        {
            CSeekBomb seek_bomb = new CSeekBomb(world, damage, index);

            seek_bomb.Speed = speed;

            seek_bomb.Physics.AnglePhysics.Rotation = world.Random.NextAngle();
            seek_bomb.Physics.PositionPhysics.Position = position;
            seek_bomb.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(seek_bomb);

            return seek_bomb;
        }

        public float Damage { get; private set; }
        public float Speed { get; set; }
        public int SeekFramesRemaining { get; set; }
        public CEnemy Target { get; set; }

        public CSeekBomb(CWorld world, float damage, PlayerIndex index)
            : base(world)
        {
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/SeekBomb"), CShip.GetPlayerColor(index));
            Visual.Color = CShip.GetPlayerColor(index);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
            Damage = damage;
            IgnoreCameraScroll = true;
            SeekFramesRemaining = 90;
        }

#if XBOX360
        public CSeekBomb()
        {
        }

        public void Init360(CWorld world, float damage, PlayerIndex index)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/SeekBomb"), CShip.GetPlayerColor(index));
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
            Damage = damage;
            IgnoreCameraScroll = true;
            SeekFramesRemaining = 90;
        }
#endif

        public override void Update()
        {
            if (Target == null)
            {
                FindTarget();
            }

            if (Target != null)
            {
                if (Target.Physics == null ||
                    Target.Collision == null ||
                    Target.Health <= 0.0f ||
                    Target.IsInScreen() == false)
                {
                    ClearTarget();
                }
            }

            Vector2 target_position = Physics.PositionPhysics.Position + Vector2.UnitY * -100.0f;
            if (Target != null)
            {
                target_position = Target.Physics.PositionPhysics.Position;
            }


            SeekFramesRemaining = Math.Max(0, SeekFramesRemaining - 1);
            if (SeekFramesRemaining > 0)
            {
                Vector2 offset = target_position - Physics.PositionPhysics.Position;
                Vector2 dir = offset.Normal();
                Physics.PositionPhysics.Velocity = Vector2.Lerp(Physics.PositionPhysics.Velocity, dir * Speed, 0.15f);
            }
            else
            {
                Physics.PositionPhysics.Velocity = Vector2.Lerp(Physics.PositionPhysics.Velocity, Physics.PositionPhysics.Velocity.Normal() * Speed, 0.15f);
            }

            Physics.AnglePhysics.Rotation += 0.1f;

                    //CDebugRender.Text(World.GameCamera.WorldMatrix, Physics.PositionPhysics.Position, ((this as CShootBall).IsSeekerTarget).ToString(), Color.White);

            if (!IsInScreen())
                Delete();

            base.Update();
        }

        protected override void OnDie()
        {
            ClearTarget();
            CEffect.Explosion(World, Physics.PositionPhysics.Position, 2.5f);
            World.Sound.Play("WeaponHitSeekBomb", 1.0f);
            base.OnDie();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.PositionPhysics.Position;
        }

        private void FindTarget()
        {
            CEnemy enemy =
                World.GetNearestEnemySeekable(Physics.PositionPhysics.Position, true) ??
                World.GetNearestEnemySeekable(Physics.PositionPhysics.Position, false);

            Target = enemy;
            if (Target != null)
            {
                Target.IsSeekerTarget = true;
            }
        }

        private void ClearTarget()
        {
            if (Target != null)
            {
                Target.IsSeekerTarget = false;
                Target = null;
            }
        }
    }
}
