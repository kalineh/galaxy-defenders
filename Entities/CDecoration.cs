//
// SpacePlatform.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CDecoration
        : CEntity
    {
        public string TextureName { get; set; }

        public CDecoration(CWorld world, Vector2 position, string texture_name)
            : base(world, "Decoration")
        {
            TextureName = texture_name;
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Decoration/" + TextureName), Color.White);
        }

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }
    }
}
