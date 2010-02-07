﻿//
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

            public CSpawnerEntity(CWorld world, Galaxy.CSpawnerEntity element)
                : base(world, "Editor.CSpawnerEntityStatic")
            {
                Physics = new CPhysics();
                Visual = new CVisual(CContent.LoadTexture2D(world.Game, "Textures/Enemy/Turret"), XnaColor.White);

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
            }

            public override float GetRadius()
            {
                return 15.0f;
            }

            public void Refresh()
            {
            }
        }
    }
}
