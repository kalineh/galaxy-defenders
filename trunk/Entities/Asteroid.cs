//
// Asteroid.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CAsteroid
        : CEntity
    {
        private float _HealthMax;
        public float HealthMax
        {
            get { return _HealthMax; }
            set { _HealthMax = value; Health = value; }
        }

        public float Health { get; private set; }
        public CVisual Cracks { get; set; }

        public CAsteroid(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Entity/Asteroid"), Color.White);
            Cracks = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Entity/Cracks"), Color.White);
            Cracks.Alpha = 0.0f;
        }

#if XBOX360
        public CAsteroid()
        {
        }

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Entity/Asteroid"), Color.White);
            Cracks = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Entity/Cracks"), Color.White);
            Cracks.Alpha = 0.0f;
        }
#endif

        public override void Update()
        {
            base.Update();

            Cracks.Update();

            if (IsOffScreenBottom())
                Delete();
        }

        public override void Draw(SpriteBatch sprite_batch)
        {
            base.Draw(sprite_batch);
            Cracks.Draw(sprite_batch, Physics.PositionPhysics.Position, Physics.AnglePhysics.Rotation);
        }

        // TODO: better system
        public void OnCollide(CShip ship)
        {
            ship.TakeCollideDamage(Physics.PositionPhysics.Position, 1.0f);
            Health -= 1.0f;
        }

        public void OnCollide(CLaser laser)
        {
            Physics.PositionPhysics.Velocity += laser.Physics.AnglePhysics.GetDir() * laser.Damage;
            TakeDamage(laser.Damage);
            laser.Die();
        }

        public void OnCollide(CBigLaser laser)
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

        public void OnCollide(CSmallPlasma plasma)
        {
            Physics.PositionPhysics.Velocity += plasma.Physics.AnglePhysics.GetDir() * plasma.Damage;
            TakeDamage(plasma.Damage);
            plasma.Die();
        }

        public void OnCollide(CBigPlasma plasma)
        {
            Physics.PositionPhysics.Velocity += plasma.Physics.AnglePhysics.GetDir() * plasma.Damage;
            TakeDamage(plasma.Damage);
            plasma.Die();
        }

        public void OnCollide(CMiniShot minishot)
        {
            Physics.PositionPhysics.Velocity += minishot.Physics.AnglePhysics.GetDir() * minishot.Damage;
            TakeDamage(minishot.Damage);
            minishot.Die();
        }

        protected override void OnDie()
        {
            CEffect.Explosion(World, Physics.PositionPhysics.Position, 1.0f);
            World.Score += 100;
            base.OnDie();
        }

        private void TakeDamage(float damage)
        {
            Health -= damage;
            Cracks.Alpha = 1.0f - Health / HealthMax;
            if (Health < 0.0f)
            {
                Die();
            }
        }

    }
}
