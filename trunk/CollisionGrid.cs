//
// CollisionGrid.cs
//

using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// TODO: type-type mapping for collision
// TODO: collision results retrieval
// TODO: !!! cache bounding objects

namespace Galaxy
{
    using XnaColor = Microsoft.Xna.Framework.Graphics.Color;

    public class CCollisionGrid
    {
        public CWorld World { get; private set; }
        public List<List<List<CEntity>>> Entities { get; private set; }
        public Vector2 Center { get; set; }
        public Vector2 Dimensions { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public CCollisionGrid(CWorld world, Vector2 dimensions, int rows, int columns)
        {
            World = world;
            Entities = new List<List<List<CEntity>>>();
            Center = Vector2.Zero;
            Dimensions = dimensions;
            Rows = rows;
            Columns = columns;
        }

        public void Clear(Vector2 center)
        {
            Center = center;
            Entities = new List<List<List<CEntity>>>();

            for (int row = 0; row < Rows; row++)
            {
                Entities.Add(new List<List<CEntity>>());
                for (int col = 0; col < Columns; col++)
                {
                    Entities[row].Add(new List<CEntity>());
                }
            }
        }

        public void Insert(IEnumerator<CEntity> entities)
        {
            float column_width = Dimensions.X / (float)Columns;
            float row_width = Dimensions.Y / (float)Rows;

            Vector2 upper = Center - Dimensions / 2.0f;
            Vector2 half_grid = new Vector2(column_width, row_width) * 0.5f;

            while (entities.MoveNext())
            {
                CEntity entity = entities.Current;

                if (entity.Collision == null)
                    continue;

                if (entity.Collision.Enabled == false)
                    continue;

                Vector2 local = entity.Physics.PositionPhysics.Position - upper + half_grid;
                int x = (int)(local.X / column_width);
                int y = (int)(local.Y / row_width);

                int col = Math.Max(0, Math.Min(y, Columns - 1));
                int row = Math.Max(0, Math.Min(x, Rows - 1));

                Entities[col][row].Add(entity);

                // TODO: hack: put all the entities in surrounding grid too
                //if (row + 1 < Rows) Entities[row + 1][col].Add(entity);
                //if (row > 0) Entities[row - 1][col].Add(entity);
                //if (col + 1 < Columns) Entities[row][col + 1].Add(entity);
                //if (col > 0) Entities[row][col - 1].Add(entity);
            }
        }

        public void Collide()
        {
            Type[] types = new Type[] { null };
            object[] parameters = new object[] { null };

            foreach (List<List<CEntity>> row in Entities)
            {
                foreach (List<CEntity> column in row)
                {
                    foreach (CEntity outer in column)
                    {
                        foreach (CEntity inner in column)
                        {
                            if (outer.Collision == null)
                                continue;

                            if (inner.Collision == null)
                                continue;

                            if (outer.GetType() == inner.GetType())
                            {
                                if (outer.Collision.IgnoreSelfType && inner.Collision.IgnoreSelfType)
                                    continue;
                            }

                            if (outer.Collision.Intersects(inner.Collision))
                            {
                                System.Type inner_type = inner.GetType();
                                System.Type outer_type = outer.GetType();
                                types[0] = inner_type;
                                MethodInfo method = outer_type.GetMethod("OnCollide", types);

                                if (method == null)
                                    continue;

                                try
                                {
                                    parameters[0] = inner;
                                    method.Invoke(outer, parameters);
                                }
                                catch (Exception exception)
                                {
                                    throw exception.InnerException;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Draw(Matrix transform, XnaColor color)
        {
            float row_width = Dimensions.Y / (float)Rows;
            float column_width = Dimensions.X / (float)Columns;

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Vector2 p = Center - Dimensions / 2.0f + new Vector2(column_width * x, row_width * y);
                    CDebugRender.Box(transform, p, new Vector2(column_width, row_width), 1.0f, color);
                    int n = Entities[y][x].Count;
                    CDebugRender.Text(transform, p, n.ToString(), 1.5f, color);

                    foreach (CEntity entity in Entities[y][x])
                    {
                        CollisionCircle circle = entity.Collision as CollisionCircle;
                        CollisionAABB box = entity.Collision as CollisionAABB;
                        if (circle != null)
                        {
                            // TODO: circle debug render!
                            CDebugRender.Box(transform, circle.Position, circle.Radius * Vector2.One, 1.0f, XnaColor.Blue);
                        }
                        if (box != null)
                        {
                            CDebugRender.Box(transform, box.Position, box.Size, 1.0f, XnaColor.Red);
                        }
                    }
                }
            }

        }

    }
}
