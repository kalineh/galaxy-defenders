//
// OneShotAnimation.cs
//

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class COneShotAnimation
        : CEntity
    {
        public struct Settings
        {
            public string TextureName { get; set; }
            public Vector2 Position { get; set; }
            public float Rotation { get; set; }
            public float Scale { get; set; }
            public float AnimationSpeed { get; set; }
            public int TileX { get; set; }
            public int TileY { get; set; }
            public Color Color { get; set; }
        }

        // TODO: scene graph :(
        public CEntity AttachToEntity { get; set; }

        public COneShotAnimation(CWorld world, Settings settings)
            : base(world)
        {
            Physics = new CPhysics();
            Physics.PositionPhysics.Position = settings.Position;
            Physics.AnglePhysics.Rotation = settings.Rotation;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, settings.TextureName), Color.White);
            Visual.TileX = settings.TileX;
            Visual.TileY = settings.TileY;
            Visual.AnimationSpeed = settings.AnimationSpeed;
            Visual.Scale = new Vector2(settings.Scale);
            Visual.Color = settings.Color;
            Visual.Update();
        }

#if XBOX360
        public COneShotAnimation()
        {
        }

        public void Init360(CWorld world, Settings settings)
        {
            base.Init360(world);

            Physics = new CPhysics();
            Physics.PositionPhysics.Position = settings.Position;
            Physics.AnglePhysics.Rotation = settings.Rotation;
            Visual = new CVisual(world, CContent.LoadTexture2D(world.Game, settings.TextureName), Color.White);
            Visual.TileX = settings.TileX;
            Visual.TileY = settings.TileY;
            Visual.AnimationSpeed = settings.AnimationSpeed;
            Visual.Scale = new Vector2(settings.Scale);
        }
#endif

        public override void Update()
        {
            base.Update();

            if (AttachToEntity != null && AttachToEntity.Physics != null)
                Physics.PositionPhysics.Position = AttachToEntity.Physics.PositionPhysics.Position;

            if (Visual.Frame >= Visual.TileX * Visual.TileY)
                Die();
        }
    }
}
