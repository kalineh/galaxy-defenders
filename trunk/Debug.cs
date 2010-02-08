//
// Debug.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public abstract class CDebugRenderElement
    {
        private static PrimitivesSample.PrimitiveBatch PrimitiveBatch;
        public static PrimitivesSample.PrimitiveBatch GetPrimitiveBatch(GraphicsDevice device)
        {
            PrimitiveBatch = PrimitiveBatch ?? new PrimitivesSample.PrimitiveBatch(device);
            return PrimitiveBatch;
        }

        public Matrix Transform { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        public CDebugRenderElement()
        {
            Transform = Matrix.Identity;
            Position = Vector2.Zero;
            Color = Color.White;
        }

        public abstract void Draw(CGalaxy game);
    }

    public class CDebugRenderLine
        : CDebugRenderElement
    {
        public Vector2 Vector { get; set; }
        public float Width { get; set; }

        public CDebugRenderLine()
        {
            Vector = Vector2.UnitY;
            Width = 1.0f; 
        }

        public override void Draw(CGalaxy game)
        {
            Vector2 perp = Vector.Normal().Perp();
            Vector2 half = perp * Width * 0.5f;

            PrimitivesSample.PrimitiveBatch batch = GetPrimitiveBatch(game.GraphicsDevice);
            batch.Begin(PrimitiveType.TriangleList);

            batch.AddVertex(Vector2.Transform(Position - half, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position - half + Vector, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position + half, Transform), Color);

            batch.AddVertex(Vector2.Transform(Position + half, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position - half + Vector, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position + half + Vector, Transform), Color);

            batch.End();
        }
    }

    public class CDebugRenderBox
        : CDebugRenderElement
    {
        public Vector2 Scale { get; set; }
        public float LineWidth { get; set; }

        public CDebugRenderBox()
        {
            Scale = Vector2.One;
            LineWidth = 1.0f; 
        }

        public override void Draw(CGalaxy game)
        {
            // TODO: implement me!
            throw new NotImplementedException();
        }
    }

    public class CDebugRender
    {
        static public List<CDebugRenderElement> RenderElements { get; set; }

        static CDebugRender()
        {
            RenderElements = new List<CDebugRenderElement>();
        }

        static public void Box(Matrix transform, Vector2 position, Vector2 scale, float width, Color color)
        {
            Vector2 halfx = new Vector2(scale.X * 0.5f, 0.0f);
            Vector2 halfy = new Vector2(0.0f, scale.Y * 0.5f);
            Vector2 scalex = new Vector2(scale.X, 0.0f);
            Vector2 scaley = new Vector2(0.0f, scale.Y);
            Line(transform, position - halfx + halfy, -scaley, width, color);
            Line(transform, position - halfx - halfy, scalex, width, color);
            Line(transform, position + halfx - halfy, scaley, width, color);
            Line(transform, position + halfx + halfy, -scalex, width, color);
        }

        static public void Line(Matrix transform, Vector2 position, Vector2 vector)
        {
            Line(transform, position, vector, 1.0f, Color.White);
        }

        static public void Line(Vector2 position, Vector2 vector)
        {
            Line(Matrix.Identity, position, vector, 1.0f, Color.White);
        }

        static public void Line(Vector2 position, Vector2 vector, float width)
        {
            Line(Matrix.Identity, position, vector, width, Color.White);
        }

        static public void Line(Matrix transform, Vector2 position, Vector2 vector, float width, Color color)
        {
            RenderElements.Add(
                new CDebugRenderLine() {
                    Transform = transform,
                    Position = position,
                    Vector = vector,
                    Width = width,
                    Color = color
                }
            );
        }

        static public void Render(CGalaxy game)
        {
            RenderElements.ForEach(element => element.Draw(game));
            RenderElements.Clear();
        }
    }

    // TODO: remove me, make a debug string type
    public class CDebug
    {
        public CGalaxy Game { get; private set; }
        public SpriteFont Font { get; private set; }

        public CDebug(CGalaxy game)
        {
            Game = game;
        }

        public void DrawString(Vector2 position, string text)
        {
            Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, text, position, Color.White); 
        }
    }
}
