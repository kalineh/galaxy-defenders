//
// Projectile.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CProjectile
        : CEntity
        , ICacheableProjectile
    {
        public CShip Owner { get; set; }
        public float Damage { get; set; }

        protected CProjectile()
        {
        }

        public virtual void Initialize(CWorld world, CShip owner, float damage)
        {
            base.Initialize(world);
            Owner = owner;
            Physics = new CPhysics();
            Damage = damage;
        }

        public override void Update()
        {
            base.Update();

            if (!IsInScreen(16.0f))
                Delete();
        }
    }
}
