//
// Building.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CBuilding
        : CEntity
    {
        private float _HealthMax;
        public float HealthMax
        {
            get { return _HealthMax; }
            set { _HealthMax = value; Health = value; }
        }

        public float Health { get; private set; }
        public CVisual VisualNormal { get; private set; }
        public CVisual VisualDestroyed { get; private set; }

        public CBuilding(CWorld world, Vector2 position)
            : base(world, "Building")
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = new CollisionCircle(Vector2.Zero, 16.0f);
            VisualNormal = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Static/Building"), Color.White);
            VisualDestroyed = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Static/BuildingDestroyed"), Color.White);
            Visual = VisualNormal;
            Health = 20.0f;
        }

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }

        public void OnCollide(CLaser laser)
        {
            Physics.PositionPhysics.Velocity += laser.Physics.AnglePhysics.GetDir() * laser.Damage;
            TakeDamage(laser.Damage);
            laser.Die();
        }

        public void OnCollide(CMissile missile)
        {
            Physics.PositionPhysics.Velocity += missile.Physics.AnglePhysics.GetDir() * missile.Damage;
            TakeDamage(missile.Damage);
            missile.Die();
        }

        private void OnDestroyed()
        {
            CExplosion.Spawn(World, Physics.PositionPhysics.Position, 1.0f);
            World.Score += 100;
            Visual = VisualDestroyed;
            Collision = null;
        }

        private void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health < 0.0f)
            {
                OnDestroyed();
            }
        }

    }
}
