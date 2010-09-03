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

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, Vector2.Zero);
        }

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
            World.Stats.ShotDamageDealt += laser.Damage;
            TakeDamage(laser.Damage);
            laser.Die();
        }

        public void OnCollide(CMissile missile)
        {
            World.Stats.ShotDamageDealt += missile.Damage;
            TakeDamage(missile.Damage);
            missile.Die();
        }

        public void OnCollide(CPlasma plasma)
        {
            World.Stats.ShotDamageDealt += plasma.Damage;
            TakeDamage(plasma.Damage);
            plasma.Die();
        }

        public void OnCollide(CMiniShot minishot)
        {
            World.Stats.ShotDamageDealt += minishot.Damage;
            TakeDamage(minishot.Damage);
            minishot.Die();
        }

        public void OnCollide(CDetonation detonation)
        {
            TakeDamage(5.0f);
        }

        public void OnCollide(CEnemyShot shot)
        {
            if (!shot.IsReflected)
                return;

            TakeDamage(shot.Damage);
            shot.Die();
        }

        public void OnCollide(CEnemyLaser laser)
        {
            if (!laser.IsReflected)
                return;

            TakeDamage(laser.Damage);
            laser.Die();
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
                CBonus bonus = new CBonus();
                bonus.Initialize(World);
                bonus.Physics.PositionPhysics.Position = Physics.PositionPhysics.Position;
                World.EntityAdd(bonus);
            }

            if (Powerup)
            {
                CPowerup powerup = new CPowerup();
                powerup.Initialize(World);
                powerup.Physics.PositionPhysics.Position = Physics.PositionPhysics.Position;
                World.EntityAdd(powerup);
            }
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health < 0.0f)
            {
                World.Stats.BuildingKills += 1;
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
                case "Building5": HealthMax = 7.0f; break;
                case "Building6": HealthMax = 6.0f; break;
                case "Building7": HealthMax = 9.0f; break;
                case "Building8": HealthMax = 9.0f; break;
            }

            Health = HealthMax;
        }
    }
}
