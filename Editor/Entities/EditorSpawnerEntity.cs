//
// EditorSpawnerEntity.cs
//

using System;
using System.Drawing.Design;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    namespace Editor
    {
        using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

        /// <summary>
        /// Editor entity for CSpawnerEntity.
        /// </summary>
        [TypeConverter(typeof(CSpawnerEntityConverter))]
        public class CSpawnerEntity
            : CEntity
        {
            [CategoryAttribute("Core")]
            [EditorAttribute(typeof(Editor.CEntityTypeSelector), typeof(UITypeEditor))]
            public Type Type { get; set; }

            [CategoryAttribute("Core")]
            public Vector2 Position
            {
                get { return Physics.PositionPhysics.Position; }
                set { Physics.PositionPhysics.Position = value; }
            }

            [CategoryAttribute("Mover")]
            [EditorAttribute(typeof(Editor.CEntityMoverPresetSelector), typeof(UITypeEditor))]
            public new CMover Mover { get; set; }

            [CategoryAttribute("Mover")]
            public float MoveSpeed { get; set; }

            [CategoryAttribute("Spawn")]
            public int SpawnCount { get; set; }

            [CategoryAttribute("Spawn")]
            public float SpawnInterval { get; set; }

            // TODO: replace me and use positional system in-game
            public int StartTime { get; set; }

            public CEntity PreviewEntity { get; set; }

            public CSpawnerEntity(CWorld world, Galaxy.CSpawnerEntity element)
                : base(world, "Editor.CSpawnerEntityStatic")
            {
                Physics = new CPhysics();

                // TODO: how to get enemy texture from typename? :|
                // TODO: not this? 

                Type type = Assembly.GetAssembly(typeof(CEntity)).GetType(element.Type.FullName);
                CEntity visual_get = Activator.CreateInstance(type, new object[] { world, Vector2.Zero }) as CEntity;
                Visual = new CVisual(CContent.LoadTexture2D(world.Game, visual_get.Visual.Texture.Name), visual_get.Visual.Color);

                // TODO: only supports CSpawnPositionFixed, CSpawnTimerInterval, CMoverSequence
                Type = element.Type;
                Position = ((Galaxy.CSpawnPositionFixed)element.SpawnPosition).Position;
                SpawnCount = element.SpawnCount;
                SpawnInterval = ((Galaxy.CSpawnTimerInterval)element.SpawnTimer).Interval;
                Mover = element.CustomMover;

                // TODO: not this hack
                MoveSpeed = 1.0f;
                if (Mover as Galaxy.CMoverSequence != null)
                {
                    MoveSpeed = ((Galaxy.CMoverSequence)Mover).SpeedMultiplier;
                }
                if (Mover as Galaxy.CMoverFixedVelocity != null)
                {
                    MoveSpeed = ((Galaxy.CMoverFixedVelocity)Mover).SpeedMultiplier;
                }
            }

            public override float GetRadius()
            {
                if (Visual == null)
                    return 15.0f;
                float texture = Math.Max(Visual.Texture.Width, Visual.Texture.Height);
                float scale = Math.Max(Visual.Scale.X, Visual.Scale.Y);
                return texture * scale * 0.5f;
            }

            public void Refresh()
            {
            }
        }
    }
}
