using System.Collections.Generic;
using System.Linq;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering;

namespace Xenko.DebugDrawer
{
    internal class ShapeCollection
    {
        private static readonly Vector2 Zero = Vector2.Zero;
        private readonly Color _color;

        private readonly object _geometryKey = new object();
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Model _model;
        private readonly ISet<IShape> _shapes;

        public ShapeCollection(Color color, GraphicsDevice graphicsDevice, Model model)
        {
            _color = color;
            _graphicsDevice = graphicsDevice;
            _model = model;
            _shapes = new HashSet<IShape>();
        }

        public void Add(IShape geometry)
        {
            lock (_geometryKey)
            {
                _shapes.Add(geometry);
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
            _model.Meshes = new List<Mesh> {mesh};
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
    }
}