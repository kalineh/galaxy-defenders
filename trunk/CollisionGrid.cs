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

                float x = 0.0f;
                float y = 0.0f;

                CollisionAABB aabb = entity.Collision as CollisionAABB;
                CollisionCircle circle = entity.Collision as CollisionCircle;

                if (aabb != null)
                {
                    x = aabb.Size.X * 0.5f;
                    y = aabb.Size.Y * 0.5f;
                }
                else if (circle != null)
                {
                    x = circle.Radius;
                    y = circle.Radius;
                }

                Vector2 halfsize = new Vector2(x, y);
                Vector2 ul = local - halfsize;
                Vector2 br = local + halfsize;

                int min_x = (int)(ul.X / column_width);
                int min_y = (int)(ul.Y / row_width);
                int max_x = (int)(br.X / column_width);
                int max_y = (int)(br.Y / row_width);

                min_x = Math.Max(0, Math.Min(min_x, Columns - 1));
                min_y = Math.Max(0, Math.Min(min_y, Rows - 1));
                max_x = Math.Max(0, Math.Min(max_x, Columns - 1));
                max_y = Math.Max(0, Math.Min(max_y, Rows - 1));

                for (int j = min_y; j <= max_y; ++j)
                {
                    for (int i = min_x; i <= max_x; ++i)
                    {
                        Entities[j][i].Add(entity);
                    }
                }
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
                    Vector2 grid_topleft = new Vector2(column_width * x, row_width * y);
                    Vector2 screen_topleft = Dimensions * 0.5f;
                    Vector2 grid_size = new Vector2(column_width, row_width);
                    Vector2 grid_halfsize = grid_size * 0.5f;
                    Vector2 local_center = Center - screen_topleft + grid_topleft;

                    int n = Entities[y][x].Count;

                    CDebugRender.Box(transform, local_center, grid_size, 1.0f, color);
                    CDebugRender.Text(transform, local_center, n.ToString(), 1.5f, color);

                    //Vector2 p = Center - Dimensions / 2.0f + new Vector2(column_width * x, row_width * y);
                    //CDebugRender.Box(transform, center, new Vector2(column_width, row_width), 1.0f, color);

                    foreach (CEntity entity in Entities[y][x])
                    {
                        CollisionCircle circle = entity.Collision as CollisionCircle;
                        CollisionAABB box = entity.Collision as CollisionAABB;
                        if (circle != null)
                        {
                            // TODO: circle debug render!
                            CDebugRender.Box(transform, circle.Position, circle.Radius * Vector2.One * 2.0f, 1.0f, XnaColor.Blue);
                        }
                        if (box != null)
                        {
                            CDebugRender.Box(transform, box.Position + box.Size * 0.5f, box.Size, 1.0f, XnaColor.Red);
                        }
                    }
                }
            }

        }

    }
}
