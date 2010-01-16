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
        public override Vector2 GetSpawnPosition(CWorld world)
        {
            Viewport viewport = world.Game.GraphicsDevice.Viewport;
            Vector2 spawn_base = new Vector2(viewport.X, viewport.Y - 50.0f);
            Vector2 spawn_random = new Vector2(world.Random.NextFloat() * viewport.Width, world.Random.NextFloat() * -50.0f);
            return spawn_base + spawn_random;
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
