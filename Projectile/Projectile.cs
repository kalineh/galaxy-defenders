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
        public float Damage { get; set; }

        protected CProjectile()
        {
        }

        public virtual void Initialize(CWorld world, PlayerIndex player_index, float damage)
        {
            base.Initialize(world);
            Physics = new CPhysics();
            Damage = damage;
        }
    }
}
