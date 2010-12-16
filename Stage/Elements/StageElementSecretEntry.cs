//
// StageElementSecretEntry.cs
//

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CStageElementSecretEntry
        : CStageElement
    {
        public CWorld World { get; set; }
        public string SecretStage { get; set; }
        public bool Expired { get; set; }

        public override void Update(CWorld world)
        {
            World = world;

            CShip ship = world.GetNearestShip(Position);
            if (ship == null)
                return;
            if (ship.Physics == null)
                return;

            Vector2 ofs = ship.Physics.Position - Position;
            float len = ofs.LengthSquared();
            if (len < 32.0f * 32.0f)
            {
                World.StartSecretStageEntry(SecretStage, Position);
                Expired = true;
            }
        }

        public override bool IsExpired()
        {
            if (World.IgnoreSecrets)
                return true;

            if (Expired)
                return true;

            if (!IsInScreen())
                return true;

            return false;
        }

        public bool IsInScreen()
        {
            return World.GameCamera.IsInside(Position, 128.0f);
        }

    }
}


