//
// Blade.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBlade
        : CProjectile
    {
        public Vector2 OwnerOffset { get; set; }

        public static CBlade Spawn(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CBlade blade = new CBlade();
            blade.Initialize(owner.World, owner, damage);

            blade.Physics.Rotation = rotation;
            blade.Physics.Position = position;
            blade.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            blade.OwnerOffset = position - owner.Physics.Position;

            owner.World.EntityAdd(blade);

            return blade;
        }

        public override void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world);

            Owner = owner;
            Physics = new CPhysics();
            Visual = CVisual.MakeSpriteCachedForPlayer(world.Game, "Textures/Weapons/Blade", owner.GameControllerIndex);
            Visual.Color = CShip.GetPlayerColor(owner.GameControllerIndex);
            Visual.Update();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 22.0f);
            Damage = damage;
        }

        public override void Update()
        {
            base.Update();

            float speed = 0.15f;
            float sign = OwnerOffset.X < 0.0f ? -1.0f : 1.0f;
            Physics.Rotation += speed * sign;

            Vector2 rotate = new Vector2(0.0f, -38.0f).Rotate(AliveTime * 0.22f * sign);
            Physics.Position = Owner.Physics.Position + OwnerOffset + rotate;

            if (AliveTime > 8)
                Delete();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            if (Visual != null)
            {
                bool flip = OwnerOffset.X < 0.0f;
                Visual.Draw(sprite_batch, Physics.Position, Physics.Rotation, flip ? SpriteEffects.FlipVertically : SpriteEffects.None);
            }
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        public void OnCollide(CEnemy enemy)
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponBeamHit, Physics.Position + World.Random.NextVector2Variable() * 16.0f * World.Random.NextSign(), Visual.Color, null, null);
            CAudio.PlaySound("WeaponHitLaser");
        }

        public void OnCollide(CBuilding building)
        {
            World.ParticleEffects.Spawn(EParticleType.WeaponBeamHit, Physics.Position + World.Random.NextVector2Variable() * 16.0f * World.Random.NextSign(), Visual.Color, null, null);
            CAudio.PlaySound("WeaponHitLaser");
        }

    }
}
