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
            Vector2 upper = Center - Dimensions / 2;
            Vector2 lower = Center - Dimensions / 2;

            // TODO: correct row/column naming and usage when brain function
            float row_width = Dimensions.Y / (float)Rows;
            float column_width = Dimensions.X / (float)Columns;

            while (entities.MoveNext())
            {
                CEntity entity = entities.Current;

                if (entity.Collision == null)
                    continue;

                if (entity.Collision.Enabled == false)
                    continue;

                Vector2 local = entity.Physics.PositionPhysics.Position - Center;
                int x = (int)(local.X % (float)Rows);
                int y = (int)(local.Y % (float)Columns);

                int row = Math.Max(0, Math.Min(x, Rows - 1));
                int col = Math.Max(0, Math.Min(y, Columns - 1));

                Entities[row][col].Add(entity);

                // TODO: hack: put all the entities in surrounding grid too
                if (row + 1 < Rows) Entities[row + 1][col].Add(entity);
                if (row > 0) Entities[row - 1][col].Add(entity);
                if (col + 1 < Columns) Entities[row][col + 1].Add(entity);
                if (col > 0) Entities[row][col - 1].Add(entity);
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
    }
}
