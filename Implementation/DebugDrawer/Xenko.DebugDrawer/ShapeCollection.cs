using System;
using System.Collections.Generic;
using System.Linq;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering;
using Xenko.DebugDrawer.Shapes;
using Buffer = SiliconStudio.Xenko.Graphics.Buffer;

namespace Xenko.DebugDrawer
{
    internal class ShapeCollection
    {
        private static readonly Vector2 Zero = Vector2.Zero;
        private readonly Color _color;

        private readonly object _geometryKey = new object();
        private readonly GraphicsDevice _graphicsDevice;
        private readonly ISet<AShape> _shapes;
        private readonly Material _material;

        public Entity Entity { get; }

        public ShapeCollection(Color color, GraphicsDevice graphicsDevice)
        {
            _color = color;
            _graphicsDevice = graphicsDevice;
            _shapes = new HashSet<AShape>();

            var model = new Model();
            _material = Materials.CreateDebugMaterial(color, true, graphicsDevice);
            model.Materials.Add(_material);
            var modelComponent = new ModelComponent(model);
            Entity = new Entity
            {
                Name = color.ToString(),
                Components = { modelComponent }
            };
        }

        public void Add(AShape shape)
        {
            if(shape == null) throw new ArgumentNullException(nameof(shape));

            lock (_geometryKey)
            {
                _shapes.Add(shape);
                UpdateMesh();
            }
        }
        
        public void Remove(AShape shape)
        {
            if (shape == null) throw new ArgumentNullException(nameof(shape));

            lock (_geometryKey)
            {
                _shapes.Remove(shape);
                UpdateMesh();
            }
        }

        public void UpdateMesh()
        {
            List<VertexPositionColorTexture> vertices = new List<VertexPositionColorTexture>();
            var indices = new LinkedList<int>();
            lock (_shapes)
            {
                foreach (var line in _shapes.SelectMany(shape => shape.Lines))
                {
                    int startIndex = OptionalInsert(line.Start, vertices);
                    int endIndex = OptionalInsert(line.End, vertices);
                    indices.AddLast(startIndex);
                    indices.AddLast(endIndex);
                }
            }

            var vertexBuffer = Buffer.Vertex.New(_graphicsDevice, vertices.ToArray());
            var indexBuffer = Buffer.Index.New(_graphicsDevice, indices.ToArray());
            var meshDraw = new MeshDraw
            {
                PrimitiveType = PrimitiveType.LineStrip,
                VertexBuffers = new[]
                {
                    new VertexBufferBinding(vertexBuffer, VertexPositionColorTexture.Layout, vertices.Count)
                },
                IndexBuffer = new IndexBufferBinding(indexBuffer, true, indices.Count),
                DrawCount = indices.Count
            };

            var mesh = new Mesh {Draw = meshDraw};
            Entity.Get<ModelComponent>().Model = new Model
            {
                Meshes = {mesh},
                Materials = { _material}
            };
        }

        private int OptionalInsert(Vector3 point, List<VertexPositionColorTexture> verticeList)
        {
            var vertex = new VertexPositionColorTexture(point, _color, Zero);
            int index = verticeList.IndexOf(vertex);
            if (index >= 0)
            {
                return index;
            }

            index = verticeList.Count;
            verticeList.Insert(index, vertex);
            return index;
        }

        public bool Contains(AShape aShape)
        {
            lock (_geometryKey)
            {
                return _shapes.Contains(aShape);
            }
        }
    }
}