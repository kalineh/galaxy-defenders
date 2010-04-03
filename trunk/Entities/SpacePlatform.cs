//
// SpacePlatform.cs
//

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    [Obsolete("replaced with CDecoration")]
    public class CSpacePlatform
        : CEntity
    {
        public CSpacePlatform(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Decoration/Platform1"), Color.White);
        }

#if XBOX360
        public CSpacePlatform()
    	{
    	}

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Decoration/Platform1"), Color.White);
        }
#endif

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }
    }

    [Obsolete("replaced with CDecoration")]
    public class CBigSpacePlatform
        : CEntity
    {
        public CBigSpacePlatform(CWorld world, Vector2 position)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Decoration/Platform1"), Color.White);
        }

#if XBOX360
        public CBigSpacePlatform()
    	{
    	}

        public void Init360(CWorld world, Vector2 position)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = position;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, "Textures/Decoration/Platform1"), Color.White);
        }
#endif

        public override void Update()
        {
            base.Update();
            if (IsOffScreenBottom())
                Delete();
        }
    }
}
