//
// Building.cs
//

using System;
using System.Linq;
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
        public int Coins { get; set; }
        public bool Powerup { get; set; }

        private string _TextureName = "Building1";
        public string TextureName
        {
            get { return _TextureName; }
            set { _TextureName = value; UpdateTexture(); }
        }

        public CVisual VisualNormal { get; private set; }
        public CVisual VisualDestroyed { get; private set; }

        public CBuilding(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 20.0f);
        }

#if XBOX360
        public CBuilding()
    	{
    	}

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 16.0f);
        }
#endif

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }

        private void UpdateTexture()
        {
            VisualNormal = CVisual.MakeSpriteCached1(World, "Textures/Static/" + TextureName);
            VisualDestroyed = CVisual.MakeSpriteCached1(World, "Textures/Static/Destroyed/" + TextureName);
            VisualNormal = VisualNormal ?? CVisual.MakeLabel(World, TextureName);
            VisualDestroyed = VisualDestroyed ?? CVisual.MakeLabel(World, "Destroyed/" + TextureName);
            Visual = VisualNormal;
        }

        // TODO: CWeapon OnCollide?
        public void OnCollide(CLaser laser)
        {
            TakeDamage(laser.Damage);
            laser.Die();
        }

        public void OnCollide(CMissile missile)
        {
            TakeDamage(missile.Damage);
            missile.Die();
        }

        public void OnCollide(CBigPlasma plasma)
        {
            TakeDamage(plasma.Damage);
            plasma.Die();
        }

        public void OnCollide(CSmallPlasma plasma)
        {
            TakeDamage(plasma.Damage);
            plasma.Die();
        }

        public void OnCollide(CMiniShot minishot)
        {
            TakeDamage(minishot.Damage);
            minishot.Die();
        }

        private int CalculateScoreFromHealth()
        {
            int base_ = (int)HealthMax * 10;
            return base_ - base_ % 10;
        }

        private void OnDestroyed()
        {
            CEffect.Explosion(World, Physics.PositionPhysics.Position, 1.0f);
            World.Score += CalculateScoreFromHealth();
            Visual = VisualDestroyed;
            Collision = null;

            for (int i = 0; i < Coins; i++)
            {
                World.EntityAdd(new CBonus(World, Physics.PositionPhysics.Position));
            }

            if (Powerup)
            {
                World.EntityAdd(new CPowerup(World, Physics.PositionPhysics.Position));
            }
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
