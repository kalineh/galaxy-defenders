//
// Swirl.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Galaxy
{
    public class CSwirl
        : CEnemy
    {
        private List<CSwirlBall> Balls { get; set; }

        public override void Initialize(CWorld world)
        {
            base.Initialize(world);

            Physics = new CPhysics();
            Collision = CCollision.GetCacheCircle(this, Vector2.Zero, 32.0f);
            Visual = CVisual.MakeSpriteCached1(world.Game, "Textures/Enemy/Swirl");

            HealthMax = 3.0f;

            Coins = 1;
        }

        public void MakeBalls()
        {
            Balls = new List<CSwirlBall>();
            Balls.Add(new CSwirlBall());
            Balls.Add(new CSwirlBall());
            Balls.Add(new CSwirlBall());

            int index = 0;
            foreach (CSwirlBall ball in Balls)
            {
                ball.Owner = this;
                ball.Index = index++;
                ball.Initialize(World);
                World.EntityAdd(ball);
            }
        }

        public override void UpdateAI()
        {
            if (Balls == null)
                MakeBalls();

            float step = MathHelper.TwoPi / Balls.Count;
            float start = World.Game.GameFrame * 0.1f;
            for (int i = 0; i < Balls.Count; ++i)
            {
                CSwirlBall ball = Balls[i];
                if (ball.IsDead)
                    continue;

                Vector2 offset = Vector2.UnitX.Rotate(start + step * i) * 36.0f;
                Vector2 target = Physics.Position + offset;

                Balls[i].Physics.Position = target;
            }
        }

        public override void UpdateCollision()
        {
            CollisionCircle circle = Collision as CollisionCircle;
            circle.Position = Physics.Position;
        }

        protected override void OnDie()
        {
            float step = MathHelper.TwoPi / Balls.Count;
            float start = World.Game.GameFrame * 0.1f;
            for (int i = 0; i < Balls.Count; ++i)
            {
                CSwirlBall ball = Balls[i];
                if (ball.IsDead)
                    continue;

                Vector2 dir = Vector2.UnitX.Rotate(start + step * i + MathHelper.PiOver2 + 0.1f);
                Vector2 velocity = dir * 2.5f;

                Balls[i].Physics.Velocity = velocity;
            }

            base.OnDie();
        }
    }
}

