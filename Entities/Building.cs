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
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, Vector2.Zero);
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

            Vector2 texture = new Vector2(Visual.Texture.Width, Visual.Texture.Height);
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Size = texture * Visual.Scale * 0.75f;

            SetDefaultHealth();
        }

        // TODO: CWeapon OnCollide?
        public void OnCollide(CLaser laser)
        {
            TakeDamage(laser.Damage);
            laser.Die();
        }

        public void OnCollide(CBigLaser laser)
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
            float s = HealthMax * 15.0f;
            float d = s * CDifficulty.MoneyScale[CSaveData.GetCurrentProfile().Difficulty];
            int score = (int)d;
            return score - score % 10;
        }

        private void OnDestroyed()
        {
            CEffect.BuildingExplosion(World, Physics.PositionPhysics.Position, HealthMax);
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

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB aabb = Collision as CollisionAABB;
            aabb.Position = Physics.PositionPhysics.Position - aabb.Size * 0.5f;
        }

        private void SetDefaultHealth()
        {
            // TODO: do this in a way that doesnt suck
            // TODO: all use default health
            //if (HealthMax > 0.0f)
                //return;

            switch (TextureName)
            {
                case "Building1": HealthMax = 4.0f; break;
                case "Building2": HealthMax = 2.0f; break;
                case "Building3": HealthMax = 10.0f; break;
                case "Building4": HealthMax = 6.0f; break;
                case "Building5": HealthMax = 11.0f; break;
                case "Building6": HealthMax = 7.0f; break;
                case "Building7": HealthMax = 9.0f; break;
                case "Building8": HealthMax = 9.0f; break;
            }

            Health = HealthMax;
        }
    }
}
