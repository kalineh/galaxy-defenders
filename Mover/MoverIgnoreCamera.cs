//
// MoverIgnoreCamera.cs
//

using System;
using Microsoft.Xna.Framework;

namespace Galaxy
{
    public class CMoverIgnoreCamera
        : CMover
    {
        public CMoverIgnoreCamera()
        {
        }

        public override void Move(CEntity entity)
        {
            if (entity.World.Game.State.GetType() != typeof(CStateGame))
                return;

            entity.Physics.PositionPhysics.Position += entity.World.Game.StageDefinition.ScrollSpeed * -Vector2.UnitY;
        }
    }
}

