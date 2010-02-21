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
            Collision = new CollisionCircle(Vector2.Zero, 16.0f);
            Visual = VisualNormal;
        }

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }

        private void UpdateTexture()
        {
            VisualNormal = CVisual.MakeSprite(World, "Textures/Static/" + TextureName);
            VisualDestroyed = CVisual.MakeSprite(World, "Textures/Static/" + TextureName + "Destroyed");
            VisualNormal = VisualNormal ?? CVisual.MakeLabel(World, TextureName);
            VisualDestroyed = VisualDestroyed ?? CVisual.MakeLabel(World, TextureName + "Destroyed");
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

        private void OnDestroyed()
        {
            CEffect.Explosion(World, Physics.PositionPhysics.Position, 1.0f);
            World.Score += 100;
            Visual = VisualDestroyed;
            Collision = null;

            foreach (int i in Enumerable.Range(0, Coins))
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
