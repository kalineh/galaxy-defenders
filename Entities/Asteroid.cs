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

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
            Visual = new CVisual(world.Game, CContent.LoadTexture2D(world.Game, "Textures/Entity/Asteroid"), Color.White);
            Cracks = new CVisual(world.Game, CContent.LoadTexture2D(world.Game, "Textures/Entity/Cracks"), Color.White);
            Cracks.Alpha = 0.0f;
        }

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
            Cracks.Draw(sprite_batch, Physics.Position, Physics.Rotation);
        }

        // TODO: better system
        public void OnCollide(CShip ship)
        {
            World.Stats.CollisionDamageReceived += 1.0f;
            ship.TakeCollideDamage(Physics.Position, 1.0f);
            TakeDamage(1.0f, ship);
        }

        public void OnCollide(CLaser laser)
        {
            Physics.Velocity += laser.Physics.GetDir() * laser.Damage;
            TakeDamage(laser.Damage, laser.Owner);
            laser.Die();
        }

        public void OnCollide(CMissile missile)
        {
            Physics.Velocity += missile.Physics.GetDir() * missile.Damage;
            TakeDamage(missile.Damage, missile.Owner);
            missile.Die();
        }

        public void OnCollide(CPlasma plasma)
        {
            Physics.Velocity += plasma.Physics.GetDir() * plasma.Damage;
            TakeDamage(plasma.Damage, plasma.Owner);
            plasma.Die();
        }

        public void OnCollide(CMiniShot minishot)
        {
            Physics.Velocity += minishot.Physics.GetDir() * minishot.Damage;
            TakeDamage(minishot.Damage, minishot.Owner);
            minishot.Die();
        }

        protected override void OnDie()
        {
            base.OnDie();
        }

        private void TakeDamage(float damage, CShip source)
        {
            Health -= damage;
            Cracks.Alpha = 1.0f - Health / HealthMax;
            if (Health < 0.0f)
            {
                source.Score += 100;
                Die();
            }
        }

    }
}
