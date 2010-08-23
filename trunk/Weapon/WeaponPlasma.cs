//
// WeaponPlasma.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponPlasma
        : CWeapon
    {
        protected override void Instantiate(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CPlasma plasma = new CPlasma();

            plasma.Initialize(world, Owner.PlayerIndex, damage);

            plasma.Physics.PositionPhysics.Position = position;
            plasma.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;
            plasma.Physics.AnglePhysics.Rotation = rotation;

            world.EntityAdd(plasma);
        }
    };
}
