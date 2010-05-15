//
// Shot.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CEnemyShot
        : CEntity
    {
        public static CEnemyShot Spawn(CWorld world, Vector2 position, float rotation, float speed, float damage)
        {
            CEnemyShot shot = new CEnemyShot(world, damage);

            shot.Physics.AnglePhysics.Rotation = rotation;
            shot.Physics.PositionPhysics.Position = position;
            shot.Physics.PositionPhysics.Velocity = Vector2.UnitX.Rotate(rotation) * speed;

            world.EntityAdd(shot);

            //world.Sound.Play("Shot", 1.0f);

            return shot;
        }

        public float Damage { get; private set; }

        public CEnemyShot(CWorld world, float damage)
            : base(world)
        {
            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/EnemyShot"), Color.White);
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
        }

#if XBOX360
        public CEnemyShot()
        {
        }

        public void Init360(CWorld world, float damage)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Weapons/EnemyShot"), Color.White);
            Collision = CCollision.GetCacheAABB(this, Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
        }
#endif

        public override void Update()
        {
            base.Update();

            if (!IsInScreen())
                Delete();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.PositionPhysics.Position;
        }

        public void OnCollide(CShip ship)
        {
            ship.TakeDamage(Damage);
            Die();
        }
    }
}
