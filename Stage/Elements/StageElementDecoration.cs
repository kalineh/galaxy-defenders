//
// StageElementDecoration.cs
//

using Microsoft.Xna.Framework;
using System.ComponentModel;
using System;
using System.Linq;

namespace Galaxy
{
    public class CStageElementDecoration
        : CStageElement
    {
        public string TextureName { get; set; }

        public override void Update(CWorld world)
        {
            CDecoration decoration = new CDecoration();
            decoration.Initialize(world);
            decoration.Physics.PositionPhysics.Position = Position;
            decoration.TextureName = TextureName;

            //decoration.UpdateTexture();

            world.EntityAdd(decoration);
        }
    }
}
