//
// SeekBomb.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CSeekBomb
        : CProjectile
    {
        public static CSeekBomb Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CSeekBomb seek_bomb = new CSeekBomb();
            seek_bomb.Initialize(owner.World, owner, damage);

            seek_bomb.Speed = speed;

            seek_bomb.Physics.Rotation = owner.World.Random.NextAngle();
            seek_bomb.Physics.Position = position;
            seek_bomb.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            owner.World.EntityAdd(seek_bomb);

            return seek_bomb;
        }

        public float Speed { get; set; }
        public int SeekFramesRemaining { get; set; }
        public CEnemy Target { get; set; }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world, owner, damage);

            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/SeekBomb", owner.PlayerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.PlayerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 1.0f);
            Damage = damage;
            SeekFramesRemaining = 90;
        }

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

            Vector2 target_position = Physics.Position + Vector2.UnitY * -100.0f;
            if (Target != null)
            {
                target_position = Target.Physics.Position;
            }


            SeekFramesRemaining = Math.Max(0, SeekFramesRemaining - 1);
            if (SeekFramesRemaining > 0)
            {
                Vector2 offset = target_position - Physics.Position;
                Vector2 dir = offset.Normal();
                Physics.Velocity = Vector2.Lerp(Physics.Velocity, dir * Speed, 0.15f);
            }
            else
            {
                Physics.Velocity = Vector2.Lerp(Physics.Velocity, Physics.Velocity.Normal() * Speed, 0.15f);
            }

            Physics.Rotation += 0.1f;

                    //CDebugRender.Text(World.GameCamera.WorldMatrix, Physics.Position, ((this as CShootBall).IsSeekerTarget).ToString(), Color.White);

            if (!IsInScreen())
                Delete();

            base.Update();
        }

        protected override void OnDie()
        {
            ClearTarget();
            CEffect.MissileExplosion(World, Physics.Position, 2.0f, Visual.Color);
            CAudio.PlaySound("WeaponHitSeekBomb", 1.0f);
            base.OnDie();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        private void FindTarget()
        {
            CEnemy enemy =
                World.GetNearestEnemySeekable(Physics.Position, true) ??
                World.GetNearestEnemySeekable(Physics.Position, false);

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
