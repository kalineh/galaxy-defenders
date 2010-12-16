//
// WeaponPlasma.cs
//

using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CWeaponPlasma
        : CWeapon
    {
        protected override void Instantiate(CShip owner, Vector2 position, float rotation, float speed, float damage)
        {
            CPlasma plasma = new CPlasma();

            plasma.Initialize(owner.World, owner, damage);

            plasma.Physics.Position = position;
            plasma.Physics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;
            plasma.Physics.Rotation = rotation;

            owner.World.EntityAdd(plasma);
        }
    };
}
