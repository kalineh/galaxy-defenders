//
// TestStage.cs
//

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    namespace Stages
    {
        public class TestStage
        {
            public static CStageDefinition GenerateDefinition()
            {
                CStageDefinition stage = new CStageDefinition("TestStage");

                return stage;
            }

            public static void SinBallBoss(CEntity entity)
            {
                CSinBall boss = entity as CSinBall;

                boss.Health *= 15.0f;
                boss.BonusDrop = 30;
                boss.Visual.Scale *= 2.5f;
                boss.Mover = new CMoverSequence()
                {
                    Velocity = new List<Vector2>() {
                        new Vector2(0.0f, 1.0f),
                        new Vector2(0.0f, 0.0f),
                    },
                    Duration = new List<float>() {
                        3.5f,
                        0.0f,
                    },
                    VelocityLerpRate = 0.02f,
                };
                CollisionCircle collision = boss.Collision as CollisionCircle;
                collision.Radius *= 2.5f;
            }

        };
    }
}