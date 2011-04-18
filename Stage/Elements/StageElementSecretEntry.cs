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
        private CWorld World { get; set; }
        public string SecretStage { get; set; }
        public bool Expired { get; set; }

        public override void Initialize(CWorld world)
        {
            // nothing needed
        }

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
            float range = 64.0f;
            if (len < range * range)
            {
                World.StartSecretStageEntry(SecretStage, Position);
                // TODO: better sound effect (too low volume)
                CAudio.PlaySound("SecretStageEnter");
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


