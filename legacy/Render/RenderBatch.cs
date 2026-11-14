//
// RenderBatch.cs
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxy
{
    public class CRenderBatch
    {
        public class CRenderElement
        {
            public Matrix model;
            public Texture2D texture;
            public Vector4 texture_coordinates;
            public Color color;
        }

        public class CRenderLayer
        {
            public ELayers Layer { get; private set; }
            public float Depth { get; private set; }
            public List<CRenderElement> Elements { get; private set; }

            public CRenderLayer(ELayers layer, float depth)
            {
                Layer = layer;
                Depth = depth;
                Elements = new List<CRenderElement>();
            }
        }

        public CGalaxy Game { get; set; }
        public List<CRenderLayer> Layers;

        public CRenderBatch(CGalaxy game)
        {
            const float LayerIncrement = 0.05f;

            Game = game;
            Layers = new List<CRenderLayer>();
            Layers.Add(new CRenderLayer(ELayers.Bottom, 0.0f));
            Layers.Add(new CRenderLayer(ELayers.Background, LayerIncrement * 1));
            Layers.Add(new CRenderLayer(ELayers.Decoration, LayerIncrement * 2));
            Layers.Add(new CRenderLayer(ELayers.Static, LayerIncrement * 3));
            Layers.Add(new CRenderLayer(ELayers.Enemy, LayerIncrement * 4));
            Layers.Add(new CRenderLayer(ELayers.Entity, LayerIncrement * 5));
            Layers.Add(new CRenderLayer(ELayers.Player, LayerIncrement * 6));
            Layers.Add(new CRenderLayer(ELayers.Weapons, LayerIncrement * 7));
            Layers.Add(new CRenderLayer(ELayers.Effects, LayerIncrement * 8));
            Layers.Add(new CRenderLayer(ELayers.UI, LayerIncrement * 9));
            Layers.Add(new CRenderLayer(ELayers.FullScreen, LayerIncrement * 10));
            Layers.Add(new CRenderLayer(ELayers.Top, 1.0f));
        }

        public void Queue(ELayers layer, Matrix model, Texture2D texture, Vector4 texture_coordinates, Color color)
        {
            CRenderLayer render_layer = Layers.Find(rl => rl.Layer == layer);
            CRenderElement render_element = new CRenderElement() {
                model = model,
                texture = texture,
                texture_coordinates = texture_coordinates,
                color = color
            };
            render_layer.Elements.Add(render_element);
        }

        public void Render(CCamera camera)
        {
            // FOREACH LAYER
            //   SORT ELEMENTS
            //   FOREACH ELEMENT
            //     GENERATE WVP
            //     FOREACH EFFECT
            //       SET SHADER PARAMS
            //       RENDER

            /*
        Matrix world = Matrix.CreateRotationX( tilt ) *
            Matrix.CreateRotationY( tilt );

        Matrix view = Matrix.CreateLookAt( new Vector3( 0, 0, 5 ), Vector3.Zero,
            Vector3.Up );

        Matrix projection = Matrix.CreatePerspectiveFieldOfView(
            (float)Math.PI / 4.0f,  // 2 PI Radians is 360 degrees, so this is 45 degrees.
            (float)graphics.GraphicsDevice.Viewport.Width /
            (float)graphics.GraphicsDevice.Viewport.Height,
            1.0f, 100.0f );
             * */

            // WORLD: batch object transform
            // VIEW: camera
            // PROJECTION: camera

            /*


            foreach (Effect in Queue)
            {
            }

        worldViewProjection = world * view * projection;

uniform extern float4x4 WorldViewProjection : WORLDVIEWPROJECTION;
uniform extern texture UserTexture;

            Effect.Parameters["ViewportSize"].SetValue(new Vector2(800.0f, 600.0f));
            Effect.Parameters["ModelMatrix"].SetValue(new Vector2(800.0f, 600.0f));
            //SamplerState state;
              
             * */
        }
    }
}
