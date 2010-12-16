//
// QuadTree.cs
//

using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;

// TODO: type-type mapping for collision
// TODO: collision results retrieval
// TODO: !!! cache bounding objects

namespace Galaxy
{
    public class CQuadTreeNode
    {
        private CQuadTreeNode Parent { get; set; }
        private List<CQuadTreeNode> Children { get; set; }
        private List<CEntity> Entities { get; set; }
        private List<CEntity> QueueRemove { get; set; }
        private List<CEntity> QueuePushUp { get; set; }
        private CollisionAABB Box { get; set; }

        public CQuadTreeNode(CQuadTreeNode parent, Vector2 position, Vector2 size)
        {
            Parent = parent;
            Children = new List<CQuadTreeNode>();
            Entities = new List<CEntity>();
            QueueRemove = new List<CEntity>();
            Box = new CollisionAABB(position - size * 0.5f, size);
        }

        public void Insert(CEntity entity)
        {
            Entities.Add(entity);
        }

        public void Remove(CEntity entity)
        {
            Entities.Remove(entity);

            foreach (CQuadTreeNode child in Children)
                child.Remove(entity);
        }

        public bool Contains(CEntity entity)
        {
            if (entity.Physics == null)
                return false;

            if (entity.Collision == null)
                return false;

            return Box.Contains(entity.Physics.Position, entity.GetRadius());
        }

        private void MakeChildren()
        {
            Vector2 size = Box.Size;
            Children.Add(new CQuadTreeNode(this, Box.Position + Box.Size * new Vector2(0.25f, 0.25f), Box.Size * 0.5f));
            Children.Add(new CQuadTreeNode(this, Box.Position + Box.Size * new Vector2(0.75f, 0.25f), Box.Size * 0.5f));
            Children.Add(new CQuadTreeNode(this, Box.Position + Box.Size * new Vector2(0.25f, 0.75f), Box.Size * 0.5f));
            Children.Add(new CQuadTreeNode(this, Box.Position + Box.Size * new Vector2(0.75f, 0.75f), Box.Size * 0.5f));
        }

        public void Update()
        {
            // expand if too many entities
            if (Children.Count == 0 && Entities.Count > 7)
                MakeChildren();

            // remove if no need for child nodes
            if (Entities.Count == 0 && Children.Count != 0)
            {
                if (Children[0].Children.Count == 0 && Children[0].Entities.Count == 0 &&
                    Children[1].Children.Count == 0 && Children[1].Entities.Count == 0 &&
                    Children[2].Children.Count == 0 && Children[2].Entities.Count == 0 &&
                    Children[3].Children.Count == 0 && Children[3].Entities.Count == 0 )
                {
                    Children.Clear();
                }
            }

            // move entities around tree
            foreach (CEntity entity in Entities)
            {
                // root node will have all non-contained entities
                if (Parent != null)
                {
                    // move to parent if entity doesnt fit in this node anymore
                    if (!Contains(entity))
                    {
                        Parent.Insert(entity);
                        QueueRemove.Add(entity);
                    }
                }

                // if entity is contained in child node, move to that node
                foreach (CQuadTreeNode child in Children)
                {
                    if (child.Contains(entity))
                    {
                        child.Insert(entity);
                        QueueRemove.Add(entity);
                        break;
                    }
                }
            }

            // remove queued
            foreach (CEntity entity in QueueRemove)
            {
                Entities.Remove(entity);
            }
            QueueRemove.Clear();

            // update children nodes
            foreach (CQuadTreeNode child in Children)
            {
                child.Update();
            }
        }

        // TODO: replace all of this with an IEnumerable for CQuadTreeNode? (linq performance problem?)
        public void Collide()
        {
            foreach (CEntity outer in Entities)
            {
                CollideRecurse(outer);
            }

            foreach (CQuadTreeNode child in Children)
            {
                child.Collide();
            }
        }

        private void CollideImpl(CEntity outer)
        {
            foreach (CEntity inner in Entities)
            {
                if (inner == outer)
                    continue;

                if (inner.Collision == null || outer.Collision == null)
                    continue;

                if (inner.Collision.Enabled == false || outer.Collision.Enabled == false)
                    continue;

                if (outer.GetType() == inner.GetType())
                {
                    if (outer.Collision.IgnoreSelfType && inner.Collision.IgnoreSelfType)
                        continue;
                }

                if (outer.Collision.Intersects(inner.Collision))
                {
                    Type[] types = new Type[] { null };
                    object[] parameters = new object[] { null };

                    // TODO: something proper
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

        private void CollideRecurse(CEntity outer)
        {
            CollideImpl(outer);

            foreach (CQuadTreeNode child in Children)
            {
                child.CollideRecurse(outer);
            }
        }

        private CQuadTreeNode FindParentNodeForEntity(CEntity entity)
        {
            CQuadTreeNode node = Parent ?? this;
            while (node != null)
            {
                if (node.Contains(entity))
                    return FindParentNodeForEntity(entity);
            }

            return node;
        }

        public void Draw(Matrix transform, Color color)
        {
            CDebugRender.Box(transform, Box.Position + Box.Size * 0.5f, Box.Size, 1.0f, color);
            CDebugRender.Text(transform, Box.Position + Box.Size * 0.5f, Entities.Count.ToString(), Color.White);

            foreach (CEntity entity in Entities)
            {
                if (entity.Physics == null || entity.Collision == null)
                    continue;

                CDebugRender.Box(transform, entity.Physics.Position, Vector2.One * entity.GetRadius(), 1.0f, Color.White);
            }

            foreach (CQuadTreeNode child in Children)
            {
                child.Draw(transform, new Color(color.R, color.G, color.B, color.A * 0.95f));
            }
        }

        public int CountTotalChildren()
        {
            int result = Children.Count;

            foreach (CQuadTreeNode child in Children)
                result += child.CountTotalChildren();

            return result;
        }
    }

    public class CQuadTree
    {
        public CWorld World { get; set; }
        public CQuadTreeNode Root { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Dimensions { get; set; }
        public List<CEntity> QueueInsert { get; set; }
        public List<CEntity> QueueRemove { get; set; }

        public CQuadTree(CWorld world, Vector2 center, Vector2 dimensions)
        {
            World = world;
            Center = center;
            Dimensions = dimensions;
            Root = new CQuadTreeNode(null, center, dimensions);
            QueueInsert = new List<CEntity>();
            QueueRemove = new List<CEntity>();
        }

        public void Update()
        {
            Root.Update();

            foreach (CEntity entity in QueueInsert)
                Root.Insert(entity);
            QueueInsert.Clear();

            foreach (CEntity entity in QueueRemove)
                Root.Remove(entity);
            QueueRemove.Clear();
        }

        public void Collide()
        {
            Root.Collide();
        }

        public void Insert(CEntity entity)
        {
            QueueInsert.Add(entity);
        }

        public void Remove(CEntity entity)
        {
            QueueRemove.Add(entity);
        }

        public void Draw()
        {
            Root.Draw(World.GameCamera.WorldMatrix, Color.Green);
        }
    }
}
