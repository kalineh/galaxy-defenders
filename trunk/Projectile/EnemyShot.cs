//
// Shot.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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

            //SoundEffect sound = world.Game.Content.Load<SoundEffect>("Shot");
            //sound.Play(0.1f, 0.0f, 0.0f);

            return shot;
        }

        public float Damage { get; private set; }

        public CEnemyShot(CWorld world, float damage)
            : base(world, "EnemyShot")
        {
            Physics = new CPhysics();
            Visual = new CVisual(world.Game.Content.Load<Texture2D>("EnemyShot"), Color.White);
            Collision = new CollisionAABB(Vector2.Zero, new Vector2(1.0f, 0.5f));
            Damage = damage;
        }

        public override void Update()
        {
            base.Update();

            if (!IsInScreen())
                Die();
        }

        public override void UpdateCollision()
        {
            // TODO: find a better way to sync these
            CollisionAABB box = Collision as CollisionAABB;
            box.Position = Physics.PositionPhysics.Position;
        }

        public void OnCollide(CShip ship)
        {
            ship.Die();
        }
    }
}
