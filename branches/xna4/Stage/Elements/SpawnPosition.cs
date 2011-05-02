//
// SpawnPosition.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public abstract class CSpawnPosition
    {
        public abstract Vector2 GetSpawnPosition(CWorld world);
    };

    public class CSpawnPositionRandom
        : CSpawnPosition
    {
        public Vector2 BasePosition { get; set; }

        public CSpawnPositionRandom()
        {
            BasePosition = Vector2.Zero;
        }

        public CSpawnPositionRandom(Vector2 base_position)
        {
            BasePosition = base_position;
        }

        public override Vector2 GetSpawnPosition(CWorld world)
        {
            Vector2 camera = world.GameCamera.Position.ToVector2();
            Vector2 size = world.GameCamera.GetBottomRight() - world.GameCamera.GetTopLeft();
            Vector2 spawn_base = new Vector2(camera.X, camera.Y - size.Y - 50.0f);
            Vector2 spawn_random = new Vector2(world.Random.NextFloat() * size.X - size.X * 0.5f, world.Random.NextFloat() * -50.0f);
            return BasePosition + spawn_base + spawn_random;
        }
    };

    public class CSpawnPositionFixed
        : CSpawnPosition
    {
        public Vector2 Position { get; set; }

        public override Vector2 GetSpawnPosition(CWorld world)
        {
            return Position;
        }
    };
}
