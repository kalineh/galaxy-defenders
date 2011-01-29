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
        public float DepthOffset { get; set; }
        public float Rotation { get; set; }
        private CDecoration Preloaded { get; set; }

        public override void Initialize(CWorld world)
        {
            CDecoration decoration = new CDecoration();
            decoration.Initialize(world);
            decoration.Physics.Position = Position;
            decoration.Physics.Rotation = MathHelper.ToRadians(Rotation);
            decoration.TextureName = TextureName;
            decoration.DepthOffset = DepthOffset;

            // NOTE: must be called after DepthOffset is set
            decoration.InitializeTexture();

            Preloaded = decoration;
        }

        public override void Update(CWorld world)
        {
            //decoration.UpdateTexture();
            world.EntityAdd(Preloaded);
        }
    }
}
