﻿//
// Debug.cs
//

using System;
using System.Collections.Generic;
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
            Vector3 scale;
            Quaternion rotation_unused;
            Vector3 translation_unused;
            Transform.Decompose(out scale, out rotation_unused, out translation_unused);

            float width = Width * 1.0f / scale.X;
            Vector2 perp = Vector.Normal().Perp();
            Vector2 half = perp * width * 0.5f;

            PrimitivesSample.PrimitiveBatch batch = GetPrimitiveBatch(game.GraphicsDevice);
            batch.Begin(PrimitiveType.TriangleList);

            batch.AddVertex(Vector2.Transform(Position - half, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position - half + Vector, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position + half, Transform), Color);

            batch.AddVertex(Vector2.Transform(Position + half, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position - half + Vector, Transform), Color);
            batch.AddVertex(Vector2.Transform(Position + half + Vector, Transform), Color);

            float _width = game.Resolution.X;
            float _height = game.Resolution.Y;
            Vector2 _half_x = Vector2.UnitX * _width * 0.5f * 2.0f;
            Vector2 _half_y = Vector2.UnitY * _height * 0.5f * 2.0f;

            //batch.AddVertex(Vector2.Transform(Position - _half_x + _half_y, Transform), Color.White);
            //batch.AddVertex(Vector2.Transform(Position - _half_x - _half_y, Transform), Color.Blue);
            //batch.AddVertex(Vector2.Transform(Position + _half_x + _half_y, Transform), Color.Green);

            //batch.AddVertex(Vector2.Transform(Position - _half_x - _half_y, Transform), Color.Red);
            //batch.AddVertex(Vector2.Transform(Position + _half_x - _half_y, Transform), Color.Gray);
            //batch.AddVertex(Vector2.Transform(Position + _half_x + _half_y, Transform), Color.Yellow);

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

    public class CDebugRenderText
        : CDebugRenderElement
    {
        public string Text { get; set; }
        public float Scale { get; set; }

        public CDebugRenderText()
        {
            Scale = 1.0f;
        }

        public override void Draw(CGalaxy game)
        {
            game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Transform);
            game.DefaultSpriteBatch.DrawString(game.DebugFont, Text, Position, Color.White, 0.0f, Vector2.Zero, 0.5f * Scale, SpriteEffects.None, 0.0f);
            game.DefaultSpriteBatch.End();
        }
    }

    public class CDebugRenderFullscreenQuad
        : CDebugRenderElement
    {
        public Vector2 Scale { get; set; }
        public Color ColorTL { get; set; }
        public Color ColorTR { get; set; }
        public Color ColorBR { get; set; }
        public Color ColorBL { get; set; }

        public CDebugRenderFullscreenQuad()
        {
            Scale = Vector2.One;
            ColorTL = Color.White;
            ColorTR = Color.White;
            ColorBR = Color.White;
            ColorBL = Color.White;
        }

        public override void Draw(CGalaxy game)
        {
            Vector3 scale;
            Quaternion rotation_unused;
            Vector3 translation_unused;
            Transform.Decompose(out scale, out rotation_unused, out translation_unused);

            float width = game.Resolution.X;
            float height = game.Resolution.Y;
            Vector2 half_x = Vector2.UnitX * width * 0.5f;
            Vector2 half_y = Vector2.UnitY * height * 0.5f;

            PrimitivesSample.PrimitiveBatch batch = GetPrimitiveBatch(game.GraphicsDevice);
            batch.Begin(PrimitiveType.TriangleList);

            Position = Vector2.Zero;

            batch.AddVertex(Vector2.Transform(Position - half_x + half_y, Transform), Color.White);
            batch.AddVertex(Vector2.Transform(Position - half_x - half_y, Transform), Color.Blue);
            batch.AddVertex(Vector2.Transform(Position + half_x + half_y, Transform), Color.Green);

            batch.AddVertex(Vector2.Transform(Position - half_x - half_y, Transform), Color.Red);
            batch.AddVertex(Vector2.Transform(Position + half_x - half_y, Transform), Color.Gray);
            batch.AddVertex(Vector2.Transform(Position + half_x + half_y, Transform), Color.Yellow);

            batch.End();
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

        static public void Circle(Matrix transform, Vector2 position, float radius, float width, Color color)
        {
            const float Segments = 14.0f;
            const float Step = MathHelper.TwoPi / Segments;
            for (int i = 0; i < Segments; ++i)
            {
                Vector2 a = position + new Vector2((float)Math.Cos(Step * (i + 0)), (float)Math.Sin(Step * (i + 0))) * radius;
                Vector2 b = position + new Vector2((float)Math.Cos(Step * (i + 1)), (float)Math.Sin(Step * (i + 1))) * radius;

                Line(transform, a, b - a, width, color);
            }
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

        static public void FullscreenQuad(Matrix transform, Vector2 position, Vector2 vector, Color color)
        {
            RenderElements.Add(
                new CDebugRenderFullscreenQuad() {
                    Transform = transform,
                    Position = position,
                    ColorTL = color
                }
            );
        }

        static public void Text(Matrix transform, Vector2 position, string text, Color color)
        {
            RenderElements.Add(
                new CDebugRenderText() {
                    Transform = transform,
                    Position = position,
                    Scale = 1.0f,
                    Color = color,
                    Text = text,
                }
            );
        }

        static public void Text(Matrix transform, Vector2 position, string text, float scale, Color color)
        {
            RenderElements.Add(
                new CDebugRenderText() {
                    Transform = transform,
                    Position = position,
                    Scale = scale,
                    Color = color,
                    Text = text,
                }
            );
        }

        static public void Render(CGalaxy game)
        {
            foreach (CDebugRenderElement element in RenderElements)
            {
                element.Draw(game);
            }

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
            Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, text, position, Color.White); 
        }
    }
}
